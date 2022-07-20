//===============================================================================

/**
 * Possible error codes.
 */
export const EU_ERROR_NONE = 0x0000;
export const EU_ERROR_UNKNOWN = 0xFFFF;
export const EU_ERROR_NOT_SUPPORTED = 0xFFFE;

export const EU_ERROR_NOT_INITIALIZED = 0x0001;
export const EU_ERROR_BAD_PARAMETER = 0x0002;
export const EU_ERROR_LIBRARY_LOAD = 0x0003;
export const EU_ERROR_READ_SETTINGS = 0x0004;
export const EU_ERROR_TRANSMIT_REQUEST = 0x0005;
export const EU_ERROR_MEMORY_ALLOCATION = 0x0006;
export const EU_WARNING_END_OF_ENUM = 0x0007;
export const EU_ERROR_PROXY_NOT_AUTHORIZED = 0x0008;
export const EU_ERROR_NO_GUI_DIALOGS = 0x0009;
export const EU_ERROR_DOWNLOAD_FILE = 0x000A;
export const EU_ERROR_WRITE_SETTINGS = 0x000B;
export const EU_ERROR_CANCELED_BY_GUI = 0x000C;
export const EU_ERROR_OFFLINE_MODE = 0x000D;

export const EU_ERROR_KEY_MEDIAS_FAILED = 0x0011;
export const EU_ERROR_KEY_MEDIAS_ACCESS_FAILED = 0x0012;
export const EU_ERROR_KEY_MEDIAS_READ_FAILED = 0x0013;
export const EU_ERROR_KEY_MEDIAS_WRITE_FAILED = 0x0014;
export const EU_WARNING_KEY_MEDIAS_READ_ONLY = 0x0015;
export const EU_ERROR_KEY_MEDIAS_DELETE = 0x0016;
export const EU_ERROR_KEY_MEDIAS_CLEAR = 0x0017;
export const EU_ERROR_BAD_PRIVATE_KEY = 0x0018;

export const EU_ERROR_PKI_FORMATS_FAILED = 0x0021;
export const EU_ERROR_CSP_FAILED = 0x0022;
export const EU_ERROR_BAD_SIGNATURE = 0x0023;
export const EU_ERROR_AUTH_FAILED = 0x0024;
export const EU_ERROR_NOT_RECEIVER = 0x0025;

export const EU_ERROR_STORAGE_FAILED = 0x0031;
export const EU_ERROR_BAD_CERT = 0x0032;
export const EU_ERROR_CERT_NOT_FOUND = 0x0033;
export const EU_ERROR_INVALID_CERT_TIME = 0x0034;
export const EU_ERROR_CERT_IN_CRL = 0x0035;
export const EU_ERROR_BAD_CRL = 0x0036;
export const EU_ERROR_NO_VALID_CRLS = 0x0037;

export const EU_ERROR_GET_TIME_STAMP = 0x0041;
export const EU_ERROR_BAD_TSP_RESPONSE = 0x0042;
export const EU_ERROR_TSP_SERVER_CERT_NOT_FOUND = 0x0043;
export const EU_ERROR_TSP_SERVER_CERT_INVALID = 0x0044;

export const EU_ERROR_GET_OCSP_STATUS = 0x0051;
export const EU_ERROR_BAD_OCSP_RESPONSE = 0x0052;
export const EU_ERROR_CERT_BAD_BY_OCSP = 0x0053;
export const EU_ERROR_OCSP_SERVER_CERT_NOT_FOUND = 0x0054;
export const EU_ERROR_OCSP_SERVER_CERT_INVALID = 0x0055;

export const EU_ERROR_LDAP_ERROR = 0x0061;

//===============================================================================

/**
 * @property <number> code - An error code.
 * @property <message> message - A localized error message.
 */
export class EndUserError {
    code: number;
    message: string;
};

//===============================================================================
