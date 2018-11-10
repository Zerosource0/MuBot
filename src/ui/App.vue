<template>
  <v-app dark>
    <v-toolbar dark color="primary" app>
      <v-toolbar-title class="headline text-uppercase">
        <span class="font-weight-light">Mu</span>
        <span>Bot</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>

    <v-layout align-center justify-center class="relative mx-auto h-full">

      <v-flex class="pin z-0">
        <h1 class="display-2 text-purple">
          <span class="font-weight-light">MU</span>BOT
        </h1>
      </v-flex>

      <v-flex class="pin z-5">
        <v-container>
          <transition name="fade" mode="out-in">
            <router-view/>
          </transition>
        </v-container>
      </v-flex>

    </v-layout>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { RawLocation } from "vue-router";
import bus from "../core/bus/Bus.vue";

export default class App extends Vue {}
</script>

<style lang="scss" scoped>
.darkened {
  color: #404040;
}
</style>
