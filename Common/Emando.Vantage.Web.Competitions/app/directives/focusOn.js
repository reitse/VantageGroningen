vantage.directive("focusOn", [
    "$timeout", "$parse",
    function($timeout, $parse) {
        return {
            link: function(scope, element, attr) {
                var model = $parse(attr.focusOn);
                scope.$watch(model, function(value) {
                    if (value === true) {
                        $timeout(function() {
                            element[0].focus();
                        });
                    }
                });
                element.bind("blur", function() {
                    console.log("blur");
                    scope.$apply(model.assign(scope, false));
                });
            }
        };
    }
]);