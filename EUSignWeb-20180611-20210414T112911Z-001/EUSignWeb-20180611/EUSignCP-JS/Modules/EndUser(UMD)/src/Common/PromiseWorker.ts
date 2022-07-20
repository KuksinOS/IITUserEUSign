//===============================================================================

class WorkerError {
    code: number;
    message: string;

    constructor(code: number, message: string) {
        this.code = code;
        this.message = message;
    }
}

//===============================================================================

class WorkerRequest {
    id: number;
    cmd: string;
    params: Array<any>;
    origin: string;
    pathname: string;

    constructor(id: number, cmd: string, params: Array<any>) {
        this.id = id;
        this.cmd = cmd;
        this.params = params;
        this.origin = window.location.origin ? 
            window.location.origin : 
            (window.location.protocol + "//" + window.location.hostname +
                (window.location.port ? ':' + window.location.port: ''));
        this.pathname = window.location.pathname;
    }
}

//===============================================================================

class WorkerResponse {
    id: number;
    cmd: string;
    result: any;
    error?: WorkerError;

    constructor(id: number, cmd: string, result: any, error?: WorkerError) {
        this.id = id;
        this.cmd = cmd;
        this.result = result;
        this.error = error;
    }
}

//===============================================================================

interface IWorkerPromise {
  resolve:(data: any) => void;
  reject: (data: any) => void;
}

//===============================================================================

export default class PromiseWorker {

//-------------------------------------------------------------------------------

    private m_worker: Worker;
    private m_promises: Array<IWorkerPromise>;

    constructor(content:string, url?: string) {
        var pThis: PromiseWorker = this;

        this.m_worker = this.loadWorker(content, url);
        this.m_worker.onmessage = function(ev: MessageEvent): void {
            var data: WorkerResponse = ev.data;
            var p: IWorkerPromise = pThis.m_promises[data.id - 1];

            if (!p) {
                return;
            }

            delete pThis.m_promises[data.id - 1];

            if (data.error) {
                p.reject(data.error);
            } else {
                p.resolve(data.result);
            }
        };

        this.m_worker.onerror = function(ev: ErrorEvent): void {
            pThis.m_promises.forEach(function(promise: any): void {
                promise.reject(ev.error);
            });
            pThis.m_promises = new Array<IWorkerPromise>();
        };

        this.m_promises = new Array<IWorkerPromise>();
    }

//-------------------------------------------------------------------------------
   
    private loadWorker(content: string, url?: string): Worker {
        try {
            if (!content)
                throw 'No worker content';

            try {
                var _window: any = window;
                var _URL:any = _window.URL || _window.webkitURL;
                var blob: any;
                try {
                    var BlobBuilder = _window.BlobBuilder ||
                    _window.WebKitBlobBuilder ||
                    _window.MozBlobBuilder ||
                    _window.MSBlobBuilder;

                    blob = new BlobBuilder();
                    blob.append(content);
                    blob = blob.getBlob();
                } catch (e) {
                    blob = new Blob([content]);
                }

                return new Worker(_URL.createObjectURL(blob));
            } catch (e) {
                return new Worker("data:application/javascript," +
                    encodeURIComponent(content));
            }
        } catch (e) {
            if (!url) {
                throw Error("Inline worker is not supported");
            }

            return new Worker(url);
        }
    }

//-------------------------------------------------------------------------------

    public postMessage(cmd:string, params: Array<any>): Promise<any> {
        var pThis: PromiseWorker = this;

        var msg: WorkerRequest =
          new WorkerRequest(-1, cmd, params);
        var p: Promise<any> = new Promise<any>((resolve, reject) => {
            msg.id = pThis.m_promises.push({
                resolve: resolve,
                reject: reject
            });
        });

        this.m_worker.postMessage(msg);

        return p;
    }

//-------------------------------------------------------------------------------
    
}

//===============================================================================

export class PromiseWorkerInternal {

//-------------------------------------------------------------------------------

    private m_context: any;
    private m_initialized: boolean;

//-------------------------------------------------------------------------------
    
    constructor(context: any) {
        this.m_context = context;
        this.m_initialized = false;

        var pThis = this;
        var _self: any = self;
        _self.onmessage = function(msg: any): void {    
            pThis.onMessage(msg.data);
        }
    }

//-------------------------------------------------------------------------------
    
    private postMessage(id: number, cmd: string,
      result: any, error?: WorkerError): void {
        var response: WorkerResponse = new WorkerResponse(
            id, cmd, result, error);
        var _self: any = self;    
        _self.postMessage(response);
    }

//-------------------------------------------------------------------------------

    private onMessage(msg: WorkerRequest): void {
        var pThis:PromiseWorkerInternal = this;
        if (!this.m_initialized) {
            setTimeout(function() {
                pThis.onMessage(msg);
            }, 50);
            return;
        }

        var p: Promise<any> = this.m_context[msg.cmd].apply(
            this.m_context, msg.params);
        p.then(
            (result: any) => {
                pThis.postMessage(msg.id, msg.cmd, result);
            },
            (error: any) => {
                pThis.postMessage(msg.id, msg.cmd, null, error);
            },
        );
    }

//-------------------------------------------------------------------------------
    
    public initialize(): void {
        this.m_initialized = true;
    }

//-------------------------------------------------------------------------------

}

//===============================================================================
