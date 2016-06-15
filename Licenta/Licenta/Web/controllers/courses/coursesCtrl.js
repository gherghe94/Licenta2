myApp.controller('coursesCtrl', ['$scope', 'coursesService', '$uibModal', function ($scope, coursesService, $uibModal) {
    var openModal = function (model) {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainAdmin/manageCourses/addCourse.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.save = function () {
                    items.restService.save($scope.course).success(function (data) {
                        if (data.IsOk) {
                            $scope.successMessage = 'The course has been saved!';
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
                    $scope.course = items.courseModel;
                    $scope.yearOptions = [1, 2, 3, 4, 5];
                    $scope.creditsOptions = [1, 2, 3, 4, 5, 6];
                };

                init();
            },
            resolve: {
                items: {
                    refresh: $scope.search,
                    restService: coursesService,
                    courseModel: model
                }
            }
        });
    };

    $scope.search = function () {
        var filter = $scope.view.filter;
        coursesService.getAll(filter).success(function (data) {
            $scope.view.Courses = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    $scope.addCourse = function () {
        openModal({});
    };

    $scope.editCourse = function (course) {
        openModal(course);
    };

    $scope.deleteCourse = function (course) {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainAdmin/manageCourses/removeCourseModal.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.delete = function () {
                    var course = items.course;
                    items.restService.remove(course).success(function (data) {
                        $scope.cancel();
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                $scope.theCourse = items.course;
            },
            resolve: {
                items: {
                    course: course,
                    refresh: $scope.search,
                    restService: coursesService
                }
            }
        });
    };

    var init = function () {
        $scope.view = {
            filter: {
                Name: '',
            },
            Courses: [],
            PossibleCredits: ['All', 1, 2, 3, 4, 5, 6],
            PossibleYears: ['All', 1, 2, 3, 4, 5]
        };

        $scope.search();
    };

    init();
}]);