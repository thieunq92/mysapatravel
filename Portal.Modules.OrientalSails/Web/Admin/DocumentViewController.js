moduleDocumentView.controller("tabController", ["$rootScope", "$scope", "$http", function ($rootScope, $scope, $http) {
    $rootScope.listDocumentCategory = [];
    $http({
        method: "POST",
        url: "WebMethod/DocumentViewWebMethod.asmx/DocumentCategoryGetAll",
        data: "",
    }).then(function (response) {
        $rootScope.listDocumentCategory = JSON.parse(response.data.d);
    }, function (response) {
        alert("Request failed. Please reload and try again. Message:" + response.data.Message);
    })
}])

moduleDocumentView.controller("allPanelController", ["$rootScope", "$scope", "$http", function ($rootScope, $scope, $http) {
    $scope.listDocument = [];
    $http({
        method: "POST",
        url: "WebMethod/DocumentViewWebMethod.asmx/DocumentGetAll",
        data: "",
    }).then(function (response) {
        $scope.listDocument = JSON.parse(response.data.d);
    }, function (response) {
        alert("Request failed. Please reload and try again. Message:" + response.data.Message);
    })
}]);

moduleDocumentView.controller("tabPaneController", ["$rootScope", "$scope", "$http", function ($rootScope, $scope, $http) {
}]);