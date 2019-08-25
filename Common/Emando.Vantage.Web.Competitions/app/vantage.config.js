vantage.config([
    "$httpProvider", "$translateProvider", "resourcesProvider", "tmhDynamicLocaleProvider",
    function ($httpProvider, $translateProvider, resourcesProvider, tmhDynamicLocaleProvider) {
        "use strict";

        $httpProvider.interceptors.push("authenticationInterceptor");

        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get["Cache-Control"] = "no-cache";
        $httpProvider.defaults.headers.get["Pragma"] = "no-cache";

        resourcesProvider.push("Web");

        $translateProvider.fallbackLanguage("en");
        $translateProvider.determinePreferredLanguage();

        tmhDynamicLocaleProvider.localeLocationPattern("scripts/angular-locale_{{locale}}.js");
    }
]);