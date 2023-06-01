let SendNoFlag = 0;
let ctlSingSopt = false;
//啟動掃描器
function QrCamStart(CamDivName, CtCamW, CtCamH, Ctfps) {
    SendNoFlag = 1;
    ctlSingSopt = false;
    const html5QrCode = new Html5Qrcode(CamDivName);

    Html5Qrcode.getCameras().then((devices) => {
        if (devices && devices.length) {
            $("#CamVideo").show();
            $("#btnOpenCam").show();
            $("#CapMsg").hide();
            html5QrCode.start(
                { facingMode: "environment" },
                {
                    fps: Ctfps,    //每秒幾偵
                    qrbox: { width: CtCamW, height: CtCamH }   //要不要有掃描框有選才有 要多大
                },
                (decodedText, decodedResult) => {
                    //讀取到代碼做的事
                    if (!ctlSingSopt) {
                        ctlSingSopt = true;   //暫停解析QRCODE
                        SendNoFlag = 0;
                        InpuSingNum(decodedText, "QR");
                    } else {
                        //正在資料確定 不執行讀取成功事項
                        console.log("讀取成功但不執行");
                    }
                },
                (errorMessage) => {
                    //解析錯誤要做的事 通常是忽略
                    if (!ctlSingSopt) {
                        SendNoFlag++;
                        console.log("解析中：" + errorMessage);
                        if (SendNoFlag > (35 * Ctfps)) {
                            SendNoFlag = 0;
                            QrCamStop(html5QrCode);
                        }
                    } else {
                        //正在資料確定作 暫停解析階段
                        console.log("暫停解析");
                    }
                })
                .catch((err) => {
                    //啟動失敗 要做的事
                    console.log("啟動失敗：" + err);
                    ErrMsg("相機啟動失敗", "請檢查是否授予權限！").show();
                });
        } else {
            ErrMsg("裝置有問題", "請鏡頭是否妥當連接並開啟權限！").show();
        }
    }).catch((err) => {
        console.log(err);   //鏡頭有誤
        $("#CapMsg").hide();
        ErrMsg("找不任何裝置", "請使用有鏡頭的裝置或開啟相機權限！").show();
    });
}
function QrCamStop(html5QrCode) {
        html5QrCode.stop().then((ignore) => {
            //掃描功能已停止
            $("#CamVideo").hide();
            $("#btnOpenCam").hide();
            console.log("已停止掃描功能：" + ignore);
        }).catch((err) => {
            //無法停止掃描功能
            console.log("無法停止掃描功能：" + err);
        });
}

//撥放音效
function playAudioFun(PlayType) {
    const Caudio = document.getElementById(PlayType);
    Caudio.currentTime = 0;
    Caudio.play();
}