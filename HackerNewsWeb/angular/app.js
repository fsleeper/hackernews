﻿'use strict';

angular.module('hackerNewsApp', ['ngRoute'])
    .config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
        $routeProvider.when("/Index", {
            controller: "indexCtrl",
            templateUrl: "/App/Views/Home.html",
//        }).when("/TodoList", {
//            controller: "todoListCtrl",
//            templateUrl: "/App/Views/TodoList.html",
//        }).when("/UserData", {
//            controller: "userDataCtrl",
//            templateUrl: "/App/Views/UserData.html",
//        }).otherwise({ redirectTo: "/Home" });
//    }]);

