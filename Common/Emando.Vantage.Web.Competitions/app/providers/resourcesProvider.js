vantage.provider("resources", [
    function () {
        this.strings = [];

        this.push = function (key) {
            this.strings.push(key);
        }

        var resolver = function () {
            return this.strings;
        };

        this.$get = function () {
            return new resolver();
        }
    }
])