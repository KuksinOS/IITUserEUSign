//=============================================================================

/*
	Змінна, що реалізує інтерфейс JavaScript обгортки бібліотеки 
	"ІІТ Користувач ЦСК Підпис (web)" типу EUSignCP
*/
var euSign = null;

//=============================================================================

/*
	Завантаження та ініціалізації бібліотеки
*/
function initialize() {  
	try { 
		/*
			Тип бібліотеки яка буде завантажена. 
			Якщо обрано тип за замовчанням, в залежності від ОС та браузера 
			буде виконано спробу завантаження бібліотеки в наступній послідовності
			Агент підпису, web-розширення, Java-апплет. Якщо завантаження не відбудеться
			буде повернено помилку.
	
			Можливі значення:
				- EndUserLibraryLoader.LIBRARY_TYPE_DEFAULT			- за замовчанням
				- EndUserLibraryLoader.LIBRARY_TYPE_JAVA_APPLET		- Java-апплет
				- EndUserLibraryLoader.LIBRARY_TYPE_SIGN_AGENT		- Агент підпису
				- EndUserLibraryLoader.LIBRARY_TYPE_WEB_EXTENSION	- web-розширення
		*/
		var libType = EndUserLibraryLoader.LIBRARY_TYPE_DEFAULT;
		/*
			Ідентифікатор об'єкту з інтерфейсом EUSignCP, який буде додано в DOM-дерево
		*/
		var objId = "euSign";
		/*
			Мова, повідомлення про помилку
			
			Можливі значенні:
				- EndUserLibraryLoader.EU_DEFAULT_LANG				- за замовчанням (українська)
				- EndUserLibraryLoader.EU_UA_LANG					- українська
				- EndUserLibraryLoader.EU_RU_LANG					- російська
				- EndUserLibraryLoader.EU_EN_LANG					- англійска
		*/
		var langCode = EndUserLibraryLoader.EU_DEFAULT_LANG;

		var loader = new EndUserLibraryLoader(libType, objId, langCode); 
		/*
			Функція, яка буде викликана у разі успіху
				- library - об'єкту з інтерфейсом EUSignCP 
		*/
		loader.onload = function(library) {
			console.log("Library loaded");

			euSign = library;

			euSign.Initialize(
				function() {
					console.log("Library initialized");
				}, 
				function(e) {
					console.log("Library not initialized: " + 
						e.toString());

					alert('Виникла помилка при ' +
						'ініціалізації криптографічної бібліотеки. ' + 
						'Опис помилки: ' + e.toString());
				});
		};
		/*
			Функція, яка буде викликана у разі помилки
				- msg - розгорнуте повідомлення про помилку з посиланнями
						на пакет з інсталяцією/оновленням бібліотеки для 
						браузера та ОС.
		*/
		loader.onerror = function(e){ 
			console.log("Library not loaded: " + e.toString());

			alert('Виникла помилка при ' +
				'завантаженні криптографічної бібліотеки. ' + 
				'Опис помилки: ' + e.toString());
		}; 

		/*
			Виклик функції завантаження бібліотеки
		*/
		loader.load(); 
	} catch (ex) { 
		console.error(ex); 
	} 
}

//=============================================================================

function enumKeys() {  
	try {
		var keysTextArea = document.getElementById("keys-textarea");
		keysTextArea.value = "";

		var _onError = function(error) {
			console.error(error);
		};

		var _enumKMDevices = function(typeIndex, devIndex, onResult) {
			euSign.EnumKeyMediaDevices(typeIndex, devIndex, 
				function(devName) {
					if (devName == null || devName == "") {
						onResult();
						return;
					}

					keysTextArea.value += "    " + devIndex + 
						". " + devName + "\n";
					
					_enumKMDevices(typeIndex, devIndex + 1, onResult);
				}, 
				_onError);
		};
		
		var _enumKMTypes = function(typeIndex, onResult) {
			euSign.EnumKeyMediaTypes(typeIndex, 
				function(typeName) {
					if (typeName == null || typeName == "") {
						onResult();
						return;
					}

					keysTextArea.value += typeIndex + 
						". " + typeName + "\n";
					
					_enumKMDevices(typeIndex, 0, 
						function() {
							_enumKMTypes(typeIndex + 1, onResult);
						});
				}, 
				_onError);
		}

		_enumKMTypes(0, function() {
			alert("Keys enum end");
		});
	} catch (ex) { 
		console.error(ex); 
	} 
}

//=============================================================================

function selectFileRead() {
	document.getElementById('select-file-read-textfield').value = '';
	
	euSign.SelectFileEx(true, "Key-6.dat", null, null, 
		"Оберіть файл з ключем", "Обрання файла з ключем", 
		function(fileName) {
			document.getElementById('select-file-read-textfield').value = fileName;
		},
		function(e) {
			console.log("SelectFileEx(read): " + e);
		});
}

function selectFileSave() {
	document.getElementById('select-file-save-textfield').value = '';
	
	euSign.SelectFileEx(false, "Key-6.dat", null, null, 
		"Оберіть файл з ключем", "Обрання файла з ключем", 
		function(fileName) {
			document.getElementById('select-file-save-textfield').value = fileName;
		},
		function(e) {
			console.log("SelectFileEx(save): " + e);
		});
}

function selectFolder() {
	document.getElementById('select-folder-textfield').value = '';
	
	euSign.SelectFolderEx("", "Оберіть директорію", "Обрання директорії", 
		function(fileName) {
			document.getElementById('select-folder-textfield').value = fileName;
		},
		function(e) {
			console.log("SelectFolderEx: " + e);
		});
}

//=============================================================================

/* 
	Реєстрація обробників подій
*/
document.addEventListener('DOMContentLoaded', function () {
	document.getElementById('init-button').
		addEventListener('click', initialize);
	document.getElementById('enum-keys-button').
		addEventListener('click', enumKeys);

	document.getElementById('select-file-read-button').
		addEventListener('click', selectFileRead);
	document.getElementById('select-file-save-button').
		addEventListener('click', selectFileSave);
	document.getElementById('select-folder-button').
		addEventListener('click', selectFolder);
});

//=============================================================================
