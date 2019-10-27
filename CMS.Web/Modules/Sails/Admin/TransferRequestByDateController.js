moduleTransferRequestByDate.controller("controllerTransferRequestByDate", ["$rootScope", "$scope", "$http", "$timeout","$compile", "$filter", function ($rootScope, $scope, $http, $timeout, $compile, $filter) {
    $rootScope.transferRequestDTO = {}
    $scope.processAjaxTransferRequestDTOGetByCriterion = function(){
        return $http({
            method: "POST",
            url: "WebMethod/TransferRequestByDateWebService.asmx/TransferRequestDTOGetByCriterion",
            data: {
                d: $scope.Date,
                bti: $scope.BusTypeId,
                ri: $scope.RouteId,
            }
        });
    }
    $scope.transferRequestDTOGetByCriterion = function(){
        $scope.processAjaxTransferRequestDTOGetByCriterion().then(function(response){
            $rootScope.transferRequestDTO = JSON.parse(response.data.d);
            setLinkModalTransportBooking();
            setToolTip();
            setPhoneInputMask();
            setPhoneFormat();
            setCollapse();
            setSticky();
        },function(response){
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        });
    }
    $scope.addBusByDate = function (busTypeDTO) {
        var busByDateDTO = {};
        busByDateDTO.Id = -1;
        busByDateDTO.Group = getGroupForNewBusByDate(busTypeDTO);
        busByDateDTO.Adult = 0;
        busByDateDTO.Child = 0;
        busByDateDTO.Baby = 0;
        busByDateDTO.PaxString = "0 pax";
        busByDateDTO.Deleted = false;
        busByDateDTO.BusByDatesGuidesDTO = [];
        busByDateDTO.BusByDatesGuidesDTO.push(
            {
                GuideDTO:
                    {
                        Id:null
                    }
            });
        busTypeDTO.ListBusByDateDTO.push(busByDateDTO);
        setLinkModalTransportBooking();
        setToolTip();
        setPhoneInputMask();
        setPhoneFormat();
    }
    $scope.deleteBusByDate = function (busByDateDTO) {
        busByDateDTO.Deleted = true;
        $scope.supplierBindBusInDate(busByDateDTO);
    }
    function getGroupForNewBusByDate(busTypeDTO) {
        groupNeed = 1;
        var listBusByDateDTO =  busTypeDTO.ListBusByDateDTO.filter(x=>x.Deleted == false).sort(function(a,b){
            if(a.Group < b.Group){
                return -1;
            }
            if(a.Group > b.Group){
                return 1;
            }
            return 0;
        });
        listBusByDateDTO = removeDuplicate(listBusByDateDTO,"Group");
        for (var i = 0 ; i < listBusByDateDTO.length; i++) {
            var groupExist = listBusByDateDTO[i].Group;
            if (groupNeed == groupExist) {
                groupNeed++;
            }else{
                break;
            }
        }
        return groupNeed;
    }
    function removeDuplicate(array, prop){       
        return Array.from(new Map(array.map(i => [(prop in i) ? i[prop] : i, i])).values());
    }
    function setLinkModalTransportBooking(){
        $timeout(function(){
            $('a[data-target = ".modal-transportbooking"]').click(function () {
                $(".modal-transportbooking iframe").attr('src', $(this).attr('data-url'))
            })
        },0);
    }
    function setToolTip(){
        $timeout(function(){
            $('[data-toggle="tooltip"]').tooltip();
        })
    }
    function setSticky(){
        $timeout(function(){
            $('.sticky').stick_in_parent({ parent: '.container-fluid', recalc_every: 1 });
        })
    }
    function setCollapse(){
        $timeout(function(){
            $('[data-toggle="collapse"').click(function () {
                if ($(this).hasClass('collapsed')) {
                    $(this).find('i').addClass('fa-minus');
                    $(this).find('i').removeClass('fa-plus');
                    $(this).find('i').attr('data-original-title','Hide');
                } else {
                    $(this).find('i').removeClass('fa-minus');
                    $(this).find('i').addClass('fa-plus');
                    $(this).find('i').attr('data-original-title','Show');
                }
            });
        })
    }
    $scope.processSupplier_AgencyGetAllByRole = function(){
        return $http({
            method: "POST",
            url: "WebMethod/TransferRequestByDateWebService.asmx/Supplier_AgencyGetAllByRole",
            data: {
            }
        });
    }
    $scope.ListSupplierDTOTo=[];
    $scope.ListSupplierDTOBack=[];
    $scope.Supplier_AgencyGetAllByRole = function(){
        $scope.processSupplier_AgencyGetAllByRole().then(function(response){
            $scope.ListSupplierDTOTo = JSON.parse(response.data.d);
            for(var i = 0; i< $scope.ListSupplierDTOTo.length; i++){
                if($scope.ListSupplierDTOTo[i].Group == null){
                    $scope.ListSupplierDTOTo[i].Group = undefined;
                }
            }
            $scope.ListSupplierDTOBack = JSON.parse(response.data.d);
            for(var i = 0; i< $scope.ListSupplierDTOBack.length; i++){
                if($scope.ListSupplierDTOBack[i].Group == null){
                    $scope.ListSupplierDTOBack[i].Group = undefined;
                }
            }
        },function(response){
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        });
    }
    $scope.supplierBindBusInDate = function(busByDateDTO){
        var selectedSupplierId = busByDateDTO.SupplierId;
        var copy = angular.copy($scope.ListSupplierDTOBack);
        var selectedSupplierIndexInListCopy = copy.findIndex( x => x.Id === selectedSupplierId)
        copy[selectedSupplierIndexInListCopy] != undefined 
        ? copy[selectedSupplierIndexInListCopy].Group = 'Supplier have bus in day'
        : true;
        if(busByDateDTO.oldSelectedSupplierId != undefined){
            copy[copy.findIndex( x => x.Id === busByDateDTO.oldSelectedSupplierId )].Group = undefined;
        }
        $scope.ListSupplierDTOBack = copy;
        busByDateDTO.oldSelectedSupplierId = selectedSupplierId;
        if(busByDateDTO.Deleted){
            copy[copy.findIndex( x => x.Id === selectedSupplierId )].Group = undefined;
        }
    }
    $scope.processGuide_AgencyGetAllByRole = function(way){
        return $http({
            method: "POST",
            url: "WebMethod/TransferRequestByDateWebService.asmx/Guide_AgencyGetAllByRole",
            data: {
                d:$scope.Date,
                ri: $scope.RouteId,
                w:way,
            }
        });
    }
    $scope.ListGuideDTOTo=[];
    $scope.ListGuideDTOBack=[];
    $scope.Guide_AgencyGetAllByRole = function(way){
        $scope.processGuide_AgencyGetAllByRole(way).then(function(response){
            if(way=="To"){
                $scope.ListGuideDTOTo = JSON.parse(response.data.d);
                for(var i = 0; i< $scope.ListGuideDTOTo.length; i++){
                    if($scope.ListGuideDTOTo[i].Group == null){
                        $scope.ListGuideDTOTo[i].Group = undefined;
                    }
                }       
            }
            if(way=="Back"){
                $scope.ListGuideDTOBack = JSON.parse(response.data.d);
                for(var i = 0; i< $scope.ListGuideDTOBack.length; i++){
                    if($scope.ListGuideDTOBack[i].Group == null){
                        $scope.ListGuideDTOBack[i].Group = undefined;
                    }
                }       
            }
        },function(response){
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        });
    }
    $scope.processAjaxGuidePhone_AgencyGetById = function(guideDTO){;
        var guideDTOId = null;
        if(guideDTO != undefined && guideDTO != null){
            guideDTOId = guideDTO.Id;
        }
        return $http({
            method: "POST",
            url: "WebMethod/TransferRequestByDateWebService.asmx/GuidePhone_AgencyGetById",
            data: {
                gi:guideDTOId
            }
        });
    }
    $scope.guidePhone_AgencyGetById = function(guideDTO){
        $scope.processAjaxGuidePhone_AgencyGetById(guideDTO).then(function(response){
            try{
                guideDTO.Phone = JSON.parse(response.data.d);
            }catch(e){}
            setPhoneFormat();
        },function(response){
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        });
    }
    $scope.processAjaxGuideName_AgencyGetById = function(guideDTO){
        var guideDTOId = null;
        if(guideDTO != undefined && guideDTO != null){
            guideDTOId = guideDTO.Id;
        }
        return $http({
            method: "POST",
            url: "WebMethod/TransferRequestByDateWebService.asmx/GuideName_AgencyGetById",
            data: {
                gi:guideDTOId
            }
        });
    }
    $scope.guideName_AgencyGetById = function(guideDTO){  
        $scope.processAjaxGuideName_AgencyGetById(guideDTO).then(function(response){
            try{
                guideDTO.Name= JSON.parse(response.data.d);
            }catch(e){}
        },function(response){
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        });
    }
    function setPhoneInputMask() {
        $timeout(function () {
            $("[data-control='phoneinputmask']").inputmask({
                'mask': '9999.999.9999',
                'placeholder': '',
            });
        }, 0);
    }
    $scope.processAjaxBusByDateExport = function(busByDateDTO){
        return $http({
            method:"POST",
            url: "WebMethod/TransferRequestByDateWebService.asmx/BusByDateExport",
            data: {
                bbdi : busByDateDTO.Id
            },
            headers:{'Content-type': 'application/json'},
            responseType : 'arraybuffer',
        });
    }
    $scope.busByDateExport = function(busByDateDTO){    
        if (busByDateDTO == 'undefined' || busByDateDTO.Id < 0) {
            alert('Group is not saved yet. Please save group first before export');
            return false;
        }
        $scope.processAjaxBusByDateExport(busByDateDTO).then(function(response){
            var blob = new Blob([response.data], {type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"});
            var fileName = "Tour Command & Welcome board" + " - " + $scope.Date + " - " + busByDateDTO.RouteName.replace("-","_")
                + " - " 
                + busByDateDTO.BusTypeName + " - " + "Group " + busByDateDTO.Group+".xlsx";
            saveAs(blob, fileName);
        },function(response){
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        });
    }
    function formatPhoneNumber(phoneNumberString) {
        var cleaned = ('' + phoneNumberString).replace(/\D/g, '')
        var match = cleaned.match(/(\d{4})(\d{3})(\d{1,})$/)
        if (match) {
            return '' + match[1] + '.' + match[2] + '.' + match[3]
        }
        return null
    }
    function setPhoneFormat(){
        $timeout(function () {
            $(".phone").each(function (i, e) {
                $(e).html(formatPhoneNumber($(e).html()));
            });
        }, 0);
    }
    $scope.busByDateCheckIsSaved = function(busByDateDTO){
        if (busByDateDTO == 'undefined' || busByDateDTO.Id < 0) {
            alert('Group is not saved yet. Please save group first before view');
            $('.modal-transportbooking').on('show.bs.modal', function (e) {
                e.preventDefault();
                e.stopImmediatePropagation(); 
            })
        }
    }
    $scope.addGuide = function(busByDateDTO){
        var guideDTO = {};
        guideDTO.GuideDTO = {};
        guideDTO.GuideDTO.Id = null;
        busByDateDTO.BusByDatesGuidesDTO.push(guideDTO);
        setPhoneFormat();
    }
    $scope.removeGuide = function(busByDateDTO,index){
        busByDateDTO.BusByDatesGuidesDTO.splice(index, 1);
    }
}])
moduleTransferRequestByDate.controller("controllerFunction",["$rootScope", "$scope", "$http", "$timeout", function ($rootScope, $scope, $http, $timeout) {
    $scope.processAjaxSave = function(){
        return $http({
            method: "POST",
            url: "WebMethod/TransferRequestByDateWebService.asmx/TransferRequestDTOSaveOrUpdate",
            data: {
                tr : $rootScope.transferRequestDTO,
                d : $scope.Date,
            }
        });
    }
    $scope.save = function () {
        $scope.processAjaxSave().then(function (response) {
            setTimeout(function () {
                __doPostBack($("#btnSave").attr("data-uniqueid"), "OnClick");
            }, 1);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })
    }
    $scope.processAjaxBusByDateExport = function(){
        return $http({
            method:"POST",
            url: "WebMethod/TransferRequestByDateWebService.asmx/BusByDateExportAll",
            data: {
                ri : $scope.RouteId,
                bti : $scope.BusTypeId,
                d : $scope.Date,
            },
            headers:{'Content-type': 'application/json'},
            responseType : 'arraybuffer',
        });
    }
    $scope.busByDateExport = function(){
        $scope.processAjaxBusByDateExport().then(function (response) {
            var blob = new Blob([response.data], {type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"});
            var fileName = "Tour command & Welcome board" + " - " + $scope.Date + " - " + $scope.RouteName + " - "
            + $scope.BusTypeName +".xlsx";
            saveAs(blob, fileName);
        }, function (response) {
            alert("Request failed. Please reload and try again. Message:" + response.data.Message);
        })
    }
}])
moduleTransferRequestByDate.controller("controllerModalTransportBooking",["$rootScope", "$scope", "$http", "$timeout", function ($rootScope, $scope, $http, $timeout) {
    
}])