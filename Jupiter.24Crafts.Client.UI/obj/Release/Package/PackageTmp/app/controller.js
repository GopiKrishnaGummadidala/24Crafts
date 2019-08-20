'use strict';
app.controller('rootController', ['$scope', 'authService', '$state', function ($scope, authService, $state) {
  
    $scope.authentication = authService.authentication.isAuth;
    $scope.loginCntrl = function () {
        $state.go('login');
    }
    $scope.$on('logoutChange', function (event,value) {
        $scope.authentication = value;
    })
     $scope.logOut = function () {
         $scope.authentication = authService.logOut();
     }
     $scope.$on('validate', function (event, value) {
         $scope.authentication = value;
     });
     $scope.$on('dynamic-loading', function (event, value) {
         $scope.loading = value;
     });

     $scope.close = function () {
         $state.go('/');
     }
     var winHeight = $(window).height();
     $('#bl-main').css('height', winHeight - 52);

    //$scope.authentication = authService.authentication;
    //$location.path('/login');
}
]);
