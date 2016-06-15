myApp.controller('roomsCtrl', ['$scope', 'roomsService', '$uibModal', function ($scope, roomsService, $uibModal) {

    /* Modal with controller for adding a new room or editing one */
    var openModal = function (model) {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainAdmin/manageRooms/addRoom.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.save = function () {
                    items.restService.save($scope.room).success(function (data) {
                        if (data.IsOk) {
                            $scope.successMessage = 'The room has been saved!';
                            $scope.errors = null;
                        } else {
                            $scope.errors = data.Errors;
                            $scope.successMessage = null;
                        }
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var init = function () {
                    $scope.room = items.roomModel;
                };

                init();
            },
            resolve: {
                items: {
                    refresh: $scope.search,
                    restService: roomsService,
                    roomModel: model
                }
            }
        });
    };

    $scope.search = function () {
        var filter = $scope.view.filter;
        roomsService.getAll(filter).success(function (data) {
            $scope.view.Rooms = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    $scope.addRoom = function () {
        openModal({});
    };

    $scope.editRoom = function (room) {
        openModal(room);
    };

    $scope.deleteRoom = function (room) {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainAdmin/manageRooms/removeRoomModal.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.delete = function () {
                    var room = items.room;

                    items.restService.remove(room).success(function (data) {
                        $scope.cancel();
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                $scope.theRoom = items.room;
            },
            resolve: {
                items: {
                    room: room,
                    refresh: $scope.search,
                    restService: roomsService
                }
            }
        });
    };

    var init = function () {
        $scope.view = {
            filter: {
                Name: '',
                Location: ''
            },
            Rooms: []
        };

        $scope.search();
    };

    init();
}]);