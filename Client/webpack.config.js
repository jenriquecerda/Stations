const path = require("path");

module.exports = {
  entry: {
    site: "./src/js/site.js",
  },
  output: {
    filename: "[name].bundle.js",
    path: path.resolve(__dirname, "..", "wwwroot", "dist"),
  },
  devtool: "source-map",
  mode: "development",
  module: {
    rules: [
      {
        test: /\.css$/,
        use: ["style-loader", "css-loader"],
      },
    ],
  },
};
