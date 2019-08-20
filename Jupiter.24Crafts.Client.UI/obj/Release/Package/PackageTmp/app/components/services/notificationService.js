'use strict';

app.factory('notificationService', ['toastr', function (toastr) {

    toastr.options = {
        "debug": false,
        "positionClass": "toast-top-rigth",
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 1000,
        "timeOut": 3000,
        "extendedTimeOut": 1000
    };
    var service = {};

    //function declartions
    var _displaySuccess = function (message) {
        toastr.success(message, '', {
            closeButton: true,
            progressBar: true
        });
    };
    var _displayError = function (error) {
        if (Array.isArray(error)) {
            error.forEach(function (err) {
                toastr.error(err, '', {
                    closeButton: true,
                    progressBar: true
                });
            });
        } else {
            toastr.error(error);
        }
    };
    var _displayWarning = function (message) {
        toastr.warning(message, '', {
            closeButton: true,
            progressBar: true
        });
    };
    var _displayInfo = function (message) {
        toastr.info(message, '', {
            closeButton: true,
            progressBar: true
        });
    };


    //function return statement declartions
    service.displaySuccess = _displaySuccess;
    service.displayError = _displayError;
    service.displayWarning = _displayWarning;
    service.displayInfo = _displayInfo;

    return service;
}]);