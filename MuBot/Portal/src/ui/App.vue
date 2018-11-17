<template>
  <v-app dark>
    <v-toolbar dark color="primary" app>
      <v-toolbar-title class="headline text-uppercase">
        <span class="font-weight-light">Mu</span>
        <span>Bot</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>
    <div class="flex z-0 justify-center pin-t pin-l h-full w-full">
      <div class="self-center">
        <h1 class="logo-text text-center text-grey-darkest tracking-tight">
          <span class="font-thin">MU</span>
          <span class="font-light">BOT</span>
        </h1>
        <div
          v-if="connected"
          class="text-center self-center text-grey-darkest text-xl"
        >..is now connected to Spotify</div>
      </div>
    </div>
    <v-container class="fixed z-5 p-0 m-0 pin-t pin-l h-screen w-screen max-w-full">
      <transition name="fade" mode="out-in">
        <router-view/>
      </transition>
    </v-container>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { RawLocation } from "vue-router";
import bus from "../core/bus/Bus.vue";
import { TokenAction, TokenGetter } from "../store/token.store";
import { IToken } from "@/core/spotify/SpotifyAuthController";

export default class App extends Vue {
  @TokenGetter
  accessToken!: IToken;

  connected():boolean
  {
    return this.accessToken !== null || this.accessToken !== undefined;
  }
}
</script>

<style lang="scss" scoped>
.h-half {
  height: 50%;
}
.logo-text {
  font-size: 20vh;
  font-size: 20vw;
}
</style>
