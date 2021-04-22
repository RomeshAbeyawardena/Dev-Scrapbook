import Axios from 'axios';
import Promise from 'promise';
const apiUrl = "http://localhost:5000/api/";
const sensorResourceName = "sensors";

const service = {
    getSensors: function (tag) {
        return new Promise((resolve, reject) => {

            var url = apiUrl + sensorResourceName;
            if (tag) {
                url += "?tag=" + tag;
            }
                
            Axios.get(url)
                .then(e => resolve(e.data.sensors))
                .catch(error => reject(error));
        });
    },
    getSensorReadings: function (sensorId, sensorType, fromDate, toDate) {
        return new Promise((resolve, reject) => {
            var url = apiUrl + sensorResourceName + "/" + sensorId;

            if (fromDate) {
                url = url + "/" + fromDate.toISOString();

                if (toDate) {
                    url = url + "/" + toDate.toISOString();
                }
            }

            Axios.get(url)
                .then(e => {

                    let readings = e.data.sensorReadings;

                    if (sensorType) {
                        readings = readings.filter(t => t.type == sensorType);
                        
                        if (!readings.length) {
                            readings = e.data.sensorReadings.filter(t => t.type == 'state');
                        }
                    }

                    return resolve(readings);
                }).catch(error => reject(error));
        });

    }
}

export default service;