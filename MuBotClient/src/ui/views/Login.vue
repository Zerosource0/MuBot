<template>
  <v-container class="pt-0">
    <v-layout class="flex z-0 justify-center pin-t pin-l h-screen w-screen">
      <v-fade-transition>
        <v-form v-if="!loading && !offline" class="self-center w-72">
          <div
            v-if="!loading && !offline && loginFailed"
            class="text-center self-center text-red-light text-sm truncate"
          >{{failedMsg}}</div>
          <v-text-field
            class
            v-validate="'required'"
            data-vv-name="username"
            v-model="username"
            label="Username"
            :error-messages="errors.collect('username')"
            required
          ></v-text-field>
          <v-text-field
            v-validate="'required'"
            data-vv-name="password"
            v-model="password"
            type="password"
            label="Password"
            :error-messages="errors.collect('password')"
            required
          ></v-text-field>
          <v-btn class="w-full mx-0" @click="submit">Login</v-btn>
        </v-form>
      </v-fade-transition>
      <v-fade-transition>
        <v-progress-circular
          v-if="loading"
          color="primary"
          :size="60"
          indeterminate
          class="text-center self-center"
        ></v-progress-circular>
      </v-fade-transition>
      <v-fade-transition>
        <h1 class="text-5x1 text-uppercase text-center self-center">{{errorMsg}}</h1>
      </v-fade-transition>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import router from "../router/router";
import Component from "vue-class-component";
import { Validator } from "vee-validate";
import LoginController from "@/core/login/LoginController";

@Component({
  name: "c-login-login",
  $_veeValidate: { validator: "new" }
})
export default class Login extends Vue {
  loading: boolean = true;
  offline: boolean = true;
  loginFailed: boolean = true;
  failedMsg: string = "";
  errorMsg: string = "";

  username: string = "";
  password: string = "";

  dictionary: any = {
    custom: {
      username: {
        required: () => "Please enter your username"
      },
      passowrd: {
        required: "Please enter your password"
      }
    }
  };

  async mounted() {
    Validator.localize("en", this.dictionary);
    await LoginController.ping().then(result => {
      if (result) {
        this.offline = false;
      } else {
        this.errorMsg = "Mubot is currently offline.";
      }
    });

    if (this.offline) {
      this.loading = false;
      return;
    }

    await LoginController.verifyToken().then(result => {
        if (result) {
          router.push({
            name: "c-spotify-login"
          });
        }
      }).finally(() => {
        this.loading = false;
      });
  }

  async submit() {
    await this.$validator.validateAll().then(isValid => {
      if (isValid) {
        this.loginFailed = false;
        this.loading = true;
        this.login();
      }
    });
  }

  async login() {
    await LoginController.login(this.username, this.password).then(result => {
      if (result === "success") {
        this.loading = false;
        router.push({
          name: "c-spotify-login"
        });
      } else {
        this.failedMsg = result as string;
        this.loginFailed = true;
        this.loading = false;
      }
    });
  }
}
</script>
<style lang="scss" scoped>
.w-72 {
  width: 20rem;
}
</style>
