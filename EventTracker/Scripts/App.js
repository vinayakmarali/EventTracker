var myApp = angular.module('myApp', []);

myApp.service('eventService', function () {
    var events;

    var setEvent = function (event) {
        events = event;
        localStorage.setItem("events", JSON.stringify(events));
    };
    var getEvent = function () {
        savedEvent = JSON.parse(localStorage.getItem("events"));
        return savedEvent;
    };
    return {
        setEvent: setEvent,
        getEvent: getEvent
    };
});

myApp.controller('mainController', function ($scope, $http, eventService) {

    $http.get('/home/GetEvents')
    .success(function(result)
    {
        $scope.events = result;
    })
    .error(function (data) {
        console.log(data);
    })

    $scope.newEvent = ""
    $scope.addEvent = function () {
        $http.post("/home/AddEvent/", { newEvent: $scope.newEvent })
        .success(function (result) {
            $scope.events = result;
            $scope.newEvent = "";
        })
        .error(function (data) {
                console.log(data);
        })
    }

    $scope.deleteEvent = function (event) {
        $http.post("/home/DeleteEvent/", { delEvent: event })
        .success(function (result) {
            $scope.events = result;
        })
        .error(function (data) {
        console.log(data);
        })
    }

    $scope.gotoAttendee = function (event) {
        eventService.setEvent(event);
        window.location = "/Attendee/Index";
    };

});

myApp.controller('attendeeController', function ($scope, $http, eventService) {

    $scope.events = eventService.getEvent();
    $http.post("/attendee/GetAttendees", { events: eventService.getEvent() })
    .success(function(result)
    {
        $scope.attendees = result;
    })
    .error(function (data) {
        console.log(data);
    })

    $scope.firstName = ""
    $scope.lastName = ""
    $scope.addAttendee = function () {
        $http.post("/attendee/AddAttendee/", { firstName: $scope.firstName,lastName: $scope.lastName,events: eventService.getEvent() })
        .success(function (result) {
            $scope.attendees = result;
            $scope.firstName = ""
            $scope.lastName = ""
        })
        .error(function (data) {
            console.log(data);
        })
    }

    $scope.deleteAttendee = function (attendee) {
        $http.post("/attendee/DeleteAttendee/", { delAttendee: attendee, events: eventService.getEvent() })
        .success(function (result) {
            $scope.attendees = result;
        })
        .error(function (data) {
            console.log(data);
        })
    }


});