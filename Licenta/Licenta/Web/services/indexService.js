myApp.service('indexService', function ($http) {

    return {
        testSecureWebApi: function () {
            return $http({
                url: "/api/Account/GetAvatar",
                method: "GET",
                //headers: { Authorization: "Basic YW5kcmVpOjEyMzQ=" }
            })
        }

    }
});