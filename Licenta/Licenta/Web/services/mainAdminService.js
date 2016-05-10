myApp.service('mainAdminService', function ($http) {
    return {
        getBarItems: function () {
            return $http({
                url: "/api/MainAdministration/GetBarItems",
                method: "GET",
            });
        }
    }
});