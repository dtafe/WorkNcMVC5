var app = angular.module('myApp', []);
app.controller('myController', function ($scope, $http) {
    getAllFactory();
    function getAllFactory() {
        $http({
            method: 'Get',
            url: '/WorkZone/GetAllFactory'
        }).success(function (data) {
            $scope.factoryList = data;

        }).error(function () {
            $scope.errorMessage = 'Not found';
        });
    }

    $scope.GetMachineList = function () {
        var getFactory = $scope.Factory;
        if (getFactory) {
            $http({
                method: 'POST',
                url: '/WorkZone/GetMachineByIdFactory/',
                data: JSON.stringify({ FactoryId: getFactory.FactoryId })
            }).success(function (data) {
                $scope.machineList = data;
            }).error(function (data) {
                alert(data.message);
                $scope.message = 'not found';
            });
        }
        else {
            $scope.machineList = null;
        }
    }

});