myApp.controller('MainStudentCtrl', ['$scope', 'authService', 'mainStudentService', function ($scope, authService, mainStudentService) {
    $scope.changeTab = function (tab) {
        $scope.currentTab = tab;
    };

    var initView = function () {
        $scope.currentTab = {};
        $scope.view = {
            student: {}
        };
    };

    var initStudent = function () {
        var auth = authService.authentication;
        mainStudentService.getLoggedStudent(auth.userName, auth.userType).success(function (data) {
            $scope.view.student = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    var initBarItems = function () {
        mainStudentService.getBarItems().success(function (data) {
            $scope.tabs = data;
        }).error(function (reason) {
            console.log(reason);
        });
    };

    var init = function () {
        initView();
        initStudent();
        initBarItems();
    };

    init();

}]);