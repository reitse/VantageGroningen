vantage.filter("format", function() {
    return function(input) {
        return input.format(Array.prototype.slice.call(arguments, 1));
    };
});