vantage.filter("formatVenue", function() {
    return function(input) {
        if (!input) {
            return null;
        }
        return "{0} ({1})".format(input.address.city, input.name);
    }
});