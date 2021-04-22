<template>
    <div style="width:300px;" class="float-right mt-4">
        <div class="input-group mb-3">
            <input type="datetime" class="form-control" placeholder="From Date" aria-label="Recipient's username" aria-describedby="button-addon2">
            <div class="input-group-append">
                <button class="btn btn-outline-secondary" type="button" id="button-addon2"><span class="fa fa-calendar-alt"></span></button>
            </div>
        </div>
        <div class="input-group mb-3">
            <input type="datetime" class="form-control" placeholder="To Date" aria-label="Recipient's username" aria-describedby="button-addon2">
            <div class="input-group-append">
                <button class="btn btn-outline-secondary" type="button" id="button-addon2"><span class="fa fa-calendar-alt"></span></button>
            </div>
        </div>
    </div>
</template>
<script lang="js">
    import Vue from "vue";
    export default Vue.component("date-range", {
        props: ["minimumDate", "maximumDate"],
        data: function () {
            return {
                filter: {
                    minimumDate: this.minimumDate,
                    maximumDate: this.maximumDate
                }
            }
        },
        methods: {
            getButtonStyle: function (option) {
                return "btn btn-" + option.buttonType;
            },
            onDateChangedToggle: function (option) {
                this.$emit('onDateChanged', option);
            }
        }
    });
</script>