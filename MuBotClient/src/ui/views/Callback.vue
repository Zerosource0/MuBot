<template>
  <v-container mt5 fill-height>
    <v-layout text-xs-center wrap>
      <v-flex xs12 align-content-center>
        <v-flex>
          <h1 class="text-5x1 text-uppercase">Callback!</h1>
        </v-flex>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { RawLocation } from "vue-router";
import router from "../router/router";
import SpotifyAuthController from "@/core/spotify/SpotifyAuthController";

@Component({
  name: "c-callback"
})
export default class HelloWorld extends Vue {
  async mounted() {
    await SpotifyAuthController.HasToken().then(result => {
      if (!result) {
        return;
      }
    });

    const code = this.$route.query["code"] as string;
    const state = this.$route.query["state"] as string;

    if (!code || !state) {
      router.push({
        name: "c-login",
        query: {
          status: "failed"
        }
      });
      return;
    }

    await SpotifyAuthController.CreateToken(code, state).then(() => {
      router.push({
        name: "c-main",
        query: {
          status: "success"
        }
      });
    });
  }
}
</script>

<style lang="scss" scoped>
.darkened {
  color: #404040;
}
</style>
