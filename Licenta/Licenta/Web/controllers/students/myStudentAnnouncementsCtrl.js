myApp.controller('myStudentAnnouncementsCtrl', ['$scope', 'studentService', function ($scope, studentService) {
    $scope.getGroupDisplayName = function (group) {
        return "Group: " + group.Specialization + " " + group.Name + " , Year: " + group.Year;
    };

    var initAnnouncements = function () {
        var groupId = $scope.view.student.GroupId;
        studentService.getGroupAnnouncements(groupId).success(function (data) {
            $scope.announcements = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    var init = function () {
        initAnnouncements();
    };

    init();
}]);