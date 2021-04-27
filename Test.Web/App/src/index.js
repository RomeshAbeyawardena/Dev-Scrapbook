import Vue from 'vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import Vuex from 'vuex';
import App from './vue/app';
import { SensorStore } from './vue/stores/sensor';
// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue);

require("./scss/index.scss");
require('./vue/components');

new Vue({
    el: '#app',
    render: h => h(App),
    store: SensorStore
}); 