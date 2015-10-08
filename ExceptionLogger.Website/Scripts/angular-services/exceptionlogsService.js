'use strict'

var exceptionLogsService = function ($http) {
    var currentHost = window.location.host;

    
    this.GetAll = function (callback) {
        console.log('exceptionLogsService.GetAll()');

        //return [
        //    {
        //        id: "560a12716b6d041d9854a6e3",
        //        cid: "801402",
        //        url: "http://www.ibcjapan.co.jp/config/global/config.xml?_=1443571932054",
        //        referer: "http://www.ibcjapan.co.jp/config/global/config.xml?_=1443571932054",
        //        ip: "63.143.86.227",
        //        browser: "Firefox",
        //        server: "xweb2",
        //        exception: "The controller for path '/config/global/config.xml' was not found or does not implement IController.",
        //        stackTrace: "xxx",
        //        is404Error: false,
        //        date: "2015-09-29T04:24:17.754Z"
        //    },
        //    {
        //        id: "560a12716b6d041d9854a6e3",
        //        cid: "801402",
        //        url: "http://www.ibcjapan.co.jp/config/global/config.xml?_=1443571932054",
        //        referer: "http://www.ibcjapan.co.jp/config/global/config.xml?_=1443571932054",
        //        ip: "63.143.86.227",
        //        browser: "Firefox",
        //        server: "xweb2",
        //        exception: "The controller for path '/config/global/config.xml' was not found or does not implement IController.",
        //        stackTrace: "xxx",
        //        is404Error: false,
        //        date: "2015-09-29T04:24:17.754Z"
        //    }
        //];

        $http.get('/api/ExceptionLog')
        .then(
        function (data) {
            callback(data);
        }
        , function (data) {
            callback(data);
        });
    }

    this.GetByID = function (id) {
        console.log('exceptionLogsService.GetByID()');

        return {
            id: "560a12716b6d041d9854a6e3",
            cid: "801402",
            url: "http://www.ibcjapan.co.jp/config/global/config.xml?_=1443571932054",
            referer: "http://www.ibcjapan.co.jp/config/global/config.xml?_=1443571932054",
            ip: "63.143.86.227",
            browser: "Firefox",
            server: "xweb2",
            exception: "The controller for path '/config/global/config.xml' was not found or does not implement IController.",
            stackTrace: "xxx",
            is404Error: false,
            date: "2015-09-29T04:24:17.754Z"
        };

        //$http.get(currentHost + "/api/ExceptionLog/" + id)
        //.success(function (response) {
        //    response = response;
        //});
    }


}