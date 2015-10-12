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
    }

    $scope.Select = function (id) {
        $scope.selectedExceptionLog = exceptionLogsService.GetByID(id);
    }

    $scope.Notify = function (title,message,url) {
        if (!Notification) {
            alert('Desktop notifications not available in your browser. Try Chromium.');
            return;
        }

        if (Notification.permission !== "granted")
            Notification.requestPermission();
        else {
            if (title === null || title === undefined) {
                title = 'no title.';
            }
            if (message === null || message === undefined) {
                message = 'no message.';
            }
            if (url === null || url === undefined) {
                url = 'http://google.com';
            }

            var notification = new Notification(title, {
                icon: 'http://ibcjapan.co.jp/Images/slides/prelog/slide4/ibchd.png',
                body: message
            });

            notification.onclick = function () {
                window.open(url);
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

        //notifyAllClients
        $scope.hub.client.notifyAllClients = function (title,message,url) {
            $scope.Notify(title, message, url);
        };

        // Start the connection.
        $.connection.hub.start(function () {
            $scope.Search();
        });

    }

    //call initializer
    $scope.Initialize();
}