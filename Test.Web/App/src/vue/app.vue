<template>
    <div>
        <h1>
            <span>{{name}}</span>
        </h1>
        <div>
            <p>Dashboard for Tag: {{ tag }}</p>
        </div>
        <b-row>
            <b-col cols="6" md="12" lg="3" class="mt-2">
                <button v-on:click="getSensors()" class="btn btn-secondary">
                    <span class="fa fa-sync-alt text-light"></span>
                    <span>Refresh </span><span class="d-none d-lg-inline">Dashboard</span>
                </button>
            </b-col>
            <b-col cols="6" md="12" lg="9" class="text-lg-right mt-2">
                <toggle-button v-on:onToggleClicked="onToggleClicked" :options="toggleButtonOptions"></toggle-button>
                <date-range v-on:filter:clicked="onFilterClicked"
                            v-if="isCustomDateSelected"
                            :from-date="filters.fromDate"
                            :to-date="filters.toDate"></date-range>
            </b-col>
        </b-row>
        <div class="mt-2" style="clear:both">
            <p class="small mb-1">Displaying sensor data for the dates between {{ filters.fromDate | date('Do MMMM YYYY HH:mm Z') }} and {{ filters.toDate | date('Do MMMM YYYY HH:mm Z') }}</p>
            <sensor-dashboard :sensor-id="selectedSensorId" :sensors="sensors" :sensor-filters="filters">

            </sensor-dashboard>
        </div>
        <p class="small">All dates on this page are displayed in UTC time (+00:00), unless otherwise specified.</p>
    </div>
</template>

<script lang="js">
    import Vue from "vue";
    import SensorService from '../services/sensor-service';
    import DateService from '../services/date-service';

    const DateFilter_Today = "Today";
    const DateFilter_PreviousDay = "PreviousDay";
    const DateFilter_Last24Hours = "Last-24Hours";
    const DateFilter_Last7Days = "Last-7Days";
    const DateFilter_Last30Days = "Last-30Days";
    const DateFilter_CustomRange = "Custom";
    const ButtonType_Primary = "primary";
    const ButtonType_Secondary = "secondary";

    export default Vue.extend({
        data: function () {
            return {
                name: 'Disruptive Tech Sensors Dashboard',
                tag: 'disruptive-tech',
                selectedSensorId: null,
                sensors: [],
                filters: {
                    fromDate: new Date(),
                    toDate: new Date()
                },
                filterMode: "Today",
                toggleButtonOptions: [
                    { buttonType: ButtonType_Primary, value: DateFilter_Today, text: "Today" },
                    { buttonType: ButtonType_Secondary, value: DateFilter_PreviousDay, text: "Previous Day", description: "This filter shows data from 8am to 8pm UTC on the previous day" },
                    { buttonType: ButtonType_Secondary, value: DateFilter_Last24Hours, text: "Last 24 hours" },
                    { buttonType: ButtonType_Secondary, value: DateFilter_Last7Days, text: "Last 7 days" },
                    { buttonType: ButtonType_Secondary, value: DateFilter_Last30Days, text: "Last month" },
                    { buttonType: ButtonType_Secondary, value: DateFilter_CustomRange, text: "Custom" }]
            }
        },
        methods: {
            setFilter: function (filterMode) {
                this.filterMode = filterMode;

                switch (filterMode) {
                    case DateFilter_CustomRange: {

                        return;
                    }
                    case DateFilter_Today: {
                        this.filters.fromDate = DateService.getDate({
                            hour: 0,
                            minute: 0,
                            second: 0,
                            millisecond: 0
                        });
                        this.filters.toDate = DateService.getDate();
                        return;
                    }
                    case DateFilter_PreviousDay: {
                        this.filters.fromDate = DateService.getDate({
                            offSet: {
                                mode: DateService.offSetMode.subtract,
                                value: 1,
                                period: "days"
                            },
                            hour: 8,
                            minute: 0,
                            second: 0,
                            millisecond: 0
                        });
                        this.filters.toDate = DateService.getDate({
                            offSet: {
                                mode: DateService.offSetMode.subtract,
                                value: 1,
                                period: "days"
                            },
                            hour: 20,
                            minute: 0,
                            second: 0,
                            millisecond: 0
                        });
                        return;
                    }
                    case DateFilter_Last24Hours: {
                        this.filters.fromDate = DateService.getDate({
                            offSet: {
                                mode: DateService.offSetMode.subtract,
                                value: 24,
                                period: "hours"
                            }
                        });
                        this.filters.toDate = DateService.getDate();
                        return;
                    }
                    case DateFilter_Last7Days: {
                        this.filters.fromDate = DateService.getDate({
                            offSet: {
                                mode: DateService.offSetMode.subtract,
                                value: 7,
                                period: "days"
                            }
                        });
                        this.filters.toDate = DateService.getDate();
                        return;
                    }
                    case DateFilter_Last30Days: {
                        this.filters.fromDate = DateService.getDate({
                            offSet: {
                                mode: DateService.offSetMode.subtract,
                                value: 30,
                                period: "days"
                            }
                        });
                        this.filters.toDate = DateService.getDate();
                        return;
                    }
                    default:
                }
            },
            onToggleClicked: function (option) {

                for (let o of this.toggleButtonOptions) {
                    o.buttonType = ButtonType_Secondary;
                }

                option.buttonType = ButtonType_Primary;
                this.setFilter(option.value);
            },
            onFilterClicked: function (filter) {
                console.log(filter);
                this.filters.fromDate = filter.start;
                this.filters.toDate = filter.end;
            },
            getSensors: function () {
                return SensorService
                    .getSensors(this.tag)
                    .then(e => {
                        this.sensors = e;
                    });
            }
        },
        computed: {
            isCustomDateSelected: function () {
                return this.filterMode === DateFilter_CustomRange;
            }
        },
        created: function () {
            this.setFilter(DateFilter_Today);
            this.getSensors();
        }
    });
</script>