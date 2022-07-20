using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using EUSignCP;

namespace EUSignTestCS.AdditionalControls
{
	public enum EU_KEY_MEDIA_FORM_TYPE
	{
		SELECT_KM = 1,
		GENERATE_PK = 2,
		CHANGE_PK_PASSWORD = 3
	};

	public partial class KeyMediaView : UserControl
	{
		EU_KEY_MEDIA_FORM_TYPE formType;

		private void UpdateDeviceListClick(object sender,
			EventArgs e)
		{
			TreeNode selectedNode = treeViewKMs.SelectedNode;
			if (selectedNode.Level == 0)
			{
				UpdateDevicesList(selectedNode, true);
				if (selectedNode.Nodes.Count > 0)
					selectedNode.Expand();
			}
		}

		private void treeViewDevices_AfterSelect(object sender,
			TreeViewEventArgs e)
		{
			TreeNode selectedNode = treeViewKMs.SelectedNode;
			IEUSignCP.EU_KEY_MEDIA_DEVICE_INFO info;
			bool enabled;
			int error;

			if (selectedNode.Level == 0)
			{
				labelKMType.Text = selectedNode.Text;
				labelKMName.Text = "";
				enabled = false;
			}
			else if (selectedNode.Level == 1)
			{
				labelKMType.Text = selectedNode.Parent.Text;
				labelKMName.Text = selectedNode.Text;
				error = IEUSignCP.GetKeyMediaDeviceInfo(KeyMedia, out info);
				if (error == IEUSignCP.EU_ERROR_NONE)
					labelKMName.Text = info.deviceNameAlias;

				enabled = true;
			}
			else
				return;

			textBoxPassword.Enabled = enabled;
			labelPassword.Enabled = enabled;
			textBoxPassword.Text = "";

			if (checkBoxFormat.Visible)
			{
				if (enabled)
				{
					bool isHardware;

					error = IEUSignCP.IsHardwareKeyMedia(KeyMedia, out isHardware);
					if (error == IEUSignCP.EU_ERROR_NONE)
						checkBoxFormat.Enabled = isHardware;
					else
						checkBoxFormat.Enabled = false;
				}
				else
					checkBoxFormat.Enabled = false;

				checkBoxFormat_CheckedChanged(null, null);
			}
			else if (panelNewPassword.Visible)
			{
				textBoxNewPassword.Enabled = enabled;
				textBoxNewPassword.Text = "";
				labelNewPassword.Enabled = enabled;
				textBoxConfirmPassword.Enabled = enabled;
				textBoxConfirmPassword.Text = "";
				labelConfirmPassword.Enabled = enabled;
			}
		}

		private void timerKeyBoard_Tick(object sender, EventArgs e)
		{
			timerKeyboard.Enabled = false;

			pictureBoxWarning.Visible = Console.CapsLock;
			labelCapslock.Visible = Console.CapsLock;
			labelLanguage.Text = InputLanguage.CurrentInputLanguage.
				Culture.TwoLetterISOLanguageName.ToUpper(); ;

			timerKeyboard.Enabled = true;
		}

		private void GetDefaultKeyMedia()
		{
			int error;
			IEUSignCP.EU_KEY_MEDIA_SOURCE_TYPE sourceType;
			bool showErrors;
			IEUSignCP.EU_KEY_MEDIA km;

			error = IEUSignCP.GetPrivateKeyMediaSettings(
				out sourceType, out showErrors, out km.typeIndex,
				out km.deviceIndex, out km.password);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return;

			if (sourceType != IEUSignCP.EU_KEY_MEDIA_SOURCE_TYPE.OPERATOR)
				return;

			km.password = "";

			this.KeyMedia = km;
		}

		private void UpdateDevicesList(TreeNode node,
			bool showError = false)
		{
			int error = IEUSignCP.EU_ERROR_NONE;

			int typeIndex = node.Index;
			int deviceIndex = 0;

			node.Nodes.Clear();

			while (true)
			{
				string deviceName;

				error = IEUSignCP.EnumKeyMediaDevices(typeIndex,
					deviceIndex, out deviceName);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error == IEUSignCP.EU_WARNING_END_OF_ENUM)
						return;

					if (showError)
						EUSignCPOwnGUI.ShowError(error);

					return;
				}

				node.Nodes.Add(deviceName);
				deviceIndex++;
			}
		}

		private void UpdateDeviceTypeList()
		{
			int error = IEUSignCP.EU_ERROR_NONE;
			int typeIndex = 0;

			treeViewKMs.Nodes.Clear();

			while (error == IEUSignCP.EU_ERROR_NONE)
			{
				string deviceType;

				error = IEUSignCP.EnumKeyMediaTypes(typeIndex,
					out deviceType);
				if (error != IEUSignCP.EU_ERROR_NONE)
				{
					if (error == IEUSignCP.EU_WARNING_END_OF_ENUM)
						return;

					EUSignCPOwnGUI.ShowError(error);
					return;
				}

				TreeNode node = treeViewKMs.Nodes.Add(deviceType);
				UpdateDevicesList(node);
				typeIndex++;
			}
		}

		public KeyMediaView()
		{
			InitializeComponent();
		}

		public void LoadForm(EU_KEY_MEDIA_FORM_TYPE formType)
		{
			this.formType = formType;

			switch (formType)
			{
				case EU_KEY_MEDIA_FORM_TYPE.GENERATE_PK:
				{
					panelNewPassword.Visible = true;
					checkBoxFormat.Visible = true;
					break;
				}
				case EU_KEY_MEDIA_FORM_TYPE.CHANGE_PK_PASSWORD:
				{
					panelNewPassword.Visible = true;
					checkBoxFormat.Visible = false;
					break;
				}
				case EU_KEY_MEDIA_FORM_TYPE.SELECT_KM:
				default:
				{
					panelNewPassword.Visible = false;
					break;
				}
			}
			
			UpdateDeviceTypeList();
			GetDefaultKeyMedia();
			timerKeyboard.Enabled = true;
		}

		public bool ValidateData()
		{
			if (!IsKMSelected)
			{
				EUSignCPOwnGUI.ShowError(
					"Не вказано носій ключової інформації");
				return false;
			}

			if (KeyMedia.password == "" && !FormatDevice)
			{
				EUSignCPOwnGUI.ShowError(
					"Не вказано пароль до носія ключової інформації");
				return false;
			}

			bool checkNewPassword = true;
			if (formType == EU_KEY_MEDIA_FORM_TYPE.GENERATE_PK &&
				!FormatDevice)
				checkNewPassword = false;

			switch (formType)
			{
				case EU_KEY_MEDIA_FORM_TYPE.GENERATE_PK:
				case EU_KEY_MEDIA_FORM_TYPE.CHANGE_PK_PASSWORD:
				{
					if (NewPassword == "" && checkNewPassword)
					{
						EUSignCPOwnGUI.ShowError(
							"Не вказано новий пароль до носія ключової інформації");
						return false;
					}

					if (NewPassword != ConfirmPassword)
					{
						EUSignCPOwnGUI.ShowError(
							"Новий пароль та його повтор не співпадають");
						return false;
					}
					break;
				}
				case EU_KEY_MEDIA_FORM_TYPE.SELECT_KM:
				default:
				{
					break;
				}
			}

			return true;
		}

		public void SaveKeyMediaToSettings()
		{
			int error;
			IEUSignCP.EU_KEY_MEDIA_SOURCE_TYPE sourceType;
			bool showErrors;
			IEUSignCP.EU_KEY_MEDIA km;

			error = IEUSignCP.GetPrivateKeyMediaSettings(
				out sourceType, out showErrors, out km.typeIndex,
				out km.deviceIndex, out km.password);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return;

			if (sourceType != IEUSignCP.EU_KEY_MEDIA_SOURCE_TYPE.OPERATOR)
				return;

			km = this.KeyMedia;
			km.password = "";

			error = IEUSignCP.SetPrivateKeyMediaSettings(
				sourceType, showErrors, km.typeIndex,
				km.deviceIndex, km.password);
			if (error != IEUSignCP.EU_ERROR_NONE)
				return;
		}

		public Boolean IsKMSelected
		{
			get
			{
				TreeNode selectedNode = treeViewKMs.SelectedNode;

				if (selectedNode.Level != 1)
					return false;

				return true;
			}
		}

		public IEUSignCP.EU_KEY_MEDIA KeyMedia
		{
			get
			{
				string password;

				if (FormatDevice)
					password = textBoxNewPassword.Text;
				else
					password = textBoxPassword.Text;

				return new IEUSignCP.EU_KEY_MEDIA(
					treeViewKMs.SelectedNode.Parent.Index,
					treeViewKMs.SelectedNode.Index,
					password);
			}

			set
			{
				TreeNode deviceType;

				if (value.typeIndex >= treeViewKMs.Nodes.Count)
					return;
				
				deviceType = treeViewKMs.Nodes[value.typeIndex];
				if (value.deviceIndex >= deviceType.Nodes.Count)
					return;

				treeViewKMs.SelectedNode = 
					deviceType.Nodes[value.deviceIndex];
				textBoxPassword.Text = value.password;
			}
		}

		public string NewPassword
		{
			get
			{
				return textBoxNewPassword.Text;
			}
		}

		public string ConfirmPassword
		{
			get
			{
				return textBoxConfirmPassword.Text;
			}
		}

		public Boolean FormatDevice
		{
			get
			{
				return checkBoxFormat.Enabled &&
					checkBoxFormat.Checked;
			}
		}

		private void checkBoxFormat_CheckedChanged(
			object sender, EventArgs e)
		{
			bool enable = FormatDevice;

			textBoxPassword.Enabled = !enable;
			labelPassword.Enabled = !enable;

			textBoxNewPassword.Enabled = enable;
			textBoxNewPassword.Text = "";
			labelNewPassword.Enabled = enable;
			textBoxConfirmPassword.Enabled = enable;
			textBoxConfirmPassword.Text = "";
			labelConfirmPassword.Enabled = enable;
		}
	}
}
