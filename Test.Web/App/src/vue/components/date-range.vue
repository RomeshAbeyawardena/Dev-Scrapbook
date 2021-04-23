<template>
    <div class="float-right" style="width:300px; border: 0.1rem solid silver; width: 275px; padding: 1rem; margin-top: -0.5rem; border-radius: 0.5rem;">
        <div class="input-group mb-3">
            <date-picker v-model="dateRange.start" mode="dateTime" :is24hr="true">
                <template v-slot="{ inputValue, inputEvents }">
                    <input v-on="inputEvents" :value="inputValue" type="datetime" class="form-control" placeholder="From Date" aria-label="Recipient's username" aria-describedby="button-addon2">
                </template>
            </date-picker>
            
            <div class="input-group-append">
                <div class="btn btn-outline-secondary">
                    <span class="fa fa-calendar-alt"></span>
                </div>
            </div>
        </div>
        <div class="input-group mb-3">
            <date-picker v-model="dateRange.end" mode="dateTime" :is24hr="true">
                <template v-slot="{ inputValue, inputEvents }">
                    <input v-on="inputEvents" :value="inputValue" type="datetime" class="form-control" placeholder="From Date" aria-label="Recipient's username" aria-describedby="button-addon2">
                </template>
            </date-picker>

            <div class="input-group-append">
                <div class="btn btn-outline-secondary">
                    <span class="fa fa-calendar-alt"></span>
                </div>
            </div>
        </div>
        <button v-on:click="filterClicked" type="button">Filter</button>
    </div>
</template>
<script lang="js">
    import Vue from "vue";
    export default Vue.component("date-range", {
        props: ["fromDate", "toDate", "minimumDate", "maximumDate"],
        data: function () {
            return {
                dateRange: {
                    start: this.fromDate,
                    end: this.toDate
                },
                filter: {
                    minimumDate: this.minimumDate,
                    maximumDate: this.maximumDate
                }
            }
        },
        watch: {
            fromDate: function (newValue) {
                this.dateRange.start = newValue;
            },
            toDate: function (newValue) {
                this.dateRange.end = newValue;
            }
        },
        methods: {
            getButtonStyle: function (option) {
                return "btn btn-" + option.buttonType;
            },
            onDateChangedToggle: function (option) {
                this.$emit('onDateChanged', option);
            },
            filterClicked: function () {
                this.$emit("filter:clicked", this.dateRange);
            }
        }
    });
</script>