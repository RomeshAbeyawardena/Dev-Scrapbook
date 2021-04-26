<!-- Sensor Card-->
<template>
    <div>
        <b-card v-if="filters.selectedSensor">
                <b-row>
                    <b-col cols="12" sm="6" md="3">
                        <button class="btn btn-secondary mb-4" v-on:click="resetView">Go back to overview</button>
                    </b-col>
                    <b-col class="text-right">
                        <h5>{{ filters.selectedSensor.displayName }}</h5>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col cols="12" sm="6" md="4" lg="3"
                         v-for="sensorType in filters.selectedSensor.types">
                        <sensor-card :type="sensorType" :sensor="filters.selectedSensor" :sensor-filters="filters"></sensor-card>
                    </b-col>
                </b-row>
        </b-card>
        <b-row v-if="!filters.selectedSensor">
            <b-col cols="12" sm="6" md="4" lg="3" v-for="sensor in filteredSensors">
                <sensor-card v-on:sensor:changed="onSensorChanged" :sensor="sensor" :sensor-filters="filters"></sensor-card>
            </b-col>
        </b-row>
    </div>
</template>
<script lang="js">
import Vue from "vue";
    export default Vue.component("sensor-dashboard", {
        props: ['sensorId', 'sensors', 'sensorFilters'],
        data: function () {
            return {
                filters: {
                    selectedSensor: null,
                    sensorFilters: this.sensorFilters,
                    sensorId: this.sensorId
                },
                model: {
                    sensors: this.sensors
                }
            }
        },
        watch: {
            sensors: function (newValue) {
                this.model.sensors = newValue;
            },
            sensorFilters: {
                handler: function (newValue) {
                    this.filters.sensorFilters = newValue;
                },
                deep: true
            },
            sensorId: function (newValue) {
                this.filters.sensorId(newValue);
            }
        },
        methods: {
            onSensorChanged: function (sensor) {
                this.filters.selectedSensor = sensor;
                this.filters.sensorId = sensor.id;
            },
            resetView: function () {
                this.filters.selectedSensor = null;
                this.filters.sensorId = null;
            }
        },
        computed: {
            filteredSensors: function () {
                if (this.filters.sensorId) {
                    return this.model.sensors.filter(f => f.id == this.filters.sensorId);
                }

                return this.model.sensors;
            }
        }
    });
</script>