vantage.filter("formatPoints", function () {
    return function (input, decimals) {
        if (!input) {
            return null;
        }
        return input.toFixed(decimals);
    }
})