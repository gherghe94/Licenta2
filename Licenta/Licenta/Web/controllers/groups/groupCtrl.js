myApp.controller('groupsCtrl', ['$scope', 'groupService', '$uibModal', function ($scope, groupService, $uibModal) {
    var openModal = function (model) {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainAdmin/manageGroups/addGroup.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.save = function () {
                    items.restService.save($scope.group).success(function (data) {
                        if (data.IsOk) {
                            $scope.successMessage = 'The group has been saved!';
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
                    $scope.group = items.groupModel;
                    $scope.view = items.view;
                };

                init();
            },
            resolve: {
                items: {
                    refresh: $scope.search,
                    restService: groupService,
                    groupModel: model,
                    view: $scope.view
                }
            }
        });
    };

    $scope.search = function () {
        var filter = $scope.view.filter;
        groupService.getAllGroups(filter).success(function (data) {
            $scope.view.Groups = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    $scope.addGroup = function () {
        openModal({});
    };

    $scope.editGroup = function (group) {
        openModal(group);
    };

    $scope.deleteGroup = function (group) {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainAdmin/manageGroups/removeGroupModal.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.delete = function () {
                    var group = items.group;

                    items.restService.remove(group).success(function (data) {
                        $scope.cancel();
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                $scope.theGroup = items.group;
            },
            resolve: {
                items: {
                    group: group,
                    refresh: $scope.search,
                    restService: groupService
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
            Groups: [],
            yearOptions: ['All', 1, 2, 3, 4, 5],
            specializationOptions: ['', 'MI', 'I', 'IAG', 'IA']
        };

        $scope.search();
    };

    init();
}]);