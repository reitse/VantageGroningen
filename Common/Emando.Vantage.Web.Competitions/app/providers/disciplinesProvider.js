vantage.provider("disciplines", function () {
    var disciplines = {};

    this.addDiscipline = function (name, d) {
        disciplines[name] = d;
    };

    var resolver = function() {
        return disciplines;
    };

    this.$get = function() {
        return new resolver();
    }
});