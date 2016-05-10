myApp.service('hourService', function ($http) {
    var baseUrl = '/api/Hours/';

    return {
        getAll: function (groupId) {
            return $http({
                url: baseUrl + "GetHours",
                method: "GET",
                params: { "groupId": groupId }
            });
        },
        save: function (hour) {
            return $http({
                url: baseUrl + "Save",
                method: "POST",
                data: hour
            });
        },
        remove: function (hour) {
            return $http({
                url: baseUrl + "Delete",
                method: "DELETE",
                data: hour
            });
        }
    }
});