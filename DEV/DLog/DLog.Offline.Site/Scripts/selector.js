/// <reference path="jquery-1.10.2.intellisense.js" />
var systemCodeData = [];
var sourceDatas = [];
var classNameDatas = [];
var methodNameDatas = [];
var remarkRegexDatas = [];
var systemCodetype = "ALL";
var searchInfoType = 1; //默认为PerfLog
function loadSystemCode(spt, callback) {
    $.ajax(
                {
                    url: SITEROOT + "Json/GetSystemCodeResult?systemCode=" + $("input[id$='SystemCode']").val() + "&source=" + $("input[id$='Source']").val() + "&className=" + $("input[id$='ClassName']").val() + "&methodName=" + $("input[id$='MethodName']").val() + "&searchInfoType=" + searchInfoType,
                    success: callback,
                    error: function (httpRequest, textStatus, errorThrown) {
                        // alert(textStatus);
                    }
                }//end of ajax config
                );
};
function setSystemCodeData(data) {
    var temp = [];
    $.each(data, function (index, item) {
        if(item.Name!=null)
        temp[temp.length] = { label: item.Name, value: item.ID }
    });
    systemCodeData = temp;
};
$(function () {
    searchInfoType = $("#hidSearchInfoType").val();

    //初始化数据
    var initData = function (data) {
        setSystemCodeData(data);
        $(".systemCodeSelector").each(function (index, el) {

            $(el).autocomplete({
                minLength: 0,
                delay: 0,
                source: systemCodeData,
                focus: function (event, ui) {
                    $(el).val(ui.item.label);
                    return false;
                },
                select: function (event, ui) {
                    $(el).val(ui.item.label);
                    return false;
                }

            })
            .data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
                        .data("item.autocomplete", item)
                        .append("<a style='width:auto;height:22px; overflow:hidden;' title=" + item.label + ">" + item.label + "</a>")
                        .appendTo(ul);
            };

            $(el).bind("focus", function (e) {
                if ($(this).attr("systemCodetype") != "systemCodetype") {
                    systemCodetype = $(this).attr("systemCodetype");
                    loadSystemCode(systemCodetype, function (data) {
                        setSystemCodeData(data);
                        $(el).autocomplete("option", "source", systemCodeData);
                        $(el).autocomplete("search", "");
                    });
                } else {

                    $(el).autocomplete("search", "");
                }

            });
            $(el).bind("blur", function (e) {

                if ($(el).autocomplete("widget").is(":visible")) {
                    $(el).autocomplete("close");
                }
                var exist = false;
                $.each(systemCodeData, function (index, item) {
                    if (item.label == $(el).val()) {
                        exist = true;
                        $("input[id$='Source']").val("");
                        $("input[id$='ClassName']").val("");
                        $("input[id$='MethodName']").val("");
                        return;
                    }
                });
                //if (!exist) $(el).val("");  不存在时则不清空.
                //失去焦点时绑定source
                $.ajax(
                  {
                      url: SITEROOT + "Json/GetSourceResult?systemCode=" + $("input[id$='SystemCode']").val() + "&source=" + $("input[id$='Source']").val() + "&className=" + $("input[id$='ClassName']").val() + "&methodName=" + $("input[id$='MethodName']").val() + "&searchInfoType=" + searchInfoType,
                      success: sourceData,
                      error: function (httpRequest, textStatus, errorThrown) {
                      }
                  }
                );
            });
            //自动绑定source
            $.ajax(
              {
                  url: SITEROOT + "Json/GetSourceResult?systemCode=" + $("input[id$='SystemCode']").val() + "&source=" + $("input[id$='Source']").val() + "&className=" + $("input[id$='ClassName']").val() + "&methodName=" + $("input[id$='MethodName']").val() + "&searchInfoType=" + searchInfoType,
                  success: sourceData,
                  error: function (httpRequest, textStatus, errorThrown) {
                  }
              }
          );

        });
    };

    //加载systemcode,并且初始化数据
    loadSystemCode(systemCodetype, initData);

    //source
    var sourceData = function (data) {
        var temp = [];
        $.each(data, function (index, item) {
            if (item.Name != null)
            temp[temp.length] = { label: item.Name, value: item.ID }
        });
        sourceDatas = temp;
        $(".sourceSelector").each(function (index, el) {
            $(el).autocomplete({
                minLength: 0,
                delay: 0,
                source: sourceDatas,
                focus: function (event, ui) {
                    $(el).val(ui.item.label);
                    return false;
                },
                select: function (event, ui) {
                    $(el).val(ui.item.label);
                    return false;
                }
            })
            .data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            };

            $(el).bind("focus", function (e) {
                $(el).autocomplete("search", "");
            });
            $(el).bind("blur", function (e) {
                if ($(el).autocomplete("widget").is(":visible")) {
                    $(el).autocomplete("close");
                }
                var exist = false;
                $.each(sourceDatas, function (index, item) {
                    if (item.label == $(el).val()) {
                        $(el).val(item.label);
                        exist = true;
                        $("input[id$='ClassName']").val("");
                        $("input[id$='MethodName']").val("");
                        return;
                    }
                });
                // if (!exist) $(el).val("");不存在时则不清空.
                //失去焦点时绑定className,methodName
                $.ajax(
                {
                    url: SITEROOT + "Json/GetClassNameResult?systemCode=" + $("input[id$='SystemCode']").val() + "&source=" + $("input[id$='Source']").val() + "&className=" + $("input[id$='ClassName']").val() + "&methodName=" + $("input[id$='MethodName']").val() + "&searchInfoType=" + searchInfoType,
                    success: classNameData,
                    error: function (httpRequest, textStatus, errorThrown) {
                    }
                });

                $.ajax(
                {
                    url: SITEROOT + "Json/GetMethodNameResult?systemCode=" + $("input[id$='SystemCode']").val() + "&source=" + $("input[id$='Source']").val() + "&className=" + $("input[id$='ClassName']").val() + "&methodName=" + $("input[id$='MethodName']").val() + "&searchInfoType=" + searchInfoType,
                    success: methodNameData,
                    error: function (httpRequest, textStatus, errorThrown) {
                    }
                });

            });

            //页面加载自动绑定className,methodName,remarkRegx
            $.ajax(
            {
                url: SITEROOT + "Json/GetClassNameResult?systemCode=" + $("input[id$='SystemCode']").val() + "&source=" + $("input[id$='Source']").val() + "&className=" + $("input[id$='ClassName']").val() + "&methodName=" + $("input[id$='MethodName']").val() + "&searchInfoType=" + searchInfoType,
                success: classNameData,
                error: function (httpRequest, textStatus, errorThrown) {
                }
            });

            $.ajax(
            {
                url: SITEROOT + "Json/GetMethodNameResult?systemCode=" + $("input[id$='SystemCode']").val() + "&source=" + $("input[id$='Source']").val() + "&className=" + $("input[id$='ClassName']").val() + "&methodName=" + $("input[id$='MethodName']").val() + "&searchInfoType=" + searchInfoType,
                success: methodNameData,
                error: function (httpRequest, textStatus, errorThrown) {
                }
            });

            $.ajax(
              {
                  url: SITEROOT + "Json/GetRemarkRegxResult",
                  success: remarkRegexData,
                  error: function (httpRequest, textStatus, errorThrown) {
                  }
              });
        })
    }
    //className
    var classNameData = function (data) {
        var temp = [];
        $.each(data, function (index, item) {
            if (item.Name != null)
            temp[temp.length] = { label: item.Name, value: item.ID }
        });
        classNameDatas = temp;

        $(".classNameSelector").each(function (index, el) {
            $(el).autocomplete({
                minLength: 0,
                delay: 0,
                source: classNameDatas,
                focus: function (event, ui) {
                    $(el).val(ui.item.label);
                    return false;
                },
                select: function (event, ui) {
                    $(el).val(ui.item.label);
                    return false;
                }
            })
            .data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            };

            $(el).bind("focus", function (e) {
                $(el).autocomplete("search", "");
            });
            $(el).bind("blur", function (e) {
                if ($(el).autocomplete("widget").is(":visible")) {
                    $(el).autocomplete("close");
                }
                var exist = false;
                $.each(classNameDatas, function (index, item) {
                    if (item.value == $(el).val()) {
                        $(el).val(item.label);
                        exist = true;
                        return;
                    }
                });
                //if (!exist) $(el).val(""); 不存在时则不清空.
                //className为空时关联methodName
                $("input[id$='MethodName']").val("");
                $.ajax(
                   {
                       url: SITEROOT + "Json/GetMethodNameResult?systemCode=" + $("input[id$='SystemCode']").val() + "&source=" + $("input[id$='Source']").val() + "&className=" + $("input[id$='ClassName']").val() + "&methodName=" + $("input[id$='MethodName']").val() + "&searchInfoType=" + searchInfoType,
                       success: methodNameData,
                       error: function (httpRequest, textStatus, errorThrown) {
                       }
                   });
            });
        });
    }

    //methodName
    var methodNameData = function (data) {
        var temp = [];
        $.each(data, function (index, item) {
            if (item.Text != null)
            temp[temp.length] = { label: item.Text, value: item.Value }
        });
        methodNameDatas = temp;

        $(".methodNameSelector").each(function (index, el) {
            $(el).autocomplete({
                minLength: 0,
                delay: 0,
                source: methodNameDatas,
                focus: function (event, ui) {
                    $(el).val(ui.item.label);
                    return false;
                },
                select: function (event, ui) {
                    $(el).val(ui.item.label);
                    return false;
                }
            })
		    .data("autocomplete")._renderItem = function (ul, item) {
		        return $("<li></li>")
				    .data("item.autocomplete", item)
				    .append("<a>" + item.label + "</a>")
				    .appendTo(ul);
		    };

            $(el).bind("focus", function (e) {
                $(el).autocomplete("search", "");
            });
            $(el).bind("blur", function (e) {
                if ($(el).autocomplete("widget").is(":visible")) {
                    $(el).autocomplete("close");
                }
                var exist = false;
                $.each(methodNameDatas, function (index, item) {
                    if (item.label == $(el).val()) {
                        $(el).val(item.label);
                        exist = true;
                        return;
                    }
                });
                //if (!exist) $(el).val(""); 不存在时则不清空.
            });
        });
    }

    //remarkRegex
    var remarkRegexData = function (data) {
        var temp = [];
        $.each(data, function (index, item) {

            temp[temp.length] = { label: item.Name, value: item.ID }
        });
        remarkRegexDatas = temp;

        $(".remarkRegexSelector").each(function (index, el) {
            $(el).autocomplete({
                minLength: 0,
                delay: 0,
                source: remarkRegexDatas,
                focus: function (event, ui) {
                    $(el).val(ui.item.value);
                    return false;
                },
                select: function (event, ui) {
                    $(el).val(ui.item.value);
                    return false;
                }
            })
		    .data("autocomplete")._renderItem = function (ul, item) {
		        return $("<li></li>")
				    .data("item.autocomplete", item)
				    .append("<a>" + item.value + "</a>")
				    .appendTo(ul);
		    };

            $(el).bind("focus", function (e) {
                $(el).autocomplete("search", "");
            });
            $(el).bind("blur", function (e) {
                if ($(el).autocomplete("widget").is(":visible")) {
                    $(el).autocomplete("close");
                }
                var exist = false;
                $.each(remarkRegexDatas, function (index, item) {
                    if (item.label == $(el).val()) {
                        $(el).val(item.value);
                    }
                    if (item.value == $(el).val()) {
                        exist = true;
                        return;
                    }
                });
                //if (!exist) $(el).val(""); 不存在时则不清空.
            });
        });
    }

});

