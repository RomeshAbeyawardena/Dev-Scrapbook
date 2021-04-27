<!-- Sensor Chart-->
<template>
    <div class="sensor-chart">
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
    import ChartService from '../../services/chart-service';

    export default Vue.component("sensor-chart", {
        props: ['sensorId', 'sensorFilters', 'sensorType'],
        data: function () {
            return {
                loading: true,
                chartSensorId: this.sensorId,
                filters: this.sensorFilters,
                type: this.sensorType,
                chartId: null,
                chart: null
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
            sensorId: function (newValue) {
                this.chartSensorId = newValue;
                this.renderChart();
            }
        },
        computed: {
            sensor: function () {
                return this.$store.getters.getSensorById(this.chartSensorId);
            }
        },
        methods: {
            renderChart: function () {
                if (this.chart || this.chart != null) {
                    this.chart.dispose();
                }

                var dateFilters = this.filters.sensorFilters;

                this.$store.dispatch("updateSensorReading", {
                    sensorId: this.chartSensorId,
                    type: this.type,
                    fromDate: dateFilters.fromDate,
                    toDate: dateFilters.toDate
                }).then(readings => {
                    this.loading = false;

                    this.$emit("sensor:readings:changed", readings);
                    window.setTimeout(() => {
                        this.chart = ChartService

                        var sensorReadings = this.sensor.readings;
                        chartService.renderChart(this.chartId, this.sensor.readings, this.type);
                    }, 250);
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
        },
        beforeDestroy: function () {
            this.chart.dispose();
        }
    });
</script>