myApp.service('groupService', function ($http) {
    var baseUrl = '/api/Group/';

    return {
        getAllGroups: function (filter) {
            return $http({
                url: baseUrl + "GetAllGroups",
                method: "GET",
                params: filter
            });
        },
        save: function (group) {
            return $http({
                url: baseUrl + "Save",
                method: "POST",
                data: group
            });
        },
        remove: function (group) {
            return $http({
                url: baseUrl + "Delete",
                method: "DELETE",
                data: group
            });
        }
    }
});