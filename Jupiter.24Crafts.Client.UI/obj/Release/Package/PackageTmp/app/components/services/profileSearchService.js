app.factory('profileSearchService', ['$http', '$q', 'ngAuthSettings',
    function ($http, $q, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;

        var httpDataObject = {};

        var config = {
            headers: {
                'Accept': 'application/json; charset=utf-8'
            }
        }

        var _profiles = function (searchString) {
            var deferred = $q.defer();
            $http.get('api/Portfolio/searchProfiles?Name=' + searchString, config).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }


        httpDataObject.getProfilesData = _profiles;
        
        return httpDataObject;

    }]);