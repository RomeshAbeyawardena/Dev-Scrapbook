import Vue from 'vue';
import Vuex from 'vuex';
import SensorService from '../../services/sensor-service';
Vue.use(Vuex);

export const SensorStore = new Vuex.Store({
    state: {
        sensors: []
    },
    getters: {
        sensors: function (state) {
            return state.sensors;
        },
        getSensorById: function (state) {
            return (id) => state.sensors.filter(a => a.id == id)[0];
        }
    },
    mutations: {
        updateSensors: function (state, sensors) {
            //
            state.sensors.length = 0;
            for (var sensor of sensors) {
                state.sensors.push(sensor);
            }
        }
    },
    actions: {
        getSensorReading: function (context, sensorId, dateFilters) {
            let sensor = context.getters.sensor(sensorId);

            SensorService
                .getSensorReadings(sensorId, this.type,
                    dateFilters.fromDate,
                    dateFilters.toDate)
                .then(readings => {
                    sensor.readings = readings;
                });

            console.log(sensor);
        },
        getSensors: function (context, tag) {
            SensorService.getSensors(tag)
                .then(sensors => context.commit('updateSensors', sensors));
        }
    }
});