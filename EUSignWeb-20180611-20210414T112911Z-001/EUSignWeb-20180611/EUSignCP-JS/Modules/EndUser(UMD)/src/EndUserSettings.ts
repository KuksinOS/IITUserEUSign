//===============================================================================

/**
 * CA configuration.
 * @property <Array<string>> issuerCNs - An array with CA common names.
 * @property <string> address - CA server address.
 * @property <string | null> ocspAccessPointAddress - CA OCSP server access point address.
 * @property <string | null> ocspAccessPointPort - CA OCSP server access point port.
 * @property <string | null> cmpAddress - CA CMP server address.
 * @property <string | null> tspAddress - CA TSP server address.
 * @property <string | null> tspAddressPort - CA TSP server port.
 * @property <boolean> directAccess - A boolean value that specifies whether CA server supports direct access without Proxy-handler.
 * @property <boolean> certsInKey - A boolean value that specifies whether the private key container can have certificates.
 */
export class CASettings {
    issuerCNs: Array<string>;
    address: string;
    ocspAccessPointAddress:	string;   
    ocspAccessPointPort: string;    
    cmpAddress: string;
    tspAddress: string;
    tspAddressPort: string;  
    directAccess: boolean;        
    certsInKey: boolean
}

/**
 * Settings to configure EndUser library.
 * @property <string> language - The language of error description. Possible values "uk", "ru", "en".
 * @property <string> encoding - The encoding of string data. Possible values "UTF-8", "UTF-16LE".
 * @property <string> httpProxyServiceURL - The URL of the http proxy service that redirects requests to the CA.
 * @property <boolean> directAccess - A boolean value that specifies whether CA server supports direct access without Proxy-handler.
 * @property <Array<CASettings> | string> CAs - An array or URL with CAs configurations.
 * @property <Uint8Array | string> CACertificates - A binary array or URL with CAs certificates.
 * 
 */
export class EndUserSettings {
    language: string;
    encoding: string;
    httpProxyServiceURL: string;
    directAccess: boolean;
    CAs: Array<CASettings> | string;
    CACertificates: Uint8Array | string;
};
    
//===============================================================================
