vantage.service("exportService", [
    "$window", "dataService", "authenticationService",
    function ($window, dataService, authenticationService) {
        this.exportCompetitionAdapters = function(competitionId) {
            return dataService.competitionExports(competitionId).query().$promise;
        }
        
        this.exportCompetition = function (competitionId, adapterName) {
            var accessToken = authenticationService.getBearerToken();
            $window.open("/api/competitions/" + competitionId + "/exports/" + adapterName + "?access_token=" + accessToken, "_blank");
        }

        this.exportLicensees = function (issuerId, discipline, category, adapterName) {
            var accessToken = authenticationService.getBearerToken();
            var uri = "/api/people/licenses/exports/" + adapterName;
            if (issuerId) {
                uri += "/" + issuerId;
                if (discipline) {
                    uri += "/" + discipline;
                    if (category) {
                        uri += "/" + category;
                    }
                }
            }
            $window.open(uri + "?access_token=" + accessToken, "_blank");
        }
    }
]);