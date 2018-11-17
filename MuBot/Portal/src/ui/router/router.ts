import Vue from "vue";
import Router from "vue-router";
import Login from "../views/Login.vue";
import Main from "../views/Main.vue";
import Callback from "../views/Callback.vue";

Vue.use(Router);

export default new Router({
  mode: "history",
  routes: [
    {
      path: "/",
      name: "index",
      redirect: {
        name: "c-login",
      }
    },
    {
      path: "/login/",
      name: "c-login",
      component: Login,
    },
    {
      path: "/main",
      name: "c-main",
      component: Main,
    },
    {
      path: "/callback/",
      name: "c-callback",
      component: Callback,
    },
  ],
});
