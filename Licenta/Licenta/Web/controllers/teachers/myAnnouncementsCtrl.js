myApp.controller('myAnnouncementsCtrl', ['$scope', 'announcementService', '$uibModal', 'groupService', function ($scope, announcementService, $uibModal, groupService) {
    var openModal = function (announcement) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'mainTeacher/myAnnouncements/addAnnouncement.html',
            controller: function ($uibModalInstance, $scope, items) {
                var fillAnnouncement = function (entity) {
                    if (entity.Group) {
                        entity.GroupId = entity.Group.Id;
                    }

                    if (!entity.Teacher) {
                        entity.Teacher = items.teacher;
                    }

                    entity.TeacherId = entity.Teacher.Id;
                };

                var putLoading = function () {
                    if (!$scope.announcement.Id || $scope.announcement.Id == 0)
                        $scope.loading = true;
                };

                var process = function (data) {
                    if (data.IsOk) {
                        $scope.successMessage = 'The announcement has been saved!';
                        $scope.errors = null;
                        if (data.EntityId) {
                            $scope.announcement.Id = data.EntityId;
                        }

                    } else {
                        $scope.errors = data.Errors;
                        $scope.successMessage = null;
                    }

                    $scope.loading = false;
                };

                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh();
                };

                $scope.save = function () {
                    fillAnnouncement($scope.announcement);
                    putLoading();
                    items.restService.save($scope.announcement).success(function (data) {
                        process(data);
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var initGroupForEditing = function () {
                    var anc = $scope.announcement;
                    if (anc.Group) {
                        var groups = $scope.Groups;
                        groups.forEach(function (item, index) {
                            if (item.Id === anc.Group.Id) {
                                $scope.announcement.Group = item;
                            }
                        });
                    }
                };

                var initGroups = function () {
                    groupService.getAllGroups({}).success(function (data) {
                        $scope.Groups = data;
                        initGroupForEditing();
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var init = function () {
                    $scope.announcement = items.announcement;
                    initGroups();
                };

                init();
            },
            resolve: {
                items: {
                    refresh: initAnnouncements,
                    restService: announcementService,
                    groupRestService: groupService,
                    announcement: announcement,
                    teacher: $scope.view.teacher
                }
            }
        });
    };

    $scope.getGroupDisplayName = function (group) {
        return "Group: " + group.Specialization + " " + group.Name + " , Year: " + group.Year;
    };

    $scope.addAnnouncement = function () {
        openModal({});
    };

    $scope.editAnnouncement = function (anc) {
        openModal(anc);
    };

    var initAnnouncements = function () {
        var teacherId = $scope.view.teacher.Id;
        announcementService.getAll(teacherId).success(function (data) {
            $scope.announcements = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    var init = function () {
        initAnnouncements();
    };

    init();
}]);