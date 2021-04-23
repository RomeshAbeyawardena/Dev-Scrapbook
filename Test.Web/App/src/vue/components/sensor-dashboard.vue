<!-- Sensor Card-->
<template>
    <div>
        <div class="card" v-if="filters.selectedSensor">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-3">
                        <button class="btn btn-secondary mb-4" v-on:click="resetView">Go back to overview</button>
                    </div>
                    <div class="col text-right">
                        <h5>{{ filters.selectedSensor.displayName }}</h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-4 col-lg-3"
                         v-for="sensorType in filters.selectedSensor.types">
                        <sensor-card :type="sensorType" :sensor="filters.selectedSensor" :sensor-filters="filters"></sensor-card>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" v-if="!filters.selectedSensor">
            <div class="col-12 col-sm-6 col-md-4 col-lg-3" v-for="sensor in filteredSensors">
                <sensor-card v-on:sensor:changed="onSensorChanged" :sensor="sensor" :sensor-filters="filters"></sensor-card>
            </div>
        </div>
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