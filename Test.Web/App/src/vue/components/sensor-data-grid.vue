<template>
    <div>
        <b-table :current-page="pager.currentPage" 
                 :per-page="pager.itemsPerPage" striped hover :fields="fields" :items="readings">
            <template #cell(timestampUtc)="data">
                {{ data.item.timestampUtc | date('DD/MM/YYYY HH:mm:ss') }}
            </template>
        </b-table>
        <b-pagination v-model="pager.currentPage" 
                      :total-rows="readings.length"
                      :per-page="pager.itemsPerPage"
                      hide-goto-end-buttons>
        </b-pagination>
    </div>
</template>

<script lang="js">
    import Vue from "vue";

    export default Vue.component("sensor-data-grid", {
        props: ['sensorReadings', 'sensorType'],
        data: function () {
            return {
                fields: [
                    {
                        key: "rawValue",
                        label: "Reading",
                        sortable: true
                    },
                    {
                        key: "timestampUtc",
                        label: "Timestamp (UTC)",
                        sortable: true
                    }
                ],
                pager: {
                    currentPage: 1,
                    itemsPerPage: 10,
                },
                loading: true,
                readings: this.sensorReadings,
                type: this.sensorType
            }
        },
        watch: {
            sensorReadings: function (newValue) {
                this.readings = newValue;
            }
        }
    });
</script>