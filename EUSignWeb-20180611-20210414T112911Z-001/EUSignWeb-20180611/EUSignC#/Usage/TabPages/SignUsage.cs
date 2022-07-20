using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EUSignCP;
using System.IO;
using EUSignTestCS.Verificators;

namespace EUSignTestCS.TabPages
{
	public partial class SignUsage : UserControl, IUsageTabPagesInterface
	{
		int curSignDataIndex;
		int curSignFileIndex;

		private void ChangeControlsState(bool enabled)
		{
			bool isPrivKeyReaded;

			isPrivKeyReaded = IEUSignCP.IsInitialized() &&
				IEUSignCP.IsPrivateKeyReaded();

			checkBoxInternalSign.Enabled = enabled;
			checkBoxHashSign.Enabled = enabled;
			checkBoxRawSign.Enabled = enabled;

			comboBoxSignType.SelectedIndex = 0;
			comboBoxSignType.Enabled = isPrivKeyReaded;
			checkBoxAddContentTimeStamp.Enabled = isPrivKeyReaded;
			checkBoxAddCACerts.Enabled = isPrivKeyReaded;

			buttonSignContextTest.Enabled = enabled;
			
			textBoxDataToSign.Enabled = isPrivKeyReaded;
			buttonSignData.Enabled = isPrivKeyReaded;
			buttonAppendSign.Enabled = isPrivKeyReaded;
			richTextBoxSignedData.Enabled = enabled;
			buttonVerifyData.Enabled = enabled;
			buttonVerifyDataNext.Enabled = enabled;
			buttonDataSignerCertificate.Enabled = enabled;
			
			textBoxFileToSign.Enabled = isPrivKeyReaded;
			buttonChooseFileToSing.Enabled = isPrivKeyReaded;
			buttonSignFile.Enabled = isPrivKeyReaded;
			buttonAppendFileSign.Enabled = isPrivKeyReaded;
			richTextBoxSignedFileData.Enabled = enabled;
			textBoxSignedFile.Enabled = enabled;
			buttonVerifyFile.Enabled = enabled;
			buttonVerifyFileNext.Enabled = enabled;
			buttonFileSignerCertificate.Enabled = enabled;

			buttonSignDataTest.Enabled = isPrivKeyReaded;
			buttonSignFileTest.Enabled = isPrivKeyReaded;
		}

		private void ClearSettings()
		{
			checkBoxInternalSign.Checked = false;
			checkBoxHashSign.Checked = false;
			checkBoxRawSign.Checked = false;
			
			checkBoxAppendCerificate.Checked = false;
			checkBoxAppendCerificate.Enabled = false;

			checkBoxHashParamsFromCert.Checked = false;
			checkBoxHashParamsFromCert.Enabled = false;

			comboBoxSignType.SelectedIndex = 0;
			checkBoxAddContentTimeStamp.Checked = true;
			checkBoxAddCACerts.Checked = true;
			checkBoxAddCACerts.Enabled = false;
		}

		private void ClearData(bool all)
		{
			textBoxDataToSign.Text = "";
			textBoxFileToSign.Text = "";

			if (all)
			{
				richTextBoxSignedData.Text = "";
				buttonVerifyDataNext.Text = "Перевірити наступний...";

				richTextBoxSignedFileData.Text = "";
				textBoxSignedFile.Text = "";
				buttonVerifyFileNext.Text = "Перевірити наступний...";

				curSignDataIndex = 0;
				curSignFileIndex = 0;
			}
		}

		private void checkBoxInternalSign_CheckedChanged(
			object sender, EventArgs e)
		{
			bool useInternal = checkBoxInternalSign.Checked;

			checkBoxHashSign.Enabled = !useInternal;
			checkBoxHashParamsFromCert.Enabled = useInternal
				&& checkBoxHashSign.Checked;

			checkBoxRawSign.Enabled = !useInternal;
			checkBoxAppendCerificate.Enabled = useInternal;
		}

		private void checkBoxRawSign_CheckedChanged(
			object sender, EventArgs e)
		{
			checkBoxInternalSign.Enabled = !checkBoxHashSign.Checked &&
				!checkBoxRawSign.Checked;
		}

		private void checkBoxHashSign_CheckedChanged(
			object sender, EventArgs e)
		{
			checkBoxInternalSign.Enabled = !checkBoxHashSign.Checked &&
				!checkBoxRawSign.Checked;

			checkBoxHashParamsFromCert.Enabled = checkBoxHashSign.Checked;
		}

		private void comboBoxSignType_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = comboBoxSignType.SelectedIndex;
			int[] signTypes =
			{
				IEUSignCP.EU_SIGN_TYPE_CADES_BES,
				IEUSignCP.EU_SIGN_TYPE_CADES_T,
				IEUSignCP.EU_SIGN_TYPE_CADES_C,
				IEUSignCP.EU_SIGN_TYPE_CADES_X_LONG
			};
			int signType = signTypes[selectedIndex];

			IEUSignCP.SetRuntimeParameter(
				IEUSignCP.EU_SIGN_TYPE_PARAMETER, signType);

			checkBoxAddCACerts.Enabled =
				(signType == IEUSignCP.EU_SIGN_TYPE_CADES_X_LONG);
		}

		private void checkBoxAddContentTimeStamp_CheckedChanged(object sender, EventArgs e)
		{
			IEUSignCP.SetRuntimeParameter(
				IEUSignCP.EU_SIGN_INCLUDE_CONTENT_TIME_STAMP_PARAMETER,
				checkBoxAddContentTimeStamp.Checked);
		}

		private void checkBoxAddCACerts_CheckedChanged(object sender, EventArgs e)
		{
			IEUSignCP.SetRuntimeParameter(
				IEUSignCP.EU_SIGN_INCLUDE_CA_CERTIFICATES_PARAMETER,
				checkBoxAddCACerts.Checked);
		}

		private bool GetOwnSignCertificate(int publicKeyType,
			out IEUSignCP.EU_CERT_INFO_EX info)
		{
			int error;
			int index = 0;

			while (true)
			{
				error = IEUSignCP.EnumOwnCertificates(index, out info);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error == IEUSignCP.EU_WARNING_END_OF_ENUM)
					{
						EUSignCPOwnGUI.ShowError(
							"Власний сертифікат підпису з відкритим ключем " + 
							(publicKeyType == IEUSignCP.EU_CERT_KEY_TYPE_DSTU4145 ? 
							"ДСТУ-4145" : "RSA") + " не знайдено");
					}
					else
					{
						EUSignCPOwnGUI.ShowError(error);
					}

					return false;
				}

				if (info.publicKeyType == publicKeyType &&
					(info.keyUsageType & IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE) ==
						IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE)
				{
					return true;
				}

				index++;
			}
		}

		private int GetOwnSignIndex(string sign, 
			int publicKeyType, out int signIndex)
		{
			int signers;
			IEUSignCP.EU_CERT_INFO_EX signerInfo;
			IEUSignCP.EU_CERT_INFO_EX ownInfo;
			byte[] cert = null;
			int error;

			signIndex = -1;

			if (!GetOwnSignCertificate(publicKeyType, out ownInfo))
				return IEUSignCP.EU_ERROR_BAD_PARAMETER;

			error = IEUSignCP.GetSignsCount(sign, out signers);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < signers; i++)
			{
				error = IEUSignCP.GetSignerInfo(
					i, sign, out signerInfo, ref cert);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (signerInfo.issuer == ownInfo.issuer &&
					signerInfo.serial == ownInfo.serial)
				{
					signIndex = i;
					return IEUSignCP.EU_ERROR_NONE;
				}
			}

			return IEUSignCP.EU_WARNING_END_OF_ENUM;
		}

		private void ShowSignerInfo(IEUSignCP.EU_CERT_INFO_EX info,
			byte[] certificate)
		{
			if (EUSignCPOwnGUI.ShowConfirm(
				"Зберегти сертифікат підписувача?"))
			{
				saveFileDialog.FileName = "EU-" +
					info.serial.ToUpper() + ".cer";
				saveFileDialog.Title = "Оберіть файл " +
					" для збереження сертифікату";

				if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
					return;

				EUUtils.WriteFile(saveFileDialog.FileName, certificate);
			}

			EUSignCPOwnGUI.ShowCertificate(info);
		}

		private int HashData(string data, out string hash)
		{
			int error;

			hash = "";

			if (checkBoxHashParamsFromCert.Checked)
			{
				IEUSignCP.EU_CERT_INFO_EX certInfo;
				byte[] certificate;

				error = EUSignCPOwnGUI.SelectCertificate(
						"Сертифікати користувачів-отримувачів", 
						EU_CERTIFICATE_TYPE.CERT_TYPE_END_USER, 
						out certInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				error = IEUSignCP.GetCertificate(certInfo.issuer, 
					certInfo.serial, out certificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				error = IEUSignCP.HashDataBeginWithParams(certificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;
			}

			error = IEUSignCP.HashDataContinue(data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.ResetOperation();
				return error;
			}

			error = IEUSignCP.HashDataEnd(out hash);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.ResetOperation();
				return error;
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private void SignData(object sender, EventArgs e)
		{
			if (!IEUSignCP.IsPrivateKeyReaded())
			{
				EUSignCPOwnGUI.ShowError(
					"Особистий ключ не зчитано");
				return;
			}

			string data;
			string hash, sign;
			int error;

			richTextBoxSignedData.Text = "";
			curSignDataIndex = 0;
			buttonVerifyDataNext.Text = "Перевірити наступний...";

			data = textBoxDataToSign.Text;

			hash = "";
			if (checkBoxHashSign.Enabled && checkBoxHashSign.Checked)
			{
				error = HashData(data, out hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error != IEUSignCP.EU_ERROR_CANCELED_BY_GUI)
						EUSignCPOwnGUI.ShowError(error);
					return;
				}
			}

			if (checkBoxRawSign.Checked)
			{
				if (checkBoxHashSign.Checked)
					error = IEUSignCP.RawSignHash(hash, out sign);
				else
					error = IEUSignCP.RawSignData(data, out sign);
			}
			else if (checkBoxInternalSign.Checked)
			{
				error = IEUSignCP.SignDataInternal(
					checkBoxAppendCerificate.Checked, data, out sign);
			}
			else
			{
				if (checkBoxHashSign.Checked)
					error = IEUSignCP.SignHash(hash, out sign);
				else
					error = IEUSignCP.SignData(data, out sign);
			}

			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			richTextBoxSignedData.Text = sign;
		}

		private void AppendSignToData(object sender, EventArgs e)
		{
			if (!IEUSignCP.IsPrivateKeyReaded())
			{
				EUSignCPOwnGUI.ShowError(
					"Особистий ключ не зчитано");
				return;
			}

			if (checkBoxRawSign.Checked)
			{
				EUSignCPOwnGUI.ShowError("Додавання підпису " +
					"до простого підпису не підтримується");
				return;
			}

			if (richTextBoxSignedData.Text == "")
			{
				EUSignCPOwnGUI.ShowError("Підпис відсутній");
				return;
			}

			string data, signedData;
			string hash, sign = "";
			bool isAlreadySigned;
			int error;

			data = textBoxDataToSign.Text;
			signedData = richTextBoxSignedData.Text;

			error = IEUSignCP.IsAlreadySigned(signedData,
				out isAlreadySigned);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (isAlreadySigned)
			{
				if (!EUSignCPOwnGUI.ShowConfirm(
						"Дані вже підписані користувачем. Перепідписати?"))
				{
					return;
				}

				int signIndex;
				string newSign;

				error = GetOwnSignIndex(signedData,
					IEUSignCP.EU_CERT_KEY_TYPE_DSTU4145, out signIndex);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				error = IEUSignCP.RemoveSign(signIndex,
					signedData, out newSign);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				signedData = newSign;
			}

			hash = "";
			if (checkBoxHashSign.Enabled && checkBoxHashSign.Checked)
			{
				error = HashData(data, out hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error != IEUSignCP.EU_ERROR_CANCELED_BY_GUI)
						EUSignCPOwnGUI.ShowError(error);
					return;
				}
			}

			if (checkBoxInternalSign.Checked)
			{
				error = IEUSignCP.AppendSignInternal(
					checkBoxAppendCerificate.Checked,
					signedData, ref sign);
			}
			else
			{
				if (checkBoxHashSign.Checked)
					error = IEUSignCP.AppendSignHash(hash, signedData, ref sign);
				else
					error = IEUSignCP.AppendSign(data, signedData, ref sign);
			}

			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			richTextBoxSignedData.Text = sign;
			curSignDataIndex = 0;
			buttonVerifyDataNext.Text = "Перевірити наступний...";
		}

		private void VerifyData(object sender, EventArgs e)
		{
			string data;
			string hash, sign;
			IEUSignCP.EU_SIGN_INFO signInfo;
			int error;

			if (richTextBoxSignedData.Text == "")
			{
				EUSignCPOwnGUI.ShowError("Підпис відсутній");
				return;
			}

			data = textBoxDataToSign.Text;
			sign = richTextBoxSignedData.Text;

			hash = "";
			if (checkBoxHashSign.Enabled && checkBoxHashSign.Checked)
			{
				error = HashData(data, out hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error != IEUSignCP.EU_ERROR_CANCELED_BY_GUI)
						EUSignCPOwnGUI.ShowError(error);
					return;
				}
			}

			if (checkBoxRawSign.Checked)
			{
				if (checkBoxHashSign.Checked)
				{
					error = IEUSignCP.RawVerifyHash(hash,
						sign, out signInfo);
				}
				else
				{
					error = IEUSignCP.RawVerifyData(data,
						sign, out signInfo);
				}
			}
			else if (checkBoxInternalSign.Checked)
			{
				byte[] binaryData;
				error = IEUSignCP.VerifyDataInternal(sign, 
					out binaryData, out signInfo);
			}
			else
			{
				if (checkBoxHashSign.Checked)
				{
					error = IEUSignCP.VerifyHash(hash,
						sign, out signInfo);
				}
				else
				{
					error = IEUSignCP.VerifyData(data,
						sign, out signInfo);
				}
			}

			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);

			IEUSignCP.FreeSignInfo(ref signInfo);
		}

		private void VerifyDataNext(object sender, EventArgs e)
		{
			string data;
			string hash, sign;
			IEUSignCP.EU_SIGN_INFO signInfo;
			int signCount;
			int error;

			if (checkBoxRawSign.Checked)
			{
				EUSignCPOwnGUI.ShowError("Додавання підпису " +
					"до простого підпису не підтримується");
				return;
			}

			if (richTextBoxSignedData.Text == "")
			{
				EUSignCPOwnGUI.ShowError("Підпис відсутній");
				return;
			}

			data = textBoxDataToSign.Text;
			sign = richTextBoxSignedData.Text;

			error = IEUSignCP.GetSignsCount(sign, out signCount);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (signCount <= 0)
			{
				EUSignCPOwnGUI.ShowError("Підписи відсутні");
				return;
			}

			buttonVerifyDataNext.Text = "Перевірити (" + (curSignDataIndex + 1)
				+ " з " + signCount + ")...";
			hash = "";

			if (checkBoxHashSign.Enabled && checkBoxHashSign.Checked)
			{
				error = HashData(data, out hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error != IEUSignCP.EU_ERROR_CANCELED_BY_GUI)
					{
						EUSignCPOwnGUI.ShowError(error);
						curSignDataIndex = (curSignDataIndex + 1) % signCount;
					}
					return;
				}
			}

			if (checkBoxInternalSign.Checked)
			{
				byte[] binaryData;
				error = IEUSignCP.VerifyDataInternalSpecific(
					curSignDataIndex, sign,
					out binaryData, out signInfo);
			}
			else
			{
				if (checkBoxHashSign.Checked)
				{
					error = IEUSignCP.VerifyHashSpecific(hash,
						curSignDataIndex, sign, out signInfo);
				}
				else
				{
					error = IEUSignCP.VerifyDataSpecific(data,
						curSignDataIndex, sign, out signInfo);
				}
			}

			curSignDataIndex = (curSignDataIndex + 1) % signCount;

			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);

			IEUSignCP.FreeSignInfo(ref signInfo);
		}

		private void ShowDataSignerCertificate(string signature)
		{
			if (signature == "")
			{
				EUSignCPOwnGUI.ShowError("Підпис відсутній");
				return;
			}

			IEUSignCP.EU_CERT_INFO_EX info = new IEUSignCP.EU_CERT_INFO_EX();
			byte[] certificate = new byte[0];
			int error;

			error = IEUSignCP.GetSignerInfo(curSignDataIndex, signature,
				out info, ref certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			ShowSignerInfo(info, certificate);
		}

		private int GetFileOwnSignIndex(string fileName,
			int publicKeyType, out int signIndex)
		{
			int signers;
			IEUSignCP.EU_CERT_INFO_EX signerInfo;
			IEUSignCP.EU_CERT_INFO_EX ownInfo;
			byte[] cert = null;
			int error;

			signIndex = -1;

			if (!GetOwnSignCertificate(publicKeyType, out ownInfo))
				return IEUSignCP.EU_ERROR_BAD_PARAMETER;

			error = IEUSignCP.GetFileSignsCount(fileName, out signers);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			for (int i = 0; i < signers; i++)
			{
				error = IEUSignCP.GetFileSignerInfo(
					i, fileName, out signerInfo, ref cert);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				if (signerInfo.issuer == ownInfo.issuer &&
					signerInfo.serial == ownInfo.serial)
				{
					signIndex = i;
					return IEUSignCP.EU_ERROR_NONE;
				}
			}

			return IEUSignCP.EU_WARNING_END_OF_ENUM;
		}

		private void ShowDataSignerCertificate(object sender, EventArgs e)
		{
			ShowDataSignerCertificate(richTextBoxSignedData.Text);
		}

		private int HashFile(string fileName, ref string hash)
		{
			int error;

			if (checkBoxHashParamsFromCert.Checked)
			{
				IEUSignCP.EU_CERT_INFO_EX certInfo;
				byte[] certificate;

				error = EUSignCPOwnGUI.SelectCertificate(
						"Сертифікати користувачів-отримувачів",
						EU_CERTIFICATE_TYPE.CERT_TYPE_END_USER,
						out certInfo);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				error = IEUSignCP.GetCertificate(certInfo.issuer,
					certInfo.serial, out certificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;

				error = IEUSignCP.HashFileWithParams(certificate,
					fileName, ref hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;
			}
			else
			{
				error = IEUSignCP.HashFile(fileName, out hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
					return error;
			}

			return IEUSignCP.EU_ERROR_NONE;
		}

		private void SignFile(object sender, EventArgs e)
		{
			if (!IEUSignCP.IsPrivateKeyReaded())
			{
				EUSignCPOwnGUI.ShowError(
					"Особистий ключ не зчитано");
				return;
			}

			if (textBoxFileToSign.Text == "")
			{
				EUSignCPOwnGUI.ShowError(
					"Файл для підпису не обрано");
				return;
			}

			string fileName, signedFileName;
			int error;

			richTextBoxSignedFileData.Text = "";
			textBoxSignedFile.Text = "";
			curSignFileIndex = 0;
			buttonVerifyFileNext.Text = "Перевірити наступний...";

			fileName = textBoxFileToSign.Text;

			if (checkBoxHashSign.Enabled && checkBoxHashSign.Checked)
			{
				string hash = "";
				string sign = "";

				error = HashFile(fileName, ref hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error != IEUSignCP.EU_ERROR_CANCELED_BY_GUI)
						EUSignCPOwnGUI.ShowError(error);
					return;
				}
				if (checkBoxRawSign.Checked)
					error = IEUSignCP.RawSignHash(hash, out sign);
				else
					error = IEUSignCP.SignHash(hash, out sign);

				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				richTextBoxSignedFileData.Text = sign;
			}
			else if (checkBoxRawSign.Checked)
			{
				signedFileName = fileName + ".raw.sig";
				error = IEUSignCP.RawSignFile(fileName,
					signedFileName);

				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				textBoxSignedFile.Text = signedFileName;
			}
			else
			{
				signedFileName = fileName + ".p7s";
				error = IEUSignCP.SignFile(fileName,
					signedFileName, !checkBoxInternalSign.Checked);

				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				textBoxSignedFile.Text = signedFileName;
			}

			EUSignCPOwnGUI.ShowInfo("Файл успішно підписано");
		}

		private void AppendSignToFile(object sender, EventArgs e)
		{
			if (!IEUSignCP.IsPrivateKeyReaded())
			{
				EUSignCPOwnGUI.ShowError(
					"Особистий ключ не зчитано");
				return;
			}

			if (checkBoxRawSign.Checked)
			{
				EUSignCPOwnGUI.ShowError("Додавання підпису " +
					"до простого підпису не підтримується");
				return;
			}

			if (textBoxFileToSign.Text == "")
			{
				EUSignCPOwnGUI.ShowError(
					"Файл для підпису не обрано");
				return;
			}

			string fileName, signedFileName;
			bool isAlreadySigned;
			int error;

			fileName = textBoxFileToSign.Text;

			if (checkBoxHashSign.Enabled && checkBoxHashSign.Checked)
			{
				string hash = "";
				string sign = "";
				string signedData;

				signedData = richTextBoxSignedFileData.Text;

				if (signedData == "")
				{
					EUSignCPOwnGUI.ShowError("Підпис відсутній");
					return;
				}

				error = IEUSignCP.IsAlreadySigned(signedData,
					out isAlreadySigned);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				if (isAlreadySigned)
				{
					if (!EUSignCPOwnGUI.ShowConfirm(
							"Файл вже підписан користувачем. Перепідписати?"))
					{
						return;
					}

					int signIndex;
					string newSign;

					error = GetOwnSignIndex(signedData,
						IEUSignCP.EU_CERT_KEY_TYPE_DSTU4145, out signIndex);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return;
					}

					error = IEUSignCP.RemoveSign(signIndex,
						signedData, out newSign);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return;
					}

					signedData = newSign;
				}

				error = HashFile(fileName, ref hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error != IEUSignCP.EU_ERROR_CANCELED_BY_GUI)
						EUSignCPOwnGUI.ShowError(error);
					return;
				}

				error = IEUSignCP.AppendSignHash(hash, signedData, ref sign);

				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				richTextBoxSignedFileData.Text = sign;
			}
			else
			{
				error = IEUSignCP.IsFileAlreadySigned(fileName,
					out isAlreadySigned);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				string tmpFileName = null;
				signedFileName = fileName;
				fileName = fileName.Substring(0,
					fileName.Length - 4);

				tmpFileName = signedFileName +
					(new Random().Next().ToString()) + ".tmp";

				if (isAlreadySigned)
				{
					if (!EUSignCPOwnGUI.ShowConfirm(
							"Файл вже підписан користувачем. Перепідписати?"))
					{
						return;
					}

					int signIndex;

					error = GetFileOwnSignIndex(signedFileName,
						IEUSignCP.EU_CERT_KEY_TYPE_DSTU4145, out signIndex);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return;
					}

					error = IEUSignCP.RemoveSignFile(signIndex,
						signedFileName, tmpFileName);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return;
					}
				}

				error = IEUSignCP.AppendSignFile(fileName,
					isAlreadySigned ? tmpFileName : signedFileName,
					isAlreadySigned ? signedFileName : tmpFileName,
					!checkBoxInternalSign.Checked);

				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (isAlreadySigned)
						File.Delete(tmpFileName);

					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				if (isAlreadySigned)
					File.Delete(tmpFileName);
				else
				{
					File.Delete(signedFileName);
					File.Move(tmpFileName, signedFileName);
				}

				textBoxSignedFile.Text = signedFileName;
			}

			curSignFileIndex = 0;
			buttonVerifyFileNext.Text = "Перевірити наступний...";

			EUSignCPOwnGUI.ShowInfo("Файл успішно підписано");
		}

		private bool CheckSignFileExtension(string fileName, out string fileExtension)
		{
			bool useRawSign = checkBoxRawSign.Enabled && checkBoxRawSign.Checked;

			if (useRawSign)
				fileExtension = ".raw.sig";
			else
				fileExtension = ".p7s";

			string	fileExt = Path.GetExtension(fileName);

			if (useRawSign && fileExt == ".sig")
			{
				string subStr = fileName.Substring(0,
					fileName.Length - 4);

				string fileRawExt = Path.GetExtension(subStr);
				if (fileRawExt != null)
					fileExt = fileRawExt + fileExt;
			}

			if (fileExt == null || fileExt == "" ||
				fileExt != fileExtension)
			{
				return false;
			}

			return true;
		}
		
		private void VerifyFile(object sender, EventArgs e)
		{
			string fileWithSign;
			string fileWithData;
			bool verifyHash = checkBoxHashSign.Enabled && checkBoxHashSign.Checked;
			IEUSignCP.EU_SIGN_INFO signInfo;
			int error;

			fileWithSign = textBoxSignedFile.Text;
			if (fileWithSign == null || fileWithSign == "")
			{
				EUSignCPOwnGUI.ShowError(
					"Файл для перевірки підпису не обрано");
				return;
			}


			if (verifyHash)
			{
				fileWithData = textBoxSignedFile.Text;
			}
			else
			{
				string fileExtension;

				if (!CheckSignFileExtension(fileWithSign, out fileExtension))
				{
					EUSignCPOwnGUI.ShowError(
						"Файл для перевірки підпису має невірний формат (*" +
						fileExtension + ")");
					return;
				}

				fileWithData = fileWithSign.Replace(fileExtension, "");
				if (checkBoxInternalSign.Checked)
				{
					fileExtension = Path.GetExtension(fileWithData);
					if (fileExtension != null && fileExtension != "")
					{
						fileWithData = fileWithData.Replace(fileExtension,
							".new") + fileExtension;
					}
					else
					{
						fileWithData += ".new";
					}
				}
			}

			if (verifyHash)
			{
				string hash, sign;
				
				sign = richTextBoxSignedFileData.Text;
				if (sign == "")
				{
					EUSignCPOwnGUI.ShowError("Підпис відсутній");
					return;
				}

				hash = "";
				error = HashFile(fileWithData, ref hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error != IEUSignCP.EU_ERROR_CANCELED_BY_GUI)
						EUSignCPOwnGUI.ShowError(error);
					return;
				}

				if (checkBoxRawSign.Checked)
				{
					error = IEUSignCP.RawVerifyHash(hash,
						sign, out signInfo);
				}
				else
				{
					error = IEUSignCP.VerifyHash(hash,
						sign, out signInfo);
				}
			}
			else if (checkBoxRawSign.Checked)
			{
				error = IEUSignCP.RawVerifyFile(fileWithSign,
					fileWithData, out signInfo);
			}
			else
			{
				error = IEUSignCP.VerifyFile(fileWithSign,
					fileWithData, out signInfo);
			}

			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);

			IEUSignCP.FreeSignInfo(ref signInfo);
		}

		private void VerifyFileNext(object sender, EventArgs e)
		{
			if (checkBoxRawSign.Checked)
			{
				EUSignCPOwnGUI.ShowError("Додавання підпису " +
					"до простого підпису не підтримується");
				return;
			}

			string fileWithSign;
			string fileWithData;
			bool verifyHash = checkBoxHashSign.Enabled && checkBoxHashSign.Checked;
			IEUSignCP.EU_SIGN_INFO signInfo;
			int signCount;
			int error;

			fileWithSign = textBoxSignedFile.Text;
			if (fileWithSign == null || fileWithSign == "")
			{
				EUSignCPOwnGUI.ShowError(
					"Файл для перевірки підпису не обрано");
				return;
			}

			if (verifyHash)
			{
				fileWithData = textBoxSignedFile.Text;
			}
			else
			{
				string fileExtension;
				if (!CheckSignFileExtension(fileWithSign, out fileExtension))
				{
					EUSignCPOwnGUI.ShowError(
						"Файл для перевірки підпису має невірний формат (*" +
						fileExtension + ")");
					return;
				}

				fileWithData = fileWithSign.Replace(fileExtension, "");
				if (checkBoxInternalSign.Checked)
				{
					fileExtension = Path.GetExtension(fileWithData);
					if (fileExtension != "")
					{
						fileWithData = fileWithData.Replace(fileExtension,
							".new") + fileExtension;
					}
					else
					{
						fileWithData += ".new";
					}
				}
			}

			if (verifyHash)
			{
				string hash, sign;
				
				sign = richTextBoxSignedFileData.Text;
				if (sign == "")
				{
					EUSignCPOwnGUI.ShowError("Підпис відсутній");
					return;
				}
				
				error = IEUSignCP.GetSignsCount(sign, out signCount);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				if (signCount <= 0)
				{
					EUSignCPOwnGUI.ShowError("Підписи відсутні");
					return;
				}

				buttonVerifyFileNext.Text = "Перевірити (" + (curSignFileIndex + 1)
					+ " з " + signCount + ")...";

				hash = "";
				error = HashFile(fileWithData, ref hash);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error != IEUSignCP.EU_ERROR_CANCELED_BY_GUI)
						EUSignCPOwnGUI.ShowError(error);
					return;
				}

				error = IEUSignCP.VerifyHashSpecific(hash,
						curSignFileIndex, sign, out signInfo);
			}
			else
			{
				error = IEUSignCP.GetFileSignsCount(fileWithSign, 
					out signCount);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				if (signCount <= 0)
				{
					EUSignCPOwnGUI.ShowError("Підписи відсутні");
					return;
				}

				buttonVerifyFileNext.Text = "Перевірити (" + (curSignFileIndex + 1)
					+ " з " + signCount + ")...";

				error = IEUSignCP.VerifyFileSpecific(curSignFileIndex, 
					fileWithSign, fileWithData, out signInfo);
			}

			curSignFileIndex = (curSignFileIndex + 1) % signCount;

			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);

			IEUSignCP.FreeSignInfo(ref signInfo);
		}

		private void ShowFileSignerCertificate(object sender, EventArgs e)
		{
			if (checkBoxRawSign.Checked)
			{
				EUSignCPOwnGUI.ShowError("Додавання підпису " +
					"до простого підпису не підтримується");
				return;
			}

			if (checkBoxHashSign.Enabled && checkBoxHashSign.Checked)
			{
				ShowDataSignerCertificate(richTextBoxSignedFileData.Text);
				return;
			}

			if (textBoxSignedFile.Text == "")
			{
				EUSignCPOwnGUI.ShowError("Файл з підписом відсутній");
				return;
			}

			string sign;
			IEUSignCP.EU_CERT_INFO_EX info = new IEUSignCP.EU_CERT_INFO_EX();
			byte[] certificate = new byte[0];
			int error;

			sign = richTextBoxSignedData.Text;

			error = IEUSignCP.GetFileSignerInfo(curSignFileIndex,
				textBoxSignedFile.Text, out info, ref certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			ShowSignerInfo(info, certificate);
		}

		private void ChooseFileToSign(object sender, EventArgs e)
		{
			openFileDialog.Title = "Оберіть файл для підпису";

			if (openFileDialog.ShowDialog(this) != DialogResult.OK)
				return;

			textBoxFileToSign.Text = openFileDialog.FileName;
		}

		private void ChooseFileToVerify(object sender, EventArgs e)
		{
			openFileDialog.Title = "Оберіть файл з підписом";

			if (openFileDialog.ShowDialog(this) != DialogResult.OK)
				return;

			textBoxSignedFile.Text = openFileDialog.FileName;
		}

		private void textBoxSignedFile_TextChanged(
			object sender, EventArgs e)
		{
			curSignFileIndex = 0;
			buttonVerifyFileNext.Text = "Перевірити наступний...";
		}

		private void RunSignDataTest(object sender, EventArgs e)
		{
			if (!IEUSignCP.IsPrivateKeyReaded())
			{
				EUSignCPOwnGUI.ShowError(
					"Особистий ключ не зчитано");
				return;
			}

			IEUSignCP.EU_CERT_INFO_EX certInfo;
			byte[] cert;
			int dataSize = 0x00800000;
			byte[] data;
			byte[] verifiedData;
			byte[] signBinary;
			string signString;
			byte[] signedDataBinary;
			string signedDataString;

			int error;
			int index;
			IEUSignCP.EU_SIGN_INFO signInfo;

			index = 0;

			while ((error = IEUSignCP.EnumOwnCertificates(
						index++, out certInfo)) ==
					IEUSignCP.EU_ERROR_NONE)
			{
				if (certInfo.publicKeyType ==
						IEUSignCP.EU_CERT_KEY_TYPE_DSTU4145 &&
					(certInfo.keyUsageType & IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE) ==
						IEUSignCP.EU_KEY_USAGE_DIGITAL_SIGNATURE)
				{
					break;
				}
			}

			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.GetCertificate(
				certInfo.issuer, certInfo.serial, out cert);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			data = new byte[dataSize];

			error = IEUSignCP.SignData(data, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyData(data, signBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			signBinary = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignData(data, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyData(data, signString, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			signString = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignData(data.ToString(), out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyData(data.ToString(), signBinary,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			signBinary = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignData(data.ToString(), out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyData(data.ToString(), signString, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			signString = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignDataContinue(data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.SignDataEnd(out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyDataBegin(signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			signBinary = null;

			error = IEUSignCP.VerifyDataContinue(data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyDataEnd(out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignDataContinue(data.ToString());
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.SignDataEnd(out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyDataBegin(signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			signString = null;

			error = IEUSignCP.VerifyDataContinue(data.ToString());
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyDataEnd(out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignDataInternal(true, data,
				out signedDataBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyDataInternal(signedDataBinary,
				out verifiedData, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			signedDataBinary = null;

			if (data.Length != verifiedData.Length)
			{
				EUSignCPOwnGUI.ShowError(
					"Виникла помилка при перевірці підпису");
				return;
			}

			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] != verifiedData[i])
				{
					EUSignCPOwnGUI.ShowError(
						"Виникла помилка при перевірці підпису");
					return;
				}
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			verifiedData = null;

			error = IEUSignCP.SignDataInternal(true, data, out signedDataString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.VerifyDataInternal(signedDataString,
				out verifiedData, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			signedDataString = null;

			if (data.Length != verifiedData.Length)
			{
				EUSignCPOwnGUI.ShowError(
					"Виникла помилка при перевірці підпису");
				return;
			}

			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] != verifiedData[i])
				{
					EUSignCPOwnGUI.ShowError(
						"Виникла помилка при перевірці підпису");
					return;
				}
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			verifiedData = null;

			error = IEUSignCP.RawSignData(data, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.RawVerifyData(data, signBinary,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.RawVerifyDataEx(cert,
				data, signBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			signBinary = null;

			error = IEUSignCP.RawSignData(data, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.RawVerifyData(data, signString,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.RawVerifyDataEx(
				cert, data, signString,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			signString = null;

			error = IEUSignCP.RawSignData(data.ToString(),
				out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.RawVerifyData(data.ToString(),
				signBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.RawVerifyDataEx(
				cert, data.ToString(),
				signBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			signBinary = null;

			error = IEUSignCP.RawSignData(data.ToString(),
				out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.RawVerifyData(data.ToString(),
				signString, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.RawVerifyDataEx(
				cert, data.ToString(),
				signString, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			signString = null;

			if (!SignHashTest())
				return;

			if (!SignRemoteTest())
				return;

			if (!SignDataCtxTest())
				return;

			if (!SignDataRSA())
				return;

			if (!VerifyOnTimeTest())
				return;

			if (!VerifyWithParamsTest())
				return;

			EUSignCPOwnGUI.ShowInfo("Тестування завершилося успішно");
		}

		private bool SignHashTest()
		{
			int dataSize = 0x00800000;
			byte[] data;
			byte[] signBinary;
			string signString;

			byte[] hashBinary;
			string hashString;
			IntPtr hashContext;
			byte[] certificate;
			int hashIterations;
			Random random = new Random();

			int error;
			IEUSignCP.EU_SIGN_INFO signInfo;


			data = new byte[dataSize];

			for (int i = 0; i < dataSize; i++)
				data[i] = (byte)random.Next(0, 255);

			error = IEUSignCP.HashData(data, out hashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawSignHash(hashBinary, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			hashBinary = null;

			error = IEUSignCP.RawVerifyData(data, signBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			signBinary = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.HashData(data, out hashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawSignHash(hashBinary, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawVerifyHash(hashBinary, signString,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			signString = null;
			hashBinary = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.HashData(data.ToString(), out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawSignHash(hashString, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawVerifyHash(hashString, signBinary,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			hashString = null;
			signBinary = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.HashData(data.ToString(), out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawSignHash(hashString, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return false;

			error = IEUSignCP.RawVerifyHash(hashString, signString,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			signString = null;
			hashString = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.GetOwnCertificate(out certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.HashDataBeginWithParamsCtx(certificate, out hashContext);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			hashIterations = random.Next(1, 20);
			for (int i = 0; i < hashIterations; i++)
			{
				error = IEUSignCP.HashDataContinueCtx(ref hashContext, certificate);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return false;
				}
			}

			error = IEUSignCP.HashDataEndCtx(hashContext, out hashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawSignHash(hashBinary, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawVerifyHash(hashBinary, signBinary,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			hashContext = IntPtr.Zero;
			hashBinary = null;
			signBinary = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.HashDataBeginWithParamsCtx(certificate, out hashContext);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			for (int i = 0; i < hashIterations; i++)
			{
				error = IEUSignCP.HashDataContinueCtx(ref hashContext, data.ToString());
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return false;
				}
			}

			error = IEUSignCP.HashDataEndCtx(hashContext, out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawSignHash(hashString, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawVerifyHash(hashString, signBinary,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			hashContext = IntPtr.Zero;
			hashString = null;
			signBinary = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			for (int i = 0; i < hashIterations; i++)
			{
				error = IEUSignCP.HashDataContinueCtx(ref hashContext, data.ToString());
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					EUSignCPOwnGUI.ShowError(error);
					return false;
				}
			}

			error = IEUSignCP.HashDataEndCtx(hashContext, out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawSignHash(hashString, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.RawVerifyHash(hashString, signBinary,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			hashContext = IntPtr.Zero;
			hashString = null;
			signBinary = null;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			return true;
		}

		private bool SignRemoteTest()
		{
			int dataSize = 0x00800000;
			byte[] data;
			byte[] signBinary;
			string signString;

			byte[] hashBinary;
			string hashString;
			byte[] certificate;

			byte[] signerBinary;
			string signerString;

			Random random = new Random();

			int error;
			IEUSignCP.EU_SIGN_INFO signInfo;

			data = new byte[dataSize];

			for (int i = 0; i < dataSize; i++)
				data[i] = (byte)random.Next(0, 255);

			error = IEUSignCP.HashData(data, out hashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.HashData(data, out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.GetOwnCertificate(out certificate);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			byte[][] testData = {null, data};
 			byte[][] testCert = {null, certificate};
			byte[] verifyedData;

			for (int i = 0; i < testData.Length; i++)
			{
				byte[] curData = testData[i];
				for (int j = 0; j < testCert.Length; j++)
				{
					byte[] curCert = testCert[j];

					error = IEUSignCP.CreateEmptySign(curData, out signBinary);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return false;
					}

					error = IEUSignCP.CreateSigner(hashBinary, out signerBinary);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return false;
					}

					error = IEUSignCP.AppendSigner(signerBinary, curCert,
						signBinary, out signBinary);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return false;
					}

					if (curData != null)
						error = IEUSignCP.VerifyDataInternal(signBinary, out verifyedData, out signInfo);
					else
						error = IEUSignCP.VerifyHash(hashBinary, signBinary, out signInfo);

					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return false;
					}

					if (EUSignCPOwnGUI.UseOwnUI)
						EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
				}
			}

			for (int i = 0; i < testData.Length; i++)
			{
				byte[] curData = testData[i];
				for (int j = 0; j < testCert.Length; j++)
				{
					byte[] curCert = testCert[j];

					error = IEUSignCP.CreateEmptySign(curData, out signString);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return false;
					}

					error = IEUSignCP.CreateSigner(hashString, out signerString);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return false;
					}

					error = IEUSignCP.AppendSigner(signerString, curCert,
						signString, out signString);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return false;
					}

					if (curData != null)
						error = IEUSignCP.VerifyDataInternal(signString, out verifyedData, out signInfo);
					else
						error = IEUSignCP.VerifyHash(hashString, signString, out signInfo);
					if (error != IEUSignCP.EU_ERROR_NONE)
					{
						EUSignCPOwnGUI.ShowError(error);
						return false;
					}

					if (EUSignCPOwnGUI.UseOwnUI)
						EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
				}
			}

			return true;
		}

		private bool SignDataCtxTest()
		{
			if (!IEUSignCP.IsPrivateKeyReaded())
			{
				EUSignCPOwnGUI.ShowError(
					"Особистий ключ не зчитано");
				return false;
			}

			int dataSize = 0x00800000;
			byte[] data;
			string dataString = "!Data to sign and verify!1234567890";
			byte[] signBinary;
			string signString;
			IntPtr context;

			int error;
			IEUSignCP.EU_SIGN_INFO signInfo;

			data = new byte[dataSize];

			context = IntPtr.Zero;
			error = IEUSignCP.SignDataContinueCtx(ref context, data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataContinueCtx(ref context, data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataEndCtx(context, true, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			context = IntPtr.Zero;
			error = IEUSignCP.VerifyDataBeginCtx(signBinary, out context);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataContinueCtx(context, data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataContinueCtx(context, data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataEndCtx(context, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			context = IntPtr.Zero;

			error = IEUSignCP.SignDataContinueCtx(ref context, dataString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataEndCtx(context, true, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			context = IntPtr.Zero;
			error = IEUSignCP.VerifyDataBeginCtx(signString, out context);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataContinueCtx(context, dataString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataEndCtx(context, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			return true;
		}

		private bool SignDataRSA()
		{
			if (!IEUSignCP.IsPrivateKeyReaded())
			{
				EUSignCPOwnGUI.ShowError(
					"Особистий ключ не зчитано");
				return false;
			}

			IEUSignCP.EU_CERT_INFO_EX info;

			if (!GetOwnSignCertificate(
					IEUSignCP.EU_CERT_KEY_TYPE_RSA, out info))
			{
				return true;
			}

			int dataSize = 0x00800000;
			byte[] data;
			string dataString = "!Data to sign and verify!1234567890";
			byte[] signBinary;
			string signString;
			byte[] verifiedData;
			IntPtr context;

			int error;
			IEUSignCP.EU_SIGN_INFO signInfo;

			data = new byte[dataSize];

			error = IEUSignCP.SignDataRSA(data, true, true, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyData(data, signBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignDataRSA(dataString, true, true, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyData(dataString, signString, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignDataRSA(dataString, true, false, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataInternal(signBinary, out verifiedData, 
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.SignDataRSAContinue(dataString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataRSAEnd(true, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyData(dataString, signString, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			signBinary = null;
			context = IntPtr.Zero;
			error = IEUSignCP.SignDataRSAContinueCtx(ref context, data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataRSAEndCtx(context, true, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			context = IntPtr.Zero;
			error = IEUSignCP.VerifyDataBeginCtx(signBinary, out context);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataContinueCtx(context, data);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataEndCtx(context, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			signBinary = null;
			context = IntPtr.Zero;

			error = IEUSignCP.SignDataRSAContinueCtx(ref context, dataString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataRSAEndCtx(context, true, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			context = IntPtr.Zero;
			error = IEUSignCP.VerifyDataBeginCtx(signString, out context);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataContinueCtx(context, dataString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.VerifyDataEndCtx(context, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			return true;
		}

		private int SignFileTest(string fileName, bool useTSP)
		{
			string fileOutName;
			string tsp;
			int error;

			tsp = useTSP ? ".tsp" : "";

			fileOutName = fileName + tsp + ".raw.sig";
			error = IEUSignCP.RawSignFile(fileName, fileOutName);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			fileOutName = fileName + tsp + ".crt.p7s";
			error = IEUSignCP.SignFile(fileName, fileOutName, true);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			fileOutName = fileName + tsp + ".int.crt.p7s";
			error = IEUSignCP.SignFile(fileName, fileOutName, false);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int VerifyFileTest(string fileName, bool useTSP)
		{
			string fileOutName;
			string tsp;
			IEUSignCP.EU_SIGN_INFO signInfo;
			int error;

			tsp = useTSP ? ".tsp" : "";

			fileOutName = fileName + tsp + ".raw.sig";
			error = IEUSignCP.RawVerifyFile(fileOutName, fileName, 
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;
			IEUSignCP.FreeSignInfo(ref signInfo);

			fileOutName = fileName + tsp + ".crt.p7s";
			error = IEUSignCP.VerifyFile(fileOutName, fileName,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;
			IEUSignCP.FreeSignInfo(ref signInfo);

			fileOutName = fileName + tsp + ".int.crt.p7s";
			error = IEUSignCP.VerifyFile(fileOutName, fileName,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;
			IEUSignCP.FreeSignInfo(ref signInfo);

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int SignFileRSATest(string fileName)
		{
			IEUSignCP.EU_SIGN_INFO signInfo;
			string outFileName;
			int error;

			outFileName = fileName + ".rsa.ext.p7s";
			error = IEUSignCP.SignFileRSA(fileName, outFileName, true);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.VerifyFile(outFileName, fileName,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;
			IEUSignCP.FreeSignInfo(ref signInfo);

			outFileName = fileName + ".rsa.int.p7s";
			error = IEUSignCP.SignFileRSA(fileName, outFileName, false);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.VerifyFile(outFileName, fileName,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;
			IEUSignCP.FreeSignInfo(ref signInfo);

			return IEUSignCP.EU_ERROR_NONE;
		}

		private int VerifyOnTimeFileTest(string fileName)
		{
			string onTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
			bool offline, noCRL;
			IEUSignCP.EU_SIGN_INFO signInfo;
			int error;

			offline = false;
			noCRL = false;

			error = IEUSignCP.SignFile(fileName,
				fileName + ".ext.p7s", true);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.SignFile(fileName,
				fileName + ".int.p7s", false);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			error = IEUSignCP.VerifyFileOnTimeEx(
				0, fileName + ".ext.p7s", fileName,
				onTime, offline, noCRL, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyFileOnTimeEx(
				0, fileName + ".int.p7s", fileName + ".new",
				onTime, offline, noCRL, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return error;

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			return IEUSignCP.EU_ERROR_NONE;
		}

		private void RunSignFileTest(object sender, EventArgs e)
		{
			if (!IEUSignCP.IsPrivateKeyReaded())
			{
				EUSignCPOwnGUI.ShowError(
					"Особистий ключ не зчитано");
				return;
			}

			string port, address;
			bool getStamps;
			string fileToTest;
			int error;

			openFileDialog.Title = "Оберіть файл для тестування";

			if (openFileDialog.ShowDialog(this) != DialogResult.OK)
				return;

			fileToTest = openFileDialog.FileName;

			error = IEUSignCP.GetTSPSettings(out getStamps,
				out address, out port);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.SetTSPSettings(false,
				address, port);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = SignFileTest(fileToTest, false);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.SetTSPSettings(getStamps, address, port);
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = IEUSignCP.SetTSPSettings(true,
				address, port);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.SetTSPSettings(getStamps, address, port);
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = SignFileTest(fileToTest, true);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				IEUSignCP.SetTSPSettings(getStamps, address, port);
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			IEUSignCP.SetTSPSettings(getStamps, address, port);

			error = SignFileRSATest(fileToTest);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			error = VerifyOnTimeFileTest(fileToTest);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return;
			}

			EUSignCPOwnGUI.ShowInfo(
				"Тестування завершилося успішно");
		}

		private bool VerifyOnTimeTest()
		{
			byte[] dataBinary = TestData.GetByteArray();
			string dataString = TestData.GetString();
			byte[] hashBinary;
			string hashString;
			byte[] signHashBinary;
			string signHashString;
			byte[] signBinary;
			string signString;
			byte[] signInternalBinary;
			string signInternalString;
			byte[] verifiedDataBinary;

			string onTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
			bool offline, noCRL;
			IEUSignCP.EU_SIGN_INFO signInfo;
			int error;

			error = IEUSignCP.HashData(dataBinary, out hashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.HashData(dataString, out hashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignHash(hashBinary, out signHashBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignHash(hashString, out signHashString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignData(dataBinary, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignData(dataString, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataInternal(true, dataBinary,
				out signInternalBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataInternal(true, dataString,
				out signInternalString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			offline = false;
			noCRL = false;

			error = IEUSignCP.VerifyHashOnTimeEx(
				hashBinary, 0, signHashBinary, onTime,
				offline, noCRL, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyHashOnTimeEx(
				hashString, 0, signHashString, onTime,
				offline, noCRL, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyDataOnTimeEx(
				dataBinary, 0, signBinary, onTime,
				offline, noCRL, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyDataOnTimeEx(
				dataString, 0, signString, onTime,
				offline, noCRL, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyDataInternalOnTimeEx(0,
				signInternalBinary, onTime, offline, noCRL,
				out verifiedDataBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (!IEUSignCP.CompareArrays(dataBinary, verifiedDataBinary))
			{
				IEUSignCP.FreeSignInfo(ref signInfo);

				EUSignCPOwnGUI.ShowError(IEUSignCP.EU_ERROR_BAD_PARAMETER);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyDataInternalOnTimeEx(0,
				signInternalString, onTime, offline, noCRL,
				out verifiedDataBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			return true;
		}

		private bool VerifyWithParamsTest()
		{
			byte[] dataBinary = TestData.GetByteArray();
			string dataString = TestData.GetString();
			byte[] signBinary;
			string signString;
			byte[] signInternalBinary;
			string signInternalString;
			byte[] verifiedDataBinary;
			byte[] signerCert;
			bool noSignerCertCheck = false;
			IEUSignCP.EU_CERT_INFO_EX info;

			string onTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
			bool offline, noCRL;
			IEUSignCP.EU_SIGN_INFO signInfo;
			int error;

			if (!GetOwnSignCertificate(
					IEUSignCP.EU_CERT_KEY_TYPE_DSTU4145, out info))
			{
				return false;
			}

			error = IEUSignCP.GetCertificate(
				info.issuer, info.serial, out signerCert);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignData(dataBinary, out signBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignData(dataString, out signString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataInternal(false, dataBinary,
				out signInternalBinary);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			error = IEUSignCP.SignDataInternal(false, dataString,
				out signInternalString);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			offline = false;
			noCRL = false;

			error = IEUSignCP.VerifyDataWithParams(
				dataBinary, 0, signBinary, onTime,
				offline, noCRL, signerCert, noSignerCertCheck,
				out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyDataWithParams(
				dataString, 0, signString, onTime,
				offline, noCRL, signerCert,
				noSignerCertCheck, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyDataInternalWithParams(0,
				signInternalBinary, onTime, offline, noCRL,
				signerCert, noSignerCertCheck,
				out verifiedDataBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (!IEUSignCP.CompareArrays(dataBinary, verifiedDataBinary))
			{
				IEUSignCP.FreeSignInfo(ref signInfo);

				EUSignCPOwnGUI.ShowError(IEUSignCP.EU_ERROR_BAD_PARAMETER);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			error = IEUSignCP.VerifyDataInternalWithParams(0,
				signInternalString, onTime, offline, noCRL,
				signerCert, noSignerCertCheck,
				out verifiedDataBinary, out signInfo);
			if (error != IEUSignCP.EU_ERROR_NONE)
			{
				EUSignCPOwnGUI.ShowError(error);
				return false;
			}

			if (EUSignCPOwnGUI.UseOwnUI)
				EUSignCPOwnGUI.ShowSignInfo(ref signInfo);
			else
				IEUSignCP.ShowSignInfo(signInfo);
			IEUSignCP.FreeSignInfo(ref signInfo);

			return true;
		}

		public void RunCtxSignTest(object sender, EventArgs e)
		{
			if (!SignVerificator.PerformTest())
			{
				EUSignCPOwnGUI.ShowError(
					"Тестування підпису завершилося з помилкою");

				return;
			}

			EUSignCPOwnGUI.ShowInfo(
				"Тестування завершилося успішно");
		}

		public SignUsage()
		{
			InitializeComponent();
		}

		public void SetEnabled(bool enabled)
		{
			ChangeControlsState(enabled);

			if (!enabled)
				ClearData(true);
		}

		public void WillShow()
		{
			bool enabled;

			enabled = IEUSignCP.IsInitialized();
			if (!enabled)
				ClearSettings();

			if (enabled && !IEUSignCP.IsPrivateKeyReaded())
				ClearData(false);
			
			ChangeControlsState(enabled);
		}
	}
}
