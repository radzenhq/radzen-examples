const path = require('path')
const ProvidePlugin = require('webpack/lib/ProvidePlugin')
const NoErrorsPlugin = require('webpack/lib/NoErrorsPlugin')
const DefinePlugin = require('webpack/lib/DefinePlugin')
const CommonsChunkPlugin = require('webpack/lib/optimize/CommonsChunkPlugin')
const OccurenceOrderPlugin = require('webpack/lib/optimize/OccurenceOrderPlugin')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const DedupePlugin = require('webpack/lib/optimize/DedupePlugin')
const ExtractTextPlugin = require('extract-text-webpack-plugin')
const UglifyJsPlugin = require('webpack/lib/optimize/UglifyJsPlugin')
const CopyWebpackPlugin = require('copy-webpack-plugin')
const ENV = process.env.ENV = process.env.NODE_ENV = 'production'

const metadata = {
  baseUrl: '/',
  ENV
}

module.exports = {
  metadata,
  devtool: 'source-map',
  debug: true,

  entry: {
    'vendor': './src/app/vendor.ts',
    'main': './src/app/main.ts'
  },

  // Config for our build files
  output: {
    path: 'dist',
    filename: '[name].bundle.js',
    sourceMapFilename: '[name].map',
    chunkFilename: '[id].chunk.js'
  },

  resolve: {
    // ensure loader extensions match
    extensions: ['', '.ts', '.js', '.json', '.css', '.html']
  },

  module: {
    loaders: [
      // Support for .ts files.
      {
        test: /\.ts$/,
        loader: 'ts',
        query: {
          compilerOptions: {
            removeComments: true,
            noEmitHelpers: true
          },
          ignoreDiagnostics: [
            2304,
            2307,
            2661,
            2339,
            2403, // 2403 -> Subsequent constiable declarations
            2300, // 2300 -> Duplicate identifier
            2374, // 2374 -> Duplicate number index signature
            2375  // 2375 -> Duplicate string index signature
          ]
        },
        exclude: [ /\.(spec|e2e)\.ts$/ ]
      },
      { test: /\.json$/, loader: 'json' },
      {
        test: /\.css$/,
        loader: ExtractTextPlugin.extract('css?minimize')
      },
      { test: /\.(png|gif|jpg|svg|ttf|woff|woff2|eot)$/, loader: 'file?name=assets/[hash].[ext]' },
      { test: /\.html$/, loader: 'raw' }
    ]
  },

  plugins: [
    new ExtractTextPlugin('style.css', { allChunks: true }),
    new DedupePlugin(),
    new OccurenceOrderPlugin(true),
    new NoErrorsPlugin(),
    new CommonsChunkPlugin({ name: 'vendor', filename: 'vendor.bundle.js', minChunks: Infinity }),
    // static assets
    new CopyWebpackPlugin([ { from: path.join(__dirname, 'src', 'assets', 'img', path.basename('assets/img/logo.png')), to: 'assets/img/logo.png' } ]),
    // generating html
    new HtmlWebpackPlugin({ template: 'src/app/index.html' }),
    // replace
    new DefinePlugin({
      'process.env': {
        'ENV': JSON.stringify(metadata.ENV),
        'NODE_ENV': JSON.stringify(metadata.ENV)
      }
    }),
    new ProvidePlugin({
      // TypeScript helpers
      '__metadata': 'ts-helper/metadata',
      '__decorate': 'ts-helper/decorate',
      '__awaiter': 'ts-helper/awaiter',
      '__extends': 'ts-helper/extends',
      '__param': 'ts-helper/param',
      'Reflect': 'es7-reflect-metadata/dist/browser'
    }),
    new UglifyJsPlugin({
      comments: false,
      compress: {
        'screw_ie8': true
      },
      mangle: {
        'screw_ie8': true
      }
    })
  ],

  // we need this due to problems with es6-shim
  node: {
    global: 'window',
    progress: false,
    crypto: 'empty',
    module: false,
    clearImmediate: false,
    setImmediate: false
  }
}
