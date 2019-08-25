vantage.filter("isInRole", [
    function() {
        return function(input, role) {
            return !!input && !!input.roles.find(function(r) { return r === role; });
        }
    }
]);