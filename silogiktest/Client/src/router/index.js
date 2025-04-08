import Vue from "vue";
import axios from "axios";
import Store from "@/store/index.js";
import VueRouter from "vue-router";
import ContactoView from "@/views/ContactoView.vue";

Vue.use(VueRouter);

const domain = Store.state.domain;

const routes = [
  {
    path: `${domain.domainName}/`,
    name: "ContactoView",
    component: ContactoView,
    meta: { title: "Formulario Contacto" },
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
