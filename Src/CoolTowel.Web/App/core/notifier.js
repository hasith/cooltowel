define(['toastr'], function (toastr) {

    toastr.options = {
        fadeIn: 500,
        fadeOut: 3000,
        positionClass: 'toast-bottom-right',
        timeOut: 1000, 
    };

    function error(message, title) {
        toastr.error(message, title);
        log("Error: " + message);
    };
    function info(message, title) {
        toastr.info(message, title);
        log("Info: " + message);
    };
    function success(message, title) {
        toastr.success(message, title);
        log("Success: " + message);
    };
    function warning(message, title) {
        toastr.warning(message, title);
        log("Warning: " + message);
    };

    // IE and google chrome workaround
    // http://code.google.com/p/chromium/issues/detail?id=48662
    function log() {
        var console = window.console;
        !!console && console.log && console.log.apply && console.log.apply(console, arguments);
    }

    return {
        error: error,
        info: info,
        success: success,
        warning: warning,
        log: log 
    };

});