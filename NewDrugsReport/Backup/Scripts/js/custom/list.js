$(function () {
    $("#PAGE_SIZE").change(function (e) {
        postQuery();
    });
    //匯出EXCEL

    $('#clear').on("click", function () {
        $('form')[0].reset();
    });

    $("#query").on('click', function (e) {
        postQuery();
    });

    postQuery = function () {
        var inputData = {};
        var formData = $("form").serializeArray();

        $.each(formData, function (i, field) {
            inputData[field.name] = field.value;
        });

        inputData["FILL_STATUS"] = $('#FILL_STATUS').val();
        inputData["SCHOOL"] = $('#SCHOOL').val();
        inputData["UP_DATE"] = $('#UP_DATE').val();
        inputData["SCHOOL_SYSTEM_SNO"] = $('#SCHOOL_SYSTEM_SNO').val();
        inputData["ACCOUNT_NAME"] = $('#ACCOUNT_NAME').val();
        inputData["COUNTY_ID"] = $('#COUNTY_ID').val();
        inputData["PAGE_SIZE"] = $('#PAGE_SIZE').val();  //因為沒在<form>內，所以只好這樣給值
        inputData["USER_ID"] = $('#USER_ID').val();


        $.ajax({
            catch: 'false',
            url: '/SpecialMember/GetGridRequests',
            type: 'POST',
            data: inputData,
            success: function (result) {
                $("#grid").html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Failed to retrieve request features!.');
            }
        });
    };


});