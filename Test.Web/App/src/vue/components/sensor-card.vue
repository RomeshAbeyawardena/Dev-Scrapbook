<!-- Sensor Card-->
<template>
    <b-card class="sensor-card" body-class="sensor-card--body">
            <h5 v-if="!filters.sensorId" 
                class="sensor-card--title">
                <tooltip :tool-tip="sensor.displayName"
                         element-type="hyperlink" href="javascript:void(0)"
                         element-class="sensor-card--title"
                         v-on:click="selectSensor(sensor)">
                    {{ sensor.displayName }}
                </tooltip>
            </h5>
            <p>{{sensorType | friendlyName('sensorType')}}</p>
            <sensor-chart v-on:sensor:readings:changed="storeSensorReadings"
                          :sensor-type="sensorType"
                          :sensor-filters="filters"
                          :sensor-id="sensor.id">
            </sensor-chart>
            <div class="sensor-card-controls">
                <button v-if="!sensor_type" 
                        v-on:click="selectSensor(sensor)" 
                        class="btn btn-secondary btn-sm mt-4">View sensor dashboard</button>
                <button v-if="sensor_type" 
                        v-on:click="viewSensorData(sensor)" 
                        class="btn btn-secondary btn-sm mt-4">
                    <span v-if="!showData">Show</span>
                    <span v-if="showData">Hide</span> data
                </button>
            </div>
            <sensor-data-grid v-if="sensor_type && showData" :sensor-type="this.sensorType" :sensor-readings="readings">

            </sensor-data-grid>
    </b-card>
</template>
<script lang="js">
    import Vue from "vue";

    export default Vue.component("sensor-card", {
        props: ['sensorId', 'sensorFilters', 'type'],
        data: function () {
            return {
                filters: this.sensorFilters,
                current_SensorId: this.sensorId,
                sensor_type: this.type,
                readings: null,
                showData: false
            }
        },
        computed: {
            currentSensorId: function () {
                return this.filters.sensorId
                    ?? this.current_SensorId;
            },
            sensor: function () {
                return this.$store.getters.getSensorById(this.currentSensorId);
            },
            sensorType: function () {
                if (this.sensor_type) {
                    return this.sensor_type;
                }

                if (!this.sensor) {
                    return null;
                }

                if (this.sensor.types.includes('temp')) {
                    return 'temp';
                }
                else if (this.sensor.types.includes('state')) {
                    return 'state';
                }
                else
                    return null;
            }
        },
        methods: {
            storeSensorReadings(readings) {
                this.readings = readings;
            },
            selectSensor: function (sensor) {
                this.$emit("sensor:changed", sensor);
            },
            viewSensorData: function (sensor) {
                this.$emit("sensor:viewSensorData", sensor);
                this.showData = !this.showData;
            }
        },
        watch: {
            sensorId: function (newValue) {
                this.currentSensorId = newValue;
            },
            sensorFilters: {
                handler: function(newValue) {
                    this.filters = newValue;
                },
                deep: true
            }
        }
    });
</script>