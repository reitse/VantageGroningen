vantage.service("dataService", [
    "$resource", "Upload",
    function ($resource, $upload) {
        this.resources = function (culture, type) {
            return $resource("api/resources/:culture/:type/:key", { culture: culture, type: type, key: "@key" });
        }

        this.licenseIssuers = $resource("/api/licenseissuers");

        this.people = $resource("/api/people/:id", { id: "@id" });

        this.licenses = $resource("/api/people/licenses/:issuerId/:discipline/:key", { issuerId: "@issuerId", discipline: "@discipline", key: "@key" }, {
            'queryCached': {
                cache: true,
                isArray: true
            },
            'update': { method: "PUT" }
        });

        this.licensesExports = $resource("/api/people/licenses/exports/:adapterName/:issuerId/:discipline/:category", { adapterName: "@adapterName", issuerId: "@issuerId", discipline: "@discipline", category: "@category" });

        this.categories = $resource("/api/people/categories/:issuerId/:discipline", { issuerId: "@issuerId", discipline: "@discipline" });

        this.venues = $resource("/api/venues/:code/:discipline", { code: "@code", discipline: "@discipline" }, {
            'update': { method: "PUT" }
        });

        this.venueTracks = $resource("/api/venues/:code/:discipline/tracks", { code: "@code", discipline: "@discipline" });

        this.users = $resource("/api/users/:id", { id: "@id" }, {
            'update': { method: "PUT" },
            'changePassword': {
                method: "PUT",
                url: "/api/users/:id/password",
                params: { id: "@id" }
            },
            'roles': {
                url: "/api/users/roles",
                isArray: true
            }
        });

        this.transponders = $resource("/api/transponders/:type/:code", { type: "@type", code: "@code" }, {
            'convert': {
                method: "PUT",
                url: "/api/transponders/:type/:label",
                params: {
                    type: "@type",
                    label: "@label"
                }
            }
        });

        this.competitionSeries = $resource("/api/competitions/series/:id", { id: "@id" }, {
            'update': { method: "PUT" }
        });

        this.competitionSerieRegistrationSettings = function (serieId) {
            return $resource("/api/competitions/series/:id/registration/settings", { id: serieId }, {
                'update': { method: "PUT" }
            });
        };

        this.buildCompetitionSerieRegistrationUri = function (serieId) {
            return window.location.protocol + "//" + window.location.host + "/api/competitions/series/" + serieId + "/registration/forward/competitorRegistration";
        };

        this.competitions = $resource("/api/competitions/:id", { id: "@id" }, {
            'update': { method: "PUT" },
            'validLicense': {
                url: "/api/competitions/:id/validLicense/:personId",
                params: {
                    id: "@id",
                    personId: "@personId"
                }
            },
            'clone': {
                method: "PUT",
                url: "/api/competitions/:id/clone",
                params: {
                    id: "@id"
                }
            }
        });

        this.competitionRegistrations = function(competitionId) {
            return $resource("/api/competitions/:id/registrations", { id: competitionId }, {
                'invitations': {
                    method: "POST",
                    url: "/api/competitions/:id/registrations/invitations",
                    isArray: true
                }
            });
        }

        this.registrationStatistics = $resource("/api/competitions/statistics/registrations", {}, {
            'serie': {
                url: "/api/competitions/series/:id/statistics/registrations",
                params: { id: "@id" }
            },
            'competition': {
                url: "/api/competitions/:id/statistics/registrations",
                params: { id: "@id" }
            }
        });

        this.competitionExports = function (competitionId) {
            return $resource("/api/competitions/:id/exports/:adapterName", { id: competitionId, adapterName: "@adapterName" });
        };

        this.competitionReports = function(competitionId) {
            return $resource("/api/competitions/:id/reports", { id: competitionId });
        }

        this.competitionRegistrationSettings = function(competitionId) {
            return $resource("/api/competitions/:id/registration/settings", { id: competitionId }, {
                'update': { method: "PUT" }
            });
        };

        this.buildCompetitionRegistrationUri = function(competitionId) {
            return window.location.protocol + "//" + window.location.host + "/api/competitions/" + competitionId + "/registration/forward/competitorRegistration";
        };

        this.buildCompetitionCompetitorListUri = function (competitionId) {
            return window.location.protocol + "//" + window.location.host + "/api/competitions/" + competitionId + "/registration/forward/competitorList";
        };

        this.personCompetitorLists = function (competitionId) {
            return $resource("/api/competitions/:competitionId/competitors/personlists/:id", { competitionId: competitionId, id: "@id" }, {
                'update': { method: "PUT" },
                'adapters': {
                    url: "/api/competitions/:competitionId/competitors/personlists/adapters",
                    params: {
                        competitionId: competitionId
                    },
                    isArray: true
                },
                'renumber': {
                    method: "PUT",
                    url: "/api/competitions/:competitionId/competitors/personlists/:id/renumber/:from/:add"
                }
            });
        };

        this.personCompetitorList = function(competitionId, listId) {
            return $resource("/api/competitions/:competitionId/competitors/personlist/:listId",
            {
                competitionId: competitionId,
                listId: listId
            });
        };

        this.teamCompetitorLists = function(competitionId) {
            return $resource("/api/competitions/:competitionId/competitors/teamlists/:id", { competitionId: competitionId, id: "@id" }, {
                'update': { method: "PUT" },
                'renumber': {
                    method: "PUT",
                    url: "/api/competitions/:competitionId/competitors/teamlists/:id/renumber/:from/:add"
                }
            });
        };

        this.teamCompetitorList = function(competitionId, listId) {
            return $resource("/api/competitions/:competitionId/competitors/teamlist/:listId",
            {
                competitionId: competitionId,
                listId: listId
            });
        };

        this.competitors = function(competitionId) {
            return $resource("/api/competitions/:competitionId/competitors/:id",
            {
                competitionId: competitionId,
                id: "@id"
            },
            {
                'updatePerson': {
                    method: "PUT",
                    url: "/api/competitions/:competitionId/competitors/:id/person",
                    params: {
                        competitionId: competitionId,
                        id: "@id"
                    }
                },
                'updateTeam': {
                    method: "PUT",
                    url: "/api/competitions/:competitionId/competitors/:id/team",
                    params: {
                        competitionId: competitionId,
                        id: "@id"
                    }
                }
            });
        };

        this.teamCompetitorMembers = function (competitionId, id) {
            return $resource("/api/competitions/:competitionId/competitors/:id/members",
            {
                competitionId: competitionId,
                id: id
            },
            {
                'update': { method: "PUT" }
            });
        };

        this.loadPersonCompetitors = function(competitionId, listId, adapter, files) {
            return $upload.upload({
                url: "/api/competitions/" + competitionId + "/competitors/personlist/" + listId + "/" + adapter,
                file: files
            });
        };

        this.distances = function(competitionId) {
            return $resource("/api/competitions/:competitionId/distances/:id",
            {
                competitionId: competitionId,
                id: "@id"
            },
            {
                'update': { method: "PUT" },
                'valid': {
                    url: "/api/competitions/:competitionId/distances/valid/:discipline",
                    params: {
                        competitionId: competitionId,
                        discipline: "@discipline"
                    },
                    isArray: true
                },
                'combinationsCompetitors': {
                    url: "/api/competitions/:competitionId/distances/:id/combinations/competitors",
                    params: {
                        competitionId: competitionId,
                        id: "@id"
                    },
                    isArray: true
                },
                'renumber': {
                    url: "/api/competitions/:competitionId/distances/:id/rounds/:round/renumber",
                    method: "PUT",
                    params: {
                        competitionId: competitionId,
                        round: "@round",
                        id: "@id"
                    }
                },
                'transponderAdapters': {
                    url: "/api/competitions/:competitionId/distances/:id/transponders/adapters",
                    params: {
                        competitionId: competitionId,
                        id: "@id"
                    },
                    isArray: true
                }
            });
        };

        this.loadDistanceTransponders = function(competitionId, distanceId, adapter, files) {
            return $upload.upload({
                url: "/api/competitions/ " + competitionId + "/distances/" + distanceId + "/transponders/" + adapter,
                file: files
            });
        };

        this.distanceCombinations = function(competitionId) {
            return $resource("/api/competitions/:competitionId/distancecombinations/:id",
            {
                competitionId: competitionId,
                id: "@id"
            },
            {
                'update': { method: "PUT" }
            });
        };

        this.distanceCombinationCompetitors = function(competitionId, combinationId) {
            return $resource("/api/competitions/:competitionId/distancecombination/:combinationId/competitors/:id",
            {
                competitionId: competitionId,
                combinationId: combinationId,
                id: "@id"
            },
            {
                'update': { method: "PUT" }
            });
        };

        this.competitorDistanceCombinations = function(competitionId, competitorId) {
            return $resource("/api/competitions/:competitionId/competitors/:competitorId/distancecombinations",
            {
                competitionId: competitionId,
                competitorId: competitorId
            },
            {
                'update': { method: "PUT" }
            });
        };

        this.competitorRaces = function(competitionId, id) {
            return $resource("/api/competitions/:competitionId/competitors/:id/races", {
                competitionId: competitionId,
                id: id
            });
        };

        this.races = function(competitionId, distanceId) {
            return $resource("/api/competitions/:competitionId/distance/:distanceId/races/:id",
            {
                competitionId: competitionId,
                distanceId: distanceId,
                id: "@id"
            },
            {
                'addToHeat': {
                    url: "/api/competitions/:competitionId/distance/:distanceId/races/rounds/:round/heats/:heat/:assignLanes",
                    method: "POST",
                    params: {
                        competitionId: competitionId,
                        distanceId: distanceId,
                        round: "@round",
                        heat: "@heat",
                        assignLanes: "@assignLanes"
                    },
                    isArray: true
                },
                'updateHeat': {
                    url: "/api/competitions/:competitionId/distance/:distanceId/races/rounds/:round/heats/:heat",
                    method: "PUT",
                    params: {
                        competitionId: competitionId,
                        distanceId: distanceId,
                        round: "@round",
                        heat: "@heat"
                    },
                    isArray: true
                },
                'deleteHeat': {
                    url: "/api/competitions/:competitionId/distance/:distanceId/races/rounds/:round/heats/:heat",
                    method: "DELETE",
                    params: {
                        competitionId: competitionId,
                        distanceId: distanceId,
                        round: "@round",
                        heat: "@heat"
                    },
                    isArray: true
                }
            });
        };

        this.race = function(competitionId, id) {
            return $resource("/api/competitions/:competitionId/race/:id", {
                competitionId: competitionId,
                id: id
            },
            {
                'presentUserResult': {
                    method: "PUT",
                    url: "/api/competitions/:competitionId/race/:id/present/User",
                    params: {
                        competitionId: competitionId,
                        id: id
                    }
                },
                'presentInstanceResult': {
                    method: "PUT",
                    url: "/api/competitions/:competitionId/race/:id/present/:instanceName",
                    params: {
                        competitionId: competitionId,
                        id: id,
                        instanceName: "@instanceName"
                    }
                },
                'saveTransponders': {
                    method: "PUT",
                    url: "/api/competitions/:competitionId/race/:id/transponders",
                    params: {
                        competitionId: competitionId,
                        id: id
                    }
                }
            });
        };
    }
]);