/// <summary>
/// Accept only numbers 
/// </summary>
/// Created by Sailaja
/// Created on 28082017
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
        return true;
    }
    else {
        return false;
    }
}
/// <summary>
/// Accept only decimals
/// </summary>
/// Created by Sailaja
/// Created on 28082017
function isDecimalKey(txt, evt) {
    // var charCode = (evt.which) ? evt.which : evt.keyCode;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        //Check if the text already contains the . character
        if (txt.value.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (charCode > 31
     && (charCode < 48 || charCode > 57))
            return false;
    }
    return true;
}


function ShowProgress() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modaldivctrl");
        $('body').append(modal);
        var loading = $(".loadingdivctrl");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
    }, 20);//display after 20 milliseconds or 0.2 seconds
   
}
function ShowProgressConfirm(MsgCont) {
    var msg = 'Are you sure you want to ' + MsgCont + ' this Record';
    if (confirm(msg) == true)
    {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modaldivctrl");
            $('body').append(modal);
            var loading = $(".loadingdivctrl");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 20);//display after 20 milliseconds or 0.2 seconds
    }
    else
    {
        return false;

    }
   
   
}
/// <summary>
/// Bootstrap Gridview
/// </summary>
/// Created by Suracharyulu
/// Created on 03102017
function gvDetailsStyles(gridid) {

    var oTable2 = $("[id$=" + gridid + "]").dataTable({

        "sDOM": "<'H'lfr>t<'F'ip>",
        "bAutoWidth": true,
        "bFilter": true,
        "bPagination": true,
        "sPaginationType": "full_numbers",
        "bStateSave": true,
        "bPaginate": true,

        //"scrollX": true,
        //"scrollCollapse": true,
        //"scrollY": "420px"


    });
    return true;

  
}
