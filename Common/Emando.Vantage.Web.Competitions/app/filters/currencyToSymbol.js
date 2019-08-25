vantage.filter("currencyToSymbol", function() {
    return function(input) {
        switch (input) {
            case "EUR":
                return "€";
            case "USD":
                return "$";
            default:
                return input;
        }
    }
});