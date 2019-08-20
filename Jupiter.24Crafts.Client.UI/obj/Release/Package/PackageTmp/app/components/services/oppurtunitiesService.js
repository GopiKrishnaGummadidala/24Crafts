app.factory('opportunitiesService', ['$http', '$q', 'ngAuthSettings',
    function ($http, $q, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;

        var httpDataObject = {};

        var config = {
            headers: {
                'Accept': 'application/json; charset=utf-8'
            }
        }

        var _opportunities = function (userId) {
            var deferred = $q.defer();
            $http.get(serviceBase + '/Opportunities/getOpportunities?userId=' + userId, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var _getMyAppliedOpportunities = function (userId) {
            var deferred = $q.defer();
            $http.get(serviceBase + '/Opportunities/getMyAppliedOpportunities?userId=' + userId, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var _getMyopportunities = function (userId) {
            var deferred = $q.defer();
            $http.get(serviceBase + '/Opportunities/getMyOpportunities?userId=' + userId, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var _fnAllProfessions = function (userId) {
            var deferred = $q.defer();
            $http.get(serviceBase + '/Portfolio/getAllProfessions?userId=' + userId, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var _fnSave = function (data) {
            var deferred = $q.defer();
            $http.post(serviceBase+'/Opportunities/createOpportunity', data, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var _fnUpdate = function (data) {
            var deferred = $q.defer();
            $http.post(serviceBase+'/Opportunities/updateOpportunity', data, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var _fnDelete = function (opportunityId) {
            var deferred = $q.defer();
            $http.get(serviceBase+'/Opportunities/DeleteOpportunity?opportunityId=' + opportunityId, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var _fnApplyOpportunity = function (data) {
            var deferred = $q.defer();
            $http.post(serviceBase + '/Opportunities/ApplyOpportunity', data, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

                    
        httpDataObject.getOpportunityData = _opportunities;
        httpDataObject.getMyOpportunityData = _getMyopportunities;
        httpDataObject.getMyAppliedOpportunityData = _getMyAppliedOpportunities;
        httpDataObject.Save = _fnSave;
        httpDataObject.Update = _fnUpdate;
        httpDataObject.Delete = _fnDelete;
        httpDataObject.getAllProfessions = _fnAllProfessions;
        httpDataObject.ApplyOpportunity = _fnApplyOpportunity;

        return httpDataObject;

    }]);