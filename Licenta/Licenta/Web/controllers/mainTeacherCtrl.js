﻿myApp.controller('MainTeacherCtrl', ['$scope', 'authService', 'mainTeacherService', function ($scope, authService, mainTeacherService) {

    $scope.changeTab = function (tab) {
        $scope.currentTab = tab;
    };

    var initView = function () {
        $scope.currentTab = {};
        $scope.view = {
            teacher: {}
        };
    };

    var initTeacher = function () {
        var auth = authService.authentication;
        mainTeacherService.getLoggedTeacher(auth.userName, auth.userType).success(function (data) {
            $scope.view.teacher = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    var initBarItems = function () {
        mainTeacherService.getBarItems().success(function (data) {
            $scope.tabs = data;
        }).error(function (reason) {
            console.log(reason);
        });
    };

    var init = function () {
        initView();
        initTeacher();
        initBarItems();
    };

    init();

}]);