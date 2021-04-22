<template>
    <div>
        <div class="btn-group" role="group" aria-label="Basic example">
            <button  v-for="option in button.options" type="button" @click="onToggle(option)" :class="getButtonStyle(option)">{{ option.text }}</button>
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