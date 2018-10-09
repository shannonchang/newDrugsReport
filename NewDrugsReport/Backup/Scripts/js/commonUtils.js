var defaultOptionFunction = {
        errorFunction:function(xhr, textStatus, errorThrown){
            $.unblockUi();
        },
        completeFunction:function(){
        },
        codeFunction:{
            codeFunction:{
                400:function(){
                    console.log("Http Status 400");
                    bootbox.alert("逾時操作，請重新登入", function(){
                        window.sessionStorage.removeItem("drugsToken");
                        window.location.href = $("#indexPath").val();
                    });
                    $.unblockUi();
                }
            }
        }
    };
function doAjaxProcess(url,parameters,successFunction, optionFunction){
    var obj = $.extend(defaultOptionFunction, optionFunction);
    var ajaxDalog = bootbox.dialog({ 
        message: '<div class="text-center"><i class="loadding"></i>&nbsp;Loading...</div>', 
        closeButton:false,
        backdrop : true
    });
    ajaxDalog.init(function(){
        var divPlent = ($(window).height() / 2)-$(".modal-content").height();
        $(ajaxDalog).find(".modal-dialog").css({"margin-top":divPlent+"px", "margin-left": "30%", "width":"40%"});
        $.ajax( {
            type: "post",       // the default
            url: url,
            dataType : "json",
            data: parameters,
            processData: true,
            async: false,
            timeout:300000,
            contentType: 'application/json; charset=utf-8',
            beforeSend:function(){
            },
            success:function(result){
                if(result.status == "success"){
                    if("token" in result){
                        window.sessionStorage.setItem('drugsToken', result.token);
                    }
                }
                eval("successFunction(result)");
            },
            error: obj.errorFunction,
            statusCode:obj.codeFunction,
            complete:obj.completeFunction
        }).done(function(){
            ajaxDalog.modal("hide");
        });
    });
}
function doAjaxUploadFile(url, formData, successFunction, optionFunction){
    var obj = $.extend(defaultOptionFunction, optionFunction);
    var ajaxDalog = bootbox.dialog({ 
        message: '<div class="text-center"><i class="loadding"></i>&nbsp;Loading...</div>', 
        closeButton:false,
        backdrop : true
    });
    ajaxDalog.init(function(){
        var divPlent = ($(window).height() / 2)-$(".modal-content").height();
        $(ajaxDalog).find(".modal-dialog").css({"margin-top":divPlent+"px", "margin-left": "30%", "width":"40%"});
        $.ajax({
            url:url,
            type: "post",
            data:formData,
            enctype:"multipart/form-data",
            processData: false,
            contentType: false,
            cache: false,
            timeout:300000,
            success: successFunction,
            error: function (){
            },
            statusCode:obj.codeFunction,
            complete:obj.completeFunction
        }).done(function(){
            ajaxDalog.modal("hide");
        });
    });
}
/**
 * 
 * @param {any} url
 * @param {any} parameters
 * @param {any} successFunction
 * @param {any} optionFunction
 */
function doAjaxLoadPage(url, parameters, successFunction, optionFunction){
    var obj = $.extend(defaultOptionFunction, optionFunction);
    var ajaxDalog = bootbox.dialog({ 
        message: '<div class="text-center"><i class="loadding"></i>&nbsp;Loading...</div>', 
        closeButton:false,
        backdrop : true
    });
    ajaxDalog.init(function(){
        var divPlent = ($(window).height() / 2)-$(".modal-content").height();
        $(ajaxDalog).find(".modal-dialog").css({"margin-top":divPlent+"px", "margin-left": "30%", "width":"40%"});
        $.ajax({
            url:url,
            type:"post",
            dataType:"html",
            timeout:300000,
            data:parameters,
            success:successFunction,
            error:function(){
            },
            statusCode:obj.codeFunction,
            complete:obj.completeFunction
        }).done(function(){
            ajaxDalog.modal("hide");
        });
    });
}
/**
 * 
 * @param {any} url
 * @param {any} parameters
 * @param {any} successFunction
 * @param {any} optionFunction
function doAjaxDownload(url, parameters, successFunction, optionFunction) {
    var obj = $.extend(defaultOptionFunction, optionFunction);
    var ajaxDalog = bootbox.dialog({ 
        message: '<div class="text-center"><i class="loadding"></i>&nbsp;Loading...</div>', 
        closeButton:false,
        backdrop : true
    });
    ajaxDalog.init(function(){
        var divPlent = ($(window).height() / 2)-$(".modal-content").height();
        $(ajaxDalog).find(".modal-dialog").css({"margin-top":divPlent+"px", "margin-left": "30%", "width":"40%"});
        $.ajax({
            url:url,
            type:"post",
            contentType: 'application/download; charset=utf-8',
            timeout:300000,
            data:parameters,
            success:successFunction,
            error:function(){
            },
            statusCode:obj.codeFunction,
            complete:obj.completeFunction
        }).done(function(){
            ajaxDalog.modal("hide");
        });
    });
}
*/

/**
 * serializeArray
 * to
 * serializeJson
 * @returns
 */
function serializeToJson(formId){
	var formArray = $("#"+formId).serializeArray()
	var returnArray = {};
	for (var i = 0; i < formArray.length; i++){
		returnArray[formArray[i]['name']] = formArray[i]['value'];
	}
	return returnArray;
}

function getAllTagName(selector){
    var arr = [];
    $(selector).each(function(){
        if($(this).prop("name") && $.inArray($(this).prop("name"),arr) < 0){
            arr.push($(this).prop("name"));
        }
    });
    return arr;
}

function isMoblie(){
	return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
}

/**
 * 
 * @returns
 */
function validIsLeap(Year){
	if (((Year % 4)==0) && ((Year % 100)!=0) || ((Year % 400)==0)){
		return true;
	}else{
		return false;
	}
}

function getAge(dateString) {
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}

function checkTwID(id){
    //建立字母分數陣列(A~Z)
    var city = new Array(1,10,19,28,37,46,55,64,39,73,82, 2,11,20,48,29,38,47,56,65,74,83,21, 3,12,30)
    id = id.toUpperCase();
    //使用「正規表達式」檢驗格式
    if (id.search(/^[A-Z](1|2)\d{8}$/i) == -1) {
        return false;
    } else {
        //將字串分割為陣列(IE必需這麼做才不會出錯)
        id = id.split('');
        //計算總分
        var total = city[id[0].charCodeAt(0)-65];
        for(var i=1; i<=8; i++){
            total += eval(id[i]) * (9 - i);
        }
        //補上檢查碼(最後一碼)
        total += eval(id[9]);
        //檢查比對碼(餘數應為0);
        return ((total%10 == 0 ));
    }
}

function jsonObject(tagList){
    var param = {};
    $.each(tagList, function(i, name){
        if($("[name="+name+"]").prop("type") == "radio"){
            param[name] = $("[name="+name+"]:checked").val();
        }else if($("[name="+name+"]").prop("type") == "checked"){
            var tmpValue = [];
            $("[name="+name+"]:checked").each(function(i, tag){
                tmpValue.push($(tag).val());
            });
            param[name] = tmpValue.join(",");
        }else{
            param[name] = $("[name="+name+"]").val();
        }
    });
    return param;
}

(function($){
	$.extend($.fn,{
		tableGrid : function(tableData){
			var $tds = $(this).find("thead tr th");
			$(this).find("tbody").remove();
			$(this).append(function(){
				var $tbody = $("<tbody>");
				$.each(tableData.rows,function(i,json){
					$tbody.append(function(){
						var $tr = $("<tr>");
						$.each($tds, function(n,element){
							var val = json[$(element).data("name")];
							if($(element).data("function")){
								val = eval($(element).data("function")+"(val, json)");
							}
							$tr.append(function(){
                                var $td = $("<td>").append(val);
								if($(element).data("hidden")){
									if($(element).data("hidden") == "Y"){
										$(element).hide();
										$td.hide();
									}
								}
								return $td;
							});
						});
						return $tr;
					});
				});
				return $tbody;
			});
			$("#"+$(this).prop("id")+"PageController").pageController(tableData.totel, tableData.page, tableData.rowNum, tableData.pageSize);
		},
		pageController: function(totel, page, rowNum, pageSize){
            $(this).empty();
			$(this).append("共").append($("<b>").text(rowNum)).append("筆資料，第").append($("<b>").text(page)).append("/").append($("<b>").text(totel));
            $(this).append("頁，每頁顯示").append(function(){
                var $select = $("<select>").attr({"id":"pageSize"});
                for(var i=10; i < 50; i=i+10){
                    $select.append(function(){
                        var $option = $("<option>").attr({"value":i}).text(i);
                        if(pageSize == i){
                            $option.attr({"selected":"selected"});
                        }
                        return $option;
                    });
                }
                return $select;
            }).append("筆 到 第 ").append(function(){
                var $select = $("<select>").attr({"id":"toPage"});
                for(var i=1; i <= totel; i++){
                    $select.append(function(){
                        var $option = $("<option>").attr({"value":i}).text(i);
                        if(page == i){
                            $option.attr({"selected":"selected"});
                        }
                        return $option;
                    });
                }
                return $select;
            }).append("頁");
		},
        tableClean:function(){
            $(this).find("tbody").remove();
            $("#"+$(this).prop("id")+"PageController").pageController(1, 1, 0);
        }
	});
    
    $.extend({
        gDialog:null,
        blockUi:function(){
            if($.gDialog === null){
                $.gDialog = bootbox.dialog({ 
                    message: '<div class="text-center"><i class="loadding"></i>&nbsp;Loading...</div>', 
                    closeButton:false
                });
            }else{
                $.gDialog.modal("show");
            }
            var divPlent = ($(window).height() / 2)-$(".modal-content").height();
            $($.gDialog).find(".modal-dialog").css({"margin-top":divPlent+"px", "margin-left": "30%", "width":"40%"});
        },
        unblockUi:function(){
            if($.gDialog !== null){
                $.gDialog.modal("hide");
            }
            $(".modal-backdrop").remove();
        },
        chtDate:function(date){
            return (date.getFullYear() - 1911) + "/" + ((date.getMonth() + 1) < 10 ? "0" : "") + (date.getMonth() + 1) + "/" + ((date.getDate() + 1) < 10 ? "0" : "") + date.getDate();
        },
        enDate:function(twDate){
            var arr = twDate.split("/");
            return (parseInt(arr[0]) + 1911) + "/" + arr[1] + "/" + arr[2];
        }
    });

    /* 身份證隱碼
    $("#stuIdNoEecode").bind("keyup keypress change paste",function(e){
        if(e.type == "paste"){
            return false;
        }
        var valArr = $(this).val().split("");
        if(e.which != 8 && e.which != 46){
            if(valArr.length > $("#stuIdNo").val().length){
                $("#stuIdNo").val($("#stuIdNo").val() + valArr[valArr.length - 1]);
            }else if($("#stuIdNo").val().length > valArr.length){
                $("#stuIdNo").val(valArr.join(""));
            }
        }else{
            if(!$(this).val()){
                $("#stuIdNo").val("");
            }else{
                var resArr = $("#stuIdNo").val().split("");
                resArr.splice(resArr.length -1 ,resArr.length - valArr.length);
                $("#stuIdNo").val(resArr.join(""));
            }
        }
        if(valArr.length > 6){
            valArr[valArr.length - 1] = "*";
        }
        $(this).val(valArr.join(""));
        console.log("Encode :" + $(this).val() + " Decode: "+$("#stuIdNo").val());
    });*/

})(jQuery);