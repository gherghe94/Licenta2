myApp.controller('myTeacherHoursCtrl', ['$scope', 'teacherService', function ($scope, teacherService) {

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
        var teacher = $scope.view.teacher;
        teacherService.getHoursForTeacher(teacher.Id).success(function (data) {
            $scope.view.schedule = constructSchedule(data);
        }).error(function (data) {
            console.log(data);
        });
    };

    init();
}]);