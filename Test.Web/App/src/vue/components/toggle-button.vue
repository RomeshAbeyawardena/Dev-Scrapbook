<template>
    <div>
        <div class="btn-group d-none d-sm-block" role="group" aria-label="Basic example">
            <button  v-for="option in button.options" 
                    v-b-popover.hover.top="option.description"
                    type="button" 
                    @click="onToggle(option)" 
                    :class="getButtonStyle(option)">{{ option.text }}</button>
        </div>
        <div class="d-block d-sm-none text-right">
            <b-dropdown id="dropdown-1" text="Filter" class="m-md-2" right>
                <b-dropdown-item v-on:click="onToggle(option)" v-for="option in button.options">{{ option.text }}</b-dropdown-item>
            </b-dropdown>
        </div>
    </div>
</template>
<script lang="js">
    import Vue from "vue";
    export default Vue.component("toggle-button", {
        props: ["options"],
        data: function () {
            return {
                button: {
                    options: this.options
                }
            }
        },
        methods: {
            getButtonStyle: function (option) {
                return "btn btn-" + option.buttonType;
            },
            onToggle: function (option) {
                this.$emit('onToggleClicked', option);
            }
        }
    });
</script>