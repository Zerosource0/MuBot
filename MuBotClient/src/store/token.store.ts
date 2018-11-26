import { ActionTree, GetterTree, MutationTree } from "vuex";
import { Action, Getter, Mutation } from "vuex-class";
import { bindToNamespace } from "./binding-helper";
import SpotifyAuthController, { IToken, TokenStoreKey } from "../core/spotify/SpotifyAuthController";
import { isEmpty } from "lodash";

export interface ITokenState {
  accessToken: IToken;
}

export class TokenState implements ITokenState {
  public accessToken: IToken;

  constructor() {
    this.accessToken = {} as any;
  }
}

export const getters: GetterTree<ITokenState, any> = {
  accessToken(state) { return isEmpty(state.accessToken) ? undefined : state.accessToken; },
};

export const mutations: MutationTree<ITokenState> = {
  setAccessToken(state, accessToken) {
    localStorage.setItem(TokenStoreKey, JSON.stringify(accessToken));
    state.accessToken = accessToken;
  },
};

export const actions: ActionTree<ITokenState, any> = {
  async authorize() {
    await SpotifyAuthController.Authorize();
  },

  async setAccessToken({ commit }, accessToken) {
    commit("setAccessToken", accessToken);
  },

  async createAccessToken({ commit }, payload: { code: any, state: any }) {
    await SpotifyAuthController.GetToken(payload.code, payload.state);
  },
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
