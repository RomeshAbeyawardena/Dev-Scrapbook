<template>
    <p :class="element.class" 
       v-on:click="elementOnClick()" 
       v-b-popover.hover.top="tooltipText" 
       v-if="element.type=='pargraph'">
        <slot></slot>
    </p>
    <a :href="element.href" 
       v-on:click="elementOnClick()" 
       :class="elClass" 
       v-b-popover.hover.topleft="tooltipText" 
       v-else-if="element.type=='hyperlink'">
        <slot></slot>
    </a>
    <span v-on:click="elementOnClick()" 
          :class="element.class" 
          v-b-popover.hover.top="tooltipText" 
          v-else-if="element.type=='span'">
        <slot></slot>
    </span>
</template>
<script lang="js">
    import Vue from "vue";
    import IsMobile from 'ismobilejs';

    export default Vue.component("tooltip", {
        props: ["elementClass", "elementType", "toolTip", "href"],
        data: function () {
            return {
                element: {
                    href: this.href,
                    class: this.elementClass,
                    type: this.elementType
                },
                tooltipText: this.toolTip,
                toolTipActivated: false
            }
        },
        methods: {
            elementOnClick: function () {
                var isMobileInfo = IsMobile(window.navigator);
                
                if (!this.toolTipActivated && isMobileInfo.any) {
                    this.toolTipActivated = true;
                    return;
                }

                this.$emit("click");
            }
        },
        computed: {
            elClass: function () {
                return this.element.class + " d-block ";
            }
        }
    });
</script>