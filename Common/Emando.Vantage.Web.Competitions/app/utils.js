if (!String.prototype.format) {
    String.prototype.format = function() {
        var values = Array.isArray(arguments[0]) ? arguments[0] : arguments;
        var s = this;
        var i = values.length;

        while (i--) {
            s = s.replace(new RegExp("\\{" + i + "\\}", "gm"), values[i]);
        }
        return s;
    };
}

if (!String.prototype.trim) {
    (function () {
        var rtrim = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g;
        String.prototype.trim = function () {
            return this.replace(rtrim, '');
        };
    })();
}

if (!Array.prototype.findIndex) {
    Array.prototype.findIndex = function (predicate) {
        if (this == null) {
            throw new TypeError("Array.prototype.findIndex called on null or undefined");
        }
        if (typeof predicate !== "function") {
            throw new TypeError("predicate must be a function");
        }
        var list = Object(this);
        var length = list.length >>> 0;
        var thisArg = arguments[1];
        var value;

        for (var i = 0; i < length; i++) {
            value = list[i];
            if (predicate.call(thisArg, value, i, list)) {
                return i;
            }
        }
        return -1;
    };
}

if (!Array.prototype.find) {
    Array.prototype.find = function(predicate) {
        if (this == null) {
            throw new TypeError("Array.prototype.find called on null or undefined");
        }
        if (typeof predicate !== "function") {
            throw new TypeError("predicate must be a function");
        }
        var list = Object(this);
        var length = list.length >>> 0;
        var thisArg = arguments[1];
        var value;

        for (var i = 0; i < length; i++) {
            value = list[i];
            if (predicate.call(thisArg, value, i, list)) {
                return value;
            }
        }
        return undefined;
    };
}

if (!Array.prototype.unique) {
    Array.prototype.unique = function (fn) {
        fn = fn || function (i) { return i; };
        var o = {}, i, l = this.length, result = [];
        for (i = 0; i < l; i += 1) {
            o[fn(this[i])] = this[i];
        }
        for (i in o) {
            result.push(o[i]);
        }
        return result;
    };
}