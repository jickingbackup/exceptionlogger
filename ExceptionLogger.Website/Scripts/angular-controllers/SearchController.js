'use strict'

var searchController = function SearchController($scope, exceptionLogsService) {
    $scope.searchkeyword = '';
    $scope.results = [];
    $scope.selectedExceptionLog = {};
    //filters
    $scope.mindate = Date.now();
    $scope.maxdate = Date.now();

    $scope.Search = function () {
        //$scope.results = exceptionLogsService.GetAll();
        exceptionLogsService.GetAll(function (data) {
            $scope.results = data.data;
        });
    }

    $scope.Select = function (id) {
        $scope.selectedExceptionLog = exceptionLogsService.GetByID(id);
    }

    $scope.Notify = function (message) {
        if (!Notification) {
            alert('Desktop notifications not available in your browser. Try Chromium.');
            return;
        }

        if (Notification.permission !== "granted")
            Notification.requestPermission();
        else {
            var notification = new Notification('Notification title', {
                icon: 'http://cdn.sstatic.net/stackexchange/img/logos/so/so-icon.png',
                body: "Hey there! You've been notified!",
            });

            if (message !== null)
                notification.body = message;


            notification.onclick = function () {
                window.open("http://stackoverflow.com/a/13328397/1269037");
            };
        }
    }

    $scope.Initialize = function () {
        $scope.Search();

        //window.setInterval(function () {
        //    $scope.Notify();
        //}, 1000);
    }

    //call initializer
    $scope.Initialize();
}