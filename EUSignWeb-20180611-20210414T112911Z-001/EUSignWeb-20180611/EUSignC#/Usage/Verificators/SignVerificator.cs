using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EUSignCP;

namespace EUSignTestCS.Verificators
{
	class SignVerificator : IDisposable
	{
		private string dataString;
		private string hashString;
		private string signString;

		IntPtr hashContext;

		private byte[] dataBinary;
		private byte[] hashBinary;
		private byte[] signBinary;
		private byte[] verifiedDataBinary;

		private string rootDirectory;
		private string dataFilePath;
		private string bigDataFilePath;
		
		private int signersCount;
		private IEUSignCP.EU_SIGN_INFO signInfo;
		private IEUSignCP.EU_CERT_INFO_EX signerCertInfo;
		private byte[] signerCert;

		public SignVerificator()
		{
			dataString = TestData.GetString();
			dataBinary = TestData.GetByteArray();

			rootDirectory = Path.Combine(
				System.IO.Path.GetTempPath(), "SignTest" +
				DateTime.Now.ToString(" - yyyy-MM-dd HH-mm-ss"));

			dataFilePath = Path.Combine(
				rootDirectory, "Data.dat");
			bigDataFilePath = Path.Combine(
				rootDirectory, "DataBig.dat");

			TestData.CreateDirectory(rootDirectory);
			TestData.CreateTestFile(dataFilePath);
			TestData.CreateTestFile(bigDataFilePath,
				TestData.BigFileSize);
		}

		public void Dispose()
		{
			TestData.DeleteDirectory(rootDirectory);
		}

		private int IsRSASignSupported(IntPtr[] pkContexts, out bool isSupported)
		{
			int error;
			byte[] rsaCertificate;
			IEUSignCP.EU_CERT_INFO_EX certInfo;

			for (int i = 0; i < pkContexts.Length; i++)
			{
				error = IEUSignCP.CtxGetOwnCertificate(pkContexts[i],
					IEUSignCP.EU_CERT_KEY_TYPE_RSA,
					IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE,
					out certInfo, out rsaCertificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					isSupported = false;

					if (error != IEUSignCP.EU_ERROR_CERT_NOT_FOUND)
						return error;

					return IEUSignCP.EU_ERROR_NONE;
				}
			}

			isSupported = true;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int IsECDSASignSupported(IntPtr[] pkContexts, out bool isSupported)
		{
			int error;
			byte[] ecdsaCertificate;
			IEUSignCP.EU_CERT_INFO_EX certInfo;

			for (int i = 0; i < pkContexts.Length; i++)
			{
				error = IEUSignCP.CtxGetOwnCertificate(pkContexts[i],
					IEUSignCP.EU_CERT_KEY_TYPE_ECDSA,
					IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE,
					out certInfo, out ecdsaCertificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					isSupported = false;

					if (error != IEUSignCP.EU_ERROR_CERT_NOT_FOUND)
						return error;

					return IEUSignCP.EU_ERROR_NONE;
				}
			}

			isSupported = true;

			return IEUSignCP.EU_ERROR_NONE;
		}

		#region SignVerificator: Test hash functions

		private int CtxHashStringTest(
			IntPtr context, int hashAlgo, byte[] certificate)
		{
			string hash1;
			string hash2;
			int error;

			error = IEUSignCP.CtxHashData(context, hashAlgo,
				certificate, dataString, out hash1);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxHashDataBegin(context, hashAlgo,
				certificate, out hashContext);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxHashDataContinue(
				hashContext, dataString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.CtxFreeHash(hashContext);

				return error;
			}

			error = IEUSignCP.CtxHashDataEnd(
				hashContext, out hash2);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.CtxFreeHash(hashContext);

				return error;
			}

			IEUSignCP.CtxFreeHash(hashContext);

			if (!hash1.Equals(hash2))
				return IEUSignCP.EU_ERROR_BAD_PARAMETER;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxHashDataTest(
			IntPtr context, int hashAlgo, byte[] certificate)
		{
			byte[] hash1;
			byte[] hash2;
			int error;

			error = IEUSignCP.CtxHashData(context, hashAlgo,
				certificate, dataBinary, out hash1);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxHashDataBegin(context, hashAlgo,
				certificate, out hashContext);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxHashDataContinue(
				hashContext, dataBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.CtxFreeHash(hashContext);

				return error;
			}

			error = IEUSignCP.CtxHashDataEnd(
				hashContext, out hash2);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.CtxFreeHash(hashContext);

				return error;
			}

			IEUSignCP.CtxFreeHash(hashContext);

			if (!IEUSignCP.CompareArrays(hash1, hash2))
				return IEUSignCP.EU_ERROR_BAD_PARAMETER;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxHashFileTest(
			IntPtr context, int hashAlgo, byte[] certificate)
		{
			int error;

			error = IEUSignCP.CtxHashFile(context, hashAlgo,
				certificate, dataFilePath, out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxHashFile(context, hashAlgo,
				certificate, dataFilePath, out hashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxHashTest(IntPtr context,
			int hashAlgo, byte[] certificate = null)
		{
			int error;

			error = CtxHashDataTest(context,
				hashAlgo, certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxHashStringTest(context,
				hashAlgo, certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxHashFileTest(context,
				hashAlgo, certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private bool CtxHashTest(IntPtr context,
			byte[] certificate, byte[] rsaCertificate, 
			byte[] ecdsaCertificate)
		{
			int error;

			error = CtxHashTest(context,
				IEUSignCP.EU_CTX_HASH_ALGO_GOST34311);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			error = CtxHashTest(context,
				IEUSignCP.EU_CTX_HASH_ALGO_GOST34311,
				certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			int[] shaAlgos = {
				IEUSignCP.EU_CTX_HASH_ALGO_SHA160,
				IEUSignCP.EU_CTX_HASH_ALGO_SHA224,
				IEUSignCP.EU_CTX_HASH_ALGO_SHA256};

			for (int i = 0; i < shaAlgos.Length; i++)
			{
				error = CtxHashTest(context, shaAlgos[i]);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				if (rsaCertificate != null)
				{
					error = CtxHashTest(context,
						shaAlgos[i], rsaCertificate);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return false;
				}

				if (ecdsaCertificate != null)
				{
					error = CtxHashTest(context,
						shaAlgos[i], ecdsaCertificate);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return false;
				}
			}

			return true;
		}

		#endregion

		#region SignVerificator: Test sign hash functions

		private int CtxSignHashIterativeDataTest(
			IntPtr context, IntPtr[] pkContexts,
			byte[] signerCertificate,
			int hashAlgo, int signAlgo)
		{
			int error;
			IntPtr hashContext;

			error = IEUSignCP.CtxHashDataBegin(context,
				hashAlgo, signerCertificate, out hashContext);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < 5; i++)
			{
				error = IEUSignCP.CtxHashDataContinue(
					hashContext, dataBinary);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeHash(hashContext);

					return error;
				}
			}

			error = IEUSignCP.CtxSignHash(pkContexts[0], signAlgo,
				hashContext, true, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.CtxFreeHash(hashContext);

				return error;
			}

			for (int i = 0; i < pkContexts.Length; i++)
			{
				bool isAlreadySigned;

				error = IEUSignCP.CtxIsAlreadySigned(pkContexts[i],
					signAlgo, signString, out isAlreadySigned);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeHash(hashContext);
					return error;
				}

				if (!isAlreadySigned)
				{
					error = IEUSignCP.CtxAppendSignHash(
						pkContexts[i], signAlgo, hashContext,
						signString, true, out signString);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						IEUSignCP.CtxFreeHash(hashContext);
						return error;
					}
				}
			}

			error = IEUSignCP.CtxGetSignsCount(
				context, signString, out signersCount);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.CtxFreeHash(hashContext);
				return error;
			}

			for (int i = 0; i < signersCount; i++)
			{
				error = IEUSignCP.CtxVerifyHash(hashContext,
					i, signString, out signInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeHash(hashContext);
					return error;
				}

				error = IEUSignCP.CtxGetSignerInfo(context,
					i, signString, out signerCertInfo, out signerCert);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeHash(hashContext);
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);
					
					return error;
				}

				if ((signInfo.issuer == null) || (signInfo.issuer == "") ||
					(signInfo.serial == null) || (signInfo.serial == "") ||
					signInfo.issuer != signerCertInfo.issuer ||
					signInfo.serial != signerCertInfo.serial)
				{
					IEUSignCP.CtxFreeHash(hashContext);
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return IEUSignCP.EU_ERROR_BAD_PARAMETER;
				}

				IEUSignCP.CtxFreeSignInfo(context, ref signInfo);
			}

			IEUSignCP.CtxFreeHash(hashContext);

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxSignHashStringDataTest(
			IntPtr context, IntPtr[] pkContexts,
			byte[] signerCertificate,
			int hashAlgo, int signAlgo)
		{
			int error;

			error = IEUSignCP.CtxHashData(context, hashAlgo,
				signerCertificate, dataString, out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxSignHashValue(pkContexts[0], signAlgo,
				hashString, true, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < pkContexts.Length; i++)
			{
				bool isAlreadySigned;

				error = IEUSignCP.CtxIsAlreadySigned(pkContexts[i],
					signAlgo, signString, out isAlreadySigned);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (!isAlreadySigned)
				{
					error = IEUSignCP.CtxAppendSignHashValue(
						pkContexts[i], signAlgo, hashString,
						signString, true, out signString);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;
				}
			}

			error = IEUSignCP.CtxGetSignsCount(
				context, signString, out signersCount);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < signersCount; i++)
			{
				error = IEUSignCP.CtxVerifyHashValue(context,
					hashString, i, signString, out signInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				error = IEUSignCP.CtxGetSignerInfo(context,
					i, signString, out signerCertInfo, out signerCert);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return error;
				}

				if ((signInfo.issuer == null) || (signInfo.issuer == "") ||
					(signInfo.serial == null) || (signInfo.serial == "") ||
					signInfo.issuer != signerCertInfo.issuer ||
					signInfo.serial != signerCertInfo.serial)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return IEUSignCP.EU_ERROR_BAD_PARAMETER;
				}

				IEUSignCP.CtxFreeSignInfo(context, ref signInfo);
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxSignHashBinaryDataTest(
			IntPtr context, IntPtr[] pkContexts,
			byte[] signerCertificate,
			int hashAlgo, int signAlgo)
		{
			int error;

			error = IEUSignCP.CtxHashData(context, hashAlgo,
				signerCertificate, dataBinary, out hashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxSignHashValue(pkContexts[0], signAlgo,
				hashBinary, true, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < pkContexts.Length; i++)
			{
				bool isAlreadySigned;

				error = IEUSignCP.CtxIsAlreadySigned(pkContexts[i],
					signAlgo, signBinary, out isAlreadySigned);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (!isAlreadySigned)
				{
					error = IEUSignCP.CtxAppendSignHashValue(
						pkContexts[i], signAlgo, hashBinary,
						signBinary, true, out signBinary);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;
				}
			}

			error = IEUSignCP.CtxGetSignsCount(
				context, signBinary, out signersCount);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < signersCount; i++)
			{
				error = IEUSignCP.CtxVerifyHashValue(context,
					hashBinary, i, signBinary, out signInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				error = IEUSignCP.CtxGetSignerInfo(context,
					i, signBinary, out signerCertInfo, out signerCert);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return error;
				}

				if ((signInfo.issuer == null) || (signInfo.issuer == "") ||
					(signInfo.serial == null) || (signInfo.serial == "") ||
					signInfo.issuer != signerCertInfo.issuer ||
					signInfo.serial != signerCertInfo.serial)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return IEUSignCP.EU_ERROR_BAD_PARAMETER;
				}

				IEUSignCP.CtxFreeSignInfo(context, ref signInfo);
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxSignHashTest(IntPtr context,
			IntPtr[] pkContexts, int hashAlgo, int signAlgo,
			byte[] certificate)
		{
			int error;

			error = CtxSignHashIterativeDataTest(context, pkContexts,
				certificate, hashAlgo, signAlgo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxSignHashStringDataTest(context, pkContexts,
				certificate, hashAlgo, signAlgo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxSignHashBinaryDataTest(context, pkContexts,
				certificate, hashAlgo, signAlgo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private bool CtxSignHashTest(IntPtr context, 
			IntPtr[] pkContexts, byte[] certificate,
			byte[] rsaCertificate, byte[] ecdsaCertificate)
		{
			int error;

			error = CtxSignHashTest(context, pkContexts, 
				IEUSignCP.EU_CTX_HASH_ALGO_GOST34311,
				IEUSignCP.EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311,
				certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			if (rsaCertificate != null)
			{
				error = CtxSignHashTest(context, pkContexts,
					IEUSignCP.EU_CTX_HASH_ALGO_SHA160,
					IEUSignCP.EU_CTX_SIGN_ALGO_RSA_WITH_SHA,
					rsaCertificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;
			}

			if (ecdsaCertificate != null)
			{
				error = CtxSignHashTest(context, pkContexts,
					IEUSignCP.EU_CTX_HASH_ALGO_SHA160,
					IEUSignCP.EU_CTX_SIGN_ALGO_ECDSA_WITH_SHA,
					ecdsaCertificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;
			}

			return true;
		}

		#endregion

		#region SignVerificator: Test sign data functions

		private int CtxSignDataStringTest(IntPtr context,
			IntPtr[] pkContexts, int signAlgo, bool external)
		{
			int error;

			error = IEUSignCP.CtxSignData(pkContexts[0], signAlgo,
				dataString, external, false, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < pkContexts.Length; i++)
			{
				bool isAlreadySigned;

				error = IEUSignCP.CtxIsAlreadySigned(pkContexts[i],
					signAlgo, signString, out isAlreadySigned);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (!isAlreadySigned)
				{
					error = IEUSignCP.CtxAppendSign(
						pkContexts[i], signAlgo,
						external ? dataString : null,
						signString, true, out signString);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;
				}
			}

			error = IEUSignCP.CtxGetSignsCount(
				context, signString, out signersCount);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < signersCount; i++)
			{
				if (external)
				{
					error = IEUSignCP.CtxVerifyData(context,
						dataString, i, signString, out signInfo);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;
				}
				else 
				{
					byte[] signedDataBinary;

					error = IEUSignCP.CtxGetDataFromSignedData(
						context, signString, out signedDataBinary);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;

					error = IEUSignCP.CtxVerifyDataInternal(
						context, i, signString, out verifiedDataBinary,
						out signInfo);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;

					if (!IEUSignCP.CompareArrays(signedDataBinary,
							verifiedDataBinary))
					{
						IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

						return IEUSignCP.EU_ERROR_BAD_PARAMETER;
					}
				}

				error = IEUSignCP.CtxGetSignerInfo(context,
					i, signString, out signerCertInfo, out signerCert);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);
					
					return error;
				}

				if ((signInfo.issuer == null) || (signInfo.issuer == "") ||
					(signInfo.serial == null) || (signInfo.serial == "") ||
					signInfo.issuer != signerCertInfo.issuer ||
					signInfo.serial != signerCertInfo.serial)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return IEUSignCP.EU_ERROR_BAD_PARAMETER;
				}

				IEUSignCP.CtxFreeSignInfo(context, ref signInfo);
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxSignDataBinaryTest(IntPtr context,
			IntPtr[] pkContexts, int signAlgo, bool external)
		{
			int error;

			error = IEUSignCP.CtxSignData(pkContexts[0], signAlgo,
				dataBinary, external, false, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < pkContexts.Length; i++)
			{
				bool isAlreadySigned;

				error = IEUSignCP.CtxIsAlreadySigned(pkContexts[i],
					signAlgo, signBinary, out isAlreadySigned);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (!isAlreadySigned)
				{
					error = IEUSignCP.CtxAppendSign(
						pkContexts[i], signAlgo,
						external ? dataBinary : null,
						signBinary, true, out signBinary);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;
				}
			}

			error = IEUSignCP.CtxGetSignsCount(
				context, signBinary, out signersCount);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			if (!external)
			{
				byte[] signedDataBinary;

				error = IEUSignCP.CtxGetDataFromSignedData(
					context, signBinary, out signedDataBinary);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (!IEUSignCP.CompareArrays(dataBinary,
						signedDataBinary))
				{
					return IEUSignCP.EU_ERROR_BAD_PARAMETER;
				}
			}

			for (int i = 0; i < signersCount; i++)
			{
				if (external)
				{
					error = IEUSignCP.CtxVerifyData(context,
						dataBinary, i, signBinary, out signInfo);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;
				}
				else
				{
					error = IEUSignCP.CtxVerifyDataInternal(
						context, i, signBinary, out verifiedDataBinary,
						out signInfo);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;

					if (!IEUSignCP.CompareArrays(dataBinary,
							verifiedDataBinary))
					{
						IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

						return IEUSignCP.EU_ERROR_BAD_PARAMETER;
					}
				}

				error = IEUSignCP.CtxGetSignerInfo(context,
					i, signBinary, out signerCertInfo, out signerCert);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return error;
				}

				if ((signInfo.issuer == null) || (signInfo.issuer == "") ||
					(signInfo.serial == null) || (signInfo.serial == "") ||
					signInfo.issuer != signerCertInfo.issuer ||
					signInfo.serial != signerCertInfo.serial)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return IEUSignCP.EU_ERROR_BAD_PARAMETER;
				}

				IEUSignCP.CtxFreeSignInfo(context, ref signInfo);
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxSignDataTest(IntPtr context,
			IntPtr[] pkContexts, int signAlgo)
		{
			int error;

			error = CtxSignDataStringTest(context,
				pkContexts, signAlgo, true);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxSignDataStringTest(context,
				pkContexts, signAlgo, false);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxSignDataBinaryTest(context,
				pkContexts, signAlgo, true);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxSignDataBinaryTest(context,
				pkContexts, signAlgo, false);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private bool CtxSignDataTest(IntPtr context,
			IntPtr[] pkContexts, byte[] certificate,
			byte[] rsaCertificate, byte[] ecdsaCertificate)
		{
			int error;

			error = CtxSignDataTest(context, pkContexts,
				IEUSignCP.EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			if (rsaCertificate != null)
			{
				error = CtxSignDataTest(context, pkContexts,
					IEUSignCP.EU_CTX_SIGN_ALGO_RSA_WITH_SHA);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;
			}

			if (ecdsaCertificate != null)
			{
				error = CtxSignDataTest(context, pkContexts,
					IEUSignCP.EU_CTX_SIGN_ALGO_ECDSA_WITH_SHA);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;
			}

			return true;
		}

		#endregion

		#region SignVerificator: Test sign file functions

		private int CtxSignFile(IntPtr context, IntPtr[] pkContexts,
			string filePath, int signAlgo, bool external)
		{
			int error;

			string signedFilePath = filePath + ".p7s";
			string verifiedFilePath = filePath + ".new";

			error = IEUSignCP.CtxSignFile(pkContexts[0], signAlgo,
				filePath, external, true, signedFilePath);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < pkContexts.Length; i++)
			{
				bool isAlreadySigned;
				string tmpSignedFilePath = filePath + ".p7s.tmp";

				error = IEUSignCP.CtxIsFileAlreadySigned(pkContexts[i],
					signAlgo, signedFilePath, out isAlreadySigned);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (!isAlreadySigned)
				{
					error = IEUSignCP.CtxAppendSignFile(
						pkContexts[i], signAlgo,
						external ? filePath : null,
						signedFilePath, true, tmpSignedFilePath);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return error;

					TestData.ReplaceFile(tmpSignedFilePath,
						signedFilePath);
				}
			}

			error = IEUSignCP.CtxGetFileSignsCount(
				context, signedFilePath, out signersCount);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < signersCount; i++)
			{
				error = IEUSignCP.CtxVerifyFile(context, i,
					signedFilePath, external ? filePath : verifiedFilePath,
					out signInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				error = IEUSignCP.CtxGetFileSignerInfo(context, i,
					signedFilePath, out signerCertInfo, out signerCert);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return error;
				}

				if ((signInfo.issuer == null) || (signInfo.issuer == "") ||
					(signInfo.serial == null) || (signInfo.serial == "") ||
					signInfo.issuer != signerCertInfo.issuer ||
					signInfo.serial != signerCertInfo.serial)
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return IEUSignCP.EU_ERROR_BAD_PARAMETER;
				}

				IEUSignCP.CtxFreeSignInfo(context, ref signInfo);
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxSignFile(IntPtr context,
			IntPtr[] pkContexts, int signAlgo, bool external)
		{
			int error;

			error = CtxSignFile(context, pkContexts,
				dataFilePath, signAlgo, external);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxSignFile(context, pkContexts,
				bigDataFilePath, signAlgo, external);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private bool CtxSignFile(IntPtr context,
			IntPtr[] pkContexts, byte[] certificate,
			byte[] rsaCertificate, byte[] ecdsaCertificate)
		{
			int error;

			error = CtxSignFile(context, pkContexts,
				IEUSignCP.EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311,
				true);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			error = CtxSignFile(context, pkContexts,
				IEUSignCP.EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311,
				false);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			if (rsaCertificate != null)
			{
				error = CtxSignFile(context, pkContexts,
					IEUSignCP.EU_CTX_SIGN_ALGO_RSA_WITH_SHA,
					true);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				error = CtxSignFile(context, pkContexts,
					IEUSignCP.EU_CTX_SIGN_ALGO_RSA_WITH_SHA,
					false);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;
			}

			if (ecdsaCertificate != null)
			{
				error = CtxSignFile(context, pkContexts,
					IEUSignCP.EU_CTX_SIGN_ALGO_ECDSA_WITH_SHA,
					true);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				error = CtxSignFile(context, pkContexts,
					IEUSignCP.EU_CTX_SIGN_ALGO_ECDSA_WITH_SHA,
					false);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;
			}


			return true;
		}
		
		#endregion

		#region SignVerificator: Test remote sign functions

		private int CtxRemoteSignStringTest(IntPtr context,
			IntPtr pkContext, int signAlgo, int hashAlgo,
			 byte[] signerCert, bool external)
		{
			int error;
			string signer;

			error = IEUSignCP.CtxHashData(context, hashAlgo, 
				signerCert, dataString, out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxCreateEmptySign(context,
				signAlgo, external ? null : dataString, 
				signerCert, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxCreateSigner(pkContext,
				signAlgo, hashString, out signer);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxAppendSigner(context,
				signAlgo, signer, signerCert,
				signString, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			if (external)
			{
				error = IEUSignCP.CtxVerifyData(context,
					dataString, 0, signString, out signInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;
			}
			else
			{
				error = IEUSignCP.CtxVerifyDataInternal(context,
					0, signString, out verifiedDataBinary, out signInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;
			}

			IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxRemoteSignBinaryTest(IntPtr context,
			IntPtr pkContext, int signAlgo, int hashAlgo,
			 byte[] signerCert, bool external)
		{
			int error;
			byte[] signer;

			error = IEUSignCP.CtxHashData(context, hashAlgo,
				signerCert, dataBinary, out hashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxCreateEmptySign(context,
				signAlgo, external ? null : dataBinary,
				signerCert, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxCreateSigner(pkContext,
				signAlgo, hashBinary, out signer);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.CtxAppendSigner(context,
				signAlgo, signer, signerCert,
				signBinary, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			if (external)
			{
				error = IEUSignCP.CtxVerifyData(context,
					dataBinary, 0, signBinary, out signInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;
			}
			else
			{
				error = IEUSignCP.CtxVerifyDataInternal(context,
					0, signBinary, out verifiedDataBinary, out signInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (!IEUSignCP.CompareArrays(dataBinary,
					verifiedDataBinary))
				{
					IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

					return IEUSignCP.EU_ERROR_BAD_PARAMETER;
				}
			}

			IEUSignCP.CtxFreeSignInfo(context, ref signInfo);

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int CtxRemoteSignTest(IntPtr context,
			IntPtr pkContext, int signAlgo, int hashAlgo,
			byte[] signerCert, bool external)
		{
			int error;

			error = CtxRemoteSignStringTest(context, pkContext,
				signAlgo, hashAlgo, signerCert, external);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = CtxRemoteSignBinaryTest(context, pkContext,
				signAlgo, hashAlgo, signerCert, external);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private bool CtxRemoteSignTest(IntPtr context,
			IntPtr[] pkContexts, byte[] certificate,
			byte[] rsaCertificate, byte[] ecdsaCertificate)
		{
			int error;

			error = CtxRemoteSignTest(context, pkContexts[0],
				IEUSignCP.EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311,
				IEUSignCP.EU_CTX_HASH_ALGO_GOST34311,
				certificate, true);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			error = CtxRemoteSignTest(context, pkContexts[0],
				IEUSignCP.EU_CTX_SIGN_ALGO_DSTU4145_WITH_GOST34311,
				IEUSignCP.EU_CTX_HASH_ALGO_GOST34311,
				certificate, false);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			if (rsaCertificate != null)
			{
				error = CtxRemoteSignTest(context, pkContexts[0],
					IEUSignCP.EU_CTX_SIGN_ALGO_RSA_WITH_SHA,
					IEUSignCP.EU_CTX_HASH_ALGO_SHA160,
					rsaCertificate, true);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				error = CtxRemoteSignTest(context, pkContexts[0],
					IEUSignCP.EU_CTX_SIGN_ALGO_RSA_WITH_SHA,
					IEUSignCP.EU_CTX_HASH_ALGO_SHA160,
					rsaCertificate, false);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;
			}

			if (ecdsaCertificate != null)
			{
				error = CtxRemoteSignTest(context, pkContexts[0],
					IEUSignCP.EU_CTX_SIGN_ALGO_ECDSA_WITH_SHA,
					IEUSignCP.EU_CTX_HASH_ALGO_SHA160,
					ecdsaCertificate, true);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				error = CtxRemoteSignTest(context, pkContexts[0],
					IEUSignCP.EU_CTX_SIGN_ALGO_ECDSA_WITH_SHA,
					IEUSignCP.EU_CTX_HASH_ALGO_SHA160,
					ecdsaCertificate, false);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;
			}

			return true;
		}

		#endregion

		public static bool PerformTest()
		{
			SignVerificator verificator = new SignVerificator();

			IntPtr context = IntPtr.Zero;
			IntPtr[] pkContexts = new IntPtr[TestData.SignKeys];

			try
			{
				int error;
				IEUSignCP.EU_CERT_INFO_EX info;
				byte[] certificate, rsaCertificate, ecdsaCertificate;
				bool isRSASignSupported;
				bool isECDSASignSupported;

				error = IEUSignCP.CtxCreate(out context);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				EUSignCPOwnGUI.ShowInfo("Тестування підпису " +
					"з одночасним використанням " + pkContexts.Length +
					" ключ(ів)");

				for (int i = 0; i < pkContexts.Length; i++)
				{
					if (!EUSignCPOwnGUI.ReadPrivKeyContext(
							context, out pkContexts[i]))
					{
						return false;
					}
				}

				error = IEUSignCP.CtxGetOwnCertificate(pkContexts[0],
					IEUSignCP.EU_CERT_KEY_TYPE_DSTU4145,
					IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE,
					out info, out certificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				error = verificator.IsRSASignSupported(pkContexts, 
					out isRSASignSupported);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				if (isRSASignSupported)
				{
					error = IEUSignCP.CtxGetOwnCertificate(pkContexts[0],
						IEUSignCP.EU_CERT_KEY_TYPE_RSA,
						IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE,
						out info, out rsaCertificate);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return false;
				}
				else
					rsaCertificate = null;

				error = verificator.IsECDSASignSupported(pkContexts,
					out isECDSASignSupported);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return false;

				if (isECDSASignSupported)
				{
					error = IEUSignCP.CtxGetOwnCertificate(pkContexts[0],
						IEUSignCP.EU_CERT_KEY_TYPE_ECDSA,
						IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE,
						out info, out ecdsaCertificate);
					if (error != IEUSignCP.EU_ERROR_NONE)
						return false;
				}
				else
					ecdsaCertificate = null;

				if (!verificator.CtxHashTest(context,
						certificate, rsaCertificate, ecdsaCertificate) ||
					!verificator.CtxSignHashTest(context,
						pkContexts, certificate, rsaCertificate, ecdsaCertificate) ||
					!verificator.CtxSignDataTest(context,
						pkContexts, certificate, rsaCertificate, ecdsaCertificate) ||
					!verificator.CtxSignFile(context,
						pkContexts, certificate, rsaCertificate, ecdsaCertificate) ||
					!verificator.CtxRemoteSignTest(context,
						pkContexts, certificate, rsaCertificate, ecdsaCertificate))
				{
					return false;
				}

				return true;
			}
			finally
			{
				for (int i = 0; i < pkContexts.Length; i++)
					if (pkContexts[i] != IntPtr.Zero)
						IEUSignCP.CtxFreePrivateKey(pkContexts[i]);

				if (context != IntPtr.Zero)
					IEUSignCP.CtxFree(context);
	
				verificator.Dispose();
				verificator = null;
			}
		}
	}
}
