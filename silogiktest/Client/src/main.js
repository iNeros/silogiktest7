import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import axios from "axios";
import vuetify from "./plugins/vuetify";
import "material-icons/iconfont/material-icons.css";

import util from "./utils/utils.vue.js";

const DEFAULT_LANG = "esMx";

let language = util.getCookie("appLang");
if (!language) {
  util.setCookie("appLang", DEFAULT_LANG);
  language = DEFAULT_LANG;
}

Vue.prototype.$appLang = language;

new Vue({
  vuetify,
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
