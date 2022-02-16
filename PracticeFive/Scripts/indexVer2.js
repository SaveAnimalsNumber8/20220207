$('.fa-bars').click(function() {
    $('.menu').toggleClass('active');
})

$(function() {
    $(".menu li ul").hide();
    $(".menu li").hover(function() {
        $(">ul:not(:animated)", this).slideDown("fast"); // 滑鼠移上去的處理
    }, function() {
        $(">ul", this).slideUp("fast"); // 滑鼠移開的處理
    });
});

$(function () {
	$('#BackTop').click(function () {
		$('html,body').animate({ scrollTop: 0 }, 333);
	});
	$(window).scroll(function () {
		if ($(this).scrollTop() > 300) {
			$('#BackTop').fadeIn(222);
		} else {
			$('#BackTop').stop().fadeOut(222);
		}
	}).scroll();
});