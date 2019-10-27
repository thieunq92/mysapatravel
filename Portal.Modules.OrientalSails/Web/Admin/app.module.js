var moduleAddSeriesBookings = angular.module("moduleAddSeriesBookings", [])
    .directive('convertToNumber', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function (val) {
                    return parseInt(val, 10);
                });
                ngModel.$formatters.push(function (val) {
                    return '' + val;
                });
            }
        };
    });
var moduleViewActivities = angular.module("moduleViewActivities", []);
var moduleDocumentView = angular.module("moduleDocumentView", []);
var moduleContractCreate = angular.module("moduleContractCreate", [])
.directive('inputMask', function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs) {
            $(el).inputmask(scope.$eval(attrs.inputMask));
            $(el).on('change', function (e) {
                scope.application == scope.application || {}
                scope.application.phone = $(e.target).val();
            });
        }
    };
});
var moduleBookingReport = angular.module("moduleBookingReport", []);
var moduleTransferRequestByDate = angular.module("moduleTransferRequestByDate", []);
moduleTransferRequestByDate.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return parseInt(val, 10);
            });
            ngModel.$formatters.push(function (val) {
                return '' + val;
            });
        }
    };
});
moduleTransferRequestByDate.filter('range', function() {
    return function(input, min, max) {
        min = parseInt(min); 
        max = parseInt(max);
        for (var i=min; i<max; i++)
            input.push(i);
        return input;
    };
});
var moduleGoldenDayCreateCampaign = angular.module("moduleGoldenDayCreateCampaign", []);
var moduleDashBoard = angular.module("moduleDashBoard", []);
var moduleBookingView = angular.module("moduleBookingView", []);
var moduleViewMeetings = angular.module("moduleViewMeetings", []);
var moduleAgencyView = angular.module("moduleAgencyView", []);
angular.module("myApp",
    ["moduleAddSeriesBookings",
    "moduleViewActivities",
    "moduleDocumentView",
    "moduleContractCreate",
    "moduleBookingReport",
    "moduleTransferRequestByDate",
    "moduleBookingView",
    "moduleGoldenDayCreateCampaign",
    "moduleDashBoard",
    "moduleViewMeetings",
    "moduleAgencyView",
    "angular.filter"]);