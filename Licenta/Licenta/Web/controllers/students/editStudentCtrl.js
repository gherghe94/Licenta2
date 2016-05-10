myApp.controller('editStudentCtrl', ['$scope', 'studentService', 'groupService', 'editStudentService', '$timeout', function ($scope, studentService, groupService, editStudentService, $timeout) {

    $scope.update = function () {
        studentService.updateStudent($scope.view.Stud).success(function (data) {
            if (data.IsOk) {
                $scope.successMessage = 'The student has been updated!';
                $scope.errors = null;
            } else {
                $scope.errors = data.Errors;
                $scope.successMessage = null;
            }
        }).error(function (reason) {
            console.log(reason);
        });
    };

    $scope.formatGroup = function () {
        var group = $scope.view.Stud.Group;
        return group.Specialization + " " + group.Name;
    };

    var init = function () {
        $scope.view = {};
        $scope.view.Stud = editStudentService.getStudent();
        $scope.view.Groups = editStudentService.getGroups();
    };

    init();
}]);