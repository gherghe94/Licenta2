myApp.controller('addStudentCtrl', ['$scope', 'studentService', 'groupService', function ($scope, studentService, groupService) {
    $scope.student = {};

    $scope.saveStudent = function () {
        console.log($scope.student);
        studentService.addStudent($scope.student).success(function (data) {
            if (data.IsOk) {
                $scope.successMessage = 'The student has been inserted!';
                $scope.errors = null;
            } else {
                $scope.errors = data.Errors;
                $scope.successMessage = null;
            }
        }).error(function (reason) {
            console.log(reason);
        });
    };

    var init = function () {
        $scope.view = {};
        groupService.getAllGroups().success(function (data) {
            $scope.view.Groups = data;
        }).error(function (reason) {
            console.log(reason);
        });
    };

    init();
}]);