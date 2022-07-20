//===============================================================================

importScripts(
    './src/ExtLibrary/euscp/euscpt.js', 
    './src/ExtLibrary/euscp/euscpm.js', 
    './src/ExtLibrary/euscp/euscp.js'
);

//===============================================================================

var s_loaded = false;
var s_endUser = null;
var s_origin = '';
var s_pathname = '';

//===============================================================================

function EUSignCPModuleInitialized(loaded) {
    s_loaded = loaded;
    s_endUser = new EndUser();
};

//===============================================================================

self.onmessage = function(msg) {
    if (!s_loaded) {
        setTimeout(function() {
            onmessage(msg);
        }, 50);
        return;
    }

    s_endUser.onMessage(msg.data);
};

//===============================================================================

var EndUser = function() {
    this.m_euSign = EUSignCP();
    this.m_settings = null;
    this.m_initialized = false;
};

//-------------------------------------------------------------------------------

EndUser.prototype.onMessage = function(msg) {
    var resp = {
        id: msg.id,
        cmd: msg.cmd,
        result: null,
        error: null
    };

    try {
        s_origin = msg.origin;
        s_pathname = msg.pathname;
        resp.result = this[msg.cmd].apply(this, msg.params);
    } catch (e) {
        if (!e.GetMessage) {
           e = this.m_euSign.MakeError(EU_ERROR_UNKNOWN);  
        }
        
        resp.error = {
            code: e.GetErrorCode(),
            message: e.GetMessage()
        };
    }

    postMessage(resp);
};

//-------------------------------------------------------------------------------

EndUser.prototype.makeURL = function(url) {
    if (url.indexOf('http://') == 0 ||
        url.indexOf('https://') == 0) {
        return url;
    }

    if ((url.indexOf('/') == 0) && s_origin) {
        return s_origin + url;
    }

    if (s_origin && s_pathname) {
        return s_origin + 
            s_pathname.substr(0, s_pathname.lastIndexOf('/')) + 
            '/' + url;
    }

    return url;
};

//-------------------------------------------------------------------------------

EndUser.prototype.DowloadData = function(url, dataType) {
    try {
        var xmlHttp = XMLHttpRequest ? 
            (new XMLHttpRequest()) : 
            new ActiveXObject("Microsoft.XMLHTTP");

        url = this.makeURL(url);

        xmlHttp.open("GET", url, false);
        if (dataType == 'binary')
            xmlHttp.responseType = 'arraybuffer';
        xmlHttp.send();
        if (xmlHttp.status != 200) {
            throw 'Download data error. URL - ' + 
                url + ', status - ' + xmlHttp.status;
        }

        switch (dataType) {
            case 'binary':
                return new Uint8Array(xmlHttp.response);
            
            case 'json':
                return JSON.parse(xmlHttp.responseText.replace(/\\'/g, "'"));

            default:
                return xmlHttp.responseText;
        }
    } catch (e) {
        console.log("EndUser.DowloadData error: " + e);
        this.m_euSign.RaiseError(EU_ERROR_DOWNLOAD_FILE);
    }
};

//-------------------------------------------------------------------------------

EndUser.prototype.GetCASettings = function(CACommonName) {
    if (!CACommonName)
        return null;
    
    var CAs = this.m_settings.CAs;
    for (var i = 0; i < CAs.length; i++) {
        var issuerCNs = CAs[i].issuerCNs;
        for (var j = 0; j < issuerCNs.length; j++) {
            if (issuerCNs[j] == CACommonName)
                return CAs[i];
        }
    }
    
    return null;
};

//-------------------------------------------------------------------------------

EndUser.prototype.SetSettings = function(CACommonName) {
    var euSign = this.m_euSign;
    var CAs = this.m_settings.CAs;
    var CASettings = this.GetCASettings(CACommonName);
    var CADefaultSettings = (CAs && (CAs.length > 0)) ? 
        CAs[0] : null;

    if (CACommonName && (CASettings == null)) {
        euSign.RaiseError(EU_ERROR_BAD_PARAMETER);
        return;
    }

    var offline = true;
    var useOCSP = false;
    var useCMP = false;

    offline = ((CASettings == null) || 
        (CASettings.address == "")) ?
        true : false;
    useOCSP = (!offline && (CASettings.ocspAccessPointAddress != ""));
    useCMP = (!offline && (CASettings.cmpAddress != ""));
   
    var settings = euSign.CreateTSPSettings();
    settings.SetGetStamps(true);
    if (CASettings.tspAddress != "") {
        settings.SetAddress(CASettings.tspAddress);
        settings.SetPort(CASettings.tspAddressPort);
    } else if (CADefaultSettings){
        settings.SetAddress(CADefaultSettings.tspAddress);
        settings.SetPort(CADefaultSettings.tspAddressPort);
    }
    euSign.SetTSPSettings(settings);

    settings = euSign.CreateOCSPSettings();
    if (useOCSP) {	
        settings.SetUseOCSP(true);
        settings.SetBeforeStore(true);
        settings.SetAddress(CASettings.ocspAccessPointAddress);
        settings.SetPort(CASettings.ocspAccessPointPort);
    }
    euSign.SetOCSPSettings(settings);

    settings = euSign.CreateCMPSettings();
    settings.SetUseCMP(useCMP);
    if (useCMP) {
        settings.SetAddress(CASettings.cmpAddress);
        settings.SetPort("80");
    }
    euSign.SetCMPSettings(settings);    
};

//-------------------------------------------------------------------------------

EndUser.prototype.SearchPrivateKeyCertificatesWithCMP = function(
    privateKey, password) {
    var euSign = this.m_euSign;
    var CAs = this.m_settings.CAs;
    var keyInfo = euSign.GetKeyInfoBinary(privateKey, password);
    var certs = null;
       
    for (var i = 0; i < CAs.length; i++) {
        var cmpAddress = CAs[i].cmpAddress;
        if (!cmpAddress || cmpAddress == "")
            continue;

        try {
            certs = euSign.GetCertificatesByKeyInfo(
                keyInfo, [cmpAddress + ":80"]);
        } catch (e) {
            var errorCode = e.GetErrorCode();
            if (errorCode == EU_ERROR_CERT_NOT_FOUND || 
                errorCode == EU_ERROR_TRANSMIT_REQUEST) {
                continue;
            }

            throw e;
        }

        return {
            "certs": certs, 
            "CACommonName": CAs[i].issuerCNs[0]
        };
    }

    return null;
};

//-------------------------------------------------------------------------------

EndUser.prototype.IsInitialized = function() {
    return this.m_initialized;
};

//-------------------------------------------------------------------------------

EndUser.prototype.Initialize = function(settings) {
    if (typeof settings.CAs === 'string') {
        settings.CAs = 
            this.DowloadData(settings.CAs, 'json');
    }

    if (typeof settings.CACertificates === 'string') {
        settings.CACertificates = 
            this.DowloadData(settings.CACertificates, 'binary');
    }

    this.m_settings = settings;

    var euSign = this.m_euSign;

    euSign.SetErrorMessageLanguage(settings.language);
    euSign.SetCharset(settings.encoding);
    euSign.SetJavaStringCompliant(true);

    euSign.Initialize();

    euSign.SetXMLHTTPProxyService(
        this.makeURL(settings.httpProxyServiceURL));

    euSign.InitializeMandatorySettings();

    var fs = euSign.CreateFileStoreSettings();
    fs.SetPath('');
    fs.SetSaveLoadedCerts(true);    
    euSign.SetFileStoreSettings(fs);

    if (settings.CACertificates != null)
        euSign.SaveCertificates(settings.CACertificates);

    var CAs = settings.CAs;
    var ocspMode = euSign.CreateOCSPAccessInfoModeSettings();
    ocspMode.SetEnabled(true);
    euSign.SetOCSPAccessInfoModeSettings(ocspMode);
    var ocspAccessInfo = euSign.CreateOCSPAccessInfoSettings();
    for (var i = 0; i < CAs.length; i++) {
        ocspAccessInfo.SetAddress(CAs[i].ocspAccessPointAddress);
        ocspAccessInfo.SetPort(CAs[i].ocspAccessPointPort);

        for (var j = 0; j < CAs[i].issuerCNs.length; j++) {
            ocspAccessInfo.SetIssuerCN(CAs[i].issuerCNs[j]);
            euSign.SetOCSPAccessInfoSettings(ocspAccessInfo);
        }
    }

    var _addDNSName = function(uri, dnsNames) {
        if (uri == '')
            return;

        uri = (uri.indexOf("://") > -1) ? 
            uri.split('/')[2] : 
            uri.split('/')[0];

        if (dnsNames.indexOf(uri) >= 0)
            return;

        dnsNames.push(uri);
    }; 

    euSign.SetXMLHTTPDirectAccess(settings.directAccess);
    if (settings.directAccess) {
        var directAccessURLs = Array();
        _addDNSName('czo.gov.ua', directAccessURLs);
        for (var i = 0; i < CAs.length; i++) {
            if (!CAs[i].directAccess)
                continue;

            _addDNSName(CAs[i].address, directAccessURLs);
            _addDNSName(CAs[i].tspAddress, directAccessURLs);
            _addDNSName(CAs[i].cmpAddress, directAccessURLs);
            _addDNSName(CAs[i].ocspAccessPointAddress,
                directAccessURLs);
        }

        directAccessURLs.forEach(function(address) {
            euSign.AddXMLHTTPDirectAccessAddress(address);
        });
    }

    this.m_initialized = true;
};

//-------------------------------------------------------------------------------

EndUser.prototype.IsPrivateKeyReaded = function() {
    return this.m_euSign.IsPrivateKeyReaded();
};

//-------------------------------------------------------------------------------

EndUser.prototype.ReadPrivateKeyBinary = function(
    privateKey, password, certs, CACommonName) {
    var euSign = this.m_euSign;
    if (certs) {
        certs.forEach(function(cert) {
            euSign.SaveCertificate(cert);
        });
    }

    if (CACommonName)
        this.SetSettings(CACommonName);

    try {
        var info = euSign.ReadPrivateKeyBinary(privateKey, password);
        if (!CACommonName)
            this.SetSettings(info.GetIssuerCN());
    } catch (e) {
        if (e.GetErrorCode() != EU_ERROR_CERT_NOT_FOUND)
            throw e;
        var result = this.SearchPrivateKeyCertificatesWithCMP(
            privateKey, password);
        if (result == null) {
            euSign.RaiseError(EU_ERROR_CERT_NOT_FOUND);
            return;
        }
        this.SetSettings(result.CACommonName);
        euSign.SaveCertificates(result.certs);
        euSign.ReadPrivateKeyBinary(privateKey, password);
    }

    return euSign.GetPrivateKeyOwnerInfo().GetTransferableObject();
};

//-------------------------------------------------------------------------------

EndUser.prototype.SignData = function(data, asBase64String) {
    return this.m_euSign.SignData(data, asBase64String);
};

//-------------------------------------------------------------------------------

EndUser.prototype.SignDataInternal = function(appendCert, data, asBase64String) {
    return this.m_euSign.SignDataInternal(appendCert, data, asBase64String);
};

//===============================================================================
