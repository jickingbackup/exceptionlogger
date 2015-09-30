'use strict'

var searchController = function SearchController($scope, exceptionLogsService) {
    $scope.searchkeyword = '';
    $scope.results = [];
    //filters
    $scope.mindate = Date.now();
    $scope.maxdate = Date.now();


    $scope.Search = function () {
        $scope.results = exceptionLogsService.GetAll();
    }

    $scope.Initialize = function () {
        $scope.Search();
    }

    //call initializer
    $scope.Initialize();
}