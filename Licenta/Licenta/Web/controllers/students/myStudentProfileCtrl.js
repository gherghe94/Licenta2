myApp.controller('myStudentProfileCtrl', ['$scope', 'studentProfileService', function ($scope, studentProfileService) {

    var resolveServerMessage = function (res) {
        $scope.successMessage = res;
        $scope.errorMessage = !res;
    }

    $scope.save = function () {
        var student = $scope.view.student;
        studentProfileService.save(student).success(function (data) {
            resolveServerMessage(data);
        }).error(function (data) {
            console.log(data);
        })
    };

    var init = function () {
    };

    init();
}]);