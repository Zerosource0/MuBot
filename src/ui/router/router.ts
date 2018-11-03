import Vue from "vue";
import Router from "vue-router";
import HelloWorld from "../views/HelloWorld.vue";
import Main from "../views/Main.vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "index",
      redirect: {
        name: "c-main",
      }
    },
    {
      path: "/Main",
      name: "c-main",
      component: Main,
    },
  ],
});
