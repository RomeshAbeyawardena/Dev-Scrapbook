<!-- Sensor Card-->
<template>
    <b-card class="chart-card">
            <h5 v-if="!filters.sensorId" 
                class="card-title text-truncate">
                <tooltip :tool-tip="sensorInfo.displayName" 
                         element-type="hyperlink" href="javascript:void(0)" 
                         element-class="text-black-50" 
                   v-on:click="selectSensor(sensorInfo)" >
                    {{ sensorInfo.displayName }}
                </tooltip>
            </h5>
            <p>{{sensorType | friendlyName('sensorType')}}</p>
            <sensor-chart v-on:sensor:readings:changed="storeSensorReadings"
                          :sensor-type="sensorType"
                          :sensor-filters="filters"
                          :sensor="sensorInfo">
            </sensor-chart>
            <div class="text-right mb-2">
                <button v-if="!sensor_type" 
                        v-on:click="selectSensor(sensorInfo)" 
                        class="btn btn-secondary btn-sm mt-4">View sensor dashboard</button>
                <button v-if="sensor_type" 
                        v-on:click="viewSensorData(sensorInfo)" 
                        class="btn btn-secondary btn-sm mt-4"><span v-if="!showData">Show</span><span v-if="showData">Hide</span> data</button>
            </div>
            <sensor-data-grid v-if="sensor_type && showData" :sensor-type="this.sensorType" :sensor-readings="readings"></sensor-data-grid>
    </b-card>
</template>
<script lang="js">
    import Vue from "vue";

    export default Vue.component("sensor-card", {
        props: ['sensorId', 'sensor', 'sensorFilters', 'type'],
        data: function () {
            return {
                filters: this.sensorFilters,
                sensorInfo: this.sensor,
                sensor_type: this.type,
                readings: null,
                showData: false
            }
        },
        computed: {
            sensorType: function () {
                if (this.sensor_type) {
                    return this.sensor_type;
                }
                if (this.sensorInfo.types.includes('temp')) {
                    return 'temp';
                }
                else if (this.sensorInfo.types.includes('state')) {
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
            sensor: function (newValue) {
                this.sensorInfo = newValue;
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