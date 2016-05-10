myApp.controller('MainAdminCtrl', ['$scope', 'mainAdminService', function ($scope, mainAdminService) {
    $scope.adminName = 'Admin';
    $scope.currentTab = {};

    $scope.changeTab = function (tab) {
        $scope.currentTab = tab;
    };

    var init = function () {
        mainAdminService.getBarItems().success(function (data) {
            $scope.tabs = data;
        }).error(function (reason) {
            console.log(reason);
        });
    };

    init();

}]);