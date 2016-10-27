const path = require('path')
const NoErrorsPlugin = require('webpack/lib/NoErrorsPlugin')
const DefinePlugin = require('webpack/lib/DefinePlugin')
const CommonsChunkPlugin = require('webpack/lib/optimize/CommonsChunkPlugin')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const ExtractTextPlugin = require('extract-text-webpack-plugin')
const CopyWebpackPlugin = require('copy-webpack-plugin')
const ENV = process.env.ENV = process.env.NODE_ENV = 'development'

const metadata = {
  baseUrl: '/',
  https: false,
  port: 8000,
  ENV
}

module.exports = {
  metadata,
  devtool: 'eval',
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
      {
        test: /\.ts$/,
        loader: 'ts',
        query: {
          ignoreDiagnostics: [
            2304,
            2403, // 2403 -> Subsequent variable declarations
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
        loader: ExtractTextPlugin.extract('css')
      },
      { test: /\.(png|gif|jpg|svg|ttf|woff|woff2|eot)$/, loader: 'file?name=assets/[hash].[ext]' },
      { test: /\.html$/, loader: 'raw' }
    ]
  },

  plugins: [
    new ExtractTextPlugin('style.css', { allChunks: true }),
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
    })
  ],
  devServer: {
    port: metadata.port,
    host: metadata.host,
    https: metadata.https,
    historyApiFallback: true,
    watchOptions: { aggregateTimeout: 300, poll: 1000 }
  },
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
