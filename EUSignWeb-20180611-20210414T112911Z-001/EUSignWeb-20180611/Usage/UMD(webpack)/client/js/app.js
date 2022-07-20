//================================================================================

const view = require( 'raw!jade-html?pretty=true!../views/index.jade' );
document.body.innerHTML = view;

require( '../style/stylus/main.styl' );
require( './eusw.umd.js' );
require('jquery')

import * as IEUSignCP from './eusw.umd';

//================================================================================

let g_euSign = null;
let g_caAddress = "ca.iit.com.ua";
let g_caPort = "80";
let g_isNeedReadNewKey = false;

//================================================================================

var eu_wait = function(first) { 
	return new (function() { 
		var self = this;
		var callback = function() {
			var args;
			if(self.deferred.length) {
				args = [].slice.call(arguments); 
				args.unshift(callback); 
				self.deferred[0].apply(self, args); 
				self.deferred.shift(); 
			}
		}
		this.deferred = [];
		this.eu_wait = function(run) {
			this.deferred.push(run); 
			return self; 
		}
		first(callback);
	});
};

//--------------------------------------------------------------------------------

function log(msg) {
	console.log(msg);
}

//================================================================================

function loadCryptoLibrary(onSuccess, onError) {
	log("Load library...");

	var loader = new IEUSignCP.EndUserLibraryLoader(
		IEUSignCP.EndUserLibraryLoader.LIBRARY_TYPE_DEFAULT, "euSign");

	loader.onload = function(library) {
		log("Libary loaded");

		g_euSign = library;
		onSuccess();
	};
	loader.onerror = function(msg, errorCode, libraryOrNull){
		log("Libary load failed. Error - " + msg);

		onError();
	};

	loader.load();
}

//--------------------------------------------------------------------------------

function initializeCryptoLibrary(onSuccess, onError) {
	log("Initialize library...");

	var _onError = function(e) {
		log("Initialize library failed. Error - " + e);

		onError(e);
	};

	eu_wait(function(runNext) {
		g_euSign.SetUIMode(false, runNext, _onError);
	}).eu_wait(function(runNext) {
		g_euSign.Initialize(runNext, _onError);
	}).eu_wait(function(runNext) {
		g_euSign.SetUIMode(false, runNext, _onError);
	}).eu_wait(function(runNext) {
		log("Library initialized");

		onSuccess();
	});
}

//--------------------------------------------------------------------------------

function setSettings(onSuccess, onError) {
	log("Set library settings...");

	var _onError = function(e) {
		log("Initialize library failed. Error - " + e);

		onError(e);
	};

	eu_wait(function(runNext) {
		g_euSign.SetRuntimeParameter(
			g_euSign.EU_SAVE_SETTINGS_PARAMETER,
			g_euSign.EU_SETTINGS_ID_NONE,
			runNext, onError);
	}).eu_wait(function(runNext) {
		var settings = g_euSign.CreateFileStoreSettings();
		settings.SetPath("");
		settings.SetAutoRefresh(true);
		settings.SetSaveLoadedCerts(true);
		settings.SetExpireTime(3600);
		settings.SetCheckCRLs(false);
		settings.SetOwnCRLsOnly(false);
		settings.SetAutoDownloadCRLs(false);
		settings.SetFullAndDeltaCRLs(false);

		g_euSign.SetFileStoreSettings(
			settings, runNext, _onError);
	}).eu_wait(function(runNext) {
		var settings = g_euSign.CreateProxySettings();
		settings.SetUseProxy(false);
		settings.SetAddress("");
		settings.SetPort("");
		settings.SetAnonymous(true);
		settings.SetUser("");
		settings.SetSavePassword(false);
		settings.SetPassword("");

		g_euSign.SetProxySettings(
			settings, runNext, _onError);
	}).eu_wait(function(runNext) {
		var settings = g_euSign.CreateTSPSettings();
		settings.SetGetStamps(true);
		settings.SetAddress(g_caAddress);
		settings.SetPort(g_caPort);

		g_euSign.SetTSPSettings(
			settings, runNext, _onError);
	}).eu_wait(function(runNext) {
		var settings = g_euSign.CreateOCSPSettings();
		settings.SetUseOCSP(true);
		settings.SetAddress(g_caAddress);
		settings.SetPort(g_caPort);
		settings.SetBeforeStore(true);

		g_euSign.SetOCSPSettings(
			settings, runNext, _onError);
	}).eu_wait(function(runNext) {
		var settings = g_euSign.CreateLDAPSettings();

		g_euSign.SetLDAPSettings(
			settings, runNext, _onError);
	}).eu_wait(function(runNext) {
		var settings = g_euSign.CreateCMPSettings();
		settings.SetUseCMP(true);
		settings.SetAddress(g_caAddress);
		settings.SetPort(g_caPort);
		settings.SetCommonName("");

		g_euSign.SetCMPSettings(
			settings, runNext, _onError);
	}).eu_wait(function(runNext) {
		var settings = g_euSign.CreateModeSettings();
		settings.SetOfflineMode(false);
		g_euSign.SetModeSettings(settings,
			runNext, _onError);
	}).eu_wait(function(runNext) {
		log("Settings set");

		onSuccess();
	});
}

//--------------------------------------------------------------------------------

function getKMTypes(onSuccess, onError) {
	log("Get key media types");

	var kmTypes = new Array();

	var _getKeyMediaType = function (index) {
		g_euSign.EnumKeyMediaTypes(index,
			function (type) {
				if (type == null || type == '') {
					log("Get key media types. Types - " + kmTypes);

					onSuccess(kmTypes);
					return;
				}

				kmTypes.push(type);
				_getKeyMediaType(index + 1);
			},
			function (e) {
				log("Get key media types failed. Error - " + e);

				onError(e);
			});
	}

	_getKeyMediaType(0);
}

//--------------------------------------------------------------------------------

function getKMDevices(typeIndex, onSuccess, onError) {
	log("Get key media devices");

	var kmDevices = new Array();

	var _getKeyMediaDevice = function (typeIndex, deviceIndex) {
		g_euSign.EnumKeyMediaDevices(typeIndex, deviceIndex,
			function (device) {
				if (device == null || device == '') {
					log("Get key media devices. Devices - " + kmDevices);
					
					onSuccess(kmDevices);
					return;
				}

				kmDevices.push(device);
				_getKeyMediaDevice(typeIndex, deviceIndex + 1);
			},
			function (e) {
				log("Get key media devices failed. Error - " + e);
				
				onError(e);
			});
	}

	_getKeyMediaDevice(typeIndex, 0);
}

//================================================================================

function updateKMTypes(onSuccess, onError) {
	log("Update key media types...");

	var _onError = function(e) {
		log("Update key media types failed. Error - " + e);

		onError(e);
	};

	eu_wait(function(runNext) {
		getKMTypes(runNext, _onError);
	}).eu_wait(function(runNext, kmTypes) {
		$('#kmTypes').empty();
		
		if (kmTypes.length != 0) {
			$.each(kmTypes, function (index, value) {
				$('#kmTypes').append(
					$('<option/>', { 
						value: value,
						text : value 
					}));
			});
			
			$('#kmTypes').val(kmTypes[0]);
		}

		getKMDevices(0, runNext, _onError);
	}).eu_wait(function(runNext, kmDevices) {
		$('#kmDevices').empty();

		if (kmDevices.length != 0) {
			$.each(kmDevices, function (index, value) {
				$('#kmDevices').append(
					$('<option/>', { 
						value: value,
						text : value 
					}));
			});

			$('#kmDevices').val(kmDevices[0]);
		}

		log("Key media types updated");

		onSuccess();
	});
}

//--------------------------------------------------------------------------------

function updateKMDevices() {
	log("Update key media devices...");

	var _onError = function(e) {
		log("Update key media devices failed. Error - " + e);

		var error = "Виникла помилка при оновленні списку носіїв ключової інформації" +
			(e ? (". " + e) : "");

		alert(error);
	};

	eu_wait(function(runNext) {
		getKMDevices($('#kmTypes').prop('selectedIndex'), 
			runNext, _onError);
	}).eu_wait(function(runNext, kmDevices) {
		$('#kmDevices').empty();

		if (kmDevices.length != 0) {
			$.each(kmDevices, function (index, value) {
				$('#kmDevices').append(
					$('<option/>', { 
						value: value,
						text : value 
					}));
			});

			$('#kmDevices').val(kmDevices[0]);
		}

		log("Key media devices updated");
	});
}

//--------------------------------------------------------------------------------

function onLoad() {
	var _onError = function(e) {
		var error = "Виникла помилка при завантаженні бібліотеки" + 
			(e ? (". " + e) : "");

		alert(error);
	};
	
	eu_wait(function(runNext) {
		loadCryptoLibrary(runNext, _onError);
	}).eu_wait(function(runNext) {
		initializeCryptoLibrary(runNext, _onError);
	}).eu_wait(function(runNext) {
		setSettings(runNext, _onError);
	}).eu_wait(function(runNext) {
		updateKMTypes(runNext, _onError);
	}).eu_wait(function(runNext) {
		$("#kmTypes").prop("disabled", false);
		$("#kmDevices").prop("disabled", false);
		$("#kmPassword").prop("disabled", false);
		$("#useInternalSign").prop("disabled", false);
		$("#selectFile").prop("disabled", false);
		$("#selectSignFile").prop("disabled", false);
		$("#signFile").prop("disabled", false);
	});
}

//--------------------------------------------------------------------------------

function onSignFile() {
	var kmType = $('#kmTypes').prop('selectedIndex');
	var kmDevice = $('#kmDevices').prop('selectedIndex');
	var password = $('#kmPassword').val();
	var fileToSign = $('#fileToSign').val();
	var fileSign = $('#fileSign').val();
	var externalSign = !$('#useInternalSign').is(':checked');

	if (kmType < 0  || kmDevice < 0) {
		alert("Не обрано носій з особистим ключем");
		return;
	}

	if (!password || password == "") {
		alert("Не вказано пароль доступу до носія з особистим ключем");
		return;
	}

	if (!fileToSign || fileToSign == "") {
		alert("Не вказано шлях до файлу для піпдису");
		return;
	}

	if (!fileSign || fileSign == "") {
		alert("Не вказано шлях до файлу з піпдисом");
		return;
	}

	var _onError = function(e) {
		var error = "Виникла помилка при підписі файлу" + 
			(e ? (". " + e) : "");

		alert(error);
	};

	eu_wait(function(runNext) {
		if (g_isNeedReadNewKey) {
			setTimeout(function(){
				runNext();
			}, 1);
			return;
		}

		g_euSign.ResetPrivateKey(runNext, _onError);
	}).eu_wait(function(runNext) {
		g_isNeedReadNewKey = false;
		g_euSign.ReadPrivateKeySilently(
			parseInt(kmType), parseInt(kmDevice), 
			password, runNext, _onError);
	}).eu_wait(function(runNext) {
		g_euSign.SignFile(fileToSign, fileSign, 
			externalSign, runNext, _onError);
	}).eu_wait(function(runNext) {
		alert("Файл успішно підписано");
	});
}

//================================================================================

window.addEventListener('load', function () {
	onLoad();

	$('#kmTypes').on('change', function(e) {
		e.preventDefault();
		updateKMDevices();
		
		g_isNeedReadNewKey = true;
	});
	$('#kmDevices').on('change', function(e) {
		e.preventDefault();

		g_isNeedReadNewKey = true;
	});
	$('#selectFile').on('click', function() {
		g_euSign.SelectFile(true, '', 
			function(fileName) {
				$('#fileToSign').val(fileName || '');
				if (fileName && $('#fileSign').val() == '')
					$('#fileSign').val(fileName + ".p7s");
			}, 
			function(e) {
				$('#fileToSign').val('');
			});
	});
	$('#selectSignFile').on('click', function() {
		g_euSign.SelectFile(true, '', 
			function(fileName) {
				$('#fileSign').val(fileName || '');
			}, 
			function(e) {
				$('#fileToSign').val('');
			});
	});
	$('#signFile').on('click', function() {
		onSignFile();
	});
});

//================================================================================
