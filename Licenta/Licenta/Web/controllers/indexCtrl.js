myApp.controller('IndexCtrl', ['$scope', 'indexService', '$location', '$rootScope', 'authService', function ($scope, indexService, $location, $rootScope, authService) {
    $scope.greeting = 'Hola!';
    $scope.showModal = false;

    $rootScope.$on('badRequest', function (msg, data) {
        $scope.showModal = !$scope.showModal;
        $scope.errorMessage = "Credentiale invalide!";
    });

    $scope.showLogOut = function () {
        return authService.authentication.isAuth;
    };

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/login');
    };

    $scope.goHome = function () {
        if (!$scope.showLogOut()) {
            $location.path('/login');
        } else {
            var type = authService.authentication.userType;
            if (type == "Admin") {
                $location.path('/mainAdmin');
            } else if (type == "Student") {
                $location.path('/mainStudent');
            } else {
                $location.path('/mainTeacher');
            }
        }
    };

}]);