<!-- Sensor Card-->
<template>
    <div class="sensor-dashboard">
        <!-- Sensor Card-->
        <b-card v-if="filters.selectedSensor" body-class="sensor-dashboard--body">
            <b-row class="sensor-dashboard--body-header">
                <b-col cols="12" xl="6">
                    <button class="btn btn-secondary mt-2 mb-4" v-on:click="resetView">Go back</button>
                </b-col>
                <b-col class="text-right">
                    <h5>{{ filters.selectedSensor.displayName }}</h5>
                </b-col>
            </b-row>
            <b-row class="sensor-dashboard--body-sensor-list">
                <b-col cols="12" xl="6"
                       v-for="sensorType in filters.selectedSensor.types"
                       v-bind:key="sensorType">
                    <sensor-card :type="sensorType" :sensor="filters.selectedSensor.id" :sensor-filters="filters">

                    </sensor-card>
                </b-col>
            </b-row>
        </b-card>
        <b-row v-if="!filters.selectedSensor" class="sensor-dashboard--body-sensor-list">
            <b-col cols="12" xl="6" v-for="sensor in filteredSensors" v-bind:key="sensor.id"> 
                <sensor-card v-on:sensor:changed="onSensorChanged" :sensor-id="sensor.id" :sensor-filters="filters">

                </sensor-card>
            </b-col>
        </b-row>
    </div>
</template>
<script lang="js">
import Vue from "vue";
    export default Vue.component("sensor-dashboard", {
        props: ['sensorId', 'sensorFilters'],
        data: function () {
            return {
                filters: {
                    selectedSensor: null,
                    sensorFilters: this.sensorFilters,
                    sensorId: this.sensorId
                }
            }
        },
        watch: {
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
            sensors: function () {
                return this.$store.getters.sensors;
            },
            columns: function () {
                var length = this.filters.selectedSensor.types.length
                if (length < 4) {

                }

                return 3;
            },
            filteredSensors: function () {
                if (this.filters.sensorId) {
                    return this.sensors.filter(f => f.id == this.filters.sensorId);
                }

                return this.sensors;
            }
        }
    });
</script>