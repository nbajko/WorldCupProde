(function ($, global) {
    var helpers = function () {
        return {
            alertError: function (text) {
                $('.alert-fixed-bottom').remove();
                $('.body-content').prepend('<div class="alert alert-danger alert-dismissible show alert-fixed-bottom" role="alert">' + text +
                    '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
                    '<span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>' );
            }
        };
    };

    global.helpers = helpers;
})(jQuery, window);