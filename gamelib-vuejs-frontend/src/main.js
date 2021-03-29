import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import vuetify from './plugins/vuetify';
import axios from 'axios';
import './plugins/base'; //import all base components
import api from './api/gamelib.api';
import VueToast from 'vue-toast-notification';
//import 'vue-toast-notification/dist/theme-default.css';
import 'vue-toast-notification/dist/theme-sugar.css';

Vue.config.productionTip = false;

Vue.prototype.$axios = axios;
Vue.prototype.$api = api;

Vue.use(VueToast);

new Vue({
    router,
    store,
    vuetify,
    render: (h) => h(App),
}).$mount('#app');
