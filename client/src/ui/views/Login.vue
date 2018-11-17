<template>
  <v-container class="pt-0">
    <v-layout class="flex z-0 justify-center pin-t pin-l h-screen w-screen">
      <v-btn large color="primary" class="self-center" @click="connect">Connect</v-btn>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import router from "../router/router";
import Component from "vue-class-component";
import { TokenAction, TokenGetter } from "../../store/token.store";
import { IToken } from "@/core/spotify/SpotifyAuthController";

@Component({
  name: "c-login"
})
export default class Login extends Vue {
  @TokenAction
  authorize!: () => {};
  @TokenGetter
  accessToken!: IToken;

  created() {
    if (this.accessToken) {
      router.push({
        name: "c-main",
        query: {
          status: "success"
        }
      });
    }
  }

  async connect() {
    await this.authorize();
  }
}
</script>

