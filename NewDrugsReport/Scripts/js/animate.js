// JavaScript Document

//進入頁面移動
function pageMoving(ID,offset) {
    var _body = (window.opera) ? (document.compatMode == "CSS1Compat" ? $('html') : $('body')) : $('html,body');
    _body.stop(false).animate({
        scrollTop: $(ID).offset().top + offset
    }, 800,'');
    return false;
}


//CheckURL//判斷目前該對應網址然後變色

function checkURL(){
	var fileName=url('file');
	//alert(fileName);
	$('#Nav li').each(function(){
        if($(this).find('a').attr('href')===fileName){
		$(this).addClass('active');	
		}
	});
}

//mobileBtn//黑箱作業
/*
function lightbox(){
	
		$('.lightbox article .sign_btn').click(function(){
		    $('.lightbox').hide();
		    $('.wrapperbg.blur').removeClass('blur');
		});
	}
*/

//mobileBtn//手機選單動作
function mobileBTN(){

	$('.mobileBtnOpen').click(function(){
		$('.block').show();
		$('#Nav,header').addClass('active');
		 return false;
		});
	$('.mobileBtnClose,.block').click(function(){
		$('.block').hide();
		$('#Nav,header').removeClass('active');
		 return false;
		});
}

function alertFunction(){
	$('.search button').click(function(){
		alert('搜尋功能即將開放');
		return false;
	});
    
	$('#Nav ul li a,.feature a,.brand a,.contact a').click(function(){
		alert('即將開放');
		return false;
	});
}

function prdFunction(){
	
	$('.prdContent .btn_control').click(function(){
		$(this).toggleClass('active');
		$(this).parent().find('ul,h4').toggle(); 
		return false;
		});
	
	//$('.prdContent .btn_control').each(function(i) {$(this).click();});
}

function servicelist(){
	$('.servicelist .btnMore').click(function(){
		$('.reportlist2').toggle() ;
		$(this).find('samp').toggle();
		$(this).find('strong').toggle();
		return false;
	});
	
	}


function selectActive(){
	    $('.select a').click(function(){
		    $('.selectArea').show();
		});
		$('.selectArea a.btnColose').click(function(){
		    $('.selectArea').hide();
		});
	}


function select2Active(){
	    $('.date-btn a').click(function(){
		    $('.selectArea2').show();
		});
		$('.selectArea2 a.btnColose2').click(function(){
		    $('.selectArea2').hide();
		});
	}

function select3Active(){
	    $('.select2 a').click(function(){
		    $('.selectArea').show();
		});
		$('.selectArea a.btnColose').click(function(){
		    $('.selectArea').hide();
		});
	}

function select4Active(){
	    
	    $('.search-btn').click(function(){
			$('.searcdrop ul').toggle(150);
			$(this).toggleClass('active');		    	
		});
	/*
		$('.searchclose').click(function(){
		    $('.searcdrop ul').hide();
			$('.search-btn.active').removeClass('active');
		});
		*/
	}


function select5Active(){
	    $('.ul-style2>li>p a').click(function(){
		    $('.selectArea').show();
		});
		$('.selectArea a.btnColose').click(function(){
		    $('.selectArea').hide();
		});
	}



//初始化	
$(function () {
//$(window).on('resize',checkWindow);
	selectActive();
	select2Active();
	select3Active();
	select4Active();
	select5Active();
	prdFunction();
	servicelist();
	checkURL();
	
	//alertFunction();
	
});//$(function).finish


