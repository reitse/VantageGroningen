vantage.filter("startFrom", function () {
    return function(input, start) {
        return input.slice(+start);
    };
});