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
                data: { client_id: "altaiportal", grant_type: "client_credentials" }
            }).then(function (response) {
                sessionStorage.setItem("token", response.data.access_token);
                alert("Success");
                },
                function(error) {
                    alert("CORS error");
                });
        };

        $scope.logout = function () {
            sessionStorage.removeItem("token");
            $scope.list = [];
        };

        $scope.getToken = function () {
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