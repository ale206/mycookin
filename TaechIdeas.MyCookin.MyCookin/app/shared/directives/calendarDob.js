var calendarDob = {
    init: function() {

        const app = angular.module("calendarDob_module", []);

        app.directive("calendarDob",
            function() {
                return {
                    restrict: "E",
                    templateUrl: "/app/shared/directives/calendarDob.html",
                    scope: false,
                    controller: function($scope) {
                        const currentDate = new Date();

                        $scope.today = function() {
                            //$scope.dt = new Date();
                        };
                        $scope.today();

                        $scope.clear = function() {
                            $scope.dt = null;
                        };

                        $scope.toggleMin = function() {
                            $scope.minDate = new Date(1900, 01, 01);
                        };

                        $scope.toggleMin();

                        //Max 18 years
                        const maxDate = new Date(currentDate.getFullYear() - 18,
                            currentDate.getMonth(),
                            currentDate.getDate());
                        $scope.maxDate = maxDate;

                        $scope.open1 = function() {
                            $scope.popup1.opened = true;
                        };

                        $scope.open2 = function() {
                            $scope.popup2.opened = true;
                        };

                        $scope.setDate = function(day, month, year) {
                            $scope.dt = new Date(day, month, year);
                        };

                        $scope.dateOptions = {
                            startingDay: 1,
                            showWeeks: false,
                            initDate: maxDate,
                            yearRows: 4,
                            yearColumns: 5
                        };

                        $scope.dateMode = "year";

                        $scope.formats = ["dd/MM/yyyy", "dd-MMMM-yyyy", "yyyy/MM/dd", "dd.MM.yyyy", "shortDate"];
                        $scope.format = $scope.formats[0];

                        $scope.popup1 = {
                            opened: false
                        };

                        $scope.popup2 = {
                            opened: false
                        };

                        const tomorrow = new Date();
                        tomorrow.setDate(tomorrow.getDate() + 1);
                        const afterTomorrow = new Date();
                        afterTomorrow.setDate(tomorrow.getDate() + 1);
                        $scope.events =
                        [
                            {
                                date: tomorrow,
                                status: "full"
                            },
                            {
                                date: afterTomorrow,
                                status: "partially"
                            }
                        ];

                        $scope.getDayClass = function(date, mode) {
                            if (mode === "day") {
                                const dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                                for (let i = 0; i < $scope.events.length; i++) {
                                    const currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                                    if (dayToCheck === currentDay) {
                                        return $scope.events[i].status;
                                    }
                                }
                            }

                            return "";
                        };
                    }
                };
            });


    }
};

module.exports = {
    init: calendarDob.init
};