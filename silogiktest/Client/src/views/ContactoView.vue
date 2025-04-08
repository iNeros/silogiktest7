<template>
  <v-container>
    <!-- <v-row class="mt-10">
      <v-col cols="12">
        <p class="cv-title-1 text-center">{{ viewLabels.title }}</p>
      </v-col>
    </v-row> -->
    <v-form ref="contactForm">
      <v-card color="transparent" flat :disabled="loading">
        <v-row class="justify-center">
          <v-col cols="12" md="8" v-if="loading">
            <v-progress-linear color="#77CBB9" stream reverse indeterminate></v-progress-linear>
          </v-col>
          <v-col cols="12" md="8" v-for="(item, index) in viewLabelsItems" :key="index">
            <div v-if="item.type == 'title'">
              <p class="cv-title-1 my-8 text-center">{{ item.labelText }}</p>
            </div>

            <div class="item-container" v-if="item.type == 'textfield'">
              <v-text-field dark outlined :rules="getRules(item)" v-model="viewLabelsItems[index].value"
                :label="item.labelText" required></v-text-field>
            </div>

            <div v-if="item.type == 'textarea'">
              <v-textarea dark outlined rows="4" auto-grow :rules="getRules(item)"
                v-model="viewLabelsItems[index].value" :label="item.labelText" required></v-textarea>
            </div>

            <div v-if="item.type == 'fileinput'">
              <v-file-input dark outlined :rules="getRules(item)" v-model="viewLabelsItems[index].value"
                :label="item.labelText" accept="image/*,application/pdf"></v-file-input>
            </div>
          </v-col>

        </v-row>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="#CDD3D5" @click="clearForm()"> {{ viewLabelsButtons.btnClear }} </v-btn>
          <v-btn color="#77CBB9" @click="validateSend()"> {{ viewLabelsButtons.btnSend }} </v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-form>

    <v-dialog v-model="responseDialog" persistent max-width="290">
      <v-card class="pt-5">
        <v-card-text>
          <p class="text-center">
            <v-icon size="50" :color="dialogData.succes ? 'green' : 'red'">
              {{ dialogData.succes ? 'mdi-check-circle' : 'mdi-alert-circle' }}
            </v-icon>
          </p>
          <p class="text-center message-text">
            {{ dialogData.message }}
          </p>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="#77CBB9" text @click="responseDialog = false">
            {{ viewLabelsButtons.btnOk }}!
          </v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog v-model="confirmationDialog" persistent max-width="290">
      <v-card class="pt-5">
        <v-card-text>
          <p class="text-center message-text">
            {{ rulesMessages.confirmation }}
          </p>
        </v-card-text>
        <v-card-actions>
          <v-btn color="#CDD3D5" text @click="confirmationDialog = false">
            <v-icon> mdi-arrow-left </v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn color="#77CBB9" text @click="sendComments()">
            {{ viewLabelsButtons.btnOk }}!
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>


  </v-container>
</template>

<script>
import { mapState } from "vuex";
import contactServices from "@/services/contacto.services.js";

export default {
  title() {
    return this.$route.meta.title
  },
  data() {
    return {
      loading: false,

      viewLabelsButtons: {
        btnClear: "Limpar",
        btnSend: "Enviar",

        btnOk: "Ok",
      },

      viewLabelsItems: [],

      form: {
        email: '',
        nombres: '',
        apellidos: '',
        comentarios: '',
        soporte: null
      },

      confirmationDialog: false,

      rulesMessages: {
        required: "Campo requerido",
        minLen: "Mínimo # caracteres",
        maxLen: "Máximo # caracteres",
        regexRules: "Formato inválido",

        confirmation: "Se enviarán los datos ingresados"
      },

      responseDialog: false,

      dialogData: {
        succes: false,
        message: "",
      },
    };
  },
  mounted() {
    this.getViewLabels();
    this.getViewRulesMessages();
  },
  methods: {
    async getViewLabels() {
      let response = await contactServices.GetViewLabels(this.$appLang);
      if (response.status == 200) {
        let data = response.data;
        this.viewLabelsItems = data;
        data.forEach((item) => {
          switch (item.label) {
            case "contactBtnClear":
              this.viewLabelsButtons.btnClear = item.labelText;
              break;
            case "contactBtnSend":
              this.viewLabelsButtons.btnSend = item.labelText;
              break;
            case "contacBtnOk":
              this.viewLabelsButtons.btnOk = item.labelText;
              break;
          }
        });
      } else {
        //TODO: ERROR HANDLING
      }
    },

    async getViewRulesMessages() {
      let response = await contactServices.GetViewRulesMessages(this.$appLang);
      if (response.status == 200) {
        let data = response.data;
        data.forEach((item) => {
          switch (item.label) {
            case "required":
              this.rulesMessages.required = item.message;
              break;
            case "minimun":
              this.rulesMessages.minLen = item.message;
              break;
            case "maximun":
              this.rulesMessages.maxLen = item.message;
              break;
            case "invalid":
              this.rulesMessages.regexRules = item.message;
              break;
            case "confirmation":
              this.rulesMessages.confirmation = item.message;
              break;
          }
        });
      } else {
        //TODO: ERROR HANDLING
      }
    },

    clearForm() {
      this.viewLabelsItems.forEach((item) => {
        if (item.type == 'textfield' || item.type == 'textarea') {
          item.value = '';
        } else if (item.type == 'fileinput') {
          item.value = null;
        }
      });

      this.$refs.contactForm.resetValidation();
    },

    async validateSend() {
      this.$refs.contactForm.validate();
      let isValid = this.$refs.contactForm.validate();
      if (isValid) {
        this.confirmationDialog = true;
      } else {
        this.confirmationDialog = false;
      }
    },

    async sendComments() {
      this.confirmationDialog = false
      this.loading = true;
      let formData = new FormData();
      this.viewLabelsItems.forEach((item) => {
        if (item.type == 'textfield' || item.type == 'textarea') {
          formData.append(item.label, item.value);
        } else if (item.type == 'fileinput') {
          //TODO: FORMAT FILE
          formData.append(item.label, item.value);
        }
      });
      formData.append("lang", this.$appLang);

      let response = await contactServices.SendContactForm(formData);

      this.dialogData.succes = response.data.success;
      this.dialogData.message = response.data.message;

      this.loading = false;
      this.responseDialog = true;

      if (response.data.success) {
        this.clearForm();
      }
    },

    getRules(item) {
      const rules = [];
      if (item.isRequired) {
        rules.push(v => !!v || this.rulesMessages.required);
      }
      if (item.minLen) {
        rules.push(v => (v && v.length >= item.minLen) || this.rulesMessages.minLen.replace('#', item.minLen));
      }
      if (item.maxLen) {
        rules.push(v => (v && v.length <= item.maxLen) || this.rulesMessages.maxLen.replace('#', item.maxLen));
      }
      if (item.regexRules) {
        const regex = new RegExp(item.regexRules);
        rules.push(v => (!v || regex.test(v)) || this.rulesMessages.regexRules);
      }
      return rules;
    }
  },

};
</script>

<style scoped>
.cv-title-1 {
  font-family: 'Roboto', sans-serif;
  font-size: 2rem;
  font-weight: bold;
  color: #77CBB9;
  text-align: left;
  margin-bottom: 20px;
}

.item-container {
  height: auto;

}

.message-text {
  font-family: 'Roboto', sans-serif;
  font-size: 1rem;
  font-weight: bold;
  margin-bottom: 5px;
}
</style>
