angular.module('MyApp')
.controller('myController', function ($scope, LocationService) {
    $scope.FactoryId = null;
    $scope.MachineId = null;
    $scope.FactoryList = null;
    $scope.MachineList = null;

    $scope.MachineTextToShow = "Select machine";
    $scope.Result = "";

    //populate Factory
    LocationService.GetFactory().then(function (d) {
        $scope.FactoryList = d.data;
    }, function (error) {
        alert('Error');
    });

    //populate machine
    $scope.GetMachine = function () {
        $scope.MachineId = null;
        $scope.MachineList = null;
        $scope.MachineTextToShow = "Please wait...";

        //load machine
        LocationService.GetMachine($scope.FactoryId).then(function (d) {
            $scope.MachineList = d.data;
            $scope.MachineTextToShow = "select machine";
        }, function (error) {
            alert('Error');
        });

    }

    //funtion show for result
    $scope.ShowResult = function () {
        $scope.Result = 'select factoryID' + $scope.FactoryId + 'MachineId' + $scope.MachineId;
    }
})
.factory('LocationService', function ($http) {
    var fac = {};
    fac.GetFactory = function () {
        
        return $http.get('/WorkZone/GetFactory')
    }
    fac.GetMachine = function (factoryId) {
        return $http.get('/WorkZone/GetMachine?factoryId='+factoryId)
    }
    return fac;
});