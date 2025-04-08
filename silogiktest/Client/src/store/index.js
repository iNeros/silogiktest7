import Vue from "vue";
import Vuex from "vuex";
import constantes from "../constants";

Vue.use(Vuex);
export default new Vuex.Store({
  state: {
    drawer: false,
    theme: false,
    domain: {
      domainName: constantes.domainName,
      domainNameWithHttp: constantes.domainNameWithHttp,
      domainNameForApi: constantes.domainNameForApi,
    },

    maxLengthComentarios: 400,
  },
  mutations: {},
  actions: {},
  getters: {},
});
