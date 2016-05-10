myApp.service('teacherProfileService', function ($http) {
    var baseUrl = '/api/TeacherProfile/';

    return {
        getCourses: function (teacherId) {
            return $http({
                url: baseUrl + "GetCourses",
                method: "GET",
                params: { "teacherId": teacherId }
            });
        },
        save: function (teacher) {
            return $http({
                url: baseUrl + "Save",
                method: "POST",
                data: teacher
            });
        }
    };
});