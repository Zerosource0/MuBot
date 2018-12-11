import Vue from "vue";
import "./plugins/vuetify";
import App from "./ui/App.vue";
import router from "./ui/router/router";
import store from "./store/global.store";
import "roboto-fontface/css/roboto/roboto-fontface.css";
import "material-design-icons-iconfont/dist/material-design-icons.css";
import "./assets/tailwind.css";
import "./core/axios/axios.config";
import VeeValidate from "vee-validate";

Vue.config.productionTip = false;
Vue.use(VeeValidate, { inject: false });

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
