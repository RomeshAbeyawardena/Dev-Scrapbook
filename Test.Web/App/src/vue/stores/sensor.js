﻿import Vue from 'vue';
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
        },
        updateSensor: function (state, sensorId, newValue) {
            let foundItem = state.sensors.filter(a => a.id == sensorId)[0];
            let index = state.sensors.findIndex(foundItem);
            state.sensors.splice(index, 1)
            state.sensors.push(newValue);
        }
    },
    actions: {
        updateSensorReading: function (context, data) {
            let sensor = context.getters.getSensorById(data.sensorId);

            var p = SensorService
                .getSensorReadings(data.sensorId,
                    null,
                    data.fromDate,
                    data.toDate);

            p.then(readings => {
                    sensor.readings = readings;
                    context.commit('updateSensor', sensor.id, sensor);
                });

            return p;
            //console.log(sensor);
        },
        getSensors: function (context, tag) {
            var p = SensorService.getSensors(tag);
                
            p.then(sensors => context.commit('updateSensors', sensors));

            return p;
        }
    }
});