myApp.service('mainTeacherService', function ($http) {
    var accountUrl = '/api/Account/';
    var teacherPanelUrl = '/api/TeacherPanel/';

    return {
        getLoggedTeacher: function (username, userType) {
            return $http({
                url: accountUrl + "GetLoggedInUser",
                method: "GET",
                params: { "username": username, "userType": userType }
            })
        },
        getBarItems: function () {
            return $http({
                url: teacherPanelUrl + "GetBarItems",
                method: "GET"
            })
        }
    }
});