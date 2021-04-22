<!-- Sensor Card-->
<template>
    <div class="card chart-card">
        <div class="card-body">
            <h5 class="card-title">{{ sensorInfo.displayName }}</h5>
            <sensor-chart sensor-type="temp"
                          :sensor-filters="filters"
                          :sensor-id="sensorInfo.id">
            </sensor-chart>
        </div>
    </div>
</template>
<script lang="js">
    import Vue from "vue";
    export default Vue.component("sensor-card", {
        props: ['sensor', 'sensorFilters'],
        data: function () {
            return {
                filters: this.sensorFilters,
                sensorInfo: this.sensor
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