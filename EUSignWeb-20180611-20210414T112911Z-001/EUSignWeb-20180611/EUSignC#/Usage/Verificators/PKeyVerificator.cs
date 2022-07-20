using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EUSignCP;

namespace EUSignTestCS.Verificators
{
	class PKeyVerificator : IDisposable
	{
		private string rootDirectory;
		private string keyInfoReportPath;
		private string exportPassword = "12345677";
		private int namedKeysCount = 3;

		private class NamedKey {
			public string label;
			public string password;

			public NamedKey(string label, string password)
			{
				this.label = label;
				this.password = password;
			}
		}

		public PKeyVerificator()
		{
			rootDirectory = Path.Combine(
				System.IO.Path.GetTempPath(), "PrivKeyTest" +
				DateTime.Now.ToString(" - yyyy-MM-dd HH-mm-ss"));

			keyInfoReportPath = Path.Combine(
				rootDirectory, "KeyInfoReport.txt");

			TestData.CreateDirectory(rootDirectory);
		}

		public void Dispose()
		{
			TestData.DeleteDirectory(rootDirectory);
		}

		#region PKeyVerificator: Test export container functions

		private void WriteKeyInfosFile(List<IEUSignCP.EU_PRIVATE_KEY_INFO> keyInfos)
		{
			string keyInfosString = "";

			for (int i = 0; i < keyInfos.Count; i++)
			{
				IEUSignCP.EU_PRIVATE_KEY_INFO info;
				info = keyInfos[i];

				keyInfosString += (i + 1) + ". Інформація про ключ:\n";
				keyInfosString += "\tТип ключа: ";
				switch ((IEUSignCP.EU_KEYS_TYPE) info.keyType)
				{
					case IEUSignCP.EU_KEYS_TYPE.DSTU_AND_ECDH_WITH_GOSTS:
						keyInfosString += "ДСТУ-4145-2002\n";
						break;

					case IEUSignCP.EU_KEYS_TYPE.RSA_WITH_SHA:
						keyInfosString += "RSA\n";
						break;

					default:
						keyInfosString += "Не визначено\n";
						break;
				}

				keyInfosString += "\tПризначення ключа:\n";
				if ((info.keyUsage & IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE) ==
						IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE)
				{
					keyInfosString += "\t\t - ЕЦП;\n";
				}

				if ((info.keyUsage & IEUSignCP.EU_KEY_USAGE_KEY_AGREEMENT) ==
						IEUSignCP.EU_KEY_USAGE_KEY_AGREEMENT)
				{
					keyInfosString += "\t\t - Протоколи розподілу ключів;\n";
				}

				keyInfosString += "\tДовірені ід. відкр. ключа: " + 
					(info.isTrustedKeyIDs ? "Так" : "Ні") + "\n";
				keyInfosString += "\tІд. відкр. ключа:\n";
				for (int j = 0; j < info.keyIDs.Length; j++)
					keyInfosString += "\t\t - " + info.keyIDs[j] + ";\n";
				keyInfosString += "\n";
			}

			EUUtils.WriteFile(keyInfoReportPath, 
				System.Text.Encoding.UTF8.GetBytes(keyInfosString)); 
		}

		private bool ExportPrivateKey(IntPtr pkContext)
		{
			int index;
			IEUSignCP.EU_PRIVATE_KEY_INFO keyInfo;
			List<IEUSignCP.EU_PRIVATE_KEY_INFO> keyInfos = 
				new List<IEUSignCP.EU_PRIVATE_KEY_INFO>();
			IEUSignCP.EU_CERT_INFO_EX certInfo;
			byte[] container;
			byte[] certificate;
			string pkFileName;
			string pkFileNameBinary;
			string certFileName;
			int error;

			index = 0;
			while (true)
			{
				error = IEUSignCP.CtxEnumPrivateKeyInfo(
					pkContext, index, out keyInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error == IEUSignCP.EU_WARNING_END_OF_ENUM)
						break;

					return false;
				}

				keyInfos.Add(keyInfo);
				index++;
			}

			WriteKeyInfosFile(keyInfos);

			bool[] trustedKeyIDs = new bool[keyInfos.Count];
			string[] keyIDs = new string[keyInfos.Count];

			for (int i = 0; i < keyInfos.Count; i++) 
			{
				keyInfo = keyInfos[i];
				trustedKeyIDs[i] = keyInfo.isTrustedKeyIDs;
				keyIDs[i] = keyInfo.keyIDs[0];

				pkFileName = Path.Combine(
					rootDirectory, keyInfo.keyIDs[0] + ".pk8");
				pkFileNameBinary = Path.Combine(
					rootDirectory, keyInfo.keyIDs[0] + "(bin).pk8");

				container = null;
				error = IEUSignCP.CtxExportPrivateKeyContainer(
					pkContext, exportPassword, keyInfo.keyIDs[0],
					out container);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				certificate = null;
				error = IEUSignCP.CtxGetCertificateFromPrivateKey(
					pkContext, keyInfo.keyIDs[0], out certInfo, out certificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				error = IEUSignCP.CtxExportPrivateKeyContainerFile(
					pkContext, exportPassword, keyInfo.keyIDs[0], pkFileName);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				certFileName = Path.Combine(
					rootDirectory, keyInfo.keyIDs[0] + ".cer");

				if (!EUUtils.WriteFile(pkFileNameBinary, container) ||
					!EUUtils.WriteFile(certFileName, certificate))
				{
					return false;
				}
			}

			pkFileName = Path.Combine(
				rootDirectory, "Key-6.pfx");
			pkFileNameBinary = Path.Combine(
				rootDirectory, "Key-6(bin).pfx");

			container = null;
			error = IEUSignCP.CtxExportPrivateKeyPFXContainer(
				pkContext, exportPassword, true, trustedKeyIDs, keyIDs,
				out container);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			if (!EUUtils.WriteFile(pkFileNameBinary, container))
				return false;

			error = IEUSignCP.CtxExportPrivateKeyPFXContainerFile(
				pkContext, exportPassword, true, trustedKeyIDs, keyIDs,
				pkFileName);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			return true; 
		}

		#endregion

		#region PKeyVerificator: Test named privatekey functions

		private int ReadNamedPrivateKeys(IntPtr context,
			IEUSignCP.EU_KEY_MEDIA keyMedia,
			List<NamedKey> namedKeys,
			out List<IntPtr> pkContexts)
		{
			int error = IEUSignCP.EU_ERROR_NONE;

			pkContexts = new List<IntPtr>();

			foreach (NamedKey namedKey in namedKeys)
			{
				IntPtr pkContext;
				IEUSignCP.EU_CERT_OWNER_INFO info;

				error = IEUSignCP.CtxReadNamedPrivateKey(
					context, keyMedia, namedKey.label, namedKey.password,
					out pkContext, out info);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					FreeNamedPrivateKeys(pkContexts);

					return error;
				}

				pkContexts.Add(pkContext);
			}

			return error;
		}

		private void FreeNamedPrivateKeys(List<IntPtr> pkContexts)
		{
			foreach (IntPtr pkContext in pkContexts)
				IEUSignCP.CtxFreePrivateKey(pkContext);
		}

		private int DestroyNamedPrivateKeys(
			IntPtr context, IEUSignCP.EU_KEY_MEDIA keyMedia,
			List<NamedKey> namedKeys, bool ignoreErrors)
		{
			int error = IEUSignCP.EU_ERROR_NONE;

			foreach (NamedKey namedKey in namedKeys)
			{
				error = IEUSignCP.CtxDestroyNamedPrivateKey(
					context, keyMedia, namedKey.label, namedKey.password);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (!ignoreErrors)
						return error;
				}
			}

			return error;
		}

		private int NamedPrivateKeyGenerateTest(
			IntPtr context, IEUSignCP.EU_KEY_MEDIA keyMedia, 
			List<NamedKey> namedKeys)
		{
			int UAKeysType = IEUSignCP.EU_CERT_KEY_TYPE_DSTU4145;
			int UADSKeysSpec = (int) IEUSignCP.EU_DS_UA_KEY_LENGTH.EC_257;
			int UAKEPKeysSpec = (int) IEUSignCP.EU_KEP_UA_KEY_LENGTH.EC_431;
			string UAParamsPath = "";
			int internationalKeysType = IEUSignCP.EU_CERT_KEY_TYPE_UNKNOWN;
			int internationalKeysSpec = (int) IEUSignCP.EU_DS_RSA_KEY_LENGTH.RSA_2048;
			string internationalParamsPath = "";
			byte[] UARequest = new byte[0];
			string UAReqFileName = "";
			byte[] UAKEPRequest = new byte[0];
			string UAKEPReqFileName = "";
			byte[] internationalRequest = null;
			string internationalReqFileName = null;
			bool isExists;
			int error;

			for (int i = 0; i < namedKeysCount; i++)
			{
				NamedKey namedKey = namedKeys[i];

				error = IEUSignCP.CtxGenerateNamedPrivateKey(context, 
					keyMedia, namedKey.label, namedKey.password,
					UAKeysType, UADSKeysSpec, UAKEPKeysSpec, UAParamsPath,
					internationalKeysType, internationalKeysSpec, internationalParamsPath,
					ref UARequest, ref UAReqFileName, ref UAKEPRequest, ref UAKEPReqFileName,
					ref internationalRequest, ref internationalReqFileName);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					DestroyNamedPrivateKeys(context, keyMedia, 
						namedKeys, true);

					return error;
				}

				error = IEUSignCP.CtxIsNamedPrivateKeyExists(context,
					keyMedia, namedKey.label, namedKey.password,
					out isExists);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					DestroyNamedPrivateKeys(context, keyMedia,
						namedKeys, true);

					return error;
				}

				if (!isExists)
				{
					DestroyNamedPrivateKeys(context, keyMedia,
						namedKeys, true);

					return IEUSignCP.EU_ERROR_BAD_PRIVATE_KEY;
				}

				if (UARequest != null)
				{
					string filename = Path.Combine(rootDirectory,
						"EU-" + namedKey.label + ".p10");
					EUUtils.WriteFile(filename, UARequest);
				}

				if (UAKEPRequest != null)
				{
					string filename = Path.Combine(rootDirectory,
						"EU-KEP-" + namedKey.label + ".p10");
					EUUtils.WriteFile(filename, UAKEPRequest);
				}

				if (internationalRequest != null)
				{
					string filename = Path.Combine(rootDirectory,
						"EU-RSA-" + namedKey.label + ".p10");
					EUUtils.WriteFile(filename, internationalRequest);
				}
			}

			if (!EUSignCPOwnGUI.ShowConfirm("Для продовження тестування " +
					"необхідно отримати сертифікати для запитів (EU-*.p10), " +
					"що знаходяться в папці " + rootDirectory))
			{
				DestroyNamedPrivateKeys(context, keyMedia,
						namedKeys, true);

				return IEUSignCP.EU_ERROR_CANCELED_BY_GUI;
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int NamedPrivateKeyDestroyTest(IntPtr context,
			IEUSignCP.EU_KEY_MEDIA keyMedia, List<NamedKey> namedKeys)
		{
			return DestroyNamedPrivateKeys(context, keyMedia,
				namedKeys, false);
		}

		private int NamedPrivateKeySignTest(
			IntPtr context, IEUSignCP.EU_KEY_MEDIA keyMedia,
			List<NamedKey> namedKeys)
		{
			List<IntPtr> pkContexts;
			int error;
			byte[] data = TestData.GetByteArray();
			byte[] sign = null;
			int count;
			IEUSignCP.EU_SIGN_INFO info;

			error = ReadNamedPrivateKeys(context, keyMedia, 
				namedKeys, out pkContexts);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			foreach (IntPtr pkContext in pkContexts)
			{
				if (sign == null)
				{
					error = IEUSignCP.CtxSignData(pkContext,
						IEUSignCP.EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311,
						data, true, true, out sign);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						FreeNamedPrivateKeys(pkContexts);

						return error;
					}
				}
				else
				{
					error = IEUSignCP.CtxAppendSign(pkContext,
						IEUSignCP.EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311,
						data, sign, true, out sign);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						FreeNamedPrivateKeys(pkContexts);

						return error;
					}
				}
			}

			FreeNamedPrivateKeys(pkContexts);

			error = IEUSignCP.CtxGetSignsCount(context, sign, out count);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < count; i++)
			{
				error = IEUSignCP.CtxVerifyData(context, data, i, sign, out info);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				IEUSignCP.CtxFreeSignInfo(context, ref info);
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int NamedPrivateKeyGetInfoTest(
			IntPtr context, IEUSignCP.EU_KEY_MEDIA keyMedia,
			List<NamedKey> namedKeys)
		{
			int error = IEUSignCP.EU_ERROR_NONE;

			foreach (NamedKey namedKey in namedKeys)
			{
				byte[] privKeyInfo;

				error = IEUSignCP.CtxGetNamedPrivateKeyInfo(
					context, keyMedia, namedKey.label, namedKey.password,
					out privKeyInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;
			}

			return error;
		}

		private int NamedPrivateKeyInvalidPasswordTest(
			IntPtr context, IEUSignCP.EU_KEY_MEDIA keyMedia,
			List<NamedKey> namedKeys)
		{
			int error = IEUSignCP.EU_ERROR_NONE;

			foreach (NamedKey namedKey in namedKeys)
			{
				IntPtr pkContext;
				IEUSignCP.EU_CERT_OWNER_INFO info;

				error = IEUSignCP.CtxReadNamedPrivateKey(
					context, keyMedia, namedKey.label, namedKey.password,
					out pkContext, out info);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				IEUSignCP.CtxFreePrivateKey(pkContext);

				error = IEUSignCP.CtxReadNamedPrivateKey(
					context, keyMedia, namedKey.label, namedKey.password.Substring(1),
					out pkContext, out info);
				if (error != IEUSignCP.EU_ERROR_BAD_PRIVATE_KEY)
					return IEUSignCP.EU_ERROR_KEY_MEDIAS_READ_FAILED;

				error = IEUSignCP.CtxReadNamedPrivateKey(
					context, keyMedia, namedKey.label, namedKey.password,
					out pkContext, out info);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				IEUSignCP.CtxFreePrivateKey(pkContext);
			}

			return error;
		}

		private bool NamedPrivateKeyTest(IntPtr context)
		{
			IEUSignCP.EU_KEY_MEDIA keyMedia;
			List<NamedKey> namedKeys = new List<NamedKey>();
			int error;

			EUSignCPOwnGUI.ShowInfo("Тестування фунцій роботи з пристроєм з декількома ос. ключами");

			if (EUSignCPOwnGUI.UseOwnUI)
				error = EUSignCPOwnGUI.GetPrivateKeyMedia(out keyMedia);
			else
				error = IEUSignCP.GetPrivatekeyMedia(out keyMedia);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			for (int i = 0; i < namedKeysCount; i++)
			{
				NamedKey namedKey = new NamedKey(
					TestData.GetString(IEUSignCP.EU_NAMED_PRIVATE_KEY_LABEL_MAX_LENGTH),
					TestData.GetString(10));
				namedKeys.Add(namedKey);
			}

			error = NamedPrivateKeyGenerateTest(context, keyMedia, namedKeys);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			error = NamedPrivateKeyGetInfoTest(context, keyMedia, namedKeys);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				DestroyNamedPrivateKeys(context, keyMedia,
					namedKeys, true);

				return false;
			}

			error = NamedPrivateKeySignTest(context, keyMedia, namedKeys);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				DestroyNamedPrivateKeys(context, keyMedia,
					namedKeys, true);

				return false;
			}

			error = NamedPrivateKeyInvalidPasswordTest(context, keyMedia, namedKeys);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				DestroyNamedPrivateKeys(context, keyMedia,
					namedKeys, true);

				return false;
			}

			error = NamedPrivateKeyDestroyTest(context, keyMedia, namedKeys);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				DestroyNamedPrivateKeys(context, keyMedia,
					namedKeys, true);

				return false;
			}

			return true;
		}

		private bool ExportPrivateKeyTest(IntPtr context) 
		{
			IntPtr pkContext = IntPtr.Zero;

			EUSignCPOwnGUI.ShowInfo("Тестування фунції експорту ос. ключа");
			if (!EUSignCPOwnGUI.ReadPrivKeyContext(
					context, out pkContext))
			{
				return false;
			}

			if (!ExportPrivateKey(pkContext))
			{
				IEUSignCP.CtxFreePrivateKey(pkContext);

				return false;
			}

			IEUSignCP.CtxFreePrivateKey(pkContext);

			return true;
		}

		#endregion

		public static bool PerformTest()
		{
			PKeyVerificator verificator = new PKeyVerificator();

			IntPtr context = IntPtr.Zero;

			try
			{
				int error;

				error = IEUSignCP.CtxCreate(out context);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				if (!verificator.NamedPrivateKeyTest(context) ||
					!verificator.ExportPrivateKeyTest(context))
				{
					return false;
				}

				return true;
			}
			finally
			{
				if (context != IntPtr.Zero)
					IEUSignCP.CtxFree(context);
	
				verificator.Dispose();
				verificator = null;
			}
		}
	}
}
