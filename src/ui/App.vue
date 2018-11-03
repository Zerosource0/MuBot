<template>
  <v-app dark>
    <v-toolbar dark color="primary" app>
      <v-toolbar-title class="headline text-uppercase">
        <span>Vertica</span>
        <span class="font-weight-light">Pim</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
  </v-toolbar> 

    <v-layout row wrap>
      <v-flex xs12 class="mt-5">
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
