moduleBookingReport.controller("expenseController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", "$interval"
    , function ($rootScope, $scope, $http, $location, $filter, $timeout, $interval) {
        $scope.listAllCruiseExpenseDTO = [];
        $scope.getListAllCruiseExpenseDTO = function () {
            $http({
                method: "POST",
                url: "WebMethod/BookingReportWebService.asmx/GetListAllCruiseExpenseDTO",
                data: {
                    d: $scope.date,
                }
            }).then(function (response) {
                $scope.listAllCruiseExpenseDTO = JSON.parse(response.data.d);
                setCruiseTabColor();
            }, function (response) {
                alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            })
        }
        $scope.listCruiseExpenseDTOOrigin = [];
        $rootScope.listCruiseExpenseDTO = [];
        $scope.processAjaxGetListCruiseExpenseDTO = function () {
            return $http({
                method: "POST",
                url: "WebMethod/BookingReportWebService.asmx/GetListCruiseExpenseDTO",
                data: {
                    d: $scope.date,
                    ci: $scope.cruiseId,
                }
            });
        }
        $scope.getListCruiseExpenseDTO = function () {
            $scope.processAjaxGetListCruiseExpenseDTO().then(function (response) {
                $rootScope.listCruiseExpenseDTO = JSON.parse(response.data.d);
                $.each($rootScope.listCruiseExpenseDTO, function (i, e) {
                    var cruiseExpenseDTO = $rootScope.listCruiseExpenseDTO[i];
                    $rootScope.listCruiseExpenseDTO[i].getExpenseLockStatus = function () {
                        for (var i = 0; i < cruiseExpenseDTO.ListGuideExpenseDTO.length; i++) {
                            if (cruiseExpenseDTO.ListGuideExpenseDTO[i].LockStatus == "Unlocked") {
                                return "Unlocked";
                            }
                        }
                        for (var i = 0; i < cruiseExpenseDTO.ListOthersExpenseDTO.length; i++) {
                            if (cruiseExpenseDTO.ListOthersExpenseDTO[i].LockStatus == "Unlocked") {
                                return "Unlocked";
                            }
                        }
                        return "Locked";
                    }
                })
                doWhenPerformEvent();
            }, function (response) {
                alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            })
        }
        $scope.addGuideExpenseDTO = function (cruise) {
            cruise.ListGuideExpenseDTO.push(
                {
                    Id: -1,
                    GuideId: -1,
                    GuideName: "",
                    GuidePhone: "",
                    Cost: "0",
                    Operator_FullName: $scope.newExpense_OperatedName,
                    Operator_UserId: $scope.newExpense_OperatedId,
                    Operator_Phone: $scope.newExpense_OperatedPhone,
                    Date: $scope.date,
                    LockStatus: "Unlocked",
                });
            doWhenPerformEvent();
        }
        $scope.addOthersExpenseDTO = function (cruise) {
            cruise.ListOthersExpenseDTO.push(
                {
                    Id: -1,
                    Name: "",
                    Phone: "",
                    Cost: "0",
                    Operator_FullName: $scope.newExpense_OperatedName,
                    Operator_UserId: $scope.newExpense_OperatedId,
                    Operator_Phone: $scope.newExpense_OperatedPhone,
                    Date: $scope.date,
                    LockStatus: "Unlocked",
                });
            doWhenPerformEvent();
        }
        $scope.removeGuideExpenseDTO = function (cruise, index) {
            if (cruise.ListGuideExpenseDTO[index].Id > 0) {
                cruise.ListDeletedGuideExpenseDTO.push(cruise.ListGuideExpenseDTO[index]);
            }
            cruise.ListGuideExpenseDTO.splice(index, 1);
            doWhenPerformEvent();
        }
        $scope.removeOthersExpenseDTO = function (cruise, index) {
            if (cruise.ListOthersExpenseDTO[index].Id > 0) {
                cruise.ListDeletedOthersExpenseDTO.push(cruise.ListOthersExpenseDTO[index]);
            }
            cruise.ListOthersExpenseDTO.splice(index, 1);
            doWhenPerformEvent();
        }
        function setToolTip() {
            $timeout(function () {
                $('[data-toggle="tooltip"]').tooltip();
            }, 0)
        }
        function setInputMask() {
            $timeout(function () {
                $("[data-control='inputmask']").inputmask({
                    'alias': 'numeric',
                    'groupSeparator': ',',
                    'autoGroup': true,
                    'digits': 2,
                    'digitsOptional': true,
                    'rightAlign': false
                })
                $('input[type="text"]').keydown(function () {
                    $(this).trigger('input');
                    $(this).trigger('change');
                });
            }, 0);
        }
        function setPhoneInputMask() {
            $timeout(function () {
                $("[data-control='phoneinputmask']").inputmask({
                    'mask': '9999.999.9999',
                    'placeholder': '',
                });
            }, 0);
        }
        function setPhoneFormat() {
            $timeout(function () {
                $(".phone").each(function (i, e) {
                    $(e).html(formatPhoneNumber($(e).html()));
                });
            }, 0);
        }
        function formatPhoneNumber(phoneNumberString) {
            var cleaned = ('' + phoneNumberString).replace(/\D/g, '')
            var match = cleaned.match(/(\d{4})(\d{3})(\d{1,})$/)
            if (match) {
                return '' + match[1] + '.' + match[2] + '.' + match[3]
            }
            return null
        }
        function setHistoryButton() {
            $timeout(function () {
                $('a[data-target = ".modal-expenseHistory"]').click(function () {
                    $(".modal-expenseHistory iframe").attr('src', $(this).attr('data-url'))
                })
            }, 0);
        }
        function setCruiseTabColor() {
            $timeout(function () {
                $("#cruiseTab a").each(function (i, e) {
                    var cruiseId = $(e).attr("data-cruiseid");
                    var pax = $(e).attr("data-pax");
                    if (typeof (cruiseId) == "undefined") {
                        return true;
                    }
                    for (var i = 0; i < $scope.listAllCruiseExpenseDTO.length; i++) {
                        if (cruiseId == $scope.listAllCruiseExpenseDTO[i].Id && pax > 0) {
                            if ($scope.listAllCruiseExpenseDTO[i].ListGuideExpenseDTO.length <= 0) {
                                $(this).addClass("custom-danger");
                            } else {
                                $(this).removeClass("custom-danger");
                            }
                        }
                    }
                    for (var i = 0; i < $scope.listCruiseExpenseDTO.length; i++) {
                        if (cruiseId == $scope.listCruiseExpenseDTO[i].Id && pax > 0) {
                            if ($scope.listCruiseExpenseDTO[i].ListGuideExpenseDTO.length <= 0) {
                                $(this).addClass("custom-danger");
                            }
                            else {
                                $(this).removeClass("custom-danger");
                            }
                        }
                    }
                })
            }, 0)
        }
        $scope.processAjaxSave = function () {
            return $http({
                method: "POST",
                url: "WebMethod/BookingReportWebService.asmx/Save",
                data: {
                    "listCruiseExpenseDTO": $rootScope.listCruiseExpenseDTO,
                }
            })
        }
        $scope.save = function () {
            $scope.processAjaxSave().then(function (response) {
                location.reload(true);
            }, function (response) {
                alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            })
        }
        $scope.processAjaxLockDate = function () {
            return $http({
                method: "POST",
                url: "WebMethod/BookingReportWebService.asmx/LockDate",
                data: {
                    "listCruiseExpenseDTO": $rootScope.listCruiseExpenseDTO,
                }
            });
        }
        $scope.lockDate = function () {
            $scope.processAjaxSave().then(function (response) {
                $scope.processAjaxGetListCruiseExpenseDTO().then(function (response) {
                    $rootScope.listCruiseExpenseDTO = JSON.parse(response.data.d);
                    $scope.processAjaxLockDate().then(function (response) {
                        setTimeout(function () {
                            __doPostBack($("#btnLockDate").attr("data-uniqueid"), "OnClick");
                        }, 0);
                    }, function (response) {
                        alert("Request failed. Please reload and try again. Message:" + response.data.Message);
                    })
                }, function (response) {
                    alert("Request failed. Please reload and try again. Message:" + response.data.Message);
                })
            }, function (response) {
                alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            })
        }
        $scope.processAjaxUnlockDate = function () {
            return $http({
                method: "POST",
                url: "WebMethod/BookingReportWebService.asmx/UnlockDate",
                data: {
                    "listCruiseExpenseDTO": $rootScope.listCruiseExpenseDTO,
                }
            });
        }
        $scope.unlockDate = function () {
            $scope.processAjaxUnlockDate().then(function (response) {
                setTimeout(function () {
                    __doPostBack($("#btnUnlockDate").attr("data-uniqueid"), "OnClick");
                }, 0);
            }, function (response) {
                alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            })
        }
        $scope.getExpenseLockStatus = function () {
            var haveAnyExpense = false;
            for (var i = 0; i < $rootScope.listCruiseExpenseDTO.length; i++) {
                if ($rootScope.listCruiseExpenseDTO[i].ListGuideExpenseDTO.length > 0) {
                    haveAnyExpense = true;
                }
                if ($rootScope.listCruiseExpenseDTO[i].ListOthersExpenseDTO.length > 0) {
                    haveAnyExpense = true;
                }
                if (!haveAnyExpense) {
                    return "Unlocked"
                } else {
                    if ($rootScope.listCruiseExpenseDTO[i].getExpenseLockStatus() == "Unlocked") {
                        return "Unlocked";
                    }
                }
            }
            return "Locked";
        }
        $scope.setExportAllVisible = function () {
            var haveAnyGuideExpense = false;
            for (var i = 0; i < $scope.listCruiseExpenseDTO.length; i++) {
                if ($scope.listCruiseExpenseDTO[i].ListGuideExpenseDTO.length > 0) {
                    haveAnyGuideExpense = true;
                }
            }
            if (haveAnyGuideExpense) {
                return true;
            }
            return false;
        }
        try {
            if (typeof (btnSaveClicked) != undefined && btnSaveClicked == true) {
                $scope.save();
            }
        } catch (ex) { }
        function doWhenPerformEvent() {
            setInputMask();
            setPhoneInputMask();
            setHistoryButton();
            setToolTip();
            setPhoneFormat();
            setCruiseTabColor();
        }
        $scope.processAjaxBusByDateExport = function (cruiseExpense,guideExpense) {
            return $http({
                method: "POST",
                url: "WebMethod/BookingReportWebService.asmx/ExportTourByCruiseAndGuide",
                data: {
                    ci: cruiseExpense.Id,
                    gi: guideExpense.GuideId,
                    d: $scope.date,
                    ob: guideExpense.Operator_FullName,
                    op: guideExpense.Operator_Phone,
                },
                headers: { 'Content-type': 'application/json' },
                responseType: 'arraybuffer',
            });
        }
        $scope.ExportTourByCruiseAndGuide = function (cruiseExpense, guideExpense) {
            if (guideExpense == 'undefined' || guideExpense.Id < 0) {
                alert('Expense is not saved yet. Please save expense first before export');
                return false;
            }
            $scope.processAjaxBusByDateExport(cruiseExpense, guideExpense).then(function (response) {
                var blob = new Blob([response.data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                var fileName = "Tour command" + " - " + $scope.date + " - " + cruiseExpense.Name + " - "
                + guideExpense.GuideName;
                saveAs(blob, fileName);
            }, function (response) {
                alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            })
        }
        $scope.processAjaxExportTourAll = function () {
            return $http({
                method: "POST",
                url: "WebMethod/BookingReportWebService.asmx/ExportTourAll",
                data: {
                    d: $scope.date,
                },
                headers: { 'Content-type': 'application/json' },
                responseType: 'arraybuffer',
            });
        }
        $scope.exportTourAll = function () {
            $scope.processAjaxExportTourAll().then(function (response) {
                var blob = new Blob([response.data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                var fileName = "Tour command" + " - " + $scope.date + " - " + "All Cruise";
                saveAs(blob, fileName);
            }, function (response) {
                alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            })
        }
    }])
