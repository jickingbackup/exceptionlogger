'use strict'

var searchController = function SearchController($scope, exceptionLogsService) {
    $scope.searchkeyword = '';
    $scope.results = [];
    $scope.selectedExceptionLog = {};
    //filters
    $scope.mindate = Date.now();
    $scope.maxdate = Date.now();

    // Declare a proxy to reference the hub.
    $scope.hub = null;

    $scope.Search = function () {
        //$scope.results = exceptionLogsService.GetAll();
        exceptionLogsService.GetAll(function (data) {
            //if(data) //TODO:CHECK IF DATA IS 200

            $scope.results = data.data;
        });

        //TEST SIGNALR
        $scope.hub.server.notifyClients();
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

            notification.onclick = function () {
                window.open("http://stackoverflow.com/a/13328397/1269037");
            };
        }
    }

    //SET UP SIGNALR HERE.
    $scope.Initialize = function () {
        // Declare a proxy to reference the hub.
        $scope.hub = $.connection.notificationHub;

        // Create a function that the hub can call to broadcast messages.
        $scope.hub.client.notifyClients = function () {
            $scope.Notify();
        };

        // Start the connection.
        $.connection.hub.start(function () {
            $scope.Search();
        });

    }

    //call initializer
    $scope.Initialize();
}