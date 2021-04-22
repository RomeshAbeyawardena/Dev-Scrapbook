const service = {
    getRandomString: function (length){
        let stringValue = "";

        while (stringValue.length < length) {
            var randomValue = this.randomInteger(97, 122);
            stringValue = stringValue + String.fromCharCode(randomValue);
            
        }

        return stringValue;
    },
    randomInteger: function(min, max) {
        // returns integer between min and max
        let rand = min + Math.random() * (max + 1 - min);
        return Math.floor(rand);
    }
}

export default service;