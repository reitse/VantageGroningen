vantage.service("reportingService", [
    "$window",
    function($window) {
        this.downloadReport = function(competitionId, key, format) {
            $window.open("/api/competitions/" + competitionId + "/reports/" + key + "/" + format, "_blank");
        };

        this.downloadDistanceReport = function(competitionId, distanceId, key, format) {
            $window.open("/api/competitions/" + competitionId + "/distances/" + distanceId + "/reports/" + key + "/" + format, "_blank");
        };
    }
]);