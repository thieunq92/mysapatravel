moduleGoldenDayCreateCampaign.controller('createCampaignController', ['$rootScope', '$scope', '$http', '$timeout', function ($rootScope, $scope, $http, $timeout) {
    $scope.getCampaignById = function(campaignId){
        $http({
            method: 'POST',
            url: 'WebMethod/GoldenDayCreateCampaignWebService.asmx/CampaignGetById',
            data: { campaignId: campaignId},
        }).then(function (response) {
            $rootScope.campaign = JSON.parse(response.data.d);
            setTimeout(function(){__doPostBack($('[data-id=btnSave]').attr('data-uniqueid'),'OnClick')},1);       
            $timeout(function () {
                autosize($('textarea'));
            })
        }, function (response) {
            alert('Request failed. Please reload and try again. Message:' + response.data.Message);
        })
    }
    $scope.pageLoad = function(){
        var campaignId = 0;
        try{
            campaignId = parseInt(getParameterValues('ci'));
        }catch(ex){}
        if(!campaignId) campaignId=0
        $scope.getCampaignById(campaignId);
    }
    $scope.pageLoad();
    $scope.buttonSaveDisabled = false;
    $scope.save = function () {
        $http({
            method: 'POST',
            url: 'WebMethod/GoldenDayCreateCampaignWebService.asmx/CampaignSaveOrUpdate',
            data: { month: $scope.month, year: $scope.year },
        }).then(function (response) {
            $rootScope.campaign = JSON.parse(response.data.d);
            addParameterToUrl('ci=' + $rootScope.campaign.Id);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
                recreateDatetimePicker($rootScope.campaign);
            });    
        }, function (response) {
            alert('Request failed. Please reload and try again. Message:' + response.data.Message);
        }).finally(function(){
            $scope.buttonSaveDisabled = false;
        })
    }
}]);

moduleGoldenDayCreateCampaign.controller('createDateController', ['$rootScope', '$scope', '$http', '$timeout','$compile', function ($rootScope, $scope, $http, $timeout, $compile) {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
        var elem = angular.element(document.getElementById("upCruiseAvailable"));
        elem.replaceWith($compile(elem)($scope));
        $scope.$apply();
    });
    $scope.buttonSaveDisabled = false;
    $scope.add = function (date, policy) {
        $rootScope.campaign.GoldenDays.push({
            Date: null,
            DateAsString: date,
            Policy: policy,
        });
        $scope.date = '';
        $timeout(function () {
            autosize($('textarea'));
            recreateDatetimePicker($rootScope.campaign);
            recreateValidation();
        })
    };
    $scope.delete = function ($index) {
        $rootScope.campaign.GoldenDays.splice($index, 1);
    };
    $scope.save = function (control) {
        if ($("#aspnetForm").valid()) {
            for (var i = 0; i<$rootScope.campaign.GoldenDays.length;i++){
                $rootScope.campaign.GoldenDays[i].Date = tryParseDateFromString($rootScope.campaign.GoldenDays[i].DateAsString,'dmy');
            }
            $http({
                method: 'POST',
                url: 'WebMethod/GoldenDayCreateCampaignWebService.asmx/GoldenDaySaveOrUpdate',
                data: { campaignDTO : $rootScope.campaign },
            }).then(function (response) {
                window.location = 'GoldenDayListCampaign.aspx?NodeId=1&SectionId=15';
            }, function (response) {
                alert('Request failed. Please reload and try again. Message:' + response.data.Message);
            }).finally(function(){
                $scope.buttonSaveDisabled = false;
            });
        }
    }
    $scope.isSelected = function(dateAsString){
        return $rootScope.campaign.GoldenDays.some(gd=>gd.DateAsString.includes(dateAsString));
    }
}])
function recreateValidation(){
    $("#aspnetForm").validate({
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("help-block");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }

            if (element.siblings("span").prop("class") === "input-group-addon") {
                error.insertAfter(element.parent()).css({ color: "#a94442" });
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).closest("div").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest("div").removeClass("has-error");
        }
    });
    $('input[ng-model="goldenDay.DateAsString"]').each(function(){
        $(this).rules('add',{
            required : true,
            messages : {
                required : 'Date is required',
            }
        });
    })
}
function recreateDatetimePicker(campaign) {
    var from = false;
    var to = false;
    if (campaign != null) {
        from = new Date(campaign.Year, campaign.Month-1, 1)
        to = new Date(campaign.Year, campaign.Month, 0);
    }
    $('[data-control=datetimepicker]').datetimepicker({
        timepicker: false,
        format: 'd/m/Y',
        scrollInput: false,
        scrollMonth: false,
        defaultDate: from,
        minDate: from.getFullYear() + '/' + (from.getMonth()+1) + '/' + from.getDate(),
        maxDate: to.getFullYear() + '/' + (to.getMonth()+1) + '/' + to.getDate(),
    });
    if (jQuery(window).width() < 1000) {
        $('[data-control="datetimepicker"]').focus(function () {
            $(this).blur();
        })
    }
}

function tryParseDateFromString(dateStringCandidateValue, format = 'ymd') {
    if (!dateStringCandidateValue) { return null; }
    let mapFormat = format
            .split("")
            .reduce(function (a, b, i) { a[b] = i; return a;}, {});
    const dateStr2Array = dateStringCandidateValue.split(/[ :\-\/]/g);
    const datePart = dateStr2Array.slice(0, 3);
    let datePartFormatted = [
            +datePart[mapFormat.y],
            +datePart[mapFormat.m]-1,
            +datePart[mapFormat.d] ];
    if (dateStr2Array.length > 3) {
        dateStr2Array.slice(3).forEach(t => datePartFormatted.push(+t));
    }
    // test date validity according to given [format]
    const dateTrial = new Date(Date.UTC.apply(null, datePartFormatted));
    return dateTrial && dateTrial.getFullYear() === datePartFormatted[0] &&
           dateTrial.getMonth() === datePartFormatted[1] &&
           dateTrial.getDate() === datePartFormatted[2]
              ? dateTrial :
              null;
}