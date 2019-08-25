vantage.filter("formatDistanceValue", [
    "$filter",
    function($filter) {
        return function(distance) {
            return $filter("translate")(distance.discipline + "_ValueQuantityFormat_" + distance.valueQuantity).format(distance.value);
        };
    }
]);