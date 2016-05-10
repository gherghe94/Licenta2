myApp.service('roomsService', function ($http) {
    var baseUrl = '/api/Room/';

    return {
        getAll: function (filter) {
            return $http({
                url: baseUrl + "GetAll",
                method: "GET",
                params: { "name": filter.Name, "location": filter.Location }
            });
        },
        save: function (room) {
            return $http({
                url: baseUrl + "Add",
                method: "POST",
                data: room
            });
        },
        remove: function (room) {
            return $http({
                url: baseUrl + "Delete",
                method: "DELETE",
                data: room
            });
        }
    }
});