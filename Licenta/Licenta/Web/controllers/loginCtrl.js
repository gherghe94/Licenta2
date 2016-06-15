myApp.controller('LoginCtrl', ['$scope', '$location', 'indexService', 'authService', '$uibModal', function ($scope, $location, indexService, authService, $uibModal) {

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

    $scope.forgotPassword = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'login/forgotPasswordModal.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };

                $scope.retrievePassword = function () {
                    items.restService.recoverPassword($scope.user).success(function (data) {
                        $scope.successMessage = "The Password has been sent to your email!";
                    }).error(function (data) {
                        $scope.errors = data;
                    });
                };
            },
            resolve: {
                items: {
                    restService: indexService
                }
            }
        });
    };

}]);