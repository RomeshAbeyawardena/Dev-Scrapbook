<template>
    <div>
        <h1>
            {{name}}
        </h1>
        <div class="row">
            <div class="col">
                <p>Dashboard for Tag: {{ tag }} Showing charts from {{ filters.fromDate | date('Do MMMM YYYY HH:mm z') }} to {{ filters.toDate | date('Do MMMM YYYY HH:mm z') }}</p>
            </div>
            <div class="col text-right">
                <toggle-button v-on:onToggleClicked="onToggleClicked" :options="toggleButtonOptions"></toggle-button>
                <date-range v-if="filterMode == 'custom'"></date-range>
            </div>
        </div>
        <div class="text-right">
            
        </div>
        <div class="row mt-2" style="clear:both">
            <div class="col-md-5 col-lg-3 col-xl-4" v-for="sensor in sensors">
                <sensor-card :sensor="sensor" :sensor-filters="filters"></sensor-card>
            </div>
        </div>
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

  export default Vue.extend({
    data: function() {
      return {
          name: 'Sensors Dashboard',
          tag: 'disruptive-tech',
          sensors: [],
          filters: {
              fromDate: new Date(),
              toDate: new Date()
          },
          filterMode: "Today",
          toggleButtonOptions: [
              { buttonType: "primary", value: DateFilter_Today, text: "Today" },
              { buttonType: "secondary", value: DateFilter_PreviousDay, text: "Previous Day" },
              { buttonType: "secondary", value: DateFilter_Last24Hours, text: "Last 24 hours" },
              { buttonType: "secondary", value: DateFilter_Last7Days, text: "Last 7 days" },
              { buttonType: "secondary", value: DateFilter_Last30Days, text: "Last month" },
              { buttonType: "secondary", value: DateFilter_CustomRange, text: "Custom" }]
        }
      },
      methods: {
          setFilter: function (filterMode) {
              this.filterMode = filterMode;
              
              switch (filterMode) {
                  case DateFilter_CustomRange: {
                      this.filters.fromDate = new Date(2021, 4, 22);
                      this.filters.toDate = null;
                      return;
                  }
                  case DateFilter_Today: {
                      this.filters.fromDate = DateService.getDate({ hour: 0, minute: 0, second: 0 });
                      this.filters.toDate = DateService.getDate();
                      return;
                  }
                  case DateFilter_PreviousDay: {
                      this.filters.fromDate = DateService.getDate({
                          offSet: { mode: "subtract", value: 1, period: "days" },
                          hour: 8,
                          minute: 0,
                          second: 0
                      });
                      this.filters.toDate = DateService.getDate({
                          offSet: { mode: "subtract", value: 1, period: "days" },
                          hour: 20,
                          minute: 0,
                          second: 0
                      });
                      return;
                  }
                  case DateFilter_Last24Hours: {
                      this.filters.fromDate = DateService.getDate({ offSet: { mode: "subtract", value: 24, period: "hours" } });
                      this.filters.toDate = DateService.getDate();
                      return;
                  }
                  case DateFilter_Last7Days: {
                      this.filters.fromDate = DateService.getDate({ offSet: { mode: "subtract", value: 7, period: "days" } });
                      this.filters.toDate = DateService.getDate();
                      return;
                  }
                  case DateFilter_Last30Days: {
                      this.filters.fromDate = DateService.getDate({ offSet: { mode: "subtract", value: 30, period: "days" } });
                      this.filters.toDate = DateService.getDate();
                      return;
                  }
                  default:
              }
          },
          onToggleClicked: function (option) {
              
              for (let o of this.toggleButtonOptions) {
                  o.buttonType = "secondary";
              }

              option.buttonType = "primary";
              this.setFilter(option.value);
          }
      },
      created: function () {
          this.setFilter(DateFilter_Today);
          SensorService
              .getSensors(this.tag)
              .then(e => {
                  this.sensors = e;
              });
      }
  });
</script>