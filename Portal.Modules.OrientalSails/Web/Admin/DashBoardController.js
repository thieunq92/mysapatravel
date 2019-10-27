
moduleDashBoard.controller("getActivityByIdController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
    $scope.getActivityById = function (activityId) {
        $http({
            method: "POST",
            url: "WebMethod/DashBoardWebService.asmx/ActivityGetById",
            data: {
                activityId: activityId,
            },
        }).then(function (response) {
            $rootScope.Activity = JSON.parse(response.data.d);
            var hidActivityIdClientId = $("#hidActivityIdClientId").attr("data-hidActivityIdClientId");
            $('#' + hidActivityIdClientId).val($rootScope.Activity.Id);
            $('[data-id = "txtNote"]').val($rootScope.Activity.Note);
            $('[data-id = "chkNeedManagerAttention"] input').prop("checked", $rootScope.Activity.NeedManagerAttention);
            $('[data-id = "ddlType"]').val($rootScope.Activity.Type);
            if ($('[data-id = "ddlType"]').val() == 'Meeting') $('#problem-group').hide();
            if ($('[data-id = "ddlType"]').val() == 'Problem Report') $('#problem-group').show();
            var dateMeeting = new Date($rootScope.Activity.DateMeeting);
            $('[data-id = "txtDateMeeting"]').val($.datepicker.formatDate('dd/mm/yy', dateMeeting));
            var problems = [];
            if ($rootScope.Activity.Problems != null) problems = $rootScope.Activity.Problems.split(',');
            for (var i = 0; i < problems.length; i++) {
                if (problems[i] == "Food") $('[data-id = "chkFood"] input').prop("checked", true);
                if (problems[i] == "Cabin") $('[data-id = "chkCabin"] input').prop("checked", true);
                if (problems[i] == "Guide") $('[data-id = "chkGuide"] input').prop("checked", true);
                if (problems[i] == "Bus") $('[data-id = "chkBus"] input').prop("checked", true);
                if (problems[i] == "Others") $('[data-id = "chkOthers"] input').prop("checked", true);
            }
            $('[data-id ="hidGuideId"]').val($rootScope.Activity.AgencyId);
            $('[data-id ="hidGuideId"]').trigger('change')
            $(document).ajaxSuccess(function () {
                $('[data-id = "ddlContact"]').val($rootScope.Activity.ContactId);
                $(document).unbind("ajaxSuccess");
            })
            $('[data-id ="txtName"]').val($rootScope.Activity.AgencyName);
            $('[data-id ="txtPosition"]').val($rootScope.Activity.ContactPosition);
            $('[data-id ="ddlCruise"]').val($rootScope.Activity.CruiseId);
        }, function (response) {
        })
    }
}]);