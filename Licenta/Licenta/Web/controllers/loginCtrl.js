myApp.controller('LoginCtrl', ['$scope', '$location', 'indexService', 'authService', function ($scope, $location, indexService, authService) {

    $scope.loginData = {};

    $scope.changeView = function (view) {
        $location.path(view);
    }

    var redirectToSpecificPage = function (response) {
        if (response.TypeToken == "Student") {
            $location.path('/mainStudent');
        } else if (response.TypeToken == "Teacher") {
            $location.path('/mainTeacher');
        } else if (response.TypeToken == "Admin") {
            $location.path('/mainAdmin');
        } else {
            $location.path('/about');
        }
    }


    $scope.login = function () {
        authService.login($scope.loginData).then(function (response) {
            redirectToSpecificPage(response);
        },
         function (err) {
         });
    };

    $scope.testUnathorized = function () {
        indexService.testSecureWebApi().success(function (data) {
            console.log(data);
        }).error(function (reason) {
            console.log(reason);
        });
    }

}]);