const Path = require( 'path' );
const Autoprefixer = require( 'autoprefixer-stylus' );
const HtmlWebpackPlugin = require( 'html-webpack-plugin' );
const Webpack = require( 'webpack' );

const plugins = [
  new HtmlWebpackPlugin({
    filename: '../index.html',
    template: 'index.template.html',
    inject: 'body'
  }),
  new Webpack.ProvidePlugin({
    'window.jQuery': 'jquery',
    'window.$': 'jquery',
    $: 'jquery'
})
];

const prod = process.argv[ 4 ] === '--production';

if (prod) {
  plugins.push( new Webpack.optimize.UglifyJsPlugin({
    compress: {
      warnings: false
    }
  }));
}

plugins.push( new Webpack.DefinePlugin({
  __PRODUCTION__: prod,
  __DEV__: !prod
}));

const config = {
  entry: {
    app: [ 'babel-polyfill', './client/js/app' ]
  },
  output: {
    path: './build',
    filename: '[name]-[hash]-bundle.js',
  },
  devtool: 'source-map',
  module: {
    loaders: [{
      test: /\.js$/,
     include: [
        Path.resolve( process.cwd(), 'client/js' )
      ],
      loader: 'babel',
      query: {
        presets: [ 'es2015' ]
      }
      },
      {
        test: /\.styl$/,
        include: [
          Path.resolve( process.cwd(), 'client/style/stylus' )
        ],
        loader: 'style!css!stylus'
      },
      {
        test: /\.(png|jpg)$/,
        include: [
          Path.resolve( process.cwd(), 'client/style' )
        ],
        loader: 'file',
        query: {
          name: '[hash].[ext]'
        }
      }]
  },
  resolve: {
    extension: [ '', '.js' ],
    modulesDirectories: [ 'node_modules' ]
  },
  stylus: {
    use: [ Autoprefixer({
      browser: [ 'last 2 versions' ]
    })]
  },
  devServer: {
    historyApiFallback: true,
    contentBase       : './'
  },
  plugins: plugins
};

module.exports = config;