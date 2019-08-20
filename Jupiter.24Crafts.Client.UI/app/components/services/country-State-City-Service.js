'use strict';

app.factory('countryStateCityService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
    function ($http, $q, localStorageService, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var countryStateCityServiceFactory = {};
        var config = {
            headers: {
                'Accept': 'application/json; charset=utf-8'
            }
        };
        var _getCountries = function () {
            var deferred = $q.defer();
            $http.get(serviceBase + 'Common/getCountries', config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };

        var _getStatesByCountryId = function (countryId) {
            var deferred = $q.defer();
            $http.get(serviceBase + 'Common/getStates?countryId=' + countryId, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };

        var _getCitiesByStateId = function (stateId) {
            var deferred = $q.defer();
            $http.get(serviceBase + 'Common/getCities?stateId=' + stateId, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };
        countryStateCityServiceFactory.getCountries = _getCountries;
        countryStateCityServiceFactory.getStatesByCountryId = _getStatesByCountryId;
        countryStateCityServiceFactory.getCitiesByStateId = _getCitiesByStateId;
        return countryStateCityServiceFactory;
    }]);