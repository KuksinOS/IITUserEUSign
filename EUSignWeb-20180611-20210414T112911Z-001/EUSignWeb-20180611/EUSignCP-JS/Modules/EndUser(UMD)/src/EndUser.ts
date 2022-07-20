//===============================================================================

import PromiseWorker from "./Common/PromiseWorker";
import * as EndUserError from "EndUserError";
import {CASettings, EndUserSettings} from "EndUserSettings";
import {EndUserOwnerInfo} from "EndUserOwnerInfo";

import * as euWorkerContent from "../Modules/build/lib/euscp.worker";
//var euWorkerContent = '';

//===============================================================================

/**
 * EndUser library object
 */
class EndUser {

//===============================================================================

    private m_worker: PromiseWorker;

//-------------------------------------------------------------------------------

    /**
     * Create library instance 
     * @param url The url for euscp.worker.js. Optional parameter.
     */
    constructor(url?: string) {
        this.m_worker = new PromiseWorker(euWorkerContent, url);
    }

//-------------------------------------------------------------------------------

    /**
     * Check library initialize status
     * @returns A Promise for the completion of which ever callback is executed.
     */
    public IsInitialized(): Promise <boolean> {
        return this.m_worker.postMessage(
            "IsInitialized", 
            Array.prototype.slice.call(arguments)
        );
    };

//-------------------------------------------------------------------------------

    /**
     * Load and initialize library 
     * @param settings The settings to initialize library.
     * @returns A Promise for the completion of which ever callback is executed.
     */
    public Initialize(settings: EndUserSettings): Promise <void> {
        return this.m_worker.postMessage(
            "Initialize", 
            Array.prototype.slice.call(arguments)
        );
    };

//-------------------------------------------------------------------------------

    /**
     * Check private key status
     * @returns A Promise for the completion of which ever callback is executed.
     */
    public IsPrivateKeyReaded(): Promise <boolean> {
        return this.m_worker.postMessage(
            "IsPrivateKeyReaded", 
            Array.prototype.slice.call(arguments)
        );
    };

//-------------------------------------------------------------------------------

    /**
     * Read private key from array of bytes
     * @param privateKey The private key in array of bytes.
     * @param password The private key password.
     * @param certs The private key certificates.
     * @param caCN The CA common name that issued private key certificates.
     * @returns A Promise for the completion of which ever callback is executed.
     */
    public ReadPrivateKeyBinary(privateKey: Uint8Array, password: string, 
        certs?: Array<Uint8Array>, caCN?: string): Promise <EndUserOwnerInfo> {
        return this.m_worker.postMessage(
            "ReadPrivateKeyBinary", 
            Array.prototype.slice.call(arguments)
        ); 
    };

//-------------------------------------------------------------------------------

    /**
     * Sign data using private key (The signature will not contain the signed data)
     * @param data The data to sign (Data in string will be converted to a byte array).
     * @param asBase64String The boolean value that specifies whether to encode a signature into a BASE64 string.
     * @returns A Promise for the completion of which ever callback is executed.
     */
    public SignData(data: Uint8Array | string, 
        asBase64String: boolean = false): Promise<Uint8Array | string> {
        return this.m_worker.postMessage(
            "SignData", 
            Array.prototype.slice.call(arguments)
        ); 
    };


//-------------------------------------------------------------------------------

    /**
     * Sign data using private key (The signature will contain the signed data)
     * @param appendCert The boolean value that specifies whether to append certificate into signature.
     * @param data The data to sign (Data in string will be converted to a byte array).
     * @param asBase64String The boolean value that specifies whether to encode a signature into a BASE64 string.
     * @returns A Promise for the completion of which ever callback is executed.
     */
    public SignDataInternal(appendCert: boolean, data: Uint8Array | string, 
        asBase64String: boolean = false): Promise<Uint8Array | string> {
        return this.m_worker.postMessage(
            "SignDataInternal", 
            Array.prototype.slice.call(arguments)
        ); 
    };

//-------------------------------------------------------------------------------

};

//===============================================================================

export {
    CASettings,
    EndUserError,
    EndUserSettings,
    EndUserOwnerInfo,
    EndUser
};

//===============================================================================
