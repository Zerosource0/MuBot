import Vue from "vue";
import Vuex, {ActionTree, GetterTree, MutationTree, Store} from "vuex";
import token, {ITokenState} from "./token.store";
import {Action, Getter, Mutation} from "vuex-class";

Vue.use(Vuex);

interface IGlobalState {
    tokenState: ITokenState;
}

class GlobalState implements IGlobalState {
    public tokenState!: ITokenState;
}

export const globalGetters: GetterTree<IGlobalState, any> = {};
export const mutations: MutationTree<IGlobalState> = {};
export const actions: ActionTree<IGlobalState, any> = {};

const store = new Store<GlobalState>({
    state: new GlobalState(),
    getters: globalGetters,
    mutations,
    actions,
    modules: {
        token
    }
});

export default store;
export const GlobalGetter = Getter;
export const GlobalMutation = Mutation;
export const GlobalAction = Action;