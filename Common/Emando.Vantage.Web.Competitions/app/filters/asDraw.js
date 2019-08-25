vantage.filter("asDraw", [
    function() {
        return function (input, distance, fixedLanes) {
            input.sort(function (a, b) {
                return a.heat === b.heat ? a.lane - b.lane : a.heat - b.heat;
            });
            var result = [];
            if (input.length) {
                var lastHeat = input[input.length - 1].heat;
                for (var heat = distance.firstHeat; heat <= lastHeat; heat++) {
                    var heatRaces = input.filter(function (r) { return r.heat === heat; });
                    var lastLane = fixedLanes ? fixedLanes - 1 : heatRaces[heatRaces.length - 1].lane;
                    for (var lane = 0; lane <= lastLane; lane++) {
                        result.push(heatRaces.find(function (r) { return r.lane === lane; }) || {
                            id: heat + "." + lane,
                            heat: heat,
                            lane: lane
                        });
                    }
                }
            }
            return result;
        }
    }
]);