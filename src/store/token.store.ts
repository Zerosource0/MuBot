import { ActionTree, GetterTree, MutationTree } from "vuex";
import { Action, Getter, Mutation } from "vuex-class";
import { bindToNamespace } from "./binding-helper";
import * as oauth2 from "simple-oauth2";
import SpotifyApiController from "../core/spotify/SpotifyApiController";

export interface ITokenState {
  accessToken: oauth2.AccessToken;
}

export class TokenState implements ITokenState {
  public accessToken: oauth2.AccessToken;

  constructor() {
    this.accessToken = {} as any;
  }
}

export const getters: GetterTree<ITokenState, any> = {
  accessToken: state => state.accessToken,
  expired: state => state.accessToken.expired
};

export const mutations: MutationTree<ITokenState> = {
  setAccessToken(state, accessToken) {
    state.accessToken = accessToken;
  },
  async refreshAccessToken(state) {
    state.accessToken = await state.accessToken.refresh();
  }
};

export const actions: ActionTree<ITokenState, any> = {
  async getAccessToken({ commit }) {
    const token = await SpotifyApiController.Authorize();
    if (token) {
      commit("setAccessToken", token);
      return true;
    }
    return false;
  },

  async refreshToken({ commit }) {
    await commit("refreshAccessToken");
  }
};

export default {
  namespaced: true,
  state: new TokenState(),
  getters,
  mutations,
  actions
};

const namespace = "token";
export const TokenGetter = bindToNamespace(namespace)(Getter);
export const TokenMutation = bindToNamespace(namespace)(Mutation);
export const TokenAction = bindToNamespace(namespace)(Action);
