vantage.filter("formatTime", function () {
    return function(input, decimals) {
        if (input == null) {
            return null;
        }
        var secondsFraction = Array((decimals ? parseInt(decimals) : 3) + 1).join("S");

        var time = moment.duration(input);
        var format;
        if (time.minutes() == 0) {
            format = "s." + secondsFraction;
        } else {
            format = "m:ss." + secondsFraction;
        }
        return moment.utc(0).add(time).format(format);
    };
})