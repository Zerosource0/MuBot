<template>
  <v-container class="pt-0">
    <v-layout class="flex z-0 justify-center pin-t pin-l h-screen w-screen">
      <div class="text-center self-center">{{profileInfo}}</div>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import SpotifyApiController from "@/core/spotify/SpotifyApiController";

@Component({
  name: "c-main"
})
export default class Main extends Vue {
  profileInfo: any = {};

  async created() {
    await SpotifyApiController.getProfileInfo().then(info => {
      console.log(info);
      this.profileInfo = JSON.parse(info.data);
    });
  }
}
</script>
