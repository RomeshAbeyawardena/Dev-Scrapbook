import Axios from 'axios'
import ChartService from './services/chart-service';
import Vue from 'vue';

import App from './vue/app';

require("./scss/index.scss");

require('./vue/components');

new Vue({
    el: '#app',
    render: h => h(App),
});