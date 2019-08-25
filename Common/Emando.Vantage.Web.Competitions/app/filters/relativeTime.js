vantage.filter("relativeTime", [
    function() {
        return function(input) {
            if (!input)
                return null;
            return moment(input).calendar();
        }
    }
])