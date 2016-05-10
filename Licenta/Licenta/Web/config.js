myApp.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    $httpProvider.defaults.headers["delete"] = {
        'Content-Type': 'application/json;charset=utf-8'
    };

    $routeProvider
        .when('/login', {
            templateUrl: 'login/login.html',
            controller: 'LoginCtrl'
        })
        .when('/about', {
            templateUrl: 'login/about.html',
        })
        .when('/mainAdmin', {
            templateUrl: 'mainAdmin/mainAdmin.html',
            controller: 'MainAdminCtrl'
        })
        .when('/mainTeacher', {
            templateUrl: 'mainTeacher/mainTeacher.html',
            controller: 'MainTeacherCtrl'
        })
        .when('/mainStudent', {
            templateUrl: 'mainStudent/mainStudent.html',
            controller: 'MainStudentCtrl'
        })
        .otherwise({ redirectTo: '/login' });
}]);
