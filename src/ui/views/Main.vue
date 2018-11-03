<template>
  <v-container mt5>
    <v-layout text-xs-center wrap>
      <v-flex xs12>
        <v-flex mb-4>
            <h1 class="display-2 font-weight-bold mb-3">
             Welcome to VERTICA<span class="font-weight-light">\PIM</span>
            </h1>
          </v-flex>
        </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component({
  name: "c-main"
})
export default class HelloWorld extends Vue {}
</script>