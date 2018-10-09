$(function () {
    $('#main_ul').css('display', 'none');

    $("body").find("section").each(
        function () {
            if ($(this).attr('class') == 'wrapperbg wrapperbg2')
                $(this).attr('class', 'wrapperbg wrapperbg3');   //改變背景
        });

       


    //從window.storage 的user_id去檢查school_system_sno，調整樣式

});


