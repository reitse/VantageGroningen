vantage.provider("$strings", function() {
    var dictionary = [];

    this.add = function (lang, entries) {
        dictionary[lang] = $.extend(dictionary[lang] || {}, entries);
    };

    this.$get = function () {
        return dictionary[moment.locale()];
    };
});