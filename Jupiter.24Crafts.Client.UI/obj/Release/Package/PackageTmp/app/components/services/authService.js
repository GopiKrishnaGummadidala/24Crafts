'use strict';

app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', '$base64', '$state', 'notificationService',
function ($http, $q, localStorageService, ngAuthSettings, $base64, $state, notificationService) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

    var config = {
        headers: {
            'Accept': 'application/json; charset=utf-8'
        }
    }

    //function declartions
    var _authentication = {
        isAuth: false,
        userName: '',
        menus: '',
        userId:'',
        businessUserId: '',
        customerId:'',
    };
    var _login = function (loginData,typeOfLogin) {
        var data = 'Basic ' + $base64.encode(loginData.userName + ':' + loginData.password);
        var deferred = $q.defer();
        $http.post(serviceBase + 'accounts/login',
            null,
            {
                headers:
                  {
                      'Content-Type': 'application/json',
                      'Authorization': data,
                      'LoginType': typeOfLogin
                  }
            }).success(function (data, status, headers, config) {
                debugger;
                _authentication.menus = data.Response['Menu'];
                if (loginData.useRefreshTokens) {
                    localStorageService.set('authorizationData', {
                        token: data.Response.TokenData['TokenKey'],
                        userName: data.Response['FirstName'] + " " + data.Response['LastName'],
                        menus: data.Response['Menu'],
                        userID: data.Response['UserID'],
                        businessUserId: angular.isUndefined(data.Response.BusinessObj) ? null : data.Response.BusinessObj['BusinessUserId'],
                        customerId: data.Response['CustomerId']
                    });
                }
                else {
                    localStorageService.set('authorizationData', {
                        token: data.Response.TokenData['TokenKey'],
                        userName: data.Response['FirstName'] + " " + data.Response['LastName'],
                        menus: data.Response['Menu'],
                        userID: data.Response['UserID'],
                        businessUserId: angular.isUndefined(data.Response.BusinessObj) ? null : data.Response.BusinessObj['BusinessUserId'],
                        customerId: data.Response['CustomerId']
                    });
                }
                //_authentication.isAuth = true;
                //_authentication.userName = data.Response['FirstName'];
                _fillAuthData();
                $state.go('/');
                notificationService.displaySuccess('Hello ' + data.Response['FirstName'] + " " + data.Response['LastName'] + '!');
                deferred.resolve(data);
            }).error(function (err, status) {
                notificationService.displayError('Login failed. Try again.');
                $state.go('login');
                deferred.reject(err);
            });
        return deferred.promise;
    };
    var _register = function(registerData) {
        var deferred = $q.defer();
        $http.post(serviceBase + 'accounts/register',
            registerData,
            {
                headers:
                  {
                      'Content-Type': 'application/json'
                  }
            }).success(function (data, status, headers, config) {
                notificationService.displaySuccess('Registration success please login.');
                $('.toggle').click();
                deferred.resolve(data);
            }).error(function (err, status) {
                notificationService.displayError('Registration failed. Try again.');
                deferred.reject(err);
            });
        return deferred.promise;
    };
    var _logOut = function () {
        localStorageService.remove('authorizationData');
        _authentication.isAuth = false;
        _authentication.userName = '';
        _authentication.menus = '';
        _authentication.userId = '';
        _authentication.businessUserId = '';
        _authentication.customerId = '';
        $state.go('login');
        notificationService.displaySuccess("Succesfully Logout!");
        return _authentication.isAuth;
    };
    var _fillAuthData = function () {
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.menus = authData.menus;
            _authentication.userId = authData.userID;
            _authentication.businessUserId = authData.businessUserId;
            _authentication.customerId = authData.customerId;
        }
    };

    var _validateProfile = function (mobileNum, profileType) {
        var deferred = $q.defer();
        $http.get('API/Portfolio/Validateprofile?mobileNum=' + mobileNum + '&profileType=' + profileType, config).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }

    var _statusOTP = function (mobileNum, otp) {
        var deferred = $q.defer();
        $http.get(serviceBase + '/common/otp?mobileNum=' + mobileNum + '&otp=' + otp, config).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }

    var _loginSendOTP = function (mobileNum) {
        var deferred = $q.defer();
        $http.get(serviceBase + '/common/LoginSendOTP?mobileNum=' + mobileNum, config).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }

    var _submitForgetPwdSendMail = function (email) {
        var deferred = $q.defer();
        $http.get(serviceBase + '/common/ForgetPasswordEmail?email=' + email, config).success(deferred.resolve).error(deferred.reject);
        return deferred.promise;
    }

    //function return statement declartions
    authServiceFactory.authentication = _authentication;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.register = _register;
    authServiceFactory.validateProfile = _validateProfile;
    authServiceFactory.statusOTP = _statusOTP;
    authServiceFactory.loginSendOTP = _loginSendOTP;
    authServiceFactory.submitForgetPwdSendMail = _submitForgetPwdSendMail;

    return authServiceFactory;
}]);