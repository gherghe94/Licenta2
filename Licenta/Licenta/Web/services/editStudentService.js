myApp.service('editStudentService', function ($http) {
    var student = null;
    var groups = null;
    var factory = {};

    factory.reset = function () {
        student = null;
        groups = null;
    };

    factory.register = function (mStudent) {
        //student = angular.copy(mStudent);
        student = mStudent;
    };

    factory.registerGroups = function (mGroups) {
        groups = mGroups;
        //groups = angular.copy(mGroups);
    };

    factory.getStudent = function () {
        return student;
    };

    factory.getGroups = function () {
        return groups;
    };

    return factory;
});