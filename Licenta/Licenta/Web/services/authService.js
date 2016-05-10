'use strict';
myApp.factory('authService', ['$http', '$q', function ($http, $q) {

    var serviceBase = '/api/Account/Login';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        token: "",
        userType: ""
    };

    var _login = function (loginData) {

        var loginCredentials = { Username: loginData.Username, Password: loginData.Password };
        var deferred = $q.defer();

        $http.post(serviceBase, loginCredentials, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            if (!response) {
                return false;
            }

            _authentication.isAuth = true;
            _authentication.userName = loginCredentials.Username;
            _authentication.token = response.Token;
            _authentication.userType = response.TypeToken;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () { // reset authentication
        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.token = "";
    };


    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}]);