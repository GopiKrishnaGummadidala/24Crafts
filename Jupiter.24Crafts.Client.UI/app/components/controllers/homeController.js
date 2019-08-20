app.controller('homeController', ['$scope', '$state', 'authService',
    function ($scope, $state, authService) {
        $scope.logOut = function () {
            authService.logOut();         
        };
        authService.logOut();
        //$scope.largeArcFlag = "asasas";
        //$scope.authentication = authService.authentication;
        //$state.path('login');
    }]);