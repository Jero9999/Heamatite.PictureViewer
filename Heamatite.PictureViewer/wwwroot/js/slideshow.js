
function GetMoreImagesForCarousel() {
	
	var carousel = $('#carouselExampleIndicators');
	var text = 1;
	var activeCarouselItem = carousel.find('.item.active');
	var allCarouselItems = carousel.find('.item');
	var numberOfCarouselItems = allCarouselItems.length;
	var activeItemIndex = allCarouselItems.index(activeCarouselItem);

	if (numberOfCarouselItems - activeItemIndex < 2) {
		var newCarouselItem = activeCarouselItem.clone();
		newCarouselItem.removeClass('active');
		newCarouselItem.text(text++);
		activeCarouselItem.parent().append(newCarouselItem);
	}

}

$(function () {
	
	$('#carouselExampleIndicators')
		.bcSwipe({ threshold: 50 })
		.on('click', 'a.right', GetMoreImagesForCarousel);
})