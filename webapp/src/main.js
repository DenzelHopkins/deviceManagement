import Vue from 'vue';
import App from './App.vue';
import axios from 'axios';
import VueAxios from 'vue-axios';

import moment from 'moment';
Vue.use(moment);

import { BootstrapVue, BootstrapVueIcons } from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
Vue.use(VueAxios, axios);
Vue.use(BootstrapVue, BootstrapVueIcons );
axios.defaults.baseURL = 'http://' + process.env.VUE_APP_ADDRESS + ':' + process.env.VUE_APP_SERVER_PORT;


import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'
import vuetify from './plugins/vuetify';
Vue.use(Vuetify)

Vue.config.productionTip = true;

new Vue({
    vuetify,
    render: h => h(App)
}).$mount('#app');
