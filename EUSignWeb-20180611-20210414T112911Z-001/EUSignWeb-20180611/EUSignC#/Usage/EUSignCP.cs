﻿//=============================================================================

/*__BEGIN_MONO_CODE__
#if (__IOS__ || __ANDROID__ || WINDOWS_UWP || __LINUX__)
#define XAMARIN_SDK
#endif
__END_MONO_CODE__*/

//=============================================================================

using System;
using System.Runtime.InteropServices;
/*__BEGIN_MONO_CODE__
using System.Linq;
using System.Text;
__END_MONO_CODE__*/

//=============================================================================

namespace EUSignCP
{
	using BOOL = System.Int32;
	using INT = System.Int32;
/*__BEGIN_MONO_CODE__
#if !XAMARIN_SDK
__END_MONO_CODE__*/
	using DWORD = System.Int32;
/*__BEGIN_MONO_CODE__
#else // !XAMARIN_SDK
	using DWORD = System.IntPtr;
#endif // !XAMARIN_SDK
__END_MONO_CODE__*/

	public class IEUSignCP
	{
		private static bool _throwExceptions = false;
		private static EU_LANG _lang = EU_LANG.DEFAULT;

		static IEUSignCP() 
		{
/*__BEGIN_MONO_CODE__
#if !XAMARIN_SDK
			// Add NuGet package System.text.Encoding.CodePage to dependencies
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			try {
				Encoding enc = Encoding.GetEncoding("windows-1251");
			} catch (Exception) {
				throw new Exception("Add NuGet package " +
					"System.text.Encoding.CodePage to dependencies");
			}
#endif // !XAMARIN_SDK
__END_MONO_CODE__*/
		}
		
		#region EUSignCP: Error codes

		public const int EU_ERROR_NONE = 0x0000;
		public const int EU_ERROR_UNKNOWN = 0xFFFF;
		public const int EU_ERROR_NOT_SUPPORTED = 0xFFFE;

		public const int EU_ERROR_NOT_INITIALIZED = 0x0001;
		public const int EU_ERROR_BAD_PARAMETER = 0x0002;
		public const int EU_ERROR_LIBRARY_LOAD = 0x0003;
		public const int EU_ERROR_READ_SETTINGS = 0x0004;
		public const int EU_ERROR_TRANSMIT_REQUEST = 0x0005;
		public const int EU_ERROR_MEMORY_ALLOCATION = 0x0006;
		public const int EU_WARNING_END_OF_ENUM = 0x0007;
		public const int EU_ERROR_PROXY_NOT_AUTHORIZED = 0x0008;
		public const int EU_ERROR_NO_GUI_DIALOGS = 0x0009;
		public const int EU_ERROR_DOWNLOAD_FILE = 0x000A;
		public const int EU_ERROR_WRITE_SETTINGS = 0x000B;
		public const int EU_ERROR_CANCELED_BY_GUI = 0x000C;
		public const int EU_ERROR_OFFLINE_MODE = 0x000D;

		public const int EU_ERROR_KEY_MEDIAS_FAILED = 0x0011;
		public const int EU_ERROR_KEY_MEDIAS_ACCESS_FAILED = 0x0012;
		public const int EU_ERROR_KEY_MEDIAS_READ_FAILED = 0x0013;
		public const int EU_ERROR_KEY_MEDIAS_WRITE_FAILED = 0x0014;
		public const int EU_WARNING_KEY_MEDIAS_READ_ONLY = 0x0015;
		public const int EU_ERROR_KEY_MEDIAS_DELETE = 0x0016;
		public const int EU_ERROR_KEY_MEDIAS_CLEAR = 0x0017;
		public const int EU_ERROR_BAD_PRIVATE_KEY = 0x0018;

		public const int EU_ERROR_PKI_FORMATS_FAILED = 0x0021;
		public const int EU_ERROR_CSP_FAILED = 0x0022;
		public const int EU_ERROR_BAD_SIGNATURE = 0x0023;
		public const int EU_ERROR_AUTH_FAILED = 0x0024;
		public const int EU_ERROR_NOT_RECEIVER = 0x0025;

		public const int EU_ERROR_STORAGE_FAILED = 0x0031;
		public const int EU_ERROR_BAD_CERT = 0x0032;
		public const int EU_ERROR_CERT_NOT_FOUND = 0x0033;
		public const int EU_ERROR_INVALID_CERT_TIME = 0x0034;
		public const int EU_ERROR_CERT_IN_CRL = 0x0035;
		public const int EU_ERROR_BAD_CRL = 0x0036;
		public const int EU_ERROR_NO_VALID_CRLS = 0x0037;

		public const int EU_ERROR_GET_TIME_STAMP = 0x0041;
		public const int EU_ERROR_BAD_TSP_RESPONSE = 0x0042;
		public const int EU_ERROR_TSP_SERVER_CERT_NOT_FOUND = 0x0043;
		public const int EU_ERROR_TSP_SERVER_CERT_INVALID = 0x0044;

		public const int EU_ERROR_GET_OCSP_STATUS = 0x0051;
		public const int EU_ERROR_BAD_OCSP_RESPONSE = 0x0052;
		public const int EU_ERROR_CERT_BAD_BY_OCSP = 0x0053;
		public const int EU_ERROR_OCSP_SERVER_CERT_NOT_FOUND = 0x0054;
		public const int EU_ERROR_OCSP_SERVER_CERT_INVALID = 0x0055;

		public const int EU_ERROR_LDAP_ERROR = 0x0061;

		#endregion

		#region EUSignCP: Constants

		public const int EU_PASS_MAX_LENGTH = 64;

		public const int EU_KEY_MEDIA_NAME_MAX_LENGTH = 256;

		public const int EU_PATH_MAX_LENGTH = 1040;
		public const int EU_PORT_MAX_LENGTH = 5;
		public const int EU_USER_NAME_MAX_LENGTH = 64;
		public const int EU_SERIAL_MAX_LENGTH = 64;
		public const int EU_ISSUER_MAX_LENGTH = 1024;

		public const int EU_CERT_INFO_EX_VERSION_2 = 2;
		public const int EU_CERT_INFO_EX_VERSION_3 = 3;
		public const int EU_CERT_INFO_EX_VERSION_4 = 4;
		public const int EU_CERT_INFO_EX_VERSION = 5;
		public const int EU_USER_INFO_VERSION = 3;

		public const int EU_TIME_INFO_VERSION_1 = 1;
		public const int EU_TIME_INFO_VERSION = 2;

		public const int EU_CR_INFO_VERSION_1 = 1;
		public const int EU_CR_INFO_VERSION_2 = 2;
		public const int EU_CR_INFO_VERSION = 3;

		public const int EU_KEY_MEDIA_DEVICE_INFO_VERSION = 1;

		public const int EU_CERT_KEY_TYPE_UNKNOWN = 0;
		public const int EU_CERT_KEY_TYPE_DSTU4145 = 1;
		public const int EU_CERT_KEY_TYPE_RSA = 2;
		public const int EU_CERT_KEY_TYPE_ECDSA = 4;

		public const int EU_KEY_USAGE_UNKNOWN = 0;
		public const int EU_KEY_USAGE_DIGITAL_SIGNATURE = 1;
		public const int EU_KEY_USAGE_KEY_AGREEMENT = 16;

		public const int EU_SETTINGS_ID_NONE = 0x00;
		public const int EU_SETTINGS_ID_ALL = 0x7FF;

		public const int EU_SETTINGS_ID_FSTORE = 0x01;
		public const int EU_SETTINGS_ID_PROXY = 0x02;
		public const int EU_SETTINGS_ID_TSP = 0x04;
		public const int EU_SETTINGS_ID_OCSP = 0x08;
		public const int EU_SETTINGS_ID_LDAP = 0x10;

		public const int EU_SETTINGS_ID_MANDATORY = 0x1F;

		public const int EU_SETTINGS_ID_MODE = 0x20;
		public const int EU_SETTINGS_ID_CMP = 0x40;
		public const int EU_SETTINGS_ID_KM = 0x80;

		public const int EU_SETTINGS_ID_OCSP_ACCESS_INFO_MODE = 0x100;
		public const int EU_SETTINGS_ID_OCSP_ACCESS_INFO = 0x200;

		public const string EU_RESOLVE_OIDS_PARAMETER = "ResolveOIDs";
		public const string EU_SAVE_SETTINGS_PARAMETER = "SaveSettings";
		public const string EU_UI_MODE_PARAMETER = "UIMode";
		public const string EU_SHOW_ERRORS_PARAMETER = "ShowErrors";
		public const string EU_MAKE_PKEY_PFX_CONTAINER_PARAMETER = 
			"MakePKeyPFXContainer";
		public const string EU_NO_CSP_SELF_TESTS_PARAMETER =
			"NoCSPSelfTests";
		public const string EU_SIGN_INCLUDE_CONTENT_TIME_STAMP_PARAMETER =
			"SignIncludeContentTimeStamp";
		public const string EU_SIGN_TYPE_PARAMETER = "SignType";
		public const string EU_SIGN_INCLUDE_CA_CERTIFICATES_PARAMETER =
			"SignIncludeCACertificates";
		public const string EU_FS_CALCULATE_FINGERPRINT = "FSCalculateFingerprint";
		public const string EU_FP_RESET = "FPReset";

		public const int EU_OCSP_SERVER_STATE_UNKNOWN = 0;
		public const int EU_OCSP_SERVER_STATE_AVAILABLE = 1;
		public const int EU_OCSP_SERVER_STATE_UNAVAILABLE = 2;

		public const int EU_SIGN_TYPE_UNKNOWN = 0;
		public const int EU_SIGN_TYPE_CADES_BES = 1;
		public const int EU_SIGN_TYPE_CADES_T = 4;
		public const int EU_SIGN_TYPE_CADES_C = 8;
		public const int EU_SIGN_TYPE_CADES_X_LONG = 16;

		public const int EU_STORAGE_VALUE_MAX_LENGTH = 0x7FFF;

		public const string EU_CHECK_PRIVATE_KEY_CONTEXT_PARAMETER = "CheckPrivateKey";
		public const string EU_RESOLVE_OIDS_CONTEXT_PARAMETER = "ResolveOIDs";
		public const string EU_EXPORATABLE_CONTEXT_CONTEXT_PARAMETER = "ExportableContext";

		public const int EU_RECIPIENT_APPEND_TYPE_BY_ISSUER_SERIAL = 1;
		public const int EU_RECIPIENT_APPEND_TYPE_BY_KEY_ID = 2;

		public const int EU_RECIPIENT_INFO_TYPE_UNKNOWN = 0;
		public const int EU_RECIPIENT_INFO_TYPE_ISSUER_SERIAL = 1;
		public const int EU_RECIPIENT_INFO_TYPE_KEY_ID = 2;

		public const int EU_CTX_HASH_ALGO_UNKNOWN = 0;
		public const int EU_CTX_HASH_ALGO_GOST34311 = 1;
		public const int EU_CTX_HASH_ALGO_SHA160 = 2;
		public const int EU_CTX_HASH_ALGO_SHA224 = 3;
		public const int EU_CTX_HASH_ALGO_SHA256 = 4;

		public const int EU_CTX_SIGN_ALGO_UNKNOWN = 0;
		public const int EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311 = 1;
		public const int EU_CTX_SIGN_ALGO_RSA_WITH_SHA = 2;
		public const int EU_CTX_SIGN_ALGO_ECDSA_WITH_SHA = 3;

		public const int EU_REG_KEY_ROOT_PATH_DEFAULT = 0;
		public const int EU_REG_KEY_ROOT_PATH_HKLM = 1;
		public const int EU_REG_KEY_ROOT_PATH_HKCU = 2;

		public const int EU_CCS_TYPE_REVOKE = 1;
		public const int EU_CCS_TYPE_HOLD = 2;

		public const int EU_REVOCATION_REASON_UNKNOWN = 0;
		public const int EU_REVOCATION_REASON_KEY_COMPROMISE = 1;
		public const int EU_REVOCATION_REASON_NEW_ISSUED = 2;

		public const int EU_COMMON_NAME_MAX_LENGTH = 64;
		public const int EU_LOCALITY_MAX_LENGTH = 128;
		public const int EU_STATE_MAX_LENGTH = 128;
		public const int EU_ORGANIZATION_MAX_LENGTH = 64;
		public const int EU_ORG_UNIT_MAX_LENGTH = 64;
		public const int EU_TITLE_MAX_LENGTH = 64;
		public const int EU_STREET_MAX_LENGTH = 128;
		public const int EU_PHONE_MAX_LENGTH = 32;
		public const int EU_SURNAME_MAX_LENGTH = 40;
		public const int EU_GIVENNAME_MAX_LENGTH = 32;
		public const int EU_EMAIL_MAX_LENGTH = 128;
		public const int EU_ADDRESS_MAX_LENGTH = 256;
		public const int EU_EDRPOU_MAX_LENGTH = 10;
		public const int EU_DRFO_MAX_LENGTH = 10;
		public const int EU_NBU_MAX_LENGTH = 6;
		public const int EU_SPFM_MAX_LENGTH = 6;
		public const int EU_O_CODE_MAX_LENGTH = 32;
		public const int EU_OU_CODE_MAX_LENGTH = 32;
		public const int EU_USER_CODE_MAX_LENGTH = 32;
		public const int EU_UPN_MAX_LENGTH = 256;
		public const int EU_UNZR_MAX_LENGTH = 14;
		public const int EU_COUNTRY_MAX_LENGTH = 2;

		public const string EU_HEADER_CA_TYPE = "UA1";
		public const int EU_HEADER_MAX_CA_TYPE_SIZE = 3;

		public const int EU_NAMED_PRIVATE_KEY_LABEL_MAX_LENGTH = 63;

		public enum EU_SUBJECT_TYPE
		{
			UNDIFFERENCED = 0,
			CA = 1,
			CA_SERVER = 2,
			RA_ADMINISTRATOR = 3,
			END_USER = 4
		};

		public enum EU_SUBJECT_SUB_TYPE
		{
			CA_SERVER_UNDIFFERENCED = 0,
			CA_SERVER_CMP = 1,
			CA_SERVER_TSP = 2,
			CA_SERVER_OCSP = 3,
		};

		public enum EU_KEY_MEDIA_SOURCE_TYPE
		{
			OPERATOR = 1,
			FIXED = 2
		};

		public enum EU_KEYS_TYPE
		{
			NONE = 0,
			DSTU_AND_ECDH_WITH_GOSTS = 1,
			RSA_WITH_SHA = 2
		};

		public enum EU_DS_UA_KEY_LENGTH
		{
			EC_191 = 1,
			EC_257 = 2,
			EC_307 = 3,
			FILE = 4
		}

		public enum EU_KEP_UA_KEY_LENGTH
		{
			EC_257 = 1,
			EC_431 = 2,
			EC_571 = 3,
			FILE = 4
		}

		public enum EU_DS_RSA_KEY_LENGTH
		{
			RSA_1024 = 1,
			RSA_2048 = 2,
			RSA_3072 = 3,
			RSA_4096 = 4,
			FILE = 5
		}

		public enum EU_CONTENT_ENC_ALGO_TYPE
		{
			TDES_CBC = 4,
			AES_128_CBC = 5,
			AES_192_CBC = 6,
			AES_256_CBC = 7
		};

		public enum EU_LANG
		{
			DEFAULT = 0,
			UA = 1,
			RU = 2,
			EN = 3
		};

		private enum EU_STRING_ENCODING
		{
			CP1251 = 1251,
			UTF8 = 65001
		};

		public enum EU_HEADER_PART_TYPE
		{
			SIGNED = 1,
			ENCRYPTED = 2,
			STAMPED = 3,
			CERTCRYPT = 4
		};

		public enum EU_DEV_CTX_IDCARD_PASSWORD_VERSION
		{
			VERSION_1 = 1,
			VERSION_2 = 2
		};

		public enum EU_DEV_CTX_IDCARD_DATA_ID
		{
			DG1 = 0x01,
			DG2 = 0x02,
			DG3 = 0x03,
			DG4 = 0x04,
			DG5 = 0x05,
			DG6 = 0x06,
			DG7 = 0x07,
			DG8 = 0x08,
			DG9 = 0x09,
			DG10 = 0x0A,
			DG11 = 0x0B,
			DG12 = 0x0C,
			DG13 = 0x0D,
			DG14 = 0x0E,
			DG15 = 0x0F,
			DG16 = 0x10,

			SOD = 0x1D,
			COM = 0x1E,

			DG32 = 0x20,
			DG33 = 0x21,
			DG34 = 0x22,
			DG35 = 0x23,
			DG36 = 0x24,
			DG37 = 0x25,
			DG38 = 0x26
		};

		#endregion

		#region EUSignCP: Structures

		public struct EU_KEY_MEDIA
		{
			public int typeIndex;
			public int deviceIndex;
			public string password;

			public EU_KEY_MEDIA(int typeIndex, int deviceIndex, string password)
			{
				this.typeIndex = typeIndex;
				this.deviceIndex = deviceIndex;
				this.password = password;
			}

			public EU_KEY_MEDIA(IntPtr intKeyMedia)
			{
				try
				{
					IntPtr curPtr = intKeyMedia;

					curPtr = EUMarshal.ReadDWORD(curPtr, out this.typeIndex);
					curPtr = EUMarshal.ReadDWORD(curPtr, out this.deviceIndex);
					this.password = PtrToStringAnsi(curPtr);
				}
				catch (Exception)
				{
					this.typeIndex = -1;
					this.deviceIndex = -1;
					this.password = "";
				}
			}
		};

		public struct EU_CERT_OWNER_INFO
		{
			public bool filled;
			public string issuer;
			public string issuerCN;
			public string serial;
			public string subject;
			public string subjCN;
			public string subjOrg;
			public string subjOrgUnit;
			public string subjTitle;
			public string subjState;
			public string subjLocality;
			public string subjFullName;
			public string subjAddress;
			public string subjPhone;
			public string subjEMail;
			public string subjDNS;
			public string subjEDRPOUCode;
			public string subjDRFOCode;

			public EU_CERT_OWNER_INFO(IntPtr intCertOwnerInfo)
			{
				try
				{
					IntPtr curPtr = intCertOwnerInfo;

					curPtr = EUMarshal.ReadBOOL(curPtr, out filled);
					if (!filled)
						throw new Exception("");

					curPtr = EUMarshal.ReadString(curPtr, out issuer);
					curPtr = EUMarshal.ReadString(curPtr, out issuerCN);
					curPtr = EUMarshal.ReadString(curPtr, out serial);
					curPtr = EUMarshal.ReadString(curPtr, out subject);
					curPtr = EUMarshal.ReadString(curPtr, out subjCN);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrg);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrgUnit);
					curPtr = EUMarshal.ReadString(curPtr, out subjTitle);
					curPtr = EUMarshal.ReadString(curPtr, out subjState);
					curPtr = EUMarshal.ReadString(curPtr, out subjLocality);
					curPtr = EUMarshal.ReadString(curPtr, out subjFullName);
					curPtr = EUMarshal.ReadString(curPtr, out subjAddress);
					curPtr = EUMarshal.ReadString(curPtr, out subjPhone);
					curPtr = EUMarshal.ReadString(curPtr, out subjEMail);
					curPtr = EUMarshal.ReadString(curPtr, out subjDNS);
					curPtr = EUMarshal.ReadString(curPtr, out subjEDRPOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjDRFOCode);
				}
				catch (Exception)
				{
					this.filled = false;
					this.issuer = "";
					this.issuerCN = "";
					this.serial = "";
					this.subject = "";
					this.subjCN = "";
					this.subjOrg = "";
					this.subjOrgUnit = "";
					this.subjTitle = "";
					this.subjState = "";
					this.subjLocality = "";
					this.subjFullName = "";
					this.subjAddress = "";
					this.subjPhone = "";
					this.subjEMail = "";
					this.subjDNS = "";
					this.subjEDRPOUCode = "";
					this.subjDRFOCode = "";
				}
			}
		};

		public struct SYSTEMTIME
		{
			public short wYear;
			public short wMonth;
			public short wDayOfWeek;
			public short wDay;
			public short wHour;
			public short wMinute;
			public short wSecond;
			public short wMilliseconds;
		};

		public struct EU_SIGN_INFO
		{
			public bool filled;
			public string issuer;
			public string issuerCN;
			public string serial;
			public string subject;
			public string subjCN;
			public string subjOrg;
			public string subjOrgUnit;
			public string subjTitle;
			public string subjState;
			public string subjLocality;
			public string subjFullName;
			public string subjAddress;
			public string subjPhone;
			public string subjEMail;
			public string subjDNS;
			public string subjEDRPOUCode;
			public string subjDRFOCode;
			public bool timeAvail;
			public bool timeStamp;
			public SYSTEMTIME time;

			public IntPtr intSignInfo;
			public EUMarshal signInfoPtr;

			public EU_SIGN_INFO(EUMarshal signInfoPtr)
			{
				try
				{
					IntPtr intSignInfo = signInfoPtr.DataPtr;
					IntPtr curPtr = intSignInfo;

					curPtr = EUMarshal.ReadBOOL(curPtr, out filled);
					if (!filled)
						throw new Exception("");

					curPtr = EUMarshal.ReadString(curPtr, out issuer);
					curPtr = EUMarshal.ReadString(curPtr, out issuerCN);
					curPtr = EUMarshal.ReadString(curPtr, out serial);
					curPtr = EUMarshal.ReadString(curPtr, out subject);
					curPtr = EUMarshal.ReadString(curPtr, out subjCN);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrg);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrgUnit);
					curPtr = EUMarshal.ReadString(curPtr, out subjTitle);
					curPtr = EUMarshal.ReadString(curPtr, out subjState);
					curPtr = EUMarshal.ReadString(curPtr, out subjLocality);
					curPtr = EUMarshal.ReadString(curPtr, out subjFullName);
					curPtr = EUMarshal.ReadString(curPtr, out subjAddress);
					curPtr = EUMarshal.ReadString(curPtr, out subjPhone);
					curPtr = EUMarshal.ReadString(curPtr, out subjEMail);
					curPtr = EUMarshal.ReadString(curPtr, out subjDNS);
					curPtr = EUMarshal.ReadString(curPtr, out subjEDRPOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjDRFOCode);

					curPtr = EUMarshal.ReadBOOL(curPtr, out timeAvail);
					curPtr = EUMarshal.ReadBOOL(curPtr, out timeStamp);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out time);

					this.intSignInfo = intSignInfo;
					this.signInfoPtr = signInfoPtr;
				}
				catch (Exception)
				{
					this.filled = false;
					this.issuer = "";
					this.issuerCN = "";
					this.serial = "";
					this.subject = "";
					this.subjCN = "";
					this.subjOrg = "";
					this.subjOrgUnit = "";
					this.subjTitle = "";
					this.subjState = "";
					this.subjLocality = "";
					this.subjFullName = "";
					this.subjAddress = "";
					this.subjPhone = "";
					this.subjEMail = "";
					this.subjDNS = "";
					this.subjEDRPOUCode = "";
					this.subjDRFOCode = "";
					this.timeAvail = false;
					this.timeStamp = false;
					this.time = new SYSTEMTIME();

					this.intSignInfo = IntPtr.Zero;
					this.signInfoPtr = null;
				}
			}
		};

		public struct EU_SENDER_INFO
		{
			public bool filled;
			public string issuer;
			public string issuerCN;
			public string serial;
			public string subject;
			public string subjCN;
			public string subjOrg;
			public string subjOrgUnit;
			public string subjTitle;
			public string subjState;
			public string subjLocality;
			public string subjFullName;
			public string subjAddress;
			public string subjPhone;
			public string subjEMail;
			public string subjDNS;
			public string subjEDRPOUCode;
			public string subjDRFOCode;
			public bool timeAvail;
			public bool timeStamp;
			public SYSTEMTIME time;

			public IntPtr intSenderInfo;
			public EUMarshal senderInfoPtr;

			public EU_SENDER_INFO(EUMarshal senderInfoPtr)
			{
				try
				{
					IntPtr intSenderInfo = senderInfoPtr.DataPtr;

					IntPtr curPtr = intSenderInfo;

					curPtr = EUMarshal.ReadBOOL(curPtr, out filled);
					if (!filled)
						throw new Exception("");

					curPtr = EUMarshal.ReadString(curPtr, out issuer);
					curPtr = EUMarshal.ReadString(curPtr, out issuerCN);
					curPtr = EUMarshal.ReadString(curPtr, out serial);
					curPtr = EUMarshal.ReadString(curPtr, out subject);
					curPtr = EUMarshal.ReadString(curPtr, out subjCN);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrg);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrgUnit);
					curPtr = EUMarshal.ReadString(curPtr, out subjTitle);
					curPtr = EUMarshal.ReadString(curPtr, out subjState);
					curPtr = EUMarshal.ReadString(curPtr, out subjLocality);
					curPtr = EUMarshal.ReadString(curPtr, out subjFullName);
					curPtr = EUMarshal.ReadString(curPtr, out subjAddress);
					curPtr = EUMarshal.ReadString(curPtr, out subjPhone);
					curPtr = EUMarshal.ReadString(curPtr, out subjEMail);
					curPtr = EUMarshal.ReadString(curPtr, out subjDNS);
					curPtr = EUMarshal.ReadString(curPtr, out subjEDRPOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjDRFOCode);

					curPtr = EUMarshal.ReadBOOL(curPtr, out timeAvail);
					curPtr = EUMarshal.ReadBOOL(curPtr, out timeStamp);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out time);

					this.intSenderInfo = intSenderInfo;
					this.senderInfoPtr = senderInfoPtr;
				}
				catch (Exception)
				{
					this.filled = false;
					this.issuer = "";
					this.issuerCN = "";
					this.serial = "";
					this.subject = "";
					this.subjCN = "";
					this.subjOrg = "";
					this.subjOrgUnit = "";
					this.subjTitle = "";
					this.subjState = "";
					this.subjLocality = "";
					this.subjFullName = "";
					this.subjAddress = "";
					this.subjPhone = "";
					this.subjEMail = "";
					this.subjDNS = "";
					this.subjEDRPOUCode = "";
					this.subjDRFOCode = "";
					this.timeAvail = false;
					this.timeStamp = false;
					this.time = new SYSTEMTIME();

					this.intSenderInfo = IntPtr.Zero;
					this.senderInfoPtr = null;
				}
			}
		};

		public struct EU_CRL_INFO
		{
			public bool filled;
			public string issuer;
			public string issuerCN;
			public int crlNumber;
			public SYSTEMTIME thisUpdate;
			public SYSTEMTIME nextUpdate;

			public EU_CRL_INFO(IntPtr intCRLInfo)
			{
				try
				{
					IntPtr curPtr = intCRLInfo;

					curPtr = EUMarshal.ReadBOOL(curPtr, out filled);
					if (!filled)
						throw new Exception("");

					curPtr = EUMarshal.ReadString(curPtr, out issuer);
					curPtr = EUMarshal.ReadString(curPtr, out issuerCN);

					curPtr = EUMarshal.ReadDWORD(curPtr, out crlNumber);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out thisUpdate);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out nextUpdate);
				}
				catch (Exception)
				{
					this.filled = false;
					this.issuer = "";
					this.issuerCN = "";
					this.crlNumber = 0;
					this.thisUpdate = new SYSTEMTIME();
					this.nextUpdate = new SYSTEMTIME();
				}
			}
		};

		public struct EU_CERT_INFO
		{
			public bool filled;
			public int version;
			public string issuer;
			public string issuerCN;
			public string serial;
			public string subject;
			public string subjCN;
			public string subjOrg;
			public string subjOrgUnit;
			public string subjTitle;
			public string subjState;
			public string subjLocality;
			public string subjFullName;
			public string subjAddress;
			public string subjPhone;
			public string subjEMail;
			public string subjDNS;
			public string subjEDRPOUCode;
			public string subjDRFOCode;
			public string subjNBUCode;
			public string subjSPFMCode;
			public string subjOCode;
			public string subjOUCode;
			public string subjUserCode;
			public SYSTEMTIME certBeginTime;
			public SYSTEMTIME certEndTime;
			public bool privKeyTimesExists;
			public SYSTEMTIME privKeyBeginTime;
			public SYSTEMTIME privKeyEndTime;
			public int publicKeyBits;
			public string publicKey;
			public string publicKeyID;
			public bool ecdhPublicKeyExists;
			public int ecdhPublicKeyBits;
			public string ecdhPublicKey;
			public string ecdhPublicKeyID;
			public string issuerPublicKeyID;
			public string keyUsage;
			public string extKeyUsages;
			public string policies;
			public string crlDistribPoint1;
			public string crlDistribPoint2;
			public bool powerCert;
			public bool subjType;
			public bool subjCA;

			public EU_CERT_INFO(IntPtr intCertInfo)
			{
				try
				{
					IntPtr curPtr = intCertInfo;

					curPtr = EUMarshal.ReadBOOL(curPtr, out filled);
					if (!filled)
						throw new Exception("");

					curPtr = EUMarshal.ReadDWORD(curPtr, out version);

					curPtr = EUMarshal.ReadString(curPtr, out issuer);
					curPtr = EUMarshal.ReadString(curPtr, out issuerCN);
					curPtr = EUMarshal.ReadString(curPtr, out serial);

					curPtr = EUMarshal.ReadString(curPtr, out subject);
					curPtr = EUMarshal.ReadString(curPtr, out subjCN);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrg);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrgUnit);
					curPtr = EUMarshal.ReadString(curPtr, out subjTitle);
					curPtr = EUMarshal.ReadString(curPtr, out subjState);
					curPtr = EUMarshal.ReadString(curPtr, out subjLocality);
					curPtr = EUMarshal.ReadString(curPtr, out subjFullName);
					curPtr = EUMarshal.ReadString(curPtr, out subjAddress);
					curPtr = EUMarshal.ReadString(curPtr, out subjPhone);
					curPtr = EUMarshal.ReadString(curPtr, out subjEMail);
					curPtr = EUMarshal.ReadString(curPtr, out subjDNS);
					curPtr = EUMarshal.ReadString(curPtr, out subjEDRPOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjDRFOCode);

					curPtr = EUMarshal.ReadString(curPtr, out subjNBUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjSPFMCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjOCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjUserCode);

					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out certBeginTime);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out certEndTime);
					curPtr = EUMarshal.ReadBOOL(curPtr, out privKeyTimesExists);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out privKeyBeginTime);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out privKeyEndTime);

					curPtr = EUMarshal.ReadDWORD(curPtr, out publicKeyBits);
					curPtr = EUMarshal.ReadString(curPtr, out publicKey);
					curPtr = EUMarshal.ReadString(curPtr, out publicKeyID);

					curPtr = EUMarshal.ReadBOOL(curPtr, out ecdhPublicKeyExists);
					curPtr = EUMarshal.ReadDWORD(curPtr, out ecdhPublicKeyBits);
					curPtr = EUMarshal.ReadString(curPtr, out ecdhPublicKey);
					curPtr = EUMarshal.ReadString(curPtr, out ecdhPublicKeyID);

					curPtr = EUMarshal.ReadString(curPtr, out issuerPublicKeyID);

					curPtr = EUMarshal.ReadString(curPtr, out keyUsage);
					curPtr = EUMarshal.ReadString(curPtr, out extKeyUsages);
					curPtr = EUMarshal.ReadString(curPtr, out policies);

					curPtr = EUMarshal.ReadString(curPtr, out crlDistribPoint1);
					curPtr = EUMarshal.ReadString(curPtr, out crlDistribPoint2);

					curPtr = EUMarshal.ReadBOOL(curPtr, out powerCert);

					curPtr = EUMarshal.ReadBOOL(curPtr, out subjType);
					curPtr = EUMarshal.ReadBOOL(curPtr, out subjCA);
				}
				catch (Exception)
				{
					this.filled = false;
					this.version = 0;
					this.issuer = "";
					this.issuerCN = "";
					this.serial = "";
					this.subject = "";
					this.subjCN = "";
					this.subjOrg = "";
					this.subjOrgUnit = "";
					this.subjTitle = "";
					this.subjState = "";
					this.subjLocality = "";
					this.subjFullName = "";
					this.subjAddress = "";
					this.subjPhone = "";
					this.subjEMail = "";
					this.subjDNS = "";
					this.subjEDRPOUCode = "";
					this.subjDRFOCode = "";
					this.subjNBUCode = "";
					this.subjSPFMCode = "";
					this.subjOCode = "";
					this.subjOUCode = "";
					this.subjUserCode = "";
					this.certBeginTime = new SYSTEMTIME();
					this.certEndTime = new SYSTEMTIME();
					this.privKeyTimesExists = false;
					this.privKeyBeginTime = new SYSTEMTIME();
					this.privKeyEndTime = new SYSTEMTIME();
					this.publicKeyBits = 0;
					this.publicKey = "";
					this.publicKeyID = "";
					this.ecdhPublicKeyExists = false;
					this.ecdhPublicKeyBits = 0;
					this.ecdhPublicKey = "";
					this.ecdhPublicKeyID = "";
					this.issuerPublicKeyID = "";
					this.keyUsage = "";
					this.extKeyUsages = "";
					this.policies = "";
					this.crlDistribPoint1 = "";
					this.crlDistribPoint2 = "";
					this.powerCert = false;
					this.subjType = false;
					this.subjCA = false;
				}
			}
		};

		public struct EU_CERT_INFO_EX
		{
			public bool filled;
			public int version;
			public string issuer;
			public string issuerCN;
			public string serial;
			public string subject;
			public string subjCN;
			public string subjOrg;
			public string subjOrgUnit;
			public string subjTitle;
			public string subjState;
			public string subjLocality;
			public string subjFullName;
			public string subjAddress;
			public string subjPhone;
			public string subjEMail;
			public string subjDNS;
			public string subjEDRPOUCode;
			public string subjDRFOCode;
			public string subjNBUCode;
			public string subjSPFMCode;
			public string subjOCode;
			public string subjOUCode;
			public string subjUserCode;
			public SYSTEMTIME certBeginTime;
			public SYSTEMTIME certEndTime;
			public bool privKeyTimesExists;
			public SYSTEMTIME privKeyBeginTime;
			public SYSTEMTIME privKeyEndTime;
			public int publicKeyBits;
			public string publicKey;
			public string publicKeyID;
			public string issuerPublicKeyID;
			public string keyUsage;
			public string extKeyUsages;
			public string policies;
			public string crlDistribPoint1;
			public string crlDistribPoint2;
			public bool powerCert;
			public bool subjType;
			public bool subjCA;
			public int chainLength;
			public string UPN;
			public int publicKeyType;
			public int keyUsageType;
			public string RSAModul;
			public string RSAExponent;
			public string OCSPAccessInfo;
			public string issuerAccessInfo;
			public string TSPAccessInfo;
			public bool limitValueAvailable;
			public int limitValue;
			public string limitValueCurrency;
			public EU_SUBJECT_TYPE subjectType;
			public EU_SUBJECT_SUB_TYPE subjectSubType;
			public string subjUNZR;
			public string subjCountry;

			public EU_CERT_INFO_EX(IntPtr intCertInfo)
			{
				try
				{
					IntPtr curPtr = intCertInfo;

					curPtr = EUMarshal.ReadBOOL(curPtr, out filled);
					if (!filled)
						throw new Exception("");

					curPtr = EUMarshal.ReadDWORD(curPtr, out version);

					curPtr = EUMarshal.ReadString(curPtr, out issuer);
					curPtr = EUMarshal.ReadString(curPtr, out issuerCN);
					curPtr = EUMarshal.ReadString(curPtr, out serial);

					curPtr = EUMarshal.ReadString(curPtr, out subject);
					curPtr = EUMarshal.ReadString(curPtr, out subjCN);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrg);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrgUnit);
					curPtr = EUMarshal.ReadString(curPtr, out subjTitle);
					curPtr = EUMarshal.ReadString(curPtr, out subjState);
					curPtr = EUMarshal.ReadString(curPtr, out subjLocality);
					curPtr = EUMarshal.ReadString(curPtr, out subjFullName);
					curPtr = EUMarshal.ReadString(curPtr, out subjAddress);
					curPtr = EUMarshal.ReadString(curPtr, out subjPhone);
					curPtr = EUMarshal.ReadString(curPtr, out subjEMail);
					curPtr = EUMarshal.ReadString(curPtr, out subjDNS);
					curPtr = EUMarshal.ReadString(curPtr, out subjEDRPOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjDRFOCode);

					curPtr = EUMarshal.ReadString(curPtr, out subjNBUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjSPFMCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjOCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjUserCode);

					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out certBeginTime);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out certEndTime);
					curPtr = EUMarshal.ReadBOOL(curPtr, out privKeyTimesExists);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out privKeyBeginTime);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out privKeyEndTime);

					curPtr = EUMarshal.ReadDWORD(curPtr, out publicKeyBits);
					curPtr = EUMarshal.ReadString(curPtr, out publicKey);
					curPtr = EUMarshal.ReadString(curPtr, out publicKeyID);

					curPtr = EUMarshal.ReadString(curPtr, out issuerPublicKeyID);

					curPtr = EUMarshal.ReadString(curPtr, out keyUsage);
					curPtr = EUMarshal.ReadString(curPtr, out extKeyUsages);
					curPtr = EUMarshal.ReadString(curPtr, out policies);

					curPtr = EUMarshal.ReadString(curPtr, out crlDistribPoint1);
					curPtr = EUMarshal.ReadString(curPtr, out crlDistribPoint2);

					curPtr = EUMarshal.ReadBOOL(curPtr, out powerCert);

					curPtr = EUMarshal.ReadBOOL(curPtr, out subjType);
					curPtr = EUMarshal.ReadBOOL(curPtr, out subjCA);

					curPtr = EUMarshal.ReadINT(curPtr, out chainLength);

					curPtr = EUMarshal.ReadString(curPtr, out UPN);

					curPtr = EUMarshal.ReadDWORD(curPtr, out publicKeyType);
					curPtr = EUMarshal.ReadDWORD(curPtr, out keyUsageType);

					curPtr = EUMarshal.ReadString(curPtr, out RSAModul);
					curPtr = EUMarshal.ReadString(curPtr, out RSAExponent);

					curPtr = EUMarshal.ReadString(curPtr, out OCSPAccessInfo);
					curPtr = EUMarshal.ReadString(curPtr, out issuerAccessInfo);
					curPtr = EUMarshal.ReadString(curPtr, out TSPAccessInfo);

					curPtr = EUMarshal.ReadBOOL(curPtr, out limitValueAvailable);
					curPtr = EUMarshal.ReadDWORD(curPtr, out limitValue);
					curPtr = EUMarshal.ReadString(curPtr, out limitValueCurrency);

					if (version > EU_CERT_INFO_EX_VERSION_2)
					{
						int nValue = 0;
						curPtr = EUMarshal.ReadDWORD(curPtr, out nValue);
						subjectType = (EU_SUBJECT_TYPE) nValue;
						curPtr = EUMarshal.ReadDWORD(curPtr, out nValue);
						subjectSubType = (EU_SUBJECT_SUB_TYPE) nValue;
					}
					else
					{
						this.subjectType = EU_SUBJECT_TYPE.UNDIFFERENCED;
						this.subjectSubType =
							EU_SUBJECT_SUB_TYPE.CA_SERVER_UNDIFFERENCED;
					}

					if (version > EU_CERT_INFO_EX_VERSION_3)
						curPtr = EUMarshal.ReadString(curPtr, out subjUNZR);
					else
						subjUNZR = "";

					if (version > EU_CERT_INFO_EX_VERSION_4)
						curPtr = EUMarshal.ReadString(curPtr, out subjCountry);
					else
						subjCountry = "";
				}
				catch (Exception)
				{
					this.filled = false;
					this.version = 0;
					this.issuer = "";
					this.issuerCN = "";
					this.serial = "";
					this.subject = "";
					this.subjCN = "";
					this.subjOrg = "";
					this.subjOrgUnit = "";
					this.subjTitle = "";
					this.subjState = "";
					this.subjLocality = "";
					this.subjFullName = "";
					this.subjAddress = "";
					this.subjPhone = "";
					this.subjEMail = "";
					this.subjDNS = "";
					this.subjEDRPOUCode = "";
					this.subjDRFOCode = "";
					this.subjNBUCode = "";
					this.subjSPFMCode = "";
					this.subjOCode = "";
					this.subjOUCode = "";
					this.subjUserCode = "";
					this.certBeginTime = new SYSTEMTIME();
					this.certEndTime = new SYSTEMTIME();
					this.privKeyTimesExists = false;
					this.privKeyBeginTime = new SYSTEMTIME();
					this.privKeyEndTime = new SYSTEMTIME();
					this.publicKeyBits = 0;
					this.publicKey = "";
					this.publicKeyID = "";
					this.issuerPublicKeyID = "";
					this.keyUsage = "";
					this.extKeyUsages = "";
					this.policies = "";
					this.crlDistribPoint1 = "";
					this.crlDistribPoint2 = "";
					this.powerCert = false;
					this.subjType = false;
					this.subjCA = false;
					this.chainLength = 0;
					this.UPN = "";
					this.publicKeyType = EU_CERT_KEY_TYPE_UNKNOWN;
					this.keyUsageType = EU_KEY_USAGE_UNKNOWN;
					this.RSAModul = "";
					this.RSAExponent = "";
					this.OCSPAccessInfo = "";
					this.issuerAccessInfo = "";
					this.TSPAccessInfo = "";
					this.limitValueAvailable = false;
					this.limitValue = 0;
					this.limitValueCurrency = "";
					this.subjectType = EU_SUBJECT_TYPE.UNDIFFERENCED;
					this.subjectSubType = 
						EU_SUBJECT_SUB_TYPE.CA_SERVER_UNDIFFERENCED;
					this.subjUNZR = "";
					this.subjCountry = "";
				}
			}
		};

		public struct EU_CRL_DETAILED_INFO
		{
			public bool filled;
			public int version;
			public string issuer;
			public string issuerCN;
			public string issuerPublicKeyID;
			public int crlNumber;
			public SYSTEMTIME thisUpdate;
			public SYSTEMTIME nextUpdate;
			public int revokedItemsCount;

			public EU_CRL_DETAILED_INFO(IntPtr intCRLDetailedInfo)
			{
				try
				{
					IntPtr curPtr = intCRLDetailedInfo;

					curPtr = EUMarshal.ReadBOOL(curPtr, out filled);
					if (!filled)
						throw new Exception("");

					curPtr = EUMarshal.ReadDWORD(curPtr, out version);

					curPtr = EUMarshal.ReadString(curPtr, out issuer);
					curPtr = EUMarshal.ReadString(curPtr, out issuerCN);
					curPtr = EUMarshal.ReadString(curPtr, out issuerPublicKeyID);

					curPtr = EUMarshal.ReadDWORD(curPtr, out crlNumber);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out thisUpdate);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out nextUpdate);

					curPtr = EUMarshal.ReadDWORD(curPtr, out revokedItemsCount);
				}
				catch (Exception)
				{
					this.filled = false;
					this.version = 0;
					this.issuer = "";
					this.issuerCN = "";
					this.issuerPublicKeyID = "";
					this.crlNumber = 0;
					this.thisUpdate = new SYSTEMTIME();
					this.nextUpdate = new SYSTEMTIME();
					this.revokedItemsCount = 0;
				}
			}
		};

		public struct EU_CR_INFO
		{
			public bool filled;
			public int version;

			public bool simple;

			public string subject;
			public string subjCN;
			public string subjOrg;
			public string subjOrgUnit;
			public string subjTitle;
			public string subjState;
			public string subjLocality;
			public string subjFullName;
			public string subjAddress;
			public string subjPhone;
			public string subjEMail;
			public string subjDNS;
			public string subjEDRPOUCode;
			public string subjDRFOCode;
			public string subjNBUCode;
			public string subjSPFMCode;
			public string subjOCode;
			public string subjOUCode;
			public string subjUserCode;

			public bool certTimesExists;
			public SYSTEMTIME certBeginTime;
			public SYSTEMTIME certEndTime;
			public bool privKeyTimesExists;
			public SYSTEMTIME privKeyBeginTime;
			public SYSTEMTIME privKeyEndTime;

			public int publicKeyType;

			public int publicKeyBits;
			public string publicKey;
			public string RSAModul;
			public string RSAExponent;

			public string publicKeyID;

			public string extKeyUsages;

			public string crlDistribPoint1;
			public string crlDistribPoint2;

			public bool subjTypeExists;
			public int subjType;
			public int subjSubType;

			public bool selfSigned;
			public string signIssuer;
			public string signSerial;

			public string subjUNZR;

			public string subjCountry;

			public byte[] certRequestInfo;

			public EU_CR_INFO(IntPtr intCRInfo, byte[] certRequestInfo)
			{
				try
				{
					IntPtr curPtr = intCRInfo;

					curPtr = EUMarshal.ReadBOOL(curPtr, out filled);
					if (!filled)
						throw new Exception("");

					curPtr = EUMarshal.ReadDWORD(curPtr, out version);

					curPtr = EUMarshal.ReadBOOL(curPtr, out simple);

					curPtr = EUMarshal.ReadString(curPtr, out subject);
					curPtr = EUMarshal.ReadString(curPtr, out subjCN);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrg);
					curPtr = EUMarshal.ReadString(curPtr, out subjOrgUnit);
					curPtr = EUMarshal.ReadString(curPtr, out subjTitle);
					curPtr = EUMarshal.ReadString(curPtr, out subjState);
					curPtr = EUMarshal.ReadString(curPtr, out subjLocality);
					curPtr = EUMarshal.ReadString(curPtr, out subjFullName);
					curPtr = EUMarshal.ReadString(curPtr, out subjAddress);
					curPtr = EUMarshal.ReadString(curPtr, out subjPhone);
					curPtr = EUMarshal.ReadString(curPtr, out subjEMail);
					curPtr = EUMarshal.ReadString(curPtr, out subjDNS);
					curPtr = EUMarshal.ReadString(curPtr, out subjEDRPOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjDRFOCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjNBUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjSPFMCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjOCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjOUCode);
					curPtr = EUMarshal.ReadString(curPtr, out subjUserCode);

					curPtr = EUMarshal.ReadBOOL(curPtr, out certTimesExists);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out certBeginTime);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out certEndTime);
					curPtr = EUMarshal.ReadBOOL(curPtr, out privKeyTimesExists);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out privKeyBeginTime);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out privKeyEndTime);

					curPtr = EUMarshal.ReadDWORD(curPtr, out publicKeyType);

					curPtr = EUMarshal.ReadDWORD(curPtr, out publicKeyBits);
					curPtr = EUMarshal.ReadString(curPtr, out publicKey);
					curPtr = EUMarshal.ReadString(curPtr, out RSAModul);
					curPtr = EUMarshal.ReadString(curPtr, out RSAExponent);

					curPtr = EUMarshal.ReadString(curPtr, out publicKeyID);

					curPtr = EUMarshal.ReadString(curPtr, out extKeyUsages);

					curPtr = EUMarshal.ReadString(curPtr, out crlDistribPoint1);
					curPtr = EUMarshal.ReadString(curPtr, out crlDistribPoint2);

					curPtr = EUMarshal.ReadBOOL(curPtr, out subjTypeExists);
					curPtr = EUMarshal.ReadINT(curPtr, out subjType);
					curPtr = EUMarshal.ReadINT(curPtr, out subjSubType);

					curPtr = EUMarshal.ReadBOOL(curPtr, out selfSigned);
					curPtr = EUMarshal.ReadString(curPtr, out signIssuer);
					curPtr = EUMarshal.ReadString(curPtr, out signSerial);

					if (version > EU_CR_INFO_VERSION_1)
						curPtr = EUMarshal.ReadString(curPtr, out subjUNZR);
					else
						subjUNZR = "";

					if (version > EU_CR_INFO_VERSION_2)
						curPtr = EUMarshal.ReadString(curPtr, out subjCountry);
					else
						subjCountry = "";

					this.certRequestInfo = certRequestInfo;
				}
				catch (Exception)
				{
					this.filled = false;
					this.version = 0;
					this.simple = false;
					this.subject = "";
					this.subjCN = "";
					this.subjOrg = "";
					this.subjOrgUnit = "";
					this.subjTitle = "";
					this.subjState = "";
					this.subjLocality = "";
					this.subjFullName = "";
					this.subjAddress = "";
					this.subjPhone = "";
					this.subjEMail = "";
					this.subjDNS = "";
					this.subjEDRPOUCode = "";
					this.subjDRFOCode = "";
					this.subjNBUCode = "";
					this.subjSPFMCode = "";
					this.subjOCode = "";
					this.subjOUCode = "";
					this.subjUserCode = "";
					this.certTimesExists = false;
					this.certBeginTime = new SYSTEMTIME();
					this.certEndTime = new SYSTEMTIME();
					this.privKeyTimesExists = false;
					this.privKeyBeginTime = new SYSTEMTIME();
					this.privKeyEndTime = new SYSTEMTIME();
					this.publicKeyType = EU_CERT_KEY_TYPE_UNKNOWN;
					this.publicKeyBits = 0;
					this.publicKey = "";
					this.RSAModul = "";
					this.RSAExponent = "";
					this.publicKeyID = "";
					this.extKeyUsages = "";
					this.crlDistribPoint1 = "";
					this.crlDistribPoint2 = "";
					this.subjTypeExists = false;
					this.subjType = (int)EU_SUBJECT_TYPE.UNDIFFERENCED;
					this.subjSubType = (int)EU_SUBJECT_SUB_TYPE.CA_SERVER_UNDIFFERENCED;
					this.selfSigned = false;
					this.signIssuer = "";
					this.signSerial = "";
					this.subjUNZR = "";
					this.subjCountry = "";

					this.certRequestInfo = new byte[0];
				}
			}
		};

		public struct EU_SCC_STATISTIC
		{
			public int version;
			public ulong activeSessions;
			public ulong gatedSessions;
			public ulong unprotectedData;
			public ulong protectedData;

			public EU_SCC_STATISTIC(IntPtr intStatistic)
			{
				try
				{
					IntPtr curPtr = intStatistic;

					curPtr = EUMarshal.ReadDWORD(curPtr, out version);
					curPtr = EUMarshal.ReadDWORDLONG(curPtr, out activeSessions);
					curPtr = EUMarshal.ReadDWORDLONG(curPtr, out gatedSessions);
					curPtr = EUMarshal.ReadDWORDLONG(curPtr, out unprotectedData);
					curPtr = EUMarshal.ReadDWORDLONG(curPtr, out protectedData);
				}
				catch (Exception)
				{
					this.version = 0;
					this.activeSessions = 0;
					this.gatedSessions = 0;
					this.unprotectedData = 0;
					this.protectedData = 0;
				}
			}
		};

		public struct EU_TIME_INFO
		{
			public int version;
			public bool timeAvail;
			public bool timeStamp;
			public SYSTEMTIME time;
			public bool signTimeStampAvail;
			public SYSTEMTIME signTimeStamp;

			public EU_TIME_INFO(IntPtr intTimeInfo)
			{
				try
				{
					IntPtr curPtr = intTimeInfo;

					curPtr = EUMarshal.ReadDWORD(curPtr, out version);
					curPtr = EUMarshal.ReadBOOL(curPtr, out timeAvail);
					curPtr = EUMarshal.ReadBOOL(curPtr, out timeStamp);
					curPtr = EUMarshal.ReadSYSTEMTIME(curPtr, out time);

					if (version > EU_TIME_INFO_VERSION_1)
					{
						curPtr = EUMarshal.ReadBOOL(curPtr,
							out signTimeStampAvail);
						curPtr = EUMarshal.ReadSYSTEMTIME(curPtr,
							out signTimeStamp);
					}
					else
					{
						this.signTimeStampAvail = false;
						this.signTimeStamp = new SYSTEMTIME();
					}
				}
				catch (Exception)
				{
					this.version = 0;
					this.timeAvail = false;
					this.timeStamp = false;
					this.time = new SYSTEMTIME();
					this.signTimeStampAvail = false;
					this.signTimeStamp = new SYSTEMTIME();
				}
			}
		};

		public struct EU_PRIVATE_KEY_INFO
		{
			public int keyType;
			public int keyUsage;
			public string[] keyIDs;
			public bool isTrustedKeyIDs;

			public EU_PRIVATE_KEY_INFO(
				int keyType, int keyUsage, string[] keyIDs)
			{
				this.keyType = keyType;
				this.keyUsage = keyUsage;
				this.keyIDs = keyIDs;
				this.isTrustedKeyIDs = (keyIDs.Length == 1);
			}
		};

		public struct EU_USER_INFO
		{
			public int version;

			public string commonName;
			public string locality;
			public string state;
			public string organization;
			public string orgUnit;
			public string title;
			public string street;
			public string phone;
			public string surname;
			public string givenname;
			public string email;
			public string dns;
			public string edrpouCode;
			public string drfoCode;
			public string nbuCode;
			public string spfmCode;
			public string oCode;
			public string ouCode;
			public string userCode;
			public string upn;

			public string unzr;
			public string country;
		};

		public struct EU_KEY_MEDIA_DEVICE_INFO
		{
			public int version;
			public string deviceNameAlias;

			public EU_KEY_MEDIA_DEVICE_INFO(IntPtr intInfo)
			{
				try
				{
					IntPtr curPtr = intInfo;

					curPtr = EUMarshal.ReadDWORD(curPtr, out version);
					curPtr = EUMarshal.ReadString(curPtr, out deviceNameAlias);
				}
				catch (Exception)
				{
					this.version = 0;
					this.deviceNameAlias = "";
				}
			}
		};

		public struct EU_TRANSPORT_HEADER
		{
			public int receiptNumber;
			public byte[] cryptoData;

			public EU_TRANSPORT_HEADER(
				int receiptNumber, byte[] cryptoData)
			{
				this.receiptNumber = receiptNumber;
				this.cryptoData = cryptoData;
			}
		};

		public struct EU_CRYPTO_HEADER
		{
			public string caType;
			public EU_HEADER_PART_TYPE headerType;
			public int headerSize;
			public byte[] cryptoData;

			public EU_CRYPTO_HEADER(
				string caType, EU_HEADER_PART_TYPE headerType, 
				int headerSize, byte[] cryptoData)
			{
				this.caType = caType;
				this.headerType = headerType;
				this.headerSize = headerSize;
				this.cryptoData = cryptoData;
			}
		};

		#endregion

		#region EUSignCP: Private section
		#region EUSignCP: Exception class

//__MONO_CODE__#if !XAMARIN_SDK
		[Serializable()]
//__MONO_CODE__#endif

		public class EUSignCPException: System.Exception
		{
			private int _errorCode;

			public EUSignCPException() : base() { }
			public EUSignCPException(
				string message) : base(message) { }
			public EUSignCPException(string message, 
				System.Exception inner) : base(message, inner) { }
			public EUSignCPException(int errorCode) 
			{
				_errorCode = errorCode;
			}
			public EUSignCPException(int errorCode,
				string message) : base (message)
			{
				_errorCode = errorCode;
			}

//__MONO_CODE__#if !XAMARIN_SDK
			protected EUSignCPException(
				System.Runtime.Serialization.SerializationInfo info,
				System.Runtime.Serialization.StreamingContext context) { }
//__MONO_CODE__#endif

			public int errorCode 
			{
				get
				{
					return _errorCode;
				}
			}
		}
		#endregion

		#region EUSignCP: Marshaling class
		public class EUMarshal
		{
			private bool _isLibraryDataPtr;
			private IntPtr _context;

			private int _dataLength;
			private IntPtr _intDataPtr;

			private IntPtr _intBinaryDataLengthPtr;

			public static int PTR_SIZE = Marshal.SizeOf(typeof(IntPtr));
			public static int INT_SIZE = 4;
			public static int BOOL_SIZE = 4;
			public static int WCHAR_SIZE = 4;
/*__BEGIN_MONO_CODE__
#if !XAMARIN_SDK
__END_MONO_CODE__*/
			public static int DWORD_SIZE = 4;
/*__BEGIN_MONO_CODE__
#else // !XAMARIN_SDK
			public static int DWORD_SIZE = PTR_SIZE;
#endif // !XAMARIN_SDK
__END_MONO_CODE__*/
			public static int DWORDLONG_SIZE = 8;
			public const int EU_KEY_MEDIA_SIZE = 81;
			public const int EU_CERT_OWNER_INFO_SIZE = 140;
			public const int EU_SIGN_INFO_SIZE = 164;
			public const int EU_SENDER_INFO_SIZE = 164;
			public const int EU_CRL_INFO_SIZE = 60;
			public const int EU_CERT_INFO_SIZE = 368;
			public const int EU_CRL_DETAILED_INFO_SIZE = 84;
			public const int EU_ERROR_MAX_DESC = 1025;
			public const int EU_USER_INFO_SIZE = 1558;


			public static IntPtr ReadIntPtr(IntPtr intPtr, out IntPtr intValue)
			{
				intValue = Marshal.ReadIntPtr(intPtr);

				return (IntPtr)(intPtr.ToInt64() + DWORD_SIZE);
			}

			public static IntPtr ReadINT(IntPtr intPtr, out int nValue)
			{
				nValue = Marshal.ReadInt32(intPtr);
				return (IntPtr)(intPtr.ToInt64() + INT_SIZE);
			}

			public static IntPtr ReadBOOL(IntPtr intPtr, out bool bValue)
			{
				bValue = (Marshal.ReadInt32(intPtr) != 0);
				return (IntPtr)(intPtr.ToInt64() + BOOL_SIZE);
			}

			public static IntPtr ReadDWORD(IntPtr intPtr, out int dwValue)
			{
//__MONO_CODE__#if !XAMARIN_SDK
				dwValue = Marshal.ReadInt32(intPtr);
/*__BEGIN_MONO_CODE__
#else // !XAMARIN_SDK
				if (DWORD_SIZE == INT_SIZE)
					dwValue = Marshal.ReadInt32(intPtr);
				else
					dwValue = (int) Marshal.ReadInt64(intPtr);
#endif // !XAMARIN_SDK
__END_MONO_CODE__*/

				return (IntPtr)(intPtr.ToInt64() + DWORD_SIZE);
			}

			public static IntPtr WriteDWORD(IntPtr intPtr, int dwValue)
			{
//__MONO_CODE__#if !XAMARIN_SDK
				Marshal.WriteInt32(intPtr, dwValue);
/*__BEGIN_MONO_CODE__
#else // !XAMARIN_SDK
				if (DWORD_SIZE == INT_SIZE)
					Marshal.WriteInt32(intPtr, dwValue);
				else
					Marshal.WriteInt64(intPtr, (long) dwValue);
#endif // !XAMARIN_SDK
__END_MONO_CODE__*/

				return (IntPtr)(intPtr.ToInt64() + DWORD_SIZE);
			}

			public static void WriteDWORD(IntPtr intPtr, int intPtrOfs, int dwValue)
			{
//__MONO_CODE__#if !XAMARIN_SDK
				Marshal.WriteInt32(intPtr, intPtrOfs, dwValue);
/*__BEGIN_MONO_CODE__
#else // !XAMARIN_SDK
				if (DWORD_SIZE == INT_SIZE)
					Marshal.WriteInt32(intPtr, intPtrOfs, dwValue);
				else
					Marshal.WriteInt64(intPtr, intPtrOfs, (long) dwValue);
#endif // !XAMARIN_SDK
__END_MONO_CODE__*/
			}

			public static IntPtr ReadDWORDLONG(IntPtr intPtr, out ulong ulValue)
			{
				ulValue = (ulong) Marshal.ReadInt64(intPtr);
				return (IntPtr)(intPtr.ToInt64() + DWORDLONG_SIZE);
			}

			public static IntPtr ReadString(IntPtr intPtr, out string szValue)
			{
				szValue = PtrToStringAnsi(Marshal.ReadIntPtr(intPtr));
				return (IntPtr)(intPtr.ToInt64() + PTR_SIZE);
			}

			public static IntPtr ReadSYSTEMTIME(IntPtr intPtr, out SYSTEMTIME time)
			{
				time = (SYSTEMTIME) Marshal.PtrToStructure(
					intPtr, typeof(SYSTEMTIME));
				return (IntPtr)(intPtr.ToInt64() + Marshal.SizeOf(typeof(SYSTEMTIME)));
			}

			public static int CopyStringToIntPtr(
				string data, IntPtr intPtr, int maxLength = -1)
			{
				if (data == null)
					data = "";

				int length = data.Length + 1;
				if (maxLength != -1 && length > maxLength)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_BAD_PARAMETER);
				}

				IntPtr intString;

				intString = StringToHGlobalAnsi(data);
				CopyMemory(intPtr, intString, (uint) length);
				Marshal.FreeHGlobal(intString);

				return length;
			}

			public static IntPtr WriteStringToIntPtr(
				string data, IntPtr intPtr, int maxLength = -1)
			{
				int length = CopyStringToIntPtr(data, intPtr, maxLength);
				if (length != -1)
					length = maxLength;

				return (IntPtr)(intPtr.ToInt64() + length);
			}

			public static void CopyArraysOfBytesToIntPtr(byte[][] array,
				ref IntPtr intArraysPointer, ref IntPtr intArraysLengthPointer)
			{
				IntPtr intArrays = IntPtr.Zero;
				IntPtr intArraysLength = IntPtr.Zero;
				int arraysCount = array.Length;

				try
				{
					intArrays = Marshal.AllocHGlobal(
						PTR_SIZE * arraysCount);
					intArraysLength = Marshal.AllocHGlobal(
						DWORD_SIZE * arraysCount);
					if (intArrays == IntPtr.Zero ||
						intArraysLength == IntPtr.Zero)
					{
						throw new EUSignCPException(
							IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
					}

					for (int i = 0; i < arraysCount; i++)
					{
						Marshal.WriteIntPtr(intArrays,
							i * PTR_SIZE, IntPtr.Zero);
					}

					for (int i = 0; i < arraysCount; i++)
					{
						IntPtr intArray = IntPtr.Zero;
						int arrayLength = array[i].Length;
						intArray = Marshal.AllocHGlobal(arrayLength);
						if (intArray == IntPtr.Zero)
						{
							throw new EUSignCPException(
								IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
						}

						Marshal.Copy(array[i], 0, intArray, arrayLength);
						Marshal.WriteIntPtr(intArrays,
							i * PTR_SIZE, intArray);
						WriteDWORD(intArraysLength,
							i * DWORD_SIZE, arrayLength);
					}

					intArraysPointer = intArrays;
					intArraysLengthPointer = intArraysLength;
				}
				catch (Exception)
				{
					FreeArraysOfBytesInIntPtr(
						arraysCount, intArrays, intArraysLength);

					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public static void FreeArraysOfBytesInIntPtr(int count,
				IntPtr intArraysPointer, IntPtr intArraysLengthPointer)
			{
				try
				{
					if (intArraysPointer != IntPtr.Zero)
					{
						for (int i = 0; i < count; i++)
						{
							IntPtr intArray = Marshal.ReadIntPtr(
								intArraysPointer,
								i * Marshal.SizeOf(typeof(IntPtr)));
							if (intArray != IntPtr.Zero)
								Marshal.FreeHGlobal(intArray);
						}

						Marshal.FreeHGlobal(intArraysPointer);
					}

					if (intArraysLengthPointer != IntPtr.Zero)
						Marshal.FreeHGlobal(intArraysLengthPointer);
				}
				catch (Exception)
				{

				}
			}

			public static bool StringToSystemTime(
				string timeString, out SYSTEMTIME time)
			{
				time = new SYSTEMTIME();

				try
				{
					DateTime date = DateTime.ParseExact(
						timeString, "dd.MM.yyyy HH:mm:ss",
						System.Globalization.CultureInfo.InvariantCulture);

					time.wYear = (short)date.Year;
					time.wMonth = (short)date.Month;
					time.wDay = (short)date.Day;

					time.wHour = (short)date.Hour;
					time.wMinute = (short)date.Minute;
					time.wSecond = (short)date.Second;

					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}

			public static bool SystemTimeToString(
				SYSTEMTIME time, out string timeString)
			{
				try
				{
					DateTime date = new DateTime(
						(int)time.wYear, (int)time.wMonth, (int)time.wDay,
						(int)time.wHour, (int)time.wMinute, (int)time.wSecond);

					timeString = date.ToString("dd.MM.yyyy HH:mm:ss");

					return true;
				}
				catch (Exception)
				{
					timeString = null;
					return false;
				}
			}

			public EUMarshal()
			{
				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_dataLength = 0;
				_intDataPtr = IntPtr.Zero;
				_intBinaryDataLengthPtr = IntPtr.Zero;
			}

			public EUMarshal(int dataLength)
			{
				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_dataLength = dataLength;
				_intBinaryDataLengthPtr = IntPtr.Zero;

				_intDataPtr = Marshal.AllocHGlobal(_dataLength);
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public EUMarshal(bool isBinaryDataPtr, IntPtr context = new IntPtr())
			{
				_isLibraryDataPtr = true;
				_context = context;
				_intBinaryDataLengthPtr = IntPtr.Zero;

				_dataLength = PTR_SIZE;

				_intDataPtr = Marshal.AllocHGlobal(_dataLength);
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}

				Marshal.WriteIntPtr(_intDataPtr, IntPtr.Zero);

				if (isBinaryDataPtr)
				{
					_intBinaryDataLengthPtr = Marshal.AllocHGlobal(
						EUMarshal.DWORD_SIZE);
					if (_intBinaryDataLengthPtr == IntPtr.Zero)
					{
						throw new EUSignCPException(
							IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
					}
				}
			}

			public EUMarshal(byte[] array)
			{
				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_intBinaryDataLengthPtr = IntPtr.Zero;

				_dataLength = array.Length;

				_intDataPtr = Marshal.AllocHGlobal(_dataLength);
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}

				Marshal.Copy(array, 0, _intDataPtr, _dataLength);
			}

			public EUMarshal(string aString, bool isANSI = true)
			{
				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_intBinaryDataLengthPtr = IntPtr.Zero;

				if (isANSI)
				{
					_dataLength = aString.Length;
					_intDataPtr = StringToHGlobalAnsi(aString);
				}
				else
				{
					_dataLength = aString.Length * 2;
					_intDataPtr = Marshal.StringToHGlobalUni(aString);
				}

				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public EUMarshal(string[] aStrings)
			{
				long offset = 0;

				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_dataLength = 0;
				_intBinaryDataLengthPtr = IntPtr.Zero;

				foreach (String str in aStrings)
					_dataLength += (str.Length + 1);
				_dataLength += 1;

				_intDataPtr = Marshal.AllocHGlobal(_dataLength + 1);
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}

				foreach (String str in aStrings)
				{
					offset += CopyStringToIntPtr(str,
						(IntPtr)(_intDataPtr.ToInt64() + offset));
				}

				Marshal.WriteByte(
					(IntPtr)(_intDataPtr.ToInt64() + offset), 0);
			}

			public EUMarshal(bool[] array)
			{
				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_intBinaryDataLengthPtr = IntPtr.Zero;

				_dataLength = array.Length;

				_intDataPtr = Marshal.AllocHGlobal(_dataLength * BOOL_SIZE);
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}

				IntPtr curPtr = _intDataPtr;
				for (int i = 0; i < _dataLength; i++)
				{
					Marshal.WriteInt32(curPtr, array[i] ? 1 : 0);
					curPtr = (IntPtr)(curPtr.ToInt64() + BOOL_SIZE);
				}
			}

			public EUMarshal(EU_KEY_MEDIA keyMedia)
			{
				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_dataLength = EU_KEY_MEDIA_SIZE;
				_intBinaryDataLengthPtr = IntPtr.Zero;

				try
				{
					_intDataPtr = Marshal.AllocHGlobal(_dataLength);

					IntPtr curPtr = _intDataPtr;
					curPtr = WriteDWORD(curPtr, keyMedia.typeIndex);
					curPtr = WriteDWORD(curPtr, keyMedia.deviceIndex);
					curPtr = WriteStringToIntPtr(keyMedia.password, 
						curPtr, EU_PASS_MAX_LENGTH + 1);
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public EUMarshal(SYSTEMTIME time)
			{
				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_dataLength = Marshal.SizeOf(time);
				_intBinaryDataLengthPtr = IntPtr.Zero;

				try
				{
					_intDataPtr = Marshal.AllocHGlobal(_dataLength);
					Marshal.StructureToPtr(time, _intDataPtr, false);
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public EUMarshal(EU_USER_INFO userInfo)
			{
				_isLibraryDataPtr = false;
				_context = IntPtr.Zero;
				_dataLength = EU_USER_INFO_SIZE;
				_intBinaryDataLengthPtr = IntPtr.Zero;

				try
				{
					_intDataPtr = Marshal.AllocHGlobal(_dataLength);
					IntPtr curPtr = _intDataPtr;

					curPtr = WriteDWORD(curPtr, EU_USER_INFO_VERSION);
					curPtr = WriteStringToIntPtr(userInfo.commonName, 
						curPtr, EU_COMMON_NAME_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.locality,
						curPtr, EU_LOCALITY_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.state,
						curPtr, EU_STATE_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.organization,
						curPtr, EU_ORGANIZATION_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.orgUnit,
						curPtr, EU_ORG_UNIT_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.title,
						curPtr, EU_TITLE_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.street,
						curPtr, EU_STREET_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.phone,
						curPtr, EU_PHONE_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.surname,
						curPtr, EU_SURNAME_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.givenname,
						curPtr, EU_GIVENNAME_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.email,
						curPtr, EU_EMAIL_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.dns,
						curPtr, EU_ADDRESS_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.edrpouCode,
						curPtr, EU_EDRPOU_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.drfoCode,
						curPtr, EU_DRFO_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.nbuCode,
						curPtr, EU_NBU_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.spfmCode,
						curPtr, EU_SPFM_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.oCode,
						curPtr, EU_O_CODE_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.ouCode,
						curPtr, EU_OU_CODE_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.userCode,
						curPtr, EU_USER_CODE_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.upn,
						curPtr, EU_UPN_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.unzr,
						curPtr, EU_UNZR_MAX_LENGTH + 1);
					curPtr = WriteStringToIntPtr(userInfo.country,
						curPtr, EU_COUNTRY_MAX_LENGTH + 1);
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			~EUMarshal()
			{
				try
				{
					FreeData();
				}
				catch (Exception)
				{
				}
			}

			public IntPtr DataPtr
			{
				get
				{
					return _intDataPtr;
				}
			}

			public IntPtr BinaryDataLengthPtr
			{
				get
				{
					return _intBinaryDataLengthPtr;
				}
			}

			public DWORD DataLength
			{
				get
				{
					return (DWORD)_dataLength;
				}
			}

			public byte[] GetBinaryData(bool freeData = true)
			{
				if (_intDataPtr == IntPtr.Zero ||
					_intBinaryDataLengthPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_BAD_PARAMETER);
				}

				try
				{
					int binaryDataLength = 0;
					ReadDWORD(_intBinaryDataLengthPtr, out binaryDataLength);
					IntPtr intBinaryData = Marshal.ReadIntPtr(_intDataPtr);

					byte[] binaryData = new byte[binaryDataLength];

					Marshal.Copy(intBinaryData, binaryData, 0, binaryDataLength);

					if (freeData)
						FreeData();

					return binaryData;
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public string GetStringData(bool freeData = true, bool isANSI = true)
			{
				if (_intDataPtr == IntPtr.Zero ||
					(!isANSI && _intBinaryDataLengthPtr == IntPtr.Zero))
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_BAD_PARAMETER);
				}

				try
				{
					string stringData;
					IntPtr intStringData;

					if (_isLibraryDataPtr)
					{
						intStringData = Marshal.ReadIntPtr(
							_intDataPtr);
					}
					else
						intStringData = _intDataPtr;

					if (isANSI)
					{
						stringData = PtrToStringAnsi(
							intStringData);
					}
					else
					{
						int stringDataLength = 0;
						ReadDWORD(_intBinaryDataLengthPtr, out stringDataLength);
						if ((stringDataLength % 2) != 0)
						{
							throw new EUSignCPException(
								IEUSignCP.EU_ERROR_BAD_PARAMETER);
						}

						stringDataLength = stringDataLength / 2;

						stringData = Marshal.PtrToStringUni(
							intStringData, stringDataLength);
					}

					if (freeData)
						FreeData();

					return stringData;
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public string[] GetStringsData(bool freeData = true)
			{
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_BAD_PARAMETER);
				}

				try
				{
					int count = 0;
					string[] strings;
					string str;

					IntPtr intStringsData;
					IntPtr intCur;

					if (_isLibraryDataPtr)
					{
						intStringsData = Marshal.ReadIntPtr(
							_intDataPtr);
					}
					else
						intStringsData = _intDataPtr;

					intCur = intStringsData;
					while (true)
					{
						str = PtrToStringAnsi(intCur);
						count++;

						intCur = (IntPtr)(intCur.ToInt64() + str.Length + 1);
						if (Marshal.ReadByte(intCur) == '\0')
							break;
					}

					strings = new string[count];

					intCur = intStringsData;
					for (int i = 0; i < count; i++)
					{
						strings[i] = PtrToStringAnsi(intCur);
						intCur = (IntPtr)(intCur.ToInt64() +
							strings[i].Length + 1);
					}

					if (freeData)
						FreeData();

					return strings;
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public int GetIntData(bool freeData = true)
			{
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_BAD_PARAMETER);
				}

				try
				{
					int data = Marshal.ReadInt32(_intDataPtr);

					if (freeData)
						FreeData();

					return data;
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public int GetDWORDData(bool freeData = true)
			{
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_BAD_PARAMETER);
				}

				try
				{
					int data = 0;
					ReadDWORD(_intDataPtr, out data);

					if (freeData)
						FreeData();

					return data;
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public bool GetBoolData(bool freeData = true)
			{
				return (GetIntData(freeData) != 0);
			}

			public IntPtr GetPointerData(bool freeData = true)
			{
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_BAD_PARAMETER);
				}

				try
				{
					IntPtr dataPtr = Marshal.ReadIntPtr(_intDataPtr);

					if (freeData)
						FreeData();

					return dataPtr;
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public EU_CERT_INFO_EX[] GetCertsInfoEx(bool freeData = true)
			{
				if (_intDataPtr == IntPtr.Zero)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_BAD_PARAMETER);
				}

				EU_CERT_INFO_EX[] certs;
				IntPtr certsPtr = IntPtr.Zero;

				try
				{
					IntPtr curPtr;
					int certsCount = 0;
					IntPtr certPtr;

					certsPtr = Marshal.ReadIntPtr(_intDataPtr);
					curPtr = certsPtr;

					curPtr = ReadDWORD(curPtr, out certsCount);
					certs = new EU_CERT_INFO_EX[certsCount];

					curPtr = Marshal.ReadIntPtr(curPtr);
					for (int i = 0; i < certsCount; i++)
					{
						certPtr = Marshal.ReadIntPtr(curPtr);
						certs[i] = new EU_CERT_INFO_EX(certPtr);
						curPtr = (IntPtr) (curPtr.ToInt64() + PTR_SIZE);
					}

					if (freeData)
					{
						EUFreeReceiversCertificates(certsPtr);
						FreeData();
					}

					return certs;
				}
				catch (Exception)
				{
					try
					{
						if (certsPtr != IntPtr.Zero)
							EUFreeReceiversCertificates(certsPtr);
					}
					catch (Exception)
					{

					}

					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}
			}

			public void FreeData()
			{
				try
				{
					if (_isLibraryDataPtr && _intDataPtr != IntPtr.Zero)
					{
						IntPtr intData = Marshal.ReadIntPtr(_intDataPtr);
						if (intData != IntPtr.Zero)
						{
							if (_context != IntPtr.Zero)
								EUCtxFreeMemory(_context, intData);
							else
								EUFreeMemory(intData);
						}

						Marshal.WriteIntPtr(_intDataPtr, IntPtr.Zero);
					}

					if (_intDataPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(_intDataPtr);
						_intDataPtr = IntPtr.Zero;
					}

					if (_intBinaryDataLengthPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(_intBinaryDataLengthPtr);
						_intBinaryDataLengthPtr = IntPtr.Zero;
					}
				}
				catch (Exception)
				{
					_intDataPtr = IntPtr.Zero;
					_intBinaryDataLengthPtr = IntPtr.Zero;
				}
			}
		}

		class EUMarshalArrayOfBytesArrays
		{
			private EUMarshal _countPtr;
			private EUMarshal _arraysPtr;
			private EUMarshal _sizesPtr;
			private FreeArrayOfBytesArrays _free;

			public delegate void FreeArrayOfBytesArrays(
				DWORD count, IntPtr arrays, IntPtr arraysSizes);

			public EUMarshalArrayOfBytesArrays(FreeArrayOfBytesArrays free)
			{
				_countPtr = new EUMarshal(Marshal.SizeOf(typeof(DWORD)));
				_arraysPtr = new EUMarshal(Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(_arraysPtr.DataPtr, IntPtr.Zero);
				_sizesPtr = new EUMarshal(Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(_sizesPtr.DataPtr, IntPtr.Zero);
				_free = free;
			}

			~EUMarshalArrayOfBytesArrays()
			{
				FreeData();
			}

			public IntPtr CountPtr
			{
				get
				{
					return _countPtr.DataPtr;
				}
			}

			public IntPtr ArraysPtr
			{
				get
				{
					return _arraysPtr.DataPtr;
				}
			}

			public IntPtr ArraysLengthesPtr
			{
				get
				{
					return _sizesPtr.DataPtr;
				}
			}

			public byte[][] GetBinaryDataArrays(bool freeData = true)
			{
				byte[][] arrays;

				int count = _countPtr.GetDWORDData(false);
				IntPtr arraysPtr = _arraysPtr.GetPointerData(false);
				IntPtr sizesPtr = _sizesPtr.GetPointerData(false);
				IntPtr arrayPtr;
                int arraySize;
				byte[] array;

				arrays = new byte[count][];

				try
				{
					for (int i = 0; i < count; i++)
					{
						arraysPtr = EUMarshal.ReadIntPtr(arraysPtr, out arrayPtr);
						sizesPtr = EUMarshal.ReadDWORD(sizesPtr, out arraySize);
						array = new byte[arraySize];
						Marshal.Copy(arrayPtr, array, 0, arraySize);

						arrays[i] = array;
					}
				}
				catch (Exception)
				{
					throw new EUSignCPException(
						IEUSignCP.EU_ERROR_MEMORY_ALLOCATION);
				}

				if (freeData)
					FreeData();

				return arrays;
			}

			public void FreeData()
			{
				if (_countPtr != null && _arraysPtr != null && _sizesPtr != null)
				{
					try {
						_free((DWORD)_countPtr.GetDWORDData(),
							_arraysPtr.GetPointerData(),
							_sizesPtr.GetPointerData());
					} catch (Exception)
					{
					}
				}

				if (_countPtr != null)
				{
					_countPtr.FreeData();
					_countPtr = null;
				}

				if (_arraysPtr != null)
				{
					_arraysPtr.FreeData();
					_arraysPtr = null;
				}

				if (_sizesPtr != null)
				{
					_sizesPtr.FreeData();
					_sizesPtr = null;
				}
			}
		}

		#endregion

		#region EUSignCP: Imported functions
/*__BEGIN_MONO_CODE__
#if __IOS__
		private const string EU_LIBRARY_NAME = "__Internal";
#elif (__ANDROID__ || __LINUX__)
		private const string EU_LIBRARY_NAME = "euscp.so";
		private const string OSI_LIBRARY_NAME = "libosi.so";
#else
__END_MONO_CODE__*/
		private const string EU_LIBRARY_NAME = "EUSignCP.dll";
/*__BEGIN_MONO_CODE__
#endif // __IOS__
__END_MONO_CODE__*/

/*__BEGIN_MONO_CODE__
#if !XAMARIN_SDK
__END_MONO_CODE__*/
		[DllImport("kernel32.dll")]
		private static extern void CopyMemory(IntPtr destination,
		IntPtr source, uint length);

		private static bool CompareMemory(byte[] arr1, byte[] arr2)
		{
			if (arr1.Length != arr2.Length)
				return false;

			for (long i = 0; i < arr1.Length; i++)
				if (arr1[i] != arr2[i])
					return false;
			return true;
		}
/*__BEGIN_MONO_CODE__
#else // !XAMARIN_SDK
		private static void CopyMemory(IntPtr destination,
			IntPtr source, uint length)
		{
			byte[] tmp = new byte[length];

			Marshal.Copy(source, tmp, 0, (int) length);
			Marshal.Copy(tmp, 0, destination, (int) length);
		}
		
		private static bool CompareMemory(byte[] arr1, byte[] arr2)
		{
			return arr1.SequenceEqual(arr2);
		}
#endif // !XAMARIN_SDK
__END_MONO_CODE__*/

/*__BEGIN_MONO_CODE__
#if __LINUX__
		[DllImport(OSI_LIBRARY_NAME)]
		private static extern int MultiByteToWideChar(
			uint CodePage, DWORD dwFlags, IntPtr lpMultiByteStr,
			int cbMultiByte, IntPtr lpWideCharStr, int cchWideChar);

		[DllImport(OSI_LIBRARY_NAME)]
		private static extern int WideCharToMultiByte(
			uint CodePage, DWORD dwFlags, IntPtr lpWideCharStr,
			int cchWideChar, IntPtr lpMultiByteStr, int cbMultiByte,
			IntPtr lpDefaultChar, IntPtr lpUsedDefaultChar);

		private static bool MultiByteToMultiByte(
			EU_STRING_ENCODING srcCodePage, IntPtr srcPtr, 
			EU_STRING_ENCODING dstCodePage, out IntPtr dstPtr)
		{
			EUMarshal wStr = null;
			int count;

			dstPtr = IntPtr.Zero;

			try
			{
				count = MultiByteToWideChar((uint) srcCodePage,
					(DWORD) 0, srcPtr, -1, IntPtr.Zero, 0);
				if (count < 0)
					return false;

				wStr = new EUMarshal(count * EUMarshal.WCHAR_SIZE);

				if (count != MultiByteToWideChar((uint) srcCodePage,
						(DWORD) 0, srcPtr, -1, wStr.DataPtr, count))
				{
					return false;
				}

				count = WideCharToMultiByte((uint) dstCodePage,
					(DWORD) 0, wStr.DataPtr, -1, IntPtr.Zero, 0,
					IntPtr.Zero, IntPtr.Zero);
				if (count < 0)
					return false;

				dstPtr = Marshal.AllocHGlobal(count);
				if (dstPtr == IntPtr.Zero)
					return false;

				if (count != WideCharToMultiByte((uint) dstCodePage,
						(DWORD) 0, wStr.DataPtr, -1, dstPtr, count,
						IntPtr.Zero, IntPtr.Zero))
				{
					Marshal.FreeHGlobal(dstPtr);
					dstPtr = IntPtr.Zero;

					return false;
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
			finally 
			{
				wStr.FreeData();
			}
		}
#endif //__LINUX__
__END_MONO_CODE__*/

		private static string PtrToStringAnsi(IntPtr strPtr)
		{
			try
			{
				string encoding = "windows-1251";
/*__BEGIN_MONO_CODE__
#if __LINUX__
				IntPtr _tmpPtr = IntPtr.Zero;

				if (!MultiByteToMultiByte(
						EU_STRING_ENCODING.CP1251, strPtr,
						EU_STRING_ENCODING.UTF8, out _tmpPtr))
				{
					return null;
				}

				strPtr = _tmpPtr;
				encoding = "utf-8";
#endif // __LINUX__
__END_MONO_CODE__*/
				long lPointer = strPtr.ToInt64();
				while (Marshal.ReadByte((IntPtr)lPointer) != 0)
					lPointer++;

				int count = (int)(lPointer - strPtr.ToInt64());

				if (count == 0)
					return "";

				byte[] strArray = new byte[count];

				Marshal.Copy(strPtr, strArray, 0, count);
/*__BEGIN_MONO_CODE__
#if __LINUX__
				Marshal.FreeHGlobal(strPtr);
#endif // __LINUX__
__END_MONO_CODE__*/

				return System.Text.Encoding.GetEncoding(encoding).GetString(strArray);
			}
			catch (Exception)
			{
				return null;
			}
		}

		private static IntPtr StringToHGlobalAnsi(string str)
		{
			try
			{
				string encoding = "windows-1251";
/*__BEGIN_MONO_CODE__
#if __LINUX__
				encoding = "utf-8";
#endif // __LINUX__
__END_MONO_CODE__*/
				byte[] strArray = System.Text.Encoding.GetEncoding(encoding).GetBytes(str);

				IntPtr strPtr = Marshal.AllocHGlobal(strArray.Length + 1);
				if (strPtr == IntPtr.Zero)
					return IntPtr.Zero;

				Marshal.Copy(strArray, 0, strPtr, strArray.Length);
				Marshal.WriteByte(
					(IntPtr)(strPtr.ToInt64() + strArray.Length), 0);

/*__BEGIN_MONO_CODE__
#if __LINUX__
				IntPtr _tmpPtr = IntPtr.Zero;

				if (!MultiByteToMultiByte(
						EU_STRING_ENCODING.UTF8, strPtr,
						EU_STRING_ENCODING.CP1251, out _tmpPtr))
				{
					Marshal.FreeHGlobal(strPtr);

					return IntPtr.Zero;
				}
				Marshal.FreeHGlobal(strPtr);

				strPtr = _tmpPtr;
#endif // __LINUX__
__END_MONO_CODE__*/

				return strPtr;
			}
			catch (Exception)
			{
				return IntPtr.Zero;
			}
		}

/*__BEGIN_MONO_CODE__
#if __ANDROID__
		[DllImport(OSI_LIBRARY_NAME)]
		private static extern void SetUSBDevice(
			int fd, IntPtr descriptors, int descriptorsSize);
#endif // __ANDROID__
__END_MONO_CODE__*/

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUInitialize();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUIsInitialized();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFinalize();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUSetSettings();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetSettingsFilePath(
			IntPtr settingsFilePath);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetSettingsFilePathEx(
			IntPtr settingsFilePath,
			DWORD rootKey, IntPtr regPath);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUShowCertificates();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUShowCRLs();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetPrivateKeyMedia(IntPtr keyMedia);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetPrivateKeyMediaEx(IntPtr caption,
			IntPtr keyMedia);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUReadPrivateKey(IntPtr keyMedia,
			IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUIsPrivateKeyReaded();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUResetPrivateKey();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeCertOwnerInfo(IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUShowOwnCertificate();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUShowSignInfo(IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeSignInfo(IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeMemory(IntPtr memory);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern IntPtr EUGetErrorDesc(DWORD error);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern IntPtr EUGetErrorLangDesc(
			DWORD error, DWORD lang);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignData(IntPtr data,
			DWORD dataLength, IntPtr signString, IntPtr signBinary,
			IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyData(IntPtr data,
			DWORD dataLength, IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataContinue(
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataEnd(IntPtr signString,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataBegin(IntPtr signString,
			IntPtr signBinary, DWORD signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataContinue(
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataEnd(IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUResetOperation();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignFile(IntPtr fileName,
			IntPtr fileNameWithsign, BOOL Externalsign);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyFile(IntPtr fileNameWithsign,
			IntPtr fileName, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataInternal(BOOL appendCert,
			IntPtr data, DWORD dataLength, IntPtr signedDataString,
			IntPtr signedDataBinary, IntPtr signedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataInternal(
			IntPtr signedDataString, IntPtr signedDataBinary,
			DWORD signedDataBinaryLength, IntPtr data, IntPtr dataLength,
			IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSelectCertificateInfo(IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUSetUIMode(BOOL uiMode);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashData(IntPtr data,
			DWORD dataLength, IntPtr hashString, IntPtr hashBinary,
			IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashDataContinue(
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashDataEnd(
			IntPtr hashString, IntPtr hashBinary,
			IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashFile(IntPtr fileName,
			IntPtr hashString, IntPtr hashBinary, IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignHash(IntPtr hashString,
			IntPtr hashBinary, DWORD hashBinaryLength, IntPtr signString,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyHash(IntPtr hashString,
			IntPtr hashBinary, DWORD hashBinaryLength, IntPtr signString,
			IntPtr signBinary, DWORD signBinaryLength, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnumKeyMediaTypes(
			DWORD typeIndex, IntPtr typeDescription);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnumKeyMediaDevices(
			DWORD typeIndex, DWORD deviceIndex, IntPtr deviceDescription);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetFileStoreSettings(
			IntPtr path, IntPtr checkCRLs, IntPtr autoRefresh,
			IntPtr ownCRLsOnly, IntPtr fullAndDeltaCRLs,
			IntPtr autoDownloadCRLs, IntPtr saveLoadedCerts,
			IntPtr expireTime);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetFileStoreSettings(IntPtr path, BOOL checkCRLs,
			BOOL autoRefresh, BOOL ownCRLsOnly, BOOL fullAndDeltaCRLs, BOOL autoDownloadCRLs,
			BOOL saveLoadedCerts, DWORD expireTime);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetProxySettings(IntPtr useProxy, IntPtr anonymous,
			IntPtr address, IntPtr port, IntPtr user, IntPtr password, IntPtr savePassword);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetProxySettings(BOOL useProxy, BOOL anonymous,
			IntPtr address, IntPtr port, IntPtr user, IntPtr password, BOOL savePassword);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetOCSPSettings(IntPtr useOCSP, IntPtr beforeStore,
			IntPtr address, IntPtr port);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetOCSPSettings(BOOL useOCSP, BOOL beforeStore,
			IntPtr address, IntPtr port);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetTSPSettings(IntPtr getStamps,
			IntPtr address, IntPtr port);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetTSPSettings(BOOL getStamps,
			IntPtr address, IntPtr port);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetLDAPSettings(IntPtr useLDAP,
			IntPtr address, IntPtr port, IntPtr anonymous, IntPtr user, IntPtr password);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetLDAPSettings(BOOL useLDAP,
			IntPtr address, IntPtr port, BOOL anonymous, IntPtr user, IntPtr password);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCMPSettings(IntPtr useCMP,
			IntPtr address, IntPtr port, IntPtr commonName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetCMPSettings(BOOL useCMP,
			IntPtr address, IntPtr port, IntPtr commonName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUDoesNeedSetSettings();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCertificatesCount(DWORD subjectType,
			DWORD subjectSubType, IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnumCertificates(DWORD subjectType,
			DWORD subjectSubType, DWORD index, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCRLsCount(IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnumCRLs(DWORD index, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeCRLInfo(IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCertificateInfo(IntPtr issuer, IntPtr serial,
			IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeCertificateInfo(IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCRLDetailedInfo(IntPtr issuer, DWORD crlNumber,
			IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeCRLDetailedInfo(IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUParseCRL(IntPtr crl, DWORD crlLength,
			IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetPrivateKeyMediaSettings(IntPtr sourceType,
			IntPtr showErrors, IntPtr typeIndex, IntPtr devIndex, IntPtr password);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetPrivateKeyMediaSettings(DWORD sourceType,
			BOOL showErrors, DWORD typeIndex, DWORD devIndex, IntPtr password);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSelectCMPServer(IntPtr commonName, IntPtr dns);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawSignData(IntPtr data, DWORD dataLength, IntPtr signString,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawVerifyData(IntPtr data, DWORD dataLength, IntPtr signString,
			IntPtr signBinary, DWORD signBinaryLength, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawVerifyDataEx(IntPtr cert, DWORD certLength,
			IntPtr data, DWORD dataLength, IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawSignHash(IntPtr hashString, IntPtr hashBinary,
			DWORD hashBinaryLength, IntPtr signString, IntPtr signBinary,
			IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawVerifyHash(IntPtr hashString, IntPtr hashBinary,
			DWORD hashBinaryLength, IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawSignFile(IntPtr fileName, IntPtr fileNameWithsign);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawVerifyFile(IntPtr fileNameWithsign, IntPtr fileName,
			IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUBASE64Encode(IntPtr dataBinary, DWORD dataBinaryLength,
			IntPtr dataString);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUBASE64Decode(IntPtr dataString, IntPtr dataBinary,
			IntPtr dataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopData(IntPtr recipientCertIssuer,
			IntPtr recipientCertSerial, BOOL signData, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevelopData(IntPtr envelopedDataString,
			IntPtr envelopedDataBinary, DWORD envelopedDataLength,
			IntPtr data, IntPtr dataLength, IntPtr senderInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUShowSenderInfo(IntPtr senderInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeSenderInfo(IntPtr senderInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUParseCertificate(IntPtr certificate,
			DWORD certificateLength, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUReadPrivateKeyBinary(IntPtr privateKey,
			DWORD privateKeyLength, IntPtr password, IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUReadPrivateKeyFile(IntPtr privateKeyFileName,
			IntPtr password, IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUSessionDestroy(IntPtr session);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUClientSessionCreateStep1(DWORD expireTime,
			IntPtr clientSession, IntPtr clientData, IntPtr clientDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUServerSessionCreateStep1(DWORD expireTime,
			IntPtr clientData, DWORD clientDataLength, IntPtr serverSession,
			IntPtr serverData, IntPtr serverDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUClientSessionCreateStep2(IntPtr clientSession,
			IntPtr serverData, DWORD serverDataLength, IntPtr clientData,
			IntPtr clientDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUServerSessionCreateStep2(IntPtr serverSession,
			IntPtr clientData, DWORD clientDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUSessionIsInitialized(IntPtr session);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSessionSave(IntPtr session,
			IntPtr sessionData, IntPtr sessionDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSessionLoad(IntPtr sessionData, DWORD sessionDataLength,
			IntPtr session);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSessionCheckCertificates(IntPtr session);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSessionEncrypt(IntPtr session,
			IntPtr data, DWORD dataLength, IntPtr encryptedData, IntPtr encryptedDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSessionEncryptContinue(IntPtr session,
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSessionDecrypt(IntPtr session,
			IntPtr encryptedData, DWORD encryptedDataLength, IntPtr data, IntPtr dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSessionDecryptContinue(IntPtr session,
			IntPtr encryptedData, DWORD encryptedDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUIsSignedData(IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUIsEnvelopedData(IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSessionGetPeerCertificateInfo(IntPtr session, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSaveCertificate(IntPtr certificate, DWORD certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURefreshFileStore(BOOL reload);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetModeSettings(IntPtr offlineMode);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetModeSettings(BOOL offlineMode);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCheckCertificate(IntPtr certificate,
			DWORD certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFile(IntPtr recipientCertIssuer,
			IntPtr recipientCertSerial, BOOL signData, IntPtr fileName,
			IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevelopFile(IntPtr envelopedFileName,
			IntPtr fileName, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUIsSignedFile(IntPtr fileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUIsEnvelopedFile(IntPtr fileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCertificate(IntPtr issuer, IntPtr serial,
			IntPtr pCertificate, IntPtr pCertificateBinary, IntPtr pCertificateBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetOwnCertificate(IntPtr pCertificate,
			IntPtr pCertificateBinary, IntPtr pCertificateBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnumOwnCertificates(DWORD index, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCertificateInfoEx(IntPtr issuer, IntPtr serial,
			IntPtr pInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeCertificateInfoEx(IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetReceiversCertificates(IntPtr pCertificates);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeReceiversCertificates(IntPtr certificates);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGeneratePrivateKeyEx(IntPtr keyMedia, BOOL setKeyMediaPassword,
			DWORD UAKeysType, DWORD UADSKeysSpec, DWORD UAKEPKeysSpec, IntPtr UAParamsPath,
			DWORD internationalKeysType, DWORD internationalKeysSpec, IntPtr internationalParamsPath,
			IntPtr userInfo, IntPtr extKeyUsages,
			IntPtr privKey, IntPtr privKeyLength,
			IntPtr privKeyInfo, IntPtr privKeyInfoLength,
			IntPtr UARequest, IntPtr UARequestLength, IntPtr UAReqFileName,
			IntPtr UAKEPRequest, IntPtr UAKEPRequestLength, IntPtr UAKEPReqFileName,
			IntPtr internationalRequest, IntPtr internationalRequestLength, IntPtr internationalReqFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUChangePrivateKeyPassword(IntPtr keyMedia,
			IntPtr newPassword);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUChangeSoftwarePrivateKeyPassword(
			IntPtr privKeySource, DWORD privKeySourceLength, IntPtr oldPassword,
			IntPtr newPassword, IntPtr privKeyTarget, IntPtr privKeyTargetLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUBackupPrivateKey(IntPtr sourceKeyMedia,
			IntPtr targetKeyMedia);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDestroyPrivateKey(IntPtr keyMedia);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUIsHardwareKeyMedia(IntPtr keyMedia,
			IntPtr isHardware);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUIsPrivateKeyExists(IntPtr keyMedia,
			IntPtr isExists);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetKeyMediaPassword(IntPtr keyMedia);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCRInfo(IntPtr request, DWORD requestLength,
			IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeCRInfo(IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSaveCertificates(IntPtr certificates,
			DWORD certificatesLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUIsCertificates(IntPtr certificates,
			DWORD certificatesLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUIsCertificatesFile(IntPtr fileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSaveCRL(BOOL fullCRL, IntPtr crl, DWORD crlLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCertificateByEMail(IntPtr email,
			DWORD certKeyType, DWORD keyUsage, IntPtr onTime, IntPtr issuer, IntPtr serial);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCertificateByNBUCode(IntPtr nbuCode,
			DWORD certKeyType, DWORD keyUsage, IntPtr onTime, IntPtr issuer, IntPtr serial);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUAppendSign(IntPtr data, DWORD dataLength,
			IntPtr previousSignString, IntPtr previousSignBinary, DWORD previousSignBinaryLength,
			IntPtr signString, IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUAppendSignInternal(BOOL appendCert,
			IntPtr previousSignString, IntPtr previousSignBinary, DWORD previousSignBinaryLength,
			IntPtr signedDataString, IntPtr signedDataBinary, IntPtr signedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataSpecific(IntPtr data, DWORD dataLength,
			DWORD signIndex, IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataInternalSpecific(DWORD signIndex,
			IntPtr signedDataString, IntPtr signedDataBinary, DWORD signedDataBinaryLength,
			IntPtr data, IntPtr dataLength, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUAppendSignBegin(IntPtr previousSignString,
			IntPtr previousSignBinary, DWORD previousSignBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataSpecificBegin(DWORD signIndex,
			IntPtr signString, IntPtr signBinary, DWORD signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUAppendSignFile(IntPtr fileName,
			IntPtr fileNameWithPreviousSign, IntPtr fileNameWithSign,
			BOOL Externalsign);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyFileSpecific(DWORD signIndex,
			IntPtr fileNameWithsign, IntPtr fileName, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUAppendSignHash(IntPtr hashString,
			IntPtr hashBinary, DWORD hashBinaryLength,
			IntPtr previousSignString, IntPtr previousSignBinary, DWORD previousSignBinaryLength,
			IntPtr signString, IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyHashSpecific(IntPtr hashString, IntPtr hashBinary,
			DWORD hashBinaryLength, DWORD signIndex, IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetSignsCount(IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetSignerInfo(DWORD signIndex, IntPtr signString,
			IntPtr signBinary, DWORD signBinaryLength, IntPtr certInfoEx,
			IntPtr certificate, IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetFileSignsCount(IntPtr fileNameWithSign,
			IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetFileSignerInfo(DWORD signIndex,
			IntPtr fileNameWithSign, IntPtr certInfoEx, IntPtr certificate,
			IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUIsAlreadySigned(IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr isAlreadySigned);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUIsFileAlreadySigned(IntPtr fileNameWithSign,
			IntPtr isAlreadySigned);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashDataWithParams(IntPtr certificate,
			DWORD certificateLength, IntPtr data, DWORD dataLength, IntPtr hashString,
			IntPtr hashBinary, IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashDataBeginWithParams(IntPtr certificate,
			DWORD certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashFileWithParams(IntPtr certificate,
			DWORD certificateLength, IntPtr fileName, IntPtr hashString,
			IntPtr hashBinary, IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashDataBeginWithParamsCtx(IntPtr certificate,
			DWORD certificateLength, IntPtr context);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashDataContinueCtx(IntPtr context,
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUHashDataEndCtx(IntPtr context,
			IntPtr hashString, IntPtr hashBinary, IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataEx(
			IntPtr recipientCertIssuers, IntPtr recipientCertSerials,
			BOOL signData, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary, IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFileEx(
			IntPtr recipientCertIssuers, IntPtr recipientCertSerials,
			BOOL signData, IntPtr fileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataToRecipients(
			DWORD recipientsCerts, IntPtr recipientCerts, IntPtr recipientCertsLength,
			BOOL signData, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary, IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFileToRecipients(
			DWORD recipientsCerts, IntPtr recipientCerts, IntPtr recipientCertsLength,
			BOOL signData, IntPtr fileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataExWithDynamicKey(
			IntPtr recipientCertIssuers, IntPtr recipientCertSerials,
			BOOL signData, BOOL appendCert, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary, IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFileExWithDynamicKey(
			IntPtr recipientCertIssuers, IntPtr recipientCertSerials,
			BOOL signData, BOOL appendCert, IntPtr fileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataToRecipientsWithDynamicKey(
			DWORD recipientsCerts, IntPtr recipientCerts, IntPtr recipientCertsLength,
			BOOL signData, BOOL appendCert, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary, IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFileToRecipientsWithDynamicKey(
			DWORD recipientsCerts, IntPtr recipientCerts, IntPtr recipientCertsLength,
			BOOL signData, BOOL appendCert, IntPtr fileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCreateEmptySign(
			IntPtr data, DWORD dataLength, IntPtr signString,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCreateSigner(
			IntPtr hashString, IntPtr hashBinary, DWORD hashBinaryLength,
			IntPtr signerString, IntPtr signerBinary, IntPtr signerBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUAppendSigner(
			IntPtr signerString, IntPtr signerBinary, DWORD signerBinaryLength,
			IntPtr certificate, DWORD certificateLength,
			IntPtr previousSignString, IntPtr previousSignBinary, DWORD previousSignLength,
			IntPtr signString, IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetRuntimeParameter(
			IntPtr parameterName, IntPtr parameterValue, DWORD parameterValueLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataToRecipientsEx(
			DWORD recipientsCerts, IntPtr recipientCerts, IntPtr recipientCertsLength,
			DWORD recipientsAppendType, BOOL signData, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary, IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFileToRecipientsEx(
			DWORD recipientsCerts, IntPtr recipientCerts, IntPtr recipientCertsLength,
			DWORD recipientsAppendType, BOOL signData, IntPtr fileName,
			IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataToRecipientsWithOCode(
			IntPtr recipientsOCode, DWORD recipientsAppendType, BOOL signData,
			IntPtr data, DWORD dataLength, IntPtr envelopedDataString,
			IntPtr envelopedDataBinary, IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataContinueCtx(IntPtr context,
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataEndCtx(IntPtr context,
			BOOL appendCert, IntPtr signString, IntPtr signBinary,
			IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataBeginCtx(
			IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr context);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataContinueCtx(
			IntPtr context, IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataEndCtx(IntPtr context,
			IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUResetOperationCtx(IntPtr context);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataRSA(IntPtr data, DWORD dataLength,
			BOOL appendCert, BOOL externalSign, IntPtr signedDataString,
			IntPtr signedDataBinary, IntPtr signedDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataRSAContinue(
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataRSAEnd(BOOL appendCert,
			IntPtr signString, IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignFileRSA(IntPtr fileName,
			IntPtr fileNameWithsign, BOOL Externalsign);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataRSAContinueCtx(IntPtr context,
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSignDataRSAEndCtx(IntPtr context,
			BOOL appendCert, IntPtr signString, IntPtr signBinary,
			IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetKeyInfo(IntPtr keyMedia,
			IntPtr privKeyInfo, IntPtr privKeyInfoLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetKeyInfoBinary(IntPtr privateKey,
			DWORD privateKeyLength, IntPtr password,
			IntPtr privKeyInfo, IntPtr privKeyInfoLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetKeyInfoFile(IntPtr privateKeyFileName,
			IntPtr password, IntPtr privKeyInfo, IntPtr privKeyInfoLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCertificatesByKeyInfo(IntPtr privKeyInfo,
			DWORD privKeyInfoLength, IntPtr cmpServers, IntPtr cmpServersPorts,
			IntPtr certificates, IntPtr certificatesLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopAppendData(IntPtr data, DWORD dataLength,
			IntPtr previousEnvelopedDataString, IntPtr previousEnvelopedDataBinary,
			DWORD previousEnvelopedDataBinaryLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopAppendFile(IntPtr fileName,
			IntPtr previousEnvelopedFileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopAppendDataEx(IntPtr data, DWORD dataLength,
			IntPtr senderCert, DWORD senderCertLength,
			IntPtr previousEnvelopedDataString, IntPtr previousEnvelopedDataBinary,
			DWORD previousEnvelopedDataBinaryLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopAppendFileEx(IntPtr fileName,
			IntPtr senderCert, DWORD senderCertLength,
			IntPtr previousEnvelopedFileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevelopDataEx(IntPtr envelopedDataString,
			IntPtr envelopedDataBinary, DWORD envelopedDataLength,
			IntPtr senderCert, DWORD senderCertLength,
			IntPtr data, IntPtr dataLength, IntPtr senderInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevelopFileEx(IntPtr envelopedFileName,
			IntPtr senderCert, DWORD senderCertLength,
			IntPtr fileName, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetOCSPAccessInfoModeSettings(
			IntPtr enabled);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetOCSPAccessInfoModeSettings(BOOL enabled);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnumOCSPAccessInfoSettings(DWORD index,
			IntPtr issuerCN, IntPtr address, IntPtr port);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetOCSPAccessInfoSettings(
			IntPtr issuerCN, IntPtr address, IntPtr port);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetOCSPAccessInfoSettings(
			IntPtr issuerCN, IntPtr address, IntPtr port);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDeleteOCSPAccessInfoSettings(
			IntPtr issuerCN);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCheckCertificateByIssuerAndSerialEx(
			IntPtr issuer, IntPtr serial, IntPtr certificate,
			IntPtr certificateBinary, IntPtr certificateBinaryLength,
			IntPtr ocspAvailability);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUParseCertificateEx(IntPtr certificate,
			DWORD certificateLength, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUClientDynamicKeySessionCreate(DWORD expireTime,
			IntPtr serverCertIssuer, IntPtr serverCertSerial,
			IntPtr serverCertBinary, DWORD serverCertBinaryLength,
			IntPtr clientSession, IntPtr clientData, IntPtr clientDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUServerDynamicKeySessionCreate(DWORD expireTime,
			IntPtr clientData, DWORD clientDataLength, IntPtr serverSession);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDownloadFileViaHTTP(IntPtr url,
			IntPtr fileName, IntPtr fileData, IntPtr fileDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetStorageParameter(BOOL isProtected,
			IntPtr name, IntPtr value);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetStorageParameter(BOOL isProtected,
			IntPtr name, IntPtr value);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUIsOldFormatSign(IntPtr signString,
			IntPtr signBinary, DWORD signBinaryLength, IntPtr oldFormatSign);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUIsOldFormatSignFile(IntPtr fileNameWithSign,
			IntPtr oldFormatSign);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetSenderInfo(IntPtr envelopedDataString,
			IntPtr envelopedDataBinary, DWORD envelopedDataBinaryLength,
			IntPtr recipientCert, DWORD recipientCertLength, IntPtr dynamicKey,
			IntPtr info, IntPtr certificate, IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetFileSenderInfo(IntPtr envelopedFileName,
			IntPtr recipientCert, DWORD recipientCertLength, IntPtr dynamicKey,
			IntPtr info, IntPtr certificate, IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSCClientIsRunning(IntPtr running);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSCClientStart();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUSCClientStop();

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSCClientAddGate(
			IntPtr gateName,
			short connectPort,
			IntPtr gatewayAddress,
			short gatewayPort,
			IntPtr externalInterface,
			IntPtr externalRouterIPAddress);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSCClientRemoveGate(
			short connectPort);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSCClientGetStatistic(
			IntPtr statistic);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSCClientFreeStatistic(
			IntPtr statistic);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxCreate(IntPtr context);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUCtxFree(IntPtr context);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxSetParameter(IntPtr context,
			IntPtr parameterName, IntPtr parameterValue, DWORD parameterValueLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUCtxFreePrivateKey(IntPtr context);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxReadPrivateKey(IntPtr context,
			IntPtr keyMedia, IntPtr privateKeyContext, IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxReadPrivateKeyBinary(IntPtr context,
			IntPtr privateKey, DWORD privateKeyLength, IntPtr privateKeyPassword,
			IntPtr privateKeyContext, IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxReadPrivateKeyFile(IntPtr context,
			IntPtr privateKeyFile, IntPtr privateKeyPassword,
			IntPtr privateKeyContext, IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetRecipientsCount(
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			DWORD envelopedDataBinaryLength, IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetFileRecipientsCount(
			IntPtr envelopedFile, IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetRecipientInfo(
			DWORD recipientIndex, IntPtr envelopedDataString,
			IntPtr envelopedDataBinary, DWORD envelopedDataBinaryLength,
			IntPtr recipientInfoType, IntPtr recipientIssuer,
			IntPtr recipientSerial, IntPtr recipientKeyID);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetFileRecipientInfo(
			DWORD recipientIndex, IntPtr envelopedFile,
			IntPtr recipientInfoType, IntPtr recipientIssuer,
			IntPtr recipientSerial, IntPtr recipientKeyID);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUCtxFreeMemory(
			IntPtr context, IntPtr memory);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUCtxFreeCertOwnerInfo(
			IntPtr context, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUCtxFreeCertificateInfoEx(
			IntPtr context, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUCtxFreeSignInfo(
			IntPtr context, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUCtxFreeSenderInfo(
			IntPtr context, IntPtr signInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetOwnCertificate(
			IntPtr privateKeyContext, DWORD certKeyType,
			DWORD keyUsage, IntPtr info, IntPtr certificate,
			IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxEnumOwnCertificates(
			IntPtr privateKeyContext, DWORD index,
			IntPtr info, IntPtr certificate,
			IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxHashData(
			IntPtr context, DWORD hashAlgo, IntPtr certificate,
			DWORD certificateLength, IntPtr data, DWORD dataLength,
			IntPtr hashBinary, IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxHashFile(
			IntPtr context, DWORD hashAlgo,
			IntPtr certificate, DWORD certificateLength,
			IntPtr fileName,
			IntPtr hashBinary, IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxHashDataBegin(
			IntPtr context, DWORD hashAlgo,
			IntPtr certificate, DWORD certificateLength,
			IntPtr hashContext);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxHashDataContinue(
			IntPtr hashContext, IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxHashDataEnd(
			IntPtr hashContext,
			IntPtr hashBinary, IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxFreeHash(
			IntPtr hashContext);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxSignHash(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr hashContext, BOOL appendCert,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxSignHashValue(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr hashBinary, DWORD hashLength,
			BOOL appendCert, IntPtr signBinary,
			IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxSignData(
			IntPtr privateKeyContext,
			DWORD signAlgo, IntPtr data, DWORD dataLength,
			BOOL external, BOOL appendCert,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxSignFile(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr inputFile, BOOL external, BOOL appendCert,
			IntPtr outputFile);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxIsAlreadySigned(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr signBinary, DWORD signBinaryLength,
			IntPtr isAlreadySigned);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxIsFileAlreadySigned(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr fileNameWithSign, IntPtr isAlreadySigned);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxAppendSignHash(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr hashContext,
			IntPtr previousSignBinary,
			DWORD previousSignBinaryLength,
			BOOL appendCert, IntPtr signBinary,
			IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxAppendSignHashValue(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr hashBinary, DWORD hashLength,
			IntPtr previousSignBinary,
			DWORD previousSignBinaryLength,
			BOOL appendCert, IntPtr signBinary,
			IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxAppendSign(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr data, DWORD dataLength,
			IntPtr previousSignBinary,
			DWORD previousSignBinaryLength, BOOL appendCert,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxAppendSignFile(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr fileName, IntPtr fileNameWithPreviousSign,
			BOOL appendCert, IntPtr fileNameWithSign);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxCreateEmptySign(
			IntPtr context, DWORD signAlgo, IntPtr data,
			DWORD dataLength, IntPtr cert, DWORD certLength,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxCreateSigner(
			IntPtr privateKeyContext, DWORD signAlgo,
			IntPtr hashBinary, DWORD hashLength,
			IntPtr signerBinary, IntPtr signerBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxAppendSigner(
			IntPtr context, DWORD signAlgo,
			IntPtr signerBinary, DWORD signerBinaryLength,
			IntPtr certificate, DWORD certificateLength,
			IntPtr previousSignBinary,
			DWORD previousSignBinaryLength,
			IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetSignsCount(
			IntPtr context, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetFileSignsCount(
			IntPtr context, IntPtr fileNameWithSign, IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetSignerInfo(
			IntPtr context, DWORD signIndex, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr info,
			IntPtr certificate, IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetFileSignerInfo(
			IntPtr context, DWORD signIndex, IntPtr fileNameWithSign, IntPtr info,
			IntPtr certificate, IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxVerifyHash(
			IntPtr hashContext, DWORD signIndex,
			IntPtr signBinary, DWORD signBinaryLength, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxVerifyHashValue(
			IntPtr context, IntPtr hashBinary, DWORD hashBinaryLength,
			DWORD signIndex, IntPtr signBinary, DWORD signBinaryLength,
			IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxVerifyData(
			IntPtr context, IntPtr data, DWORD dataLength,
			DWORD signIndex, IntPtr signBinary, DWORD signBinaryLength,
			IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxVerifyDataInternal(
			IntPtr context, DWORD signIndex,
			IntPtr signBinary, DWORD signBinaryLength,
			IntPtr data, IntPtr dataLength, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxVerifyFile(
			IntPtr context, DWORD signIndex,
			IntPtr fileNameWithSign, IntPtr fileName, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxEnvelopData(
			IntPtr privateKeyContext, DWORD recipientCertsCount,
			IntPtr recipientCerts, IntPtr recipientCertsLength,
			DWORD recipientAppendType, BOOL signData,
			BOOL appendCert, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataBinary, IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxEnvelopFile(
			IntPtr privateKeyContext, DWORD recipientCertsCount,
			IntPtr recipientCerts, IntPtr recipientCertsLength,
			DWORD recipientAppendType, BOOL signData,
			BOOL appendCert, IntPtr fileName,
			IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetSenderInfo(IntPtr context,
			IntPtr envelopedData, DWORD envelopedDataLength,
			IntPtr recipientCert, DWORD recipientCertLength, IntPtr dynamicKey,
			IntPtr info, IntPtr certificate, IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetFileSenderInfo(IntPtr context,
			IntPtr envelopedFileName, IntPtr recipientCert,
			DWORD recipientCertLength, IntPtr dynamicKey,
			IntPtr info, IntPtr certificate, IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetRecipientsCount(IntPtr context,
			IntPtr envelopedData, DWORD envelopedDataLength, IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetFileRecipientsCount(
			IntPtr context, IntPtr envelopedFile, IntPtr count);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetRecipientInfo(
			IntPtr context, DWORD recipientIndex,
			IntPtr envelopedData, DWORD envelopedDataLength,
			IntPtr recipientInfoType, IntPtr recipientIssuer,
			IntPtr recipientSerial, IntPtr recipientKeyID);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetFileRecipientInfo(
			IntPtr context, DWORD recipientIndex, IntPtr envelopedFile,
			IntPtr recipientInfoType, IntPtr recipientIssuer,
			IntPtr recipientSerial, IntPtr recipientKeyID);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxDevelopData(
			IntPtr privateKeyContext, IntPtr envelopedDataString,
			IntPtr envelopedDataBinary, DWORD envelopedDataLength,
			IntPtr senderCert, DWORD senderCertLength,
			IntPtr data, IntPtr dataLength, IntPtr senderInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxDevelopFile(
			IntPtr privateKeyContext, IntPtr envelopedFileName,
			IntPtr senderCert, DWORD senderCertLength,
			IntPtr fileName, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxEnvelopAppendData(
			IntPtr privateKeyContext, IntPtr data,
			DWORD dataLength, IntPtr senderCert, DWORD senderCertSize,
			IntPtr previousEnvelopedDataBinary,
			DWORD previousEnvelopedDataBinaryLength,
			IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxEnvelopAppendFile(
			IntPtr privateKeyContext, IntPtr fileName,
			IntPtr senderCert, DWORD senderCertSize,
			IntPtr previousEnvelopedFileName,
			IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetDataFromSignedData(
			IntPtr context, IntPtr signedData, DWORD signedDataSize,
			IntPtr data, IntPtr dataSize);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetDataFromSignedFile(
			IntPtr context, IntPtr fileNameWithSignedData,
			IntPtr fileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUSetSettingsRegPath(
			DWORD rootKey, IntPtr regPath);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxIsDataInSignedDataAvailable(
			IntPtr context, IntPtr signedData, DWORD signedDataSize,
			IntPtr available);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxIsDataInSignedFileAvailable(
			IntPtr context, IntPtr fileNameWithSignedData,
			IntPtr available);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetCertificatesFromLDAPByEDRPOUCode(
			IntPtr edrpou, DWORD certKeyType, DWORD keyUsage,
			IntPtr ldapServers, IntPtr ldapServersPorts,
			IntPtr certificates, IntPtr certificatesLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUProtectDataByPassword(
			IntPtr data, DWORD dataLength, IntPtr password,
			IntPtr protectedDataString,
			IntPtr protectedDataBinary,
			IntPtr protectedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUUnprotectDataByPassword(
			IntPtr protectedDataString,
			IntPtr protectedDataBinary, DWORD protectedDataBinaryLength,
			IntPtr password,
			IntPtr data, IntPtr dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUFreeTimeInfo(
			IntPtr timeInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetSignTimeInfo(
			DWORD signIndex, IntPtr signedDataString,
			IntPtr signedDataBinary, DWORD signedDataBinaryLength,
			IntPtr timeInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetFileSignTimeInfo(
			DWORD signIndex, IntPtr fileNameWithSignedData,
			IntPtr timeInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyHashOnTimeEx(
			IntPtr hashString, IntPtr hashBinary, DWORD hashBinaryLength,
			DWORD signIndex,
			IntPtr signString, IntPtr signBinary, DWORD signBinaryLength,
			IntPtr onTime, BOOL offline, BOOL noCRL, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataOnTimeEx(
			IntPtr data, DWORD dataLength, DWORD signIndex,
			IntPtr signString, IntPtr signBinary, DWORD signBinaryLength,
			IntPtr onTime, BOOL offline, BOOL noCRL, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataInternalOnTimeEx(
			DWORD signIndex,
			IntPtr signString, IntPtr signBinary, DWORD signBinaryLength,
			IntPtr onTime, BOOL offline, BOOL noCRL,
			IntPtr data, IntPtr dataLength, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataOnTimeBeginEx(
			DWORD signIndex,
			IntPtr signString, IntPtr signBinary, DWORD signBinaryLength,
			IntPtr onTime, BOOL offline, BOOL noCRL);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyFileOnTimeEx(
			DWORD signIndex, IntPtr fileNameWithSign, IntPtr fileName,
			IntPtr onTime, BOOL offline, BOOL noCRL, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxEnumPrivateKeyInfo(
			IntPtr privateKeyContext, DWORD index,
			IntPtr keyType, IntPtr keyUsage, IntPtr keyIDsCount, IntPtr keyIDs);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxExportPrivateKeyContainer(
			IntPtr privateKeyContext, IntPtr password, IntPtr keyID,
			IntPtr privateKey, IntPtr privateKeyLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxExportPrivateKeyPFXContainer(
			IntPtr privateKeyContext, IntPtr password, BOOL exportCerts,
			DWORD keyIDsCount, IntPtr trustedKeyIDs, IntPtr keyIDs,
			IntPtr pfxContainer, IntPtr pfxContainerLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxExportPrivateKeyContainerFile(
			IntPtr privateKeyContext, IntPtr password, IntPtr keyID,
			IntPtr fileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxExportPrivateKeyPFXContainerFile(
			IntPtr privateKeyContext, IntPtr password, BOOL exportCerts,
			DWORD keyIDsCount, IntPtr trustedKeyIDs, IntPtr keyIDs,
			IntPtr fileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetCertificateFromPrivateKey(
			IntPtr privateKeyContext, IntPtr keyID,
			IntPtr infoEx, IntPtr certificate, IntPtr certificateLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawEnvelopData(
			IntPtr recipientCert, DWORD recipientCertLength,
			IntPtr data, DWORD dataLength, IntPtr envDataString,
			IntPtr envDataBinary, IntPtr envDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURawDevelopData(
			IntPtr envDataString,
			IntPtr envDataBinary, DWORD envDataBinaryLength,
			IntPtr data, IntPtr dataLength, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataRSAEx(
			DWORD contentEncAlgoType,
			IntPtr recipientCertIssuers, IntPtr recipientCertSerials,
			BOOL signData, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataRSA(
			DWORD contentEncAlgoType,
			IntPtr recipientCertIssuer, IntPtr recipientCertSerial,
			BOOL signData, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFileRSAEx(
			DWORD contentEncAlgoType,
			IntPtr recipientCertIssuers, IntPtr recipientCertSerials,
			BOOL signData, IntPtr fileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFileRSA(
			DWORD contentEncAlgoType,
			IntPtr recipientCertIssuer, IntPtr recipientCertSerial,
			BOOL signData, IntPtr fileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetReceiversCertificatesRSA(
			IntPtr certificates);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataToRecipientsRSA(
			DWORD contentEncAlgoType, DWORD recipientsCerts,
			IntPtr recipientCerts, IntPtr recipientCertsLength,
			BOOL signData, IntPtr data, DWORD dataLength,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopFileToRecipientsRSA(
			DWORD contentEncAlgoType, DWORD recipientsCerts,
			IntPtr recipientCerts, IntPtr recipientCertsLength,
			BOOL signData, IntPtr fileName, IntPtr envelopedFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURemoveSign(
			DWORD signIndex, IntPtr previousSignString,
			IntPtr previousSignBinary, DWORD previousSignBinaryLength,
			IntPtr signString, IntPtr signBinary, IntPtr signBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EURemoveSignFile(
			DWORD signIndex, IntPtr previousSignFile, IntPtr signFile);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGeneratePRNGSequence(
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUChangeOwnCertificatesStatus(
			DWORD requestType, DWORD revocationReason);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxChangeOwnCertificatesStatus(
			IntPtr privateKeyContext,
			DWORD requestType, DWORD revocationReason);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUAppendTransportHeader(
			IntPtr caType, IntPtr fileName, IntPtr clientEMail,
			IntPtr clientCert, DWORD clientCertLength,
			IntPtr cryptoData, DWORD cryptoDataLength,
			IntPtr transportData, IntPtr transportDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUParseTransportHeader(
			IntPtr transportData, DWORD transportDataLength,
			IntPtr receiptNumber, 
			IntPtr cryptoData, IntPtr cryptoDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUAppendCryptoHeader(
			IntPtr caType, DWORD headerType, 
			IntPtr cryptoData, DWORD cryptoDataLength,
			IntPtr transportData, IntPtr transportDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUParseCryptoHeader(
			IntPtr transportData, DWORD transportDataLength,
			IntPtr caType, IntPtr headerType, IntPtr headerSize,
			IntPtr cryptoData, IntPtr cryptoDataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxEnumVirtual(IntPtr deviceContext,
			IntPtr typeDescription);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxEnum(IntPtr deviceContext,
			IntPtr deviceDescription);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUDevCtxClose(IntPtr deviceContext);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxOpenIDCard(
			IntPtr typeDescription, IntPtr deviceDescription,
			IntPtr password, DWORD passwordVersion,
			IntPtr deviceContext);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxChangeIDCardPasswords(
			IntPtr deviceContext,
			IntPtr pin, IntPtr puk);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxAuthenticateIDCard(
			IntPtr deviceContext,
			IntPtr parameter1, IntPtr parameter2);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxVerifyIDCardData(
			IntPtr deviceContext, byte tag);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxUpdateIDCardData(
			IntPtr deviceContext, IntPtr privateKeyContext, byte tag,
			IntPtr data, DWORD dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxEnumIDCardData(
			IntPtr deviceContext, byte tag, DWORD index,
			IntPtr data, IntPtr dataLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataWithSettings(
			IntPtr recipientCertIssuer, IntPtr recipientCertSerial, 
			BOOL signData, IntPtr data, DWORD dataLength,
			BOOL checkRecipientCertOffline, BOOL checkRecipientCertNoCRL, 
			BOOL noTSP, BOOL appendCert,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnvelopDataToRecipientsWithSettings(
			DWORD recipientsCerts, IntPtr recipientCerts, 
			IntPtr recipientCertsLength,
			BOOL signData, IntPtr data, DWORD dataLength,
			BOOL checkRecipientCertsOffline, 
			BOOL checkRecipientCertsNoCRL, BOOL noTSP, BOOL appendCert,
			IntPtr envelopedDataString, IntPtr envelopedDataBinary,
			IntPtr envelopedDataBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern BOOL EUShowSecureConfirmDialog(
			IntPtr caption, IntPtr label, IntPtr footer);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxIsNamedPrivateKeyExists(
			IntPtr context, IntPtr keyMedia, 
			IntPtr namedPrivateKeyLabel, IntPtr namedPrivateKeyPassword, 
			IntPtr exists);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGenerateNamedPrivateKey(
			IntPtr context, IntPtr keyMedia,
			IntPtr namedPrivateKeyLabel, IntPtr namedPrivateKeyPassword,
			DWORD uaKeysType, DWORD uaDSKeysSpec, DWORD uaKEPKeysSpec, 
			IntPtr uaParamsPath, DWORD internationalKeysType,
			DWORD internationalKeysSpec, IntPtr internationalParamsPath,
			IntPtr uaRequest, IntPtr uaRequestLength, IntPtr uaReqFileName,
			IntPtr uaKEPRequest, IntPtr uaKEPRequestLength, IntPtr uaKEPReqFileName,
			IntPtr internationalRequest, IntPtr internationalRequestLength,
			IntPtr internationalReqFileName);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxReadNamedPrivateKey(
			IntPtr context, IntPtr keyMedia,
			IntPtr namedPrivateKeyLabel, IntPtr namedPrivateKeyPassword,
			IntPtr privateKeyContext, IntPtr certInfo);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxDestroyNamedPrivateKey(
			IntPtr context, IntPtr keyMedia,
			IntPtr namedPrivateKeyLabel, IntPtr namedPrivateKeyPassword);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxChangeNamedPrivateKeyPassword(
			IntPtr context, IntPtr keyMedia,
			IntPtr namedPrivateKeyLabel, IntPtr namedPrivateKeyPassword, 
			IntPtr namedPrivateKeyNewPassword);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDeleteCertificate(
			IntPtr issuer, IntPtr serial);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxEnumIDCardDataChangeDate(
			IntPtr deviceContext, byte tag, DWORD index,
			IntPtr onTime);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetDataHashFromSignedData(
			DWORD signIndex, IntPtr signString, IntPtr signBinary,
			DWORD signBinaryLength, IntPtr hashString, IntPtr hashBinary,
			IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetDataHashFromSignedFile(
			DWORD signIndex, IntPtr fileNameWithSignedData,
			IntPtr hashString, IntPtr hashBinary, IntPtr hashBinaryLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxVerifyIDCardSecurityObjectDocument(
			IntPtr deviceContext,
			IntPtr certificatesStorePath);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnumJKSPrivateKeys(
			IntPtr container, DWORD containerLength, DWORD index,
			IntPtr keyAlias);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUEnumJKSPrivateKeysFile(
			IntPtr fileName, DWORD index, IntPtr keyAlias);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeCertificatesArray(
			DWORD certificatesCount, IntPtr certificates, 
			IntPtr certificatesLengthes);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetJKSPrivateKey(
			IntPtr container, DWORD containerLength, IntPtr keyAlias,
			IntPtr privateKey, IntPtr privateKeyLength, 
			IntPtr certificatesCount, IntPtr certificates, 
			IntPtr certificatesLengthes);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetJKSPrivateKeyFile(
			IntPtr fileName, IntPtr keyAlias,
			IntPtr privateKey, IntPtr privateKeyLength,
			IntPtr certificatesCount, IntPtr certificates,
			IntPtr certificatesLengthes);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataWithParams(
			IntPtr data, DWORD dataLength, DWORD signIndex,
			IntPtr signString, IntPtr signBinary, DWORD signBinaryLength,
			IntPtr onTime, BOOL offline, BOOL noCRL,
			IntPtr signerCert, DWORD signerCertLength, BOOL noSignerCertCheck,
			IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUVerifyDataInternalWithParams(
			DWORD signIndex,
			IntPtr signString, IntPtr signBinary, DWORD signBinaryLength,
			IntPtr onTime, BOOL offline, BOOL noCRL,
			IntPtr signerCert, DWORD signerCertLength, BOOL noSignerCertCheck,
			IntPtr data, IntPtr dataLength, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxGetNamedPrivateKeyInfo(
			IntPtr context, IntPtr keyMedia,
			IntPtr namedPrivateKeyLabel, IntPtr namedPrivateKeyPassword,
			IntPtr privateKeyInfo, IntPtr privateKeyInfoLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUMakeNewCertificate(
			IntPtr oldKeyMedia, IntPtr oldPrivateKey, 
			DWORD oldPrivateKeyLength, IntPtr oldPrivateKeyPassword,
			DWORD UAKeysType, DWORD UADSKeysSpec, BOOL useUADSKeyAsKEP, 
			DWORD UAKEPKeysSpec, IntPtr UAParamsPath,
			DWORD internationalKeysType, DWORD internationalKeysSpec, 
			IntPtr internationalParamsPath, IntPtr newKeyMedia,
			IntPtr newPrivateKeyPassword, IntPtr newPrivateKey, 
			IntPtr newPrivateKeyLength);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern void EUFreeKeyMediaDeviceInfo(
			IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUGetKeyMediaDeviceInfo(
			IntPtr keyMedia, IntPtr info);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUCtxEnumNamedPrivateKeys(
			IntPtr context, IntPtr keyMedia, DWORD index,
			IntPtr namedPrivateKeyLabel);

		[DllImport(EU_LIBRARY_NAME)]
		private static extern DWORD EUDevCtxInternalAuthenticateIDCard(
			IntPtr deviceContext,
			DWORD CVCertsCount, IntPtr CVCerts,
			IntPtr CVCertsLength,
			IntPtr privateKey, DWORD privateKeyLength, IntPtr password);

		#endregion

		#region EUSignCP: Private functions

		#region EUSignCP: General functions

		private static void RaiseError(int errorCode)
		{
			if (!_throwExceptions)
				return;

			if (errorCode == EU_ERROR_NONE)
				return;

			string message = "";

			if (errorCode == EU_ERROR_LIBRARY_LOAD)
			{
				if (_lang == EU_LANG.DEFAULT || _lang == EU_LANG.UA)
				{
					message =
						"Виникла помилка при завантаженні " +
						"криптографічної бібліотеки";
				}
				else if (_lang == EU_LANG.RU)
				{
					message =
						"Возникла ошибка при загрузке " +
						"криптографической библиотеки";
				}
				else if (_lang == EU_LANG.EN)
				{
					message =
						"An error has occurred while " +
						"loading the cryptographic library";
				}
			}
			else
			{
				message = _GetErrorLangDesc(errorCode, _lang);
			}

			throw new EUSignCPException(errorCode, message);
		}

/*__BEGIN_MONO_CODE__
#if __ANDROID__
		private static int _SetUSBDevice(int fd, byte[] descriptors)
		{
			EUMarshal descriptorsPtr = new EUMarshal(descriptors);

			try
			{
				SetUSBDevice(fd,
					descriptorsPtr.DataPtr, (int) descriptorsPtr.DataLength);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _PreLoadLibraries(string[] libraries)
		{
			try
			{
				foreach (string library in libraries)
					Java.Lang.Runtime.GetRuntime().LoadLibrary(library);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}
#endif // __ANDROID__
__END_MONO_CODE__*/

		private static int _Initialize()
		{
			try
			{
				return (int) EUInitialize();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
		}

		private static int _Finalize()
		{
			try
			{
				EUFinalize();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsInitialized(out bool isInitialize)
		{
			isInitialize = false;

			try
			{
				isInitialize = (EUIsInitialized() != 0);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ResetOperation()
		{
			try
			{
				EUResetOperation();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ResetOperationCtx(IntPtr context)
		{
			try
			{
				EUResetOperationCtx(context);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static string _GetErrorDesc(int error)
		{
			try
			{
				IntPtr intError;

				intError = EUGetErrorDesc((DWORD) error);
				if (intError == IntPtr.Zero)
					return "";

				return PtrToStringAnsi(intError);
			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}

		private static string _GetErrorLangDesc(int error,
			EU_LANG lang = EU_LANG.DEFAULT)
		{
			try
			{
				IntPtr intError;

				intError = EUGetErrorLangDesc((DWORD)error, (DWORD)lang);
				if (intError == IntPtr.Zero)
					return "";

				return PtrToStringAnsi(intError);
			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}

		private static int _BASE64Encode(byte[] data,
			out string dataString)
		{
			EUMarshal dataPtr = null;
			EUMarshal dataStringPtr = null;

			dataString = null;

			try
			{
				int error;
				dataPtr = new EUMarshal(data);
				dataStringPtr = new EUMarshal(false);

				error = (int) EUBASE64Encode(dataPtr.DataPtr,
					dataPtr.DataLength, dataStringPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				dataString = dataStringPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (dataStringPtr != null)
					dataStringPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _BASE64Decode(string dataString,
			out byte[] data)
		{
			EUMarshal dataStringPtr = null;
			EUMarshal dataPtr = null;

			data = null;

			try
			{
				int error;
				dataStringPtr = new EUMarshal(dataString);
				dataPtr = new EUMarshal(true);

				error = (int) EUBASE64Decode(dataStringPtr.DataPtr,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataStringPtr != null)
					dataStringPtr.FreeData();
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CompareArrays(byte[] arr1,
			byte[] arr2, out bool isEqual)
		{
			isEqual = false;

			try
			{
				isEqual = CompareMemory(arr1, arr2);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DownloadFileViaHTTP(string url,
			string fileName, out byte[] fileData)
		{
			EUMarshal fileDataPtr = null;

			fileData = null;

			try
			{
				int error;
				EUMarshal urlPtr = new EUMarshal(url);
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				fileDataPtr = new EUMarshal(true);

				error = (int) EUDownloadFileViaHTTP(urlPtr.DataPtr,
					fileNamePtr.DataPtr, fileDataPtr.DataPtr,
					fileDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				fileData = fileDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (fileDataPtr != null)
					fileDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GeneratePRNGSequence(
			ref byte[] data)
		{
			EUMarshal dataPtr = null;
			try
			{
				int error;
				dataPtr = new EUMarshal(data);

				error = (int) EUGeneratePRNGSequence(
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;

				Marshal.Copy(dataPtr.DataPtr, data, 0, data.Length);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static bool _ShowSecureConfirmDialog(
			string caption, string label, string footer)
		{
			try
			{
				EUMarshal captionPtr = new EUMarshal();
				EUMarshal labelPtr = new EUMarshal();
				EUMarshal footerPtr = new EUMarshal();

				if (caption != null)
					captionPtr = new EUMarshal(caption);
				if (label != null)
					labelPtr = new EUMarshal(label);
				if (footer != null)
					footerPtr = new EUMarshal(footer);

				return EUShowSecureConfirmDialog(
					captionPtr.DataPtr, labelPtr.DataPtr,
					footerPtr.DataPtr) != 0;
			}
			catch (Exception)
			{
				return false;
			}
		}

		#endregion

		#region EUSignCP: Get/Set library settings functions

		private static int _DoesNeedSetSettings(
			out bool setSettings)
		{
			setSettings = true;

			try
			{
				setSettings = (EUDoesNeedSetSettings() > 0);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetSettings()
		{
			try
			{
				EUSetSettings();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetSettingsFilePath(string path)
		{
			try
			{
				int error;
				EUMarshal pathPtr = new EUMarshal(path);

				error = (int) EUSetSettingsFilePath(
					pathPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetSettingsFilePathEx(string path, 
			int rootKey, string regPath)
		{
			try
			{
				int error;
				EUMarshal pathPtr = new EUMarshal(path);
				EUMarshal regPathPtr = new EUMarshal();
				if (regPath != null)
					regPathPtr = new EUMarshal(regPath);

				error = (int) EUSetSettingsFilePathEx(
					pathPtr.DataPtr, (DWORD) rootKey, 
					regPathPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetSettingsRegPath(
			int rootKey, string regPath)
		{
			try
			{
				int error;
				EUMarshal pathPtr = new EUMarshal();
				if (regPath != null)
					pathPtr = new EUMarshal(regPath);

				error = (int) EUSetSettingsRegPath(
					(DWORD) rootKey, pathPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetUIMode(bool uiMode)
		{
			try
			{
				EUSetUIMode(uiMode ? 1 : 0);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetModeSettings(out bool offlineMode)
		{
			offlineMode = false;

			try
			{
				int error;
				EUMarshal offlineModePtr = new EUMarshal(EUMarshal.INT_SIZE);

				error = (int) EUGetModeSettings(offlineModePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				offlineMode = offlineModePtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetModeSettings(bool offlineMode)
		{
			try
			{
				int error = (int) EUSetModeSettings(offlineMode ? 1 : 0);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetFileStoreSettings(out string path,
			out bool checkCRLs, out bool autoRefresh, out bool ownCRLsOnly,
			out bool fullAndDeltaCRLs, out bool autoDownloadCRLs,
			out bool saveLoadedCerts, out int expireTime)
		{
			path = null;
			checkCRLs = autoRefresh = ownCRLsOnly = fullAndDeltaCRLs =
				autoDownloadCRLs = saveLoadedCerts = false;
			expireTime = 0;

			try
			{
				int error;
				EUMarshal pathPtr = new EUMarshal(EU_PATH_MAX_LENGTH + 1);
				EUMarshal checkCRLsPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal autoRefreshPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal ownCRLsOnlyPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal fullAndDeltaCRLsPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal autoDownloadCRLsPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal saveLoadedCertsPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal expireTimePtr = new EUMarshal(EUMarshal.INT_SIZE);

				error = (int) EUGetFileStoreSettings(pathPtr.DataPtr,
					checkCRLsPtr.DataPtr, autoRefreshPtr.DataPtr,
					ownCRLsOnlyPtr.DataPtr, fullAndDeltaCRLsPtr.DataPtr,
					autoDownloadCRLsPtr.DataPtr, saveLoadedCertsPtr.DataPtr,
					expireTimePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				path = pathPtr.GetStringData();
				checkCRLs = checkCRLsPtr.GetBoolData();
				autoRefresh = autoRefreshPtr.GetBoolData();
				ownCRLsOnly = ownCRLsOnlyPtr.GetBoolData();
				fullAndDeltaCRLs = fullAndDeltaCRLsPtr.GetBoolData();
				autoDownloadCRLs = autoDownloadCRLsPtr.GetBoolData();
				saveLoadedCerts = saveLoadedCertsPtr.GetBoolData();
				expireTime = expireTimePtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetFileStoreSettings(string path,
			bool checkCRLs, bool autoRefresh, bool ownCRLsOnly,
			bool fullAndDeltaCRLs, bool autoDownloadCRLs,
			bool saveLoadedCerts, int expireTime)
		{
			if (path.Length > EU_PATH_MAX_LENGTH)
				return EU_ERROR_BAD_PARAMETER;

			try
			{
				int error;
				EUMarshal pathPtr = new EUMarshal(path);

				error = (int) EUSetFileStoreSettings(pathPtr.DataPtr,
					checkCRLs ? 1 : 0, autoRefresh ? 1 : 0,
					ownCRLsOnly ? 1 : 0, fullAndDeltaCRLs ? 1 : 0,
					autoDownloadCRLs ? 1 : 0, saveLoadedCerts ? 1 : 0,
					(DWORD) expireTime);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetProxySettings(out bool useProxy,
			out bool anonymous, out string address,
			out string port, out string user, out string password,
			out bool savePassword)
		{
			useProxy = anonymous = savePassword = false;
			address = port = user = password = "";

			try
			{
				int error;
				EUMarshal useProxyPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal anonymousPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal addressPtr = new EUMarshal(EU_ADDRESS_MAX_LENGTH + 1);
				EUMarshal portPtr = new EUMarshal(EU_PORT_MAX_LENGTH + 1);
				EUMarshal userPtr = new EUMarshal(EU_USER_NAME_MAX_LENGTH + 1);
				EUMarshal passwordPtr = new EUMarshal(EU_PASS_MAX_LENGTH + 1);
				EUMarshal savePasswordPtr = new EUMarshal(EUMarshal.INT_SIZE);

				error = (int) EUGetProxySettings(useProxyPtr.DataPtr,
					anonymousPtr.DataPtr, addressPtr.DataPtr,
					portPtr.DataPtr, userPtr.DataPtr,
					passwordPtr.DataPtr, savePasswordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				useProxy = useProxyPtr.GetBoolData();
				anonymous = anonymousPtr.GetBoolData();
				address = addressPtr.GetStringData();
				port = portPtr.GetStringData();
				user = userPtr.GetStringData();
				password = passwordPtr.GetStringData();
				savePassword = savePasswordPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetProxySettings(bool useProxy,
			bool anonymous, string address, string port,
			string user, string password, bool savePassword)
		{
			if (address.Length > EU_ADDRESS_MAX_LENGTH ||
				port.Length > EU_PORT_MAX_LENGTH ||
				user.Length > EU_USER_NAME_MAX_LENGTH ||
				password.Length > EU_PASS_MAX_LENGTH)
			{
				return EU_ERROR_BAD_PARAMETER;
			}

			try
			{
				int error;
				EUMarshal addressPtr = new EUMarshal(address);
				EUMarshal portPtr = new EUMarshal(port);
				EUMarshal userPtr = new EUMarshal(user);
				EUMarshal passwordPtr = new EUMarshal(password);

				error = (int) EUSetProxySettings(
					useProxy ? 1 : 0, anonymous ? 1 : 0,
					addressPtr.DataPtr, portPtr.DataPtr,
					userPtr.DataPtr, passwordPtr.DataPtr,
					savePassword ? 1 : 0);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetOCSPSettings(out bool useOCSP,
			out bool beforeStore, out string address, out string port)
		{
			useOCSP = beforeStore = false;
			address = port = "";

			try
			{
				int error;
				EUMarshal useOCSPPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal beforeStorePtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal addressPtr = new EUMarshal(EU_ADDRESS_MAX_LENGTH + 1);
				EUMarshal portPtr = new EUMarshal(EU_PORT_MAX_LENGTH + 1);

				error = (int) EUGetOCSPSettings(useOCSPPtr.DataPtr,
					beforeStorePtr.DataPtr, addressPtr.DataPtr,
					portPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				useOCSP = useOCSPPtr.GetBoolData();
				beforeStore = beforeStorePtr.GetBoolData();
				address = addressPtr.GetStringData();
				port = portPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetOCSPSettings(bool useOCSP,
			bool beforeStore, string address, string port)
		{
			if (address.Length > EU_ADDRESS_MAX_LENGTH ||
				port.Length > EU_PORT_MAX_LENGTH)
			{
				return EU_ERROR_BAD_PARAMETER;
			}

			try
			{
				int error;
				EUMarshal addressPtr = new EUMarshal(address);
				EUMarshal portPtr = new EUMarshal(port);

				error = (int) EUSetOCSPSettings(useOCSP ? 1 : 0,
					beforeStore ? 1 : 0, addressPtr.DataPtr,
					portPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetTSPSettings(
			out bool getStamps, out string address,
			out string port)
		{
			getStamps = false;
			address = port = "";

			try
			{
				int error;
				EUMarshal getStampsPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal addressPtr = new EUMarshal(EU_ADDRESS_MAX_LENGTH + 1);
				EUMarshal portPtr = new EUMarshal(EU_PORT_MAX_LENGTH + 1);

				error = (int) EUGetTSPSettings(getStampsPtr.DataPtr,
					addressPtr.DataPtr, portPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				getStamps = getStampsPtr.GetBoolData();
				address = addressPtr.GetStringData();
				port = portPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetTSPSettings(bool getStamps,
			string address, string port)
		{
			if (address.Length > EU_ADDRESS_MAX_LENGTH ||
				port.Length > EU_PORT_MAX_LENGTH)
			{
				return EU_ERROR_BAD_PARAMETER;
			}

			try
			{
				int error;
				EUMarshal addressPtr = new EUMarshal(address);
				EUMarshal portPtr = new EUMarshal(port);

				error = (int) EUSetTSPSettings(getStamps ? 1 : 0,
					addressPtr.DataPtr, portPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetLDAPSettings(out bool useLDAP,
			out string address, out string port, out bool anonymous,
			out string user, out string password)
		{
			useLDAP = anonymous = false;
			address = port = user = password = "";

			try
			{
				int error;
				EUMarshal useLDAPPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal addressPtr = new EUMarshal(EU_ADDRESS_MAX_LENGTH + 1);
				EUMarshal portPtr = new EUMarshal(EU_PORT_MAX_LENGTH + 1);
				EUMarshal anonymousPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal userPtr = new EUMarshal(EU_USER_NAME_MAX_LENGTH + 1);
				EUMarshal passwordPtr = new EUMarshal(EU_PASS_MAX_LENGTH + 1);

				error = (int) EUGetLDAPSettings(useLDAPPtr.DataPtr,
					addressPtr.DataPtr, portPtr.DataPtr,
					anonymousPtr.DataPtr, userPtr.DataPtr,
					passwordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				useLDAP = useLDAPPtr.GetBoolData();
				address = addressPtr.GetStringData();
				port = portPtr.GetStringData();
				anonymous = anonymousPtr.GetBoolData();
				user = userPtr.GetStringData();
				password = passwordPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetLDAPSettings(bool useLDAP,
			string address, string port, bool anonymous,
			string user, string password)
		{
			if (address.Length > EU_ADDRESS_MAX_LENGTH ||
				port.Length > EU_PORT_MAX_LENGTH ||
				user.Length > EU_USER_NAME_MAX_LENGTH ||
				password.Length > EU_PASS_MAX_LENGTH)
			{
				return EU_ERROR_BAD_PARAMETER;
			}

			try
			{
				int error;
				EUMarshal addressPtr = new EUMarshal(address);
				EUMarshal portPtr = new EUMarshal(port);
				EUMarshal userPtr = new EUMarshal(user);
				EUMarshal passwordPtr = new EUMarshal(password);

				error = (int) EUSetLDAPSettings(useLDAP ? 1 : 0,
					addressPtr.DataPtr, portPtr.DataPtr,
					anonymous ? 1 : 0, userPtr.DataPtr,
					passwordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCMPSettings(
			out bool useCMP, out string address,
			out string port, out string commonName)
		{
			useCMP = false;
			address = port = commonName = "";

			try
			{
				int error;
				EUMarshal useCMPPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal addressPtr = new EUMarshal(EU_ADDRESS_MAX_LENGTH + 1);
				EUMarshal portPtr = new EUMarshal(EU_PORT_MAX_LENGTH + 1);
				EUMarshal commonNamePtr = new EUMarshal(
					EU_COMMON_NAME_MAX_LENGTH + 1);

				error = (int) EUGetCMPSettings(useCMPPtr.DataPtr,
					addressPtr.DataPtr, portPtr.DataPtr,
					commonNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				useCMP = useCMPPtr.GetBoolData();
				address = addressPtr.GetStringData();
				port = portPtr.GetStringData();
				commonName = commonNamePtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetCMPSettings(bool useCMP,
			string address, string port, string commonName)
		{
			if (address.Length > EU_ADDRESS_MAX_LENGTH ||
				port.Length > EU_PORT_MAX_LENGTH ||
				commonName.Length > EU_COMMON_NAME_MAX_LENGTH)
			{
				return EU_ERROR_BAD_PARAMETER;
			}

			try
			{
				int error;
				EUMarshal addressPtr = new EUMarshal(address);
				EUMarshal portPtr = new EUMarshal(port);
				EUMarshal commonNamePtr = new EUMarshal(commonName);

				error = (int) EUSetCMPSettings(useCMP ? 1 : 0,
					addressPtr.DataPtr, portPtr.DataPtr,
					commonNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SelectCMPServer(
			out string commonName, out string dns)
		{
			commonName = dns = "";

			try
			{
				int error;
				EUMarshal commonNamePtr = new EUMarshal(
					EU_COMMON_NAME_MAX_LENGTH + 1);
				EUMarshal dnsPtr = new EUMarshal(EU_ADDRESS_MAX_LENGTH + 1);

				error = (int) EUSelectCMPServer(commonNamePtr.DataPtr,
					dnsPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				commonName = commonNamePtr.GetStringData();
				dns = dnsPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetPrivateKeyMediaSettings(
			out EU_KEY_MEDIA_SOURCE_TYPE sourceType,
			out bool showErrors, out int typeIndex,
			out int devIndex, out string password)
		{
			sourceType = EU_KEY_MEDIA_SOURCE_TYPE.OPERATOR;
			showErrors = true;
			typeIndex = devIndex = 0;
			password = "";

			try
			{
				int error;
				EUMarshal sourceTypePtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal showErrorsPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal typeIndexPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal devIndexPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal passwordPtr = new EUMarshal(EU_PASS_MAX_LENGTH + 1);

				error = (int) EUGetPrivateKeyMediaSettings(sourceTypePtr.DataPtr,
					showErrorsPtr.DataPtr, typeIndexPtr.DataPtr,
					devIndexPtr.DataPtr, passwordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sourceType = (EU_KEY_MEDIA_SOURCE_TYPE)
					sourceTypePtr.GetIntData();
				showErrors = showErrorsPtr.GetBoolData();
				typeIndex = typeIndexPtr.GetIntData();
				devIndex = devIndexPtr.GetIntData();
				password = passwordPtr.GetStringData() ;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetPrivateKeyMediaSettings(
			EU_KEY_MEDIA_SOURCE_TYPE sourceType, bool showErrors,
			int typeIndex, int devIndex, string password)
		{
			if (password.Length > EU_PASS_MAX_LENGTH || 
					typeIndex < 0 || devIndex < 0)
			{
				return EU_ERROR_BAD_PARAMETER;
			}

			try
			{
				int error;
				EUMarshal passwordPtr = new EUMarshal(password);

				error = (int) EUSetPrivateKeyMediaSettings(
					(DWORD) sourceType,
					showErrors ? 1 : 0,
					(DWORD) typeIndex, (DWORD) devIndex,
					passwordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetRuntimeParameter(
			string parameterName, object parameterValue)
		{
			try
			{
				int error;
				EUMarshal parameterNamePtr = new EUMarshal(parameterName);
				EUMarshal parameterPtr = new EUMarshal();

				if (parameterValue is bool)
				{
					bool blValue = (bool)parameterValue;
					parameterPtr = new EUMarshal(EUMarshal.INT_SIZE);
					Marshal.WriteInt32(parameterPtr.DataPtr,
						blValue ? 1 : 0);
				}
				else if (parameterValue is int)
				{
					int value = (int)parameterValue;
					parameterPtr = new EUMarshal(EUMarshal.INT_SIZE);
					Marshal.WriteInt32(parameterPtr.DataPtr, value);
				}
				else if (parameterValue is string)
				{
					parameterPtr = new EUMarshal((string)parameterValue);
				}

				error = (int) EUSetRuntimeParameter(parameterNamePtr.DataPtr,
					parameterPtr.DataPtr, parameterPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetStorageParameter(
			bool isProtected, string name, out string value)
		{
			value = "";

			try
			{
				int error;
				EUMarshal namePtr = new EUMarshal(name);
				EUMarshal valuePtr = new EUMarshal(
					EU_STORAGE_VALUE_MAX_LENGTH);

				error = (int) EUGetStorageParameter(
					isProtected ? 1 : 0, namePtr.DataPtr,
					valuePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				value = valuePtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetStorageParameter(
			bool isProtected, string name, string value)
		{
			if (value.Length >= EU_STORAGE_VALUE_MAX_LENGTH)
				return EU_ERROR_BAD_PARAMETER;

			try
			{
				int error;
				EUMarshal namePtr = new EUMarshal(name);
				EUMarshal valuePtr = new EUMarshal(value);

				error = (int) EUSetStorageParameter(
					isProtected ? 1 : 0, namePtr.DataPtr,
					valuePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetOCSPAccessInfoModeSettings(
			out bool enabled)
		{
			enabled = false;

			try
			{
				int error;
				EUMarshal enabledPtr = new EUMarshal(EUMarshal.INT_SIZE);

				error = (int) EUGetOCSPAccessInfoModeSettings(enabledPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				enabled = enabledPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetOCSPAccessInfoModeSettings(
			bool enabled)
		{
			try
			{
				int error;

				error = (int) EUSetOCSPAccessInfoModeSettings(enabled ? 1 : 0);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnumOCSPAccessInfoSettings(
			int index, out string issuerCN, out string address,
			out string port)
		{
			address = port = issuerCN = "";

			try
			{
				int error;
				EUMarshal issuerCNPtr =
					new EUMarshal(EU_COMMON_NAME_MAX_LENGTH + 1);
				EUMarshal addressPtr = new EUMarshal(EU_ADDRESS_MAX_LENGTH + 1);
				EUMarshal portPtr = new EUMarshal(EU_PORT_MAX_LENGTH + 1);

				error = (int)EUEnumOCSPAccessInfoSettings((DWORD) index,
					issuerCNPtr.DataPtr, addressPtr.DataPtr,
					portPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				issuerCN = issuerCNPtr.GetStringData();
				address = addressPtr.GetStringData();
				port = portPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetOCSPAccessInfoSettings(
			string issuerCN, out string address, out string port)
		{
			address = port = "";

			if (issuerCN.Length > EU_COMMON_NAME_MAX_LENGTH)
				return EU_ERROR_BAD_PARAMETER;

			try
			{
				int error;
				EUMarshal issuerCNPtr = new EUMarshal(issuerCN);
				EUMarshal addressPtr = new EUMarshal(EU_ADDRESS_MAX_LENGTH + 1);
				EUMarshal portPtr = new EUMarshal(EU_PORT_MAX_LENGTH + 1);

				error = (int) EUGetOCSPAccessInfoSettings(issuerCNPtr.DataPtr,
					addressPtr.DataPtr, portPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				address = addressPtr.GetStringData();
				port = portPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SetOCSPAccessInfoSettings(
			string issuerCN, string address, string port)
		{
			if (issuerCN.Length > EU_COMMON_NAME_MAX_LENGTH ||
				address.Length > EU_ADDRESS_MAX_LENGTH ||
				port.Length > EU_PORT_MAX_LENGTH)
			{
				return EU_ERROR_BAD_PARAMETER;
			}

			try
			{
				int error;
				EUMarshal issuerCNPtr = new EUMarshal(issuerCN);
				EUMarshal addressPtr = new EUMarshal(address);
				EUMarshal portPtr = new EUMarshal(port);

				error = (int) EUSetOCSPAccessInfoSettings(issuerCNPtr.DataPtr,
					addressPtr.DataPtr, portPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DeleteOCSPAccessInfoSettings(
			string issuerCN)
		{
			if (issuerCN.Length > EU_COMMON_NAME_MAX_LENGTH)
				return EU_ERROR_BAD_PARAMETER;

			try
			{
				int error;
				EUMarshal issuerCNPtr = new EUMarshal(issuerCN);

				error = (int) EUDeleteOCSPAccessInfoSettings(
					issuerCNPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: Certificate and CRLs storage functions

		private static int _RefreshFileStore(bool reload)
		{
			try
			{
				int error = (int) EURefreshFileStore(reload ? 1 : 0);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ShowCertificates()
		{
			try
			{
				EUShowCertificates();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _FreeCertOwnerInfo(
			EUMarshal certOwnerInfo,
			IntPtr context = new IntPtr())
		{
			if (certOwnerInfo == null)
				return EU_ERROR_NONE;

			try
			{
				if (certOwnerInfo.DataPtr != IntPtr.Zero)
				{
					if (context != IntPtr.Zero)
					{
						EUCtxFreeCertOwnerInfo(context,
							certOwnerInfo.DataPtr);
					}
					else
						EUFreeCertOwnerInfo(certOwnerInfo.DataPtr);
				}
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SelectCertInfo(
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);
				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int) EUSelectCertificateInfo(
					certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCertificatesCount(
			EU_SUBJECT_TYPE subjectType,
			EU_SUBJECT_SUB_TYPE subjectSubType, out int count)
		{
			count = 0;

			try
			{
				int error;
				EUMarshal countPtr = new EUMarshal(EUMarshal.INT_SIZE);

				error = (int)EUGetCertificatesCount((DWORD) subjectType,
					(DWORD) subjectSubType, countPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				count = countPtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnumCertificates(
			EU_SUBJECT_TYPE subjectType,
			EU_SUBJECT_SUB_TYPE subjectSubType, int index,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;

				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);
				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int)EUEnumCertificates((DWORD)subjectType,
					(DWORD)subjectSubType, (DWORD) index, certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _FreeCertInfo(EUMarshal certInfo)
		{
			if (certInfo == null)
				return EU_ERROR_NONE;

			try
			{
				if (certInfo.DataPtr != IntPtr.Zero)
					EUFreeCertificateInfo(certInfo.DataPtr);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCertificateInfo(
			string issuer, string serial, out EU_CERT_INFO certInfo)
		{
			EUMarshal certInfoPtr = null;

			certInfo = new EU_CERT_INFO();

			try
			{
				int error;
				EUMarshal issuerPtr = new EUMarshal(issuer);
				EUMarshal serialPtr = new EUMarshal(serial);
				certInfoPtr = new EUMarshal(EUMarshal.EU_CERT_INFO_SIZE);

				Marshal.WriteInt32(certInfoPtr.DataPtr, 0);

				error = (int) EUGetCertificateInfo(issuerPtr.DataPtr,
					serialPtr.DataPtr, certInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certInfo = new EU_CERT_INFO(certInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfo(certInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _FreeCertInfoEx(EUMarshal certInfoEx,
			IntPtr context = new IntPtr())
		{
			if (certInfoEx == null)
				return EU_ERROR_NONE;

			try
			{
				if (certInfoEx.DataPtr == IntPtr.Zero)
					return EU_ERROR_NONE;

				IntPtr intCertInfoEx = certInfoEx.GetPointerData(false);
				if (context != IntPtr.Zero)
					EUCtxFreeCertificateInfoEx(context, intCertInfoEx);
				else
					EUFreeCertificateInfoEx(intCertInfoEx);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCertificateInfoEx(
			string issuer, string serial, out EU_CERT_INFO_EX certInfo)
		{
			EUMarshal certInfoPtr = null;

			certInfo = new EU_CERT_INFO_EX();

			try
			{
				int error;
				EUMarshal issuerPtr = new EUMarshal(issuer);
				EUMarshal serialPtr = new EUMarshal(serial);
				certInfoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(certInfoPtr.DataPtr,
					IntPtr.Zero);

				error = (int) EUGetCertificateInfoEx(issuerPtr.DataPtr,
					serialPtr.DataPtr, certInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certInfo = new EU_CERT_INFO_EX(
					certInfoPtr.GetPointerData());
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(certInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCertificate(
			string issuer, string serial,
			ref string certificateString,
			ref byte[] certificateBinary)
		{
			EUMarshal certificateStringPtr = null;
			EUMarshal certificateBinaryPtr = null;

			try
			{
				int error;
				EUMarshal issuerPtr = new EUMarshal(issuer);
				EUMarshal serialPtr = new EUMarshal(serial);

				certificateStringPtr = new EUMarshal();
				certificateBinaryPtr = new EUMarshal();

				if (certificateString != null)
					certificateStringPtr = new EUMarshal(false);
				else if (certificateBinaryPtr != null)
					certificateBinaryPtr = new EUMarshal(true);

				error = (int) EUGetCertificate(issuerPtr.DataPtr,
					serialPtr.DataPtr, certificateStringPtr.DataPtr,
					certificateBinaryPtr.DataPtr,
					certificateBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (certificateString != null)
				{
					certificateString =
						certificateStringPtr.GetStringData();
				}
				else if (certificateBinary != null)
				{
					certificateBinary =
						certificateBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (certificateStringPtr != null)
					certificateStringPtr.FreeData();
				if (certificateBinaryPtr != null)
					certificateBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CheckCertificate(byte[] certificate)
		{
			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal(certificate);

				error = (int) EUCheckCertificate(certificatePtr.DataPtr,
					certificatePtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CheckCertificateByIssuerAndSerialEx(
			string issuer, string serial, ref string certificateString,
			ref byte[] certificateBinary, out int ocspAvailability)
		{
			EUMarshal certificateStringPtr = null;
			EUMarshal certificateBinaryPtr = null;

			ocspAvailability = EU_OCSP_SERVER_STATE_UNKNOWN;

			try
			{
				int error;
				EUMarshal issuerPtr = new EUMarshal(issuer);
				EUMarshal serialPtr = new EUMarshal(serial);
				EUMarshal ocspAvalabilityPtr =
					new EUMarshal(EUMarshal.INT_SIZE);

				certificateStringPtr = new EUMarshal();
				certificateBinaryPtr = new EUMarshal();

				if (certificateString != null)
					certificateStringPtr = new EUMarshal(false);
				else if (certificateBinary != null)
					certificateBinaryPtr = new EUMarshal(true);

				error = (int) EUCheckCertificateByIssuerAndSerialEx(
					issuerPtr.DataPtr, serialPtr.DataPtr,
					certificateStringPtr.DataPtr,
					certificateBinaryPtr.DataPtr,
					certificateBinaryPtr.BinaryDataLengthPtr,
					ocspAvalabilityPtr.DataPtr);
				if (error != EU_ERROR_NONE)
				{
					ocspAvailability = ocspAvalabilityPtr.GetIntData();
					return error;
				}

				ocspAvailability = ocspAvalabilityPtr.GetIntData();

				if (certificateString != null)
				{
					certificateString = 
						certificateStringPtr.GetStringData();
				}
				else if (certificateBinary != null)
				{
					certificateBinary = 
						certificateBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (certificateStringPtr != null)
					certificateStringPtr.FreeData();
				if (certificateBinaryPtr != null)
					certificateBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ParseCertificate(
			byte[] certificate, out EU_CERT_INFO certInfo)
		{
			EUMarshal certInfoPtr = null;

			certInfo = new EU_CERT_INFO();

			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal(certificate);
				certInfoPtr = new EUMarshal(EUMarshal.EU_CERT_INFO_SIZE);
				Marshal.WriteInt32(certInfoPtr.DataPtr, 0);

				error = (int) EUParseCertificate(certificatePtr.DataPtr,
					certificatePtr.DataLength, certInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certInfo = new EU_CERT_INFO(certInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfo(certInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _ParseCertificateEx(
			byte[] certificate, out EU_CERT_INFO_EX certInfo)
		{
			EUMarshal certInfoPtr = null;

			certInfo = new EU_CERT_INFO_EX();

			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal(certificate);
				certInfoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(certInfoPtr.DataPtr,
					IntPtr.Zero);

				error = (int) EUParseCertificateEx(certificatePtr.DataPtr,
					certificatePtr.DataLength, certInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certInfo = new EU_CERT_INFO_EX(
					certInfoPtr.GetPointerData());
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(certInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _SaveCertificate(
			byte[] certificate, bool certChain)
		{
			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal(certificate);

				if (certChain)
				{
					error = (int) EUSaveCertificates(certificatePtr.DataPtr,
						certificatePtr.DataLength);
				}
				else
				{
					error = (int) EUSaveCertificate(certificatePtr.DataPtr,
						certificatePtr.DataLength);
				}

				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DeleteCertificate(
			string issuer, string serial)
		{
			try
			{
				int error;
				EUMarshal issuerPtr = new EUMarshal(issuer);
				EUMarshal serialPtr = new EUMarshal(serial);

				error = (int) EUDeleteCertificate(
					issuerPtr.DataPtr, serialPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsCertificates(byte[] certificates)
		{
			try
			{
				int error;
				EUMarshal certificatesPtr = new EUMarshal(certificates);

				error = (int) EUIsCertificates(certificatesPtr.DataPtr,
					certificatesPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsCertificatesFile(string fileName)
		{
			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				error = (int) EUIsCertificatesFile(fileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ShowOwnCertificate()
		{
			try
			{
				EUShowOwnCertificate();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnumOwnCertificates(
			int index, out EU_CERT_INFO_EX certInfo)
		{
			EUMarshal certInfoPtr = null;

			certInfo = new EU_CERT_INFO_EX();

			try
			{
				int error;

				certInfoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(certInfoPtr.DataPtr,
					IntPtr.Zero);

				error = (int)EUEnumOwnCertificates((DWORD) index,
					certInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certInfo = new EU_CERT_INFO_EX(
					certInfoPtr.GetPointerData());
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(certInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _GetOwnCertificate(
			ref string certificateString,
			ref byte[] certificateBinary)
		{
			EUMarshal certificateStringPtr = null;
			EUMarshal certificateBinaryPtr = null;

			try
			{
				int error;

				certificateBinaryPtr = new EUMarshal();
				certificateStringPtr = new EUMarshal();

				if (certificateString != null)
					certificateStringPtr = new EUMarshal(false);
				else if (certificateBinary != null)
					certificateBinaryPtr = new EUMarshal(true);

				error = (int) EUGetOwnCertificate(certificateStringPtr.DataPtr,
					certificateBinaryPtr.DataPtr,
					certificateBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (certificateString != null)
				{
					certificateString =
						certificateStringPtr.GetStringData();
				}
				else if (certificateBinary != null)
				{
					certificateBinary =
						certificateBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (certificateStringPtr != null)
					certificateStringPtr.FreeData();
				if (certificateBinaryPtr != null)
					certificateBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ShowCRLs()
		{
			try
			{
				EUShowCRLs();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCRLsCount(out int count)
		{
			count = 0;

			try
			{
				int error;
				EUMarshal countPtr = new EUMarshal(
					EUMarshal.INT_SIZE);

				error = (int) EUGetCRLsCount(countPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				count = countPtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _FreeCRLInfo(EUMarshal crlInfo)
		{
			if (crlInfo == null)
				return EU_ERROR_NONE;

			try
			{
				if (crlInfo.DataPtr != IntPtr.Zero)
					EUFreeCRLInfo(crlInfo.DataPtr);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				crlInfo.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnumCRLs(int index,
			out EU_CRL_INFO crlInfo)
		{
			EUMarshal crlInfoPtr = null;

			crlInfo = new EU_CRL_INFO();

			try
			{
				int error;

				crlInfoPtr = new EUMarshal(
					EUMarshal.EU_CRL_INFO_SIZE);

				Marshal.WriteInt32(crlInfoPtr.DataPtr, 0);

				error = (int)EUEnumCRLs((DWORD)index, crlInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				crlInfo = new EU_CRL_INFO(crlInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCRLInfo(crlInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _FreeCRLDetailedInfo(
			EUMarshal crlDetailedInfo)
		{
			if (crlDetailedInfo == null)
				return EU_ERROR_NONE;

			try
			{
				if (crlDetailedInfo.DataPtr != IntPtr.Zero)
					EUFreeCRLDetailedInfo(crlDetailedInfo.DataPtr);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				crlDetailedInfo.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCRLDetailedInfo(
			string issuer, int crlNumber,
			out EU_CRL_DETAILED_INFO crlDetailedInfo)
		{
			EUMarshal crlDetailedInfoPtr = null;

			crlDetailedInfo = new EU_CRL_DETAILED_INFO();

			try
			{
				int error;
				EUMarshal issuerPtr = new EUMarshal(issuer);
				crlDetailedInfoPtr = new EUMarshal(
					EUMarshal.EU_CRL_DETAILED_INFO_SIZE);

				Marshal.WriteInt32(crlDetailedInfoPtr.DataPtr, 0);

				error = (int) EUGetCRLDetailedInfo(issuerPtr.DataPtr,
					(DWORD)crlNumber, crlDetailedInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				crlDetailedInfo = new EU_CRL_DETAILED_INFO(
					crlDetailedInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCRLDetailedInfo(crlDetailedInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _SaveCRL(
			bool isFullCRL, byte[] crl)
		{
			try
			{
				int error;
				EUMarshal crlPtr = new EUMarshal(crl);

				error = (int) EUSaveCRL(isFullCRL ? 1 : 0,
					crlPtr.DataPtr, crlPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ParseCRLInfo(
			byte [] crl, out EU_CRL_DETAILED_INFO crlDetailedInfo)
		{
			EUMarshal crlDetailedInfoPtr = null;

			crlDetailedInfo = new EU_CRL_DETAILED_INFO();

			try
			{
				int error;
				EUMarshal crlPtr = new EUMarshal(crl);
				crlDetailedInfoPtr = new EUMarshal(
					EUMarshal.EU_CRL_DETAILED_INFO_SIZE);

				Marshal.WriteInt32(crlDetailedInfoPtr.DataPtr, 0);

				error = (int) EUParseCRL(crlPtr.DataPtr,
					crlPtr.DataLength, crlDetailedInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				crlDetailedInfo = new EU_CRL_DETAILED_INFO(
					crlDetailedInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCRLDetailedInfo(crlDetailedInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCertificateByEmail(string email,
			int certKeyType, int keyUsage, SYSTEMTIME onTime,
			out string issuer, out string serial)
		{
			issuer = null;
			serial = null;

			try
			{
				int error;
				EUMarshal emailPtr = new EUMarshal(email);
				EUMarshal issuerPtr = new EUMarshal(EU_ISSUER_MAX_LENGTH + 1);
				EUMarshal serialPtr = new EUMarshal(EU_SERIAL_MAX_LENGTH + 1);
				EUMarshal onTimePtr = new EUMarshal(onTime);

				error = (int) EUGetCertificateByEMail(emailPtr.DataPtr,
					(DWORD) certKeyType, (DWORD) keyUsage, onTimePtr.DataPtr,
					issuerPtr.DataPtr, serialPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				issuer = issuerPtr.GetStringData();
				serial = serialPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCertificateByNBUCode(
			string NBUCode, int certKeyType, int keyUsage,
			SYSTEMTIME onTime, out string issuer, out string serial)
		{
			issuer = null;
			serial = null;

			try
			{
				int error; 
				EUMarshal NBUCodePtr = new EUMarshal(NBUCode);
				EUMarshal issuerPtr = new EUMarshal(EU_ISSUER_MAX_LENGTH + 1);
				EUMarshal serialPtr = new EUMarshal(EU_SERIAL_MAX_LENGTH + 1);
				EUMarshal onTimePtr = new EUMarshal(onTime);

				error = (int) EUGetCertificateByNBUCode(NBUCodePtr.DataPtr,
					(DWORD)certKeyType, (DWORD)keyUsage, onTimePtr.DataPtr,
					issuerPtr.DataPtr, serialPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				issuer = issuerPtr.GetStringData();
				serial = serialPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCertificatesByKeyInfo(
			byte[] privKeyInfo, string[] CMPServers,
			string[] CMPServersPorts, out byte[] certificates)
		{
			EUMarshal certificatesPtr = null;

			certificates = null;

			try
			{
				int error;
				EUMarshal privKeyInfoPtr = new EUMarshal(privKeyInfo);
				EUMarshal CMPServersPtr = new EUMarshal(CMPServers);
				EUMarshal CMPServersPortsPtr = new EUMarshal(CMPServersPorts);
				certificatesPtr = new EUMarshal(true);

				error = (int) EUGetCertificatesByKeyInfo(privKeyInfoPtr.DataPtr,
					privKeyInfoPtr.DataLength, CMPServersPtr.DataPtr,
					CMPServersPortsPtr.DataPtr, certificatesPtr.DataPtr,
					certificatesPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certificates = certificatesPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (certificatesPtr != null)
					certificatesPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCertificatesFromLDAPByEDRPOUCode(
			string edrpouCode, int certKeyType, int keyUsage,
			string[] LDAPServers, string[] LDAPServersPorts, out byte[] certificates)
		{
			EUMarshal certificatesPtr = null;

			certificates = null;

			try
			{
				int error;
				EUMarshal edrpouCodePtr = new EUMarshal(edrpouCode);
				EUMarshal LDAPServersPtr = new EUMarshal(LDAPServers);
				EUMarshal LDAPServersPortsPtr = new EUMarshal(LDAPServersPorts);
				certificatesPtr = new EUMarshal(true);

				error = (int) EUGetCertificatesFromLDAPByEDRPOUCode(
					edrpouCodePtr.DataPtr, (DWORD)certKeyType, (DWORD)keyUsage,
					LDAPServersPtr.DataPtr, LDAPServersPortsPtr.DataPtr,
					certificatesPtr.DataPtr,
					certificatesPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certificates = certificatesPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (certificatesPtr != null)
					certificatesPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _FreeCRInfo(EUMarshal crInfo)
		{
			if (crInfo == null)
				return EU_ERROR_NONE;

			try
			{
				if (crInfo.DataPtr == IntPtr.Zero)
					return EU_ERROR_NONE;

				IntPtr intCRInfo = crInfo.GetPointerData();
				EUFreeCRInfo(intCRInfo);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetCRInfo(byte[] request,
			out EU_CR_INFO crInfo)
		{
			EUMarshal crInfoPtr = null;

			crInfo = new EU_CR_INFO();

			try
			{
				int error;
				EUMarshal requestPtr = new EUMarshal(request);
				crInfoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteInt32(crInfoPtr.DataPtr, 0);

				error = (int) EUGetCRInfo(requestPtr.DataPtr,
					requestPtr.DataLength, crInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				crInfo = new EU_CR_INFO(
					crInfoPtr.GetPointerData(), request);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCRInfo(crInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: KeyMedia and private key functions

		private static int _GetPrivatekeyMedia(
			out EU_KEY_MEDIA keyMedia)
		{
			keyMedia = new EU_KEY_MEDIA(-1, -1, null);

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);

				error = (int) EUGetPrivateKeyMedia(keyMediaPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				keyMedia = new EU_KEY_MEDIA(keyMediaPtr.DataPtr);
				if (keyMedia.typeIndex == -1 && keyMedia.deviceIndex == -1)
					return EU_ERROR_UNKNOWN;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetPrivatekeyMediaEx(
			string caption, out EU_KEY_MEDIA keyMedia)
		{
			keyMedia = new EU_KEY_MEDIA(-1, -1, null);

			try
			{
				int error;
				EUMarshal captionPtr = new EUMarshal(caption);
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);

				error = (int) EUGetPrivateKeyMediaEx(captionPtr.DataPtr, 
					keyMediaPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				keyMedia = new EU_KEY_MEDIA(keyMediaPtr.DataPtr);
				if (keyMedia.typeIndex == -1 && keyMedia.deviceIndex == -1)
					return EU_ERROR_UNKNOWN;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnumKeyMediaTypes(int typeIndex,
			out string typeDescription)
		{
			typeDescription = null;

			try
			{
				int error;
				EUMarshal typeDescrPtr = new EUMarshal(
					EU_KEY_MEDIA_NAME_MAX_LENGTH + 1);

				error = (int) EUEnumKeyMediaTypes(
					(DWORD) typeIndex,
					typeDescrPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				typeDescription = typeDescrPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnumKeyMediaDevices(
			int typeIndex, int deviceIndex,
			out string deviceDescription)
		{
			deviceDescription = null;

			try
			{
				int error;
				EUMarshal deviceDescrPtr = new EUMarshal(
					EU_KEY_MEDIA_NAME_MAX_LENGTH + 1);

				error = (int) EUEnumKeyMediaDevices(
					(DWORD)typeIndex, (DWORD)deviceIndex,
					deviceDescrPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				deviceDescription = deviceDescrPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsPrivateKeyReaded(
			out bool isPrivKeyReaded)
		{
			isPrivKeyReaded = false;

			try
			{
				isPrivKeyReaded = EUIsPrivateKeyReaded() != 0;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ResetPrivateKey()
		{
			try
			{
				EUResetPrivateKey();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ReadPrivateKey(EU_KEY_MEDIA keyMedia,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);

				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int) EUReadPrivateKey(keyMediaPtr.DataPtr,
					certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _ReadPrivateKeyBinary(byte[] privateKey,
			string password, out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				EUMarshal privateKeyPtr = new EUMarshal(privateKey);
				EUMarshal passwordPtr = new EUMarshal(password);
				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);

				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int) EUReadPrivateKeyBinary(privateKeyPtr.DataPtr,
					privateKeyPtr.DataLength, passwordPtr.DataPtr,
					certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _ReadPrivateKeyFile(
			string privateKeyFileName, string password,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				EUMarshal privateKeyFileNamePtr = new EUMarshal(
					privateKeyFileName);
				EUMarshal passwordPtr = new EUMarshal(password);
				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);

				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int) EUReadPrivateKeyFile(
					privateKeyFileNamePtr.DataPtr,
					passwordPtr.DataPtr, certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _ReadFixedPrivateKey(
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);

				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int) EUReadPrivateKey(IntPtr.Zero,
					certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxFreePrivateKey(IntPtr privateKeyContext)
		{
			try
			{
				EUCtxFreePrivateKey(privateKeyContext);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxReadPrivateKey(IntPtr context,
			EU_KEY_MEDIA keyMedia, out IntPtr privateKeyContext,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			privateKeyContext = IntPtr.Zero;
			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				EUMarshal privateKeyContextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);
				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int) EUCtxReadPrivateKey(context,
					keyMediaPtr.DataPtr, privateKeyContextPtr.DataPtr,
					certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privateKeyContext = privateKeyContextPtr.GetPointerData();
				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr, context);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxReadPrivateKeyBinary(
			IntPtr context, byte[] privateKey, string password,
			out IntPtr privateKeyContext, out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			privateKeyContext = IntPtr.Zero;
			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				EUMarshal privateKeyPtr = new EUMarshal(privateKey);
				EUMarshal passwordPtr = new EUMarshal(password);
				EUMarshal privateKeyContextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);
				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int) EUCtxReadPrivateKeyBinary(context,
					privateKeyPtr.DataPtr, privateKeyPtr.DataLength,
					passwordPtr.DataPtr, privateKeyContextPtr.DataPtr,
					certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privateKeyContext = privateKeyContextPtr.GetPointerData();
				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr, context);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxReadPrivateKeyFile(
			IntPtr context, string privateKeyFileName,
			string password, out IntPtr privateKeyContext,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			privateKeyContext = IntPtr.Zero;
			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				EUMarshal privateKeyFileNamePtr = new EUMarshal(
					privateKeyFileName);
				EUMarshal passwordPtr = new EUMarshal(password);
				EUMarshal privateKeyContextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);
				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int) EUCtxReadPrivateKeyFile(context,
					privateKeyFileNamePtr.DataPtr, passwordPtr.DataPtr,
					privateKeyContextPtr.DataPtr, certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privateKeyContext = privateKeyContextPtr.GetPointerData();
				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr, context);
			}

			return EU_ERROR_NONE;
		}

		private static int _IsHardwareKeyMedia(
			bool bSelect, EU_KEY_MEDIA keyMedia,
			out bool isHardware)
		{
			isHardware = false;

			try
			{
				int error;
				EUMarshal keyMediaPtr;
				EUMarshal isHardwarePtr = new EUMarshal(
					EUMarshal.INT_SIZE);

				if (bSelect)
					keyMediaPtr = new EUMarshal();
				else
					keyMediaPtr = new EUMarshal(keyMedia);

				error = (int) EUIsHardwareKeyMedia(keyMediaPtr.DataPtr, 
					isHardwarePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				isHardware = isHardwarePtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsPrivateKeyExists(
			bool bSelect,  EU_KEY_MEDIA keyMedia,
			out bool isExists)
		{
			isExists = false;

			try
			{
				int error;
				EUMarshal keyMediaPtr;
				EUMarshal isExistsPtr = new EUMarshal(
					EUMarshal.INT_SIZE);

				if (bSelect)
					keyMediaPtr = new EUMarshal();
				else
					keyMediaPtr = new EUMarshal(keyMedia);

				error = (int) EUIsPrivateKeyExists(keyMediaPtr.DataPtr,
					isExistsPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				isExists = isExistsPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GeneratePrivateKey(
			bool bSelect, bool bFormat, EU_KEY_MEDIA keyMedia,
			int UAKeysType, int UADSKeysSpec,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath,
			bool useUserInfo, EU_USER_INFO userInfo,
			string extKeyUsages,
			ref byte[] privKey, ref byte[] privKeyInfo,
			ref byte[] UARequest, ref string UAReqFileName,
			ref byte[] UAKEPRequest, ref string UAKEPReqFileName,
			ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			EUMarshal privKeyPtr = null;
			EUMarshal privKeyInfoPtr = null;
			EUMarshal UARequestPtr = null;
			EUMarshal UAKEPRequestPtr = null;
			EUMarshal internationalRequestPtr = null;

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal();
				EUMarshal UAReqFileNamePtr = new EUMarshal();
				EUMarshal UAKEPReqFileNamePtr = new EUMarshal();
				EUMarshal internationalReqFileNamePtr = new EUMarshal();
				EUMarshal UAParamsPathPtr = new EUMarshal(UAParamsPath);
				EUMarshal internationalParamsPathPtr = 
					new EUMarshal(internationalParamsPath);
				EUMarshal userInfoPtr = new EUMarshal();
				EUMarshal extKeyUsagesPtr = new EUMarshal();

				privKeyPtr = new EUMarshal();
				privKeyInfoPtr = new EUMarshal();
				UARequestPtr = new EUMarshal();
				UAKEPRequestPtr = new EUMarshal();
				internationalRequestPtr = new EUMarshal();

				if (!bSelect)
					keyMediaPtr = new EUMarshal(keyMedia);

				if (useUserInfo)
					userInfoPtr = new EUMarshal(userInfo);

				if (extKeyUsages != null)
					extKeyUsagesPtr = new EUMarshal(extKeyUsages);

				if (privKey != null)
					privKeyPtr = new EUMarshal(true);

				if (privKeyInfo != null)
					privKeyInfoPtr = new EUMarshal(true);

				if (UARequest != null)
					UARequestPtr = new EUMarshal(true);

				if (UAReqFileName != null)
					UAReqFileNamePtr = new EUMarshal(EU_PATH_MAX_LENGTH + 1);

				if (UAKEPRequest != null)
					UAKEPRequestPtr = new EUMarshal(true);

				if (UAKEPReqFileName != null)
					UAKEPReqFileNamePtr = new EUMarshal(EU_PATH_MAX_LENGTH + 1);

				if (internationalRequest != null)
					internationalRequestPtr = new EUMarshal(true);

				if (internationalReqFileName != null)
				{
					internationalReqFileNamePtr =
						new EUMarshal(EU_PATH_MAX_LENGTH + 1);
				}

				error = (int) EUGeneratePrivateKeyEx(keyMediaPtr.DataPtr,
					bFormat ? 1 : 0,
					(DWORD)UAKeysType, (DWORD)UADSKeysSpec, (DWORD)UAKEPKeysSpec,
					UAParamsPathPtr.DataPtr,
					(DWORD)internationalKeysType, (DWORD)internationalKeysSpec,
					internationalParamsPathPtr.DataPtr,
					userInfoPtr.DataPtr, extKeyUsagesPtr.DataPtr,
					privKeyPtr.DataPtr, privKeyPtr.BinaryDataLengthPtr,
					privKeyInfoPtr.DataPtr,
					privKeyInfoPtr.BinaryDataLengthPtr,
					UARequestPtr.DataPtr,
					UARequestPtr.BinaryDataLengthPtr,
					UAReqFileNamePtr.DataPtr,
					UAKEPRequestPtr.DataPtr,
					UAKEPRequestPtr.BinaryDataLengthPtr,
					UAKEPReqFileNamePtr.DataPtr,
					internationalRequestPtr.DataPtr,
					internationalRequestPtr.BinaryDataLengthPtr,
					internationalReqFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (privKey != null)
					privKey = privKeyPtr.GetBinaryData();

				if (privKeyInfo != null)
					privKeyInfo = privKeyInfoPtr.GetBinaryData();

				if (UARequest != null)
					UARequest = UARequestPtr.GetBinaryData();

				if (UAReqFileName != null)
					UAReqFileName = UAReqFileNamePtr.GetStringData();

				if (UAKEPRequest != null)
					UAKEPRequest = UAKEPRequestPtr.GetBinaryData();

				if (UAKEPReqFileName != null)
					UAKEPReqFileName = UAKEPReqFileNamePtr.GetStringData();

				if (internationalRequest != null)
				{
					internationalRequest =
						internationalRequestPtr.GetBinaryData();
				}

				if (internationalReqFileName != null)
				{
					internationalReqFileName =
						internationalReqFileNamePtr.GetStringData();
				}
			}

			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (privKeyPtr != null)
					privKeyPtr.FreeData();
				if (privKeyInfoPtr != null)
					privKeyInfoPtr.FreeData();
				if (UARequestPtr != null)
					UARequestPtr.FreeData();
				if (UAKEPRequestPtr != null)
					UAKEPRequestPtr.FreeData();
				if (internationalRequestPtr != null)
					internationalRequestPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _MakeNewCertificate(
			bool selectOldKeyMedia, EU_KEY_MEDIA oldKeyMedia,
			byte[] oldPrivateKey, string oldPrivateKeyPassword,
			int UAKeysType, int UADSKeysSpec, bool useUADSKeyAsKEP,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath,
			bool selectNewKeyMedia, EU_KEY_MEDIA newKeyMedia,
			string newPrivateKeyPassword, ref byte[] newPrivateKey)
		{
			EUMarshal newPrivateKeyPtr = null;

			try
			{
				int error;
				EUMarshal oldKeyMediaPtr = new EUMarshal();
				EUMarshal oldPrivateKeyPtr = new EUMarshal();
				EUMarshal oldPrivateKeyPasswordPtr = new EUMarshal();
				EUMarshal UAParamsPathPtr = new EUMarshal(UAParamsPath);
				EUMarshal internationalParamsPathPtr =
					new EUMarshal(internationalParamsPath);
				EUMarshal newKeyMediaPtr = new EUMarshal();
				EUMarshal newPrivateKeyPasswordPtr = new EUMarshal();

				newPrivateKeyPtr = new EUMarshal();

				if (!selectOldKeyMedia)
				{
					if (oldPrivateKey != null)
					{
						oldPrivateKeyPtr = new EUMarshal(oldPrivateKey);
						oldPrivateKeyPasswordPtr = 
							new EUMarshal(oldPrivateKeyPassword);
					}
					else
					{
						oldKeyMediaPtr = new EUMarshal(oldKeyMedia);
					}
				}

				if (!selectNewKeyMedia)
				{
					if (newPrivateKey != null)
					{
						newPrivateKeyPtr = new EUMarshal(true);
						newPrivateKeyPasswordPtr =
							new EUMarshal(newPrivateKeyPassword);
					}
					else
					{
						newKeyMediaPtr = new EUMarshal(newKeyMedia);
					}
				}

				error = (int) EUMakeNewCertificate(
					oldKeyMediaPtr.DataPtr,
					oldPrivateKeyPtr.DataPtr,
					oldPrivateKeyPtr.DataLength,
					oldPrivateKeyPasswordPtr.DataPtr,
					(DWORD)UAKeysType, (DWORD)UADSKeysSpec, useUADSKeyAsKEP ? 1 : 0,
					(DWORD)UAKEPKeysSpec, UAParamsPathPtr.DataPtr,
					(DWORD)internationalKeysType, (DWORD)internationalKeysSpec,
					internationalParamsPathPtr.DataPtr,
					newKeyMediaPtr.DataPtr, newPrivateKeyPasswordPtr.DataPtr,
					newPrivateKeyPtr.DataPtr, newPrivateKeyPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (newPrivateKey != null)
					newPrivateKey = newPrivateKeyPtr.GetBinaryData();
			}

			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (newPrivateKeyPtr != null)
					newPrivateKeyPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int SetKeyMediaPassword(
			bool bSelect, EU_KEY_MEDIA keyMedia)
		{
			try
			{
				int error;
				EUMarshal keyMediaPtr;

				if (bSelect)
					keyMediaPtr = new EUMarshal();
				else
					keyMediaPtr = new EUMarshal(keyMedia);

				error = (int) EUSetKeyMediaPassword(keyMediaPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ChangePrivateKeyPassword(
			bool bSelect, EU_KEY_MEDIA keyMedia, string newPassword)
		{
			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal();
				EUMarshal newPasswordPtr = new EUMarshal();

				if (!bSelect)
				{
					keyMediaPtr = new EUMarshal(keyMedia);
					newPasswordPtr = new EUMarshal(newPassword);
				}

				error = (int) EUChangePrivateKeyPassword(
					keyMediaPtr.DataPtr, newPasswordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ChangeSoftwarePrivateKeyPassword(
			byte[] privKeySource, string oldPassword,
			string newPassword, out byte[] privKeyTarget)
		{
			EUMarshal privKeyTargetPtr = null;

			privKeyTarget = null;

			try
			{
				int error;
				EUMarshal privKeySourcePtr = 
					new EUMarshal(privKeySource);
				EUMarshal oldPasswordPtr =
					new EUMarshal(oldPassword);
				EUMarshal newPasswordPtr =
					new EUMarshal(newPassword);
				privKeyTargetPtr = new EUMarshal(true);

				error = (int) EUChangeSoftwarePrivateKeyPassword(
					privKeySourcePtr.DataPtr, privKeySourcePtr.DataLength,
					oldPasswordPtr.DataPtr, newPasswordPtr.DataPtr,
					privKeyTargetPtr.DataPtr, privKeyTargetPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privKeyTarget = privKeyTargetPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (privKeyTargetPtr != null)
					privKeyTargetPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _BackupPrivateKey(bool bSelect,
			EU_KEY_MEDIA sourceKeyMedia, EU_KEY_MEDIA targetKeyMedia)
		{
			try
			{
				int error;
				EUMarshal sourceKeyMediaPtr = new EUMarshal();
				EUMarshal targetKeyMediaPtr = new EUMarshal();

				if (!bSelect)
				{
					sourceKeyMediaPtr = new EUMarshal(sourceKeyMedia);
					targetKeyMediaPtr = new EUMarshal(targetKeyMedia);
				}

				error = (int) EUBackupPrivateKey(
					sourceKeyMediaPtr.DataPtr,
					targetKeyMediaPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DestroyPrivateKey(
			bool bSelect, EU_KEY_MEDIA keyMedia)
		{
			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal();

				if (!bSelect)
					keyMediaPtr = new EUMarshal(keyMedia);

				error = (int) EUDestroyPrivateKey(
					keyMediaPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetKeyInfo(EU_KEY_MEDIA keyMedia,
			out byte[] privKeyInfo)
		{
			EUMarshal privKeyInfoPtr = null;

			privKeyInfo = null;

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				privKeyInfoPtr = new EUMarshal(true);

				error = (int) EUGetKeyInfo(keyMediaPtr.DataPtr,
					privKeyInfoPtr.DataPtr,
					privKeyInfoPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privKeyInfo = privKeyInfoPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (privKeyInfoPtr != null)
					privKeyInfoPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetKeyInfoBinary(byte[] privateKey,
			string password, out byte[] privKeyInfo)
		{
			EUMarshal privKeyInfoPtr = null;

			privKeyInfo = null;

			try
			{
				int error;
				EUMarshal privateKeyPtr = new EUMarshal(privateKey);
				EUMarshal passwordPtr = new EUMarshal(password);
				privKeyInfoPtr = new EUMarshal(true);

				error = (int) EUGetKeyInfoBinary(
					privateKeyPtr.DataPtr, privateKeyPtr.DataLength,
					passwordPtr.DataPtr, privKeyInfoPtr.DataPtr,
					privKeyInfoPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privKeyInfo = privKeyInfoPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (privKeyInfoPtr != null)
					privKeyInfoPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetKeyInfoFile(string privateKeyFile,
			string password, out byte[] privKeyInfo)
		{
			EUMarshal privKeyInfoPtr = null;

			privKeyInfo = null;

			try
			{
				int error;
				EUMarshal privateKeyFilePtr =
					new EUMarshal(privateKeyFile);
				EUMarshal passwordPtr = new EUMarshal(password);
				privKeyInfoPtr = new EUMarshal(true);

				error = (int) EUGetKeyInfoFile(
					privateKeyFilePtr.DataPtr,
					passwordPtr.DataPtr, privKeyInfoPtr.DataPtr,
					privKeyInfoPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privKeyInfo = privKeyInfoPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (privKeyInfoPtr != null)
					privKeyInfoPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnumJKSPrivateKeys(byte[] container,
			int index, out string keyAlias)
		{
			EUMarshal keyAliasPtr = null;

			keyAlias = "";

			try
			{
				int error;
				EUMarshal containerPtr = new EUMarshal(container);
				keyAliasPtr = new EUMarshal(false);

				error = (int) EUEnumJKSPrivateKeys(
					containerPtr.DataPtr, containerPtr.DataLength,
					(DWORD) index, keyAliasPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				keyAlias = keyAliasPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (keyAliasPtr != null)
					keyAliasPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnumJKSPrivateKeysFile(string containerFile,
			int index, out string keyAlias)
		{
			EUMarshal keyAliasPtr = null;

			keyAlias = "";

			try
			{
				int error;
				EUMarshal containerFilePtr =
					new EUMarshal(containerFile);
				keyAliasPtr = new EUMarshal(false);

				error = (int)EUEnumJKSPrivateKeysFile(
					containerFilePtr.DataPtr,
					(DWORD)index, keyAliasPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				keyAlias = keyAliasPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (keyAliasPtr != null)
					keyAliasPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetJKSPrivateKey(byte[] container,
			string keyAlias, out byte[] privateKey, out byte[][] certificates)
		{
			EUMarshal privateKeyPtr = null;
			EUMarshalArrayOfBytesArrays certificatesPtr = null;

			privateKey = null;
			certificates = null;

			try
			{
				int error;
				EUMarshal containerPtr = new EUMarshal(container);
				EUMarshal keyAliasPtr = new EUMarshal(keyAlias);

				privateKeyPtr = new EUMarshal(true);
				certificatesPtr = new EUMarshalArrayOfBytesArrays(
					EUFreeCertificatesArray);

				error = (int) EUGetJKSPrivateKey(
					containerPtr.DataPtr, containerPtr.DataLength,
					keyAliasPtr.DataPtr, privateKeyPtr.DataPtr, 
					privateKeyPtr.BinaryDataLengthPtr,
					certificatesPtr.CountPtr,
					certificatesPtr.ArraysPtr,
					certificatesPtr.ArraysLengthesPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privateKey = privateKeyPtr.GetBinaryData();
				certificates = certificatesPtr.GetBinaryDataArrays();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (privateKeyPtr != null)
					privateKeyPtr.FreeData();
				if (certificatesPtr != null)
					certificatesPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetJKSPrivateKeyFile(string containerFile,
			string keyAlias, out byte[] privateKey, out byte[][] certificates)
		{
			EUMarshal privateKeyPtr = null;
			EUMarshalArrayOfBytesArrays certificatesPtr = null;

			privateKey = null;
			certificates = null;

			try
			{
				int error;
				EUMarshal containerFilePtr = new EUMarshal(containerFile);
				EUMarshal keyAliasPtr = new EUMarshal(keyAlias);

				privateKeyPtr = new EUMarshal(true);
				certificatesPtr = new EUMarshalArrayOfBytesArrays(
					EUFreeCertificatesArray);

				error = (int)EUGetJKSPrivateKeyFile(
					containerFilePtr.DataPtr, keyAliasPtr.DataPtr,
					privateKeyPtr.DataPtr, privateKeyPtr.BinaryDataLengthPtr,
					certificatesPtr.CountPtr,
					certificatesPtr.ArraysPtr,
					certificatesPtr.ArraysLengthesPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privateKey = privateKeyPtr.GetBinaryData();
				certificates = certificatesPtr.GetBinaryDataArrays();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (privateKeyPtr != null)
					privateKeyPtr.FreeData();
				if (certificatesPtr != null)
					certificatesPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _FreeKeyMediaDeviceInfo(EUMarshal info)
		{
			if (info == null)
				return EU_ERROR_NONE;

			try
			{
				if (info.DataPtr == IntPtr.Zero)
					return EU_ERROR_NONE;

				IntPtr infoPtr = info.GetPointerData(false);
				EUFreeKeyMediaDeviceInfo(infoPtr);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetKeyMediaDeviceInfo(EU_KEY_MEDIA keyMedia,
			out EU_KEY_MEDIA_DEVICE_INFO info)
		{
			EUMarshal infoPtr = null;

			info = new EU_KEY_MEDIA_DEVICE_INFO();

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				infoPtr = new EUMarshal(
					EUMarshal.PTR_SIZE);

				Marshal.WriteInt32(infoPtr.DataPtr, 0);

				error = (int) EUGetKeyMediaDeviceInfo(
					keyMediaPtr.DataPtr,
					infoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_KEY_MEDIA_DEVICE_INFO(
					infoPtr.GetPointerData());
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeKeyMediaDeviceInfo(infoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetOwnCertificate(
			IntPtr privateKeyContext, int certKeyType, int keyUsage,
			out EU_CERT_INFO_EX info, out byte[] certificate)
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;
			int error;

			info = new EU_CERT_INFO_EX();
			certificate = null;

			try
			{
				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);
				certificatePtr = new EUMarshal(true,
					privateKeyContext);

				error = (int) EUCtxGetOwnCertificate(
					privateKeyContext, (DWORD)certKeyType, (DWORD)keyUsage,
					infoPtr.DataPtr, certificatePtr.DataPtr,
					certificatePtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());

				certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr,
					privateKeyContext);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}


			return EU_ERROR_NONE;
		}

		private static int _CtxEnumOwnCertificates(
			IntPtr privateKeyContext, int index,
			out EU_CERT_INFO_EX info,
			out byte[] certificate)
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;
			int error;

			info = new EU_CERT_INFO_EX();
			certificate = null;

			try
			{
				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);
				certificatePtr = new EUMarshal(true,
					privateKeyContext);

				error = (int) EUCtxEnumOwnCertificates(
					privateKeyContext, (DWORD)index,
					infoPtr.DataPtr, certificatePtr.DataPtr,
					certificatePtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());
				certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr,
					privateKeyContext);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxEnumPrivateKeyInfo(
			IntPtr privateKeyContext, int index,
			out EU_PRIVATE_KEY_INFO info)
		{
			EUMarshal keyIDsPtr = null;
			int error;

			info = new EU_PRIVATE_KEY_INFO();

			try
			{
				EUMarshal keyTypePtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal keyUsagePtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal keyIDsCountPtr = 
					new EUMarshal(EUMarshal.INT_SIZE);
				keyIDsPtr = new EUMarshal(false, privateKeyContext);

				error = (int) EUCtxEnumPrivateKeyInfo(
					privateKeyContext, (DWORD)index,
					keyTypePtr.DataPtr, keyUsagePtr.DataPtr, 
					keyIDsCountPtr.DataPtr, keyIDsPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				string[] keyIDs = keyIDsPtr.GetStringsData();
				if (keyIDs.Length != keyIDsCountPtr.GetIntData())
					return EU_ERROR_BAD_PARAMETER;

				info = new EU_PRIVATE_KEY_INFO(
					keyTypePtr.GetIntData(),
					keyUsagePtr.GetIntData(),
					keyIDs);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (keyIDsPtr != null)
					keyIDsPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxExportPrivateKeyContainer(
			IntPtr privateKeyContext, string password, string keyID,
			out byte[] container)
		{
			EUMarshal containerPtr = null;
			int error;

			container = null;

			try
			{
				EUMarshal passwordPtr = new EUMarshal(password);
				EUMarshal keyIDPtr = new EUMarshal(keyID);
				containerPtr = new EUMarshal(true, privateKeyContext);

				error = (int) EUCtxExportPrivateKeyContainer(
					privateKeyContext, passwordPtr.DataPtr,
					keyIDPtr.DataPtr, containerPtr.DataPtr,
					containerPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				container = containerPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (containerPtr != null)
					containerPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxExportPrivateKeyPFXContainer(
			IntPtr privateKeyContext, string password, 
			bool exportCerts, bool [] trustedKeyIDs, string[] keyIDs,
			out byte[] container)
		{
			EUMarshal containerPtr = null;
			int error;

			container = null;

			if (trustedKeyIDs.Length != keyIDs.Length)
				return EU_ERROR_BAD_PARAMETER;

			try
			{
				EUMarshal passwordPtr = new EUMarshal(password);
				EUMarshal trustedKeyIDsPtr = 
					new EUMarshal(trustedKeyIDs);
				EUMarshal keyIDsPtr = new EUMarshal(keyIDs);
				containerPtr = new EUMarshal(true, privateKeyContext);

				error = (int) EUCtxExportPrivateKeyPFXContainer(
					privateKeyContext, passwordPtr.DataPtr,
					exportCerts ? 1 : 0, (DWORD)keyIDs.Length, 
					trustedKeyIDsPtr.DataPtr, keyIDsPtr.DataPtr,
					containerPtr.DataPtr,
					containerPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				container = containerPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (containerPtr != null)
					containerPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxExportPrivateKeyContainerFile(
			IntPtr privateKeyContext, string password,
			string keyID, string fileName)
		{
			int error;

			try
			{
				EUMarshal passwordPtr = new EUMarshal(password);
				EUMarshal keyIDPtr = new EUMarshal(keyID);
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				error = (int) EUCtxExportPrivateKeyContainerFile(
					privateKeyContext, passwordPtr.DataPtr,
					keyIDPtr.DataPtr, fileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxExportPrivateKeyPFXContainerFile(
			IntPtr privateKeyContext, string password,
			bool exportCerts, bool[] trustedKeyIDs, string[] keyIDs,
			string fileName)
		{
			int error;

			if (trustedKeyIDs.Length != keyIDs.Length)
				return EU_ERROR_BAD_PARAMETER;

			try
			{
				EUMarshal passwordPtr = new EUMarshal(password);
				EUMarshal trustedKeyIDsPtr =
					new EUMarshal(trustedKeyIDs);
				EUMarshal keyIDsPtr = new EUMarshal(keyIDs);
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				error = (int) EUCtxExportPrivateKeyPFXContainerFile(
					privateKeyContext, passwordPtr.DataPtr,
					exportCerts ? 1 : 0, (DWORD)keyIDs.Length,
					trustedKeyIDsPtr.DataPtr, keyIDsPtr.DataPtr,
					fileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetCertificateFromPrivateKey(
			IntPtr privateKeyContext, string keyID,
			out EU_CERT_INFO_EX info,
			out byte[] certificate)
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;
			int error;

			info = new EU_CERT_INFO_EX();
			certificate = null;

			try
			{
				EUMarshal keyIDPtr = new EUMarshal(keyID);
				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);
				certificatePtr = new EUMarshal(true,
					privateKeyContext);

				error = (int) EUCtxGetCertificateFromPrivateKey(
					privateKeyContext, keyIDPtr.DataPtr,
					infoPtr.DataPtr, certificatePtr.DataPtr,
					certificatePtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());
				certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr,
					privateKeyContext);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ChangeOwnCertificatesStatus(
			int requestType, int revocationReason)
		{
			try
			{
				int error;
				error = (int) EUChangeOwnCertificatesStatus(
					(DWORD)requestType, (DWORD)revocationReason);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxChangeOwnCertificatesStatus(
			IntPtr privateKeyContext, int requestType, int revocationReason)
		{
			try
			{
				int error;
				error = (int) EUCtxChangeOwnCertificatesStatus(
					privateKeyContext, (DWORD)requestType, (DWORD)revocationReason);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxIsNamedPrivateKeyExists(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			out bool isExists)
		{
			isExists = false;

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				EUMarshal namedPKeyLabelPtr = new EUMarshal(
					namedPrivateKeyLabel, true);
				EUMarshal namedPKeyPasswordPtr = new EUMarshal(
					namedPrivateKeyPassword, true);
				EUMarshal isExistsPtr = new EUMarshal(
					EUMarshal.INT_SIZE);

				error = (int) EUCtxIsNamedPrivateKeyExists(
					context, keyMediaPtr.DataPtr,
					namedPKeyLabelPtr.DataPtr, 
					namedPKeyPasswordPtr.DataPtr, 
					isExistsPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				isExists = isExistsPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGenerateNamedPrivateKey(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			int UAKeysType, int UADSKeysSpec,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath,
			ref byte[] UARequest, ref string UAReqFileName,
			ref byte[] UAKEPRequest, ref string UAKEPReqFileName,
			ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			EUMarshal UARequestPtr = null;
			EUMarshal UAKEPRequestPtr = null;
			EUMarshal internationalRequestPtr = null;

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				EUMarshal namedPKeyLabelPtr = new EUMarshal(
					namedPrivateKeyLabel, true);
				EUMarshal namedPKeyPasswordPtr = new EUMarshal(
					namedPrivateKeyPassword, true);
				EUMarshal UAReqFileNamePtr = new EUMarshal();
				EUMarshal UAKEPReqFileNamePtr = new EUMarshal();
				EUMarshal internationalReqFileNamePtr = new EUMarshal();
				EUMarshal UAParamsPathPtr = new EUMarshal(UAParamsPath);
				EUMarshal internationalParamsPathPtr =
					new EUMarshal(internationalParamsPath);

				UARequestPtr = new EUMarshal();
				UAKEPRequestPtr = new EUMarshal();
				internationalRequestPtr = new EUMarshal();

				if (UARequest != null)
					UARequestPtr = new EUMarshal(true, context);

				if (UAReqFileName != null)
					UAReqFileNamePtr = new EUMarshal(EU_PATH_MAX_LENGTH + 1);

				if (UAKEPRequest != null)
					UAKEPRequestPtr = new EUMarshal(true, context);

				if (UAKEPReqFileName != null)
					UAKEPReqFileNamePtr = new EUMarshal(EU_PATH_MAX_LENGTH + 1);

				if (internationalRequest != null)
					internationalRequestPtr = new EUMarshal(true, context);

				if (internationalReqFileName != null)
				{
					internationalReqFileNamePtr =
						new EUMarshal(EU_PATH_MAX_LENGTH + 1);
				}

				error = (int)EUCtxGenerateNamedPrivateKey(
					context, keyMediaPtr.DataPtr, 
					namedPKeyLabelPtr.DataPtr, namedPKeyPasswordPtr.DataPtr,
					(DWORD)UAKeysType, (DWORD)UADSKeysSpec, (DWORD)UAKEPKeysSpec,
					UAParamsPathPtr.DataPtr,
					(DWORD)internationalKeysType, (DWORD)internationalKeysSpec,
					internationalParamsPathPtr.DataPtr,
					UARequestPtr.DataPtr,
					UARequestPtr.BinaryDataLengthPtr,
					UAReqFileNamePtr.DataPtr,
					UAKEPRequestPtr.DataPtr,
					UAKEPRequestPtr.BinaryDataLengthPtr,
					UAKEPReqFileNamePtr.DataPtr,
					internationalRequestPtr.DataPtr,
					internationalRequestPtr.BinaryDataLengthPtr,
					internationalReqFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (UARequest != null)
					UARequest = UARequestPtr.GetBinaryData();

				if (UAReqFileName != null)
					UAReqFileName = UAReqFileNamePtr.GetStringData();

				if (UAKEPRequest != null)
					UAKEPRequest = UAKEPRequestPtr.GetBinaryData();

				if (UAKEPReqFileName != null)
					UAKEPReqFileName = UAKEPReqFileNamePtr.GetStringData();

				if (internationalRequest != null)
				{
					internationalRequest =
						internationalRequestPtr.GetBinaryData();
				}

				if (internationalReqFileName != null)
				{
					internationalReqFileName =
						internationalReqFileNamePtr.GetStringData();
				}
			}

			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (UARequestPtr != null)
					UARequestPtr.FreeData();
				if (UAKEPRequestPtr != null)
					UAKEPRequestPtr.FreeData();
				if (internationalRequestPtr != null)
					internationalRequestPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxReadNamedPrivateKey(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			out IntPtr privateKeyContext,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			EUMarshal certOwnerInfoPtr = null;

			privateKeyContext = IntPtr.Zero;
			certOwnerInfo = new EU_CERT_OWNER_INFO();

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				EUMarshal namedPKeyLabelPtr = new EUMarshal(
					namedPrivateKeyLabel, true);
				EUMarshal namedPKeyPasswordPtr = new EUMarshal(
					namedPrivateKeyPassword, true);
				EUMarshal privateKeyContextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				certOwnerInfoPtr = new EUMarshal(
					EUMarshal.EU_CERT_OWNER_INFO_SIZE);
				Marshal.WriteInt32(certOwnerInfoPtr.DataPtr, 0);

				error = (int)EUCtxReadNamedPrivateKey(context,
					keyMediaPtr.DataPtr, 
					namedPKeyLabelPtr.DataPtr, 
					namedPKeyPasswordPtr.DataPtr,
					privateKeyContextPtr.DataPtr,
					certOwnerInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				privateKeyContext = privateKeyContextPtr.GetPointerData();
				certOwnerInfo = new EU_CERT_OWNER_INFO(
					certOwnerInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertOwnerInfo(certOwnerInfoPtr, context);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxDestroyNamedPrivateKey(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword)
		{
			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				EUMarshal namedPKeyLabelPtr = new EUMarshal(
					namedPrivateKeyLabel, true);
				EUMarshal namedPKeyPasswordPtr = new EUMarshal(
					namedPrivateKeyPassword, true);

				error = (int)EUCtxDestroyNamedPrivateKey(context,
					keyMediaPtr.DataPtr,
					namedPKeyLabelPtr.DataPtr,
					namedPKeyPasswordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxChangeNamedPrivateKeyPassword(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword, 
			string namedPrivateKeyNewPassword)
		{
			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				EUMarshal namedPKeyLabelPtr = new EUMarshal(
					namedPrivateKeyLabel, true);
				EUMarshal namedPKeyPasswordPtr = new EUMarshal(
					namedPrivateKeyPassword, true);
				EUMarshal namedPKeyNewPasswordPtr = new EUMarshal(
					namedPrivateKeyNewPassword, true);

				error = (int)EUCtxChangeNamedPrivateKeyPassword(
					context, keyMediaPtr.DataPtr,
					namedPKeyLabelPtr.DataPtr,
					namedPKeyPasswordPtr.DataPtr,
					namedPKeyNewPasswordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetNamedPrivateKeyInfo(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			out byte[] keyInfo)
		{
			EUMarshal keyInfoPtr = null;

			keyInfo = new byte[0];

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				EUMarshal namedPKeyLabelPtr = new EUMarshal(
					namedPrivateKeyLabel, true);
				EUMarshal namedPKeyPasswordPtr = new EUMarshal(
					namedPrivateKeyPassword, true);

				keyInfoPtr = new EUMarshal(true, context);

				error = (int) EUCtxGetNamedPrivateKeyInfo(context,
					keyMediaPtr.DataPtr,
					namedPKeyLabelPtr.DataPtr,
					namedPKeyPasswordPtr.DataPtr,
					keyInfoPtr.DataPtr, keyInfoPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				keyInfo = keyInfoPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (keyInfoPtr != null)
					keyInfoPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxEnumNamedPrivateKeys(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			int index, out string namedPrivateKeyLabel)
		{
			EUMarshal labelPtr = null;

			namedPrivateKeyLabel = "";

			try
			{
				int error;
				EUMarshal keyMediaPtr = new EUMarshal(keyMedia);
				labelPtr = new EUMarshal(
					EU_NAMED_PRIVATE_KEY_LABEL_MAX_LENGTH + 1);

				error = (int) EUCtxEnumNamedPrivateKeys(
					context, keyMediaPtr.DataPtr, (DWORD) index,
					labelPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				namedPrivateKeyLabel = labelPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: Hash functions

		private static int _HashData(
			string dataString, byte[] dataBinary,
			ref string hashString, ref byte[] hashBinary)
		{
			EUMarshal hashStringPtr = null;
			EUMarshal hashBinaryPtr = null;
			EUMarshal dataPtr = null;

			try
			{
				int error;
				dataPtr = new EUMarshal();

				hashStringPtr = new EUMarshal();
				hashBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (hashString != null)
					hashStringPtr = new EUMarshal(false);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(true);

				error = (int) EUHashData(dataPtr.DataPtr, dataPtr.DataLength,
					hashStringPtr.DataPtr, hashBinaryPtr.DataPtr,
					hashBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (hashString != null)
					hashString = hashStringPtr.GetStringData();
				else if (hashBinary != null)
					hashBinary = hashBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (hashStringPtr != null)
					hashStringPtr.FreeData();
				if (hashBinaryPtr != null)
					hashBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _HashDataContinue(
			string dataString, byte[] dataBinary)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUHashDataContinue(dataPtr.DataPtr,
					dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _HashDataEnd(ref string hashString,
			ref byte[] hashBinary)
		{
			EUMarshal hashStringPtr = null;
			EUMarshal hashBinaryPtr = null;

			try
			{
				int error;

				hashStringPtr = new EUMarshal();
				hashBinaryPtr = new EUMarshal();

				if (hashString != null)
					hashStringPtr = new EUMarshal(false);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(true);

				error = (int) EUHashDataEnd(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr,
					hashBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (hashString != null)
					hashString = hashStringPtr.GetStringData();
				else if (hashBinary != null)
					hashBinary = hashBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashStringPtr != null)
					hashStringPtr.FreeData();
				if (hashBinaryPtr != null)
					hashBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _HashFile(string fileName, 
			ref string hashString, ref byte[] hashBinary)
		{
			EUMarshal hashStringPtr = null;
			EUMarshal hashBinaryPtr = null;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				hashStringPtr = new EUMarshal();
				hashBinaryPtr = new EUMarshal();

				if (hashString != null)
					hashStringPtr = new EUMarshal(false);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(true);

				error = (int) EUHashFile(fileNamePtr.DataPtr,
					hashStringPtr.DataPtr, hashBinaryPtr.DataPtr,
					hashBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (hashString != null)
					hashString = hashStringPtr.GetStringData();
				else if (hashBinary != null)
					hashBinary = hashBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashStringPtr != null)
					hashStringPtr.FreeData();
				if (hashBinaryPtr != null)
					hashBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _HashDataWithParams(
			byte[] certificate, string dataString,
			byte[] dataBinary, ref string hashString,
			ref byte[] hashBinary)
		{
			EUMarshal hashStringPtr = null;
			EUMarshal hashBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal certificatePtr = new EUMarshal(certificate);

				hashStringPtr = new EUMarshal();
				hashBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (hashString != null)
					hashStringPtr = new EUMarshal(false);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(true);

				error = (int) EUHashDataWithParams(certificatePtr.DataPtr,
					certificatePtr.DataLength, dataPtr.DataPtr,
					dataPtr.DataLength, hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (hashString != null)
					hashString = hashStringPtr.GetStringData();
				else if (hashBinary != null)
					hashBinary = hashBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashStringPtr != null)
					hashStringPtr.FreeData();
				if (hashBinaryPtr != null)
					hashBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _HashDataBeginWithParams(
			byte[] certificate)
		{
			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal(certificate);

				error = (int) EUHashDataBeginWithParams(certificatePtr.DataPtr,
					certificatePtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _HashFileWithParams(
			byte[] certificate, string fileName,
			ref string hashString, ref byte[] hashBinary)
		{
			EUMarshal hashStringPtr = null;
			EUMarshal hashBinaryPtr = null;

			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal(certificate);
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				hashStringPtr = new EUMarshal();
				hashBinaryPtr = new EUMarshal();

				if (hashString != null)
					hashStringPtr = new EUMarshal(false);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(true);

				error = (int) EUHashFileWithParams(certificatePtr.DataPtr,
					certificatePtr.DataLength, fileNamePtr.DataPtr,
					hashStringPtr.DataPtr, hashBinaryPtr.DataPtr,
					hashBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (hashString != null)
					hashString = hashStringPtr.GetStringData();
				else if (hashBinary != null)
					hashBinary = hashBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashStringPtr != null)
					hashStringPtr.FreeData();
				if (hashBinaryPtr != null)
					hashBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _HashDataBeginWithParamsCtx(
			byte[] certificate, out IntPtr context)
		{
			int error;
			context = IntPtr.Zero;

			try
			{
				EUMarshal certificatePtr = new EUMarshal(certificate);
				EUMarshal contextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				error = (int) EUHashDataBeginWithParamsCtx(
					certificatePtr.DataPtr, certificatePtr.DataLength,
					contextPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				context = contextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _HashDataContinueCtx(ref IntPtr context,
			byte[] dataBinary, string dataString)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal contextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				Marshal.WriteIntPtr(contextPtr.DataPtr, context);

				error = (int) EUHashDataContinueCtx(contextPtr.DataPtr,
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;

				if (context == IntPtr.Zero)
					context = contextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _HashDataEndCtx(IntPtr context,
			ref string hashString, ref byte[] hashBinary)
		{
			EUMarshal hashStringPtr = null;
			EUMarshal hashBinaryPtr = null;

			try
			{
				int error;

				hashStringPtr = new EUMarshal();
				hashBinaryPtr = new EUMarshal();

				if (hashString != null)
					hashStringPtr = new EUMarshal(false);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(true);

				error = (int) EUHashDataEndCtx(context, 
					hashStringPtr.DataPtr, hashBinaryPtr.DataPtr,
					hashBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (hashString != null)
					hashString = hashStringPtr.GetStringData();
				else if (hashBinary != null)
					hashBinary = hashBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashBinaryPtr != null)
					hashBinaryPtr.FreeData();
				if (hashStringPtr != null)
					hashStringPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxHashData(
			IntPtr context, int hashAlgo, byte[] certificate,
			string dataString, byte[] dataBinary, out byte[] hash)
		{
			EUMarshal hashPtr = null;

			hash = null;

			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal();
				EUMarshal dataPtr = new EUMarshal();

				hashPtr = new EUMarshal(true, context);

				if (certificate != null)
					certificatePtr = new EUMarshal(certificate);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int)EUCtxHashData(context, (DWORD)hashAlgo,
					certificatePtr.DataPtr, certificatePtr.DataLength,
					dataPtr.DataPtr, dataPtr.DataLength,
					hashPtr.DataPtr, hashPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				hash = hashPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashPtr != null)
					hashPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxHashFile(
			IntPtr context, int hashAlgo,
			byte[] certificate, string fileName,
			out byte[] hash)
		{
			EUMarshal hashPtr = null;

			hash = null;

			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal();
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				if (certificate != null)
					certificatePtr = new EUMarshal(certificate);

				hashPtr = new EUMarshal(true, context);

				error = (int) EUCtxHashFile(
					context, (DWORD)hashAlgo, certificatePtr.DataPtr,
					certificatePtr.DataLength, fileNamePtr.DataPtr,
					hashPtr.DataPtr, hashPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				hash = hashPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashPtr != null)
					hashPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxHashDataBegin(
			IntPtr context, int hashAlgo,
			byte[] certificate, out IntPtr hashContext)
		{
			hashContext = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal certificatePtr = new EUMarshal();
				EUMarshal hashContextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				if (certificate != null)
					certificatePtr = new EUMarshal(certificate);

				error = (int) EUCtxHashDataBegin(context,
					(DWORD)hashAlgo, certificatePtr.DataPtr,
					certificatePtr.DataLength,
					hashContextPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				hashContext = hashContextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxHashDataContinue(
			IntPtr hashContext, string dataString, byte[] dataBinary)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUCtxHashDataContinue(hashContext,
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxHashDataEnd(
			IntPtr hashContext, out byte[] hash)
		{
			EUMarshal hashPtr = null;

			hash = null;

			try
			{
				int error;

				hashPtr = new EUMarshal(true, hashContext);

				error = (int) EUCtxHashDataEnd(hashContext,
					hashPtr.DataPtr, hashPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				hash = hashPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashPtr != null)
					hashPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxFreeHash(
			IntPtr hashContext)
		{
			try
			{
				EUCtxFreeHash(hashContext);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: Sign functions

		private static int _ShowSignInfo(EU_SIGN_INFO signInfo)
		{
			try
			{
				if (!signInfo.filled)
					return EU_ERROR_NONE;

				EUShowSignInfo(signInfo.intSignInfo);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static void _FreeSignInfo(
			EUMarshal signInfoPtr, IntPtr context = new IntPtr())
		{
			try
			{
				if (signInfoPtr.DataPtr == IntPtr.Zero)
					return;

				if (context != IntPtr.Zero)
					EUCtxFreeSignInfo(context, signInfoPtr.DataPtr);
				else
					EUFreeSignInfo(signInfoPtr.DataPtr);
				signInfoPtr.FreeData();
			}
			catch (Exception)
			{
			}
		}

		private static int _FreeSignInfo(
			ref EU_SIGN_INFO signInfo, IntPtr context = new IntPtr())
		{
			try
			{
				if (!signInfo.filled || signInfo.intSignInfo == IntPtr.Zero)
					return EU_ERROR_NONE;

				_FreeSignInfo(signInfo.signInfoPtr, context);

				signInfo.filled = false;
				signInfo.intSignInfo = IntPtr.Zero;
				signInfo.signInfoPtr = null;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsSignedData(
			byte[] data, out bool isSigned)
		{
			isSigned = false;

			try
			{
				EUMarshal dataPtr = new EUMarshal(data);

				isSigned = EUIsSignedData(
					dataPtr.DataPtr, dataPtr.DataLength) != 0;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsSignedFile(
			string fileName, out bool isSigned)
		{
			isSigned = false;

			try
			{
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				isSigned = (EUIsSignedFile(fileNamePtr.DataPtr) != 0);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetSignsCount(string signString,
			byte[] signBinary, out int count)
		{
			count = 0;

			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();
				EUMarshal countPtr = new EUMarshal(EUMarshal.INT_SIZE);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUGetSignsCount(signStringPtr.DataPtr,
					signBinaryPtr.DataPtr, signBinaryPtr.DataLength,
					countPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				count = countPtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetSignerInfo(
			int signIndex, string signString, byte[] signBinary,
			out EU_CERT_INFO_EX info, ref byte[] certificate)
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;

			info = new EU_CERT_INFO_EX();

			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);
				certificatePtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				if (certificate != null)
					certificatePtr = new EUMarshal(true);

				error = (int)EUGetSignerInfo((DWORD)signIndex,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, infoPtr.DataPtr,
					certificatePtr.DataPtr,
					certificatePtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());
				if (certificate != null)
					certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _FreeTimeInfo(EUMarshal timeInfo)
		{
			if (timeInfo == null)
				return EU_ERROR_NONE;

			try
			{
				if (timeInfo.DataPtr == IntPtr.Zero)
					return EU_ERROR_NONE;

				IntPtr intInfo = timeInfo.GetPointerData(false);
				EUFreeTimeInfo(intInfo);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetSignTimeInfo(
			int signIndex, string signString, byte[] signBinary,
			out EU_TIME_INFO info)
		{
			EUMarshal infoPtr = null;

			info = new EU_TIME_INFO();

			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int)EUGetSignTimeInfo((DWORD)signIndex,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, infoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_TIME_INFO(infoPtr.GetPointerData());
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeTimeInfo(infoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _GetFileSignsCount(
			string fileNameWithSign, out int count)
		{
			int error;

			count = 0;

			try
			{
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);
				EUMarshal countPtr =
					new EUMarshal(EUMarshal.INT_SIZE);

				error = (int) EUGetFileSignsCount(
					fileNameWithSignPtr.DataPtr,
					countPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				count = countPtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetFileSignerInfo(
			int signIndex, string fileNameWithSign,
			out EU_CERT_INFO_EX info, ref byte[] certificate)
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;

			info = new EU_CERT_INFO_EX();

			try
			{
				int error;
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);

				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);

				certificatePtr = new EUMarshal();

				if (certificate != null)
					certificatePtr = new EUMarshal(true);

				error = (int)EUGetFileSignerInfo((DWORD)signIndex,
					fileNameWithSignPtr.DataPtr, 
					infoPtr.DataPtr, certificatePtr.DataPtr,
					certificatePtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());
				if (certificate != null)
					certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetFileSignTimeInfo(
			int signIndex, string fileNameWithSign,
			out EU_TIME_INFO info)
		{
			EUMarshal infoPtr = null;

			info = new EU_TIME_INFO();

			try
			{
				int error;
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);

				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);

				error = (int)EUGetFileSignTimeInfo((DWORD)signIndex,
					fileNameWithSignPtr.DataPtr,
					infoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_TIME_INFO(infoPtr.GetPointerData());
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeTimeInfo(infoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _GetDataHashFromSignedData(int signIndex,
			string signString, byte[] signBinary, 
			ref string hashString, ref byte[] hashBinary)
		{
			EUMarshal hashStringPtr = null;
			EUMarshal hashBinaryPtr = null;

			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				hashStringPtr = new EUMarshal();
				hashBinaryPtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				if (hashString != null)
					hashStringPtr = new EUMarshal(false);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(true);

				error = (int)EUGetDataHashFromSignedData((DWORD) signIndex, 
					signStringPtr.DataPtr, signBinaryPtr.DataPtr, 
					signBinaryPtr.DataLength, hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (hashString != null)
					hashString = hashStringPtr.GetStringData();
				else if (hashBinary != null)
					hashBinary = hashBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashStringPtr != null)
					hashStringPtr.FreeData();
				if (hashBinaryPtr != null)
					hashBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetDataHashFromSignedFile(int signIndex,
			string fileNameWithSignedData,
			ref string hashString, ref byte[] hashBinary)
		{
			EUMarshal hashStringPtr = null;
			EUMarshal hashBinaryPtr = null;

			try
			{
				int error;
				EUMarshal fileNameWithSignedDataPtr = 
					new EUMarshal(fileNameWithSignedData);

				hashStringPtr = new EUMarshal();
				hashBinaryPtr = new EUMarshal();

				if (hashStringPtr != null)
					hashStringPtr = new EUMarshal(false);
				else if (hashBinaryPtr != null)
					hashBinaryPtr = new EUMarshal(true);

				error = (int)EUGetDataHashFromSignedFile((DWORD)signIndex,
					fileNameWithSignedDataPtr.DataPtr, hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (hashString != null)
					hashString = hashStringPtr.GetStringData();
				else if (hashBinary != null)
					hashBinary = hashBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (hashStringPtr != null)
					hashStringPtr.FreeData();
				if (hashBinaryPtr != null)
					hashBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _SignData(
			string dataString, byte[] dataBinary,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;
				EUMarshal data = new EUMarshal();

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (dataString != null)
					data = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					data = new EUMarshal(dataBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUSignData(
					data.DataPtr, data.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyData(string dataString,
			byte[] dataBinary, string signString, 
			byte[] signBinary, out EU_SIGN_INFO signInfo)
		{
			EUMarshal dataPtr = null;
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				dataPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUVerifyData(dataPtr.DataPtr, dataPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataOnTimeEx(
			string dataString, byte[] dataBinary, int signIndex,
			string signString, byte[] signBinary,
			string onTimeString, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			SYSTEMTIME onTime = new SYSTEMTIME();
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			if (onTimeString != null)
			{
				if (!EUMarshal.StringToSystemTime(
						onTimeString, out onTime))
				{
					return EU_ERROR_BAD_PARAMETER;
				}
			}

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();
				EUMarshal onTimePtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				if (onTimeString != null)
					onTimePtr = new EUMarshal(onTime);

				error = (int) EUVerifyDataOnTimeEx(
					dataPtr.DataPtr, dataPtr.DataLength, (DWORD)signIndex,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, onTimePtr.DataPtr, 
					offline ? 1 : 0, noCRL ? 1 : 0, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataWithParams(
			string dataString, byte[] dataBinary, int signIndex,
			string signString, byte[] signBinary,
			string onTimeString, bool offline, bool noCRL,
			byte[] signerCert, bool noSignerCertCheck,
			out EU_SIGN_INFO signInfo)
		{
			SYSTEMTIME onTime = new SYSTEMTIME();
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			if (onTimeString != null)
			{
				if (!EUMarshal.StringToSystemTime(
						onTimeString, out onTime))
				{
					return EU_ERROR_BAD_PARAMETER;
				}
			}

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();
				EUMarshal onTimePtr = new EUMarshal();
				EUMarshal signerCertPtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				if (onTimeString != null)
					onTimePtr = new EUMarshal(onTime);

				if (signerCert != null)
					signerCertPtr = new EUMarshal(signerCert);

				error = (int)EUVerifyDataWithParams(
					dataPtr.DataPtr, dataPtr.DataLength, (DWORD)signIndex,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, onTimePtr.DataPtr,
					offline ? 1 : 0, noCRL ? 1 : 0,
					signerCertPtr.DataPtr, signerCertPtr.DataLength,
					noSignerCertCheck ? 1 : 0,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataContinue(
			string dataString, byte[] dataBinary)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUSignDataContinue(
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataEnd(
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUSignDataEnd(signStringPtr.DataPtr,
					signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataBegin(
			string signString, byte[] signBinary)
		{
			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUVerifyDataBegin(signStringPtr.DataPtr,
					signBinaryPtr.DataPtr, signBinaryPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataOnTimeBeginEx(
			int signIndex, string signString, byte[] signBinary,
			string onTimeString, bool offline, bool noCRL)
		{
			SYSTEMTIME onTime = new SYSTEMTIME();

			if (onTimeString != null)
			{
				if (!EUMarshal.StringToSystemTime(
						onTimeString, out onTime))
				{
					return EU_ERROR_BAD_PARAMETER;
				}
			}

			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();
				EUMarshal onTimePtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				if (onTimeString != null)
					onTimePtr = new EUMarshal(onTime);

				error = (int)EUVerifyDataOnTimeBeginEx((DWORD)signIndex, 
					signStringPtr.DataPtr, signBinaryPtr.DataPtr, 
					signBinaryPtr.DataLength, onTimePtr.DataPtr, 
					offline ? 1 : 0, noCRL ? 1 : 0);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataContinue(
			string dataString, byte[] dataBinary)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUVerifyDataContinue(
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataEnd(
			out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EUVerifyDataEnd(signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _SignFile(string fileName,
			string fileNameWithSign, bool externalSign)
		{
			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);

				error = (int) EUSignFile(fileNamePtr.DataPtr,
					fileNameWithSignPtr.DataPtr, externalSign ? 1 : 0);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyFile(string fileNameWithSign,
			string fileName, out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EUVerifyFile(fileNameWithSignPtr.DataPtr,
					fileNamePtr.DataPtr, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyFileOnTimeEx(
			int signIndex, string fileNameWithSign,
			string fileName, string onTimeString, 
			bool offline, bool noCRL, out EU_SIGN_INFO signInfo)
		{
			SYSTEMTIME onTime = new SYSTEMTIME();
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			if (onTimeString != null)
			{
				if (!EUMarshal.StringToSystemTime(
						onTimeString, out onTime))
				{
					return EU_ERROR_BAD_PARAMETER;
				}
			}

			try
			{
				int error;
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal onTimePtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (onTimeString != null)
					onTimePtr = new EUMarshal(onTime);

				error = (int)EUVerifyFileOnTimeEx((DWORD)signIndex,
					fileNameWithSignPtr.DataPtr,
					fileNamePtr.DataPtr, onTimePtr.DataPtr,
					offline ? 1 : 0, noCRL ? 1 : 0,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataInternal(bool appendCert,
			string dataString, byte[] dataBinary,
			ref string signedDataString, ref byte[] signedDataBinary)
		{
			EUMarshal signedDataStringPtr = null;
			EUMarshal signedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal data = new EUMarshal();

				signedDataStringPtr = new EUMarshal();
				signedDataBinaryPtr = new EUMarshal();

				if (dataString != null)
					data = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					data = new EUMarshal(dataBinary);

				if (signedDataString != null)
					signedDataStringPtr = new EUMarshal(false);
				else if (signedDataBinary != null)
					signedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUSignDataInternal(appendCert ? 1 : 0,
					data.DataPtr, data.DataLength,
					signedDataStringPtr.DataPtr,
					signedDataBinaryPtr.DataPtr,
					signedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signedDataString != null)
				{
					signedDataString =
						signedDataStringPtr.GetStringData();
				}
				else if (signedDataBinary != null)
				{
					signedDataBinary =
						signedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signedDataStringPtr != null)
					signedDataStringPtr.FreeData();
				if (signedDataBinaryPtr != null)
					signedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataInternal(
			string signedDataString, byte[] signedDataBinary,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			EUMarshal signedDataStringPtr = null;
			EUMarshal signedDataBinaryPtr = null;
			EUMarshal dataPtr = null;
			EUMarshal signInfoPtr = null;

			data = null;
			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				signedDataStringPtr = new EUMarshal();
				signedDataBinaryPtr = new EUMarshal();

				dataPtr = new EUMarshal(true);
				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (signedDataString != null)
					signedDataStringPtr = new EUMarshal(signedDataString);
				else if (signedDataBinary != null)
					signedDataBinaryPtr = new EUMarshal(signedDataBinary);

				error = (int) EUVerifyDataInternal(signedDataStringPtr.DataPtr,
					signedDataBinaryPtr.DataPtr,
					signedDataBinaryPtr.DataLength,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr, 
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signedDataStringPtr != null)
					signedDataStringPtr.FreeData();
				if (signedDataBinaryPtr != null)
					signedDataBinaryPtr.FreeData();
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataInternalOnTimeEx(
			int signIndex, string signedDataString, byte[] signedDataBinary,
			string onTimeString, bool offline, bool noCRL,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			SYSTEMTIME onTime = new SYSTEMTIME();
			EUMarshal dataPtr = null;
			EUMarshal signInfoPtr = null;

			data = null;
			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			if (onTimeString != null)
			{
				if (!EUMarshal.StringToSystemTime(
						onTimeString, out onTime))
				{
					return EU_ERROR_BAD_PARAMETER;
				}
			}

			try
			{
				int error;
				EUMarshal signedDataStringPtr = new EUMarshal();
				EUMarshal signedDataBinaryPtr = new EUMarshal();
				EUMarshal onTimePtr = new EUMarshal();

				dataPtr = new EUMarshal(true);
				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (signedDataString != null)
					signedDataStringPtr = new EUMarshal(signedDataString);
				else if (signedDataBinary != null)
					signedDataBinaryPtr = new EUMarshal(signedDataBinary);

				if (onTimeString != null)
					onTimePtr = new EUMarshal(onTime);

				error = (int) EUVerifyDataInternalOnTimeEx(
					(DWORD)signIndex, signedDataStringPtr.DataPtr,
					signedDataBinaryPtr.DataPtr,
					signedDataBinaryPtr.DataLength,
					onTimePtr.DataPtr, offline ? 1 : 0, noCRL ? 1 : 0,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataInternalWithParams(
			int signIndex, string signedDataString, byte[] signedDataBinary,
			string onTimeString, bool offline, bool noCRL,
			byte[] signerCert, bool noSignerCertCheck,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			SYSTEMTIME onTime = new SYSTEMTIME();
			EUMarshal dataPtr = null;
			EUMarshal signInfoPtr = null;

			data = null;
			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			if (onTimeString != null)
			{
				if (!EUMarshal.StringToSystemTime(
						onTimeString, out onTime))
				{
					return EU_ERROR_BAD_PARAMETER;
				}
			}

			try
			{
				int error;
				EUMarshal signedDataStringPtr = new EUMarshal();
				EUMarshal signedDataBinaryPtr = new EUMarshal();
				EUMarshal onTimePtr = new EUMarshal();
				EUMarshal signerCertPtr = new EUMarshal();

				dataPtr = new EUMarshal(true);
				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (signedDataString != null)
					signedDataStringPtr = new EUMarshal(signedDataString);
				else if (signedDataBinary != null)
					signedDataBinaryPtr = new EUMarshal(signedDataBinary);

				if (onTimeString != null)
					onTimePtr = new EUMarshal(onTime);

				if (signerCert != null)
					signerCertPtr = new EUMarshal(signerCert);

				error = (int)EUVerifyDataInternalWithParams(
					(DWORD)signIndex, signedDataStringPtr.DataPtr,
					signedDataBinaryPtr.DataPtr,
					signedDataBinaryPtr.DataLength,
					onTimePtr.DataPtr, offline ? 1 : 0, noCRL ? 1 : 0,
					signerCertPtr.DataPtr, signerCertPtr.DataLength,
					noSignerCertCheck ? 1 : 0,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _SignHash(
			string hashString, byte[] hashBinary,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;
				EUMarshal hashStringPtr = new EUMarshal();
				EUMarshal hashBinaryPtr = new EUMarshal();

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (hashString != null)
					hashStringPtr = new EUMarshal(hashString);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(hashBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUSignHash(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyHash(string hashString,
			byte[] hashBinary, string signString, byte[] signBinary,
			out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal hashStringPtr = new EUMarshal();
				EUMarshal hashBinaryPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (hashString != null)
					hashStringPtr = new EUMarshal(hashString);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(hashBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUVerifyHash(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyHashOnTimeEx(
			string hashString, byte[] hashBinary, int signIndex,
			string signString, byte[] signBinary, 
			string onTimeString, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			SYSTEMTIME onTime = new SYSTEMTIME();
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			if (onTimeString != null)
			{
				if (!EUMarshal.StringToSystemTime(
						onTimeString, out onTime))
				{
					return EU_ERROR_BAD_PARAMETER;
				}
			}

			try
			{
				int error;
				EUMarshal hashStringPtr = new EUMarshal();
				EUMarshal hashBinaryPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();
				EUMarshal onTimePtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (hashString != null)
					hashStringPtr = new EUMarshal(hashString);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(hashBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				if (onTimeString != null)
					onTimePtr = new EUMarshal(onTime);

				error = (int) EUVerifyHashOnTimeEx(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.DataLength,
					(DWORD)signIndex, signStringPtr.DataPtr, 
					signBinaryPtr.DataPtr, signBinaryPtr.DataLength,
					onTimePtr.DataPtr, offline ? 1 : 0, noCRL ? 1 : 0,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _RawSignData(
			string dataString, byte[] dataBinary,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;
			EUMarshal dataPtr = null;

			try
			{
				int error;
				dataPtr = new EUMarshal();

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EURawSignData(
					dataPtr.DataPtr, dataPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _RawVerifyData(
			string dataString, byte[] dataBinary,
			string signString, byte[] signBinary,
			out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;
			EUMarshal dataPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				dataPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EURawVerifyData(
					dataPtr.DataPtr, dataPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _RawVerifyDataEx(
			byte[] certBinary, string dataString,
			byte[] dataBinary, string signString,
			byte[] signBinary, out EU_SIGN_INFO signInfo)
		{
			EUMarshal dataPtr = null;
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal certPtr = new EUMarshal();
				dataPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				certPtr = new EUMarshal(certBinary);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EURawVerifyDataEx(
					certPtr.DataPtr, certPtr.DataLength,
					dataPtr.DataPtr, dataPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _RawSignHash(
			string hashString, byte[] hashBinary,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;
				EUMarshal hashStringPtr = new EUMarshal();
				EUMarshal hashBinaryPtr = new EUMarshal();

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (hashString != null)
					hashStringPtr = new EUMarshal(hashString);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(hashBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EURawSignHash(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		public static int _RawVerifyHash(string hashString,
			byte[] hashBinary, string signString, byte[] signBinary,
			out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal hashStringPtr = new EUMarshal();
				EUMarshal hashBinaryPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (hashString != null)
					hashStringPtr = new EUMarshal(hashString);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(hashBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EURawVerifyHash(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _RawSignFile(
			string fileName, string fileNameWithSign)
		{
			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);

				error = (int) EURawSignFile(fileNamePtr.DataPtr,
					fileNameWithSignPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _RawVerifyFile(string fileNameWithSign,
			string fileName, out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EURawVerifyFile(fileNameWithSignPtr.DataPtr,
					fileNamePtr.DataPtr, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _IsAlreadySigned(string signString, 
			byte[] signBinary, out bool isAlreadySigned)
		{
			isAlreadySigned = false;

			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();
				EUMarshal isAlreadySignedPtr = 
					new EUMarshal(EUMarshal.INT_SIZE);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUIsAlreadySigned(signStringPtr.DataPtr,
					signBinaryPtr.DataPtr, signBinaryPtr.DataLength,
					isAlreadySignedPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				isAlreadySigned = isAlreadySignedPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsFileAlreadySigned(
			string fileNameWithSign, out bool isAlreadySigned)
		{
			isAlreadySigned = false;

			try
			{
				int error;
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);
				EUMarshal isAlreadySignedPtr =
					new EUMarshal(EUMarshal.INT_SIZE);

				error = (int) EUIsFileAlreadySigned(fileNameWithSignPtr.DataPtr,
					isAlreadySignedPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				isAlreadySigned = isAlreadySignedPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _AppendSign(
			string dataString, byte[] dataBinary,
			string previousSignString, byte[] previousSignBinary,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal previousSignStringPtr = new EUMarshal();
				EUMarshal previousSignBinaryPtr = new EUMarshal();

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (previousSignString != null)
				{
					previousSignStringPtr =
						new EUMarshal(previousSignString);
				}
				else if (previousSignBinary != null)
				{
					previousSignBinaryPtr =
						new EUMarshal(previousSignBinary);
				}

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUAppendSign(
					dataPtr.DataPtr, dataPtr.DataLength,
					previousSignStringPtr.DataPtr,
					previousSignBinaryPtr.DataPtr,
					previousSignBinaryPtr.DataLength,
					signStringPtr.DataPtr,
					signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _AppendSignInternal(bool appendCertificate,
			string previousSignedDataString, byte[] previousSignedDataBinary,
			ref string signedDataString, ref byte[] signedDataBinary)
		{
			EUMarshal signedDataStringPtr = null;
			EUMarshal signedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal previousSignedDataStringPtr = new EUMarshal();
				EUMarshal previousSignedDataBinaryPtr = new EUMarshal();

				signedDataStringPtr = new EUMarshal();
				signedDataBinaryPtr = new EUMarshal();

				if (previousSignedDataString != null)
				{
					previousSignedDataStringPtr =
						new EUMarshal(previousSignedDataString);
				}
				else if (previousSignedDataBinary != null)
				{
					previousSignedDataBinaryPtr =
						new EUMarshal(previousSignedDataBinary);
				}

				if (signedDataString != null)
					signedDataStringPtr = new EUMarshal(false);
				else if (signedDataBinary != null)
					signedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUAppendSignInternal(appendCertificate ? 1 : 0,
					previousSignedDataStringPtr.DataPtr,
					previousSignedDataBinaryPtr.DataPtr,
					previousSignedDataBinaryPtr.DataLength,
					signedDataStringPtr.DataPtr, signedDataBinaryPtr.DataPtr,
					signedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signedDataString != null)
					signedDataString = signedDataStringPtr.GetStringData();
				else if (signedDataBinary != null)
					signedDataBinary = signedDataBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signedDataStringPtr != null)
					signedDataStringPtr.FreeData();
				if (signedDataBinaryPtr != null)
					signedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _RemoveSign(int signIndex,
			string previousSignedDataString, byte[] previousSignedDataBinary,
			ref string signedDataString, ref byte[] signedDataBinary)
		{
			EUMarshal signedDataStringPtr = null;
			EUMarshal signedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal previousSignedDataStringPtr = new EUMarshal();
				EUMarshal previousSignedDataBinaryPtr = new EUMarshal();

				signedDataStringPtr = new EUMarshal();
				signedDataBinaryPtr = new EUMarshal();

				if (previousSignedDataString != null)
				{
					previousSignedDataStringPtr =
						new EUMarshal(previousSignedDataString);
				}
				else if (previousSignedDataBinary != null)
				{
					previousSignedDataBinaryPtr =
						new EUMarshal(previousSignedDataBinary);
				}

				if (signedDataString != null)
					signedDataStringPtr = new EUMarshal(false);
				else if (signedDataBinary != null)
					signedDataBinaryPtr = new EUMarshal(true);

				error = (int)EURemoveSign((DWORD)signIndex,
					previousSignedDataStringPtr.DataPtr,
					previousSignedDataBinaryPtr.DataPtr,
					previousSignedDataBinaryPtr.DataLength,
					signedDataStringPtr.DataPtr, signedDataBinaryPtr.DataPtr,
					signedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signedDataString != null)
					signedDataString = signedDataStringPtr.GetStringData();
				else if (signedDataBinary != null)
					signedDataBinary = signedDataBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signedDataStringPtr != null)
					signedDataStringPtr.FreeData();
				if (signedDataBinaryPtr != null)
					signedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _RemoveSignFile(int signIndex,
			string fileNameWithPreviousSign, string fileNameWithSign)
		{
			try
			{
				int error;
				EUMarshal fileNameWithPreviousSignPtr =
					new EUMarshal(fileNameWithPreviousSign);
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);

				error = (int)EURemoveSignFile((DWORD)signIndex,
					fileNameWithPreviousSignPtr.DataPtr,
					fileNameWithSignPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataSpecific(
			string dataString, byte[] dataBinary, int signIndex,
			string signString, byte[] signBinary, out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUVerifyDataSpecific(
					dataPtr.DataPtr, dataPtr.DataLength, (DWORD)signIndex,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataInternalSpecific(int signIndex,
			string signedDataString, byte[] signedDataBinary,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;
			EUMarshal dataPtr = null;

			data = null;
			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal signedDataStringPtr = new EUMarshal();
				EUMarshal signedDataBinaryPtr = new EUMarshal();

				dataPtr = new EUMarshal(true);
				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (signedDataString != null)
					signedDataStringPtr = new EUMarshal(signedDataString);
				else if (signedDataBinary != null)
					signedDataBinaryPtr = new EUMarshal(signedDataBinary);

				error = (int) EUVerifyDataInternalSpecific((DWORD) signIndex,
					signedDataStringPtr.DataPtr, signedDataBinaryPtr.DataPtr,
					signedDataBinaryPtr.DataLength, dataPtr.DataPtr,
					dataPtr.BinaryDataLengthPtr, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _AppendSignBegin(
			string previousSignString, byte[] previousSignBinary)
		{
			try
			{
				int error;
				EUMarshal previousSignStringPtr = new EUMarshal();
				EUMarshal previousSignBinaryPtr = new EUMarshal();

				if (previousSignString != null)
				{
					previousSignStringPtr =
						new EUMarshal(previousSignString);
				}
				else if (previousSignBinary != null)
				{
					previousSignBinaryPtr =
						new EUMarshal(previousSignBinary);
				}

				error = (int) EUAppendSignBegin(
					previousSignStringPtr.DataPtr,
					previousSignBinaryPtr.DataPtr,
					previousSignBinaryPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataSpecificBegin(
			int signIndex, string signString, byte[] signBinary)
		{
			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUVerifyDataSpecificBegin((DWORD) signIndex,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _AppendSignFile(string fileName,
			string fileNameWithPreviousSign, string fileNameWithSign,
			bool externalSign)
		{
			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal fileNameWithPreviousSignPtr =
					new EUMarshal(fileNameWithPreviousSign);
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);

				error = (int) EUAppendSignFile(fileNamePtr.DataPtr,
					fileNameWithPreviousSignPtr.DataPtr,
					fileNameWithSignPtr.DataPtr, externalSign ? 1 : 0);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyFileSpecific(int signIndex,
			string fileNameWithSign, string fileName,
			out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EUVerifyFileSpecific((DWORD) signIndex,
					fileNameWithSignPtr.DataPtr,
					fileNamePtr.DataPtr, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _AppendSignHash(
			string hashString, byte[] hashBinary,
			string previousSignString, byte[] previousSignBinary,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;
				EUMarshal hashStringPtr = new EUMarshal();
				EUMarshal hashBinaryPtr = new EUMarshal();
				EUMarshal previousSignStringPtr = new EUMarshal();
				EUMarshal previousSignBinaryPtr = new EUMarshal();

				if (hashString != null)
					hashStringPtr = new EUMarshal(hashString);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(hashBinary);

				if (previousSignString != null)
				{
					previousSignStringPtr =
						new EUMarshal(previousSignString);
				}
				else if (previousSignBinary != null)
				{
					previousSignBinaryPtr =
						new EUMarshal(previousSignBinary);
				}

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUAppendSignHash(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.DataLength,
					previousSignStringPtr.DataPtr,
					previousSignBinaryPtr.DataPtr,
					previousSignBinaryPtr.DataLength,
					signStringPtr.DataPtr,
					signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyHashSpecific(
			string hashString, byte[] hashBinary, int signIndex,
			string signString, byte[] signBinary, out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal hashStringPtr = new EUMarshal();
				EUMarshal hashBinaryPtr = new EUMarshal();
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (hashString != null)
					hashStringPtr = new EUMarshal(hashString);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(hashBinary);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUVerifyHashSpecific(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.DataLength,
					(DWORD) signIndex, signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.DataLength, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _CreateEmptySign(byte[] data,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				if (data != null)
					dataPtr = new EUMarshal(data);

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUCreateEmptySign(dataPtr.DataPtr,
					dataPtr.DataLength, signStringPtr.DataPtr,
					signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CreateSigner(
			string hashString, byte[] hashBinary,
			ref string signerString, ref byte[] signerBinary)
		{
			EUMarshal signerStringPtr = null;
			EUMarshal signerBinaryPtr = null;

			try
			{
				int error;
				EUMarshal hashStringPtr = new EUMarshal();
				EUMarshal hashBinaryPtr = new EUMarshal();

				signerStringPtr = new EUMarshal();
				signerBinaryPtr = new EUMarshal();

				if (hashString != null)
					hashStringPtr = new EUMarshal(hashString);
				else if (hashBinary != null)
					hashBinaryPtr = new EUMarshal(hashBinary);

				if (signerString != null)
					signerStringPtr = new EUMarshal(false);
				else if (signerBinary != null)
					signerBinaryPtr = new EUMarshal(true);

				error = (int) EUCreateSigner(hashStringPtr.DataPtr,
					hashBinaryPtr.DataPtr, hashBinaryPtr.DataLength,
					signerStringPtr.DataPtr, signerBinaryPtr.DataPtr,
					signerBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signerString != null)
					signerString = signerStringPtr.GetStringData();
				else if (signerBinary != null)
					signerBinary = signerBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signerStringPtr != null)
					signerStringPtr.FreeData();
				if (signerBinaryPtr != null)
					signerBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _AppendSigner(string signerString,
			byte[] signerBinary, byte[] certificate,
			string previousSignString, byte[] previousSignBinary,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;
				EUMarshal signerStringPtr = new EUMarshal();
				EUMarshal signerBinaryPtr = new EUMarshal();
				EUMarshal certificatePtr = new EUMarshal();
				EUMarshal previousSignStringPtr = new EUMarshal();
				EUMarshal previousSignBinaryPtr = new EUMarshal();

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (signerString != null)
					signerStringPtr = new EUMarshal(signerString);
				else if (signerBinary != null)
					signerBinaryPtr = new EUMarshal(signerBinary);

				if (certificate != null)
					certificatePtr = new EUMarshal(certificate);

				if (previousSignString != null)
				{
					previousSignStringPtr =
						new EUMarshal(previousSignString);
				}
				else if (previousSignBinary != null)
				{
					previousSignBinaryPtr =
						new EUMarshal(previousSignBinary);
				}

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUAppendSigner(signerStringPtr.DataPtr,
					signerBinaryPtr.DataPtr, signerBinaryPtr.DataLength,
					certificatePtr.DataPtr, certificatePtr.DataLength,
					previousSignStringPtr.DataPtr,
					previousSignBinaryPtr.DataPtr,
					previousSignBinaryPtr.DataLength,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataContinueCtx(
			ref IntPtr context, string dataString, byte[] dataBinary)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal contextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				Marshal.WriteIntPtr(contextPtr.DataPtr, context);

				error = (int) EUSignDataContinueCtx(contextPtr.DataPtr,
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;

				if (context == IntPtr.Zero)
					context = contextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataEndCtx(IntPtr context,
			bool appendCert, ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUSignDataEndCtx(context,
					appendCert ? 1 : 0, signStringPtr.DataPtr,
					signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				else if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataBeginCtx(string signString,
			byte[] signBinary, out IntPtr context)
		{
			context = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();
				EUMarshal contextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				error = (int) EUVerifyDataBeginCtx(signStringPtr.DataPtr,
					signBinaryPtr.DataPtr, signBinaryPtr.DataLength,
					contextPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				context = contextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataContinueCtx(
			IntPtr context, string dataString, byte[] dataBinary)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUVerifyDataContinueCtx(context,
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _VerifyDataEndCtx(
			IntPtr context, out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;
				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EUVerifyDataEndCtx(context, signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataRSA(string dataString,
			byte[] dataBinary, bool appendCert, bool externalSign,
			ref string signedDataString, ref byte[] signedDataBinary)
		{
			EUMarshal signedDataStringPtr = null;
			EUMarshal signedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				signedDataStringPtr = new EUMarshal();
				signedDataBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (signedDataString != null)
					signedDataStringPtr = new EUMarshal(false);
				else if (signedDataBinary != null)
					signedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUSignDataRSA(
					dataPtr.DataPtr, dataPtr.DataLength,
					appendCert ? 1 : 0, externalSign ? 1 : 0,
					signedDataStringPtr.DataPtr,
					signedDataBinaryPtr.DataPtr,
					signedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signedDataString != null)
					signedDataString = signedDataStringPtr.GetStringData();
				else if (signedDataBinary != null)
					signedDataBinary = signedDataBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signedDataStringPtr != null)
					signedDataStringPtr.FreeData();
				if (signedDataBinaryPtr != null)
					signedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataRSAContinue(string dataString, 
			byte[] dataBinary)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUSignDataRSAContinue(dataPtr.DataPtr,
					dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataRSAEnd(bool appendCert,
			ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUSignDataRSAEnd(appendCert ? 1 : 0,
					signStringPtr.DataPtr, signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataRSAContinueCtx(
			ref IntPtr context, string dataString, byte[] dataBinary)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal contextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				Marshal.WriteIntPtr(contextPtr.DataPtr, context);

				error = (int) EUSignDataRSAContinueCtx(contextPtr.DataPtr,
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;

				if (context == IntPtr.Zero)
					context = contextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SignDataRSAEndCtx(IntPtr context,
			bool appendCert, ref string signString, ref byte[] signBinary)
		{
			EUMarshal signStringPtr = null;
			EUMarshal signBinaryPtr = null;

			try
			{
				int error;

				signStringPtr = new EUMarshal();
				signBinaryPtr = new EUMarshal();

				if (signString != null)
					signStringPtr = new EUMarshal(false);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(true);

				error = (int) EUSignDataRSAEndCtx(context,
					appendCert ? 1 : 0, signStringPtr.DataPtr,
					signBinaryPtr.DataPtr,
					signBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (signString != null)
					signString = signStringPtr.GetStringData();
				else if (signBinary != null)
					signBinary = signBinaryPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signStringPtr != null)
					signStringPtr.FreeData();
				else if (signBinaryPtr != null)
					signBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _SignFileRSA(string fileName,
			string fileNameWithSign, bool externalSign)
		{
			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);

				error = (int) EUSignFileRSA(fileNamePtr.DataPtr,
					fileNameWithSignPtr.DataPtr, externalSign ? 1 : 0);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsOldFormatSign(string signString,
			byte[] signBinary, out bool oldFormatSign)
		{
			oldFormatSign = false;

			try
			{
				int error;
				EUMarshal signStringPtr = new EUMarshal();
				EUMarshal signBinaryPtr = new EUMarshal();
				EUMarshal oldFormatSignPtr =
					new EUMarshal(EUMarshal.INT_SIZE);

				if (signString != null)
					signStringPtr = new EUMarshal(signString);
				else if (signBinary != null)
					signBinaryPtr = new EUMarshal(signBinary);

				Marshal.WriteInt32(oldFormatSignPtr.DataPtr, 0);

				error = (int) EUIsOldFormatSign(signStringPtr.DataPtr,
					signBinaryPtr.DataPtr, signBinaryPtr.DataLength, 
					oldFormatSignPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				oldFormatSign = oldFormatSignPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsOldFormatSignFile(
			string fileName, out bool oldFormatSign)
		{
			oldFormatSign = false;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal oldFormatSignPtr =
					new EUMarshal(EUMarshal.INT_SIZE);

				Marshal.WriteInt32(oldFormatSignPtr.DataPtr, 0);

				error = (int) EUIsOldFormatSignFile(fileNamePtr.DataPtr,
					oldFormatSignPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				oldFormatSign = oldFormatSignPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxSignHash(
			IntPtr privateKeyContext, int signAlgo,
			IntPtr hashContext, bool appendCert,
			out byte[] sign)
		{
			EUMarshal signPtr = null;

			sign = null;

			try
			{
				int error;

				signPtr = new EUMarshal(true, privateKeyContext);

				error = (int)EUCtxSignHash(privateKeyContext, 
					(DWORD) signAlgo,
					hashContext, appendCert ? 1 : 0,
					signPtr.DataPtr, signPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sign = signPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signPtr != null)
					signPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxSignHashValue(
			IntPtr privateKeyContext, int signAlgo,
			string hashString, byte[] hashBinary,
			bool appendCert, out byte[] sign)
		{
			EUMarshal signPtr = null;

			sign = null;

			try
			{
				int error;
				EUMarshal hashPtr;

				if (hashString != null)
				{
					error = BASE64Decode(hashString, out hashBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				hashPtr = new EUMarshal(hashBinary);
				signPtr = new EUMarshal(
					true, privateKeyContext);

				error = (int) EUCtxSignHashValue(privateKeyContext,
					(DWORD) signAlgo,
					hashPtr.DataPtr, hashPtr.DataLength, appendCert ? 1 : 0,
					signPtr.DataPtr, signPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sign = signPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signPtr != null)
					signPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxSignData(
			IntPtr privateKeyContext, int signAlgo,
			string dataString, byte[] dataBinary, 
			bool external, bool appendCert,
			out byte[] sign)
		{
			EUMarshal signPtr = null;

			sign = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if(dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				signPtr = new EUMarshal(
					true, privateKeyContext);

				error = (int)EUCtxSignData(privateKeyContext,
					(DWORD) signAlgo,
					dataPtr.DataPtr, dataPtr.DataLength,
					external ? 1 : 0, appendCert ? 1 : 0,
					signPtr.DataPtr, signPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sign = signPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signPtr != null)
					signPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxSignFile(
			IntPtr privateKeyContext, int signAlgo,
			string inputFile, bool external,
			bool appendCert, string outputFile)
		{
			try
			{
				int error;
				EUMarshal inputFilePtr = new EUMarshal(inputFile);
				EUMarshal outputFilePtr = new EUMarshal(outputFile);

				error = (int) EUCtxSignFile(privateKeyContext,
					(DWORD) signAlgo,
					inputFilePtr.DataPtr, external ? 1 : 0,
					appendCert ? 1 : 0, outputFilePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxIsAlreadySigned(
			IntPtr privateKeyContext, int signAlgo,
			string signString, byte[] signBinary,
			out bool isAlreadySigned)
		{
			isAlreadySigned = false;

			try
			{
				int error;

				if (signString != null)
				{
					error = BASE64Decode(signString, out signBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal signPtr = new EUMarshal(signBinary);
				EUMarshal isAlreadySignedPtr = new EUMarshal(
					EUMarshal.INT_SIZE);

				error = (int) EUCtxIsAlreadySigned(privateKeyContext,
					(DWORD) signAlgo, signPtr.DataPtr, signPtr.DataLength,
					isAlreadySignedPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				isAlreadySigned = isAlreadySignedPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxIsFileAlreadySigned(
			IntPtr privateKeyContext, int signAlgo,
			string fileNameWithSign, out bool isAlreadySigned)
		{
			isAlreadySigned = false;

			try
			{
				int error;

				EUMarshal fileNameWithSignPtr = new EUMarshal(
					fileNameWithSign);
				EUMarshal isAlreadySignedPtr = new EUMarshal(
					EUMarshal.INT_SIZE);

				error = (int) EUCtxIsFileAlreadySigned(privateKeyContext,
					(DWORD) signAlgo, fileNameWithSignPtr.DataPtr,
					isAlreadySignedPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				isAlreadySigned = isAlreadySignedPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxAppendSignHash(
			IntPtr privateKeyContext, int signAlgo,
			IntPtr hashContext, string previousSignString,
			byte[] previousSignBinary,
			bool appendCert, out byte[] sign)
		{
			EUMarshal signPtr = null;

			sign = null;

			try
			{
				int error;

				if (previousSignString != null)
				{
					error = BASE64Decode(previousSignString, 
						out previousSignBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal previousSignPtr = new EUMarshal(
					previousSignBinary);
				signPtr = new EUMarshal(true, privateKeyContext);

				error = (int) EUCtxAppendSignHash(
					privateKeyContext, (DWORD) signAlgo, hashContext,
					previousSignPtr.DataPtr, previousSignPtr.DataLength,
					appendCert ? 1 : 0, signPtr.DataPtr,
					signPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sign = signPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signPtr != null)
					signPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxAppendSignHashValue(
			IntPtr privateKeyContext, int signAlgo,
			string hashString, byte[] hashBinary,
			string prevSignString, byte[] prevSignBinary,
			bool appendCert, out byte[] sign)
		{
			EUMarshal signPtr = null;

			sign = null;

			try
			{
				int error;

				if (hashString != null)
				{
					error = BASE64Decode(hashString, out hashBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				if (prevSignString != null)
				{
					error = BASE64Decode(prevSignString,
						out prevSignBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal hashPtr = new EUMarshal(hashBinary);
				EUMarshal prevSignPtr = new EUMarshal(
					prevSignBinary);
				signPtr = new EUMarshal(true, privateKeyContext);

				error = (int) EUCtxAppendSignHashValue(
					privateKeyContext, (DWORD) signAlgo,
					hashPtr.DataPtr, hashPtr.DataLength,
					prevSignPtr.DataPtr, prevSignPtr.DataLength,
					appendCert ? 1 : 0, signPtr.DataPtr,
					signPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sign = signPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signPtr != null)
					signPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxAppendSign(
			IntPtr privateKeyContext, int signAlgo,
			string dataString, byte[] dataBinary,
			string prevSignString, byte[] prevSignBinary,
			bool appendCert, out byte[] sign)
		{
			EUMarshal signPtr = null;

			sign = null;

			try
			{
				int error;

				if (prevSignString != null)
				{
					error = BASE64Decode(prevSignString,
						out prevSignBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal dataPtr = new EUMarshal();
				EUMarshal prevSignPtr = new EUMarshal(prevSignBinary);
				signPtr = new EUMarshal(true, privateKeyContext);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUCtxAppendSign(
					privateKeyContext, (DWORD) signAlgo,
					dataPtr.DataPtr, dataPtr.DataLength,
					prevSignPtr.DataPtr, prevSignPtr.DataLength,
					appendCert ? 1 : 0, signPtr.DataPtr,
					signPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sign = signPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signPtr != null)
					signPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxAppendSignFile(
			IntPtr privateKeyContext, int signAlgo,
			string fileName, string fileNameWithPrevSign,
			bool appendCert, string fileNameWithSign)
		{
			try
			{
				int error;

				EUMarshal fileNamePtr = new EUMarshal();
				EUMarshal fileNameWithPrevSignPtr =
					new EUMarshal(fileNameWithPrevSign);
				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);

				if (fileName != null)
					fileNamePtr = new EUMarshal(fileName);

				error = (int) EUCtxAppendSignFile(
					privateKeyContext, (DWORD) signAlgo,
					fileNamePtr.DataPtr,
					fileNameWithPrevSignPtr.DataPtr,
					appendCert ? 1 : 0, fileNameWithSignPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxCreateEmptySign(
			IntPtr context, int signAlgo,
			string dataString, byte[] dataBinary,
			byte[] certificate, out byte[] sign)
		{
			EUMarshal signPtr = null;

			sign = null;

			try
			{
				int error;

				EUMarshal dataPtr = new EUMarshal();
				EUMarshal certPtr = new EUMarshal(certificate);

				signPtr = new EUMarshal(true, context);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUCtxCreateEmptySign(
					context, (DWORD) signAlgo,
					dataPtr.DataPtr, dataPtr.DataLength,
					certPtr.DataPtr, certPtr.DataLength,
					signPtr.DataPtr, signPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sign = signPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signPtr != null)
					signPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxCreateSigner(
			IntPtr privateKeyContext, int signAlgo,
			string hashString, byte[] hashBinary,
			out byte[] signer)
		{
			EUMarshal signerPtr = null;

			signer = null;

			try
			{
				int error;

				if (hashString != null)
				{
					error = BASE64Decode(hashString,
						out hashBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal hashPtr = new EUMarshal(hashBinary);
				signerPtr = new EUMarshal(true, privateKeyContext);

				error = (int) EUCtxCreateSigner(
					privateKeyContext, (DWORD) signAlgo,
					hashPtr.DataPtr, hashPtr.DataLength,
					signerPtr.DataPtr,
					signerPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signer = signerPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signerPtr != null)
					signerPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxAppendSigner(
			IntPtr context, int signAlgo,
			string signerString, byte[] signerBinary,
			byte[] certificate, string prevSignString,
			byte[] prevSignBinary, out byte[] sign)
		{
			EUMarshal signPtr = null;

			sign = null;

			try
			{
				int error;

				if (signerString != null)
				{
					error = BASE64Decode(signerString,
						out signerBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				if (prevSignString != null)
				{
					error = BASE64Decode(prevSignString,
						out prevSignBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal signerPtr = new EUMarshal(signerBinary);
				EUMarshal prevSignPtr = 
					new EUMarshal(prevSignBinary);
				EUMarshal certificatePtr = new EUMarshal();

				if (certificate != null)
					certificatePtr = new EUMarshal(certificate);

				signPtr = new EUMarshal(true, context);

				error = (int) EUCtxAppendSigner(
					context, (DWORD) signAlgo, signerPtr.DataPtr,
					signerPtr.DataLength, certificatePtr.DataPtr,
					certificatePtr.DataLength,
					prevSignPtr.DataPtr, prevSignPtr.DataLength,
					signPtr.DataPtr, signPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sign = signPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signPtr != null)
					signPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetSignsCount(
			IntPtr context, string signString, 
			byte[] signBinary, out int count)
		{
			count = 0;

			try
			{
				int error;

				if (signString != null)
				{
					error = BASE64Decode(signString,
						out signBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal signPtr = new EUMarshal(signBinary);
				EUMarshal countPtr = new EUMarshal(
					EUMarshal.INT_SIZE);

				error = (int) EUCtxGetSignsCount(context,
					signPtr.DataPtr, signPtr.DataLength,
					countPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				count = countPtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetFileSignsCount(
			IntPtr context, string fileNameWithSign, out int count)
		{
			count = 0;

			try
			{
				int error;

				EUMarshal fileNameWithSignPtr = 
					new EUMarshal(fileNameWithSign);
				EUMarshal countPtr = new EUMarshal(
					EUMarshal.INT_SIZE);

				error = (int) EUCtxGetFileSignsCount(context,
					fileNameWithSignPtr.DataPtr, countPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				count = countPtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetSignerInfo(
			IntPtr context, int signIndex, string signString,
			byte[] signBinary, out EU_CERT_INFO_EX info,
			out byte[] certificate)
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;

			info = new EU_CERT_INFO_EX();
			certificate = null;

			try
			{
				int error;

				if (signString != null)
				{
					error = BASE64Decode(signString,
						out signBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal signPtr = new EUMarshal(signBinary);
				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);

				certificatePtr = new EUMarshal(true, context);

				error = (int) EUCtxGetSignerInfo(context, (DWORD) signIndex,
					signPtr.DataPtr, signPtr.DataLength, 
					infoPtr.DataPtr, certificatePtr.DataPtr,
					certificatePtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());
				certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr, context);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetFileSignerInfo(
			IntPtr context, int signIndex, string fileNameWithSign,
			out EU_CERT_INFO_EX info, out byte[] certificate)
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;

			info = new EU_CERT_INFO_EX();
			certificate = null;

			try
			{
				int error;

				EUMarshal fileNameWithSignPtr = 
					new EUMarshal(fileNameWithSign);
				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);
				certificatePtr = new EUMarshal(true, context);

				error = (int) EUCtxGetFileSignerInfo(context, (DWORD) signIndex,
					fileNameWithSignPtr.DataPtr, infoPtr.DataPtr,
					certificatePtr.DataPtr, 
					certificatePtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());
				certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr, context);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxIsDataInSignedDataAvailable(
			IntPtr context, string signedDataString,
			byte[] signedDataBinary, out bool available)
		{
			available = false;

			try
			{
				int error;

				if (signedDataString != null)
				{
					error = BASE64Decode(signedDataString,
						out signedDataBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal availPtr = new EUMarshal(EUMarshal.INT_SIZE);
				EUMarshal signedDataPtr = new EUMarshal(signedDataBinary);

				error = (int) EUCtxIsDataInSignedDataAvailable(context,
					signedDataPtr.DataPtr, signedDataPtr.DataLength,
					availPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				available = availPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxIsDataInSignedFileAvailable(
			IntPtr context, string fileNameWithSignedData,
			out bool available)
		{
			available = false;

			try
			{
				int error;

				EUMarshal fileNameWithSignedDataPtr =
					new EUMarshal(fileNameWithSignedData);

				EUMarshal availPtr = new EUMarshal(EUMarshal.INT_SIZE);

				error = (int) EUCtxIsDataInSignedFileAvailable(context,
					fileNameWithSignedDataPtr.DataPtr, availPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				available = availPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetDataFromSignedData(
			IntPtr context, string signedDataString,
			byte[] signedDataBinary, out byte[] data)
		{
			EUMarshal dataPtr = null;

			data = null;

			try
			{
				int error;

				if (signedDataString != null)
				{
					error = BASE64Decode(signedDataString,
						out signedDataBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal signedDataPtr = new EUMarshal(signedDataBinary);

				dataPtr = new EUMarshal(true, context);

				error = (int) EUCtxGetDataFromSignedData(context,
					signedDataPtr.DataPtr, signedDataPtr.DataLength,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxGetDataFromSignedFile(
			IntPtr context, string fileNameWithSignedData,
			string fileName)
		{
			int error;

			try
			{
				EUMarshal fileNameWithSignedDataPtr =
					new EUMarshal(fileNameWithSignedData);
				EUMarshal fileNamePtr =
					new EUMarshal(fileName);

				error = (int) EUCtxGetDataFromSignedFile(
					context, fileNameWithSignedDataPtr.DataPtr,
					fileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxVerifyHash(
			IntPtr hashContext, int signIndex,
			string signString, byte[] signBinary,
			out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;

				if (signString != null)
				{
					error = BASE64Decode(signString,
						out signBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal signPtr = new EUMarshal(signBinary);
				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EUCtxVerifyHash(hashContext, (DWORD) signIndex,
					signPtr.DataPtr, signPtr.DataLength,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
				{
					_FreeSignInfo(signInfoPtr,
						hashContext);
				}
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxVerifyHashValue(
			IntPtr context, string hashString, byte[] hashBinary,
			int signIndex, string signString, byte[] signBinary,
			out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;

				if (hashString != null)
				{
					error = BASE64Decode(hashString,
						out hashBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				if (signString != null)
				{
					error = BASE64Decode(signString,
						out signBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal hashPtr = new EUMarshal(hashBinary);
				EUMarshal signPtr = new EUMarshal(signBinary);

				signInfoPtr = new EUMarshal(
					EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EUCtxVerifyHashValue(context,
					hashPtr.DataPtr, hashPtr.DataLength, (DWORD) signIndex,
					signPtr.DataPtr, signPtr.DataLength,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr, context);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxVerifyData(
			IntPtr context, string dataString, byte[] dataBinary,
			int signIndex, string signString, byte[] signBinary,
			out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;

				if (signString != null)
				{
					error = BASE64Decode(signString,
						out signBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal dataPtr = new EUMarshal();
				EUMarshal signPtr = new EUMarshal(signBinary);

				signInfoPtr = new EUMarshal(
					EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				error = (int) EUCtxVerifyData(context, dataPtr.DataPtr,
					dataPtr.DataLength, (DWORD) signIndex,
					signPtr.DataPtr, signPtr.DataLength,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr, context);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxVerifyDataInternal(
			IntPtr context, int signIndex,
			string signString, byte[] signBinary,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			EUMarshal dataPtr = null;
			EUMarshal signInfoPtr = null;

			data = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;

				if (signString != null)
				{
					error = BASE64Decode(signString,
						out signBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal signPtr = new EUMarshal(signBinary);

				dataPtr = new EUMarshal(true, context);
				signInfoPtr = new EUMarshal(
					EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EUCtxVerifyDataInternal(context, (DWORD) signIndex,
					signPtr.DataPtr, signPtr.DataLength,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr, context);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxVerifyFile(IntPtr context,
			int signIndex, string fileNameWithSign,
			string fileName, out EU_SIGN_INFO signInfo)
		{
			EUMarshal signInfoPtr = null;

			signInfo = new EU_SIGN_INFO();
			signInfo.filled = false;
			signInfo.intSignInfo = IntPtr.Zero;

			try
			{
				int error;

				EUMarshal fileNameWithSignPtr =
					new EUMarshal(fileNameWithSign);
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				signInfoPtr = new EUMarshal(EUMarshal.EU_SIGN_INFO_SIZE);
				Marshal.WriteInt32(signInfoPtr.DataPtr, 0);

				error = (int) EUCtxVerifyFile(context, (DWORD) signIndex,
					fileNameWithSignPtr.DataPtr, fileNamePtr.DataPtr,
					signInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				signInfo = new EU_SIGN_INFO(signInfoPtr);
				if (signInfo.signInfoPtr != null)
					signInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (signInfoPtr != null)
					_FreeSignInfo(signInfoPtr, context);
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: Envelop functions

		private static int _ShowSenderInfo(EU_SENDER_INFO senderInfo)
		{
			try
			{
				if (!senderInfo.filled)
					return EU_ERROR_NONE;

				EUShowSenderInfo(senderInfo.intSenderInfo);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static void _FreeSenderInfo(EUMarshal senderInfoPtr,
			IntPtr context = new IntPtr())
		{
			try
			{
				if (senderInfoPtr.DataPtr == IntPtr.Zero)
					return;

				if (context != IntPtr.Zero)
					EUCtxFreeSenderInfo(context, senderInfoPtr.DataPtr);
				else
					EUFreeSenderInfo(senderInfoPtr.DataPtr);
				senderInfoPtr.FreeData();
			}
			catch (Exception)
			{
			}
		}

		private static int _FreeSenderInfo(
			ref EU_SENDER_INFO senderInfo, IntPtr context = new IntPtr())
		{
			try
			{
				if (!senderInfo.filled || 
						senderInfo.intSenderInfo == IntPtr.Zero)
					return EU_ERROR_NONE;

				_FreeSenderInfo(senderInfo.senderInfoPtr, context);

				senderInfo.filled = false;
				senderInfo.intSenderInfo = IntPtr.Zero;
				senderInfo.senderInfoPtr = null;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetSenderInfo(
			string envelopedDataString, byte[] envelopedDataBinary,
			byte[] recipientCert, out bool dynamicKey,
			out EU_CERT_INFO_EX info, ref byte[] certificate, IntPtr context = new IntPtr())
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;

			info = new EU_CERT_INFO_EX();
			dynamicKey = false;

			try
			{
				int error;
				EUMarshal envelopedDataStringPtr = new EUMarshal();
				EUMarshal envelopedDataBinaryPtr = new EUMarshal();
				EUMarshal recipientCertPtr = new EUMarshal(recipientCert);
				EUMarshal dynamicKeyPtr = new EUMarshal(EUMarshal.INT_SIZE);

				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);
				certificatePtr = new EUMarshal();

				if (certificate != null)
					certificatePtr = new EUMarshal(true, context);

				if (context != IntPtr.Zero)
				{
					if (envelopedDataString != null)
					{
						error = BASE64Decode(envelopedDataString,
							out envelopedDataBinary);
						if (error != EU_ERROR_NONE)
							return error;
					}

					if (envelopedDataBinary != null)
						envelopedDataBinaryPtr = new EUMarshal(envelopedDataBinary);

					error = (int) EUCtxGetSenderInfo(context,
						envelopedDataBinaryPtr.DataPtr,
						envelopedDataBinaryPtr.DataLength,
						recipientCertPtr.DataPtr, recipientCertPtr.DataLength,
						dynamicKeyPtr.DataPtr, infoPtr.DataPtr,
						certificatePtr.DataPtr,
						certificatePtr.BinaryDataLengthPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}
				else
				{
					if (envelopedDataString != null)
						envelopedDataStringPtr = new EUMarshal(envelopedDataString);
					else if (envelopedDataBinary != null)
						envelopedDataBinaryPtr = new EUMarshal(envelopedDataBinary);

					error = (int) EUGetSenderInfo(envelopedDataStringPtr.DataPtr,
						envelopedDataBinaryPtr.DataPtr, envelopedDataBinaryPtr.DataLength,
						recipientCertPtr.DataPtr, recipientCertPtr.DataLength,
						dynamicKeyPtr.DataPtr, infoPtr.DataPtr,
						certificatePtr.DataPtr,
						certificatePtr.BinaryDataLengthPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}

				dynamicKey = dynamicKeyPtr.GetBoolData();
				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());
				if (certificate != null)
					certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr, context);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetFileSenderInfo(
			string envelopedFileName, byte[] recipientCert,
			out bool dynamicKey, out EU_CERT_INFO_EX info,
			ref byte[] certificate, IntPtr context = new IntPtr())
		{
			EUMarshal infoPtr = null;
			EUMarshal certificatePtr = null;

			info = new EU_CERT_INFO_EX();
			dynamicKey = false;

			try
			{
				int error;
				EUMarshal envelopedFileNamePtr = new EUMarshal(envelopedFileName);
				EUMarshal recipientCertPtr = new EUMarshal(recipientCert);
				EUMarshal dynamicKeyPtr = new EUMarshal(EUMarshal.INT_SIZE);

				infoPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(infoPtr.DataPtr,
					IntPtr.Zero);
				certificatePtr = new EUMarshal();

				if (certificate != null)
					certificatePtr = new EUMarshal(true, context);

				if (context != IntPtr.Zero)
				{
					error = (int) EUCtxGetFileSenderInfo(context, 
						envelopedFileNamePtr.DataPtr,
						recipientCertPtr.DataPtr,
						recipientCertPtr.DataLength,
						dynamicKeyPtr.DataPtr, infoPtr.DataPtr,
						certificatePtr.DataPtr,
						certificatePtr.BinaryDataLengthPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}
				else
				{
					error = (int) EUGetFileSenderInfo(
						envelopedFileNamePtr.DataPtr,
						recipientCertPtr.DataPtr,
						recipientCertPtr.DataLength,
						dynamicKeyPtr.DataPtr, infoPtr.DataPtr,
						certificatePtr.DataPtr,
						certificatePtr.BinaryDataLengthPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}

				dynamicKey = dynamicKeyPtr.GetBoolData();
				info = new EU_CERT_INFO_EX(infoPtr.GetPointerData());
				if (certificate != null)
					certificate = certificatePtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfoEx(infoPtr, context);
				if (certificatePtr != null)
					certificatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetRecipientsCount(
			string envelopedDataString, byte[] envelopedDataBinary,
			out int count, IntPtr context = new IntPtr())
		{
			count = 0;

			try
			{
				int error;
				EUMarshal envelopedDataStringPtr = new EUMarshal();
				EUMarshal envelopedDataBinaryPtr = new EUMarshal();
				EUMarshal countPtr = new EUMarshal(EUMarshal.INT_SIZE);

				if (context != IntPtr.Zero)
				{
					if (envelopedDataString != null)
					{
						error = BASE64Decode(envelopedDataString,
							out envelopedDataBinary);
						if (error != EU_ERROR_NONE)
							return error;
					}

					if (envelopedDataBinary != null)
						envelopedDataBinaryPtr = new EUMarshal(envelopedDataBinary);

					error = (int) EUCtxGetRecipientsCount(context,
						envelopedDataBinaryPtr.DataPtr,
						envelopedDataBinaryPtr.DataLength,
						countPtr.DataPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}
				else
				{
					if (envelopedDataString != null)
						envelopedDataStringPtr = new EUMarshal(envelopedDataString);
					else if (envelopedDataBinary != null)
						envelopedDataBinaryPtr = new EUMarshal(envelopedDataBinary);

					error = (int) EUGetRecipientsCount(
						envelopedDataStringPtr.DataPtr,
						envelopedDataBinaryPtr.DataPtr,
						envelopedDataBinaryPtr.DataLength,
						countPtr.DataPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}

				count = countPtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetFileRecipientsCount(
			string envelopedFile, out int count,
			IntPtr context = new IntPtr())
		{
			int error;

			count = 0;

			try
			{
				EUMarshal envelopedFilePtr =
					new EUMarshal(envelopedFile);
				EUMarshal countPtr =
					new EUMarshal(EUMarshal.INT_SIZE);

				if (context != IntPtr.Zero)
				{
					error = (int) EUCtxGetFileRecipientsCount(
						context, envelopedFilePtr.DataPtr,
						countPtr.DataPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}
				else
				{
					error = (int) EUGetFileRecipientsCount(
						envelopedFilePtr.DataPtr,
						countPtr.DataPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}

				count = countPtr.GetIntData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _GetRecipientInfo(
			int recipientIndex, string envelopedDataString,
			byte[] envelopedDataBinary,
			out int recipientInfoType, out string recipientIssuer,
			out string recipientSerial, out string recipientKeyID,
			IntPtr context = new IntPtr())
		{
			EUMarshal recipientInfoTypePtr = null;
			EUMarshal recipientIssuerPtr = null;
			EUMarshal recipientSerialPtr = null;
			EUMarshal recipientKeyIDPtr = null;

			recipientInfoType = EU_RECIPIENT_INFO_TYPE_UNKNOWN;
			recipientIssuer = null;
			recipientSerial = null;
			recipientKeyID = null;

			try
			{
				int error;
				EUMarshal envelopedDataStringPtr = new EUMarshal();
				EUMarshal envelopedDataBinaryPtr = new EUMarshal();

				recipientInfoTypePtr = new EUMarshal(
					Marshal.SizeOf(typeof(int)));
				recipientIssuerPtr = new EUMarshal(false, context);
				recipientSerialPtr = new EUMarshal(false, context);
				recipientKeyIDPtr = new EUMarshal(false, context);

				if (context != IntPtr.Zero)
				{
					if (envelopedDataString != null)
					{
						error = BASE64Decode(envelopedDataString,
							out envelopedDataBinary);
						if (error != EU_ERROR_NONE)
							return error;
					}

					if (envelopedDataBinary != null)
						envelopedDataBinaryPtr = new EUMarshal(envelopedDataBinary);

					error = (int)EUCtxGetRecipientInfo(context, (DWORD) recipientIndex,
						envelopedDataBinaryPtr.DataPtr, envelopedDataBinaryPtr.DataLength,
						recipientInfoTypePtr.DataPtr, recipientIssuerPtr.DataPtr,
						recipientSerialPtr.DataPtr, recipientKeyIDPtr.DataPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}
				else
				{
					if (envelopedDataString != null)
						envelopedDataStringPtr = new EUMarshal(envelopedDataString);
					else if (envelopedDataBinary != null)
						envelopedDataBinaryPtr = new EUMarshal(envelopedDataBinary);

					error = (int)EUGetRecipientInfo((DWORD) recipientIndex,
						envelopedDataStringPtr.DataPtr,
						envelopedDataBinaryPtr.DataPtr, envelopedDataBinaryPtr.DataLength,
						recipientInfoTypePtr.DataPtr, recipientIssuerPtr.DataPtr,
						recipientSerialPtr.DataPtr, recipientKeyIDPtr.DataPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}

				recipientInfoType = recipientInfoTypePtr.GetIntData();
				recipientIssuer = recipientIssuerPtr.GetStringData();
				recipientSerial = recipientSerialPtr.GetStringData();
				recipientKeyID = recipientKeyIDPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (recipientIssuerPtr != null)
					recipientIssuerPtr.FreeData();
				if (recipientSerialPtr != null)
					recipientSerialPtr.FreeData();
				if (recipientKeyIDPtr != null)
					recipientKeyIDPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _GetFileRecipientInfo(
			int recipientIndex, string envelopedFileName,
			out int recipientInfoType, out string recipientIssuer,
			out string recipientSerial, out string recipientKeyID,
			IntPtr context = new IntPtr())
		{
			EUMarshal recipientInfoTypePtr = null;
			EUMarshal recipientIssuerPtr = null;
			EUMarshal recipientSerialPtr = null;
			EUMarshal recipientKeyIDPtr = null;

			recipientInfoType = EU_RECIPIENT_INFO_TYPE_UNKNOWN;
			recipientIssuer = null;
			recipientSerial = null;
			recipientKeyID = null;

			try
			{
				int error;
				EUMarshal envelopedFileNamePtr = new EUMarshal();

				recipientInfoTypePtr = new EUMarshal(
					Marshal.SizeOf(typeof(int)));
				recipientIssuerPtr = new EUMarshal(false, context);
				recipientSerialPtr = new EUMarshal(false, context);
				recipientKeyIDPtr = new EUMarshal(false, context);

				envelopedFileNamePtr = new EUMarshal(envelopedFileName);

				if (context != IntPtr.Zero)
				{
					error = (int) EUCtxGetFileRecipientInfo(context,
						(DWORD) recipientIndex, envelopedFileNamePtr.DataPtr,
						recipientInfoTypePtr.DataPtr, recipientIssuerPtr.DataPtr,
						recipientSerialPtr.DataPtr, recipientKeyIDPtr.DataPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}
				else
				{
					error = (int) EUGetFileRecipientInfo(
						(DWORD) recipientIndex, envelopedFileNamePtr.DataPtr,
						recipientInfoTypePtr.DataPtr, recipientIssuerPtr.DataPtr,
						recipientSerialPtr.DataPtr, recipientKeyIDPtr.DataPtr);
					if (error != EU_ERROR_NONE)
						return error;
				}

				recipientInfoType = recipientInfoTypePtr.GetIntData();
				recipientIssuer = recipientIssuerPtr.GetStringData();
				recipientSerial = recipientSerialPtr.GetStringData();
				recipientKeyID = recipientKeyIDPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (recipientIssuerPtr != null)
					recipientIssuerPtr.FreeData();
				if (recipientSerialPtr != null)
					recipientSerialPtr.FreeData();
				if (recipientKeyIDPtr != null)
					recipientKeyIDPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopData(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			EUMarshal dataPtr = null;
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal recipientCertIssuerPtr =
					new EUMarshal(recipientCertIssuer);
				EUMarshal recipientCertSerialPtr =
					new EUMarshal(recipientCertSerial);
				dataPtr = new EUMarshal();

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopData(recipientCertIssuerPtr.DataPtr,
					recipientCertSerialPtr.DataPtr, signData ? 1 : 0,
					dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataWithSettings(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string dataString, byte[] dataBinary,
			bool checkRecipientCertOffline, bool checkRecipientCertNoCRL, 
			bool noTSP, bool appendCert, ref string envelopedDataString,
			ref byte[] envelopedDataBinary)
		{
			EUMarshal dataPtr = null;
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal recipientCertIssuerPtr =
					new EUMarshal(recipientCertIssuer);
				EUMarshal recipientCertSerialPtr =
					new EUMarshal(recipientCertSerial);
				dataPtr = new EUMarshal();

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataWithSettings(
					recipientCertIssuerPtr.DataPtr,
					recipientCertSerialPtr.DataPtr, signData ? 1 : 0,
					dataPtr.DataPtr, dataPtr.DataLength,
					checkRecipientCertOffline ? 1 : 0, 
					checkRecipientCertNoCRL ? 1 : 0,
					noTSP ? 1 : 0, appendCert ? 1 : 0,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo, 
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal recipientCertIssuerPtr =
					new EUMarshal(recipientCertIssuer);
				EUMarshal recipientCertSerialPtr =
					new EUMarshal(recipientCertSerial);
				EUMarshal dataPtr = new EUMarshal();

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataRSA(
					(DWORD) algo,
					recipientCertIssuerPtr.DataPtr,
					recipientCertSerialPtr.DataPtr, signData ? 1 : 0,
					dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _DevelopData(
			string envelopedDataString, byte[] envelopedDataBinary,
			out byte[] data, out EU_SENDER_INFO senderInfo)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;
			EUMarshal dataPtr = null;
			EUMarshal senderInfoPtr = null;

			senderInfo = new EU_SENDER_INFO();
			senderInfo.filled = false;
			senderInfo.intSenderInfo = IntPtr.Zero;
			senderInfo.senderInfoPtr = null;

			data = null;

			try
			{
				int error;
				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				dataPtr = new EUMarshal(true);
				senderInfoPtr = new EUMarshal(EUMarshal.EU_SENDER_INFO_SIZE);
				Marshal.WriteInt32(senderInfoPtr.DataPtr, 0);

				if (envelopedDataString != null)
				{
					envelopedDataStringPtr =
						new EUMarshal(envelopedDataString);
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinaryPtr =
						new EUMarshal(envelopedDataBinary);
				}

				error = (int) EUDevelopData(envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.DataLength,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr,
					senderInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				senderInfo = new EU_SENDER_INFO(senderInfoPtr);
				if (senderInfo.senderInfoPtr != null)
					senderInfoPtr = null;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
				if (senderInfoPtr != null)
					_FreeSenderInfo(senderInfoPtr);
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFile(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string fileName, string envelopedFileName)
		{
			try
			{
				int error;
				EUMarshal recipientCertIssuerPtr =
					new EUMarshal(recipientCertIssuer);
				EUMarshal recipientCertSerialPtr =
					new EUMarshal(recipientCertSerial);
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				error = (int) EUEnvelopFile(recipientCertIssuerPtr.DataPtr,
					recipientCertSerialPtr.DataPtr, signData ? 1 : 0,
					fileNamePtr.DataPtr, envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFileRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string fileName, string envelopedFileName)
		{
			try
			{
				int error;
				EUMarshal recipientCertIssuerPtr =
					new EUMarshal(recipientCertIssuer);
				EUMarshal recipientCertSerialPtr =
					new EUMarshal(recipientCertSerial);
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				error = (int) EUEnvelopFileRSA((DWORD) algo, 
					recipientCertIssuerPtr.DataPtr,
					recipientCertSerialPtr.DataPtr, signData ? 1 : 0,
					fileNamePtr.DataPtr, envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevelopFile(string envelopedFileName,
			string fileName, out EU_SENDER_INFO senderInfo)
		{
			EUMarshal senderInfoPtr = null;

			senderInfo = new EU_SENDER_INFO();
			senderInfo.filled = false;
			senderInfo.intSenderInfo = IntPtr.Zero;
			senderInfo.senderInfoPtr = null;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				senderInfoPtr = new EUMarshal(EUMarshal.EU_SENDER_INFO_SIZE);
				Marshal.WriteInt32(senderInfoPtr.DataPtr, 0);

				error = (int) EUDevelopFile(envelopedFileNamePtr.DataPtr,
					fileNamePtr.DataPtr, senderInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				senderInfo = new EU_SENDER_INFO(senderInfoPtr);
				if (senderInfo.senderInfoPtr != null)
					senderInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (senderInfoPtr != null)
					_FreeSenderInfo(senderInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _RawEnvelopData(
			byte[] recipientCert,
			string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal recipientCertPtr = 
					new EUMarshal(recipientCert);
				EUMarshal dataPtr = new EUMarshal();

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EURawEnvelopData(
					recipientCertPtr.DataPtr, recipientCertPtr.DataLength,
					dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _RawDevelopData(
			string envelopedDataString, byte[] envelopedDataBinary,
			out byte[] data, out EU_SENDER_INFO senderInfo)
		{
			EUMarshal dataPtr = null;
			EUMarshal senderInfoPtr = null;

			senderInfo = new EU_SENDER_INFO();
			senderInfo.filled = false;
			senderInfo.intSenderInfo = IntPtr.Zero;
			senderInfo.senderInfoPtr = null;

			data = null;

			try
			{
				int error;
				EUMarshal envelopedDataStringPtr = new EUMarshal();
				EUMarshal envelopedDataBinaryPtr = new EUMarshal();

				dataPtr = new EUMarshal(true);
				senderInfoPtr = new EUMarshal(EUMarshal.EU_SENDER_INFO_SIZE);
				Marshal.WriteInt32(senderInfoPtr.DataPtr, 0);

				if (envelopedDataString != null)
				{
					envelopedDataStringPtr =
						new EUMarshal(envelopedDataString);
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinaryPtr =
						new EUMarshal(envelopedDataBinary);
				}

				error = (int) EURawDevelopData(envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.DataLength,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr,
					senderInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				senderInfo = new EU_SENDER_INFO(senderInfoPtr);
				if (senderInfo.senderInfoPtr != null)
					senderInfoPtr = null;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (senderInfoPtr != null)
					_FreeSenderInfo(senderInfoPtr);
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _IsEnvelopedData(byte[] data,
			out bool isEnvelopedData)
		{
			isEnvelopedData = false;

			try
			{
				int result;
				EUMarshal dataPtr = new EUMarshal(data);

				result = EUIsEnvelopedData(dataPtr.DataPtr,
					dataPtr.DataLength);

				isEnvelopedData = (result != 0);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _IsEnvelopedFile(string fileName,
			out bool isEnvelopedData)
		{
			isEnvelopedData = false;

			try
			{
				int result;
				EUMarshal fileNamePtr = new EUMarshal(fileName);

				result = EUIsEnvelopedFile(fileNamePtr.DataPtr);

				isEnvelopedData = (result != 0);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static void _FreeReceiversCertificates(
			EUMarshal receiversCertificates)
		{
			try
			{
				IntPtr certs = receiversCertificates.GetPointerData();
				if (certs == IntPtr.Zero)
					return;

				EUFreeReceiversCertificates(certs);
			}
			catch (Exception)
			{
			}
		}

		private static int _GetReceiversCertificates(
			out EU_CERT_INFO_EX[] certificates)
		{
			EUMarshal certificatesPtr = null;

			certificates = new EU_CERT_INFO_EX[0];

			try
			{
				int error;
				certificatesPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(certificatesPtr.DataPtr,
					IntPtr.Zero);

				error = (int) EUGetReceiversCertificates(
					certificatesPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certificates = certificatesPtr.GetCertsInfoEx();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (certificatesPtr != null)
					_FreeReceiversCertificates(certificatesPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _GetReceiversCertificatesRSA(
			out EU_CERT_INFO_EX[] certificates)
		{
			EUMarshal certificatesPtr = null;

			certificates = new EU_CERT_INFO_EX[0];

			try
			{
				int error;
				certificatesPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				Marshal.WriteIntPtr(certificatesPtr.DataPtr,
					IntPtr.Zero);

				error = (int) EUGetReceiversCertificatesRSA(
					certificatesPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certificates = certificatesPtr.GetCertsInfoEx();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (certificatesPtr != null)
					_FreeReceiversCertificates(certificatesPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataEx(
			string[] recipientsCertIssuers, string[] recipientsCertSerials,
			bool signData, string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal recipientsCertIssuersPtr = 
					new EUMarshal(recipientsCertIssuers);
				EUMarshal recipientsCertSerialsPtr =
					new EUMarshal(recipientsCertSerials);
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataEx(
					recipientsCertIssuersPtr.DataPtr,
					recipientsCertSerialsPtr.DataPtr,
					signData ? 1 : 0,
					dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataRSAEx(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string[] recipientsCertIssuers, string[] recipientsCertSerials,
			bool signData, string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal recipientsCertIssuersPtr =
					new EUMarshal(recipientsCertIssuers);
				EUMarshal recipientsCertSerialsPtr =
					new EUMarshal(recipientsCertSerials);
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataRSAEx(
					(DWORD) algo,
					recipientsCertIssuersPtr.DataPtr,
					recipientsCertSerialsPtr.DataPtr,
					signData ? 1 : 0,
					dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFileEx(
			string[] recipientsCertIssuers,
			string[] recipientsCertSerials, bool signData,
			string fileName, string envelopedFileName)
		{
			try
			{
				int error;
				EUMarshal recipientsCertIssuersPtr =
					new EUMarshal(recipientsCertIssuers);
				EUMarshal recipientsCertSerialsPtr =
					new EUMarshal(recipientsCertSerials);
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				error = (int) EUEnvelopFileEx(recipientsCertIssuersPtr.DataPtr,
					recipientsCertSerialsPtr.DataPtr, signData ? 1 : 0,
					fileNamePtr.DataPtr, envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFileRSAEx(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string[] recipientsCertIssuers,
			string[] recipientsCertSerials, bool signData,
			string fileName, string envelopedFileName)
		{
			try
			{
				int error;
				EUMarshal recipientsCertIssuersPtr =
					new EUMarshal(recipientsCertIssuers);
				EUMarshal recipientsCertSerialsPtr =
					new EUMarshal(recipientsCertSerials);
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				error = (int) EUEnvelopFileRSAEx((DWORD) algo, 
					recipientsCertIssuersPtr.DataPtr,
					recipientsCertSerialsPtr.DataPtr, signData ? 1 : 0,
					fileNamePtr.DataPtr, envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataToRecipients(
			byte[][] recipientCerts, bool signData,
			string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int)EUEnvelopDataToRecipients((DWORD) recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength,
					signData ? 1 : 0, dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);

				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataToRecipientsWithSettings(
			byte[][] recipientCerts, bool signData,
			string dataString, byte[] dataBinary,
			bool checkRecipientCertsOffline, bool checkRecipientCertsNoCRL, 
			bool noTSP, bool appendCert,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataToRecipientsWithSettings(
					(DWORD)recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength,
					signData ? 1 : 0, dataPtr.DataPtr, dataPtr.DataLength,
					checkRecipientCertsOffline ? 1 : 0, 
					checkRecipientCertsNoCRL ? 1 : 0, 
					noTSP ? 1 : 0, appendCert ? 1 : 0,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);

				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataToRecipientsRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			byte[][] recipientCerts, bool signData,
			string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataToRecipientsRSA((DWORD) algo,
					(DWORD) recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength,
					signData ? 1 : 0, dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);

				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFileToRecipients(
			byte[][] recipientCerts, bool signData,
			string fileName, string envelopedFileName)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				error = (int)EUEnvelopFileToRecipients((DWORD) recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength,
					signData ? 1 : 0, fileNamePtr.DataPtr,
					envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFileToRecipientsRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			byte[][] recipientCerts, bool signData,
			string fileName, string envelopedFileName)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				error = (int) EUEnvelopFileToRecipientsRSA((DWORD) algo,
					(DWORD) recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength,
					signData ? 1 : 0, fileNamePtr.DataPtr,
					envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataExWithDynamicKey(
			string[] recipientsCertIssuers, string[] recipientsCertSerials,
			bool signData, bool appendCert,
			string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal recipientsCertIssuersPtr =
					new EUMarshal(recipientsCertIssuers);
				EUMarshal recipientsCertSerialsPtr =
					new EUMarshal(recipientsCertSerials);
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataExWithDynamicKey(
					recipientsCertIssuersPtr.DataPtr,
					recipientsCertSerialsPtr.DataPtr,
					signData ? 1 : 0, appendCert ? 1 : 0,
					dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFileExWithDynamicKey(
			string[] recipientsCertIssuers, string[] recipientsCertSerials,
			bool signData, bool appendCert,
			string fileName, string envelopedFileName)
		{
			try
			{
				int error;
				EUMarshal recipientsCertIssuersPtr =
					new EUMarshal(recipientsCertIssuers);
				EUMarshal recipientsCertSerialsPtr =
					new EUMarshal(recipientsCertSerials);
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				error = (int) EUEnvelopFileExWithDynamicKey(
					recipientsCertIssuersPtr.DataPtr,
					recipientsCertSerialsPtr.DataPtr,
					signData ? 1 : 0, appendCert ? 1 : 0,
					fileNamePtr.DataPtr, envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataToRecipientsWithDynamicKey(
			byte[][] recipientCerts, bool signData, bool appendCert,
			string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataToRecipientsWithDynamicKey(
					(DWORD) recipientCerts.Length, intRecipientsCerts,
					intRecipientsCertsLength,
					signData ? 1 : 0, appendCert ? 1 : 0,
					dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);

				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFileToRecipientsWithDynamicKey(
			byte[][] recipientCerts, bool signData, bool appendCert,
			string fileName, string envelopedFileName)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				error = (int) EUEnvelopFileToRecipientsWithDynamicKey(
					(DWORD) recipientCerts.Length, intRecipientsCerts,
					intRecipientsCertsLength,
					signData ? 1 : 0, appendCert ? 1 : 0,
					fileNamePtr.DataPtr, envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataToRecipientsEx(byte[][] recipientCerts,
			int recipientAppendType, bool signData, string dataString,
			byte[] dataBinary, ref string envelopedDataString,
			ref byte[] envelopedDataBinary)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataToRecipientsEx(
					(DWORD) recipientCerts.Length, intRecipientsCerts,
					intRecipientsCertsLength, (DWORD) recipientAppendType,
					signData ? 1 : 0, dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);

				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopFileToRecipientsEx(
			byte[][] recipientCerts, int recipientAppendType,
			bool signData, string fileName, string envelopedFileName)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				error = (int) EUEnvelopFileToRecipientsEx(
					(DWORD) recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength,
					(DWORD) recipientAppendType, signData ? 1 : 0,
					fileNamePtr.DataPtr, envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopDataToRecipientsWithOCode(
			string recipientsOCode, int recipientAppendType,
			bool signData, string dataString, byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal recipientsOCodePtr =
					new EUMarshal(recipientsOCode);
				EUMarshal dataPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataStringPtr = new EUMarshal();
				envelopedDataBinaryPtr = new EUMarshal();

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopDataToRecipientsWithOCode(
					recipientsOCodePtr.DataPtr,
					(DWORD) recipientAppendType, signData ? 1 : 0,
					dataPtr.DataPtr, dataPtr.DataLength,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopAppendData(
			string dataString, byte[] dataBinary,
			string previousEnvelopedString, byte[] previousEnvelopedBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal previousEnvelopedStringPtr = new EUMarshal();
				EUMarshal previousEnvelopedBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (previousEnvelopedString != null)
				{
					previousEnvelopedStringPtr =
						new EUMarshal(previousEnvelopedString);
				}
				else if (previousEnvelopedBinary != null)
				{
					previousEnvelopedBinaryPtr =
						new EUMarshal(previousEnvelopedBinary);
				}

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopAppendData(
					dataPtr.DataPtr,dataPtr.DataLength,
					previousEnvelopedStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.DataLength,
					previousEnvelopedStringPtr.DataPtr,
					previousEnvelopedBinaryPtr.DataPtr,
					previousEnvelopedBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopAppendFile(string fileName,
			string previousEnvelopedFileName, string envelopedFileName)
		{
			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal previousEnvelopedFileNamePtr =
					new EUMarshal(previousEnvelopedFileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				error = (int) EUEnvelopAppendFile(fileNamePtr.DataPtr,
					previousEnvelopedFileNamePtr.DataPtr,
					envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopAppendDataEx(
			string dataString, byte[] dataBinary,
			byte[] senderCert, string previousEnvelopedString,
			byte[] previousEnvelopedBinary, ref string envelopedDataString,
			ref byte[] envelopedDataBinary)
		{
			EUMarshal envelopedDataStringPtr = null;
			EUMarshal envelopedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal senderCertPtr = new EUMarshal();
				EUMarshal previousEnvelopedStringPtr = new EUMarshal();
				EUMarshal previousEnvelopedBinaryPtr = new EUMarshal();

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (senderCert != null)
					senderCertPtr = new EUMarshal(senderCert);

				if (previousEnvelopedString != null)
				{
					previousEnvelopedStringPtr =
						new EUMarshal(previousEnvelopedString);
				}
				else if (previousEnvelopedBinary != null)
				{
					previousEnvelopedBinaryPtr =
						new EUMarshal(previousEnvelopedBinary);
				}

				if (envelopedDataString != null)
					envelopedDataStringPtr = new EUMarshal(false);
				else if (envelopedDataBinary != null)
					envelopedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUEnvelopAppendDataEx(
					dataPtr.DataPtr,dataPtr.DataLength,
					senderCertPtr.DataPtr, senderCertPtr.DataLength,
					previousEnvelopedStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.DataLength,
					previousEnvelopedStringPtr.DataPtr,
					previousEnvelopedBinaryPtr.DataPtr,
					previousEnvelopedBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (envelopedDataString != null)
				{
					envelopedDataString =
						envelopedDataStringPtr.GetStringData();
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinary =
						envelopedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataStringPtr != null)
					envelopedDataStringPtr.FreeData();
				if (envelopedDataBinaryPtr != null)
					envelopedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _EnvelopAppendFileEx(string fileName,
			byte[] senderCert, string previousEnvelopedFileName,
			string envelopedFileName)
		{
			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal senderCertPtr = new EUMarshal();
				EUMarshal previousEnvelopedFileNamePtr =
					new EUMarshal(previousEnvelopedFileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				if (senderCert != null)
					senderCertPtr = new EUMarshal(senderCert);

				error = (int) EUEnvelopAppendFileEx(fileNamePtr.DataPtr,
					senderCertPtr.DataPtr, senderCertPtr.DataLength,
					previousEnvelopedFileNamePtr.DataPtr,
					envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevelopDataEx(string envelopedDataString,
			byte[] envelopedDataBinary, byte[] senderCert,
			out byte[] data, out EU_SENDER_INFO senderInfo)
		{
			EUMarshal dataPtr = null;
			EUMarshal senderInfoPtr = null;

			senderInfo = new EU_SENDER_INFO();
			senderInfo.filled = false;
			senderInfo.intSenderInfo = IntPtr.Zero;
			senderInfo.senderInfoPtr = null;

			data = null;

			try
			{
				int error;
				EUMarshal senderCertPtr = new EUMarshal();
				EUMarshal envelopedDataStringPtr = new EUMarshal();
				EUMarshal envelopedDataBinaryPtr = new EUMarshal();

				dataPtr = new EUMarshal(true);
				senderInfoPtr = new EUMarshal(EUMarshal.EU_SENDER_INFO_SIZE);
				Marshal.WriteInt32(senderInfoPtr.DataPtr, 0);

				if (senderCert != null)
					senderCertPtr = new EUMarshal(senderCert);

				if (envelopedDataString != null)
				{
					envelopedDataStringPtr =
						new EUMarshal(envelopedDataString);
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinaryPtr =
						new EUMarshal(envelopedDataBinary);
				}

				error = (int) EUDevelopDataEx(envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.DataLength,
					senderCertPtr.DataPtr, senderCertPtr.DataLength,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr,
					senderInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				senderInfo = new EU_SENDER_INFO(senderInfoPtr);
				if (senderInfo.senderInfoPtr != null)
					senderInfoPtr = null;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (senderInfoPtr != null)
					_FreeSenderInfo(senderInfoPtr);
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _DevelopFileEx(
			string envelopedFileName, byte[] senderCert,
			string fileName, out EU_SENDER_INFO senderInfo)
		{
			EUMarshal senderInfoPtr = null;

			senderInfo = new EU_SENDER_INFO();
			senderInfo.filled = false;
			senderInfo.intSenderInfo = IntPtr.Zero;
			senderInfo.senderInfoPtr = null;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal senderCertPtr = new EUMarshal();
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				senderInfoPtr = new EUMarshal(EUMarshal.EU_SENDER_INFO_SIZE);
				Marshal.WriteInt32(senderInfoPtr.DataPtr, 0);

				if (senderCert != null)
					senderCertPtr = new EUMarshal(senderCert);

				error = (int) EUDevelopFileEx(envelopedFileNamePtr.DataPtr,
					senderCertPtr.DataPtr, senderCertPtr.DataLength,
					fileNamePtr.DataPtr, senderInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				senderInfo = new EU_SENDER_INFO(senderInfoPtr);
				if (senderInfo.senderInfoPtr != null)
					senderInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (senderInfoPtr != null)
					_FreeSenderInfo(senderInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _ProtectDataByPassword(
			string dataString, byte[] dataBinary,
			string password, ref string protectedDataString,
			ref byte[] protectedDataBinary)
		{
			EUMarshal protectedDataStringPtr = null;
			EUMarshal protectedDataBinaryPtr = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();
				EUMarshal passwordPtr = new EUMarshal();

				protectedDataStringPtr = new EUMarshal();
				protectedDataBinaryPtr = new EUMarshal();

				if (password != null)
					passwordPtr = new EUMarshal(password);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (protectedDataString != null)
					protectedDataStringPtr = new EUMarshal(false);
				else if (protectedDataBinary != null)
					protectedDataBinaryPtr = new EUMarshal(true);

				error = (int) EUProtectDataByPassword(
					dataPtr.DataPtr, dataPtr.DataLength,
					passwordPtr.DataPtr,
					protectedDataStringPtr.DataPtr,
					protectedDataBinaryPtr.DataPtr,
					protectedDataBinaryPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (protectedDataString != null)
				{
					protectedDataString =
						protectedDataStringPtr.GetStringData();
				}
				else if (protectedDataBinary != null)
				{
					protectedDataBinary =
						protectedDataBinaryPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (protectedDataStringPtr != null)
					protectedDataStringPtr.FreeData();
				if (protectedDataBinaryPtr != null)
					protectedDataBinaryPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _UnprotectDataByPassword(
			string protectedDataString, byte[] protectedDataBinary,
			string password, ref string dataString,
			ref byte[] dataBinary)
		{
			EUMarshal dataPtr = null;

			try
			{
				int error;
				EUMarshal protectedDataStringPtr = new EUMarshal();
				EUMarshal protectedDataBinaryPtr = new EUMarshal();
				EUMarshal passwordPtr = new EUMarshal();

				dataPtr = new EUMarshal(true);

				if (password != null)
					passwordPtr = new EUMarshal(password);

				if (protectedDataString != null)
					protectedDataStringPtr = new EUMarshal(protectedDataString);
				else if (protectedDataBinary != null)
					protectedDataBinaryPtr = new EUMarshal(protectedDataBinary);

				error = (int) EUUnprotectDataByPassword(
					protectedDataStringPtr.DataPtr,
					protectedDataBinaryPtr.DataPtr,
					protectedDataBinaryPtr.DataLength,
					passwordPtr.DataPtr,
					dataPtr.DataPtr,
					dataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (dataString != null)
				{
					dataString =
						dataPtr.GetStringData(true, false);
				}
				else if (dataBinary != null)
				{
					dataBinary =
						dataPtr.GetBinaryData();
				}
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxEnvelopData(
			IntPtr privateKeyContext, byte[][] recipientCerts,
			int recipientAppendType, bool signData, bool appendCert,
			string dataString, byte[] dataBinary,
			out byte[] envelopedData)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;
			EUMarshal envelopedDataPtr = null;

			envelopedData = null;

			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal();

				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				envelopedDataPtr = new EUMarshal(true,
					privateKeyContext);

				error = (int) EUCtxEnvelopData(privateKeyContext,
					(DWORD)recipientCerts.Length, intRecipientsCerts,
					intRecipientsCertsLength,
					(DWORD)recipientAppendType, signData ? 1 : 0,
					appendCert ? 1 : 0, dataPtr.DataPtr,
					dataPtr.DataLength,
					envelopedDataPtr.DataPtr,
					envelopedDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				envelopedData = envelopedDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);

				if (envelopedDataPtr != null)
					envelopedDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxEnvelopFile(
			IntPtr privateKeyContext, byte[][] recipientCerts,
			int recipientAppendType, bool signData, bool appendCert,
			string fileName, string envelopedFileName)
		{
			IntPtr intRecipientsCerts = IntPtr.Zero;
			IntPtr intRecipientsCertsLength = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);
				EUMarshal.CopyArraysOfBytesToIntPtr(recipientCerts,
					ref intRecipientsCerts, ref intRecipientsCertsLength);

				error = (int) EUCtxEnvelopFile(privateKeyContext,
					(DWORD)recipientCerts.Length, intRecipientsCerts,
					intRecipientsCertsLength,
					(DWORD)recipientAppendType, signData ? 1 : 0,
					appendCert ? 1 : 0, fileNamePtr.DataPtr,
					envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(recipientCerts.Length,
					intRecipientsCerts, intRecipientsCertsLength);
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxDevelopData(
			IntPtr privateKeyContext, string envelopedDataString,
			byte[] envelopedDataBinary, byte[] senderCert,
			out byte[] data, out EU_SENDER_INFO senderInfo)
		{
			EUMarshal dataPtr = null;
			EUMarshal senderInfoPtr = null;

			senderInfo = new EU_SENDER_INFO();
			senderInfo.filled = false;
			senderInfo.intSenderInfo = IntPtr.Zero;
			senderInfo.senderInfoPtr = null;

			data = null;

			try
			{
				int error;
				EUMarshal senderCertPtr = new EUMarshal();
				EUMarshal envelopedDataStringPtr = new EUMarshal();
				EUMarshal envelopedDataBinaryPtr = new EUMarshal();

				dataPtr = new EUMarshal(true, privateKeyContext);
				senderInfoPtr = new EUMarshal(EUMarshal.EU_SENDER_INFO_SIZE);
				Marshal.WriteInt32(senderInfoPtr.DataPtr, 0);

				if (senderCert != null)
					senderCertPtr = new EUMarshal(senderCert);

				if (envelopedDataString != null)
				{
					envelopedDataStringPtr =
						new EUMarshal(envelopedDataString);
				}
				else if (envelopedDataBinary != null)
				{
					envelopedDataBinaryPtr =
						new EUMarshal(envelopedDataBinary);
				}

				error = (int) EUCtxDevelopData(privateKeyContext,
					envelopedDataStringPtr.DataPtr,
					envelopedDataBinaryPtr.DataPtr,
					envelopedDataBinaryPtr.DataLength,
					senderCertPtr.DataPtr, senderCertPtr.DataLength,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr,
					senderInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				senderInfo = new EU_SENDER_INFO(senderInfoPtr);
				if (senderInfo.senderInfoPtr != null)
					senderInfoPtr = null;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (senderInfoPtr != null)
				{
					_FreeSenderInfo(senderInfoPtr, privateKeyContext);
				}

				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxDevelopFile(
			IntPtr privateKeyContext, string envelopedFileName, byte[] senderCert,
			string fileName, out EU_SENDER_INFO senderInfo)
		{
			EUMarshal senderInfoPtr = null;

			senderInfo = new EU_SENDER_INFO();
			senderInfo.filled = false;
			senderInfo.intSenderInfo = IntPtr.Zero;
			senderInfo.senderInfoPtr = null;

			try
			{
				int error;
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal senderCertPtr = new EUMarshal();
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				senderInfoPtr = new EUMarshal(EUMarshal.EU_SENDER_INFO_SIZE);
				Marshal.WriteInt32(senderInfoPtr.DataPtr, 0);

				if (senderCert != null)
					senderCertPtr = new EUMarshal(senderCert);

				error = (int) EUCtxDevelopFile(privateKeyContext, 
					envelopedFileNamePtr.DataPtr,
					senderCertPtr.DataPtr, senderCertPtr.DataLength,
					fileNamePtr.DataPtr, senderInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				senderInfo = new EU_SENDER_INFO(senderInfoPtr);
				if (senderInfo.senderInfoPtr != null)
					senderInfoPtr = null;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (senderInfoPtr != null)
				{
					_FreeSenderInfo(senderInfoPtr,
						privateKeyContext);
				}
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxEnvelopAppendData(
			IntPtr privateKeyContext, string dataString, byte[] dataBinary,
			byte[] senderCert, string prevEnvelopedString,
			byte[] prevEnvelopedBinary, out byte[] envelopedData)
		{
			EUMarshal envelopedDataPtr = null;

			envelopedData = null;

			try
			{
				int error;

				if (prevEnvelopedString != null)
				{
					error = BASE64Decode(prevEnvelopedString,
						out prevEnvelopedBinary);
					if (error != EU_ERROR_NONE)
						return error;
				}

				EUMarshal dataPtr = new EUMarshal();
				EUMarshal senderCertPtr = new EUMarshal();
				EUMarshal prevEnvelopedPtr = new EUMarshal(
					prevEnvelopedBinary);

				if (dataString != null)
					dataPtr = new EUMarshal(dataString, false);
				else if (dataBinary != null)
					dataPtr = new EUMarshal(dataBinary);

				if (senderCert != null)
					senderCertPtr = new EUMarshal(senderCert);

				envelopedDataPtr = new EUMarshal(true, privateKeyContext);

				error = (int) EUCtxEnvelopAppendData(
					privateKeyContext, dataPtr.DataPtr,
					dataPtr.DataLength, senderCertPtr.DataPtr,
					senderCertPtr.DataLength, prevEnvelopedPtr.DataPtr,
					prevEnvelopedPtr.DataLength, envelopedDataPtr.DataPtr,
					envelopedDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				envelopedData = envelopedDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (envelopedDataPtr != null)
					envelopedDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxEnvelopAppendFile(
			IntPtr privateKeyContext, string fileName,
			byte[] senderCert, string prevEnvelopedFileName,
			string envelopedFileName)
		{
			try
			{
				int error;

				EUMarshal dataPtr = new EUMarshal();
				EUMarshal senderCertPtr = new EUMarshal();
				EUMarshal fileNamePtr = new EUMarshal(fileName);
				EUMarshal prevEnvelopedFileNamePtr =
					new EUMarshal(prevEnvelopedFileName);
				EUMarshal envelopedFileNamePtr =
					new EUMarshal(envelopedFileName);

				if (senderCert != null)
					senderCertPtr = new EUMarshal(senderCert);

				error = (int) EUCtxEnvelopAppendFile(
					privateKeyContext, fileNamePtr.DataPtr,
					senderCertPtr.DataPtr, senderCertPtr.DataLength,
					prevEnvelopedFileNamePtr.DataPtr,
					envelopedFileNamePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: Client/Server secure session functions

		private static int _SessionDestroy(IntPtr session)
		{
			try
			{
				EUSessionDestroy(session);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ClientSessionCreateStep1(int expireTime,
			out IntPtr clientSession, out byte[] clientData)
		{
			EUMarshal clientDataPtr = null;

			clientSession = IntPtr.Zero;
			clientData = null;

			try
			{
				int error;
				EUMarshal clientSessionPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				clientDataPtr = new EUMarshal(true);

				error = (int)EUClientSessionCreateStep1((DWORD)expireTime,
					clientSessionPtr.DataPtr, clientDataPtr.DataPtr,
					clientDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				clientSession = clientSessionPtr.GetPointerData();
				clientData = clientDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (clientDataPtr != null)
					clientDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ServerSessionCreateStep1(int expireTime,
			byte[] clientData, out IntPtr serverSession, out byte[] serverData)
		{
			EUMarshal serverDataPtr = null;

			serverSession = IntPtr.Zero;
			serverData = null;

			try
			{
				int error;
				EUMarshal clientDataPtr = new EUMarshal(clientData);
				EUMarshal serverSessionPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				serverDataPtr = new EUMarshal(true);

				error = (int)EUServerSessionCreateStep1((DWORD)expireTime,
					clientDataPtr.DataPtr, clientDataPtr.DataLength,
					serverSessionPtr.DataPtr, serverDataPtr.DataPtr,
					serverDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				serverSession = serverSessionPtr.GetPointerData();
				serverData = serverDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (serverDataPtr != null)
					serverDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ClientSessionCreateStep2(
			IntPtr clientSession, byte[] serverData, ref byte[] clientData)
		{
			EUMarshal clientDataPtr = null;

			try
			{
				int error;
				EUMarshal serverDataPtr = new EUMarshal(serverData);
				clientDataPtr = new EUMarshal();

				if (clientData != null)
					clientDataPtr = new EUMarshal(true);

				error = (int) EUClientSessionCreateStep2(clientSession,
					serverDataPtr.DataPtr, serverDataPtr.DataLength,
					clientDataPtr.DataPtr, clientDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				if (clientData != null)
					clientData = clientDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (clientDataPtr != null)
					clientDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ServerSessionCreateStep2(
			IntPtr serverSession, byte[] clientData)
		{
			try
			{
				int error;
				EUMarshal clientDataPtr = new EUMarshal(clientData);

				error = (int) EUServerSessionCreateStep2(serverSession,
					clientDataPtr.DataPtr, clientDataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _ClientDynamicKeySessionCreate(
			int expireTime, string serverCertIssuer,
			string serverCertSerial, byte[] serverCert,
			out IntPtr clientSession, out byte[] clientData)
		{
			EUMarshal clientDataPtr = null;

			clientSession = IntPtr.Zero;
			clientData = null;

			try
			{
				int error;
				EUMarshal serverCertIssuerPtr = new EUMarshal();
				EUMarshal serverCertSerialPtr = new EUMarshal();
				EUMarshal serverCertPtr = new EUMarshal();
				EUMarshal clientSessionPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));
				clientDataPtr = new EUMarshal(true);

				if (serverCertIssuer != null && serverCertSerial != null)
				{
					serverCertIssuerPtr = new EUMarshal(serverCertIssuer);
					serverCertSerialPtr = new EUMarshal(serverCertSerial);
				}
				else if (serverCert != null)
				{
					serverCertPtr = new EUMarshal(serverCert);
				}

				error = (int)EUClientDynamicKeySessionCreate((DWORD)expireTime,
					serverCertIssuerPtr.DataPtr, serverCertSerialPtr.DataPtr,
					serverCertPtr.DataPtr, serverCertPtr.DataLength,
					clientSessionPtr.DataPtr, clientDataPtr.DataPtr,
					clientDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				clientSession = clientSessionPtr.GetPointerData();
				clientData = clientDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (clientDataPtr != null)
					clientDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ServerDynamicKeySessionCreate(
			int expireTime, byte[] clientData, out IntPtr serverSession)
		{
			serverSession = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal clientDataPtr = new EUMarshal(clientData);
				EUMarshal serverSessionPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				error = (int)EUServerDynamicKeySessionCreate((DWORD)expireTime,
					clientDataPtr.DataPtr, clientDataPtr.DataLength,
					serverSessionPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				serverSession = serverSessionPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SessionIsInitialized(
			IntPtr session, out bool isInitializes)
		{
			isInitializes = false;

			try
			{
				isInitializes = (EUSessionIsInitialized(session) != 0);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SessionSave(
			IntPtr session, out byte[] sessionData)
		{
			EUMarshal sessionDataPtr = null;

			sessionData = null;

			try
			{
				int error;
				sessionDataPtr = new EUMarshal(true);

				error = (int) EUSessionSave(session,
					sessionDataPtr.DataPtr, sessionDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				sessionData = sessionDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (sessionDataPtr != null)
					sessionDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _SessionLoad(
			byte[] sessionData, out IntPtr session)
		{
			session = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal sessionDataPtr = new EUMarshal(sessionData);
				EUMarshal sessionPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				error = (int) EUSessionLoad(sessionDataPtr.DataPtr,
					sessionDataPtr.DataLength, sessionPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				session = sessionPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SessionCheckCertificates(IntPtr session)
		{
			try
			{
				return (int) EUSessionCheckCertificates(session);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
		}

		private static int _SessionProccessData(IntPtr session,
			bool encrypt, byte[] dataIn, out byte[] dataOut)
		{
			EUMarshal dataOutPtr = null;

			dataOut = null;

			try
			{
				int error;
				EUMarshal dataInPtr = new EUMarshal(dataIn);
				dataOutPtr = new EUMarshal(true);

				if (encrypt)
				{
					error = (int) EUSessionEncrypt(session,
						dataInPtr.DataPtr, dataInPtr.DataLength,
						dataOutPtr.DataPtr, dataOutPtr.BinaryDataLengthPtr);
				}
				else
				{
					error = (int) EUSessionDecrypt(session,
						dataInPtr.DataPtr, dataInPtr.DataLength,
						dataOutPtr.DataPtr, dataOutPtr.BinaryDataLengthPtr);
				}

				if (error != EU_ERROR_NONE)
					return error;

				dataOut = dataOutPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataOutPtr != null)
					dataOutPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _SessionProccessDataContinue(
			IntPtr session, bool encrypt, ref byte[] data)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal(data);

				if (encrypt)
				{
					error = (int) EUSessionEncryptContinue(session,
						dataPtr.DataPtr, dataPtr.DataLength);
				}
				else
				{
					error = (int) EUSessionDecryptContinue(session,
						dataPtr.DataPtr, dataPtr.DataLength);
				}

				if (error != EU_ERROR_NONE)
					return error;

				Marshal.Copy(dataPtr.DataPtr, data, 0, data.Length);
				dataPtr.FreeData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SessionGetPeerCertificateInfo(
			IntPtr session, out EU_CERT_INFO certInfo)
		{
			EUMarshal certInfoPtr = null;

			certInfo = new EU_CERT_INFO();

			try
			{
				int error;
				certInfoPtr = new EUMarshal(EUMarshal.EU_CERT_INFO_SIZE);

				Marshal.WriteInt32(certInfoPtr.DataPtr, 0);

				error = (int) EUSessionGetPeerCertificateInfo(
					session, certInfoPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				certInfo = new EU_CERT_INFO(certInfoPtr.DataPtr);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_FreeCertInfo(certInfoPtr);
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: Library context functions

		private static int _CtxCreate(out IntPtr context)
		{
			context = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal contextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				error = (int) EUCtxCreate(contextPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				context = contextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxFree(IntPtr context)
		{
			try
			{
				EUCtxFree(context);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _CtxSetParameter(
			IntPtr context, string parameterName, object parameterValue)
		{
			try
			{
				int error;
				EUMarshal parameterNamePtr = new EUMarshal(parameterName);
				EUMarshal parameterPtr = new EUMarshal();

				if (parameterValue is bool)
				{
					bool blValue = (bool)parameterValue;
					parameterPtr = new EUMarshal(EUMarshal.INT_SIZE);
					Marshal.WriteInt32(parameterPtr.DataPtr,
						blValue ? 1 : 0);
				}
				else if (parameterValue is int)
				{
					int value = (int)parameterValue;
					parameterPtr = new EUMarshal(EUMarshal.INT_SIZE);
					Marshal.WriteInt32(parameterPtr.DataPtr, value);
				}
				else if (parameterValue is string)
				{
					parameterPtr = new EUMarshal((string)parameterValue);
				}

				error = (int) EUCtxSetParameter(context, parameterNamePtr.DataPtr,
					parameterPtr.DataPtr, parameterPtr.DataLength);
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: SC client functions

		private static int _SCClientIsRunning(out bool running)
		{
			running = false;

			try
			{
				int error;
				EUMarshal runningPtr = new EUMarshal(EUMarshal.INT_SIZE);

				error = (int) EUSCClientIsRunning(runningPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				running = runningPtr.GetBoolData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SCClientStart()
		{
			try
			{
				return (int) EUSCClientStart();
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
		}

		private static void _SCClientStop()
		{
			try
			{
				EUSCClientStop();
			}
			catch (Exception)
			{
			}
		}

		private static int _SCClientAddGate(
			string gateName, short connectPort, 
			string gatewayAddress, short gatewayPort, 
			string externalInterface, string externalRouterIPAddress)
		{
			try
			{
				int error;
				EUMarshal gateNamePtr = new EUMarshal(gateName);
				EUMarshal gatewayAddressPtr = new EUMarshal(gatewayAddress);
				EUMarshal externalInterfacePtr = (externalInterface != null) ? 
					new EUMarshal(externalInterface) : new EUMarshal();
				EUMarshal externalRouterIPAddressPtr = 
					(externalRouterIPAddress != null) ? 
						new EUMarshal(externalRouterIPAddress) : 
						new EUMarshal();

				error = (int) EUSCClientAddGate(
					gateNamePtr.DataPtr, connectPort,
					gatewayAddressPtr.DataPtr, gatewayPort,
					externalInterfacePtr.DataPtr,
					externalRouterIPAddressPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SCClientRemoveGate(
			short connectPort)
		{
			try
			{
				int error;

				error = (int) EUSCClientRemoveGate(connectPort);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _SCClientGetStatistic(
			out EU_SCC_STATISTIC	statistic)
		{
			EUMarshal statisticPtr = null;
			statistic = new EU_SCC_STATISTIC();

			try
			{
				int error;
				statisticPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				error = (int) EUSCClientGetStatistic(statisticPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				statistic = new EU_SCC_STATISTIC(
					statisticPtr.GetPointerData());
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				_SCClientFreeStatistic(statisticPtr);
			}

			return EU_ERROR_NONE;
		}

		private static int _SCClientFreeStatistic(EUMarshal statistic)
		{
			if (statistic == null)
				return EU_ERROR_NONE;

			try
			{
				if (statistic.DataPtr == IntPtr.Zero)
					return EU_ERROR_NONE;

				IntPtr intStatistic = statistic.GetPointerData();
				EUFreeCertificateInfoEx(intStatistic);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: TaxService functions

		private static int _AppendTransportHeader(
			string caType, string fileName, string clientEMail,
			byte[] clientCert, byte[] cryptoData,
			out byte[] transportData)
		{
			EUMarshal transportDataPtr = null;

			transportData = null;

			try
			{
				int error;
				EUMarshal caTypePtr = new EUMarshal();
				EUMarshal fileNamePtr = new EUMarshal();
				EUMarshal clientEMailPtr = new EUMarshal();
				EUMarshal clientCertPtr = new EUMarshal(clientCert);
				EUMarshal cryptoDataPtr = new EUMarshal(cryptoData);

				if (caType != null)
					caTypePtr = new EUMarshal(caType);
				if (fileName != null)
					fileNamePtr = new EUMarshal(fileName);
				if (clientEMail != null)
					clientEMailPtr = new EUMarshal(clientEMail);

				transportDataPtr = new EUMarshal(true);

				error = (int)EUAppendTransportHeader(caTypePtr.DataPtr,
					fileNamePtr.DataPtr, clientEMailPtr.DataPtr,
					clientCertPtr.DataPtr, clientCertPtr.DataLength,
					cryptoDataPtr.DataPtr, cryptoDataPtr.DataLength,
					transportDataPtr.DataPtr, transportDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				transportData = transportDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (transportDataPtr != null)
					transportDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ParseTransportHeader(
			byte[] transportData, out EU_TRANSPORT_HEADER header)
		{
			EUMarshal cryptoDataPtr = null;

			header = new EU_TRANSPORT_HEADER();

			try
			{
				int error;
				EUMarshal transportDataPtr = new EUMarshal(transportData);
				EUMarshal receiptNumberPtr = new EUMarshal(EUMarshal.DWORD_SIZE);
				
				cryptoDataPtr = new EUMarshal(true);

				error = (int) EUParseTransportHeader(
					transportDataPtr.DataPtr, transportDataPtr.DataLength,
					receiptNumberPtr.DataPtr,
					cryptoDataPtr.DataPtr, cryptoDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				header.receiptNumber = receiptNumberPtr.GetDWORDData();
				header.cryptoData = cryptoDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (cryptoDataPtr != null)
					cryptoDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _AppendCryptoHeader(
			string caType, EU_HEADER_PART_TYPE headerType,
			byte[] cryptoData, out byte[] transportData)
		{
			EUMarshal transportDataPtr = null;

			transportData = null;

			try
			{
				int error;
				EUMarshal caTypePtr = new EUMarshal();
				EUMarshal cryptoDataPtr = new EUMarshal(cryptoData);

				if (caType != null)
					caTypePtr = new EUMarshal(caType);

				transportDataPtr = new EUMarshal(true);

				error = (int)EUAppendCryptoHeader(caTypePtr.DataPtr,
					(DWORD) headerType, 
					cryptoDataPtr.DataPtr, cryptoDataPtr.DataLength,
					transportDataPtr.DataPtr, transportDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				transportData = transportDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (transportDataPtr != null)
					transportDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _ParseCryptoHeader(
			byte[] transportData, out EU_CRYPTO_HEADER header)
		{
			EUMarshal cryptoDataPtr = null;

			header = new EU_CRYPTO_HEADER();

			try
			{
				int error;
				EUMarshal transportDataPtr = new EUMarshal(transportData);
				EUMarshal caTypePtr = new EUMarshal(EU_HEADER_MAX_CA_TYPE_SIZE + 1);
				EUMarshal headerTypePtr = new EUMarshal(EUMarshal.DWORD_SIZE);
				EUMarshal headerSizePtr = new EUMarshal(EUMarshal.DWORD_SIZE);

				cryptoDataPtr = new EUMarshal(true);

				error = (int) EUParseCryptoHeader(
					transportDataPtr.DataPtr, transportDataPtr.DataLength,
					caTypePtr.DataPtr, headerTypePtr.DataPtr, headerSizePtr.DataPtr,
					cryptoDataPtr.DataPtr, cryptoDataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				header.caType = caTypePtr.GetStringData();
				header.headerType = (EU_HEADER_PART_TYPE)headerTypePtr.GetDWORDData();
				header.headerSize = (int)headerSizePtr.GetDWORDData();
				header.cryptoData = cryptoDataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (cryptoDataPtr != null)
					cryptoDataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#region EUSignCP: Library device context functions

		private static int _DevCtxEnumVirtual(ref IntPtr deviceContext,
			out string typeDescription)
		{
			typeDescription = null;

			try
			{
				int error;
				EUMarshal typeDescrPtr = new EUMarshal(
					EU_KEY_MEDIA_NAME_MAX_LENGTH + 1);
				EUMarshal deviceContextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				Marshal.WriteIntPtr(deviceContextPtr.DataPtr, deviceContext);

				error = (int)EUDevCtxEnumVirtual(
					deviceContextPtr.DataPtr,
					typeDescrPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				typeDescription = typeDescrPtr.GetStringData();

				if (deviceContext == IntPtr.Zero)
					deviceContext = deviceContextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxEnum(IntPtr deviceContext,
			out string deviceDescription)
		{
			deviceDescription = null;

			try
			{
				int error;
				EUMarshal deviceDescrPtr = new EUMarshal(
					EU_KEY_MEDIA_NAME_MAX_LENGTH + 1);

				error = (int)EUDevCtxEnum(
					deviceContext,
					deviceDescrPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				deviceDescription = deviceDescrPtr.GetStringData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxClose(IntPtr deviceContext)
		{
			try
			{
				EUDevCtxClose(deviceContext);
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxOpenIDCard(
			string typeDescription, string deviceDescription,
			string password, int passwordVersion,
			out IntPtr deviceContext)
		{
			deviceContext = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal typeDescriptionPtr = new EUMarshal(typeDescription);
				EUMarshal deviceDescriptionPtr = new EUMarshal(deviceDescription);
				EUMarshal passwordPtr = new EUMarshal(password);
				EUMarshal deviceContextPtr = new EUMarshal(
					Marshal.SizeOf(typeof(IntPtr)));

				error = (int)EUDevCtxOpenIDCard(
					typeDescriptionPtr.DataPtr,
					deviceDescriptionPtr.DataPtr,
					passwordPtr.DataPtr,
					(DWORD)passwordVersion,
					deviceContextPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				deviceContext = deviceContextPtr.GetPointerData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxChangeIDCardPasswords(
			IntPtr deviceContext, string pin, string puk)
		{
			try
			{
				int error;
				EUMarshal pinPtr = new EUMarshal(pin);
				EUMarshal pukPtr = new EUMarshal(puk);

				error = (int)EUDevCtxChangeIDCardPasswords(
					deviceContext, pinPtr.DataPtr, pukPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxAuthenticateIDCard(
			IntPtr deviceContext, string parameter1, string parameter2)
		{
			try
			{
				int error;
				EUMarshal parameter1Ptr = new EUMarshal(parameter1);
				EUMarshal parameter2Ptr = new EUMarshal(parameter2);

				error = (int)EUDevCtxAuthenticateIDCard(
					deviceContext, parameter1Ptr.DataPtr,
					parameter2Ptr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxVerifyIDCardData(
			IntPtr deviceContext, byte tag)
		{
			try
			{
				int error;

				error = (int)EUDevCtxVerifyIDCardData(
					deviceContext, tag);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxUpdateIDCardData(
			IntPtr deviceContext, IntPtr privateKeyContext,
			byte tag, byte[] data)
		{
			try
			{
				int error;
				EUMarshal dataPtr = new EUMarshal(data);

				error = (int)EUDevCtxUpdateIDCardData(
					deviceContext, privateKeyContext, tag,
					dataPtr.DataPtr, dataPtr.DataLength);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxEnumIDCardData(
			IntPtr deviceContext, byte tag, int index,
			ref byte[] data)
		{
			EUMarshal dataPtr = null;

			try
			{
				int error;
				dataPtr = new EUMarshal(true);

				error = (int)EUDevCtxEnumIDCardData(
					deviceContext, tag, (DWORD)index,
					dataPtr.DataPtr, dataPtr.BinaryDataLengthPtr);
				if (error != EU_ERROR_NONE)
					return error;

				data = dataPtr.GetBinaryData();
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (dataPtr != null)
					dataPtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxEnumIDCardDataChangeDate(
			IntPtr deviceContext, byte tag, int index,
			out SYSTEMTIME changeDate)
		{
			EUMarshal changeDatePtr = null;

			changeDate = new SYSTEMTIME();

			try
			{
				int error;

				changeDatePtr = new EUMarshal(Marshal.SizeOf(typeof(SYSTEMTIME)));

				error = (int)EUDevCtxEnumIDCardDataChangeDate(
					deviceContext, tag, (DWORD)index,
					changeDatePtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;

				changeDate = (SYSTEMTIME)Marshal.PtrToStructure(
					changeDatePtr.DataPtr, typeof(SYSTEMTIME));
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				if (changeDatePtr != null)
					changeDatePtr.FreeData();
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxVerifyIDCardSecurityObjectDocument(
			IntPtr deviceContext, string certificatesStorePath)
		{
			try
			{
				int error;
				EUMarshal certificatesStorePathPtr = new EUMarshal(certificatesStorePath);

				error = (int)EUDevCtxVerifyIDCardSecurityObjectDocument(
					deviceContext, certificatesStorePathPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}

			return EU_ERROR_NONE;
		}

		private static int _DevCtxInternalAuthenticateIDCard(
			IntPtr deviceContext, byte[][] CVCerts,
			byte[] privateKey, string password)
		{
			IntPtr intCVCerts = IntPtr.Zero;
			IntPtr intCVCertsLength = IntPtr.Zero;

			try
			{
				int error;
				EUMarshal privateKeyPtr = new EUMarshal();

				EUMarshal.CopyArraysOfBytesToIntPtr(CVCerts,
					ref intCVCerts, ref intCVCertsLength);

				privateKeyPtr = new EUMarshal(privateKey);

				EUMarshal passwordPtr = new EUMarshal(password);

				error = (int)EUDevCtxInternalAuthenticateIDCard(
					deviceContext, (DWORD)CVCerts.Length,
					intCVCerts, intCVCertsLength,
					privateKeyPtr.DataPtr, privateKeyPtr.DataLength,
					passwordPtr.DataPtr);
				if (error != EU_ERROR_NONE)
					return error;
			}
			catch (EUSignCPException ex)
			{
				return ex.errorCode;
			}
			catch (Exception)
			{
				return EU_ERROR_UNKNOWN;
			}
			finally
			{
				EUMarshal.FreeArraysOfBytesInIntPtr(CVCerts.Length,
					intCVCerts, intCVCertsLength);
			}

			return EU_ERROR_NONE;
		}

		#endregion

		#endregion
		#endregion

		#region EUSignCP: Public section

		#region EUSignCP: General function
		/*__BEGIN_MONO_CODE__
#if __ANDROID__
		public static void SetUSBDevice(int fd, byte[] descriptors)
		{
			int error;

			error = _SetUSBDevice(fd, descriptors);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static void PreLoadLibraries(string[] libraries)
		{
			int error;

			error = _PreLoadLibraries(libraries);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}
#endif // __ANDROID__
__END_MONO_CODE__*/
		public static int Initialize()
		{
			int error;

			error = _Initialize();
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void Finalize()
		{
			int error;

			error = _Finalize();
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static bool IsInitialized()
		{
			int error;
			bool isInitialized;

			error = _IsInitialized(out isInitialized);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return isInitialized;
		}

		public static void ResetOperation()
		{
			int error;

			error = _ResetOperation();
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static void ResetOperationCtx(IntPtr context)
		{
			int error;

			error = _ResetOperationCtx(context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static string GetErrorDesc(int error)
		{
			return _GetErrorDesc(error);
		}

		public static string GetErrorLangDesc(int error,
			EU_LANG lang = EU_LANG.DEFAULT)
		{
			return _GetErrorLangDesc(error, lang);
		}

		public static int BASE64Encode(byte[] data, out string dataString)
		{
			int error;

			error = _BASE64Encode(data, out dataString);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int BASE64Decode(string dataString, out byte[] data)
		{
			int error;

			error = _BASE64Decode(dataString, out data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static bool CompareArrays(byte[] arr1, byte[] arr2)
		{
			int error;
			bool isEqual;

			error = _CompareArrays(arr1, arr2, out isEqual);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return isEqual;
		}

		public static int DownloadFileViaHTTP(string url,
			string fileName, out byte[] fileData)
		{
			int error;

			error = _DownloadFileViaHTTP(url,
				fileName, out fileData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GeneratePRNGSequence(ref byte[] data)
		{
			int error;

			error = _GeneratePRNGSequence(ref data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static bool ShowSecureConfirmDialog(
			string caption, string label, string footer)
		{
			return _ShowSecureConfirmDialog(caption, label, footer);
		}

		#endregion

		#region EUSignCP: Get/Set library parameters functions

		public static void SetThrowExceptions(bool throwExceptions,
			EU_LANG lang = EU_LANG.DEFAULT)
		{
			_throwExceptions = throwExceptions;
			_lang = lang;
		}

		public static bool DoesNeedSetSettings()
		{
			int error;
			bool setSettings;

			error = _DoesNeedSetSettings(out setSettings);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return setSettings;
		}

		public static void SetSettings()
		{
			int error;

			error = _SetSettings();
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int SetSettingsFilePath(string settingsFilePath)
		{
			int error;

			error = _SetSettingsFilePath(settingsFilePath);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetSettingsFilePathEx(
			string settingsFilePath, int rootKey, string regPath)
		{
			int error;

			error = _SetSettingsFilePathEx(settingsFilePath, rootKey, regPath);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetSettingsRegPath(int rootKey, string regPath)
		{
			int error;

			error = _SetSettingsRegPath(rootKey, regPath);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void SetUIMode(bool uiMode)
		{
			int error;

			error = _SetUIMode(uiMode);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int GetModeSettings(out bool offlineMode)
		{
			int error;

			error = _GetModeSettings(out offlineMode);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetModeSettings(bool offlineMode)
		{
			int error;

			error = _SetModeSettings(offlineMode);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetFileStoreSettings(out string path,
			out bool checkCRLs, out bool autoRefresh,
			out bool ownCRLsOnly, out bool fullAndDeltaCRLs,
			out bool autoDownloadCRLs, out bool saveLoadedCerts,
			out int expireTime)
		{
			int error;

			error = _GetFileStoreSettings(out path, out checkCRLs,
				out autoRefresh, out ownCRLsOnly, out fullAndDeltaCRLs,
				out autoDownloadCRLs, out saveLoadedCerts, out expireTime);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetFileStoreSettings(string path, bool checkCRLs,
			bool autoRefresh, bool ownCRLsOnly, bool fullAndDeltaCRLs,
			bool autoDownloadCRLs, bool saveLoadedCerts, int expireTime)
		{
			int error;

			error = _SetFileStoreSettings(path, checkCRLs,
				autoRefresh, ownCRLsOnly, fullAndDeltaCRLs,
				autoDownloadCRLs, saveLoadedCerts, expireTime);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetProxySettings(out bool useProxy,
			out bool anonymous, out string address,
			out string port, out string user, out string password,
			out bool savePassword)
		{
			int error;

			error = _GetProxySettings(out useProxy,
				out anonymous, out address, out port, out user,
				out password, out savePassword);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetProxySettings(bool useProxy,
			bool anonymous, string address, string port,
			string user, string password, bool savePassword)
		{
			int error;

			error = _SetProxySettings(useProxy,
				anonymous, address, port, user,
				password, savePassword);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetOCSPSettings(out bool useOCSP,
			out bool beforeStore, out string address, out string port)
		{
			int error;

			error = _GetOCSPSettings(out useOCSP,
				out beforeStore, out address, out port);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetOCSPSettings(bool useOCSP,
			bool beforeStore, string address, string port)
		{
			int error;

			error = _SetOCSPSettings(useOCSP,
				beforeStore, address, port);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetTSPSettings(out bool getStamps,
			out string address, out string port)
		{
			int error;

			error = _GetTSPSettings(out getStamps,
				out address, out port);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetTSPSettings(bool getStamps,
			string address, string port)
		{
			int error;

			error = _SetTSPSettings(getStamps,
				address, port);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetLDAPSettings(out bool useLDAP,
			out string address, out string port, out bool anonymous,
			out string user, out string password)
		{
			int error;

			error = _GetLDAPSettings(out useLDAP,
				out address, out port, out anonymous,
				out user, out password);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetLDAPSettings(bool useLDAP,
			string address, string port, bool anonymous,
			string user, string password)
		{
			int error;

			error = _SetLDAPSettings(useLDAP,
				address, port, anonymous,
				user, password);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCMPSettings(out bool useCMP,
			out string address, out string port,
			out string commonName)
		{
			int error;

			error = _GetCMPSettings(out useCMP,
				out address, out  port, out commonName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetCMPSettings(bool useCMP,
			string address, string port, string commonName)
		{
			int error;

			error = _SetCMPSettings(useCMP,
				address, port, commonName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SelectCMPServer(
			out string commonName, out string dns)
		{
			int error;

			error = _SelectCMPServer(out commonName,
				out dns);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetPrivateKeyMediaSettings(
			out EU_KEY_MEDIA_SOURCE_TYPE sourceType,
			out bool showErrors, out int typeIndex,
			out int devIndex, out string password)
		{
			int error;

			error = _GetPrivateKeyMediaSettings(
				out sourceType, out showErrors,
				out typeIndex, out devIndex, out password);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetPrivateKeyMediaSettings(
			EU_KEY_MEDIA_SOURCE_TYPE sourceType, bool showErrors,
			int typeIndex, int devIndex, string password)
		{
			int error;

			error = _SetPrivateKeyMediaSettings(
				sourceType, showErrors, typeIndex,
				devIndex, password);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetRuntimeParameter(string parameterName,
			bool parameterValue)
		{
			int error;

			error = _SetRuntimeParameter(parameterName,
				parameterValue);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetRuntimeParameter(string parameterName,
			int parameterValue)
		{
			int error;

			error = _SetRuntimeParameter(parameterName,
				parameterValue);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetStorageParameter(
			bool isProtected, string name, out string value)
		{
			int error;

			error = _GetStorageParameter(
				isProtected, name, out value);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetStorageParameter(
			bool isProtected, string name, string value)
		{
			int error;

			error = _SetStorageParameter(
				isProtected, name, value);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetOCSPAccessInfoModeSettings(out bool enabled)
		{
			int error;

			error = _GetOCSPAccessInfoModeSettings(out enabled);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetOCSPAccessInfoModeSettings(bool enabled)
		{
			int error;

			error = _SetOCSPAccessInfoModeSettings(enabled);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnumOCSPAccessInfoSettings(int index,
			out string issuerCN, out string address, out string port)
		{
			int error;

			error = _EnumOCSPAccessInfoSettings(
				index, out issuerCN, out address, out port);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetOCSPAccessInfoSettings(string issuerCN,
			out string address, out string port)
		{
			int error;

			error = _GetOCSPAccessInfoSettings(
				issuerCN, out address, out port);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetOCSPAccessInfoSettings(string issuerCN,
			string address, string port)
		{
			int error;

			error = _SetOCSPAccessInfoSettings(issuerCN,
				address, port);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DeleteOCSPAccessInfoSettings(string issuerCN)
		{
			int error;

			error = _DeleteOCSPAccessInfoSettings(issuerCN);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		#endregion

		#region EUSignCP: Certificate and CRLs storage functions

		public static int RefreshFileStore(bool reload)
		{
			int error;

			error = _RefreshFileStore(reload);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void ShowCertificates()
		{
			int error;

			error = _ShowCertificates();
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int SelectCertInfo(
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _SelectCertInfo(out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCertificatesCount(EU_SUBJECT_TYPE subjectType,
			EU_SUBJECT_SUB_TYPE subjectSubType, out int count)
		{
			int error;

			error = _GetCertificatesCount(subjectType,
				subjectSubType, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnumCertificates(
			EU_SUBJECT_TYPE subjectType,
			EU_SUBJECT_SUB_TYPE subjectSubType, int index,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _EnumCertificates(subjectType,
				subjectSubType, index, out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCertificateInfo(string issuer,
			string serial, out EU_CERT_INFO certInfo)
		{
			int error;

			error = _GetCertificateInfo(issuer,
				serial, out certInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCertificateInfoEx(
			string issuer, string serial,
			out EU_CERT_INFO_EX certInfoEx)
		{
			int error;

			error = _GetCertificateInfoEx(issuer,
				serial, out certInfoEx);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCertificate(
			string issuer, string serial,
			out byte[] certificate)
		{
			int error;
			string certificateString = null;

			certificate = new byte[0];

			error = _GetCertificate(issuer, serial,
				ref certificateString, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CheckCertificate(byte[] certificate)
		{
			int error;

			error = _CheckCertificate(certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CheckCertificateByIssuerAndSerialEx(
			string issuer, string serial, out byte[] certificate,
			out int ocspAvailability)
		{
			int error;
			string certificateString = null;

			certificate = new byte[0];

			error = _CheckCertificateByIssuerAndSerialEx(
				issuer, serial, ref certificateString,
				ref certificate, out ocspAvailability);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CheckCertificateByIssuerAndSerial(
			string issuer, string serial, out byte[] certificate)
		{
			int ocspAvailability;

			return CheckCertificateByIssuerAndSerialEx(
				issuer, serial, out certificate,
				out ocspAvailability);
		}

		public static int ParseCertificate(byte[] certificate,
			out EU_CERT_INFO certInfo)
		{
			int error;

			error = _ParseCertificate(certificate,
				out certInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ParseCertificateEx(byte[] certificate,
			out EU_CERT_INFO_EX certInfo)
		{
			int error;

			error = _ParseCertificateEx(certificate,
				out certInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SaveCertificate(
			byte[] certificate)
		{
			int error;

			error = _SaveCertificate(
				certificate, false);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DeleteCertificate(
			string issuer, string serial)
		{
			int error;

			error = _DeleteCertificate(
				issuer, serial);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SaveCertificates(
			byte[] certificates)
		{
			int error;

			error = _SaveCertificate(
				certificates, true);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsCertificates(
			byte[] certificates)
		{
			int error;

			error = _IsCertificates(
				certificates);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsCertificatesFile(
			string fileName)
		{
			int error;

			error = _IsCertificatesFile(fileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void ShowOwnCertificate()
		{
			int error;

			error = _ShowOwnCertificate();
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int EnumOwnCertificates(int index,
			out EU_CERT_INFO_EX certInfo)
		{
			int error;

			error = _EnumOwnCertificates(index,
				out certInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetOwnCertificate(
			out byte[] certificate)
		{
			int error;
			string certificateString = null;

			certificate = new byte[0];

			error = _GetOwnCertificate(
				ref certificateString, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetOwnEnvelopCertificate(
			int publicKeyType, out string issuer, out string serial)
		{
			int error;
			IEUSignCP.EU_CERT_INFO_EX info;
			int index = 0;

			issuer = "";
			serial = "";

			while (true)
			{
				error = EnumOwnCertificates(index, out info);
				if (error != EU_ERROR_NONE)
					break;

				if ((info.publicKeyType == publicKeyType) &&
					(info.keyUsageType & IEUSignCP.EU_KEY_USAGE_KEY_AGREEMENT) ==
					IEUSignCP.EU_KEY_USAGE_KEY_AGREEMENT)
				{
					issuer = info.issuer;
					serial = info.serial;

					break;
				}
				else
				{
					index++;
				}
			}

			return error;
		}

		public static void ShowCRLs()
		{
			int error;

			error = _ShowCRLs();
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int GetCRLsCount(
			out int count)
		{
			int error;

			error = _GetCRLsCount(out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnumCRLs(int index,
			out EU_CRL_INFO crlInfo)
		{
			int error;

			error = _EnumCRLs(index,
				out crlInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCRLDetailedInfo(
			string issuer, int crlNumber,
			out EU_CRL_DETAILED_INFO crlDetailedInfo)
		{
			int error;

			error = _GetCRLDetailedInfo(
				issuer, crlNumber, out crlDetailedInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SaveCRL(
			bool isFullCRL, byte[] crl)
		{
			int error;

			error = _SaveCRL(isFullCRL, crl);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ParseCRL(byte[] crl,
			out EU_CRL_DETAILED_INFO crlDetailedInfo)
		{
			int error;

			error = _ParseCRLInfo(crl,
				out crlDetailedInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCertificateByEmail(string email,
			int certKeyType, int keyUsage, SYSTEMTIME onTime,
			out string issuer, out string serial)
		{
			int error;

			error = _GetCertificateByEmail(email,
				certKeyType, keyUsage, onTime,
				out issuer, out serial);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCertificateByNBUCode(string NBUCode,
			int certKeyType, int keyUsage, SYSTEMTIME onTime,
			out string issuer, out string serial)
		{
			int error;

			error = _GetCertificateByNBUCode(NBUCode,
				certKeyType, keyUsage, onTime,
				out issuer, out serial);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCertificatesByKeyInfo(
			byte[] privKeyInfo, string[] CMPServers,
			string[] CMPServersPorts, out byte[] certificates)
		{
			int error;

			error = _GetCertificatesByKeyInfo(privKeyInfo,
				CMPServers, CMPServersPorts, out certificates);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCertificatesFromLDAPByEDRPOUCode(
			string edrpouCode, int certKeyType, int keyUsage,
			string[] LDAPServers, string[] LDAPServersPorts,
			out byte[] certificates)
		{
			int error;

			error = _GetCertificatesFromLDAPByEDRPOUCode(
				edrpouCode, certKeyType, keyUsage,
				LDAPServers, LDAPServersPorts, out certificates);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetCRInfo(byte[] request,
			out EU_CR_INFO crInfo)
		{
			int error;

			error = _GetCRInfo(request, out crInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}


		#endregion

		#region EUSignCP: KeyMedia and private key functions

		public static int GetPrivatekeyMedia(out EU_KEY_MEDIA keyMedia)
		{
			int error;

			error = _GetPrivatekeyMedia(out keyMedia);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetPrivatekeyMediaEx(string caption,
			out EU_KEY_MEDIA keyMedia)
		{
			int error;

			error = _GetPrivatekeyMediaEx(caption, out keyMedia);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnumKeyMediaTypes(int typeIndex,
			out string typeDescription)
		{
			int error;

			error = _EnumKeyMediaTypes(typeIndex,
				out typeDescription);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnumKeyMediaDevices(
			int typeIndex, int deviceIndex,
			out string deviceDescription)
		{
			int error;

			error = _EnumKeyMediaDevices(typeIndex,
				deviceIndex, out deviceDescription);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static bool IsPrivateKeyReaded()
		{
			int error;
			bool isPrivKeyReaded;

			error = _IsPrivateKeyReaded(out isPrivKeyReaded);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return isPrivKeyReaded;
		}

		public static void ResetPrivateKey()
		{
			int error;

			error = _ResetPrivateKey();
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int ReadPrivateKey(EU_KEY_MEDIA keyMedia,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _ReadPrivateKey(keyMedia, out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ReadPrivateKeyBinary(byte[] privateKey,
			string password, out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _ReadPrivateKeyBinary(privateKey,
				password, out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ReadPrivateKeyFile(string privateKeyFileName,
			string password, out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _ReadPrivateKeyFile(privateKeyFileName,
				password, out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ReadFixedPrivateKey(
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _ReadFixedPrivateKey(out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void CtxFreePrivateKey(IntPtr privateKeyContext)
		{
			int error;

			error = _CtxFreePrivateKey(privateKeyContext);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int CtxReadPrivateKey(IntPtr context,
			EU_KEY_MEDIA keyMedia, out IntPtr privateKeyContext,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _CtxReadPrivateKey(context, keyMedia,
				out privateKeyContext, out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxReadPrivateKeyBinary(
			IntPtr context, byte[] privateKey, string password,
			out IntPtr privKeyContext, out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _CtxReadPrivateKeyBinary(
				context, privateKey, password,
				out privKeyContext, out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxReadPrivateKeyFile(IntPtr context,
			string privateKeyFileName, string password,
			out IntPtr privateKeyContext,
			out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _CtxReadPrivateKeyFile(context,
				privateKeyFileName, password, out privateKeyContext,
				out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsHardwareKeyMedia(
			EU_KEY_MEDIA keyMedia, out bool isHardware)
		{
			int error;

			error = _IsHardwareKeyMedia(
				false, keyMedia, out isHardware);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsHardwareKeyMedia(
			out bool isHardware)
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA();

			error = _IsHardwareKeyMedia(
				true, keyMedia, out isHardware);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsPrivateKeyExists(
			EU_KEY_MEDIA keyMedia, out bool isExists)
		{
			int error;

			error = _IsPrivateKeyExists(
				false, keyMedia, out isExists);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsPrivateKeyExists(
			out bool isExists)
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA();

			error = _IsPrivateKeyExists(
				true, keyMedia, out isExists);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GeneratePrivateKeySilently(EU_KEY_MEDIA keyMedia,
			int UAKeysType, int UADSKeysSpec, int UAKEPKeysSpec,
			string UAParamsPath, int internationalKeysType,
			int internationalKeysSpec, string internationalParamsPath,
			ref byte[] privKeyInfo,
			ref byte[] UARequest, ref string UAReqFileName,
			ref byte[] UAKEPRequest, ref string UAKEPReqFileName,
			ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			int error;
			byte[] privKey = null;
			EU_USER_INFO userInfo = new EU_USER_INFO();

			error = _GeneratePrivateKey(false, false, keyMedia,
				UAKeysType, UADSKeysSpec, UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath,
				false, userInfo, null,
				ref privKey, ref privKeyInfo,
				ref UARequest, ref UAReqFileName, ref UAKEPRequest,
				ref UAKEPReqFileName, ref internationalRequest,
				ref internationalReqFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GeneratePrivateKeyGUI(int UAKeysType,
			int UADSKeysSpec, int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath, ref byte[] privKeyInfo,
			ref byte[] UARequest, ref string UAReqFileName,
			ref byte[] UAKEPRequest, ref string UAKEPReqFileName,
			ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA();
			EU_USER_INFO userInfo = new EU_USER_INFO();
			byte[] privKey = null;

			error = _GeneratePrivateKey(true, false, keyMedia,
				UAKeysType, UADSKeysSpec, UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath,
				false, userInfo, null,
				ref privKey, ref privKeyInfo,
				ref UARequest, ref UAReqFileName, ref UAKEPRequest,
				ref UAKEPReqFileName, ref internationalRequest,
				ref internationalReqFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GeneratePrivateKeyBLOB(string password,
			int UAKeysType, int UADSKeysSpec,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath,
			ref byte[] privKey, ref byte[] privKeyInfo,
			ref byte[] UARequest, ref string UAReqFileName,
			ref byte[] UAKEPRequest, ref string UAKEPReqFileName,
			ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA(0, 0, password);
			EU_USER_INFO userInfo = new EU_USER_INFO();

			error = _GeneratePrivateKey(false, false, keyMedia,
				UAKeysType, UADSKeysSpec, UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath,
				false, userInfo, null,
				ref privKey, ref privKeyInfo,
				ref UARequest, ref UAReqFileName, ref UAKEPRequest,
				ref UAKEPReqFileName, ref internationalRequest,
				ref internationalReqFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GeneratePrivateKeyGUIEx(int UAKeysType,
			int UADSKeysSpec, int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath,
			EU_USER_INFO userInfo, string extKeyUsages,
			ref byte[] privKeyInfo,
			ref byte[] UARequest, ref string UAReqFileName,
			ref byte[] UAKEPRequest, ref string UAKEPReqFileName,
			ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA();
			byte[] privKey = null;

			error = _GeneratePrivateKey(true, false, keyMedia,
				UAKeysType, UADSKeysSpec, UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath,
				true, userInfo, extKeyUsages,
				ref privKey, ref privKeyInfo,
				ref UARequest, ref UAReqFileName, ref UAKEPRequest,
				ref UAKEPReqFileName, ref internationalRequest,
				ref internationalReqFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GeneratePrivateKeyBLOBEx(string password,
			int UAKeysType, int UADSKeysSpec,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath,
			EU_USER_INFO userInfo, string extKeyUsages,
			ref byte[] privKey, ref byte[] privKeyInfo,
			ref byte[] UARequest, ref string UAReqFileName,
			ref byte[] UAKEPRequest, ref string UAKEPReqFileName,
			ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA(0, 0, password);

			error = _GeneratePrivateKey(false, false, keyMedia,
				UAKeysType, UADSKeysSpec, UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath,
				true, userInfo, extKeyUsages,
				ref privKey, ref privKeyInfo,
				ref UARequest, ref UAReqFileName, ref UAKEPRequest,
				ref UAKEPReqFileName, ref internationalRequest,
				ref internationalReqFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GeneratePrivateKeySilentlyEx(EU_KEY_MEDIA keyMedia,
			bool formatKeyMedia, int UAKeysType, int UADSKeysSpec,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath,
			ref byte[] privKeyInfo, ref byte[] UARequest,
			ref string UAReqFileName, ref byte[] UAKEPRequest,
			ref string UAKEPReqFileName, ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			int error;
			byte[] privKey = null;
			EU_USER_INFO userInfo = new EU_USER_INFO();

			error = _GeneratePrivateKey(false, formatKeyMedia,
				keyMedia, UAKeysType, UADSKeysSpec,
				UAKEPKeysSpec, UAParamsPath, internationalKeysType,
				internationalKeysSpec, internationalParamsPath,
				false, userInfo, null,
				ref privKey, ref privKeyInfo, ref UARequest,
				ref UAReqFileName, ref UAKEPRequest, ref UAKEPReqFileName,
				ref internationalRequest, ref internationalReqFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GeneratePrivateKeySilentlyEx(EU_KEY_MEDIA keyMedia,
			bool formatKeyMedia, int UAKeysType, int UADSKeysSpec,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			EU_USER_INFO userInfo, string extKeyUsages,
			string internationalParamsPath,
			ref byte[] privKeyInfo, ref byte[] UARequest,
			ref string UAReqFileName, ref byte[] UAKEPRequest,
			ref string UAKEPReqFileName, ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			int error;
			byte[] privKey = null;

			error = _GeneratePrivateKey(false, formatKeyMedia,
				keyMedia, UAKeysType, UADSKeysSpec,
				UAKEPKeysSpec, UAParamsPath, internationalKeysType,
				internationalKeysSpec, internationalParamsPath,
				true, userInfo, extKeyUsages,
				ref privKey, ref privKeyInfo, ref UARequest,
				ref UAReqFileName, ref UAKEPRequest, ref UAKEPReqFileName,
				ref internationalRequest, ref internationalReqFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int MakeNewCertificateGUI(
			int UAKeysType, int UADSKeysSpec, bool useUADSKeyAsKEP,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath)
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA(0, 0, "");
			byte[] newPrivateKey = null;

			error = _MakeNewCertificate(true, keyMedia, null, "",
				UAKeysType, UADSKeysSpec, useUADSKeyAsKEP, 
				UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath, true, keyMedia, 
				"", ref newPrivateKey);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int MakeNewCertificateSilently(
			EU_KEY_MEDIA oldKeyMedia, 
			int UAKeysType, int UADSKeysSpec, bool useUADSKeyAsKEP,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath, EU_KEY_MEDIA newKeyMedia)
		{
			int error;
			byte[] newPrivateKey = null;

			error = _MakeNewCertificate(false, oldKeyMedia, null, "",
				UAKeysType, UADSKeysSpec, useUADSKeyAsKEP,
				UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath, false, newKeyMedia,
				"", ref newPrivateKey);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int MakeNewCertificateBLOB(
			byte[] oldPrivateKey, string oldPrivateKeyPassword,
			int UAKeysType, int UADSKeysSpec, bool useUADSKeyAsKEP,
			int UAKEPKeysSpec, string UAParamsPath,
			int internationalKeysType, int internationalKeysSpec,
			string internationalParamsPath,
			string newPrivateKeyPassword, out byte[] newPrivateKey)
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA(0, 0, "");
			newPrivateKey = new byte[0];

			error = _MakeNewCertificate(false, keyMedia, 
				oldPrivateKey, oldPrivateKeyPassword,
				UAKeysType, UADSKeysSpec, useUADSKeyAsKEP,
				UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath, false, keyMedia,
				newPrivateKeyPassword, ref newPrivateKey);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetKeyMediaPassword()
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA();

			error = SetKeyMediaPassword(true, keyMedia);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SetKeyMediaPassword(EU_KEY_MEDIA keyMedia)
		{
			int error;

			error = SetKeyMediaPassword(false, keyMedia);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ChangePrivateKeyPassword()
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA();

			error = _ChangePrivateKeyPassword(
				true, keyMedia, null);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ChangePrivateKeyPassword(
			EU_KEY_MEDIA keyMedia, string newPassword)
		{
			int error;

			error = _ChangePrivateKeyPassword(
				false, keyMedia, newPassword);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ChangeSoftwarePrivateKeyPassword(
			byte[] privKeySource, string oldPassword,
			string newPassword, out byte[] privKeyTarget)
		{
			int error;

			error = _ChangeSoftwarePrivateKeyPassword(
				privKeySource, oldPassword,
				newPassword, out privKeyTarget);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int BackupPrivateKey()
		{
			int error;
			EU_KEY_MEDIA sourceKeyMedia = new EU_KEY_MEDIA();
			EU_KEY_MEDIA targetKeyMedia = new EU_KEY_MEDIA();

			error = _BackupPrivateKey(
				true, sourceKeyMedia, targetKeyMedia);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int BackupPrivateKey(
			EU_KEY_MEDIA sourceKeyMedia,
			EU_KEY_MEDIA targetKeyMedia)
		{
			int error;

			error = _BackupPrivateKey(
				false, sourceKeyMedia, targetKeyMedia);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DestroyPrivateKey()
		{
			int error;
			EU_KEY_MEDIA keyMedia = new EU_KEY_MEDIA();

			error = _DestroyPrivateKey(
				true, keyMedia);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DestroyPrivateKey(
			EU_KEY_MEDIA keyMedia)
		{
			int error;

			error = _DestroyPrivateKey(
				false, keyMedia);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetKeyInfo(EU_KEY_MEDIA keyMedia,
			out byte[] privKeyInfo)
		{
			int error;

			error = _GetKeyInfo(keyMedia,
				out privKeyInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetKeyInfoBinary(byte[] privateKey,
			string password, out byte[] privKeyInfo)
		{
			int error;

			error = _GetKeyInfoBinary(privateKey,
				password, out privKeyInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetKeyInfoFile(string privateKeyFile,
			string password, out byte[] privKeyInfo)
		{
			int error;

			error = _GetKeyInfoFile(privateKeyFile,
				password, out privKeyInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnumJKSPrivateKeys(byte[] container,
			int index, out string keyAlias)
		{
			int error;

			error = _EnumJKSPrivateKeys(container,
				index, out keyAlias);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnumJKSPrivateKeysFile(string containerFile,
			int index, out string keyAlias)
		{
			int error;

			error = _EnumJKSPrivateKeysFile(containerFile,
				index, out keyAlias);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetJKSPrivateKey(byte[] container,
			string keyAlias, out byte[] privateKey, out byte[][] certificates)
		{
			int error;

			error = _GetJKSPrivateKey(container,
				keyAlias, out privateKey, out certificates);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetJKSPrivateKeyFile(string containerFile,
			string keyAlias, out byte[] privateKey, out byte[][] certificates)
		{
			int error;

			error = _GetJKSPrivateKeyFile(containerFile,
				keyAlias, out privateKey, out certificates);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetKeyMediaDeviceInfo(
			EU_KEY_MEDIA keyMedia, out EU_KEY_MEDIA_DEVICE_INFO info)
		{
			int error;

			error = _GetKeyMediaDeviceInfo(keyMedia, out info);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetOwnCertificate(
			IntPtr privateKeyContext, int certKeyType, int keyUsage,
			out EU_CERT_INFO_EX info, out byte[] certificate)
		{
			int error;

			error = _CtxGetOwnCertificate(
				privateKeyContext, certKeyType,
				keyUsage, out info, out certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnumOwnCertificates(
			IntPtr privateKeyContext, int index,
			out EU_CERT_INFO_EX info, out byte[] certificate)
		{
			int error;

			error = _CtxEnumOwnCertificates(
				privateKeyContext, index,
				out info, out certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnumPrivateKeyInfo(
			IntPtr privateKeyContext, int index,
			out EU_PRIVATE_KEY_INFO info)
		{
			int error;

			error = _CtxEnumPrivateKeyInfo(
				privateKeyContext, index, out info);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxExportPrivateKeyContainer(
			IntPtr privateKeyContext, string password,
			string keyID, out byte[] container)
		{
			int error;

			error = _CtxExportPrivateKeyContainer(
				privateKeyContext, password, keyID, out container);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxExportPrivateKeyPFXContainer(
			IntPtr privateKeyContext, string password,
			bool exportCerts, bool[] trustedKeyIDs, string[] keyIDs,
			out byte[] container)
		{
			int error;

			error = _CtxExportPrivateKeyPFXContainer(
				privateKeyContext, password, exportCerts,
				trustedKeyIDs, keyIDs, out container);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxExportPrivateKeyContainerFile(
			IntPtr privateKeyContext, string password,
			string keyID, string fileName)
		{
			int error;

			error = _CtxExportPrivateKeyContainerFile(
				privateKeyContext, password, keyID, fileName);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxExportPrivateKeyPFXContainerFile(
			IntPtr privateKeyContext, string password,
			bool exportCerts, bool[] trustedKeyIDs,
			string[] keyIDs, string fileName)
		{
			int error;

			error = _CtxExportPrivateKeyPFXContainerFile(
				privateKeyContext, password, exportCerts,
				trustedKeyIDs, keyIDs, fileName);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetCertificateFromPrivateKey(
			IntPtr privateKeyContext, string keyID,
			out EU_CERT_INFO_EX info, out byte[] certificate)
		{
			int error;

			error = _CtxGetCertificateFromPrivateKey(
				privateKeyContext, keyID, out info, out certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ChangeOwnCertificatesStatus(
			int requestType, int revocationReason)
		{
			int error;

			error = _ChangeOwnCertificatesStatus(
				requestType, revocationReason);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxChangeOwnCertificatesStatus(
			IntPtr privateKeyContext, int requestType, int revocationReason)
		{
			int error;

			error = _CtxChangeOwnCertificatesStatus(
				privateKeyContext, requestType, revocationReason);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxIsNamedPrivateKeyExists(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			out bool isExists)
		{
			int error;

			error = _CtxIsNamedPrivateKeyExists(
				context, keyMedia, namedPrivateKeyLabel,
				namedPrivateKeyPassword, out isExists);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGenerateNamedPrivateKey(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			int UAKeysType, int UADSKeysSpec, int UAKEPKeysSpec,
			string UAParamsPath, int internationalKeysType,
			int internationalKeysSpec, string internationalParamsPath,
			ref byte[] UARequest, ref string UAReqFileName,
			ref byte[] UAKEPRequest, ref string UAKEPReqFileName,
			ref byte[] internationalRequest,
			ref string internationalReqFileName)
		{
			int error;

			error = _CtxGenerateNamedPrivateKey(context, keyMedia,
				namedPrivateKeyLabel, namedPrivateKeyPassword,
				UAKeysType, UADSKeysSpec, UAKEPKeysSpec, UAParamsPath,
				internationalKeysType, internationalKeysSpec,
				internationalParamsPath,
				ref UARequest, ref UAReqFileName, ref UAKEPRequest,
				ref UAKEPReqFileName, ref internationalRequest,
				ref internationalReqFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxReadNamedPrivateKey(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			out IntPtr privateKeyContext, out EU_CERT_OWNER_INFO certOwnerInfo)
		{
			int error;

			error = _CtxReadNamedPrivateKey(
				context, keyMedia, namedPrivateKeyLabel,
				namedPrivateKeyPassword, out privateKeyContext,
				out certOwnerInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxDestroyNamedPrivateKey(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword)
		{
			int error;

			error = _CtxDestroyNamedPrivateKey(
				context, keyMedia, namedPrivateKeyLabel,
				namedPrivateKeyPassword);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxChangeNamedPrivateKeyPassword(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			string namedPrivateKeyNewPassword)
		{
			int error;

			error = _CtxChangeNamedPrivateKeyPassword(
				context, keyMedia, namedPrivateKeyLabel,
				namedPrivateKeyPassword, namedPrivateKeyNewPassword);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetNamedPrivateKeyInfo(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			string namedPrivateKeyLabel, string namedPrivateKeyPassword,
			out byte[] keyInfo)
		{
			int error;

			error = _CtxGetNamedPrivateKeyInfo(
				context, keyMedia, namedPrivateKeyLabel,
				namedPrivateKeyPassword, out keyInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnumNamedPrivateKeys(
			IntPtr context, EU_KEY_MEDIA keyMedia,
			int index, out string namedPrivateKeyLabel)
		{
			int error;

			error = _CtxEnumNamedPrivateKeys(
				context, keyMedia, index, out namedPrivateKeyLabel);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		#endregion

		#region EUSignCP: Hash functions

		public static int HashData(string data, out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _HashData(data, null, ref hash,
				ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashData(string data, out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _HashData(data, null, ref hashString,
				ref hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashData(byte[] data, out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _HashData(null, data, ref hashString,
				ref hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashData(byte[] data, out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _HashData(null, data, ref hash,
				ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataContinue(string data)
		{
			int error;

			error = _HashDataContinue(data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataContinue(byte[] data)
		{
			int error;

			error = _HashDataContinue(null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataEnd(out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _HashDataEnd(ref hash,
				ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataEnd(out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _HashDataEnd(ref hashString,
				ref hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashFile(
			string fileName, out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _HashFile(fileName,
				ref hash, ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashFile(
			string fileName, out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _HashFile(fileName,
				ref hashString, ref hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataWithParams(byte[] certificate,
			string dataString, byte[] dataBinary,
			ref string hashString, ref byte[] hashBinary)
		{
			int error;

			error = HashDataWithParams(certificate,
				dataString, dataBinary, ref hashString, ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataWithParams(
			byte[] certificate, string data, out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _HashDataWithParams(certificate, data,
				null, ref hash, ref hashBinary);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int HashDataWithParams(
			byte[] certificate, string data, out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _HashDataWithParams(certificate, data,
				null, ref hashString, ref hash);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int HashDataWithParams(
			byte[] certificate, byte[] data, out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _HashDataWithParams(certificate, null,
				data, ref hashString, ref hash);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int HashDataWithParams(
			byte[] certificate, byte[] data, out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _HashDataWithParams(certificate, null,
				data, ref hash, ref hashBinary);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int HashDataBeginWithParams(
			byte[] certificate)
		{
			int error;

			error = _HashDataBeginWithParams(certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashFileWithParams(
			byte[] certificate, string fileName, ref string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _HashFileWithParams(
				certificate, fileName, ref hash, ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashFileWithParams(
			byte[] certificate, string fileName, ref byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _HashFileWithParams(
				certificate, fileName, ref hashString, ref hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataBeginWithParamsCtx(
			byte[] certificate, out IntPtr context)
		{
			int error;

			error = _HashDataBeginWithParamsCtx(certificate,
				out context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataContinueCtx(
			ref IntPtr context, byte[] data)
		{
			int error;

			error = _HashDataContinueCtx(ref context, data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataContinueCtx(
			ref IntPtr context, string data)
		{
			int error;

			error = _HashDataContinueCtx(ref context, null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataEndCtx(IntPtr context, out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _HashDataEndCtx(context,
				ref hashString, ref hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int HashDataEndCtx(IntPtr context, out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _HashDataEndCtx(context,
				ref hash, ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashData(IntPtr context,
			int hashAlgo, byte[] certificate,
			string data, out string hash)
		{
			int error;
			byte[] hashBinary;

			hash = null;

			error = _CtxHashData(
				context, hashAlgo, certificate,
				data, null, out hashBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(hashBinary, out hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashData(IntPtr context,
			int hashAlgo, byte[] certificate,
			string data, out byte[] hash)
		{
			int error;

			error = _CtxHashData(
				context, hashAlgo, certificate,
				data, null, out hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashData(IntPtr context,
			int hashAlgo, byte[] certificate,
			byte[] data, out byte[] hash)
		{
			int error;

			error = _CtxHashData(
				context, hashAlgo, certificate,
				null, data, out hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashData(IntPtr context,
			int hashAlgo, byte[] certificate,
			byte[] data, out string hash)
		{
			int error;
			byte[] hashBinary;

			hash = null;

			error = _CtxHashData(
				context, hashAlgo, certificate,
				null, data, out hashBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(hashBinary, out hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashFile(IntPtr context,
			int hashAlgo, byte[] certificate,
			string fileName, out string hash)
		{
			int error;
			byte[] hashBinary;

			hash = null;

			error = _CtxHashFile(
				context, hashAlgo, certificate,
				fileName, out hashBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(hashBinary, out hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashFile(IntPtr context,
			int hashAlgo, byte[] certificate,
			string fileName, out byte[] hash)
		{
			int error;

			error = _CtxHashFile(
				context, hashAlgo, certificate,
				fileName, out hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashDataBegin(
			IntPtr context, int hashAlgo,
			byte[] certificate, out IntPtr hashContext)
		{
			int error;

			error = _CtxHashDataBegin(
				context, hashAlgo,
				certificate, out hashContext);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashDataContinue(
			IntPtr hashContext, string data)
		{
			int error;

			error = _CtxHashDataContinue(
				hashContext, data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashDataContinue(
			IntPtr hashContext, byte[] data)
		{
			int error;

			error = _CtxHashDataContinue(
				hashContext, null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashDataEnd(
			IntPtr hashContext, out string hash)
		{
			int error;
			byte[] hashBinary;

			hash = null;

			error = _CtxHashDataEnd(
				hashContext, out hashBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(hashBinary, out hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxHashDataEnd(
			IntPtr hashContext, out byte[] hash)
		{
			int error;

			error = _CtxHashDataEnd(
				hashContext, out hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void CtxFreeHash(IntPtr hashContext)
		{
			int error;

			error = (int)EUCtxFreeHash(hashContext);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);
		}

		#endregion

		#region EUSignCP: Sign functions

		public static void ShowSignInfo(EU_SIGN_INFO signInfo)
		{
			int error;

			error = _ShowSignInfo(signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static void FreeSignInfo(ref EU_SIGN_INFO signInfo)
		{
			int error;

			error = _FreeSignInfo(ref signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static void CtxFreeSignInfo(IntPtr context,
			ref EU_SIGN_INFO signInfo)
		{
			int error;

			error = _FreeSignInfo(ref signInfo, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static bool IsSignedData(byte[] data)
		{
			int error;
			bool isSigned;

			error = _IsSignedData(data, out isSigned);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return isSigned;
		}

		public static bool IsSignedFile(
			string fileName)
		{
			int error;
			bool isSigned;

			error = _IsSignedFile(fileName, out isSigned);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return isSigned;
		}

		public static int GetSignsCount(string sign, out int count)
		{
			int error;

			error = _GetSignsCount(sign, null, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetSignsCount(byte[] sign, out int count)
		{
			int error;

			error = _GetSignsCount(null, sign, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetSignerInfo(int signIndex,
			string signString, byte[] signBinary,
			out EU_CERT_INFO_EX info, ref byte[] certificate)
		{
			int error;

			error = _GetSignerInfo(signIndex,
				signString, signBinary, out info,
				ref certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetSignerInfo(int signIndex, string sign,
			out EU_CERT_INFO_EX info, ref byte[] certificate)
		{
			int error;

			error = _GetSignerInfo(signIndex, sign, null,
				out info, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetSignerInfo(int signIndex, byte[] sign,
			out EU_CERT_INFO_EX info, ref byte[] certificate)
		{
			int error;

			error = _GetSignerInfo(signIndex, null, sign,
				out info, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetSignTimeInfo(int signIndex,
			string signString, byte[] signBinary, out EU_TIME_INFO info)
		{
			int error;

			error = _GetSignTimeInfo(signIndex,
				signString, signBinary, out info);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetSignTimeInfo(int signIndex,
			string sign, out EU_TIME_INFO info)
		{
			int error;

			error = _GetSignTimeInfo(signIndex,
				sign, null, out info);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetSignTimeInfo(int signIndex,
			byte[] sign, out EU_TIME_INFO info)
		{
			int error;

			error = _GetSignTimeInfo(signIndex,
				null, sign, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetFileSignsCount(
			string fileNameWithSign, out int count)
		{
			int error;

			error = _GetFileSignsCount(
				fileNameWithSign, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetFileSignerInfo(int signIndex,
			string fileNameWithSign, out EU_CERT_INFO_EX info,
			ref byte[] certificate)
		{
			int error;

			error = _GetFileSignerInfo(
				signIndex, fileNameWithSign,
				out info, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetFileSignTimeInfo(int signIndex,
			string fileNameWithSign, out EU_TIME_INFO info)
		{
			int error;

			error = _GetFileSignTimeInfo(
				signIndex, fileNameWithSign, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetDataHashFromSignedData(
			int signIndex, string sign, out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _GetDataHashFromSignedData(signIndex,
				sign, null, ref hash, ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetDataHashFromSignedData(
			int signIndex, byte[] sign, out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _GetDataHashFromSignedData(signIndex,
				null, sign, ref hashString, ref hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetDataHashFromSignedFile(
			int signIndex, string fileNameWithSignedData, 
			out string hash)
		{
			int error;
			byte[] hashBinary = null;

			hash = "";

			error = _GetDataHashFromSignedFile(signIndex,
				fileNameWithSignedData, ref hash, ref hashBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetDataHashFromSignedFile(
			int signIndex, string fileNameWithSignedData, 
			out byte[] hash)
		{
			int error;
			string hashString = null;

			hash = new byte[0];

			error = _GetDataHashFromSignedFile(signIndex,
				fileNameWithSignedData, ref hashString, ref hash);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignData(string data, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _SignData(data, null, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignData(string data, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _SignData(data, null, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignData(byte[] data, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _SignData(null, data, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignData(byte[] data, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _SignData(null, data, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyData(string data,
			string sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyData(data, null,
				sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyData(string data,
			byte[] sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyData(data, null,
				null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyData(byte[] data,
			byte[] sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyData(null, data,
				null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyData(byte[] data,
			string sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyData(null, data,
				sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataOnTimeEx(
			string data, int signIndex, string sign,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataOnTimeEx(
				data, null, signIndex, sign, null,
				onTime, offline, noCRL, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataOnTimeEx(
			string data, int signIndex, byte[] sign,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataOnTimeEx(
				data, null, signIndex, null, sign,
				onTime, offline, noCRL, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataOnTimeEx(
			byte[] data, int signIndex, byte[] sign,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataOnTimeEx(
				null, data, signIndex, null, sign,
				onTime, offline, noCRL, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataOnTimeEx(
			byte[] data, int signIndex, string sign,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataOnTimeEx(
				null, data, signIndex, sign, null,
				onTime, offline, noCRL, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataWithParams(
			string data, int signIndex, string sign,
			string onTime, bool offline, bool noCRL,
			byte[] signerCert, bool noSignerCertCheck,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataWithParams(
				data, null, signIndex, sign, null,
				onTime, offline, noCRL,
				signerCert, noSignerCertCheck, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataWithParams(
			string data, int signIndex, byte[] sign,
			string onTime, bool offline, bool noCRL,
			byte[] signerCert, bool noSignerCertCheck,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataWithParams(
				data, null, signIndex, null, sign,
				onTime, offline, noCRL,
				signerCert, noSignerCertCheck, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataWithParams(
			byte[] data, int signIndex, byte[] sign,
			string onTime, bool offline, bool noCRL,
			byte[] signerCert, bool noSignerCertCheck,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataWithParams(
				null, data, signIndex, null, sign,
				onTime, offline, noCRL,
				signerCert, noSignerCertCheck, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataWithParams(
			byte[] data, int signIndex, string sign,
			string onTime, bool offline, bool noCRL,
			byte[] signerCert, bool noSignerCertCheck,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataWithParams(
				null, data, signIndex, sign, null,
				onTime, offline, noCRL,
				signerCert, noSignerCertCheck, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataContinue(string data)
		{
			int error;

			error = _SignDataContinue(data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataContinue(byte[] data)
		{
			int error;

			error = _SignDataContinue(null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataEnd(out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _SignDataEnd(ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataEnd(out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _SignDataEnd(ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataBegin(string sign)
		{
			int error;

			error = _VerifyDataBegin(sign, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataBegin(byte[] sign)
		{
			int error;

			error = _VerifyDataBegin(null, sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataOnTimeBeginEx(
			int signIndex, string sign,
			string onTime, bool offline, bool noCRL)
		{
			int error;

			error = _VerifyDataOnTimeBeginEx(signIndex,
				sign, null, onTime, offline, noCRL);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataOnTimeBeginEx(
			int signIndex, byte[] sign,
			string onTime, bool offline, bool noCRL)
		{
			int error;

			error = _VerifyDataOnTimeBeginEx(signIndex,
				null, sign, onTime, offline, noCRL);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataContinue(string data)
		{
			int error;

			error = _VerifyDataContinue(data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataContinue(byte[] data)
		{
			int error;

			error = _VerifyDataContinue(null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataEnd(out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataEnd(out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignFile(string fileName,
			string fileNameWithSign, bool externalSign)
		{
			int error;

			error = _SignFile(fileName,
				fileNameWithSign, externalSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyFile(string fileNameWithSign,
			string fileName, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyFile(fileNameWithSign,
				fileName, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyFileOnTimeEx(int signIndex,
			string fileNameWithSign, string fileName,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyFileOnTimeEx(signIndex,
				fileNameWithSign, fileName,
				onTime, offline, noCRL, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataInternal(bool appendCert, string data,
			out string signedData)
		{
			int error;
			byte[] signedDataBinary = null;

			signedData = "";

			error = _SignDataInternal(appendCert,
				data, null, ref signedData, ref signedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataInternal(bool appendCert, string data,
			out byte[] signedData)
		{
			int error;
			string signedDataString = null;

			signedData = new byte[0];

			error = _SignDataInternal(appendCert,
				data, null, ref signedDataString, ref signedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataInternal(bool appendCert, byte[] data,
			out byte[] signedData)
		{
			int error;
			string signedDataString = null;

			signedData = new byte[0];

			error = _SignDataInternal(appendCert,
				null, data, ref signedDataString, ref signedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataInternal(bool appendCert, byte[] data,
			out string signedData)
		{
			int error;
			byte[] signedDataBinary = new byte[0];

			signedData = "";

			error = _SignDataInternal(appendCert,
				null, data, ref signedData, ref signedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternal(string signedData,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataInternal(signedData, null,
				out data, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternal(byte[] signedData,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataInternal(null, signedData,
				out data, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternalOnTimeEx(
			int signIndex, string signedData,
			string onTime, bool offline, bool noCRL,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataInternalOnTimeEx(
				signIndex, signedData, null,
				onTime, offline, noCRL,
				out data, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternalOnTimeEx(
			int signIndex, byte[] signedData,
			string onTime, bool offline, bool noCRL,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataInternalOnTimeEx(
				signIndex, null, signedData,
				onTime, offline, noCRL,
				out data, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternalWithParams(
			int signIndex, string signedData,
			string onTime, bool offline, bool noCRL,
			byte[] signerCert, bool noSignerCertCheck,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataInternalWithParams(
				signIndex, signedData, null,
				onTime, offline, noCRL,
				signerCert, noSignerCertCheck,
				out data, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternalWithParams(
			int signIndex, byte[] signedData,
			string onTime, bool offline, bool noCRL,
			byte[] signerCert, bool noSignerCertCheck,
			out byte[] data, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataInternalWithParams(
				signIndex, null, signedData,
				onTime, offline, noCRL,
				signerCert, noSignerCertCheck,
				out data, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignHash(string hash, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _SignHash(hash, null, ref sign,
				ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignHash(string hash, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _SignHash(hash, null,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignHash(byte[] hash, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _SignHash(null, hash,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignHash(byte[] hash, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _SignHash(null, hash, ref sign,
				ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHash(string hash, string sign,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHash(hash, null,
				sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHash(string hash, byte[] sign,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHash(hash, null,
				null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHash(byte[] hash, byte[] sign,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHash(null, hash,
				null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHash(byte[] hash, string sign,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHash(null, hash,
				sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHashOnTimeEx(
			string hash, int signIndex, string sign,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHashOnTimeEx(
				hash, null, signIndex,
				sign, null, onTime, offline, noCRL,
				out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHashOnTimeEx(
			string hash, int signIndex, byte[] sign,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHashOnTimeEx(
				hash, null, signIndex,
				null, sign, onTime, offline, noCRL,
				out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHashOnTimeEx(
			byte[] hash, int signIndex, byte[] sign,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHashOnTimeEx(
				null, hash, signIndex,
				null, sign, onTime, offline, noCRL,
				out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHashOnTimeEx(
			byte[] hash, int signIndex, string sign,
			string onTime, bool offline, bool noCRL,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHashOnTimeEx(
				null, hash, signIndex,
				sign, null, onTime, offline, noCRL,
				out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignData(string data, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _RawSignData(data, null,
				ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignData(string data, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _RawSignData(data, null,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignData(byte[] data, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _RawSignData(null, data,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignData(byte[] data, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _RawSignData(null, data,
				ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyData(string data,
			string sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyData(data, null,
				sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyData(string data,
			byte[] sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyData(data, null,
				null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyData(byte[] data,
			byte[] sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyData(null, data,
				null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyData(byte[] data,
			string sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyData(null, data,
				sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyDataEx(
			byte[] cert, string data,
			string sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyDataEx(
				cert, data, null, sign, null,
				out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyDataEx(
			byte[] cert, string data,
			byte[] sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyDataEx(
				cert, data, null, null, sign,
				out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyDataEx(
			byte[] cert, byte[] data,
			byte[] sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyDataEx(
				cert, null, data, null, sign,
				out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyDataEx(
			byte[] cert, byte[] data,
			string sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyDataEx(
				cert, null, data, sign, null,
				out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignHash(string hash, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _RawSignHash(hash, null, ref sign,
				ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignHash(string hash, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _RawSignHash(hash, null,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignHash(byte[] hash, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _RawSignHash(null, hash,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignHash(byte[] hash, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _RawSignHash(null, hash, ref sign,
				ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyHash(string hash, string sign,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyHash(hash, null,
				sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyHash(string hash, byte[] sign,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyHash(hash, null,
				null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyHash(byte[] hash, byte[] sign,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyHash(null, hash,
				null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyHash(byte[] hash, string sign,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyHash(null, hash,
				sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawSignFile(string fileName,
			string fileNameWithSign)
		{
			int error;

			error = _RawSignFile(fileName,
				fileNameWithSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawVerifyFile(string fileNameWithSign,
			string fileName, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _RawVerifyFile(fileNameWithSign,
				fileName, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsAlreadySigned(
			string sign, out bool isAlreadySigned)
		{
			int error;

			error = _IsAlreadySigned(
				sign, null, out isAlreadySigned);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsAlreadySigned(
			byte[] sign, out bool isAlreadySigned)
		{
			int error;

			error = _IsAlreadySigned(
				null, sign, out isAlreadySigned);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsFileAlreadySigned(
			string fileNameWithSign, out bool isAlreadySigned)
		{
			int error;

			error = _IsFileAlreadySigned(
				fileNameWithSign, out isAlreadySigned);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSign(string dataString, byte[] dataBinary,
			string previousSignString, byte[] previousSignBinary,
			ref string signString, ref byte[] signBinary)
		{
			int error;

			error = _AppendSign(dataString, dataBinary,
				previousSignString, previousSignBinary,
				ref signString, ref signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSign(
			string data, string previousSign, ref string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _AppendSign(data, null, previousSign,
				null, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSign(
			string data, byte[] previousSign, ref byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _AppendSign(data, null, null,
				previousSign, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSign(
			byte[] data, byte[] previousSign, ref byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _AppendSign(null, data, null, previousSign,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSign(
			byte[] data, string previousSign, ref string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _AppendSign(null, data, previousSign,
				null, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignInternal(bool appendCertificate,
			string previousSignString, byte[] previousSignBinary,
			ref string signString, ref byte[] signBinary)
		{
			int error;

			error = _AppendSignInternal(appendCertificate,
				previousSignString, previousSignBinary,
				ref signString, ref signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignInternal(bool appendCertificate,
			string previousSign, ref string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _AppendSignInternal(appendCertificate,
				previousSign, null, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignInternal(bool appendCertificate,
			string previousSign, ref byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _AppendSignInternal(appendCertificate,
				previousSign, null, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignInternal(bool appendCertificate,
			byte[] previousSign, ref byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _AppendSignInternal(appendCertificate,
				null, previousSign, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignInternal(bool appendCertificate,
			byte[] previousSign, ref string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _AppendSignInternal(appendCertificate,
				null, previousSign, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RemoveSign(int signIndex,
			string previousSignString, byte[] previousSignBinary,
			ref string signString, ref byte[] signBinary)
		{
			int error;

			error = _RemoveSign(signIndex,
				previousSignString, previousSignBinary,
				ref signString, ref signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RemoveSign(int signIndex,
			string previousSign, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _RemoveSign(signIndex,
				previousSign, null, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RemoveSign(int signIndex,
			string previousSign, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _RemoveSign(signIndex,
				previousSign, null, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RemoveSign(int signIndex,
			byte[] previousSign, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _RemoveSign(signIndex,
				null, previousSign, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RemoveSign(int signIndex,
			byte[] previousSign, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _RemoveSign(signIndex,
				null, previousSign, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RemoveSignFile(int signIndex,
			string fileNameWithPreviousSign, string fileNameWithSign)
		{
			int error;

			error = _RemoveSignFile(signIndex,
				fileNameWithPreviousSign, fileNameWithSign);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataSpecific(
			string dataString, byte[] dataBinary, int signIndex,
			string signString, byte[] signBinary, out EU_SIGN_INFO info)
		{
			int error;

			error = _VerifyDataSpecific(dataString, dataBinary,
				signIndex, signString, signBinary, out info);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataSpecific(string data,
			int signIndex, string sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataSpecific(data, null,
				signIndex, sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataSpecific(string data,
			int signIndex, byte[] sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataSpecific(data, null,
				signIndex, null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataSpecific(byte[] data,
			int signIndex, byte[] sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataSpecific(null, data,
				signIndex, null, sign, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataSpecific(byte[] data,
			int signIndex, string sign, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataSpecific(null, data,
				signIndex, sign, null, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternalSpecific(int signIndex,
			string signedDataString, byte[] signedDataBinary,
			out byte[] data, out EU_SIGN_INFO info)
		{
			int error;

			error = _VerifyDataInternalSpecific(signIndex,
				signedDataString, signedDataBinary, out data, out info);
			if (error != IEUSignCP.EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternalSpecific(int signIndex,
			string signedData, out byte[] data, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataInternalSpecific(signIndex,
				signedData, null, out data, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataInternalSpecific(int signIndex,
			byte[] signedData, out byte[] data, out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataInternalSpecific(signIndex,
				null, signedData, out data, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignBegin(string previousSign)
		{
			int error;

			error = _AppendSignBegin(previousSign, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignBegin(byte[] previousSign)
		{
			int error;

			error = _AppendSignBegin(null, previousSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataSpecificBegin(
			int signIndex, string sign)
		{
			int error;

			error = _VerifyDataSpecificBegin(
				signIndex, sign, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataSpecificBegin(
			int signIndex, byte[] sign)
		{
			int error;

			error = _VerifyDataSpecificBegin(
				signIndex, null, sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignFile(string fileName,
			string fileNameWithPreviousSign, string fileNameWithSign,
			bool externalSign)
		{
			int error;

			error = _AppendSignFile(fileName,
				fileNameWithPreviousSign, fileNameWithSign, externalSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyFileSpecific(int signIndex,
			string fileNameWithSign, string fileName,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyFileSpecific(signIndex,
				fileNameWithSign, fileName, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignHash(
			string hashString, byte[] hashBinary,
			string previousSignString, byte[] previousSignBinary,
			ref string signString, ref byte[] signBinary)
		{
			int error;

			error = _AppendSignHash(
				hashString, hashBinary, previousSignString,
				previousSignBinary, ref signString, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignHash(string hash,
			string previousSign, ref string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _AppendSignHash(
				hash, null, previousSign, null,
				ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSignHash(byte[] hash,
			byte[] previousSign, ref byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _AppendSignHash(
				null, hash, null, previousSign,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHashSpecific(
			string hashString, byte[] hashBinary,
			int signIndex, string signString, byte[] signBinary,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyHashSpecific(hashString, hashBinary,
				signIndex, signString, signBinary, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHashSpecific(string hash,
			int signIndex, string sign, out EU_SIGN_INFO info)
		{
			int error;

			error = _VerifyHashSpecific(hash, null, signIndex,
				sign, null, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyHashSpecific(byte[] hash,
			int signIndex, byte[] sign, out EU_SIGN_INFO info)
		{
			int error;

			error = _VerifyHashSpecific(null, hash, signIndex,
				null, sign, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CreateEmptySign(
			byte[] data, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _CreateEmptySign(data,
				ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CreateEmptySign(
			byte[] data, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _CreateEmptySign(data,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CreateSigner(string hash, out string signer)
		{
			int error;
			byte[] signerBinary = null;

			signer = "";

			error = _CreateSigner(hash, null,
				ref signer, ref signerBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CreateSigner(byte[] hash, out byte[] signer)
		{
			int error;
			string signerString = null;
			signer = new byte[0];

			error = _CreateSigner(null, hash,
				ref signerString, ref signer);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSigner(
			string signerString, byte[] signerBinary, byte[] certificate,
			string previousSignString, byte[] previousSignBinary,
			ref string signString, ref byte[] signBinary)
		{
			int error;

			error = _AppendSigner(signerString,
				signerBinary, certificate, previousSignString,
				previousSignBinary, ref signString, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSigner(string signer,
			byte[] certificate, string previousSign, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _AppendSigner(signer, null, certificate,
				previousSign, null, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendSigner(byte[] signer,
			byte[] certificate, byte[] previousSign, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _AppendSigner(null, signer, certificate,
				null, previousSign, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataContinueCtx(
			ref IntPtr context, string data)
		{
			int error;

			error = _SignDataContinueCtx(
				ref context, data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataContinueCtx(
			ref IntPtr context, byte[] data)
		{
			int error;

			error = _SignDataContinueCtx(
				ref context, null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataEndCtx(IntPtr context,
			bool appendCert, out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _SignDataEndCtx(context,
				appendCert, ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataEndCtx(IntPtr context,
			bool appendCert, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _SignDataEndCtx(context,
				appendCert, ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataBeginCtx(
			string sign, out IntPtr context)
		{
			int error;

			error = _VerifyDataBeginCtx(
				sign, null, out context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataBeginCtx(
			byte[] sign, out IntPtr context)
		{
			int error;

			error = _VerifyDataBeginCtx(
				null, sign, out context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataContinueCtx(
			IntPtr context, string data)
		{
			int error;

			error = _VerifyDataContinueCtx(context, data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataContinueCtx(
			IntPtr context, byte[] data)
		{
			int error;

			error = _VerifyDataContinueCtx(context, null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int VerifyDataEndCtx(IntPtr context,
			out EU_SIGN_INFO signInfo)
		{
			int error;

			error = _VerifyDataEndCtx(context, out signInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSA(string data, bool appendCert,
			bool externalSign, out string signedData)
		{
			int error;
			byte[] dataBinary = null;
			byte[] signedDataBinary = null;
			signedData = "";

			error = _SignDataRSA(data, dataBinary, appendCert,
				externalSign, ref signedData, ref signedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSA(string data, bool appendCert,
			bool externalSign, out byte[] signedData)
		{
			int error;
			byte[] dataBinary = null;
			string signedDataString = null;
			signedData = new byte[0];

			error = _SignDataRSA(data, dataBinary, appendCert,
				externalSign, ref signedDataString, ref signedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSA(byte[] data, bool appendCert,
			bool externalSign, out byte[] signedData)
		{
			int error;
			string dataString = null;
			string signedDataString = null;
			signedData = new byte[0];

			error = _SignDataRSA(dataString, data, appendCert,
				externalSign, ref signedDataString, ref signedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSA(byte[] data,
			bool appendCert, bool externalSign,
			out string signedData)
		{
			int error;
			string dataString = null;
			byte[] signedDataBinary = null;
			signedData = "";

			error = _SignDataRSA(dataString, data, appendCert,
				externalSign, ref signedData, ref signedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSAContinue(string data)
		{
			int error;

			error = _SignDataRSAContinue(data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSAContinue(byte[] data)
		{
			int error;

			error = _SignDataRSAContinue(null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSAEnd(bool appendCert,
			out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _SignDataRSAEnd(appendCert,
				ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSAEnd(bool appendCert,
			out byte[] sign)
		{
			int error;
			string signString = null;

			sign = new byte[0];

			error = _SignDataRSAEnd(appendCert,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSAContinueCtx(
			ref IntPtr context, string data)
		{
			int error;

			error = _SignDataRSAContinueCtx(
				ref context, data, null);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSAContinueCtx(
			ref IntPtr context, byte[] data)
		{
			int error;

			error = _SignDataRSAContinueCtx(
				ref context, null, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSAEndCtx(IntPtr context,
			bool appendCert, out string sign)
		{
			int error;
			byte[] signBinary = null;

			sign = "";

			error = _SignDataRSAEndCtx(context, appendCert,
				ref sign, ref signBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignDataRSAEndCtx(IntPtr context,
			bool appendCert, out byte[] sign)
		{
			int error;
			string signString = null;
			sign = new byte[0];

			error = _SignDataRSAEndCtx(context, appendCert,
				ref signString, ref sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SignFileRSA(string fileName,
			string fileNameWithSign, bool externalSign)
		{
			int error;

			error = _SignFileRSA(fileName,
				fileNameWithSign, externalSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsOldFormatSign(
			string data, out bool isOldFormatSign)
		{
			int error;

			error = _IsOldFormatSign(
				data, null, out isOldFormatSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsOldFormatSign(
			byte[] data, out bool isOldFormatSign)
		{
			int error;

			error = _IsOldFormatSign(
				null, data, out isOldFormatSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int IsOldFormatSignFile(
			string fileName, out bool isOldFormatSign)
		{
			int error;

			error = _IsOldFormatSignFile(
				fileName, out isOldFormatSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignHash(
			IntPtr privateKeyContext, int signAlgo,
			IntPtr hashContext, bool appendCert,
			out byte[] sign)
		{
			int error;

			error = _CtxSignHash(privateKeyContext,
				signAlgo, hashContext, appendCert, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignHash(
			IntPtr privateKeyContext, int signAlgo,
			IntPtr hashContext, bool appendCert,
			out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxSignHash(privateKeyContext,
				signAlgo, hashContext, appendCert, out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignHashValue(
			IntPtr privateKeyContext, int signAlgo,
			byte[] hash, bool appendCert,
			out byte[] sign)
		{
			int error;

			error = _CtxSignHashValue(privateKeyContext,
				signAlgo, null, hash, appendCert, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignHashValue(
			IntPtr privateKeyContext, int signAlgo,
			string hash, bool appendCert,
			out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxSignHashValue(privateKeyContext,
				signAlgo, hash, null, appendCert, out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignData(
			IntPtr privateKeyContext, int signAlgo,
			string data, bool external, bool appendCert,
			out byte[] sign)
		{
			int error;

			error = _CtxSignData(privateKeyContext,
				signAlgo, data, null, external, appendCert,
				out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignData(
			IntPtr privateKeyContext, int signAlgo,
			string data, bool external, bool appendCert,
			out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxSignData(privateKeyContext,
				signAlgo, data, null, external, appendCert,
				out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignData(
			IntPtr privateKeyContext, int signAlgo,
			byte[] data, bool external, bool appendCert,
			out byte[] sign)
		{
			int error;

			error = _CtxSignData(privateKeyContext,
				signAlgo, null, data, external, appendCert,
				out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignData(
			IntPtr privateKeyContext, int signAlgo,
			byte[] data, bool external, bool appendCert,
			out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxSignData(privateKeyContext,
				signAlgo, null, data, external, appendCert,
				out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxSignFile(
			IntPtr privateKeyContext, int signAlgo,
			string fileName, bool external, bool appendCert,
			string fileNameWithSign)
		{
			int error;

			error = _CtxSignFile(privateKeyContext,
				signAlgo, fileName, external, appendCert,
				fileNameWithSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxIsAlreadySigned(
			IntPtr privateKeyContext, int signAlgo,
			string sign, out bool isAlreadySigned)
		{
			int error;

			error = _CtxIsAlreadySigned(privateKeyContext,
				signAlgo, sign, null, out isAlreadySigned);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxIsAlreadySigned(
			IntPtr privateKeyContext, int signAlgo,
			byte[] sign, out bool isAlreadySigned)
		{
			int error;

			error = _CtxIsAlreadySigned(privateKeyContext,
				signAlgo, null, sign, out isAlreadySigned);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxIsFileAlreadySigned(
			IntPtr privateKeyContext, int signAlgo,
			string fileNameWithSign, out bool isAlreadySigned)
		{
			int error;

			error = _CtxIsFileAlreadySigned(privateKeyContext,
				signAlgo, fileNameWithSign, out isAlreadySigned);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSignHash(
			IntPtr privateKeyContext, int signAlgo,
			IntPtr hashContext, string previousSign,
			bool appendCert, out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxAppendSignHash(privateKeyContext,
				signAlgo, hashContext, previousSign,
				null, appendCert, out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSignHash(
			IntPtr privateKeyContext, int signAlgo,
			IntPtr hashContext, byte[] previousSign,
			bool appendCert, out byte[] sign)
		{
			int error;

			error = _CtxAppendSignHash(privateKeyContext,
				signAlgo, hashContext, null, previousSign,
				appendCert, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSignHashValue(
			IntPtr privateKeyContext, int signAlgo,
			string hash, string previousSign,
			bool appendCert, out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxAppendSignHashValue(privateKeyContext,
				signAlgo, hash, null, previousSign,
				null, appendCert, out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSignHashValue(
			IntPtr privateKeyContext, int signAlgo,
			byte[] hash, byte[] previousSign,
			bool appendCert, out byte[] sign)
		{
			int error;

			error = _CtxAppendSignHashValue(privateKeyContext,
				signAlgo, null, hash, null, previousSign,
				appendCert, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSign(
			IntPtr privateKeyContext, int signAlgo,
			string data, string previousSign,
			bool appendCert, out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxAppendSign(privateKeyContext,
				signAlgo, data, null, previousSign, null,
				appendCert, out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSign(
			IntPtr privateKeyContext, int signAlgo,
			byte[] data, byte[] previousSign,
			bool appendCert, out byte[] sign)
		{
			int error;

			error = _CtxAppendSign(privateKeyContext,
				signAlgo, null, data, null, previousSign,
				appendCert, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSignFile(
			IntPtr privateKeyContext, int signAlgo,
			string fileName, string fileNameWithPrevSign,
			bool appendCert, string fileNameWithSign)
		{
			int error;

			error = _CtxAppendSignFile(privateKeyContext,
				signAlgo, fileName, fileNameWithPrevSign,
				appendCert, fileNameWithSign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxCreateEmptySign(
			IntPtr context, int signAlgo, string data,
			byte[] certificate, out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxCreateEmptySign(context,
				signAlgo, data, null, certificate, out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxCreateEmptySign(
			IntPtr context, int signAlgo,
			string data, byte[] certificate, out byte[] sign)
		{
			int error;

			error = _CtxCreateEmptySign(context,
				signAlgo, data, null, certificate, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxCreateEmptySign(
			IntPtr context, int signAlgo,
			byte[] data, byte[] certificate, out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxCreateEmptySign(context,
				signAlgo, null, data, certificate, out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxCreateEmptySign(
			IntPtr context, int signAlgo,
			byte[] data, byte[] certificate, out byte[] sign)
		{
			int error;

			error = _CtxCreateEmptySign(context,
				signAlgo, null, data, certificate, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxCreateSigner(
			IntPtr privateKeyContext, int signAlgo,
			string hash, out string signer)
		{
			int error;
			byte[] signerBinary;

			signer = null;

			error = _CtxCreateSigner(privateKeyContext,
				signAlgo, hash, null, out signerBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signerBinary, out signer);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxCreateSigner(
			IntPtr privateKeyContext, int signAlgo,
			byte[] hash, out byte[] signer)
		{
			int error;

			signer = null;

			error = _CtxCreateSigner(privateKeyContext,
				signAlgo, null, hash, out signer);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSigner(
			IntPtr context, int signAlgo,
			string signer, byte[] certificate,
			string prevSign, out string sign)
		{
			int error;
			byte[] signBinary;

			sign = null;

			error = _CtxAppendSigner(context,
				signAlgo, signer, null, certificate,
				prevSign, null, out signBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);

				return error;
			}

			error = BASE64Encode(signBinary, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxAppendSigner(
			IntPtr context, int signAlgo,
			byte[] signer, byte[] certificate,
			byte[] prevSign, out byte[] sign)
		{
			int error;

			error = _CtxAppendSigner(context,
				signAlgo, null, signer, certificate,
				null, prevSign, out sign);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetSignsCount(
			IntPtr context, string sign, out int count)
		{
			int error;

			error = _CtxGetSignsCount(context,
				sign, null, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetSignsCount(
			IntPtr context, byte[] sign, out int count)
		{
			int error;

			error = _CtxGetSignsCount(context,
				null, sign, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetFileSignsCount(
			IntPtr context, string fileNameWithSign, out int count)
		{
			int error;

			error = _CtxGetFileSignsCount(context,
				fileNameWithSign, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetSignerInfo(
			IntPtr context, int signIndex, string sign,
			out EU_CERT_INFO_EX info, out byte[] certificate)
		{
			int error;

			error = _CtxGetSignerInfo(context,
				signIndex, sign, null,
				out info, out certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetSignerInfo(
			IntPtr context, int signIndex, byte[] sign,
			out EU_CERT_INFO_EX info, out byte[] certificate)
		{
			int error;

			error = _CtxGetSignerInfo(context,
				signIndex, null, sign,
				out info, out certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetFileSignerInfo(
			IntPtr context, int signIndex, string fileNameWithSign,
			out EU_CERT_INFO_EX info, out byte[] certificate)
		{
			int error;

			error = _CtxGetFileSignerInfo(context,
				signIndex, fileNameWithSign,
				out info, out certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxIsDataInSignedDataAvailable(
			IntPtr context, string signedData, out bool available)
		{
			int error;

			error = _CtxIsDataInSignedDataAvailable(
				context, signedData, null, out available);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxIsDataInSignedDataAvailable(
			IntPtr context, byte[] signedData, out bool available)
		{
			int error;

			error = _CtxIsDataInSignedDataAvailable(
				context, null, signedData, out available);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxIsDataInSignedFileAvailable(
			IntPtr context, string fileNameWithSignedData,
			out bool available)
		{
			int error;

			error = _CtxIsDataInSignedFileAvailable(
				context, fileNameWithSignedData, out available);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetDataFromSignedData(
			IntPtr context, string signedData, out byte[] data)
		{
			int error;

			error = _CtxGetDataFromSignedData(
				context, signedData, null, out data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetDataFromSignedData(
			IntPtr context, byte[] signedData,
			out byte[] data)
		{
			int error;

			error = _CtxGetDataFromSignedData(
				context, null, signedData, out data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetDataFromSignedFile(
			IntPtr context, string fileNameWithSignedData,
			string fileName)
		{
			int error;

			error = _CtxGetDataFromSignedFile(
				context, fileNameWithSignedData, fileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxVerifyHash(
			IntPtr hashContext, int signIndex,
			string sign, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyHash(
				hashContext, signIndex,
				sign, null, out info);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int CtxVerifyHash(
			IntPtr hashContext, int signIndex,
			byte[] sign, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyHash(
				hashContext, signIndex,
				null, sign, out info);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int CtxVerifyHashValue(
			IntPtr context, string hash, int signIndex,
			string sign, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyHashValue(
				context, hash, null, signIndex,
				sign, null, out info);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int CtxVerifyHashValue(
			IntPtr context, string hash, int signIndex,
			byte[] sign, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyHashValue(
				context, hash, null, signIndex,
				null, sign, out info);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int CtxVerifyHashValue(
			IntPtr context, byte[] hash, int signIndex,
			string sign, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyHashValue(
				context, null, hash, signIndex,
				sign, null, out info);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int CtxVerifyHashValue(
			IntPtr context, byte[] hash, int signIndex,
			byte[] sign, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyHashValue(
				context, null, hash, signIndex,
				null, sign, out info);
			if (error != EU_ERROR_NONE)
				return error;

			return error;
		}

		public static int CtxVerifyData(
			IntPtr context, string data,
			int signIndex, string sign,
			out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyData(context,
				data, null, signIndex, sign, null, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxVerifyData(
			IntPtr context, string data,
			int signIndex, byte[] sign,
			out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyData(context,
				data, null, signIndex, null, sign, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxVerifyData(
			IntPtr context, byte[] data,
			int signIndex, string sign,
			out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyData(context,
				null, data, signIndex, sign, null, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxVerifyData(
			IntPtr context, byte[] data,
			int signIndex, byte[] sign,
			out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyData(context,
				null, data, signIndex, null, sign, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxVerifyDataInternal(
			IntPtr context, int signIndex, string sign,
			out byte[] data, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyDataInternal(context,
				signIndex, sign, null, out data, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxVerifyDataInternal(
			IntPtr context, int signIndex, byte[] sign,
			out byte[] data, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyDataInternal(context,
				signIndex, null, sign, out data, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxVerifyFile(
			IntPtr context, int signIndex,
			string fileNameWithSign,
			string fileName, out EU_SIGN_INFO info)
		{
			int error;

			error = _CtxVerifyFile(context, signIndex,
				fileNameWithSign, fileName, out info);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		#endregion

		#region EUSignCP: Envelop functions

		public static void ShowSenderInfo(EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _ShowSenderInfo(senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static void FreeSenderInfo(ref EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _FreeSenderInfo(ref senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static void CtxFreeSenderInfo(IntPtr context,
			ref EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _FreeSenderInfo(ref senderInfo, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int GetSenderInfo(
			string envelopedData, byte[] recipientCert,
			out bool dynamicKey, out EU_CERT_INFO_EX info,
			ref byte[] certificate)
		{
			int error;

			error = _GetSenderInfo(envelopedData,
				null, recipientCert, out dynamicKey,
				out info, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetSenderInfo(
			byte[] envelopedData, byte[] recipientCert,
			out bool dynamicKey, out EU_CERT_INFO_EX info,
			ref byte[] certificate)
		{
			int error;

			error = _GetSenderInfo(null,
				envelopedData, recipientCert, out dynamicKey,
				out info, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetFileSenderInfo(
			string envelopedFileName, byte[] recipientCert,
			out bool dynamicKey, out EU_CERT_INFO_EX info,
			ref byte[] certificate)
		{
			int error;

			error = _GetFileSenderInfo(
				envelopedFileName, recipientCert,
				out dynamicKey, out info, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetRecipientsCount(
			string envelopedData, out int count)
		{
			int error;

			error = _GetRecipientsCount(
				envelopedData, null, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetRecipientsCount(
			byte[] envelopedData, out int count)
		{
			int error;

			error = _GetRecipientsCount(
				null, envelopedData, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetFileRecipientsCount(
			string envelopedFileName, out int count)
		{
			int error;

			error = _GetFileRecipientsCount(
				envelopedFileName, out count);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetFileRecipientsCount(IntPtr context,
			string envelopedFileName, out int count)
		{
			int error;

			error = _GetFileRecipientsCount(
				envelopedFileName, out count, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetRecipientInfo(
			int recipientIndex, string envelopedData,
			out int recipientInfoType, out string recipientIssuer,
			out string recipientSerial, out string recipientKeyID)
		{
			int error;

			error = _GetRecipientInfo(recipientIndex,
				envelopedData, null, out recipientInfoType,
				out recipientIssuer, out recipientSerial,
				out recipientKeyID);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetRecipientInfo(
			int recipientIndex, byte[] envelopedData,
			out int recipientInfoType, out string recipientIssuer,
			out string recipientSerial, out string recipientKeyID)
		{
			int error;

			error = _GetRecipientInfo(recipientIndex,
				null, envelopedData, out recipientInfoType,
				out recipientIssuer, out recipientSerial,
				out recipientKeyID);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetFileRecipientInfo(
			int recipientIndex, string envelopedFileName,
			out int recipientInfoType, out string recipientIssuer,
			out string recipientSerial, out string recipientKeyID)
		{
			int error;

			error = _GetFileRecipientInfo(recipientIndex,
				envelopedFileName, out recipientInfoType,
				out recipientIssuer, out recipientSerial,
				out recipientKeyID);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopData(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopData(recipientCertIssuer,
				recipientCertSerial, signData, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopData(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopData(recipientCertIssuer,
				recipientCertSerial, signData, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopData(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, byte[] data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopData(recipientCertIssuer,
				recipientCertSerial, signData, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopData(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, byte[] data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopData(recipientCertIssuer,
				recipientCertSerial, signData, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataWithSettings(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string data, bool checkRecipientCertOffline,
			bool checkRecipientCertNoCRL, bool noTSP, bool appendCert,
			out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataWithSettings(recipientCertIssuer,
				recipientCertSerial, signData, data, null,
				checkRecipientCertOffline, checkRecipientCertNoCRL,
				noTSP, appendCert, ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataWithSettings(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string data, bool checkRecipientCertOffline, 
			bool checkRecipientCertNoCRL, bool noTSP, bool appendCert, 
			out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataWithSettings(recipientCertIssuer,
				recipientCertSerial, signData, data, null,
				checkRecipientCertOffline, checkRecipientCertNoCRL, 
				noTSP, appendCert, ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataWithSettings(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, byte[] data, bool checkRecipientCertOffline,
			bool checkRecipientCertNoCRL, bool noTSP, bool appendCert,
			out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataWithSettings(recipientCertIssuer,
				recipientCertSerial, signData, null, data,
				checkRecipientCertOffline, checkRecipientCertNoCRL, 
				noTSP, appendCert, ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataWithSettings(
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, byte[] data, bool checkRecipientCertOffline,
			bool checkRecipientCertNoCRL, bool noTSP, bool appendCert,
			out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataWithSettings(recipientCertIssuer,
				recipientCertSerial, signData, null, data,
				checkRecipientCertOffline, checkRecipientCertNoCRL, 
				noTSP, appendCert, ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataRSA(algo,
				recipientCertIssuer, recipientCertSerial,
				signData, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataRSA(algo,
				recipientCertIssuer, recipientCertSerial,
				signData, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, byte[] data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataRSA(algo,
				recipientCertIssuer, recipientCertSerial,
				signData, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, byte[] data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataRSA(algo,
				recipientCertIssuer, recipientCertSerial,
				signData, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevelopData(string envelopedData,
			out byte[] data, out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _DevelopData(envelopedData,
				null, out data, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevelopData(byte[] envelopedData,
			out byte[] data, out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _DevelopData(null,
				envelopedData, out data, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFile(string recipientCertIssuer,
			string recipientCertSerial, bool signData,
			string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFile(recipientCertIssuer,
				recipientCertSerial, signData, fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFileRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string recipientCertIssuer, string recipientCertSerial,
			bool signData, string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFileRSA(algo,
				recipientCertIssuer, recipientCertSerial,
				signData, fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevelopFile(string envelopedFileName,
			string fileName, out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _DevelopFile(envelopedFileName,
				fileName, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawEnvelopData(
			byte[] recipientCert,
			string data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _RawEnvelopData(
				recipientCert, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawEnvelopData(
			byte[] recipientCert,
			string data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _RawEnvelopData(
				recipientCert, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawEnvelopData(
			byte[] recipientCert,
			byte[] data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _RawEnvelopData(
				recipientCert, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawEnvelopData(
			byte[] recipientCert,
			byte[] data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _RawEnvelopData(
				recipientCert, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawDevelopData(
			string envelopedData,
			out byte[] data, out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _RawDevelopData(envelopedData,
				null, out data, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int RawDevelopData(
			byte[] envelopedData,
			out byte[] data, out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _RawDevelopData(null,
				envelopedData, out data, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static bool IsEnvelopedData(byte[] data)
		{
			int error;
			bool isEnvelopedData;

			error = _IsEnvelopedData(data, out isEnvelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return isEnvelopedData;
		}

		public static bool IsEnvelopedFile(string fileName)
		{
			int error;
			bool isEnvelopedFile;

			error = _IsEnvelopedFile(fileName, out isEnvelopedFile);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return isEnvelopedFile;
		}

		public static int GetReceiversCertificates(
			out EU_CERT_INFO_EX[] certificates)
		{
			int error;

			error = _GetReceiversCertificates(
				out certificates);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int GetReceiversCertificatesRSA(
			out EU_CERT_INFO_EX[] certificates)
		{
			int error;

			error = _GetReceiversCertificatesRSA(
				out certificates);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataEx(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, string data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataEx(recipientsCertIssuer,
				recipientsCertSerial, signData, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataEx(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, string data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataEx(recipientsCertIssuer,
				recipientsCertSerial, signData, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataEx(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, byte[] data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataEx(recipientsCertIssuer,
				recipientsCertSerial, signData, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataEx(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, byte[] data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataEx(recipientsCertIssuer,
				recipientsCertSerial, signData, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataRSAEx(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, string data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataRSAEx(algo,
				recipientsCertIssuer, recipientsCertSerial,
				signData, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataRSAEx(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, string data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataEx(recipientsCertIssuer,
				recipientsCertSerial, signData, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataRSAEx(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, byte[] data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataRSAEx(algo,
				recipientsCertIssuer, recipientsCertSerial,
				signData, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataRSAEx(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, byte[] data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataRSAEx(algo,
				recipientsCertIssuer, recipientsCertSerial,
				signData, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFileEx(string[] recipientsCertIssuer,
			string[] recipientsCertSerial, bool signData,
			string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFileEx(recipientsCertIssuer,
				recipientsCertSerial, signData,
				fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFileRSAEx(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			string[] recipientsCertIssuer,
			string[] recipientsCertSerial, bool signData,
			string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFileRSAEx(algo,
				recipientsCertIssuer, recipientsCertSerial,
				signData, fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipients(byte[][] recipientCerts,
			bool signData, ref string dataString, ref byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			int error;

			error = _EnvelopDataToRecipients(
				recipientCerts, signData, dataString, dataBinary,
				ref envelopedDataString, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipients(byte[][] recipientCerts,
			bool signData, string data, ref string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipients(
				recipientCerts, signData, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipients(byte[][] recipientCerts,
			bool signData, string data, ref byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipients(
				recipientCerts, signData, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipients(byte[][] recipientCerts,
			bool signData, byte[] data, ref byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipients(
				recipientCerts, signData, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipients(byte[][] recipientCerts,
			bool signData, byte[] data, ref string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipients(
				recipientCerts, signData, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithSettings(
			byte[][] recipientCerts, bool signData, string data,
			bool checkRecipientCertsOffline, bool checkRecipientCertsNoCRL, 
			bool noTSP, bool appendCert, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsWithSettings(
				recipientCerts, signData, data, null,
				checkRecipientCertsOffline, checkRecipientCertsNoCRL, 
				noTSP, appendCert, ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithSettings(
			byte[][] recipientCerts, bool signData, string data,
			bool checkRecipientCertsOffline, bool checkRecipientCertsNoCRL, 
			bool noTSP, bool appendCert, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsWithSettings(
				recipientCerts, signData, data, null,
				checkRecipientCertsOffline, checkRecipientCertsNoCRL, 
				noTSP, appendCert, ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithSettings(
			byte[][] recipientCerts, bool signData, byte[] data,
			bool checkRecipientCertsOffline, bool checkRecipientCertsNoCRL, 
			bool noTSP, bool appendCert, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsWithSettings(
				recipientCerts, signData, null, data,
				checkRecipientCertsOffline, checkRecipientCertsNoCRL, 
				noTSP, appendCert, ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithSettings(
			byte[][] recipientCerts, bool signData, byte[] data,
			bool checkRecipientCertsOffline, bool checkRecipientCertsNoCRL, 
			bool noTSP, bool appendCert, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsWithSettings(
				recipientCerts, signData, null, data,
				checkRecipientCertsOffline, checkRecipientCertsNoCRL, 
				noTSP, appendCert, ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo, byte[][] recipientCerts,
			bool signData, ref string dataString, ref byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			int error;

			error = _EnvelopDataToRecipientsRSA(
				algo, recipientCerts, signData, dataString, dataBinary,
				ref envelopedDataString, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo, byte[][] recipientCerts,
			bool signData, string data, ref string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsRSA(
				algo, recipientCerts, signData, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo, byte[][] recipientCerts,
			bool signData, string data, ref byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsRSA(
				algo, recipientCerts, signData, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo, byte[][] recipientCerts,
			bool signData, byte[] data, ref byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsRSA(
				algo, recipientCerts, signData, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo, byte[][] recipientCerts,
			bool signData, byte[] data, ref string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsRSA(
				algo, recipientCerts, signData, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFileToRecipients(
			byte[][] recipientCerts, bool signData,
			string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFileToRecipients(recipientCerts,
				signData, fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFileToRecipientsRSA(
			EU_CONTENT_ENC_ALGO_TYPE algo,
			byte[][] recipientCerts, bool signData,
			string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFileToRecipientsRSA(
				algo, recipientCerts,
				signData, fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataExWithDynamicKey(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, bool appendCert,
			string data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataExWithDynamicKey(recipientsCertIssuer,
				recipientsCertSerial, signData, appendCert, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataExWithDynamicKey(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, bool appendCert,
			string data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataExWithDynamicKey(recipientsCertIssuer,
				recipientsCertSerial, signData, appendCert, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataExWithDynamicKey(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, bool appendCert,
			byte[] data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataExWithDynamicKey(recipientsCertIssuer,
				recipientsCertSerial, signData, appendCert, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataExWithDynamicKey(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, bool appendCert, byte[] data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataExWithDynamicKey(recipientsCertIssuer,
				recipientsCertSerial, signData, appendCert, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFileExWithDynamicKey(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, bool appendCert,
			string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFileExWithDynamicKey(recipientsCertIssuer,
				recipientsCertSerial, signData, appendCert,
				fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithDynamicKey(
			byte[][] recipientCerts, bool signData, bool appendCert,
			string data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsWithDynamicKey(
				recipientCerts, signData, appendCert, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithDynamicKey(
			byte[][] recipientCerts, bool signData, bool appendCert,
			string data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsWithDynamicKey(
				recipientCerts, signData, appendCert, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithDynamicKey(
			byte[][] recipientCerts, bool signData, bool appendCert,
			byte[] data, out byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsWithDynamicKey(
				recipientCerts, signData, appendCert, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithDynamicKey(
			byte[][] recipientCerts, bool signData, bool appendCert,
			byte[] data, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsWithDynamicKey(
				recipientCerts, signData, appendCert, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFileToRecipientsWithDynamicKey(
			byte[][] recipientCerts, bool signData, bool appendCert,
			string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFileToRecipientsWithDynamicKey(
				recipientCerts, signData, appendCert,
				fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsEx(byte[][] recipientCerts,
			int recipientAppendType, bool signData, string data,
			ref string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsEx(recipientCerts,
				recipientAppendType, signData, data, null,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsEx(byte[][] recipientCerts,
			int recipientAppendType, bool signData, string data,
			ref byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsEx(recipientCerts,
				recipientAppendType, signData, data, null,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsEx(byte[][] recipientCerts,
			int recipientAppendType, bool signData, byte[] data,
			ref byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsEx(recipientCerts,
				recipientAppendType, signData, null, data,
				ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsEx(byte[][] recipientCerts,
			int recipientAppendType, bool signData, byte[] data,
			ref string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsEx(recipientCerts,
				recipientAppendType, signData, null, data,
				ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopFileToRecipientsEx(
			byte[][] recipientCerts, int recipientAppendType,
			bool signData, string fileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopFileToRecipientsEx(recipientCerts,
				recipientAppendType, signData, fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithOCode(
			string recipientsOCode, int recipientAppendType,
			bool signData, string data, ref string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsWithOCode(
				recipientsOCode, recipientAppendType, signData,
				data, null, ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithOCode(
			string recipientsOCode, int recipientAppendType,
			bool signData, string data, ref byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsWithOCode(
				recipientsOCode, recipientAppendType, signData,
				data, null, ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithOCode(
			string recipientsOCode, int recipientAppendType,
			bool signData, byte[] data, ref byte[] envelopedData)
		{
			int error;
			string envelopedDataString = null;

			envelopedData = new byte[0];

			error = _EnvelopDataToRecipientsWithOCode(
				recipientsOCode, recipientAppendType, signData,
				null, data, ref envelopedDataString, ref envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopDataToRecipientsWithOCode(
			string recipientsOCode, int recipientAppendType,
			bool signData, byte[] data, ref string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary = null;

			envelopedData = "";

			error = _EnvelopDataToRecipientsWithOCode(
				recipientsOCode, recipientAppendType, signData,
				null, data, ref envelopedData, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopAppendData(string dataString, byte[] dataBinary,
			string previousEnvelopedString, byte[] previousEnvelopedBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			int error;

			error = _EnvelopAppendData(dataString, dataBinary,
				previousEnvelopedString, previousEnvelopedBinary,
				ref envelopedDataString, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopAppendData(string data,
			string previousEnveloped, out string enveloped)
		{
			int error;
			byte[] envelopedBinary = null;

			enveloped = "";

			error = _EnvelopAppendData(data, null, previousEnveloped,
				null, ref enveloped, ref envelopedBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopAppendData(byte[] data,
			byte[] previousEnveloped, out byte[] enveloped)
		{
			int error;
			string envelopedString = null;

			enveloped = new byte[0];

			error = _EnvelopAppendData(null, data, null,
				previousEnveloped, ref envelopedString, ref enveloped);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopAppendFile(string fileName,
			string previousEnvelopedFileName, string envelopedFileName)
		{
			int error;

			error = _EnvelopAppendFile(fileName,
				previousEnvelopedFileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopAppendDataEx(
			string dataString, byte[] dataBinary,
			byte[] senderCert, string previousEnvelopedString,
			byte[] previousEnvelopedBinary, ref string envelopedDataString,
			ref byte[] envelopedDataBinary)
		{
			int error;

			error = _EnvelopAppendDataEx(
				dataString, dataBinary, senderCert,
				previousEnvelopedString, previousEnvelopedBinary,
				ref envelopedDataString, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopAppendDataEx(string data,
			byte[] senderCert, string previousEnveloped,
			out string enveloped)
		{
			int error;
			byte[] envelopedBinary = null;

			enveloped = "";

			error = _EnvelopAppendDataEx(
				data, null, senderCert,
				previousEnveloped, null, ref enveloped,
				ref envelopedBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopAppendDataEx(byte[] data,
			byte[] senderCert, byte[] previousEnveloped,
			out byte[] enveloped)
		{
			int error;
			string envelopedString = null;

			enveloped = new byte[0];

			error = _EnvelopAppendDataEx(
				null, data, senderCert, null,
				previousEnveloped, ref envelopedString, 
				ref enveloped);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int EnvelopAppendFileEx(string fileName,
			byte[] senderCert, string previousEnvelopedFileName,
			string envelopedFileName)
		{
			int error;

			error = _EnvelopAppendFileEx(fileName,
				senderCert, previousEnvelopedFileName,
				envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevelopDataEx(string envelopedData,
			byte[] senderCert, out byte[] data,
			out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _DevelopDataEx(envelopedData,
				null, senderCert, out data, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevelopDataEx(byte[] envelopedData,
			byte[] senderCert, out byte[] data,
			out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _DevelopDataEx(null, envelopedData,
				senderCert, out data, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevelopFileEx(string envelopedFileName,
			byte[] senderCert, string fileName, out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _DevelopFileEx(envelopedFileName,
				senderCert, fileName, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ProtectDataByPassword(
			string dataString, byte[] dataBinary, string password,
			ref string protectedDataString, ref byte[] protectedDataBinary)
		{
			int error;

			error = _ProtectDataByPassword(
				dataString, dataBinary, password,
				ref protectedDataString, ref protectedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ProtectDataByPassword(
			string data, string password, out string protectedData)
		{
			int error;
			byte[] protectedDataBinary = null;

			protectedData = "";

			error = _ProtectDataByPassword(
				data, null, password,
				ref protectedData, ref protectedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ProtectDataByPassword(
			byte[] data, string password, out byte[] protectedData)
		{
			int error;
			string protectedDataString = null;

			protectedData = new byte[0];

			error = _ProtectDataByPassword(
				null, data, password,
				ref protectedDataString, ref protectedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int UnprotectDataByPassword(
			string protectedDataString, byte[] protectedDataBinary,
			string password, ref string dataString, ref byte[] dataBinary)
		{
			int error;

			error = _UnprotectDataByPassword(
				protectedDataString, protectedDataBinary, password,
				ref dataString, ref dataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int UnprotectDataByPassword(
			string protectedData, string password,
			out string data)
		{
			int error;
			byte[] dataBinary = null;

			data = "";

			error = _UnprotectDataByPassword(
				protectedData, null, password,
				ref data, ref dataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int UnprotectDataByPassword(
			byte[] protectedData, string password,
			out byte[] data)
		{
			int error;
			string dataString = null;

			data = new byte[0];

			error = _UnprotectDataByPassword(
				null, protectedData, password,
				ref dataString, ref data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetSenderInfo(IntPtr context,
			string envelopedData, byte[] recipientCert,
			out bool dynamicKey, out EU_CERT_INFO_EX info,
			ref byte[] certificate)
		{
			int error;

			error = _GetSenderInfo(envelopedData,
				null, recipientCert, out dynamicKey,
				out info, ref certificate, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetSenderInfo(IntPtr context,
			byte[] envelopedData, byte[] recipientCert,
			out bool dynamicKey, out EU_CERT_INFO_EX info,
			ref byte[] certificate)
		{
			int error;

			error = _GetSenderInfo(null,
				envelopedData, recipientCert, out dynamicKey,
				out info, ref certificate, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetFileSenderInfo(IntPtr context,
			string envelopedFileName, byte[] recipientCert,
			out bool dynamicKey, out EU_CERT_INFO_EX info,
			ref byte[] certificate)
		{
			int error;

			error = _GetFileSenderInfo(
				envelopedFileName, recipientCert,
				out dynamicKey, out info, ref certificate,
				context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetRecipientsCount(IntPtr context,
			string envelopedData, out int count)
		{
			int error;

			error = _GetRecipientsCount(
				envelopedData, null, out count, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetRecipientsCount(IntPtr context,
			byte[] envelopedData, out int count)
		{
			int error;

			error = _GetRecipientsCount(
				null, envelopedData, out count, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetFileRecipientsCount(IntPtr context,
			string fileName, out int count)
		{
			int error;

			error = _GetFileRecipientsCount(
				fileName, out count, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetRecipientInfo(IntPtr context,
			int recipientIndex, string envelopedData,
			out int recipientInfoType, out string recipientIssuer,
			out string recipientSerial, out string recipientKeyID)
		{
			int error;

			error = _GetRecipientInfo(recipientIndex,
				envelopedData, null, out recipientInfoType,
				out recipientIssuer, out recipientSerial,
				out recipientKeyID, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetRecipientInfo(IntPtr context,
			int recipientIndex, byte[] envelopedData,
			out int recipientInfoType, out string recipientIssuer,
			out string recipientSerial, out string recipientKeyID)
		{
			int error;

			error = _GetRecipientInfo(recipientIndex,
				null, envelopedData, out recipientInfoType,
				out recipientIssuer, out recipientSerial,
				out recipientKeyID, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxGetFileRecipientInfo(IntPtr context,
			int recipientIndex, string envelopedFileName,
			out int recipientInfoType, out string recipientIssuer,
			out string recipientSerial, out string recipientKeyID)
		{
			int error;

			error = _GetFileRecipientInfo(recipientIndex,
				envelopedFileName, out recipientInfoType,
				out recipientIssuer, out recipientSerial,
				out recipientKeyID, context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnvelopData(IntPtr privateKeyContext,
			byte[][] recipientCerts, int recipientAppendType,
			bool signData, bool appendCert, string data,
			out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary;

			envelopedData = null;

			error = _CtxEnvelopData(privateKeyContext,
				recipientCerts, recipientAppendType,
				signData, appendCert, data, null, out envelopedDataBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);
				return error;
			}

			error = BASE64Encode(envelopedDataBinary, out envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnvelopData(IntPtr privateKeyContext,
			byte[][] recipientCerts, int recipientAppendType,
			bool signData, bool appendCert, string data,
			out byte[] envelopedData)
		{
			int error;

			envelopedData = null;

			error = _CtxEnvelopData(privateKeyContext,
				recipientCerts, recipientAppendType,
				signData, appendCert, data, null, out envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnvelopData(IntPtr privateKeyContext,
			byte[][] recipientCerts, int recipientAppendType,
			bool signData, bool appendCert, byte[] data,
			out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary;

			envelopedData = null;

			error = _CtxEnvelopData(privateKeyContext,
				recipientCerts, recipientAppendType,
				signData, appendCert, null, data, out envelopedDataBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);
				return error;
			}

			error = BASE64Encode(envelopedDataBinary, out envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnvelopData(IntPtr privateKeyContext,
			byte[][] recipientCerts, int recipientAppendType,
			bool signData, bool appendCert, byte[] data,
			out byte[] envelopedData)
		{
			int error;

			envelopedData = null;

			error = _CtxEnvelopData(privateKeyContext,
				recipientCerts, recipientAppendType,
				signData, appendCert, null, data, out envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxDevelopData(IntPtr privateKeyContext,
			string envelopedData, byte[] senderCert, out byte[] data,
			out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _CtxDevelopData(privateKeyContext,
				envelopedData, null, senderCert,
				out data, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxDevelopData(IntPtr privateKeyContext,
			byte[] envelopedData, byte[] senderCert, out byte[] data,
			out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _CtxDevelopData(privateKeyContext,
				null, envelopedData, senderCert,
				out data, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnvelopFile(IntPtr privateKeyContext,
			byte[][] recipientCerts, int recipientAppendType,
			bool signData, bool appendCert, string fileName,
			string envelopedFileName)
		{
			int error;

			error = _CtxEnvelopFile(privateKeyContext,
				recipientCerts, recipientAppendType, signData,
				appendCert, fileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxDevelopFile(IntPtr privateKeyContext,
			string envelopedFileName, byte[] senderCert,
			string fileName, out EU_SENDER_INFO senderInfo)
		{
			int error;

			error = _CtxDevelopFile(privateKeyContext,
				envelopedFileName, senderCert,
				fileName, out senderInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnvelopAppendData(
			IntPtr privateKeyContext, string data, byte[] senderCert,
			string previousEnvelopedData, out string envelopedData)
		{
			int error;
			byte[] envelopedDataBinary;

			envelopedData = null;

			error = _CtxEnvelopAppendData(privateKeyContext,
				data, null, senderCert, previousEnvelopedData,
				null, out envelopedDataBinary);
			if (error != EU_ERROR_NONE)
			{
				RaiseError(error);
				return error;
			}

			error = BASE64Encode(envelopedDataBinary, out envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnvelopAppendData(
			IntPtr privateKeyContext, byte[] data, byte[] senderCert,
			byte[] previousEnvelopedData, out byte[] envelopedData)
		{
			int error;

			error = _CtxEnvelopAppendData(
				privateKeyContext, null, data, senderCert,
				null, previousEnvelopedData, out envelopedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxEnvelopAppendFile(
			IntPtr privateKeyContext, string fileName,
			byte[] senderCert, string prevEnvelopedFileName,
			string envelopedFileName)
		{
			int error;

			error = _CtxEnvelopAppendFile(
				privateKeyContext, fileName, senderCert,
				prevEnvelopedFileName, envelopedFileName);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		#endregion

		#region EUSignCP: Client/Server secure session functions

		public static void SessionDestroy(IntPtr session)
		{
			int error;

			error = _SessionDestroy(session);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int ClientSessionCreateStep1(int expireTime,
			out IntPtr clientSession, out byte[] clientData)
		{
			int error;

			error = _ClientSessionCreateStep1(expireTime,
				out clientSession, out clientData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ServerSessionCreateStep1(int expireTime,
			byte[] clientData, out IntPtr serverSession, out byte[] serverData)
		{
			int error;

			error = _ServerSessionCreateStep1(expireTime,
				clientData, out serverSession, out serverData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ClientSessionCreateStep2(IntPtr clientSession,
			byte[] serverData, out byte[] clientData)
		{
			int error;

			clientData = new byte[0];

			error = _ClientSessionCreateStep2(clientSession,
				serverData, ref clientData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ClientSessionCreateStep2(IntPtr clientSession,
			byte[] serverData)
		{
			int error;

			byte[] clientData = null;

			error = _ClientSessionCreateStep2(
				clientSession, serverData, ref clientData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ServerSessionCreateStep2(IntPtr serverSession,
			byte[] clientData)
		{
			int error;

			error = _ServerSessionCreateStep2(
				serverSession, clientData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ClientDynamicKeySessionCreate(
			int expireTime, string serverCertIssuer,
			string serverCertSerial,
			out IntPtr clientSession, out byte[] clientData)
		{
			int error;

			error = _ClientDynamicKeySessionCreate(expireTime,
				serverCertIssuer, serverCertSerial, null,
				out clientSession, out clientData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ClientDynamicKeySessionCreate(
			int expireTime, byte[] serverCert,
			out IntPtr clientSession, out byte[] clientData)
		{
			int error;

			error = _ClientDynamicKeySessionCreate(expireTime,
				null, null, serverCert, out clientSession, out clientData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ServerDynamicKeySessionCreate(int expireTime,
			byte[] clientData, out IntPtr serverSession)
		{
			int error;

			error = _ServerDynamicKeySessionCreate(expireTime,
				clientData, out serverSession);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static bool SessionIsInitialized(IntPtr session)
		{
			int error;
			bool isInitialized;

			error = _SessionIsInitialized(session, out isInitialized);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return isInitialized;
		}

		public static int SessionSave(IntPtr session,
			out byte[] sessionData)
		{
			int error;

			error = _SessionSave(
				session, out sessionData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SessionLoad(byte[] sessionData, out IntPtr session)
		{
			int error;

			error = _SessionLoad(
				sessionData, out session);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SessionCheckCertificates(IntPtr session)
		{
			int error;

			error = _SessionCheckCertificates(
				session);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SessionEncrypt(IntPtr session,
			byte[] data, out byte[] encryptedData)
		{
			int error;

			error = _SessionProccessData(
				session, true, data, out encryptedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SessionEncryptContinue(
			IntPtr session, ref byte[] data)
		{
			int error;

			error = _SessionProccessDataContinue(
				session, true, ref data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SessionDecrypt(IntPtr session,
			byte[] encryptedData, out byte[] data)
		{
			int error;

			error = _SessionProccessData(
				session, false, encryptedData, out data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SessionDecryptContinue(
			IntPtr session, ref byte[] encryptedData)
		{
			int error;

			error = _SessionProccessDataContinue(
				session, false, ref encryptedData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SessionGetPeerCertificateInfo(
			IntPtr session, out EU_CERT_INFO certInfo)
		{
			int error;

			error = _SessionGetPeerCertificateInfo(
				session, out certInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		#endregion

		#region EUSignCP: Library context functions

		public static int CtxCreate(out IntPtr context)
		{
			int error;

			error = _CtxCreate(out context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void CtxFree(IntPtr context)
		{
			int error;

			error = _CtxFree(context);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int CtxSetParameter(IntPtr context,
			string parameterName, bool parameterValue)
		{
			int error;

			error = _CtxSetParameter(context,
				parameterName, parameterValue);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		#endregion

		#region EUSignCP: SC client functions

		public static int SCClientIsRunning(out bool running)
		{
			int error;

			error = _SCClientIsRunning(out running);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SCClientStart()
		{
			int error;

			error = _SCClientStart();
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void SCClientStop()
		{
			_SCClientStop();
		}

		public static int SCClientAddGate(
			string gateName, short connectPort,
			string gatewayAddress, short gatewayPort,
			string externalInterface, string externalRouterIPAddress)
		{
			int error;

			error = _SCClientAddGate(gateName, connectPort,
				gatewayAddress, gatewayPort,
				externalInterface, externalRouterIPAddress);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SCClientAddGate(
			string gateName, int connectPort,
			string gatewayAddress, int gatewayPort,
			string externalInterface, string externalRouterIPAddress)
		{
			return SCClientAddGate(gateName, (short) connectPort,
				gatewayAddress, (short) gatewayPort,
				externalInterface, externalRouterIPAddress);
		}

		public static int SCClientRemoveGate(
			short connectPort)
		{
			int error;

			error = _SCClientRemoveGate(connectPort);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int SCClientRemoveGate(
			int connectPort)
		{
			return SCClientRemoveGate((short)connectPort);
		}

		public static int SCClientGetStatistic(
			out EU_SCC_STATISTIC statistic)
		{
			int error;

			error = _SCClientGetStatistic(out statistic);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}
		#endregion

		#region EUSignCP: TaxService functions

		public static int AppendTransportHeader(
			string caType, string fileName, string clientEMail,
			byte[] clientCert, byte[] cryptoData, out byte[] transportData)
		{
			int error;

			error = _AppendTransportHeader(
				caType, fileName, clientEMail, clientCert, 
				cryptoData, out transportData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ParseTransportHeader(
			byte[] transportData, out EU_TRANSPORT_HEADER header)
		{
			int error;

			error = _ParseTransportHeader(
				transportData, out header);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int AppendCryptoHeader(
			string caType, EU_HEADER_PART_TYPE headerType,
			byte[] cryptoData, out byte[] transportData)
		{
			int error;

			error = _AppendCryptoHeader(
				caType, headerType, cryptoData, out transportData);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int ParseCryptoHeader(
			byte[] transportData, out EU_CRYPTO_HEADER header)
		{
			int error;

			error = _ParseCryptoHeader(
				transportData, out header);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		#endregion

		#region EUSignCP: Deprecated functions

		[Obsolete]
		public static int EnvelopDataEx(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, ref string dataString, ref byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			int error;

			error = _EnvelopDataEx(recipientsCertIssuer,
				recipientsCertSerial, signData, dataString, dataBinary,
				ref envelopedDataString, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		[Obsolete]
		public static int EnvelopDataExWithDynamicKey(
			string[] recipientsCertIssuer, string[] recipientsCertSerial,
			bool signData, bool appendCert,
			ref string dataString, ref byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			int error;

			error = _EnvelopDataExWithDynamicKey(recipientsCertIssuer,
				recipientsCertSerial, signData, appendCert, dataString, dataBinary,
				ref envelopedDataString, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		[Obsolete]
		public static int EnvelopDataToRecipientsWithDynamicKey(
			byte[][] recipientCerts, bool signData, bool appendCert,
			ref string dataString, ref byte[] dataBinary,
			ref string envelopedDataString, ref byte[] envelopedDataBinary)
		{
			int error;

			error = _EnvelopDataToRecipientsWithDynamicKey(
				recipientCerts, signData, appendCert, dataString, dataBinary,
				ref envelopedDataString, ref envelopedDataBinary);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		[Obsolete]
		public static int GetSenderInfo(
			string envelopedDataString, byte[] envelopedDataBinary,
			byte[] recipientCert, out bool dynamicKey, out EU_CERT_INFO_EX info,
			ref byte[] certificate)
		{
			int error;

			error = _GetSenderInfo(
				envelopedDataString, envelopedDataBinary,
				recipientCert, out dynamicKey,
				out info, ref certificate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxReadPrivateKey(IntPtr context,
			EU_KEY_MEDIA keyMedia, out EU_CERT_OWNER_INFO certOwnerInfo,
			 out IntPtr privateKeyContext)
		{
			int error;

			error = _CtxReadPrivateKey(context, keyMedia,
				out privateKeyContext, out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxReadPrivateKeyBinary(
			IntPtr context, byte[] privateKey, string password,
			out EU_CERT_OWNER_INFO certOwnerInfo, out IntPtr privKeyContext)
		{
			int error;

			error = _CtxReadPrivateKeyBinary(
				context, privateKey, password,
				out privKeyContext, out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int CtxReadPrivateKeyFile(IntPtr context,
			string privateKeyFileName, string password,
			out EU_CERT_OWNER_INFO certOwnerInfo,
			out IntPtr privateKeyContext)
		{
			int error;

			error = _CtxReadPrivateKeyFile(context,
				privateKeyFileName, password, out privateKeyContext,
				out certOwnerInfo);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}
		#endregion

		#region EUSignCP: Library device context functions

		public static int DevCtxEnumVirtual(
			ref IntPtr deviceContext,
			out string typeDescription)
		{
			int error;

			error = _DevCtxEnumVirtual(ref deviceContext,
				out typeDescription);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxEnum(
			IntPtr deviceContext,
			out string deviceDescription)
		{
			int error;

			error = _DevCtxEnum(deviceContext,
				out deviceDescription);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static void DevCtxClose(
			IntPtr deviceContext)
		{
			int error;

			error = _DevCtxClose(deviceContext);
			if (error != EU_ERROR_NONE)
				RaiseError(error);
		}

		public static int DevCtxOpenIDCard(
			string typeDescription, string deviceDescription,
			string password, int passwordVersion,
			out IntPtr deviceContext)
		{
			int error;

			error = _DevCtxOpenIDCard(
				typeDescription, deviceDescription,
				password, passwordVersion,
				out deviceContext);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxChangeIDCardPasswords(
			IntPtr deviceContext, string pin, string puk)
		{
			int error;

			error = _DevCtxChangeIDCardPasswords(
				deviceContext, pin, puk);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxAuthenticateIDCard(
			IntPtr deviceContext,
			string parameter1, string parameter2)
		{
			int error;

			error = _DevCtxAuthenticateIDCard(
				deviceContext, parameter1, parameter2);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxVerifyIDCardData(
			IntPtr deviceContext, byte tag)
		{
			int error;

			error = _DevCtxVerifyIDCardData(
				deviceContext, tag);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxUpdateIDCardData(
			IntPtr deviceContext, IntPtr privateKeyContext,
			byte tag, byte[] data)
		{
			int error;

			error = _DevCtxUpdateIDCardData(
				deviceContext, privateKeyContext,
				tag, data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxEnumIDCardData(
			IntPtr deviceContext, byte tag, int index,
			out byte[] data)
		{
			int error;

			data = new byte[0];

			error = _DevCtxEnumIDCardData(
				deviceContext, tag, index,
				ref data);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxEnumIDCardDataChangeDate(
			IntPtr deviceContext, byte tag, int index,
			out SYSTEMTIME changeDate)
		{
			int error;

			error = _DevCtxEnumIDCardDataChangeDate(
				deviceContext, tag, index,
				out changeDate);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxVerifyIDCardSecurityObjectDocument(
			IntPtr deviceContext,
			string certificatesStorePath)
		{
			int error;

			error = _DevCtxVerifyIDCardSecurityObjectDocument(
				deviceContext, certificatesStorePath);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		public static int DevCtxInternalAuthenticateIDCard(
			IntPtr deviceContext, byte[][] CVCerts,
			byte[] privateKey, string password)
		{
			int error;

			error = _DevCtxInternalAuthenticateIDCard(
				deviceContext, CVCerts,
				privateKey, password);
			if (error != EU_ERROR_NONE)
				RaiseError(error);

			return error;
		}

		#endregion

		#endregion
	}
}

//=============================================================================
