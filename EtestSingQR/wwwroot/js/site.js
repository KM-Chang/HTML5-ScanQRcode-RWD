function artMsg(titletxt, Conttext) {//自訂對話框提醒
    var msgAlertHtml = "<div class='modal fade' id='opalrt' tabindex='-1'><div class='modal-dialog modal-sm'><div class='modal-content'>" +
        "<div class='modal-body alert-info' style='margin:0;padding-bottom:10px;'>" +
        "<div class='card'><div class='card-header text-white MybtnColor1 MyrotateX'></div><div class='card-body'><h3>" +
        "<i class='bi bi-info-circle-fill' style='color:sandybrown;font-size:1.05em;'></i>&nbsp;<strong>" + titletxt + "</strong></h3><h6>" + Conttext + "</h6><div class='d-grid gap-2 mx-auto'><br /><button type='button' class='btn MybtnColor1' data-bs-dismiss='modal'>知道了</button>" +
        "</div></div></div></div></div></div></div>";
    if ($("#opalrt").length == 0) { $("body").append(msgAlertHtml); } else {
        $("#opalrt").remove(); $("body").append(msgAlertHtml);
    }
    return new bootstrap.Modal(document.getElementById("opalrt"), {
        keyboard: false
    });
}
function ErrMsg(titletxt, Conttext) {//自訂對話框錯誤禁止
    var msgAlertHtml = "<div class='modal fade' id='opalrt' tabindex='-1'><div class='modal-dialog modal-sm'><div class='modal-content'>" +
        "<div class='modal-body alert-danger' style='margin:0;padding-bottom:10px;'>" +
        "<div class='card'><div class='card-header text-white bg-danger MyrotateX'></div><div class='card-body'><h3>" +
        "<i class='bi bi-x-circle-fill' style='color:#f84949;font-size:1.05em;'></i>&nbsp;<strong>" + titletxt + "</strong></h3><h6>" + Conttext + "</h6><div class='d-grid gap-2 mx-auto'><br /><button type='button' class='btn btn-danger' data-bs-dismiss='modal'>確定</button>" +
        "</div></div></div></div></div></div></div>";
    if ($("#opalrt").length == 0) { $("body").append(msgAlertHtml); } else {
        $("#opalrt").remove(); $("body").append(msgAlertHtml);
    }
    return new bootstrap.Modal(document.getElementById("opalrt"), {
        keyboard: false
    });
}
function OkMsg(titletxt, Conttext) {//自訂對話框正確通過
    var msgAlertHtml = "<div class='modal fade' id='opalrt' tabindex='-1'><div class='modal-dialog modal-sm'><div class='modal-content'>" +
        "<div class='modal-body alert-success' style='margin:0;padding-bottom:10px;'>" +
        "<div class='card'><div class='card-header text-white bg-success MyrotateX'></div><div class='card-body'><h3>" +
        "<i class='bi bi-check-circle-fill' style='color:#f84949;font-size:1.05em;'></i>&nbsp;<strong>" + titletxt + "</strong></h3><h6>" + Conttext + "</h6><div class='d-grid gap-2 mx-auto'><br /><button type='button' class='btn btn-success' data-bs-dismiss='modal'>確定</button>" +
        "</div></div></div></div></div></div></div>";
    if ($("#opalrt").length == 0) { $("body").append(msgAlertHtml); } else {
        $("#opalrt").remove(); $("body").append(msgAlertHtml);
    }
    return new bootstrap.Modal(document.getElementById("opalrt"), {
        keyboard: false
    });
}
function KTsimalret(BGColstyle, Conttext) {//簡易對話框 後自動消失 BGColstyle選顏色
    var msgAlertHtml = "<div class='alert alert-" + BGColstyle + "' id='KTsialrt' OnClick=" + '"' + "$('#KTsialrt').fadeOut(); " + '"' +
        " style='position:fixed; left:43vw; top:150px; cursor:pointer; z-index:9999;opacity:.97;'  >" +
        "<span style='font-size:1.15em;'>" + Conttext + "</span></div>";
    if ($("#KTsialrt").length == 0) { $("body").append(msgAlertHtml); } else {
        $("#KTsialrt").remove(); $("body").append(msgAlertHtml);
    }
    $('#KTsialrt').show();
    $('#KTsialrt').delay(1700).fadeOut();
}
//使用 cfrmMsg("標題", "內容斷行吃HTML tag").on(function (e) { alert("返回结果：" + e) });
//若要傳後端建議複製這段html到前端自己做一個按鈕
function cfrmMsg(titletxt, Conttext, Okbtns, Nobtns) {
    if (Okbtns == null || Okbtns == "") { Okbtns = "確定"; }
    if (Nobtns == null || Nobtns == "") { Nobtns = "取消"; }
    var msgAlertHtml = "<div class='modal fade' id='opalrt' tabindex='-1'><div class='modal-dialog'><div class='modal-content'>" +
        "<div class='modal-body alert-info' style='margin:0;padding-bottom:10px;'>" +
        "<div class='card'><div class='card-header text-white MyBgColor MyrotateX'></div><div class='card-body'><h3>" +
        "<i class='bi bi-question-circle-fill' style='color:#1f80dd;font-size:1.2em;'></i>&nbsp;" + titletxt + "</h3><h5>" + Conttext + "</h5><div class='d-grid gap-2 d-md-flex justify-content-md-center'><br />" +
        "<button id='MsbOkbtn' type='button' class='btn btn-lg MybtnColor1' data-bs-dismiss='modal'>&nbsp;" + Okbtns + "&nbsp;</button>&nbsp;&nbsp;&nbsp;" + "<button id='MsbNobtn' type='button' class='btn btn-lg btn-outline-secondary' data-bs-dismiss='modal'>&nbsp;" + Nobtns + "&nbsp;</button>" +
        "</div></div></div></div></div></div></div>";
    if ($("#opalrt").length == 0) { $("body").append(msgAlertHtml); } else {
        $("#opalrt").remove(); $("body").append(msgAlertHtml);
    }
    //new bootstrap.Modal(document.getElementById("opalrt"), { keyboard: false, backdrop:"static"}).show();
    $("#opalrt").modal('show');
    return {
        on: function (callback) {
            if (callback && callback instanceof Function) {
                $("#MsbOkbtn").on("click", function () { callback(true); })
                $("#MsbNobtn").on("click", function () { callback(false); })
            }
        }
    };
}

//簡易互動視窗 JQShowmodal('sm','標題','.內容','按鈕名逗點隔開','primary,info,default','Y').on(function (e) {alert('點了...' + e);});
//若要傳後端處理建議複製這段html到前端自己做一個modal
function JQShowmodal(MdSize, Mdtitle, MdConttext, MdFooter, Mdbtncolor, MdstatYN) {
    if (MdSize != "") MdSize = "modal-" + MdSize;       // modalSize 大lg小sm 
    if (MdstatYN) MdstatYN = " data-bs-backdrop='static'";       // MdstatYN 是否不能任意鍵關閉
    if (MdFooter != "") {
        var Mdbtn = MdFooter.split(",");        //按鍵名稱
        var Mdcolor = Mdbtncolor.split(",");    //按鍵顏色
        MdFooter = "";
        for (var r = 0; r < Mdbtn.length; r++) {
            MdFooter += "<button id='MyMdbtn" + r + "' type='button' class='btn btn-" + Mdcolor[r] + " btn-lg'>" + Mdbtn[r] + "</button>";
        }
        MdFooter = "<div class='modal-footer'>" + MdFooter + "</div>";
    }
    var msgAlertHtml = "<div id='MyJQmodal' class='modal fade' tabindex='-1' aria-labelledby='myModalLabel'" + MdstatYN + ">" +
        "<div class='modal-dialog " + MdSize + "'>" +
        "<div class='modal-content'><div class='modal-header'><h4 class='modal-title'>" + Mdtitle + "</h4><button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='Close'></button>" +
        "</div><div class='modal-body'>" + MdConttext + "</div>" + MdFooter + "</div></div></div>";
    if ($("#MyJQmodal").length == 0) { $("body").append(msgAlertHtml); } else {
        $("#MyJQmodal").remove(); $("body").append(msgAlertHtml);
    }
    $("#MyJQmodal").modal('show');

    return {
        on: function (callback) {
            if (callback && callback instanceof Function) {
                $(".btn").each(function (r) {
                    $("#MyMdbtn" + r).on("click", function () { callback(r); }) //按鍵方法由0開始
                });
            }
        }
    };
}
//自製等待畫面 BarYN:是否顯示進度條
function JQWaitBar(WaitTitle, BarConten, BarYN) {
    BarConten = "<span id='BarCtKT'>" + BarConten + "</span>";
    if (BarYN) {
        BarConten += "<br /><div class='progress'><div id='MyBarw' class='progress-bar progress-bar-striped progress-bar-animated MyBgColor' role='progressbar' aria-valuenow='2' aria-valuemin='0' aria-valuemax='100' style='width:2%'></div></div>";
    } else {
        BarConten += "<div class='BallAim'></div>";
    }
    var WaitBarHtml = "<div class='modal' id='WbaeMaskKT' data-bs-backdrop='static' data-bs-keyboard='false' tabindex='-1' aria-labelledby='WbaeMaskKTLabel' aria-hidden='true'>" +
        "<div class='modal-dialog modal-sm'>" +
        "<div class='modal-content'><div class='modal-header MyBgColor'>" +
        "<h5 class='modal-title'><b>" + WaitTitle + "</b></h5></div><div class='modal-body'>" + BarConten + "</div></div></div></div>";
    if ($("#WbaeMaskKT").length == 0) { $("body").append(WaitBarHtml); } else {
        $("#WbaeMaskKT").remove(); $("body").append(WaitBarHtml);
    }
    return new bootstrap.Modal(document.querySelector("#WbaeMaskKT"));
}

function JQisNumber(val) { var reg = /^[0-9]*$/; return reg.test(val); }   //是否數字
function JQcheckID(id) {// 驗證身份證字號是否符合格式
    tab = "ABCDEFGHJKLMNPQRSTUVXYWZIO"
    A1 = new Array(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3);
    A2 = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5);
    Mx = new Array(9, 8, 7, 6, 5, 4, 3, 2, 1, 1);

    if (id.length != 10) return false;
    i = tab.indexOf(id.charAt(0));
    if (i == -1) return false;
    sum = A1[i] + A2[i] * 9;

    for (i = 1; i < 10; i++) {
        v = parseInt(id.charAt(i));
        if (isNaN(v)) return false;
        sum = sum + v * Mx[i];
    }
    if (sum % 10 != 0) return false;
    return true;
}

document.onkeydown = function (e) {
    /*    if (e.keyCode == 13 && e.target.type != "textarea") { $(this).focus(); return false; }  //不可按enter*/
    if (e.keyCode == 8 && !(e.target.type == "text" || e.target.type == "textarea" || e.target.type == "number" || e.target.type == "password" || e.target.type == "email")) { return false; }      //不可按backspace回上頁
}


/*JQhtml標籤加解密轉換*/
function htmlEncode(value) {
    return $('<div/>').text(value.replaceAll("<script", "").replaceAll("/script", "").replaceAll("iframe", "p")).html();
}
function htmlDecode(value) {
    return $('<div/>').html(value).text();
}
function MyUrlEecode(value) {
    return encodeURI(value.replace(/[/*/</$/#/+/(/)/>/'/"]/g, ""));
}

//
//全域初始化
//var tooltip = new bootstrap.Tooltip($('[data-bs-toggle="tooltip"]'), {});  //啟用工作提示
//var popover = new bootstrap.Popover($('[data-bs-toggle="popover"]'), {});  //啟用工作提示