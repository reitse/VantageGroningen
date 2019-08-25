vantage.service("competitionEventsService", [
    function () {
        this.connection = null;
        
        this.start = function(scope, filter) {
            this.connection = $.hubConnection();
            this.connection.logging = true;
            var proxy = this.connection.createHubProxy("competitionEventsHub");
            proxy.on("handleEvent", function (e) {
                if (typeof (filter) !== "function" || filter(e)) {
                    scope.$broadcast("competitionEvent", e);
                }
            });
            this.connection.start();
        };

        this.stop = function() {
            this.connection.stop();
        };
    }
]);