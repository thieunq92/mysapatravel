
moduleAddSeriesBookings.controller("checkPermissionController", ["$rootScope", "$scope", "$http", function ($rootScope, $scope, $http) {
    $scope.displayErrorPanel = false;
    $scope.displayMainPanel = false;
    $http({
        method: "POST",
        url: "WebMethod/AddSeriesBookingsWebMethod.asmx/CheckHaveAddSeriesPermission",
        data: "",
    }).then(function (response) {
        if (JSON.parse(response.data.d) == false) {
            $scope.displayErrorPanel = true;
            $scope.displayMainPanel = false;
        } else {
            $scope.displayErrorPanel = false;
            $scope.displayMainPanel = true;
        }
    })
}])

moduleAddSeriesBookings.controller("seriesController", ["$rootScope", "$scope", "$http", "$location", function ($rootScope, $scope, $http, $location) {
    $scope.title = "Series adding";
    $scope.lblBtnSeries = "Add Series";

    $scope.show = {
        bookerLoadingIcon: false,
        createSeriesLoadingIcon: false,
        updateSeriesLoadingIcon: false,
        seriesForm: false,
        seriesPanel: false,
    }

    $rootScope.series = {
        Agency: {
            Id: null,
            Name: null,
        },
        Booker: {
            Id: null,
        },
        SeriesCode: null,
        CutoffDate: null,
        NoOfDays: "",
        SpecialRequest: null,
    };
    $scope.listBooker = null;
    $rootScope.rsShow = {
        bookingForm: true,
    }

    var seriesId = getParameterValues("si");
    if (seriesId != null) {
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/SeriesGetById",
            data: { "si": seriesId }
        }).then(function (response) {
            $rootScope.series = JSON.parse(response.data.d);
            $rootScope.rsBooking.SpecialRequest = $rootScope.series.SpecialRequest;
            var agencyName = null;
            try {
                agencyName = $rootScope.series.Agency.Name;
            } catch (e) { }

            $scope.title = "Series Code : " + $rootScope.series.SeriesCode + " - " + agencyName;
            $scope.createby = "Created by " + $rootScope.series.CreatedBy + " at " + $rootScope.series.CreatedDate;
            var agencyId = null;
            try {
                agencyId = $rootScope.series.Agency.Id;
            } catch (e) { }

            $scope.bookerGetAllByAgency(agencyId);
            $rootScope.BookingGetBySeries($rootScope.series.Id);
            $rootScope.tripGetAll($rootScope.series.NoOfDays);
            $("#ctl00_AdminContent_agencySelector").val(agencyId);
            $scope.show.seriesPanel = true;
            $rootScope.rsShow.bookingForm = true;
        }, function (response) {
            $scope.show.seriesForm = true;
            $rootScope.rsShow.bookingForm = false;
        });
    } else {
        $scope.show.seriesForm = true;
        $rootScope.rsShow.bookingForm = false;
    }

    $scope.bookerGetAllByAgency = function (agencyId) {
        $scope.show.bookerLoadingIcon = true;
        if (agencyId == null)
            agencyId = $("#ctl00_AdminContent_agencySelector").val()

        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/AgencyContactGetAllByAgency",
            data: { "ai": agencyId }
        }).then(function (response) {
            $scope.show.bookerLoadingIcon = false;
            $scope.listBooker = JSON.parse(response.data.d);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            $scope.show.bookerLoadingIcon = false;
        })
    }

    $scope.workingState = false;
    $scope.createOrUpdateSeries = function () {
        if ($scope.workingState) {
            return;
        }
        $scope.workingState = true;
        if ($("#aspnetForm").valid()) {
            if (seriesId == null) {
                createSeries();
            } else {
                updateSeries();
            }

        }
    }

    function createSeries() {
        $scope.show.createSeriesLoadingIcon = true;
        var bookerId = null;
        try {
            bookerId = $rootScope.series.Booker.Id;
        } catch (e) { }
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/CreateSeries",
            data: {
                "ai": $("#ctl00_AdminContent_agencySelector").val(),
                "bi": bookerId,
                "sc": $rootScope.series.SeriesCode,
                "cd": $rootScope.series.CutoffDate,
                "nod": $rootScope.series.NoOfDays,
                "sr": $rootScope.series.SpecialRequest,
            }
        }).then(function (response) {
            $rootScope.series = JSON.parse(response.data.d);
            $rootScope.rsBooking.SpecialRequest = $rootScope.series.SpecialRequest;
            $(".series.alert-success").show().empty().append("<strong>Success!</strong> Tạo series thành công")
            var agencyName = null;
            try {
                agencyName = $rootScope.series.Agency.Name;
            } catch (e) { }

            $scope.title = "Series Code : " + $rootScope.series.SeriesCode + " - " + agencyName;
            $scope.createby = "Created by " + $rootScope.series.CreatedBy + " at " + $rootScope.series.CreatedDate;          
            $scope.show.seriesForm = false;
            $scope.show.seriesPanel = true;
            $rootScope.rsShow.bookingForm = true;
            $rootScope.tripGetAll($rootScope.series.NoOfDays);
            window.location.replace("AddSeriesBookings.aspx?NodeId=1&SectionId=15&si=" + $rootScope.series.Id);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        }).then(function () {
            $scope.show.createSeriesLoadingIcon = false;
            $scope.workingState = false;
        })


    }

    function updateSeries() {
        $scope.show.updateSeriesLoadingIcon = true;
        var bookerId = null;
        try {
            bookerId = $rootScope.series.Booker.Id;
        } catch (e) { }
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/UpdateSeries",
            data: {
                "si": $rootScope.series.Id,
                "ai": $("#ctl00_AdminContent_agencySelector").val(),
                "bi": bookerId,
                "sc": $rootScope.series.SeriesCode,
                "cd": $rootScope.series.CutoffDate,
                "nod": $rootScope.series.NoOfDays,
                "sr": $rootScope.series.SpecialRequest,
            }
        }).then(function (response) {
            $rootScope.series = JSON.parse(response.data.d);
            $rootScope.rsBooking.SpecialRequest = $rootScope.series.SpecialRequest;
            $(".series.alert-success").show().empty().append("<strong>Success!</strong> Cập nhật series thành công")
            var agencyName = null;
            try {
                agencyName = $rootScope.series.Agency.Name;
            } catch (e) { }

            $scope.title = "Series Code : " + $rootScope.series.SeriesCode + " - " + agencyName;
            $scope.createby = "Created by " + $rootScope.series.CreatedBy + " at " + $rootScope.series.CreatedDate;
            $scope.show.seriesForm = false;
            $scope.show.seriesPanel = true;
            $rootScope.rsShow.bookingForm = true;
            $rootScope.tripGetAll($rootScope.series.NoOfDays);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);     
        }).then(function () {
            $scope.show.updateSeriesLoadingIcon = false;
            $scope.workingState = false;
        })
    }

    $scope.editSeries = function () {
        $scope.show.seriesForm = true;
        $scope.show.seriesPanel = false;
        $rootScope.rsShow.bookingForm = false;
        $scope.lblBtnSeries = "Update Series";
    }

}]);

moduleAddSeriesBookings.controller("bookingController", ["$rootScope", "$scope", "$http", function ($rootScope, $scope, $http) {
    $scope.show = {
        checkLoadingIcon: false,
        checkRoomPanel: false,
        saveBookingLoadingIcon: false,
    }

    $rootScope.rsBooking = {
        SpecialRequest: null,
        Trip: {
            Id: null,
        },
        StartDate: null,
        TACode: null,
        Cruise: {
            Id: null,
            Name: null,
        }
    }

    $rootScope.numberOfBooking = 1;
    $scope.listCheckRoomResult = [];
    $scope.listAvaiableRoom = [];
    $scope.show.selectRoomPanel = false;

    $rootScope.tripGetAll = function (noOfDays) {
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/TripGetAllByNoOfDays",
            data: {
                nod: noOfDays,
            }
        }).then(function (response) {
            $scope.listTrip = JSON.parse(response.data.d);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })
    }

    $scope.checkRoom = function () {
        $scope.show.checkLoadingIcon = true;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/CheckRoom",
            data: {
                "sd": $rootScope.rsBooking.StartDate,
                "ti": $rootScope.rsBooking.Trip.Id,
                "SectionId": 15,
                "NodeId": 1,
            }
        }).then(function (response) {
            $scope.listCheckRoomResult = JSON.parse(response.data.d);
            for (i = 0; i < $scope.listCheckRoomResult.length; i++) {
                $scope.listCheckRoomResult[i]["style"] = "success";
                if ($scope.listCheckRoomResult[i].NoOfRoomAvaiable == 0)
                    $scope.listCheckRoomResult[i]["style"] = "danger";
            }
            $scope.show.checkLoadingIcon = false;
            $scope.show.checkRoomPanel = true;
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            $scope.show.checkLoadingIcon = false;
        })
    }

    $scope.getAvaiableRoom = function (cruiseId, cruiseName) {
        $rootScope.rsBooking.Cruise.Id = cruiseId;
        $rootScope.rsBooking.Cruise.Name = cruiseName;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/GetAvaiableRoom",
            data: {
                "ci": $rootScope.rsBooking.Cruise.Id,
                "sd": $rootScope.rsBooking.StartDate,
                "ti": $rootScope.rsBooking.Trip.Id,
                "SectionId": 15,
                "NodeId": 1,
            }
        }).then(function (response) {
            var listAvaiableRoom = JSON.parse(response.data.d)
            $scope.listAvaiableRoom = listAvaiableRoom;
            for (i = 0; i < listAvaiableRoom.length; i++) {
                $scope.listAvaiableRoom[i]["selectedRoom"] = null;
                $scope.listAvaiableRoom[i]["selectedChild"] = null;
                $scope.listAvaiableRoom[i]["selectedBaby"] = null;
            }
            $scope.show.selectRoomPanel = true;
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })
    }

    $scope.workingState = false;
    $scope.saveBooking = function () {
        if ($scope.workingState) {
            return;
        }
        $scope.workingState = true;
        $scope.show.saveBookingLoadingIcon = true;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/SaveBooking",
            data: {
                "listAvaiableRoomDTO": $scope.listAvaiableRoom,
                "si": $rootScope.series.Id,
                "ci": $rootScope.rsBooking.Cruise.Id,
                "sd": $rootScope.rsBooking.StartDate,
                "ti": $rootScope.rsBooking.Trip.Id,
                "tac": $rootScope.rsBooking.TACode,
                "bsr": $rootScope.rsBooking.SpecialRequest,
            }
        }).then(function (response) {
            var booking = JSON.parse(response.data.d);
            $(".booking.alert-success").empty().show().append("<strong>Success!</strong> Tạo thành công booking <u>OS" + booking.Id + "</u>")
            $rootScope.BookingGetBySeries();
            $rootScope.numberOfBooking = $rootScope.listBooked.length + 1;
            $rootScope.rsBooking = {
                SpecialRequest: null,
                Trip: {
                    Id: null,
                },
                StartDate: null,
                TACode: null,
                Cruise: {
                    Id: null,
                    Name: null,
                }
            }
            $scope.show.checkRoomPanel = false;
            $scope.show.selectRoomPanel = false;
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);       
        }).then(function () {
            $scope.workingState = false;
            $scope.show.saveBookingLoadingIcon = false;
        });
    }

    $scope.range = function (from, count, increament) {
        var items = [];

        for (var i = from; i <= count; i = i + increament) {
            items.push(i)
        }
        return items;
    }
}]);

moduleAddSeriesBookings.controller("bookedController", ["$rootScope", "$scope", "$http", function ($rootScope, $scope, $http) {
    $rootScope.listBooked = [];
    $scope.show = {
        loadingBookingTableIcon: false,
    }
    $rootScope.BookingGetBySeries = function () {
        $scope.show.loadingBookingTableIcon = true;
        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/BookingGetBySeries",
            data: { "si": $rootScope.series.Id }
        }).then(function (response) {
            $rootScope.listBooked = JSON.parse(response.data.d);
            $rootScope.numberOfBooking = $rootScope.listBooked.length + 1;
            $scope.show.loadingBookingTableIcon = false;
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
            $scope.show.loadingBookingTableIcon = false;
        });
    }

    $scope.BookingApproved = function (bookingId) {
        var cf = confirm('Chuẩn bị approved booking này xin hãy xác nhận');
        if (!cf)
            return;

        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/BookingApproved",
            data: {
                "bi": bookingId,
            }
        }).then(function (response) {
            $rootScope.BookingGetBySeries();
            alert("Approved thành công booking OS" + bookingId)
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        });
    }


    $scope.BookingCancel = function (bookingId) {
        var cf = confirm('Chuẩn bị cancel booking này xin hãy xác nhận');
        if (!cf)
            return;

        $http({
            method: "POST",
            url: "WebMethod/AddSeriesBookingsWebMethod.asmx/BookingCancel",
            data: {
                "bi": bookingId,
            }
        }).then(function (response) {
            $rootScope.BookingGetBySeries();
            alert("Cancel thành công booking OS" + bookingId)
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        });
    }
}])

