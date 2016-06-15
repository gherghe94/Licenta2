myApp.controller('myStudentHoursCtrl', ['$scope', 'studentService', '$uibModal', function ($scope, studentService, $uibModal) {
    var getHoursFrom = function (day, hours) {
        var todayHours = [];
        hours.forEach(function (item, index) {
            if (item.TheDay === day) {
                todayHours.push(item);
            }
        });

        todayHours.sort(function (a, b) {
            if (a.TheHour > b.TheHour)
                return 1;
            if (a.TheHour == b.TheHour)
                return 0;
            return -1;
        });

        return todayHours;
    };

    var constructSchedule = function (hours) {
        var schedule = [];
        var days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];

        days.forEach(function (item, index) {
            schedule.push({
                Day: item,
                Hours: getHoursFrom(item, hours)
            });
        });

        return schedule;
    };

    var init = function () {
        var student = $scope.view.student;
        studentService.getStudentHours(student.GroupId).success(function (data) {
            $scope.view.schedule = constructSchedule(data);
        }).error(function (data) {
            console.log(data);
        });
    };

    init();

    $scope.viewDetails = function (hour) {
        $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainStudent/myHours/hourDetail.html',
            controller: function ($uibModalInstance, $scope, items) {
                var getEqualEntity = function (entity, responseArray) {
                    for (var idx = 0; idx < responseArray.length; idx++) {
                        if (entity.Id === responseArray[idx].Id) {
                            return responseArray[idx];
                        }
                    }

                    return null;
                };

                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };

                var modalInit = function () {
                    $scope.hour = items.hourModel;
                };

                modalInit();
            },
            resolve: {
                items: {
                    hourModel: hour,
                }
            }
        });
    };

}]);