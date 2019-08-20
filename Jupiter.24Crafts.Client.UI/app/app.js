/// <reference path="vendors/angular.js" />
/// <reference path="vendors/angular-ui-router.js" />

var app = angular.module('rootApplication',['ui.router', 'LocalStorageModule', 'base64', 'toastr','ngMaterial', 'ngMessages', 'material.svgAssetsCache']);
app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/');
    $stateProvider.state('/', {
        url: '/'
    });
    $stateProvider.state('aboutUs', {
        url: '/aboutUs',
        views: {
            'aboutUs': {
                templateUrl: 'app/components/views/about-us-template.html',
                controller: 'aboutUsController'
            }
        }
    });

    $stateProvider.state('contact', {
        url: '/contact',
        views: {
            'contact': {
                controller: 'contactController'
            }
        }
    });
    $stateProvider.state('portfolio', {
        url: '/portfolio',
        views: {
            'portfolio': {
                templateUrl: 'app/components/views/portfolio-template.html',
                controller: 'portfolioController'
            }
        }
    });
    $stateProvider.state('login', {
        url: '/login',
        controller: 'loginController'         
    });
    
    $stateProvider.state('registration', {
        url: 'registration',
        views: {
            'registration': {
                controller: 'registrationController'
            }
        }
    });
    //test
    $stateProvider.state('opportunities', {
        url: '/opportunities',        
        views: {
            'opportunities': {
                templateUrl: 'app/components/views/opportunities-template.html',
                controller: 'opportunitiesController'
            }
        }
    });
    $stateProvider.state('profileSearch', {
        url: '/profileSearch',      
        views: {
            'profileSearch': {
                templateUrl: 'app/components/views/profile-search-template.html',
                controller: 'profileSearchController'
            }
        }
    });
   
}]);


var serviceBase = 'http://24crafts/API/';
//var serviceBase = 'http://smsclient/service/';
//var serviceBase = 'http://craftsapi.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp',
    authentication:false
});

/*
This directive allows us to pass a function in on an enter key to do what we want.
 */

app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});

app.directive('ngFileModel', ['$parse','$rootScope', function ($parse,$rootScope) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.ngFileModel);
            var isMultiple = attrs.multiple;
            var modelSetter = model.assign;
            
            element.bind('change', function () {
                var values = [];
                angular.forEach(element[0].files, function (item) {
                    var value = {
                        // File Name 
                        name: item.name,
                        //File Size 
                        size: item.size,
                        //File URL to view 
                        url: URL.createObjectURL(item),
                        // File Input Value 
                        _file: item
                    };
                    values.push(value);                   
                });
                scope.$apply(function () {
                    if (isMultiple) {
                        modelSetter(scope, values);
                    } else {
                        modelSetter(scope, values[0]);
                    }
                });
                //if (element[0].attributes.id.value == 'imageupload') {
                //    $rootScope.$broadcast('portfolioimageCall', values);
                //}
                //else if (element[0].attributes.id.value == 'videoupload')
                //{
                //    $rootScope.$broadcast('portfoliovideoCall', values);
                //}
            });
        }
    };
}]);
app.run(['authService', '$rootScope', '$state', 'ngAuthSettings', function (authService, $rootScope, $state, ngAuthSettings) {
    authService.fillAuthData();
    $rootScope.$on('$stateChangeStart',
        function (event, toState, toParams, fromState, fromParams) {
            $('#close-btn').css('top', '59px');
            if(toState.name.indexOf('portfolio') > -1){
                if (!authService.authentication.isAuth) {
                    $('.portfolio-individual-container').trigger('click');
                    $('.loginModal').trigger('click');
                    
                    event.preventDefault();
                    $state.go('login');
                }
            }
            if (!authService.authentication.isAuth) {
                console.log('DENY : Redirecting to Login');       
                //event.preventDefault();
                //$state.path('login');
            } else {           
                console.log('Allow');
            }
           // $scope.$broadcast('validate', authService.authentication.isAuth);
        });
}]);


app.directive('ngFlatDatepicker', ngFlatDatepickerDirective);

function ngFlatDatepickerDirective($templateCache, $compile, $document, datesCalculator) {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            config: '=datepickerConfig'
        },
        link: function(scope, element, attrs, ngModel) {

            var template     = angular.element($templateCache.get('datepicker.html'));
            var dateSelected = '';
            var today        = moment().tz('America/Toronto');

            // Default options
            var defaultConfig = {
                allowFuture: true,
                dateFormat: 'mm DD YYYY',
                minDate: null,
                maxDate: null
            };

            // Apply and init options
            scope.config = angular.extend(defaultConfig, scope.config);
            if (angular.isDefined(scope.config.minDate)) moment(scope.config.minDate).tz('America/Toronto').subtract(1, 'day');
            if (angular.isDefined(scope.config.maxDate)) moment(scope.config.maxDate).tz('America/Toronto').add(1, 'day');

            // Data
            scope.calendarCursor  = today;
            scope.currentWeeks    = [];
            scope.daysNameList    = datesCalculator.getDaysNames();
            scope.monthsList      = moment.months();
            scope.yearsList       = datesCalculator.getYearsList();

            // Display
            scope.pickerDisplayed = false;

            scope.$watch(function(){ return ngModel.$modelValue; }, function(value){
                if (value) {
                    dateSelected = scope.calendarCursor = moment(value, scope.config.dateFormat).tz('America/Toronto');
                }
            });

            scope.$watch('calendarCursor', function(val){
                scope.currentWeeks = getWeeks(val);
            });

            /**
             * ClickOutside, handle all clicks outside the DatePicker when visible
             */
            element.bind('click', function(e) {
                scope.$apply(function(){
                    scope.pickerDisplayed = true;
                    $document.on('click', onDocumentClick);
                });
            });

            function onDocumentClick(e) {
                if (template !== e.target && !template[0].contains(e.target) && e.target !== element[0]) {
                    $document.off('click', onDocumentClick);
                    scope.$apply(function () {
                        scope.calendarCursor = dateSelected ? dateSelected : today;
                        scope.pickerDisplayed = scope.showMonthsList = scope.showYearsList = false;
                    });
                }
            }

            init();

            /**
             * Display the previous month in the datepicker
             * @return {}
             */
            scope.prevMonth = function() {
                scope.calendarCursor = moment(scope.calendarCursor).subtract(1, 'months');
            };

            /**
             * Display the next month in the datepicker
             * @return {}
             */
            scope.nextMonth = function nextMonth() {
                scope.calendarCursor = moment(scope.calendarCursor).add(1, 'months');
            };

            /**
             * Select a month and display it in the datepicker
             * @param  {string} month The month selected in the select element
             * @return {}
             */
            scope.selectMonth = function selectMonth(month) {
                scope.showMonthsList = false;
                scope.calendarCursor = moment(scope.calendarCursor).month(month);
            };

            /**
             * Select a year and display it in the datepicker depending on the current month
             * @param  {string} year The year selected in the select element
             * @return {}
             */
            scope.selectYear = function selectYear(year) {
                scope.showYearsList = false;
                scope.calendarCursor = moment(scope.calendarCursor).year(year);
            };

            /**
             * Select a day
             * @param  {[type]} day [description]
             * @return {[type]}     [description]
             */
            scope.selectDay = function(day) {
                if (!day.isFuture || (scope.config.allowFuture && day.isFuture)) {
                    resetSelectedDays();
                    day.isSelected = true;
                    ngModel.$setViewValue(moment.tz(moment(day.date).format('YYYY-MM-DD'), 'America/Toronto').format(scope.config.dateFormat));
                    ngModel.$render();
                    scope.pickerDisplayed = false;
                }
            };

            /**
             * Init the directive
             * @return {}
             */
            function init() {

                element.wrap('<div class="ng-flat-datepicker-wrapper"></div>');

                $compile(template)(scope);
                element.after(template);

                if (angular.isDefined(ngModel.$modelValue) && moment.isDate(ngModel.$modelValue)) {
                    scope.calendarCursor = ngModel.$modelValue;
                }
            }

            /**
             * Get all weeks needed to display a month on the Datepicker
             * @return {array} list of weeks objects
             */
            function getWeeks (date) {

                var weeks = [];
                var date = moment(date).tz('America/Toronto');
                var firstDayOfMonth = moment(date).tz('America/Toronto').date(1);
                var lastDayOfMonth  = moment(date).tz('America/Toronto').date(date.daysInMonth());

                var startDay = moment(firstDayOfMonth).tz('America/Toronto');
                var endDay   = moment(lastDayOfMonth).tz('America/Toronto');
                // NB: We use weekday() to get a locale aware weekday
                startDay = firstDayOfMonth.weekday() === 0 ? startDay.tz('America/Toronto') : startDay.weekday(0).tz('America/Toronto');
                endDay   = lastDayOfMonth.weekday()  === 6 ? endDay.tz('America/Toronto')   : endDay.weekday(6).tz('America/Toronto');

                var currentWeek = [];

                for (var start = moment(startDay).tz('America/Toronto'); start.isBefore(moment(endDay).tz('America/Toronto').add(1, 'days')); start.add(1, 'days')) {

                    var afterMinDate  = !scope.config.minDate || start.isAfter(scope.config.minDate, 'day');
                    var beforeMaxDate = !scope.config.maxDate || start.isBefore(scope.config.maxDate, 'day');
                    var isFuture      = start.isAfter(today);
                    var beforeFuture  = scope.config.allowFuture || !isFuture;

                    var day = {
                        date: new Date(moment(start).tz('America/Toronto').format('YYYY-MM-DD HH:mm')),
                        isToday: start.isSame(today, 'day'),
                        isInMonth: start.isSame(firstDayOfMonth, 'month'),
                        isSelected: start.isSame(dateSelected, 'day'),
                        isSelectable: afterMinDate && beforeMaxDate && beforeFuture
                    };

                    currentWeek.push(day);

                    if (start.weekday() === 6 || start === endDay) {
                        weeks.push(currentWeek);
                        currentWeek = [];
                    }
                }

                return weeks;
            }

            /**
             * Reset all selected days
             */
            function resetSelectedDays () {
                scope.currentWeeks.forEach(function(week, wIndex){
                    week.forEach(function(day, dIndex){
                        scope.currentWeeks[wIndex][dIndex].isSelected = false;
                    });
                });
            }
        }
    };
}
ngFlatDatepickerDirective.$inject = ["$templateCache", "$compile", "$document", "datesCalculator"];

app.factory('datesCalculator', datesCalculator);
function datesCalculator () {

        /**
         * List all years for the select
         * @return {[type]} [description]
         */
        function getYearsList() {
            var yearsList = [];
            for (var i = 2005; i <= moment().year(); i++) {
                yearsList.push(i);
            }
            return yearsList;
        }

        /**
         * List all days name in the current locale
         * @return {[type]} [description]
         */
        function getDaysNames () {
            var daysNameList = [];
            for (var i = 0; i < 7 ; i++) {
                daysNameList.push(moment().weekday(i).format('ddd'));
            }
            return daysNameList;
        }

        return {
            getYearsList: getYearsList,
            getDaysNames: getDaysNames
        };
    }

app.run(["$templateCache", function($templateCache) {
    $templateCache.put("datepicker.html", "<div class=\"ng-flat-datepicker\" ng-show=\"pickerDisplayed\">\n    <div class=\"ng-flat-datepicker-table-header-bckgrnd\"></div>\n    <table>\n        <caption>\n            <div class=\"ng-flat-datepicker-header-wrapper\">\n                <span class=\"ng-flat-datepicker-arrow ng-flat-datepicker-arrow-left\" ng-click=\"prevMonth()\">\n                    <svg version=\"1.0\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"50\" y=\"50\" viewBox=\"0 0 100 100\" xml:space=\"preserve\">\n                        <polygon points=\"64.8,36.2 35.2,6.5 22.3,19.4 51.9,49.1 22.3,78.8 35.2,91.7 77.7,49.1\" />\n                    </svg>\n                </span>\n                <div class=\"ng-flat-datepicker-header-year\">\n                    <div class=\"ng-flat-datepicker-custom-select-box\" outside-click=\"showMonthsList = false\">\n                        <span class=\"ng-flat-datepicker-custom-select-title ng-flat-datepicker-month-name\" ng-click=\"showMonthsList = !showMonthsList; showYearsList = false\" ng-class=\"{selected: showMonthsList }\">{{ calendarCursor.isValid() ? calendarCursor.format(\'MMMM\') : \"\" }}</span>\n                        <div class=\"ng-flat-datepicker-custom-select\" ng-show=\"showMonthsList\">\n                            <span ng-repeat=\"monthName in monthsList\" ng-click=\"selectMonth(monthName); showMonthsList = false\">{{ monthName }}</span>\n                        </div>\n                    </div>\n                    <div class=\"ng-flat-datepicker-custom-select-box\" outside-click=\"showYearsList = false\">\n                        <span class=\"ng-flat-datepicker-custom-select-title\" ng-click=\"showYearsList = !showYearsList; showMonthsList = false\" ng-class=\"{selected: showYearsList }\">{{ calendarCursor.isValid() ? calendarCursor.format(\'YYYY\') : \"\" }}</span>\n                        <div class=\"ng-flat-datepicker-custom-select\" ng-show=\"showYearsList\">\n                            <span ng-repeat=\"yearNumber in yearsList\" ng-click=\"selectYear(yearNumber)\">{{ yearNumber }}</span>\n                        </div>\n                    </div>\n                </div>\n                <span class=\"ng-flat-datepicker-arrow ng-flat-datepicker-arrow-right\" ng-click=\"nextMonth()\">\n                    <svg version=\"1.0\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"50\" y=\"50\" viewBox=\"0 0 100 100\" xml:space=\"preserve\">\n                        <polygon points=\"64.8,36.2 35.2,6.5 22.3,19.4 51.9,49.1 22.3,78.8 35.2,91.7 77.7,49.1\" />\n                    </svg>\n                </span>\n            </div>\n        </caption>\n        <tbody>\n            <tr class=\"days-head\">\n                <td class=\"day-head\" ng-repeat=\"dayName in daysNameList\">{{ dayName }}</td>\n            </tr>\n            <tr class=\"days\" ng-repeat=\"week in currentWeeks\">\n                <td ng-repeat=\"day in week\" ng-click=\"selectDay(day)\" ng-class=\"[\'day-item\', { \'isToday\': day.isToday, \'isInMonth\': day.isInMonth, \'isDisabled\': !day.isSelectable, \'isSelected\': day.isSelected }]\">{{ day.date | date:\'dd\' }}</td>\n            </tr>\n        </tbody>\n    </table>\n</div>\n");
}]);


    