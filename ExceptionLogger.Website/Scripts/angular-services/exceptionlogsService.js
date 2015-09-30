'use strict'

var exceptionLogsService = function ($http) {
    var currentHost = window.location.host;

    
    this.GetAll = function () {
        console.log('exceptionLogsService.GetAll()');

        return [
            {
                Id: "XXX",
                CID: "xxx",
                URL: "xxx",
                Referer: "xxx",
                IP: "xxx"
            },
            {
                Id: "YYY",
                CID: "xxx",
                URL: "xxx",
                Referer: "xxx",
                IP: "xxx"
            }
        ];
        //$http.get(currentHost + "/api/ExceptionLog")
        //.success(function (response) {
        //    return response;
        //});
    }

    this.GetByID = function (id) {
        return {
            Id: "YYY",
            CID: "xxx",
            URL: "xxx",
            Referer: "xxx",
            IP: "xxx"
        };

        //$http.get(currentHost + "/api/ExceptionLog/" + id)
        //.success(function (response) {
        //    response = response;
        //});
    }


}