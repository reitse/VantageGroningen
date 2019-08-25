vantage.controller("alert", [
    "$scope", "$modalInstance", "header", "message",
    function($scope, $modalInstance, header, message) {
        $scope.header = header;
        $scope.message = message;
    }
]);

vantage.service("alertService", [
    "$modal", "$filter",
    function ($modal, $filter) {
        this.alert = function(message, header, template) {
            var instance = $modal.open({
                template: template ||
                    "<div class=\"modal-header\">" +
                    "   <h3 class=\"modal-title\">{{ header }}</h3>" +
                    "</div>" +
                    "<div class=\"modal-body\">" +
                    "   {{ message }}" +
                    "</div>" +
                    "<div class=\"modal-footer\">" +
                    "   <button class=\"btn btn-primary\" ng-click=\"$close()\">{{ \"Close\" | translate }}</button>" +
                    "</div>",
                controller: "alert",
                resolve: {
                    header: function() {
                        return header || $filter("translate")("Alert_Alert");
                    },
                    message: function() {
                        return message;
                    }
                }
            });
            return instance.result;
        };

        this.info = function(message) {
            return this.alert(message, $filter("translate")("Alert_Info"));
        };

        this.error = function(message) {
            return this.alert(message, $filter("translate")("Alert_Error"));
        };

        this.warn = function(message) {
            return this.alert(message, $filter("translate")("Alert_Warning"));
        };

        this.confirm = function(message) {
            return this.alert(message, $filter("translate")("Alert_Confirm"),
                "<div class=\"modal-header\">" +
                "   <h3 class=\"modal-title\">{{ header }}</h3>" +
                "</div>" +
                "<div class=\"modal-body\">" +
                "   {{ message }}" +
                "</div>" +
                "<div class=\"modal-footer\">" +
                "   <button class=\"btn btn-primary\" ng-click=\"$close()\">{{ \"Alert_Confirm_Yes\" | translate }}</button>" +
                "   <button class=\"btn btn-default\" ng-click=\"$dismiss()\">{{ \"Alert_Confirm_No\" | translate }}</button>" +
                "</div>");
        };
    }
]);