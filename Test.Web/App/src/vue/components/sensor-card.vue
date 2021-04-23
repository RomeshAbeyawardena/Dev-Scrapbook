<!-- Sensor Card-->
<template>
    <div class="card chart-card">
        <div class="card-body">
            <h5 v-if="!filters.sensorId" class="card-title"><a href="#" class="text-black-50" v-on:click="selectSensor(sensorInfo)" >{{ sensorInfo.displayName }}</a></h5>
            <p>{{sensorType | friendlyName('sensorType')}}</p>
            <sensor-chart
                          :sensor-type="sensorType"
                          :sensor-filters="filters"
                          :sensor-id="sensorInfo.id">
            </sensor-chart>
            <div class="text-right">
                <button v-on:click="selectSensor(sensorInfo)" class="btn btn-secondary btn-sm mt-4">View sensor dashboard</button>
            </div>
        </div>
    </div>
</template>
<script lang="js">
    import Vue from "vue";

    export default Vue.component("sensor-card", {
        props: ['sensorId', 'sensor', 'sensorFilters', 'type'],
        data: function () {
            return {
                filters: this.sensorFilters,
                sensorInfo: this.sensor,
                sensor_type: this.type
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
            selectSensor: function (sensor) {
                this.$emit("sensor:changed", sensor);
            }
        },
        watch: {
            sensorFilters: {
                handler: function(newValue) {
                    this.filters = newValue;
                },
                deep: true
            }
        }
    });
</script>