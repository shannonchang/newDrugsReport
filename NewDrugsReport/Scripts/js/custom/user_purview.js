
$(function () {

	var condition;
	var data = {};

	$("#back").on('click', function (e) {
		history.back();
	});
	
	$('#query').on('click', function (e) {
		condition = $("form").serializeArray();
		/*
		if ($('#COUNTY_ID').val() == "0") {
			alert("請選擇縣市");
			return false;
		}
		if ($('#SCHOOL_SYSTEM_SNO').val() == "0") {
			alert("請選擇學制");
			return false;
		}*/

		$.each(condition, function (i, field) {
			data[field.name] = field.value;
        });

        data["USER_ID"] = $('#USER_ID_HD').val();

		$.ajax({
			cache: false,
			type: "POST",
			url: "/UserData/GetUserListRequests",
			data: data,
			datatype: "JSON",
			success: function (result) {
				//$('#grid').html(result);;
				$("#USER_ID").find("option").remove();
				$.each(result, function (i, field) {
					console.log("值:" + field.USER_ID + " 顯示:" + field.SCHOOL);
					$("#USER_ID").append($('<option/>').attr('value', field.COMM_CODE).text(field.COMM_VALUE));

				});
			},
			error: function (xhr, ajaxOptions, thrownError) {
				alert('Failed to retrieve request features!.');
			}
		});
		//debugger;
	});

	//單筆移入群組
	$("#singleToRight").on('click', function (e) {
		var inputData = {};
		$("#USER_ID option:selected").each(function (i, selected) {
			console.log($(selected).val());
			console.log($(selected).text());
			$("#RELATIVE_USER_ID").append($("<option/>").attr('value', $(selected).val()).text($(selected).text()));
			inputData.USER_ID = $("#account_lbl").text();
			inputData.RELATIVE_USER_ID = $(selected).val();
			//屆時還要加入CR_USER欄位
			postControl(inputData,"add");  //ajax funtion

		});
		$("#USER_ID option:selected").remove();
	});

	//單筆移出群組
	$("#singleToLeft").on('click', function (e) {
		var inputData = {};

		$("#RELATIVE_USER_ID option:selected").each(function (i, selected) {
			//console.log($(selected).val());
			//console.log($(selected).text());
			$("#USER_ID").append($("<option/>").attr('value', $(selected).val()).text($(selected).text()));
			inputData.USER_ID = $("#account_lbl").text();
			inputData.RELATIVE_USER_ID = $(selected).val();

			postControl(inputData, "move");  //ajax funtion
			
		});
		$("#RELATIVE_USER_ID option:selected").remove();
	});
	
	///移入群組
	$("#allToRight").on('click', function (e) {
        var inputData = [];  //要變成陣列  才能用push
		$("#USER_ID option").each(function (i, selected) {
			//console.log($(selected).val());
			//console.log($(selected).text());
			$("#RELATIVE_USER_ID").append($("<option/>").attr('value', $(selected).val()).text($(selected).text()));
			//inputData.USER_ID = $("#account_lbl").text();
			//inputData.RELATIVE_USER_ID = $(selected).val();
            //postControl(inputData, "add");  //ajax funtion
            inputData.push({ "USER_ID": $("#account_lbl").text(), "RELATIVE_USER_ID": $(selected).val() });

        });
        var jsonInput = JSON.stringify(inputData);
        postControl({"jsonInput": jsonInput }, "addList");

		$("#USER_ID option").remove();
	});

	///移出群組
	$("#allToLeft").on('click', function (e) {
        var inputData = [];   //要變成陣列  才能用push
		$("#RELATIVE_USER_ID option").each(function (i, selected) {
			//console.log($(selected).val());
			//console.log($(selected).text());
			$("#USER_ID").append($("<option/>").attr('value', $(selected).val()).text($(selected).text()));
			//inputData.USER_ID = $("#account_lbl").text();
			//inputData.RELATIVE_USER_ID = $(selected).val();
            inputData.push({ "USER_ID": $("#account_lbl").text(), "RELATIVE_USER_ID": $(selected).val() });
			//postControl(inputData, "move");  //ajax funtion

        });

        var jsonInput = JSON.stringify(inputData);
        postControl({ "jsonInput": jsonInput }, "moveList");

		$("#RELATIVE_USER_ID option").remove();
	});


	//給單筆及全部共用 (新增與刪除)
	postControl = function (inputData, type) {
        //debugger;
		$.ajax({
			cache: false,
			type: "POST",
			url: "/UserData/" + type + "UserRelativeRequests",
            data: inputData,
			success: function (result) {
				//alert('success!');
			},
			error: function (xhr, ajaxOptions, thrownError) {
				alert('Failed to retrieve request features!.');
			}
		});
	}

});