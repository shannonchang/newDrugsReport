$(function () {
    var auth = JSON.parse(storage.getItem('auth'));
    if (auth.MenuItem01 == 0) {
        $('ul[class="inlineBlock TabMenu"] li:nth-child(1)').css("display", "none");  //特定人員   Drug/Index.cshtml
        $('#main_ul li:nth-child(1)').css("display", "none");   //_layout.cshtml
    }

    if (auth.MenuItem02 == 0) {
        $('ul[class="inlineBlock TabMenu"] li:nth-child(2)').css("display", "none");  //已成立春暉小組    Drug/Index.cshtml
        $('#main_ul li:nth-child(2)').css("display", "none");   //_layout.cshtml
    }

    if (auth.MenuItem03 == 0) {
        $('ul[class="inlineBlock TabMenu"] li:nth-child(3)').css("display", "none");  //未成立春暉小組   Drug/Index.cshtml
        $('#main_ul li:nth-child(3)').css("display", "none");   //_layout.cshtml
    }


    if (auth.MenuItem04 == 0) {
        $('ul[class="inlineBlock TabMenu"] li:nth-child(4)').css("display", "none");  //重大案件    Drug/Index.cshtml
        $('#main_ul li:nth-child(4)').css("display", "none");   //_layout.cshtml
    }


    if (auth.MenuItem05 == 0) {
        $('ul[class="inlineBlock TabMenu"] li:nth-child(5)').css("display", "none");  //報表專區   Drug/Index.cshtml
        $('#main_ul li:nth-child(5)').css("display", "none");   //_layout.cshtml
    }


    if (auth.MenuItem06 == null) {
        $('ul[class="inlineBlock TabMenu"] li:nth-child(6)').css("display", "none");  //下載專區    Drug/Index.cshtml
        $('#main_ul li:nth-child(6)').css("display", "none");   //_layout.cshtml
    }

    //下載專區細部的權限移動至 download.js 避免影響到整個網站的同名元件
    //下載專區細部的權限移動至 _downloadGrid.cshtml 並且用C#控制(Grid的內容)


    if (auth.MenuItem07.InnerItem01 == 0) {
        $('.search-box.report-box a:nth-child(3)').remove();   //user_edit.cshtml
    }

    if (auth.MenuItem07.InnerItem02 == 0) {
        $('.specificBtn button:nth-child(2)').css("display", "none");   //_auth_menu.cshtml
    }

    if (auth.MenuItem07.InnerItem03 == 0) {
        $('.specificBtn button:nth-child(3)').css("display", "none");   //_auth_menu.cshtml
    }

    if (auth.MenuItem07.InnerItem04 == 0) {
        $('.specificBtn button:nth-child(4)').css("display", "none");   //_auth_menu.cshtml
    }

    if (auth.MenuItem07.InnerItem05 == 0) {
        $('.specificBtn button:nth-child(5)').css("display", "none");   //_auth_menu.cshtml
    }
});
