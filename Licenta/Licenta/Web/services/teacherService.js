myApp.service('teacherService', function ($http) {
    var baseUrl = '/api/Teacher/';
    var baseTeacherCourse = '/api/TeacherCourses/';
    var baseExternalPanelUrl = '/api/TeacherPanel/';

    return {
        getAll: function (filter) {
            return $http({
                url: baseUrl + "GetAll",
                method: "GET",
                params: filter
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
        },
        saveCoursesFor: function (dto) {
            return $http({
                url: baseTeacherCourse + "Save",
                method: "POST",
                data: dto
            });
        },
        getTeachersFor: function (courseId) {
            return $http({
                url: baseTeacherCourse + "GetAllTeachersFor",
                method: "GET",
                params: { "courseId": courseId }
            });
        },
        getHoursForTeacher: function (teacherId) {
            return $http({
                url: baseExternalPanelUrl + "GetHours",
                method: "GET",
                params: { "teacherId": teacherId }
            });
        }
    }
});