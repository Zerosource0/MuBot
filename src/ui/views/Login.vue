<template>
  <v-container mt5 fill-height="">
    <v-layout text-xs-center wrap>
      <v-flex xs12 align-content-center>
          <v-flex>
            <v-btn large color="primary" @click="connect">Connect</v-btn>
          </v-flex>
        </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { TokenAction } from "../../store/token.store";

@Component({
  name: "c-login"
})
export default class Login extends Vue {
  @TokenAction public getAccessToken!: () => Promise<boolean>;

  async connect() {
    const result = await this.getAccessToken();
    console.log("getAccessToken", result);
  }
}
</script>

<style lang="scss" scoped>
.darkened {
  color: #404040;
}
</style>
