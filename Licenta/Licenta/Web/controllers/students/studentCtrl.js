myApp.controller('studentCtrl', ['$scope', 'studentService', 'groupService', '$uibModal', 'editStudentService', function ($scope, studentService, groupService, $uibModal, editStudentService) {
    var addState = 'add';
    var editState = 'edit';

    var pleaseSelectLabel = 'Please select!';

    var shouldRetrieveAll = function () {
        return ($scope.studentsFilter.GroupName === pleaseSelectLabel &&
            $scope.studentsFilter.FirstName === '' &&
            $scope.studentsFilter.LastName === '')
    }

    var decorateStudentsFilter = function () {
        var filter = $scope.studentsFilter;
        filter.RetrieveAll = shouldRetrieveAll();
    };

    var getStudents = function () {
        studentService.getStudentsByFilter($scope.studentsFilter).success(function (data) {
            $scope.view.Students = data;
        }).error(function (reason) {
            console.log(reason);
        });
    };

    var getGroups = function () {
        groupService.getAllGroups().success(function (data) {
            $scope.view.Groups = data;
            $scope.studentsFilter.GroupName = data[0].Name;
            $scope.search();
        }).error(function (reason) {
            console.log(reason);
        });
    };

    $scope.search = function () {
        decorateStudentsFilter();
        getStudents();
    };

    //TODO: here add / edit functionalities. to be managed in the same way
    $scope.addStudent = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'mainAdmin/manageStudents/addStudent.html',
        });
    };

    $scope.editStudent = function (tStudent) {
        editStudentService.registerGroups($scope.view.Groups);
        editStudentService.register(tStudent);

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'mainAdmin/manageStudents/editStudent.html',
        });
    };

    $scope.removeStudent = function (tStudent) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'mainAdmin/manageStudents/removeStudentModal.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    $scope.refresh();
                };

                $scope.delete = function () {
                    items.StudentService.removeStudent($scope.Stud).success(function (data) {
                        $scope.cancel();
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                $scope.Stud = items.Student;
                $scope.refresh = items.Search;
            },
            resolve: {
                items: {
                    Student: tStudent,
                    Search: $scope.search,
                    StudentService: studentService
                }
            }
        });

    };

    var init = function () {
        $scope.studentsFilter = {
            GroupName: pleaseSelectLabel,
            FirstName: '',
            LastName: ''
        };

        $scope.view = {
            Students: [],
            Groups: [],
            currentPage: 1,
            showWindow: false,
            windowState: ''
        };

        getGroups();
    };

    init();
}]);