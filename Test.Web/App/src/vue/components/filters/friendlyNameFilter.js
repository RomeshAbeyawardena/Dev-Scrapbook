import Vue from "vue";

const category_SensorType = "sensorType";

const sensorType_Temperature = "temp";
const sensorType_State = "state";
const sensorType_Battery = "battery";
const sensorType_Network = "network";

function getSensorTypeFriendlyNames(value) {
    switch (value) {
        case sensorType_Temperature:
            return "Temperature";
        case sensorType_State:
            return "Touch/Door sensor";
        case sensorType_Battery:
            return "Battery (remaining capacity)";
        case sensorType_Network: {
            return "Network strength";
        }
        default:
    }
}

Vue.filter("friendlyName", function (value, type) {
        switch (type) {
            case category_SensorType:
                return getSensorTypeFriendlyNames(value);
            default:
                return value;
        };
});