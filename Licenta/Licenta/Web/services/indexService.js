myApp.service('indexService', function ($http) {

    return {
        testSecureWebApi: function () {
            return $http({
                url: "/api/Account/GetAvatar",
                method: "GET",
                //headers: { Authorization: "Basic YW5kcmVpOjEyMzQ=" }
            })
        },
        recoverPassword: function (username) {
            return $http({
                url: "/api/Account/RecoverPassword",
                method: "PUT",
                data: { "username": username }
            })
        },
    }
});