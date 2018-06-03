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
            },
            saveMatch: function (obj, url) {
                var $target = $(obj.target),
                    $penalties = $target.closest('tr').find('.match-penalties'),
                    homePenalties = $target.closest('tr').find('.match-penalties.match-home').val(),
                    awayPenalties = $target.closest('tr').find('.match-penalties.match-away').val(),
                    homeGoals = $target.closest('tr').find('.match-goals.match-home').val(),
                    awayGoals = $target.closest('tr').find('.match-goals.match-away').val(),
                    matchId = $target.data('match-id'),
                    dataJSON = { id: matchId, homeGoals: homeGoals, awayGoals: awayGoals };

                $target.siblings('.progress-circle').hide();
                $target.siblings('.glyphicon-ok').hide();
                $target.siblings('.glyphicon-remove').hide();

                if (homeGoals == '' || awayGoals == '') {
                    window.helpers().alertError('<strong>Ups.</strong> Te falto cargar el resultado!');
                    $target.siblings('.glyphicon-remove').show();
                    return;
                }

                if (homeGoals < 0 || awayGoals < 0) {
                    window.helpers().alertError('<strong>Ups.</strong> Los goles tienen que ser positivos!');
                    $target.siblings('.glyphicon-remove').show();
                    return;
                }

                if ($penalties.length && homeGoals == awayGoals) {
                    if (homePenalties == '' || awayPenalties == '') {
                        window.helpers().alertError('<strong>Ups.</strong> Te falto cargar los penales!');
                        $target.siblings('.glyphicon-remove').show();
                        return;
                    }

                    if (homePenalties < 0 || awayPenalties < 0) {
                        window.helpers().alertError('<strong>Ups.</strong> Los penales tienen que ser positivos!');
                        $target.siblings('.glyphicon-remove').show();
                        return;
                    }

                    if (homePenalties == awayPenalties) {
                        window.helpers().alertError('<strong>Ups.</strong> Tenes que especificar un ganador por penales!');
                        $target.siblings('.glyphicon-remove').show();
                        return;
                    }

                    dataJSON.homePenalties = homePenalties;
                    dataJSON.awayPenalties = awayPenalties;
                }

                $target.siblings('.progress-circle').show();
                $target.prop('disabled', true);

                $.ajax(url,
                    {
                        type: 'POST',
                        data: dataJSON,
                        success: function () {
                            $target.siblings('.glyphicon-ok').show();
                        }
                    }).fail(function (e) {
                        var errorMessage = 'Hubo un error al guardar el resultado!';

                        if (e.responseJSON && e.responseJSON.ExceptionMessage) {
                            errorMessage = e.responseJSON.ExceptionMessage;
                        } else if (e.responseText) {
                            console.error(e.responseText);
                        }

                        $target.siblings('.glyphicon-remove').show();
                        window.helpers().alertError('<strong>Ups.</strong> ' + errorMessage);
                    }).always(function () {
                        $target.siblings('.progress-circle').hide();
                        $target.prop('disabled', false);
                    });
            }
        };
    };

    global.helpers = helpers;
})(jQuery, window);