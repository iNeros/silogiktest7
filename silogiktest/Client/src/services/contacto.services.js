import Store from "@/store/index.js";
import axios from "axios";

const domain = Store.state.domain;

export default {
  async GetViewLabels(lang) {
    const url = `${domain.domainNameForApi}Contact/GetViewLabels`;

    var formData = new FormData();
    formData.append("lang", lang);

    try {
      let response = await axios.post(url, formData);
      return response;
    } catch (error) {
      console.error("ex", error);
      throw error;
    }
  },

  async GetViewRulesMessages(lang) {
    const url = `${domain.domainNameForApi}Contact/GetViewRulesMessages`;

    var formData = new FormData();
    formData.append("lang", lang);

    try {
      let response = await axios.post(url, formData);
      return response;
    } catch (error) {
      console.error("ex", error);
      throw error;
    }
  },

  async SendContactForm(formdata) {
    const url = `${domain.domainNameForApi}Contact/SendContactForm`;
    try {
      let response = await axios.post(url, formdata);
      return response;
    } catch (error) {
      console.error("ex", error);
      throw error;
    }
  },
};
