'use strict';
app.controller('loginController', ['$scope', '$rootScope', '$state', '$location', 'authService', 'notificationService',
function ($scope, $rootScope, $state, $location, authService, notificationService) {
    if (authService.authentication.isAuth) {
        $state.go('/');
    }

    $scope.phoneNumberPattern = (function () {
        var regexp = /^\(?(\d{3})\)?[ .-]?(\d{3})[ .-]?(\d{4})$/;
        return {
            test: function (value) {
                if ($scope.requireTel === false) {
                    return true;
                }
                return regexp.test(value);
            }
        };
    })();

    $scope.isValidProfileSuccess = false;
    $scope.isValidProfileFail = false;
    $scope.isOtpValidSuccess = false;
    $scope.isOtpValidFailure = false;
    $scope.showforgetPassword = false;
    $scope.viewLoginOTP = false;
    $scope.validateLogin = true;
    $scope.userForm = {};
    $scope.registerForm = {};
    $scope.forgetpasswordForm = {};
    $scope.loginOTPdata = {};
    $scope.loginData = {
        userName: "",
        password: ""
    };
    $scope.otpData = {
        otpnumber: "",
        loginotp:""
    };
    $scope.registerData=
    {
        firstName:'',
        lastName: '',
        email: '',
        phoneno: '',
        profileType: '',
        password_c: '',
        password: '',
        otp:''
    }

    $scope.forgetData = {
        forgetmailid: ''
    }

    $scope.loginOTPdata = {
        otpnumber: '',
        loginotp :''
    }
    // Toggle Function
   
    $scope.validateProfile = function(phoneno, profileType)
    {
        authService.validateProfile($scope.registerData.phoneno, $scope.registerData.profileType).then(function (res) {
            if (res.Response) {
                $scope.isValidProfileSuccess = true;
                $scope.isValidProfileFail = false;
            }
            else {
                $scope.isValidProfileFail = true;
                $scope.isValidProfileSuccess = false;
            }
        },
        function (reason) {
            console.log('ERROR' + reason);
        });
    }
   
    $scope.sendotptoNumber = function () {
        if ($scope.registerData.phoneno) {
            authService.statusOTP($scope.registerData.phoneno, '').then(function (res) {
                if (res.Response == 'OTP sent successfully') {
                    notificationService.displaySuccess(res.Response);
                    
                } else {
                    notificationService.displayError(res.Response);
                }
            },
            function (reason) {
                console.log('ERROR' + reason);
            });
        }
    }

    $scope.sendLoginotptoNumber = function () {
        if ($scope.otpData.otpnumber != '') {
            authService.loginSendOTP($scope.otpData.otpnumber).then(function (res) {
            },
            function (reason) {
                console.log('ERROR' + reason);
            });
        }
    }

    $scope.OTPLogin = function () {
        if ($scope.otpData.otpnumber != '' && $scope.loginotpForm.loginotp != '') {
            $scope.$emit('dynamic-loading', true);
            authService.login({ userName: $scope.otpData.otpnumber, password: $scope.otpData.loginotp }, 'OTP')
             .then(function (result) {
                 $scope.$emit('validate', authService.authentication.isAuth);
                 $scope.$emit('dynamic-loading', false);
                 $scope.loginData.userName = '';
                 $scope.loginData.password = '';
                 $('#loginModal').modal('hide');
             },
                function (reason) {
                    $scope.$emit('dynamic-loading', false);
                    console.log('Error' + reason);
                });
        }
    }
    $scope.$watch('registerData.otp', function (value) {
        var otpValue = value;
    });

    $scope.login = function () {
        if ($scope.userForm.$valid) {
            $scope.$emit('dynamic-loading', true);
            authService.login($scope.loginData,'Normal')
            .then(function (result) {
                $scope.$emit('validate', authService.authentication.isAuth);
                $scope.$emit('dynamic-loading', false);
                $scope.loginData.userName = '';
                $scope.loginData.password = '';
                $('#loginModal').modal('hide');             
            },
               function (reason) {
                   $scope.$emit('dynamic-loading', false);
                   console.log('Error' + reason);
               });
        }
    }

    $scope.registerSubmit = function () {
        if ($scope.registerForm.$valid && $scope.isValidProfileSuccess) {
            $scope.$emit('dynamic-loading', true);
            authService.statusOTP($scope.registerData.phoneno, $scope.registerData.otp).then(function (res) {
                if (res.Response == '1') {
                    $scope.isOtpValidSuccess = true;
                    $scope.isOtpValidFailure = false;
                    var inputData = {
                        FirstName: $scope.registerData.firstName,
                        LastName: $scope.registerData.lastName,
                        Password: $scope.registerData.password,
                        EmailID: $scope.registerData.email,
                        PhoneNum: $scope.registerData.phoneno,
                        ProfileType: $scope.registerData.profileType
                    };
                    authService.register(inputData)
                     .then(function (result) {
                           $scope.$emit('dynamic-loading', false);
                         $('#loginModal').modal('hide');
                         $scope.closePopUp();
                     },
                       function (reason) {
                           $scope.$emit('dynamic-loading', false);
                           console.log('Error' + reason);
                       });
                }
                else {
                    $scope.isOtpValidSuccess = false;
                    $scope.isOtpValidFailure = true;
                    $scope.$emit('dynamic-loading', false);
                    notificationService.displayError(res.Response);
                }
            },
            function (reason) {
                console.log('ERROR' + reason);
            });
        }
    }

    $scope.closePopUp = function () {
        $scope.registerForm.$setPristine();
        $scope.registerForm.$setUntouched();
        $scope.userForm.$setPristine();
        $scope.userForm.$setUntouched();
        $scope.registerData.firstName = '';
        $scope.registerData.lastName = '';
        $scope.registerData.email = '';
        $scope.registerData.phoneno = '';
        $scope.registerData.profileType = '';
        $scope.registerData.password_c = '';
        $scope.registerData.password = '';
        $scope.registerData.otp = '';
        $scope.isValidProfileSuccess = false;
        $scope.isValidProfileFail = false;
        $scope.loginData.userName = '';
        $scope.loginData.password = '';
        $scope.loginData.userName = '';
        $scope.isOtpValidSuccess = false;
        $scope.isOtpValidFailure = false;
        $scope.forgetData.mail = '';
        $scope.loginotpForm.loginotp = 0;
        $scope.forgetData.forgetmailid = '';
    }

    $scope.forgetPassword = function () {
        $scope.showforgetPassword = true;
        $scope.viewLoginOTP = false;
        $scope.validateLogin = false;
    }
    
    $scope.submitforgetPassword = function () {
        if ($scope.forgetData.forgetmailid != '') {
            $scope.$emit('dynamic-loading', true);
            authService.submitForgetPwdSendMail($scope.forgetData.forgetmailid)
            .then(function (result) {
                $scope.$emit('dynamic-loading', false);
                if (result.Response)
                {
                    alert('Please check your email.. Email sent successfully !!')
                }
                $('#loginModal').modal('hide');
            },
               function (reason) {
                   $scope.$emit('dynamic-loading', false);
                   console.log('Error' + reason);
               });
        }
    }

    $scope.loginwithOTP = function () {
        $scope.viewLoginOTP = true;
        $scope.showforgetPassword = false;
        $scope.validateLogin = false;
    }

    $scope.loginOtpSubmit = function () {

    }
    $scope.showLoginForm = function () {
        $scope.validateLogin = true;
        $scope.viewLoginOTP = false;
        $scope.showforgetPassword = false;
    }


}]);
app.directive('validPasswordC', function () {
    return {
        require: 'ngModel',
        scope: {

            reference: '=validPasswordC'

        },
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue, $scope) {

                var noMatch = viewValue != scope.reference;
                ctrl.$setValidity('noMatch', !noMatch);
                return (noMatch) ? noMatch : !noMatch;
            });

            scope.$watch("reference", function (value) {;
                ctrl.$setValidity('noMatch', value === ctrl.$viewValue);

            });
        }
    }
});


