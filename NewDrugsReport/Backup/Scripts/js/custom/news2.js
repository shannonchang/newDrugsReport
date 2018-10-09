$(function () {
    $("#PAGE_SIZE").change(function (e) {
        postQuery();
    });

    $("#query").on('click', function (e) {
        postQuery();
    });

    postQuery = function () {
        var inputData = {};
        var formData = $("form").serializeArray();

        //$.each(formData, function (i, field) {
        //    inputData[field.name] = field.value;
        //});

        inputData["PAGE_SIZE"] = $('#PAGE_SIZE').val();  //因為沒在<form>內，所以只好這樣給值
        inputData["TITLE"] = $('#TITLE').val(); 



        //$.ajax({
        //    catch: 'false',
        //    url: '/Drugs/GetGridRequests',
        //    type: 'POST',
        //    data: inputData,
        //    success: function (result) {
        //        $("#grid").html(result);
        //    },
        //    error: function (xhr, ajaxOptions, thrownError) {
        //        alert('Failed to retrieve request features!.');
        //    }
        //});
    };

});