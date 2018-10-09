$(function () {

    //權限設定
    var auth = JSON.parse(storage.getItem('auth'));

    if (auth.MenuItem06.Add == 0) {
        $('.newbtn').remove();   //移除新增按鈕
    }

    //其他的權限設定在 _DownloadGrid.cshtml裡面

    //$("#PAGE_SIZE").change(function (e) {
    //    postQuery();
    //});

    //$("#query").on('click', function (e) {
    //    postQuery();
    //});

    //postQuery = function () {
    //    var inputData = {};
    //    var formData = $("form").serializeArray();

    //    $.each(formData, function (i, field) {
    //        inputData[field.name] = field.value;
    //    });

    //    inputData["CR_USER"] = 'admin';
    //    inputData["CR_IP"] = '192.168.0.1';  //待修改

    //    inputData["PAGE_SIZE"] = $('#PAGE_SIZE').val();  //因為沒在<form>內，所以只好這樣給值


    //    $.ajax({
    //        catch: 'false',
    //        url: '/DownloadData/GetGridRequests',
    //        type: 'POST',
    //        data: inputData,
    //        success: function (result) {
    //            $("#grid").html(result);
    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //            alert('Failed to retrieve request features!.');
    //        }
    //    });
    //};


});