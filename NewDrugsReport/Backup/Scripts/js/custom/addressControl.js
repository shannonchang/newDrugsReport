$(function () {
	var inputData = {};  //json格式(API需要的參數)

	$("#clear").on('click', function (e) {  //重填
		$("form")[0].reset();    //form 為html標籤
	});


	//預設若縣市沒選擇，則區域及路名不得做選擇
	if ($("#COUNTY_ID").val() == "0") {
		$("#CITY").prop('disabled', true);
		$("#ROAD").prop('disabled', true);
	}

	//縣市下拉連動
	$("#COUNTY_ID").change(function (e) {
		if (this.value != "0") {
			inputData.COMM_CODE = this.value;
			$.ajax({
				type: "POST",
				url: $(this).data("eventurl"),
				datatype: "json",
				data: inputData,
				success: function (result) {
					$("#CITY,#ROAD").find('option').remove().end().append($($('<option/>')
						.attr('value', '0')
						.text('請選擇')));
					$.each(result, function (key, obj) {   //key為索引值, obj為JSON陣列(F12觀察)
						$('#CITY')
							.append($($("<option></option>")
								.attr("value", obj.COMM_CODE)
								.text(obj.COMM_VALUE)));
					});
				},
				error: function (xhr, ajaxOptions, thrownError) {
					alert('Failed to retrieve request features!.');
				}
			});

			$("#CITY").prop('disabled', false);
			$("#ROAD").val($("#ROAD option:first").val());
			$("#ROAD").prop('disabled', true);
		}
		else {
			$("#CITY").val($("#CITY option:first").val());
			$("#CITY").prop('disabled', true);
			$("#ROAD").val($("#ROAD option:first").val());
			$("#ROAD").prop('disabled', true);
		}
	});

	//行政區下拉連動
	$("#CITY").change(function (e) {
		if (this.value != "0") {
			inputData.COMM_CODE = this.value;
			$.ajax({
				cache: false,
				type: "POST",
				url: $(this).data("eventurl"),
				datatype: "json",
				data: inputData,
				success: function (result) {
					$("#ROAD").find("option").remove().end().append($('<option/>').attr('value', '0').text('請選擇'));
					$.each(result, function (key, obj) {
						$("#ROAD").append($("<option/>").attr('value', obj.COMM_CODE).text(obj.COMM_VALUE));
					});
				},
				error: function (xhr, ajaxOptions, thrownError) {
					alert('Failed to retrieve request features!.');
				}
			});
			$("#ROAD").prop('disabled', false);
		}
		else {
			$("#ROAD").val($("#ROAD option:first").val());
			$("#ROAD").prop('disabled', true);
		}
    });


});