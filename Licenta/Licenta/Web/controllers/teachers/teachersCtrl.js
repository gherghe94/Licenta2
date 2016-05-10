myApp.controller('teachersCtrl', ['$scope', 'teacherService', 'coursesService', '$uibModal', function ($scope, teacherService, coursesService, $uibModal) {
    var openModal = function (model) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'mainAdmin/manageTeachers/addTeacher.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.save = function () {
                    items.restService.save($scope.teacher).success(function (data) {
                        if (data.IsOk) {
                            $scope.successMessage = 'The teacher has been saved!';
                            $scope.errors = null;
                        } else {
                            $scope.errors = data.Errors;
                            $scope.successMessage = null;
                        }
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var init = function () {
                    $scope.teacher = items.teacherModel;
                    $scope.titleOptions = ['Asistent', 'Lector', 'Conferentiar', 'Profesor'];
                };

                init();
            },
            resolve: {
                items: {
                    refresh: $scope.search,
                    restService: teacherService,
                    teacherModel: model
                }
            }
        });
    };

    $scope.addTeacher = function () {
        openModal({});
    };

    $scope.editTeacher = function (teacher) {
        openModal(teacher);
    };

    $scope.removeTeacher = function (teacher) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'mainAdmin/manageTeachers/removeTeacherModal.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.delete = function () {
                    var teacher = items.teacher;

                    items.restService.remove(teacher).success(function (data) {
                        $scope.cancel();
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                $scope.theTeacher = items.teacher;
            },
            resolve: {
                items: {
                    teacher: teacher,
                    refresh: $scope.search,
                    restService: teacherService
                }
            }
        });
    };

    $scope.editCourses = function (teacher) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'mainAdmin/manageTeachers/addCoursesToTeacher.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                var getDto = function () {
                    return {
                        TeacherId: $scope.teacher.Id,
                        Courses: $scope.allCourses
                    };
                }

                $scope.save = function () {
                    items.teacherRestService.saveCoursesFor(getDto()).success(function (data) {
                        $scope.successMessage = "Successfully saved!";
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var teaches = function (teacher, course) {
                    var all = teacher.Courses;
                    for (var index = 0; index < all.length; index++) {
                        if (all[index].Name == course.Name) {
                            return true;
                        }
                    }

                    return false;
                }

                var decorateCourses = function (courses) {
                    var teacher = $scope.teacher;
                    courses.forEach(function (item, index) {
                        item.IsChecked = teaches(teacher, item);
                    });
                };

                var init = function () {
                    $scope.teacher = items.teacherModel;
                    coursesService.getAll({}).success(function (data) {
                        $scope.allCourses = data;
                        decorateCourses($scope.allCourses);
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                init();
            },
            resolve: {
                items: {
                    teacherRestService: teacherService,
                    coursesService: coursesService,
                    teacherModel: teacher,
                    refresh: $scope.search
                }
            }
        });
    };

    $scope.search = function () {
        var filter = $scope.view.filter;
        teacherService.getAll(filter).success(function (data) {
            $scope.view.Teachers = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    var init = function () {
        $scope.view = {
            filter: {
                FullName: '',
                Title: ''
            },
            Teachers: [],
            titleOptions: ['', 'Asistent', 'Lector', 'Conferentiar', 'Profesor']
        };

        $scope.search();
    };

    init();
}]);