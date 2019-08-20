app.factory('portfolioService', ['$http', '$q', 'ngAuthSettings',
    function ($http, $q, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;

        var httpDataObject = {};

        var config = {
            headers: {
                'Accept': 'application/json; charset=utf-8'
            }
        }

        var _fnGetProfileDataByUserId = function (userId) {
            var deferred = $q.defer();
            $http.get('api/Portfolio/getProfileByUserId?userId=' + userId, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };
        var _fnCreateProfile = function (input) {
            var deferred = $q.defer();
            $http.post('api/Portfolio/createPortfolio',input,config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };
       var _fnDeleteImageByPath = function (input) {
            var deferred = $q.defer();
            $http.get('api/Portfolio/deleteFiles?filePaths='+ input, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };
        var _fnUploadFiles = function (input) {
            var deferred = $q.defer();
            $http.post('api/Portfolio/uploadFiles', input, {
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };
        var _fnBusinessCreation = function (input) {
            var deferred = $q.defer();
            $http.post('api/Portfolio/createBusinessProfie', input, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };
       
        httpDataObject.GetProfileDataByUserId = _fnGetProfileDataByUserId;
        httpDataObject.CreateProfile = _fnCreateProfile;
        httpDataObject.CreateBusiness = _fnBusinessCreation;
        httpDataObject.UploadFiles = _fnUploadFiles;
        httpDataObject.DeleteImageByPath = _fnDeleteImageByPath
        return httpDataObject;

    }]);