$(function () {
    var inputData = {};

    /*
    $('#PASSWORD').keyup(function (e) {
        $('#PASSWORD').val($('#PASSWORD').val().replace(" ", "").replace(/　/i, ""));    //不可輸入空白
    });

    $('#PASSWORD').change(function (e) {
        $('#PASSWORD').val($('#PASSWORD').val().trim());    //清除空白
    });

    $('#ACCOUNT_NAME').keyup(function (e) {
        $('#ACCOUNT_NAME').val($('#ACCOUNT_NAME').val().replace(" ", "").replace(/　/i, ""));  //不可輸入空白
    });

    $('#ACCOUNT_NAME').change(function (e) {
        $('#ACCOUNT_NAME').val($('#ACCOUNT_NAME').val().trim());  //不可輸入空白
    });
    */

    $('#ACCOUNT,#PASSWORD,#ACCOUNT_NAME,#EMAIL').change(function (e) {
        var currentId = $(this).attr('id').toLowerCase();
        $('#' + currentId + "_validate_lbl").text('');
    });
    $('#clear').on("click", function () {
        $('form')[0].reset();
    });

    $("#PASSWORD").change(function (e) {
        
        passwordValidator(this.value);
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

    $("#EMAIL").change(function (e) {
        emailValidator(this.value);
    });

    //帳號重複性檢查
    $("#ACCOUNT").keyup(function (e) {

        $('#ACCOUNT').val($('#ACCOUNT').val().replace(" ", ""));  //不可輸入空白

        var regex = new RegExp("^[a-zA-Z0-9 ]+$");

        if (!regex.test(this.value)) {
            $("#ACCOUNT").val('');
            $("#ACCOUNT").focus();
            $("#account_validate_lbl").text("請輸入英文或數字");
            $("#account_validate_lbl").addClass("invalid-text");
            return false;
        }

        var formData = $("form").serializeArray();
        $.each(formData, function (i, field) {
            inputData[field.name] = field.value;
        });


    });
});

function passwordValidator(text) {
    var key;
    var violate_count = 0;
    var passwordregex8digits = new RegExp("^(?=.{8,})");
    var passwordregexLowercase = new RegExp("^(?=.*[a-z,A-Z])");

    var passwordregexNumber = new RegExp("^(?=.*[0-9])");


    if (!passwordregex8digits.test(text)) {
        violate_count++;
    }
    if (!passwordregexLowercase.test(text)) {
        violate_count++;
    }

    if (!passwordregexNumber.test(text)) {
        violate_count++;
    }


    if (violate_count > 0) {
        key = "N";
        $("#PASSWORD").focus();
        $("#password_validate_lbl").text("密碼原則：需8位數以上的英文、數字混合，不接受空白字元");
        $("#password_validate_lbl").addClass("invalid-text");
    }else if(text === "1qaz2wsx"){
        key = "N";
        $("#PASSWORD").focus();
        $("#password_validate_lbl").text("不合法密碼，請重新輸入。");
        $("#password_validate_lbl").addClass("invalid-text");
    }
    else {
        key = "Y";
        $("#password_validate_lbl").text("");
    }

    violate_count = 0;  //歸零計算
    return key;
}

function emailValidator(inputData) {
    var key;

    var re = /^(([.](?=[^.]|^))|[\w_%{|}#$~`+!?-])+@(?:[\w-]+\.)+[a-zA-Z.]{2,63}$/;
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