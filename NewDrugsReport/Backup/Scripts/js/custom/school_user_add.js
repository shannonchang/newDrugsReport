$(function () {

    var inputData = {};
    $('#ACCOUNT,#PASSWORD,#ACCOUNT_NAME,#EMAIL').change(function (e) {
        var currentId = $(this).attr('id').toLowerCase();
        $('#' + currentId + "_validate_lbl").text('');
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
    }else {
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