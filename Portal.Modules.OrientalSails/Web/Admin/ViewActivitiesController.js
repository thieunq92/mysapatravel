moduleViewActivities.controller("userController", ["$rootScope", "$scope", "$http", function ($rootScope, $scope, $http) {
    $scope.user = null;

    var userId = getParameterValues("ui");
    if (userId != null) {
        $http({
            method: "POST",
            url: "WebMethod/ViewActivitiesWebMethod.asmx/UserGetById",
            data: { "ui": userId }
        }).then(function (response) {
            $scope.user = JSON.parse(response.data.d);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })
    }
}])

moduleViewActivities.controller("activitiesController", ["$rootScope", "$scope", "$http", function ($rootScope, $scope, $http) {
    var from = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
    var to = new Date();
    to.setDate(from.getDate() + 7);
    $scope.from = ('0' + from.getDate()).slice(-2) + "/" + ('0' + (from.getMonth() + 1)).slice(-2) + "/" + from.getFullYear();
    $scope.to = ('0' + to.getDate()).slice(-2) + "/" + ('0' + (to.getMonth() + 1)).slice(-2) + "/" + to.getFullYear();

    function loadData() {
        $scope.listMeeting = [];
        $scope.listBooking = [];
        $scope.listSeries = [];
        $scope.listAgency = [];

        var userId = getParameterValues("ui");
        $http({
            method: "POST",
            url: "WebMethod/ViewActivitiesWebMethod.asmx/MeetingGetAllBy",
            data: {
                "ui": userId,
                "f": $scope.from,
                "t": $scope.to,
            }
        }).then(function (response) {
            $scope.listMeeting = JSON.parse(response.data.d);
            $(document).ready(function () {
                $('article').readmore({
                    collapsedHeight: 50,
                });
            });
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })

        $http({
            method: "POST",
            url: "WebMethod/ViewActivitiesWebMethod.asmx/BookingGetAllBy",
            data: {
                "ui": userId,
                "f": $scope.from,
                "t": $scope.to,
            }
        }).then(function (response) {
            $scope.listBooking = JSON.parse(response.data.d);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })

        $http({
            method: "POST",
            url: "WebMethod/ViewActivitiesWebMethod.asmx/SeriesGetAllBy",
            data: {
                "ui": userId,
                "f": $scope.from,
                "t": $scope.to,
            }
        }).then(function (response) {
            $scope.listSeries = JSON.parse(response.data.d);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })

        $http({
            method: "POST",
            url: "WebMethod/ViewActivitiesWebMethod.asmx/AgencyGetAllBy",
            data: {
                "ui": userId,
                "f": $scope.from,
                "t": $scope.to,
            }
        }).then(function (response) {
            $scope.listAgency = JSON.parse(response.data.d);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })
    }

    loadData();
    $scope.reload = function () {
        loadData();
    }
}])
