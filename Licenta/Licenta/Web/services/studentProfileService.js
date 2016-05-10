myApp.service('studentProfileService', function ($http) {
    var baseUrl = '/api/StudentProfile/';

    return {
        save: function (student) {
            return $http({
                url: baseUrl + "Save",
                method: "POST",
                data: student
            });
        }
    };
});