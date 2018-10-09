$(function () {
    var selector = ".specificBtn button:nth-child(5)";
    $(selector).removeClass();
    $(selector).addClass("specificBtn01");

    $("#main_ul li").removeClass();  //在_Layout.cshtml裡面
    $("#main_ul li:nth-child(7)").addClass("active");
});