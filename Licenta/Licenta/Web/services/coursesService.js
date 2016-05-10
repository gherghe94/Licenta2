myApp.service('coursesService', function ($http) {
    var baseUrl = '/api/Course/';

    return {
        getAll: function (filter) {
            return $http({
                url: baseUrl + "GetAll",
                method: "GET",
                params: filter
            });
        },
        save: function (course) {
            return $http({
                url: baseUrl + "Save",
                method: "POST",
                data: course
            });
        },
        remove: function (course) {
            return $http({
                url: baseUrl + "Delete",
                method: "DELETE",
                data: course
            });
        }
    }
});