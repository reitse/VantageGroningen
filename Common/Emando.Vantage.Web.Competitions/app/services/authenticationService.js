vantage.service("authenticationService", [
    "$http", "$q",
    function ($http, $q) {
        this.isLoggingIn = false;
        this.user = null;
        this.refreshTokenPromise = null;

        this.loadLogin = function() {
            var data = localStorage ? angular.fromJson(localStorage.authData) : null;
            if (data) {
                if (moment(data.expires).isAfter(moment())) {
                    this.user = data;
                    var deferred = $q.defer();
                    deferred.resolve();
                    return deferred.promise;
                }

                this.isLoggingIn = true;
                var self = this;
                return this.refreshToken(data).finally(function() {
                    self.isLoggingIn = false;
                });
            }
            return $q.reject();
        };

        this.getUser = function() {
            return this.user;
        }

        this.getBearerToken = function() {
            return this.user ? this.user.bearerToken : undefined;
        }

        this.setUser = function(userName, givenName, roles, bearerToken, refreshToken, expires, persist) {
            this.user = {
                userName: userName,
                givenName: givenName,
                roles: roles,
                bearerToken: bearerToken,
                expires: expires,
                refreshToken: refreshToken
            };
            $http.defaults.headers.common.Authorization = "bearer " + bearerToken;
            if (localStorage) {
                if (persist) {
                    localStorage.authData = angular.toJson(this.user);
                } else {
                    localStorage.authData = null;
                }
            }
        };

        this.isAuthenticated = function() {
            return !!this.getUser();
        };

        this.getToken = function(request, persist) {
            var config = {
                method: "POST",
                url: "/api/token",
                headers: {
                    'Content-Type': "application/x-www-form-urlencoded",
                },
                data: request
            };

            var deferred = $q.defer();
            var self = this;
            $http(config)
                .success(function(response) {
                    self.setUser(response.username, response.givenname, response.roles.split(", "), response.access_token, response.refresh_token, response[".expires"], persist);
                    deferred.resolve(response.access_token);
                })
                .error(function (response) {
                    self.logout();
                    if (response.error_description) {
                        deferred.reject(response.error_description);
                    } else {
                        deferred.reject("Unable to contact server; please try again later.");
                    }
                });

            return deferred.promise;
        };

        this.login = function(userName, password, persist) {
            var request = "grant_type=password" +
                "&username=" + encodeURIComponent(userName) +
                "&password=" + encodeURIComponent(password) +
                "&client_id=Manager";
            return this.getToken(request, persist);
        };

        this.refreshToken = function (data) {
            data = data || this.user;
            if (!data) {
                return $q.reject("No refresh token available.");
            }

            if (!this.refreshTokenPromise) {
                var request = "grant_type=refresh_token" +
                    "&refresh_token=" + encodeURIComponent(data.refreshToken) +
                    "&client_id=Manager";
                var self = this;
                this.refreshTokenPromise = this.getToken(request, true).finally(function() {
                    self.refreshTokenPromise = null;
                });
            }
            return this.refreshTokenPromise;
        };

        this.logout = function() {
            this.user = null;
            $http.defaults.headers.common.Authorization = null;
            if (localStorage) {
                localStorage.authData = null;
            }
        };
    }
]);

vantage.factory("authenticationInterceptor", [
    "$q", "$injector",
    function($q, $injector) {
        return {
            'responseError':
                function(response) {
                    if (response.status === 401 && !response.config.isRetry) {
                        var deferred = $q.defer();
                        $injector.get("authenticationService").refreshToken().then(function (token) {
                            response.config.isRetry = true;
                            response.config.headers.Authorization = "bearer " + token;
                            $injector.get("$http")(response.config).then(function(r) {
                                deferred.resolve(r);
                            }, function() {
                                deferred.reject(response);
                            });
                        }, function() {
                            deferred.reject(response);
                            $injector.get("$state").go("login");
                        });
                        return deferred.promise;
                    }
                    throw response;
                }
        };
    }
]);