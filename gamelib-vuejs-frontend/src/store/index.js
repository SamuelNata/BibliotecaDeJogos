import Vue from 'vue';
import Vuex from 'vuex';
import createPersistedState from "vuex-persistedstate";
import api from '../api/gamelib.api';

Vue.use(Vuex);

export default new Vuex.Store({
    plugins: [createPersistedState({
        storage: window.sessionStorage,
    })],
    state: {
        token: null,
        userId: null,
        nickname: null
    },
    mutations: {
        setToken(state, payload) {
            state.token = payload;
        },
        setUserInfo(state, payload) {
            state.userId = payload.userId;
            state.nickname = payload.nickname;
        },
        clearUserInfo(state) {
            state.token = null;
            state.userId = null;
            state.nickname = null;
        },
    },
    actions: {
        async login({ commit }, data) {
            try {
                console.log(data);
                let response = await api.accounts.login(data.username, data.password);
                console.log("sdfasd");
                commit('setToken', response.data.token);
                commit('setUserInfo', {
                    userId: response.data.user.id,
                    nickname: response.data.user.nickname
                });
                return {
                    success: true,
                    message: response.data.message
                }
            } catch (error) {
                return {
                    success: false,
                    message: error.response.data.message
                }
            }
        },
        logout({commit}){
            commit('clearUserInfo');
        }
    },
    modules: {},
});
