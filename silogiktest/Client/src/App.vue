<template>
    <v-app>
        <v-app-bar app color="#77CBB9" dark>
            <v-toolbar-title>Silogik - Test7</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-menu offset-y color="black">
                <template v-slot:activator="{ on, attrs }">
                    <v-btn dark v-bind="attrs" v-on="on">
                        <v-img max-height="18" max-width="18" :src="getFlag(dafultLang.img)"></v-img>
                        <span class="ml-1">{{ dafultLang.title }} </span>
                    </v-btn>
                </template>
                <v-list-item-group v-model="selectedItem" dark color="#77CBB9" class="menubg">
                    <v-list-item v-for="(item, i) in items" :key="i" @click="setLang(item.value)">
                        <v-list-item-icon>
                            <v-img max-height="20" max-width="20" :src="getFlag(item.img)"></v-img>
                        </v-list-item-icon>
                        <v-list-item-content>
                            <v-list-item-title> {{ item.title }} </v-list-item-title>
                        </v-list-item-content>
                    </v-list-item>
                </v-list-item-group>
            </v-menu>
        </v-app-bar>
        <v-main class="main-content">
            <router-view></router-view>
        </v-main>
        <v-footer app color="#77CBB9" dark>
            <v-row>
                <v-col cols="12">
                    <p class="white--text footer-text"> {{ footerText }} <v-icon>mdi-heart</v-icon></p>
                </v-col>
            </v-row>

        </v-footer>
    </v-app>
</template>

<script>
import { mapState } from "vuex";
import util from "./utils/utils.vue.js";


export default {
    data() {
        return {
            lang: this.$store.state.lang,
            footerText: "Made with love by @iNeros",
            items: [
                { title: 'MX', img: "mx.png", value: 'esMx' },
                { title: 'US', img: "us.jpg", value: 'enUs' },
                { title: 'BR', img: "br.jpg", value: 'ptBr' },
            ],
            selectedItem: 0,

            dafultLang: {
                title: 'MX', img: "mx.png"
            },
        };
    },
    mounted() {
        this.setDafaultLang();
    },
    methods: {
        setDafaultLang() {
            switch (this.$appLang) {
                case "esMx":
                    this.selectedItem = 0;
                    this.dafultLang.title = "MX";
                    this.dafultLang.img = "mx.png";
                    break;
                case "enUs":
                    this.selectedItem = 1;
                    this.dafultLang.title = "US";
                    this.dafultLang.img = "us.jpg";
                    break;
                case "ptBr":
                    this.selectedItem = 2;
                    this.dafultLang.title = "BR";
                    this.dafultLang.img = "br.jpg";
                    break;
                default:
                    this.selectedItem = 0;
                    this.dafultLang.title = "MX";
                    this.dafultLang.img = "mx.png";
            }
        },

        getFlag(name) {
            return require(`@/assets/lang/${name.split('-')[0]}`)
        },

        setLang(lang) {
            util.setCookie("appLang", lang);
            window.location.reload();
        },
    },

};
</script>

<style scoped>
.footer-text {
    font-family: 'Roboto', sans-serif;
    font-size: 1rem;
    font-weight: bold;
    text-align: center;
    margin: 0;
}

.main-content {
    background-color: #212121;
}

.menubg {
    background-color: #212121;
}
</style>