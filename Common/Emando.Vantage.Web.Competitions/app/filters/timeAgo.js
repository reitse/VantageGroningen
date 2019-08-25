vantage.filter("timeAgo", [
    function() {
        return function (input) {
            if (!input)
                return null;
            return moment(input).fromNow();
        }
    }
]);