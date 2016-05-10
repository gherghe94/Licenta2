myApp.service('mainStudentService', function ($http) {
    var accountUrl = "/api/Account/";
    var studentPanelUrl = '/api/StudentPanel/';

    return {
        getLoggedStudent: function (username, userType) {
            return $http({
                url: accountUrl + "GetLoggedInUser",
                method: "GET",
                params: { "username": username, "userType": userType }
            })
        },
        getBarItems: function () {
            return $http({
                url: studentPanelUrl + "GetBarItems",
                method: "GET"
            })
        }
    }
});