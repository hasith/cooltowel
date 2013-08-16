define(['toastr', 'durandal/system'], function (toastr, system) {

    toastr.options = {
        fadeIn: 500,
        fadeOut: 3000,
        positionClass: 'toast-bottom-right',
        timeOut: 1000,
    };

    function error(message, title) {
        toastr.error(message, title);
        system.log("Error: ", message);
    };
    function info(message, title) {
        toastr.info(message, title);
        system.log("Info: ", message);
    };
    function success(message, title) {
        toastr.success(message, title);
        system.log("Success: ", message);
    };
    function warning(message, title) {
        toastr.warning(message, title);
        system.log("Warning: ", message);
    };

    return {
        error: error,
        info: info,
        success: success,
        warning: warning
    };

});