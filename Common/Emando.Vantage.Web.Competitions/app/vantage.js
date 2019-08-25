var vantage = angular.module("vantage", ["ngFileUpload", "pascalprecht.translate", "tmh.dynamicLocale"]);

vantage.run([
    "$rootScope", "$state", "$stateParams", "authenticationService", "reportingService", "exportService", "countries", "cultures", "tmhDynamicLocale",
    function ($rootScope, $state, $stateParams, authenticationService, reportingService, exportService, countries, cultures, tmhDynamicLocale) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        $rootScope.authenticationService = authenticationService;
        $rootScope.reportingService = reportingService;
        $rootScope.exportService = exportService;
        $rootScope.countries = countries;
        $rootScope.cultures = cultures;

        $rootScope.$on("$stateChangeError", function (e, to, toParams, from, fromParams, error) {
            if (error && error.name === "NotAuthorized") {
                $state.go("login");
                e.preventDefault();
            }
        });

        $rootScope.$on("$translateChangeSuccess", function (e, args) {
            var culture = args.language.split("_");
            moment.locale(culture[0]);
            tmhDynamicLocale.set(culture.join("-"));
        });

        $rootScope.logout = function () {
            authenticationService.logout();
            $state.go("home");
        };

        $rootScope.toggleFlag = function (value, flag) {
            return value ^ flag;
        };

        authenticationService.loadLogin();
    }
]);