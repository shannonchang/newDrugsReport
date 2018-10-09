$(function () {
	var condition;
	var data = {};
	$('#query').on('click', function (e) {

		//condition = $("form").serializeArray();
		//$.each(condition, function (i, field) {
		//	data[field.name] = field.value;
		//});


        //linkSubmit($(this).data("href"), {
        //    "USER_ID": $('#USER_ID').val(),
        //    "SCHOOL_SYSTEM_SNO": $('#SCHOOL_SYSTEM_SNO').val(),
        //    "COUNTY_ID": $('#COUNTY_ID').val(),
        //    "SCHOOL": $('#SCHOOL').val()
        //});  //在_layout.cshtml中

        /*
		$.ajax({
			cache: false,
			type: "POST",
			url: "/UserData/GetGridRequests",
			data: data,
			success: function (result) {
				$('#grid').html(result);
			},
			error: function (xhr, ajaxOptions, thrownError) {
				alert('Failed to retrieve request features!.');
			}
		});*/

		//debugger;
	});

});