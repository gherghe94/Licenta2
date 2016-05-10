'use strict';
myApp.factory('authInterceptorService', function ($location, $injector, $rootScope) {
    var service = this;

    service.request = function (config) {
        var loginData = $injector.get('authService').authentication;
        var token = loginData.token;
        var basic = "Basic ";

        if (token) {
            config.headers.Authorization = basic + token;
        }

        return config;
    };

    service.responseError = function (response) {
        if (response.status == 401) {
            $location.path('/login'); // redirect to login actually and log out
        }

        if (response.status == 400) {
            $rootScope.$broadcast('badRequest', response);
            return false;
        }

        return response;
    }

    return service;
});
