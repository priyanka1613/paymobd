/// <reference path="jquery-1.6.4-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.js" />

$(document).ready(function () {
    $('.wallet_money').hide();
    $(":input[data-datepicker]").datepicker({
        dateFormat: 'd-M,yy',
        showWeek: true,
        monthNamesShort: ['jan', 'feb', 'mar', 'apr', 'may', 'jun', 'jul', 'aug', 'sep', 'oct', 'nov', 'dec'],
        dayNamesMin: ['Sö', 'Må', 'Ti', 'On', 'To', 'Fr', 'Lö'],
        weekHeader: 'Ve',
        firstDay: 1 // The first day of the week, Sun = 0, Mon = 1, ...

    });
    });
    function show_button() {
        var w = $('wm').val();
        var s = document.getElementById("amount");
  var p=
        if (parseInt(w) > parseInt(amount)) {
            $('.wallet_money').show();
        }
    }
