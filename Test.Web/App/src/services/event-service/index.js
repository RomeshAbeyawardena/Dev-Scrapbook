import { HubConnectionBuilder } from '@microsoft/signalr';

const eventService = {
    hubConnection: null,
    connect: function (endpoint, callback) {
        if (!this.hubConnection) {
            this.hubConnection = new HubConnectionBuilder()
                .withUrl("http://localhost:5000/sensorhub")//move to config;
                .build();
        }

        this.hubConnection.start();
        this.hubConnection.on(endpoint, callback);
        return this.hubConnection;
    },
    disconnect: function () {
        if (this.hubConnection) {
            this.hubConnection.stop();
        }
    }
}

export default eventService;