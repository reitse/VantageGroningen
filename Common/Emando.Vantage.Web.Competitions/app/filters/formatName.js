vantage.filter("formatName", function () {
    return function(input) {
        if (input == null) {
            return null;
        }

        if (typeof input === "string") {
            return input;
        }

        if (typeof input === "object") {
            var name = input.firstName || "";
            if (input.surnamePrefix) {
                name += " " + (input.surnamePrefix || "");
            }
            name += " " + (input.surname || "");
            return name;
        }

        return null;
    };
});