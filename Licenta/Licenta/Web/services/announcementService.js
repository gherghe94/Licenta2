myApp.service('announcementService', function ($http) {
    var baseUrl = '/api/Announcements/';

    return {
        getAll: function (teacherId) {
            return $http({
                url: baseUrl + "GetAll",
                method: "GET",
                params: { "teacherId": teacherId }
            });
        },
        getAllForGroup: function (groupId) {
            return $http({
                url: baseUrl + "GetAll",
                method: "GET",
                params: { "teacherId": teacherId }
            });
        },
        save: function (announcement) {
            return $http({
                url: baseUrl + "Save",
                method: "POST",
                data: announcement
            });
        }
    }
});