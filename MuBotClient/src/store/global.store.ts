import Vue from "vue";
import Vuex, {ActionTree, GetterTree, MutationTree, Store} from "vuex";
import {Action, Getter, Mutation} from "vuex-class";

Vue.use(Vuex);

interface IGlobalState {
}

class GlobalState implements IGlobalState {
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
    }
});

export default store;
export const GlobalGetter = Getter;
export const GlobalMutation = Mutation;
export const GlobalAction = Action;
