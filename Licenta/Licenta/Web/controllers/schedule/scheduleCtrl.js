myApp.controller('scheduleCtrl', ['$scope', '$uibModal', 'groupService', 'hourService', 'coursesService', 'teacherService', 'roomsService', function ($scope, $uibModal, groupService, hourService, coursesService, teacherService, roomsService) {

    var getHoursToday = function (day, hours) {
        var arr = [];

        for (var index = 0; index < hours.length; index++) {
            if (hours[index].TheDay == day) {
                arr.push(hours[index]);
            }
        }

        return arr;
    };

    var isHourInInterval = function (hours, interval) {
        for (var index = 0; index < hours.length; ++index) {
            var current = hours[index];
            if (current.TheHour == interval) {
                return current;
            }
        }

        return null;
    };

    var constructHours = function (day, hours) {
        var intervals = ["08:00-9:50", "10:00-11:50", "12:00-13:50", "14:00-15:50", "16:00-17:50", "18:00-19:50", "20:00-21:50"];
        var hoursInDay = getHoursToday(day, hours);
        hoursInDay.sort(function (a, b) {
            if (a.TheHour > b.TheHour)
                return 1;
            if (a.TheHour == b.TheHour)
                return 0;
            return -1;
        });

        var result = [];

        for (var intervalIndex = 0; intervalIndex < intervals.length; intervalIndex++) {
            var currentInterval = intervals[intervalIndex];
            var foundHour = isHourInInterval(hoursInDay, currentInterval);
            if (foundHour) {
                result.push(foundHour);
            } else {
                result.push({
                    TheHour: currentInterval,
                    TheDay: day,
                    Group: $scope.currentGroup,
                    GroupId: $scope.currentGroup.Id,
                    IsPlaceholder: true
                });
            }
        }

        return result;
    };

    var construct = function (hours) {
        var days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];
        var schedule = [];

        for (var dayIndex = 0; dayIndex < days.length; ++dayIndex) {
            var scheduleDay = {
                TheDay: days[dayIndex],
                Hours: constructHours(days[dayIndex], hours)
            };
            schedule.push(scheduleDay);
        }

        return schedule;
    };

    $scope.showSchedule = function (group) {
        $scope.currentGroup = group;
        hourService.getAll(group.Id).success(function (data) {
            var hours = data;
            var schedule = construct(hours);
            $scope.view.currentSchedule = schedule;
        }).error(function (data) {
            console.log(data);
        });
    };

    /* Add/Edit Hour Ctrl */
    var openModal = function (hour) {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainAdmin/manageSchedule/addHour.html',
            controller: function ($uibModalInstance, $scope, items) {
                var getEqualEntity = function (entity, responseArray) {
                    for (var idx = 0; idx < responseArray.length; idx++) {
                        if (entity.Id === responseArray[idx].Id) {
                            return responseArray[idx];
                        }
                    }

                    return null;
                };

                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh(items.currentGroup);
                };

                $scope.getTeachers = function (course) {
                    items.tcRestService.getTeachersFor(course.Id).success(function (data) {
                        $scope.Teachers = data;
                        var teacher = $scope.hour.Teacher;
                        if (teacher) {
                            $scope.hour.Teacher = getEqualEntity(teacher, $scope.Teachers);
                        }
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                $scope.save = function () {
                    console.log($scope.hour);
                    items.hourRestService.save($scope.hour).success(function (data) {
                        if (data.IsOk) {
                            $scope.successMessage = 'The Hour has been saved!';
                            $scope.errors = null;
                        } else {
                            $scope.errors = data.Errors;
                            $scope.successMessage = null;
                        }
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var initCourses = function () {
                    items.coursesRestService.getAll({}).success(function (data) {
                        $scope.Courses = data;
                        var course = $scope.hour.Course;
                        if (course) {
                            $scope.hour.Course = getEqualEntity(course, $scope.Courses);
                            $scope.getTeachers(course);
                        }
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var initRooms = function () {
                    items.roomRestService.getAll({ Name: '', Location: '' }).success(function (data) {
                        $scope.Rooms = data;
                        var room = $scope.hour.Room;
                        if (room) {
                            $scope.hour.Room = getEqualEntity(room, $scope.Rooms);
                        }
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var initView = function () {
                    $scope.view = {};
                    $scope.view.weekTypes = ['Even', 'Odd', 'Both'];
                    $scope.view.hourTypes = ['Lab', 'Workshop', 'Class'];
                };

                var modalInit = function () {
                    $scope.hour = items.hourModel;
                    initView();
                    initCourses();
                    initRooms();
                };

                modalInit();
            },
            resolve: {
                items: {
                    hourRestService: hourService,
                    coursesRestService: coursesService,
                    tcRestService: teacherService,
                    roomRestService: roomsService,
                    hourModel: hour,
                    refresh: $scope.showSchedule,
                    currentGroup: $scope.currentGroup
                }
            }
        });
    };

    $scope.addHour = function (hour) {
        openModal(hour);
    };

    $scope.editHour = function (hour) {
        openModal(hour);
    };

    $scope.deleteHour = function (hour) {
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            keyboard: false,
            templateUrl: 'mainAdmin/manageSchedule/deleteHourModal.html',
            controller: function ($uibModalInstance, $scope, items) {
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                    items.refresh(items.currentGroup);
                };

                $scope.delete = function () {
                    hourService.remove($scope.hour).success(function (data) {
                        $scope.cancel();
                    }).error(function (data) {
                        console.log(data);
                    });
                };

                var modalInit = function () {
                    $scope.hour = items.hourModel;
                };

                modalInit();
            },
            resolve: {
                items: {
                    hourRestService: hourService,
                    hourModel: hour,
                    refresh: $scope.showSchedule,
                    currentGroup: $scope.currentGroup
                }
            }
        });
    };

    var initView = function () {
        groupService.getAllGroups({}).success(function (data) {
            $scope.view.Groups = data;
        }).error(function (data) {
            console.log(data);
        });
    };

    var init = function () {
        $scope.view = {
            Groups: [],
            currentSchedule: {}
        };

        initView();
    };

    init();
}]);