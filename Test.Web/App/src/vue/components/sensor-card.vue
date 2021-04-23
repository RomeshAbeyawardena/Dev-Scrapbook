<!-- Sensor Card-->
<template>
    <div class="card chart-card">
        <div class="card-body">
            <h5 v-if="!filters.sensorId" class="card-title">{{ sensorInfo.displayName }}</h5>
            <p>{{sensorType | friendlyName('sensorType')}}</p>
            <sensor-chart
                          :sensor-type="sensorType"
                          :sensor-filters="filters"
                          :sensor-id="sensorInfo.id">
            </sensor-chart>
            <button v-on:click="selectSensor(sensorInfo)" class="btn btn-secondary btn-sm mt-4">Sensor dashboard</button>
        </div>
    </div>
</template>
<script lang="js">
    import Vue from "vue";

    export default Vue.component("sensor-card", {
        props: ['sensorId', 'sensor', 'sensorFilters'],
        data: function () {
            return {
                filters: this.sensorFilters,
                sensorInfo: this.sensor
            }
        },
        computed: {
            sensorType: function () {

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