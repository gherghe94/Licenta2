myApp.controller('myStudentHoursCtrl', ['$scope', 'studentService', function ($scope, studentService) {
    var getHoursFrom = function (day, hours) {
        var todayHours = [];
        hours.forEach(function (item, index) {
            if (item.TheDay === day) {
                todayHours.push(item);
            }
        });

        todayHours.sort(function (h1, h2) {
            return h1.TheHour - h2.TheHour;
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
}]);