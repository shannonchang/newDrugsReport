$(function () {
	var inputData = {};
    var violate_count = 0;

    $('#NAME,#SCHOOL,#SCHOOL_ADDRESS,#SCHOOL_PRESIDENT,#JOB,#PHONE,#EMAIL').change(function (e) {
        var currentId = $(this).attr('id').toLowerCase();
        $('#' + currentId + "_validate_lbl").text('');
    });
    $('#save').on('click', function (e) {

        if (!$.trim($('#USER_ID').val().replace(/\　\t/g, ""))) {
            $('#code_validate_lbl').text('必填');
            alert('請填寫學校代號!');
            $('#USER_ID').focus();
            return false;
        }

        if (!$.trim($('#ACCOUNT').val().replace(/\　\t/g, ""))) {
            $('#validate_lbl').text('必填');
            alert('請填寫帳號!');
            $('#ACCOUNT').focus();
            return false;
        }

        if (!$.trim($('#PASSWORD').val().replace(/\　/g, ""))) {
            alert("請填寫密碼");
            $('#PASSWORD').focus();
            return false;
        }
        if (passwordValidator($("#PASSWORD").val()) == "N") {
            return false;
        }

        if ($('#PASSWORD2').val() != $('#PASSWORD').val()) {
            alert('確認密碼欄位需與密碼欄位相同，請重新輸入');
            $('#PASSWORD2').focus();
            return false;
        }


        //$('#SCHOOL').val($('#SCHOOL').val().replace(" ", ""));  //不可輸入空白

        if (!$.trim($('#SCHOOL').val().replace(/\　/g, ""))) {
            $('#school_validate_lbl').text('必填');
            alert('請填寫校名（全銜）!');
            $("#SCHOOL").focus();
            return false;
        }

        if ($('#SCHOOL_SYSTEM_SNO').val() == "0") {
            alert("請選擇學制");
            return false;
        }


        if ($('#COUNTY_ID').val() == "0") {
            alert("請選擇縣市");
            return false;
        }
        if ($('#CITY').val() == "0") {
            alert("請選擇區域");
            return false;
        }
        if ($('#ROAD').val() == "0") {
            alert("請選擇街道/路名");
            return false;
        }

        if (!$.trim($('#SCHOOL_ADDRESS').val().replace(/\　/g, ""))) {
            $('#school_address_validate_lbl').text('必填');
            alert('請填寫完整校址!');
            $("#SCHOOL_ADDRESS").focus();
            return false;
        }
        /*
        if (!$.trim($('#SCHOOL_PRESIDENT').val().replace(/\　/g, ""))) {
            $('#school_president_validate_lbl').text('必填');
            alert('請填寫校長名稱!');
            $("#SCHOOL_PRESIDENT").focus();
            return false;
        }*/

        /*20180309新增*/
        if (!$.trim($('#NAME').val().replace(/\　/g, ""))) {
            $('#name_validate_lbl').text('必填');
            alert('請填寫春暉承辦人姓名!');
            $('#NAME').focus();
            return false;
        }

        if (!$.trim($('#JOB').val().replace(/\　/g, ""))) {
            $('#job_validate_lbl').text('必填');
            alert('請填寫春暉承辦人職稱!');
            $('#JOB').focus();
            return false;
        }


        if (!$.trim($('#PHONE').val().replace(/\　/g, ""))) {
            $('#phone_validate_lbl').text('必填');
            alert('請填寫春暉承辦人電話!');
            $('#PHONE').focus();
            return false;
        }

        if (!$.trim($('#EMAIL').val().replace(/\　/g, ""))) {
            $('#email_validate_lbl').text('必填');
            alert('請填寫春暉承辦人E-mail');
            $('#EMAIL').focus();
            return false;
        }

        if (emailValidator($("#EMAIL").val()) == "N") {
            return false;
        }


        if ($('#ACCOUNT_STATUS').val() == "0") {
            alert("請選擇帳號狀態");
            return false;
        }

		var formData = $("form").serializeArray();
		$.each(formData, function (i, field) {
			inputData[field.name] = field.value;
		});


		$.ajax({
			cache: false,
			type: "POST",
			url: $(this).data("saveurl"),
			data: inputData,
			success: function () {
                alert("新增成功");
                linkSubmit($('[id=save]').data("href"), { "userId": $('#USER_ID').val() });  //在_layout.cshtml
				//window.location.href = "/UserData/user_purview_edit?userid=" + $('#USER_ID').val();
			},
			error: function (xhr, ajaxOptions, thrownError) {
				alert('Failed to retrieve request features!.');
			}
		});
	});
	

    $("#PASSWORD").change(function (e) {
        passwordValidator(this.value);
    });

    $("#EMAIL").change(function (e) {
        emailValidator(this.value);
    });

    $("#PASSWORD2").change(function (e) {
        if ($('#PASSWORD2').val() != $('#PASSWORD').val()) {
            $("#password2_validate_lbl").html("確認密碼欄位需與密碼欄位相同，請重新輸入");
            $('#PASSWORD2').val('');
            $('#PASSWORD2').focus();
        }
        else {
            $("#password2_validate_lbl").text('');
        }
    });


    passwordValidator = function (text) {
        var key;
 
        var passwordregex8digits = new RegExp("^(?=.{8,})");
        var passwordregexLowercase = new RegExp("^(?=.*[a-z,A-Z])");
        //var passwordregexUppercase = new RegExp("^(?=.*[A-Z])");
        var passwordregexNumber = new RegExp("^(?=.*[0-9])");
        //var passwordRegexSpecial = new RegExp("^(?=.*[!@#$%^&*])");
        //var passwordRegexAll = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})");

        if (!passwordregex8digits.test(text)) {
            violate_count++;
        }
        if (!passwordregexLowercase.test(text)) {
            violate_count++;
        }
		/*
		if (!passwordregexUppercase.test(text)) {
			violate_count++;
		}
		*/
        if (!passwordregexNumber.test(text)) {
            violate_count++;
        }
        /*
		if (!passwordRegexSpecial.test(text)) {
			violate_count++;
		}
        */
		/*
		if (!passwordRegexAll.test(text)) {
			violate_count++;
		}*/

        //alert('違規次數:' + violate_count);

        if (violate_count > 0) {
            key = "N";
            $("#PASSWORD").focus();
            $("#password_validate_lbl").html("密碼原則：需8位數以上的英文、<br />數字混合，不接受空白字元");
            $("#password_validate_lbl").addClass("invalid-text");
        }else if(text === "1qaz2wsx"){
            key = "N";
            $("#PASSWORD").focus();
            $("#password_validate_lbl").text("不合法密碼，請重新輸入。");
            $("#password_validate_lbl").addClass("invalid-text");
        }else {
            key = "Y";
            $("#password_validate_lbl").text("");
        }

        violate_count = 0;  //歸零計算
        return key;
    }

    emailValidator = function (inputData) {
        var key;

        const re = /^(([.](?=[^.]|^))|[\w_%{|}#$~`+!?-])+@(?:[\w-]+\.)+[a-zA-Z.]{2,63}$/;
        if (re.test(inputData)) {
            key = "Y"
            $("#email_validate_lbl").text("");
        }
        else {
            key = "N";
            $("#email_validate_lbl").text("請輸入符合格式的EMAIL");
            $("#email_validate_lbl").addClass("invalid-text");
        }

        return key;
    }

});