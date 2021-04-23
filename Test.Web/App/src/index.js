﻿import Vue from 'vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import App from './vue/app';

// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue)

require("./scss/index.scss");
require('./vue/components');

new Vue({
    el: '#app',
    render: h => h(App),
}); 