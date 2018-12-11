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
import SpotifyAuthController from "@/core/spotify/SpotifyAuthController";

@Component({
  name: "c-spotify-login"
})
export default class SpotifyLogin extends Vue {
  async mounted() {
    await SpotifyAuthController.HasToken().then(result => {
      if (!result) {
        return;
      }
      router.push({
        name: "c-main",
        query: {
          status: "success"
        }
      });
    });
  }

  connect() {
    SpotifyAuthController.Authorize();
  }
}
</script>

