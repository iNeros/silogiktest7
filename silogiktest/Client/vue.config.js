const path = require("path");

function resolve(dir) {
  return path.join(__dirname, dir);
}

module.exports = {
  transpileDependencies: ["vuetify"],
  publicPath: "./",
  lintOnSave: false,
  outputDir: "../wwwroot/dir",
  filenameHashing: false,

  configureWebpack: {
    output: {
      hotUpdateChunkFilename: "../trash/delete/",
      hotUpdateMainFilename: "../trash/delete/",
    },
  },
  chainWebpack: (config) => {
    config.resolve.alias
      .set("@", resolve("src"))
      .set("assets", resolve("src/assets"))
      .set("css", resolve("src/assets/template/css"));
  },
};
