﻿function alertE(msg) {
    $.alert({
        //icon: 'glyphicon glyphicon-info-sign',
        title: '',
        content: msg,
        backgroundDismiss: true,
        confirmButton: 'ok',
        confirmButtonClass: 'btn-success',
        confirm: function () {

        }
    });
}

function confirmE(msg, okFun, cancelFun) {
    $.confirm({
        //icon: 'glyphicon glyphicon-info-sign',
        title: '',
        content: msg,
        //theme: 'bootstrap', // 'material', 'bootstrap'
        confirmButtonClass: 'btn-info',
        cancelButtonClass: 'btn-danger',
        confirmButton: '确定',
        cancelButton: '取消',
        confirm: okFun,
        cancel: cancelFun
    });
}
function checkMobile(str) {
    var reg = /^1\d{10}$/;
    return reg.test(str);
}

function checkEmail(str) {
    var reg = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;
    return reg.test(str);
}

function checkPhone(str) {
    var reg = /^0\d{2,3}-?\d{7,8}$/;
    return reg.test(str);
}

function checkIDNum(str) {
    var reg = /^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$/;
    return reg.test(str);
}