import Moment from 'moment';
import gH from '../general';
const service = {
    getDate: function(args) {
        
        let date = new Moment().utc();
        
        if (!args) {
            console.warn("No args provided");
            return date.toDate();
        }

        if (!gH.isNullOrUndefined(args.year)) {
            date = date.year(args.year);
        }

        if (!gH.isNullOrUndefined(args.month)) {
            date = date.month(args.month);
        }

        if (!gH.isNullOrUndefined(args.day)) {
            date = date.day(args.day)
        }

        if (!gH.isNullOrUndefined(args.offSet)) {
            var offSet = args.offSet;

            if (offSet.mode === 'add') {
                date.add(offSet.value, offSet.period);
            }
            else if (offSet.mode == 'subtract') {
                date.subtract(offSet.value, offSet.period);
            }
        }

        if (!gH.isNullOrUndefined(args.hour)) {
            date = date.hour(args.hour)
        }

        if (!gH.isNullOrUndefined(args.minute)) {
            date = date.minute(args.minute);
        }

        return date.toDate();
    }
}

export default service;