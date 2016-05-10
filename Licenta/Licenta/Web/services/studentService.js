myApp.service('studentService', function ($http) {
    var baseUrl = '/api/Students/';
    var addStudentBaseUrl = '/api/AddStudent/';
    var studentPanelUrl = '/api/StudentPanel/';

    return {
        getStudentsByFilter: function (studentsFilter) {
            return $http({
                url: baseUrl + "GetStudents",
                method: "POST",
                data: studentsFilter
            });
        },
        addStudent: function (student) {
            return $http({
                url: addStudentBaseUrl + "AddStudent",
                method: "POST",
                data: student
            });
        },
        updateStudent: function (entity) {
            return $http({
                url: baseUrl + "EditStudent",
                method: "PUT",
                data: entity
            });
        },
        removeStudent: function (entity) {
            return $http({
                url: baseUrl + "RemoveStudent",
                method: "DELETE",
                data: entity
            });
        },
        getStudentHours: function (groupId) {
            return $http({
                url: studentPanelUrl + "GetHours",
                method: "GET",
                params: { "groupId": groupId }
            })
        },
        getGroupAnnouncements: function (groupId) {
            return $http({
                url: baseUrl + "GetGroupAnnouncements",
                method: "GET",
                params: { "groupId": groupId }
            })
        }
    }
});