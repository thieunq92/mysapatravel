moduleContractCreate.controller("addRemoveValidTimeController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
    $rootScope.rs = { listValidTime: [{}] };
    $scope.addValidTime = function () {
        $rootScope.rs.listValidTime.push({});
        $timeout(function () {
            $("[data-control='datetimepicker']").datetimepicker({
                timepicker: false,
                format: 'd/m/Y',
                scrollInput: false,
                scrollMonth: false,
            })
        }, 0);
    };
    $scope.removeValidTime = function (index) {
        $rootScope.rs.listValidTime.splice(index, 1);
    }
}]);
moduleContractCreate.controller("os2d1nController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
    $scope.$watch("rs.listValidTime.length", function () {
        $("#os2d1nCharterHeader").attr("colspan", $rootScope.rs.listValidTime.length * 4 + 1);
    })
}]);
moduleContractCreate.controller("cls2d1nController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
    $scope.$watch("rs.listValidTime.length", function () {
        $("#cls2d1nCharterHeader").attr("colspan", $rootScope.rs.listValidTime.length * 4 + 1);
    })
}]);
moduleContractCreate.controller("stl2d1nController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
}]);
moduleContractCreate.controller("os3d2nController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
    $scope.$watch("rs.listValidTime.length", function () {
        $("#os3d2nCharterHeader").attr("colspan", $rootScope.rs.listValidTime.length * 4 + 1);
    })
}]);
moduleContractCreate.controller("cls3d2nController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
    $scope.$watch("rs.listValidTime.length", function () {
        $("#cls3d2nCharterHeader").attr("colspan", $rootScope.rs.listValidTime.length * 4 + 1);
    })
}]);
moduleContractCreate.controller("stl3d2nController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
}]);
moduleContractCreate.controller("contractCreateController", ["$rootScope", "$scope", "$http", "$location", "$filter", "$timeout", function ($rootScope, $scope, $http, $location, $filter, $timeout) {
    $scope.currency = 1;
    $scope.createContract = function () {
        $http({
            method: "POST",
            url: "WebMethod/ContractCreateWebMethod.asmx/CreateContract",
            data: {
                "listContractDTO": $rootScope.rs.listValidTime,
                "name": $scope.name,
                "currency": $scope.currency,
            },
        }).then(function (response) {
            window.location.href = "ContractManagement.aspx?NodeId=1&SectionId=15"
        }, function (response) {
        })
    }
}]);
