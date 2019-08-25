vantage.service("authorizationService", [
    "authenticationService",
    function(authenticationService) {
        function notAuthorized(message) {
            return { name: "NotAuthorized", message: message };
        }

        this.throwIfNotAuthorized = function() {
            if (!authenticationService.isAuthenticated()) {
                throw new notAuthorized("Not authorized.");
            }
        };

        this.isInRole = function (role) {
            var user = authenticationService.getUser();
            return user && !!user.roles.find(function(r) { return r === role; });
        }
    }
]);