myApp.controller('myProfileCtrl', ['$scope', 'teacherProfileService', function ($scope, teacherProfileService) {

    var resolveServerMessage = function (res) {
        $scope.successMessage = res;
        $scope.errorMessage = !res;
    }

    $scope.save = function () {
        var teacher = $scope.view.teacher;
        teacherProfileService.save(teacher).success(function (data) {
            resolveServerMessage(data);
        }).error(function (data) {
            console.log(data);
        })
    };

    var initCourses = function () {
        teacherProfileService.getCourses($scope.view.teacher.Id).success(function (data) {
            $scope.view.teachedCourses = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    var init = function () {
        initCourses();
    };

    init();
}]);