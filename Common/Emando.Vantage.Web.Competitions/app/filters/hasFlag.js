vantage.filter("hasFlag", function () {
    return function(a, b) {
        return (a & b) === b;
    };
});