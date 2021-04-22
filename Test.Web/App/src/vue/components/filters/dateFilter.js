import Vue from "vue";
import Moment from "moment";

Vue.filter("date", function (value, format) {
    var date = Moment(value);
    return date.format(format);
});