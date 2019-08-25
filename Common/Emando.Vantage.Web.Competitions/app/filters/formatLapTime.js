vantage.filter("formatLapTime", function () {
    return function(input, previous) {
        var time = moment.duration(input || 0);
        var previousTime = moment.duration(previous || 0);
        var seconds = time.subtract(previousTime).asSeconds();
        return (Math.floor(seconds * 10) / 10).toFixed(1);
    };
});