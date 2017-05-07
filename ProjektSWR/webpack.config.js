const path = require("path");

module.exports = {
  entry: __dirname + '/Scripts/Messages/controller',
  output: {
    path: __dirname + '/dist',
    filename: 'messages-bundle.js'
  },
  module: {
    //loaders: []
    rules: [
      {test: /\.ts$/, use: "ts-loader"},
      {test: /\.css$/, loader: 'style-loader!css-loader'}
    ]
  },
  resolve: {
    extensions: ["*", ".webpack.js", ".web.js", ".js", ".json", ".ts"] 
  }
}