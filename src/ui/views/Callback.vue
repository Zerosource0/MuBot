<template>
  <v-container mt5 fill-height>
    <v-layout text-xs-center wrap="">
      <v-flex xs12 align-content-center>
        <v-flex>
          <h1 class="display-2 text-5x1 text-uppercase">Callback!</h1>
        </v-flex>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component({
  name: "c-callback"
})
export default class HelloWorld extends Vue {}
</script>

<style lang="scss" scoped>
.darkened {
  color: #404040;
}
</style>
