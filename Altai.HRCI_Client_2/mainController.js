(function () {
    'use strict';

    var myApp = angular.module('myApp', []);

    angular.module('myApp').controller("mainController", ["$scope", "$http", function ($scope, $http) {
        const baseUrl = "http://localhost:53000";
        $scope.list = [];

        $scope.auth = function () {
            $http({
                method: 'POST',
                url: baseUrl + "/token",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: { client_id: "hrci-client-2", client_secret: "ec7eb3d0c9f44778aec1fee95bf20347df647e430d8e4f6e9a08f604a9794fc4", grant_type: "client_credentials" }
            }).then(function (response) {
                sessionStorage.setItem("token", response.data.access_token);
                alert("Success");
              }, function () {
                alert("Auth error");
              });
        };

        $scope.logout = function () {
            sessionStorage.removeItem("token");
            $scope.list = [];
        };

        $scope.getToken = function() {
            return sessionStorage.getItem("token");
        };

        $scope.getMovieList = function () {
            $scope.list = [];
            $http({
                method: 'GET',
                url: baseUrl + "/api/movielist",
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem("token") }
            }).then(function (response) {
                $scope.list = response.data;
            }, function (error) {
                $scope.logout();
                alert(error.data.Message);
            });
        };
    }]);
})();