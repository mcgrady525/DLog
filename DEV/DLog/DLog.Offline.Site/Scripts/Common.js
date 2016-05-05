//VS2010+可感知此引用
/// <reference path="jquery-1.10.2.js" />



//jQuery(function () {
//    GridViewColor("gvBillingList", "#fff", "#eee", "#fc6", "#fd6");
//});
//參數依次為（後兩個如果指定為空值，則不會發生相應的事件）：
//GridView ID, 正常行背景色,交替行背景色,鼠標指向行背景色,鼠標點擊後背景色
function GridViewColor(GridViewId, NormalColor, AlterColor, HoverColor, SelectColor) {
    //獲取所有要控制的行
    if (!document.getElementById(GridViewId)) { return; }
    var AllRows = document.getElementById(GridViewId).getElementsByTagName("tr");

    //設置每一行的背景色和事件，循環從1開始而非0，可以避開表頭那一行
    for (var i = 1; i < AllRows.length; i++) {
        //設定本行默認的背景色
        AllRows[i].style.background = i % 2 == 0 ? NormalColor : AlterColor;

        //如果指定了鼠標指向的背景色，則添加onmouseover/onmouseout事件
        //處于選中狀態的行發生這兩個事件時不改變顏色
        if (HoverColor != "") {
            AllRows[i].onmouseover = function () { if (!this.selected) this.style.background = HoverColor; }
            if (i % 2 == 0) {
                AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = NormalColor; }
            }
            else {
                AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = AlterColor; }
            }
        }

        //如果指定了鼠標點擊的背景色，則添加onclick事件
        //在事件響應中修改被點擊行的選中狀態
        if (SelectColor != "") {
            AllRows[i].onclick = function () {
                this.style.background = this.style.background == SelectColor ? HoverColor : SelectColor;
                this.selected = !this.selected;
            }
        }
    }
}


function GridViewColorByClass(classSelector) {
    jQuery(classSelector).each(function (idx, item) {
        GridViewColor2(item, "#fff", "#eee", "#fc6", "#fd6");
    });
}

function GridViewColorByClassNew(classSelector, NormalColor, AlterColor, HoverColor, SelectColor) {
    jQuery(classSelector).each(function (idx, item) {
        GridViewColor2(item, NormalColor, AlterColor, HoverColor, SelectColor);
    });
}

function GridViewColor2(obj, NormalColor, AlterColor, HoverColor, SelectColor) {
    //獲取所有要控制的行

    var AllRows = obj.getElementsByTagName("tr");

    //設置每一行的背景色和事件，循環從1開始而非0，可以避開表頭那一行
    for (var i = 1; i < AllRows.length; i++) {
        //設定本行默認的背景色
        AllRows[i].style.background = i % 2 == 0 ? NormalColor : AlterColor;

        //如果指定了鼠標指向的背景色，則添加onmouseover/onmouseout事件
        //處于選中狀態的行發生這兩個事件時不改變顏色
        if (HoverColor != "") {
            AllRows[i].onmouseover = function () { if (!this.selected) this.style.background = HoverColor; }
            if (i % 2 == 0) {
                AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = NormalColor; }
            }
            else {
                AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = AlterColor; }
            }
        }

        //如果指定了鼠標點擊的背景色，則添加onclick事件
        //在事件響應中修改被點擊行的選中狀態
        if (SelectColor != "") {
            AllRows[i].onclick = function () {
                this.style.background = this.style.background == SelectColor ? HoverColor : SelectColor;
                this.selected = !this.selected;
            }
        }
    }
}

function GridViewColorWithGray(GridViewId, NormalColor, AlterColor, HoverColor, SelectColor) {
    //獲取所有要控制的行
    if (!document.getElementById(GridViewId)) { return; }
    var AllRows = document.getElementById(GridViewId).getElementsByTagName("tr");

    //設置每一行的背景色和事件，循環從1開始而非0，可以避開表頭那一行
    for (var i = 1; i < AllRows.length; i++) {
        //設定本行默認的背景色
        if (AllRows[i].style.background == "") {
            AllRows[i].style.background = i % 2 == 0 ? NormalColor : AlterColor;
        }

        //如果指定了鼠標指向的背景色，則添加onmouseover/onmouseout事件
        //處于選中狀態的行發生這兩個事件時不改變顏色
        if (HoverColor != "") {
            AllRows[i].onmouseover = function () { if (!this.selected) this.style.background = HoverColor; }
            if (AllRows[i].getAttribute("data-color") != undefined && AllRows[i].getAttribute("data-color") != null) {
                AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = this.getAttribute("data-color"); }
            }
            else {
                if (i % 2 == 0) {
                    AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = NormalColor; }
                }
                else {
                    AllRows[i].onmouseout = function () { if (!this.selected) this.style.background = AlterColor; }
                }
            }
        }

        //如果指定了鼠標點擊的背景色，則添加onclick事件
        //在事件響應中修改被點擊行的選中狀態
        if (SelectColor != "") {
            AllRows[i].onclick = function () {
                this.style.background = this.style.background == SelectColor ? HoverColor : SelectColor;
                this.selected = !this.selected;
            }
        }
    }
}


function ShowOrHide(e) {
    if (e.innerHTML == '[+]') {
        var divBody = jQuery(e).attr('title', '收起').html('[-]').parent().parent().next();
        if (divBody.hasClass("pnlFareTitle")) {
            divBody.next().toggle();
        }
        else {
            divBody.toggle();
        }

    } else {
        var divBody = jQuery(e).attr('title', '展開').html('[+]').parent().parent().next();
        if (divBody.hasClass("pnlFareTitle")) {
            divBody.next().toggle();
        }
        else {
            divBody.toggle();
        }

    }
    e.blur();
    return false;
}

function GetUrlDict(key, default_) {
    if (default_ == null) default_ = "";
    var keyInLower = key.toLowerCase();
    var url = window.location.href;
    var urlInLower = url.toLowerCase();
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + keyInLower + "=([^&#]*)");
    var qs = regex.exec(urlInLower);
    if (qs == null)
        return default_;
    else
        return qs[1];
}

(function ($) {
    // the code of this function is from  
    // http://lucassmith.name/pub/typeof.html 
    $.type = function (o) {
        try {
            var _toS = Object.prototype.toString;
            var _types = {
                'undefined': 'undefined',
                'number': 'number',
                'boolean': 'boolean',
                'string': 'string',
                '[object Function]': 'function',
                '[object RegExp]': 'regexp',
                '[object Array]': 'array',
                '[object Date]': 'date',
                '[object Error]': 'error'
            };
            return _types[typeof o] || _types[_toS.call(o)] || (o ? 'object' : 'null');
        } catch (e) {
            //alert(e.name + ": " + e.message);
        }
    };
    // the code of these two functions is from mootools 
    // http://mootools.net 
    var $specialChars = { '\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\' };
    var $replaceChars = function (chr) {
        return $specialChars[chr] || '\\u00' + Math.floor(chr.charCodeAt() / 16).toString(16) + (chr.charCodeAt() % 16).toString(16);
    };
    $.toJSON = function (o) {
        var s = [];
        switch ($.type(o)) {
            case 'undefined':
                return 'undefined';
                break;
            case 'null':
                return 'null';
                break;
            case 'number':
            case 'boolean':
            case 'date':
            case 'function':
                return o.toString();
                break;
            case 'string':
                return '"' + o.replace(/[\x00-\x1f\\"]/g, $replaceChars) + '"';
                break;
            case 'array':
                for (var i = 0, l = o.length; i < l; i++) {
                    s.push($.toJSON(o[i]));
                }
                return '[' + s.join(',') + ']';
                break;
            case 'error':
            case 'object':
                for (var p in o) {
                    s.push(p + ':' + $.toJSON(o[p]));
                }
                return '{' + s.join(',') + '}';
                break;
            default:
                return '';
                break;
        }
    };
    $.evalJSON = function (s) {
        if ($.type(s) != 'string' || !s.length) return null;
        return eval('(' + s + ')');
    };
})(jQuery);


var HtmlHelper = {};
(function (o) {
    o.EnableSubmit = function () {
        $("input[type='submit'],input[type='button']").each(function (idx, item) {
            var txt = $(item).attr("__txt");
            if (item.tagName == "BUTTON")
                $(item).attr("disabled", false).html(txt);
            else if (item.tagName == "INPUT")
                $(item).attr("disabled", false).val(txt);
        });
        $(document).unbind(".cancel");
    }

    o.DisableSubmit = function () {
        $("input[type='submit'],input[type='button']").each(function (idx, item) {
            if (item.tagName == "BUTTON")
                $(item).attr({ "__txt": $(item).html(), disabled: true })
                        .html("處理中,請稍候...");
            else if (item.tagName == "INPUT")
                $(item).attr({ "__txt": $(item).val(), disabled: true })
                .val("處理中,請稍候...");
        });
        $(document).bind("keydown.cancel", function (e) {
            if (e.keyCode == 27) {
                o.EnableSubmit();
            }
        });
    }

    $(window).bind("beforeunload", function () {
        o.DisableSubmit();
    }).bind("unload", function () {
        o.EnableSubmit();
    });

    //$('a').live('click', function (e) {
    //    var jslinkstart = "javascript:";
    //    var thisLinkHref = $(this).attr('href') || "";
    //    var thisLinkJs = thisLinkHref.substring(jslinkstart.length);
    //    if (thisLinkHref) {
    //        var thisLinkHrefStart = thisLinkHref.substring(0, jslinkstart.length);
    //        if (thisLinkHrefStart == jslinkstart) {
    //            e.preventDefault();
    //            eval(thisLinkJs);
    //        }
    //    }
    //});
})(HtmlHelper);

//TODO: 要加上id值, 方便close()時直接抓到dialog的id關閉. 否則art.dialog.close()不起作用.
//彈出遮罩
function showWaitMoment(mes) {
    if (mes == null || mes == "") {
        mes = "請稍候,正在為您查詢數據...";
    }
    art.dialog({
        id: 'loadingID',
        title: '',
        content: mes,
        opacity: 0.5,
        esc: true,
        lock: true,
        resize: false,
        drag: false
    });
}

//關閉等待
function closeWaitMoment() {
    art.dialog.list["loadingID"].close();  //關閉等待
}

//jquery正則擴展
$.extend($.expr[':'], {
    regex: function (elem, index, match) {
        var matchParams = match[3].match(/(\w+?),(.+)/);
        validLabels = /^(data|css):/,
                attr = {
                    method: matchParams[1].match(validLabels) ? matchParams[1].split(':')[1] : 'attr',
                    property: matchParams[1]
                },
                regexOpts = matchParams[2].match(/\/(.+)?\/(\w*)/);
        regex = new RegExp(regexOpts[1], regexOpts.length == 3 ? regexOpts[2] : "");
        return regex.test(jQuery(elem)[attr.method](attr.property));
    }
});

var BookingHelper = {};
(function (o) {

    o.CheckInsurance = function (dom) {
        var haveInsurance = $(":regex(id, /^travellers__\\d+__HaveInsurance$/)");
        for (var i = 0; i < haveInsurance.length; i++) {
            if ($(dom).val() == "") {
                $(haveInsurance[i]).removeAttr("checked");
                $(haveInsurance[i]).parent().hide();
            }
            else {
                $(haveInsurance[i]).attr("checked", "checked");
                $(haveInsurance[i]).parent().show();
            }
            $(haveInsurance[i]).trigger("change");
        }
    }

    o.PassportTypeChange = function () {
        var obj = this;
        o.PassportTypeOpt(obj);
        //nation.attr("disabled", true).removeClass("input-validation-error");
        //if (nation.parents("span.input-validation-error-wrapper").length > 0)
        //    nation.unwrap();
        //issuedPlace.attr("disabled", true).removeClass("input-validation-error");
        //if (issuedPlace.parents("span.input-validation-error-wrapper").length > 0)
        //    issuedPlace.unwrap();
        //expiredDate.attr("disabled", true).removeClass("input-validation-error");

        //switch (v) {
        //    case "1":
        //        nation.attr("disabled", true);
        //        issuedPlace.attr("disabled", true);
        //        expiredDate.attr("disabled", true);
        //        break;
        //    case "2":
        //        $("<span class='red'>*</span>").prependTo(lblNation);
        //        $("<span class='red'>*</span>").prependTo(lblIssuedPlace);
        //        $("<span class='red'>*</span>").prependTo(lblExpiredDate);
        //        nation.attr("disabled", false);
        //        issuedPlace.attr("disabled", false);
        //        expiredDate.attr("disabled", false);
        //        break;
        //}
    }

    o.PassportTypeOpt = function (obj) {

        var i = obj.id.match(/^travellers__(\d+)__PassportType$/)[1];
        var v = $(obj)[0].checked;

        $("input:regex(id,/^travellers__" + i + "__PassportType$/)").rules("add", {
            required: true,
            messages: {
                required: "請選擇證件類型"
            }
        });

        var lblHomeReturnNo = $("label:regex(for,/^travellers__" + i + "__HomeReturnNo$/)");
        var lblPassportNo = $("label:regex(for,/^travellers__" + i + "__PassportNo$/)");
        var lblNation = $("label:regex(for,/^travellers__" + i + "__Nation$/)");
        var lblIssuedPlace = $("label:regex(for,/^travellers__" + i + "__PassportNation$/)");
        var lblExpiredDate = $("label:regex(for,/^travellers__" + i + "__PassportDueDate$/)").first();
        var birthday = $("label[for='travellers__" + i + "__Birthday']");

        var homereturnno = $("input:regex(id,/^travellers__" + i + "__HomeReturnNo$/)");
        var passportno = $("input:regex(id,/^travellers__" + i + "__PassportNo$/)");
        var nation = $("select:regex(id,/^travellers__" + i + "__Nation$/)");
        var issuedPlace = $("select:regex(id,/^travellers__" + i + "__PassportNation$/)");
        var expiredDate = $("input:regex(id,/^travellers__" + i + "__PassportDueDate$/)");
        var txtbirthday = $("input:regex(id,/^travellers__" + i + "__Birthday$/)");

        if (v && $(obj).attr("PassportType") == 128) {
            $("<span class='red'>*</span>").prependTo(lblHomeReturnNo);
            homereturnno.rules("add", "required");
        } else if (!v && $(obj).attr("PassportType") == 128) {
            lblHomeReturnNo.find("span").remove();
            homereturnno.rules("remove");
            homereturnno.removeClass("input-validation-error");
        }

        if (v && $(obj).attr("PassportType") == 1) {
            $("<span class='red'>*</span>").prependTo(lblPassportNo);
            $("<span class='red'>*</span>").prependTo(lblNation);
            $("<span class='red'>*</span>").prependTo(lblIssuedPlace);
            $("<span class='red'>*</span>").prependTo(lblExpiredDate);
            passportno.rules("add", "required");
            expiredDate.rules("add", "required");

            //出生日期
            if (birthday.find("span[class='red']").length == 0) {
                $("<span class='red'>*</span>").prependTo(birthday);
                txtbirthday.rules("add", "required");
            }
        } else if (!v && $(obj).attr("PassportType") == 1) {
            lblPassportNo.find("span").remove();
            lblNation.find("span").remove();
            lblIssuedPlace.find("span").remove();
            lblExpiredDate.find("span").remove();
            passportno.rules("remove");
            passportno.removeClass("input-validation-error");
            expiredDate.rules("remove");
            expiredDate.removeClass("input-validation-error");

            var haveInsurance = $("input:regex(id,/^travellers__" + i + "__HaveInsurance$/)");
            if (haveInsurance.attr("checked") != "checked" && haveInsurance.attr("BHR") != "1") {
                birthday.find("span[class='red']").remove();
                txtbirthday.removeClass("input-validation-error");
                txtbirthday.rules("remove");
            }
        }

        var ckbValue = 0;
        $("input:regex(id,/^travellers__" + i + "__PassportType$/)").each(function () {
            if ($(this).attr("checked") == "checked" && $(this).attr("PassportType") == 1) {
                ckbValue += 1;
            } else if ($(this).attr("checked") == "checked" && $(this).attr("PassportType") == 128) {
                ckbValue += 128;
            }
        });
        $("input:regex(id,/^travellers__" + i + "__PassportType$/)").val(ckbValue);
        $("input:regex(id,/^travellers__" + i + "__PassportType$/)").next().val(ckbValue);

    }

    o.HaveInsurance = function () {
        var bhr = this.getAttribute("BHR");

        var i = this.id.match(/^travellers__(\d+)__HaveInsurance$/)[1];
        var idcardno = $("label[for='travellers__" + i + "__IDCardNo']");
        var birthday = $("label[for='travellers__" + i + "__Birthday']");
        var txtidcardno = $("input:regex(id,/^travellers__" + i + "__IDCardNo$/)");  //身份證號
        var txtbirthday = $("input:regex(id,/^travellers__" + i + "__Birthday$/)");    //出生日期

        if (bhr != "1") {
            birthday.find("span[class='red']").remove();
            txtbirthday.removeClass("input-validation-error");
            txtbirthday.rules("remove");
        } else if (birthday.find("span[class='red']").length == 0) {
            $("<span class='red'>*</span>").prependTo(birthday);
            txtbirthday.rules("add", "required");
        }

        //買保險，身份證必填
        if (this.checked) {
            if (idcardno.find("span").length == 0) {
                $("<span class='red'>*</span>").prependTo(idcardno);
                txtidcardno.rules("add", "required");
            }
            if (birthday.find("span[class='red']").length == 0) {
                $("<span class='red'>*</span>").prependTo(birthday);
                txtbirthday.rules("add", "required");
            }
        } else {
            idcardno.find("span").remove();
            txtidcardno.removeClass("input-validation-error");
            txtidcardno.rules("remove");

            var isDel = true;
            $("input:regex(id,/^travellers__" + i + "__PassportType$/)").each(function () {    //護照不允許刪除生日
                if ($(this).attr("checked") == "checked" && $(this).attr("PassportType") == 1) {
                    isDel = false;
                }
            });
            if (isDel && bhr != "1") {
                birthday.find("span[class='red']").remove();
                txtbirthday.removeClass("input-validation-error");
                txtbirthday.rules("remove");
            }
        }
    }

    o.CheckPassengerDuplicate = function () {
        var mingENs = $(":input:regex(id,/travellers__\\d+__MingEN/i)");
        var xingENs = $(":input:regex(id,/travellers__\\d+__XingEN/i)");

        var tmp = {};
        for (var i = 0; i < mingENs.length; i++) {
            var key = mingENs[i].value + "/" + xingENs[i].value;
            if (key == "/")
                continue;

            if (key.length > 25) {
                alert("旅客姓名" + key + "不能超過24個字符");
                return false;
            }

            if (tmp[key] == undefined)
                tmp[key] = 1;
            else {
                alert("重復的旅客姓名" + key);
                return false;
            }
        }

        return true;
    }

    ///點擊 航班詳情 或者 鼠標移上“查看飛行時間”時都會調用此方法
    o.GetFlightDetail = function (dom) {

        var divparent = $(dom).parents(".segment_detail");
        var index = divparent.index();

        rData.RoutingGlobalInd = $(divparent).attr("RInd");

        showWaitMoment();
        $.ajax({
            url: $("#SearchFlightDetailsURL").val(),
            type: 'POST',
            data: $.toJSON(rData),
            dataType: 'json',
            contentType: 'application/json;charset=utf-8',
            success: function (data) {
                closeWaitMoment();  //關閉等待
                if (data.success == true) {
                    var seg = divparent.find(".segment_info");
                    seg.html(data.message);
                }
            },
            error: function () {
                closeWaitMoment();
            }
        });
    }

})(BookingHelper);

var OrderHelper = {};
(function (o) {

    o.ShowMailLogDetail = function (logID) {
        art.dialog.open('../Mail/MailLogDetail?logID=' + logID, { title: '郵件日誌詳細信息', lock: true, width: "760px", height: "80%" });
    }

    o.ShowSmsLogDetail = function (logID) {
        art.dialog.open('../SMS/ShowSMSLogContent?sendLogID=' + logID, { title: '短信日誌詳細信息', lock: true, width: "760px", height: "80%" });
    }


})(OrderHelper);

//日期處理
var DateHelper = {};
(function (o) {

    //返回js日期對象，value 字符串日期格式： dd/MM/yyyy HH:mm:ss , 01/01/2001 12:50:55
    o.FormatFullDatetime = function (value) {
        if (value == "") return new Date();
        var arrTemp = value.split(" ");
        var arrPart1 = arrTemp[0].split('/');
        var arrPart2 = arrTemp[1].split(':');

        var day = Number(arrPart1[0]);
        var month = Number(arrPart1[1]) - 1;
        var year = Number(arrPart1[2]);
        var hour = Number(arrPart2[0]);
        var minute = Number(arrPart2[1]);
        var second = Number(arrPart2[2]);

        return new Date(year, month, day, hour, minute, second);
    }

    //返回js日期對象，value 字符串日期格式： dd/MM/yyyy ,    01/01/2001
    o.FormatDatetime = function (value) {
        if (value == "") return new Date();
        var arrTemp = value.split("/");

        var day = Number(arrTemp[0]);
        var month = Number(arrTemp[1]) - 1;
        var year = Number(arrTemp[2]);

        return new Date(year, month, day);
    }

})(DateHelper);

//日期處理
var commonHelper = {};
(function (o) {

    //檢查是否為數字
    o.CheckIsNumber = function (obj, bool, isExceptZero) {
        var value = obj;
        if (bool) {
            value = obj.value;
        }
        var re = /^[0-9]\d*$/
        if (!re.test(value) || (value == 0 && isExceptZero)) {
            return false;
        }
        return true;
    }

    //傳入數字【字符串】，返回香港貨幣格式的字符串，如傳入1000，則返回1,000
    o.outputMoney = function (number) {
        number = number.replace(/\,/g, "");
        if (number == "") return "";
        //返回不帶小數點的貨幣格式
        if (number < 0)
            return '-' + outputDollars(Math.floor(Math.abs(number) - 0) + '');
        else
            return outputDollars(Math.floor(number - 0) + '');
    }

    outputDollars = function (number) {
        if (number.length <= 3)
            return (number == '' ? '0' : number);
        else {
            var mod = number.length % 3;
            var output = (mod == 0 ? '' : (number.substring(0, mod)));
            for (i = 0 ; i < Math.floor(number.length / 3) ; i++) {
                if ((mod == 0) && (i == 0))
                    output += number.substring(mod + 3 * i, mod + 3 * i + 3);
                else
                    output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
            }
            return (output);
        }
    }

})(commonHelper);

//重寫alert方法，使用artDialog方法彈出
//window.alert = showAlert;
//function showAlert(str) {
//    str = str.toString();
//    art.dialog(str, function () { });
//}

function AlertDialog(content, ok, close) {
    art.dialog({
        id: 'Alert',
        fixed: true,
        lock: true,
        content: content,
        ok: ok,
        close: close
    });
}
function ConfirmDialog(content, yes, no) {
    art.dialog({
        id: 'Confirm',
        icon: 'question',
        fixed: true,
        lock: true,
        content: content,
        ok: function (here) {
            return yes.call(this, here);
        },
        cancel: function (here) {
            return no && no.call(this, here);
        } 
    });
}
