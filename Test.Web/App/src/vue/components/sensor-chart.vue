<!-- Sensor Chart-->
<template>
    <div>
        <div v-if="loading" class="text-center">
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <div v-if="!loading">
            <div class="chart" :id="chartId"></div>
        </div>
    </div>
</template>

<script lang="js">
    import Vue from "vue";
    import RandomStringGenerator from "../../services/random-string-generator";
    import SensorService from '../../services/sensor-service';
    import ChartService from '../../services/chart-service';

    export default Vue.component("sensor-chart", {
        props: ['sensor', 'sensorFilters', 'sensorType'],
        data: function () {
            return {
                loading: true,
                sensorInfo: this.sensor,
                filters: this.sensorFilters,
                type: this.sensorType,
                chartId: null,
            }
        },
        watch: {
            sensorFilters: {
                handler: function (newValue) {
                    this.loading = true;
                    this.filters = newValue;
                    this.renderChart();
                },
                deep: true
            },
            sensor: function (newValue) {
                this.sensorInfo = newValue;
                this.renderChart();
            }
        },
        methods: {
            renderChart: function () {

                var dateFilters = this.filters.sensorFilters;
                SensorService
                    .getSensorReadings(this.sensorInfo.id, this.type,
                        dateFilters.fromDate,
                        dateFilters.toDate)
                    .then(readings => {
                        this.loading = false;

                        this.$emit("sensor:readings:changed", readings);
                        window.setTimeout(() => ChartService
                            .renderChart(this.chartId, readings, this.type), 250);
                    });
            }
        },
        created: function () {
            this.chartId = RandomStringGenerator.getRandomString(30);
        },
        mounted: function () {
            this.$nextTick(function () {
                this.renderChart();
            });
        }
    });
</script>