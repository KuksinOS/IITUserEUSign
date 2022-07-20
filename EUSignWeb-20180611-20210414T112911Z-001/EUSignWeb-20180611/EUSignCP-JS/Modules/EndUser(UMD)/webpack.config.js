//===============================================================================

const EUSCP_WORKER_DIR = './src/ExtLibrary/euscp';
const MODULES_DIR = 'Modules';
const BUILD_DIR = MODULES_DIR + '/build';

//===============================================================================

const { join } = require('path')
const fs = require('fs');
const webpack = require('webpack');
const npm = require('npm');

//===============================================================================

function mkdir(path) {
  path.split('/')
  .reduce((currentPath, folder) => {
    currentPath += folder + '/';
    if (!fs.existsSync(currentPath)){
      fs.mkdirSync(currentPath);
    }
    return currentPath;
  }, '');
}

function cp(src, dst) {
  fs.writeFileSync(dst, fs.readFileSync(src));
}

function rm(path) {
  if (!fs.existsSync(path))
    return;

  if (!fs.lstatSync(path).isDirectory()) {
    fs.unlinkSync(path);
    return;
  }
   
  fs.readdirSync(path).forEach(function (file, index) {
      rm(join(path, file));
  });

  fs.rmdirSync(path);
};

function mv(src, dst) {
  cp(src, dst);
  rm(src);
}

//===============================================================================

function makeLibName(name, withWorker, mode, target) {
  if (!withWorker)
    name += '.noWorker';

  mode = ({
    'none': 'debug', 
    'production': '', 
    'development': 'dev'
  }[mode]) || '';
  if (mode)
    name += '.' + mode;

  return name + '.js';
}

//===============================================================================

function importWorkerScripts(srcFile, dstFile) {
  var src = fs.readFileSync(join(__dirname, srcFile), 'utf-8');

  var importPattern = 'importScripts(';
  var start = src.indexOf(importPattern);
  if (!start)
    throw 'Invalid file ' + srcFile;
  
  var end = start;
  while (end < src.length) {
    if (src[end] == ')')
      break;
    end++;
  }
  if ((end == start) || (end >= src.length))
    throw 'Invalid file ' + srcFile;

  var dst = src.substring(0, start);
  var scripts = src.substring(
    start + importPattern.length, end);

  scripts = scripts.split(',')
  scripts.forEach(function(path) {
    path = path.trim();
    path = path.substring(1, path.length - 1);
    dst += '//' + path + '\r\n';
    dst += fs.readFileSync(path);
  });
  dst += src.substring(end + 1);

  fs.writeFileSync(join(__dirname, dstFile), dst);
}

//===============================================================================

function createConfig(withWorker, mode, target) {
  var outFileName = makeLibName('[name]', withWorker, mode, target);
 
  return {
    mode: mode,
    devtool: (mode == 'development' ? 'cheap-source-map' : ''),

    resolveLoader: {
      modules: ["node_modules"],
      extensions: [ '.js', '.ts' ],
    },
    resolve: {
        modules: [join(__dirname, "src"), "node_modules"],
        extensions: ['.js', '.ts']
    },  

    entry: {
      '/lib/euscp': join(__dirname, 'src/EndUser'),
    },
      
    output: {
      path: join(__dirname, BUILD_DIR),
      filename: outFileName,
      libraryTarget: target,
      umdNamedDefine: true,
      globalObject: "this",
      devtoolModuleFilenameTemplate: '[absolute-resource-path]'
    },
    plugins: [
      new webpack.ProvidePlugin({
        Promise: ['es6-promise', 'Promise']
      }),
      new WorkerImportScriptsPlugin(),
      new CreateBundlePlugin()
    ],
    
    module: {
      rules: [
        {
          test: /\.ts$/,
          use: ['ts-loader']
        },
        {
          test: /\euscp.worker.js$/,
          loader: ['raw-loader']
        }
      ]
    }
  }
}

//===============================================================================

function WorkerImportScriptsPlugin() {
  WorkerImportScriptsPlugin.prototype.apply = function (compiler) {
    compiler.hooks.beforeRun.tap('beforeRun', function() {
      /**
       * Preprocess worker file to import worker scripts
       */
      mkdir(BUILD_DIR + '/lib');
      importWorkerScripts(
        join(EUSCP_WORKER_DIR, 'euscp.worker.src.js'), 
        join(BUILD_DIR + '/lib', 'euscp.worker.js'));
    });  
  }
};

//===============================================================================

function CreateBundlePlugin(){}
  CreateBundlePlugin.prototype.apply = function (compiler) {
    compiler.hooks.afterEmit.tap('afterEmit', function() {
      var buildDir = compiler.options.output.path;
      var packageConfPath = join(__dirname, "package.json.src");
      var packageConf = JSON.parse(fs.readFileSync(packageConfPath, 'utf8'));
      var tarName = packageConf.name + '-' + packageConf.version + '.tgz';
      var tmp = join(buildDir, '/tmp');

      var files = [
        {name: "package.json", path: packageConfPath},
        {name: "euscp.js", path: join(buildDir, "/lib/euscp.js")},
        {name: "euscp.d.ts", path: join(buildDir, "/src/EndUser.d.ts")},
        {name: "EndUserError.d.ts", path: join(buildDir, "/src/EndUserError.d.ts")},
        {name: "EndUserOwnerInfo.d.ts", path: join(buildDir, "/src/EndUserOwnerInfo.d.ts")},
        {name: "EndUserSettings.d.ts", path: join(buildDir, "/src/EndUserSettings.d.ts")}
      ];

      mkdir(tmp);
      files.forEach(function(file) {
        cp(file.path, join(tmp, file.name));
      });

      npm.load({parseable:false}, function(er, npm) {
        npm.commands.pack(
          [tmp], 
          {
            destRelativeToPackage: true 
          }, 
          function() {
            rm(buildDir);
            mv(join(__dirname, tarName), join(MODULES_DIR, tarName));
          });
      });
    });
};

//===============================================================================

/**
 * Before build run npm install --save-dev to install build toolchain
 */
module.exports = [
  //createConfig(true, 'none', 'umd'),
  createConfig(true, 'production', 'umd')
];

//===============================================================================
