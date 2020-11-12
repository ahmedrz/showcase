var Script = function() {

	// easy pie chart

	$('.ep_22').easyPieChart({
		animate : 1000,
		size : 60,
		barColor : '#a768f3'
	});

	$(".spark6").sparkline([70, 60, 65, 60, 50, 60, 70], {
		type : 'bar',
		barWidth : 3,
		barSpacing : 10,
		height : '40',
		barColor : '#426af1'
	});

	$(".spark7").sparkline([70, 60, 65, 60, 50, 60, 70], {
		type : 'bar',
		barWidth : 3,
		barSpacing : 10,
		height : '40',
		barColor : '#e0276a'
	});

	$(".spark8").sparkline([70, 60, 65, 60, 50, 60, 70], {
		type : 'bar',
		barWidth : 3,
		barSpacing : 10,
		height : '40',
		barColor : '#009f3c'
	});
	
	    $(".spark9").sparkline([70, 60, 65, 60, 50, 60, 70], {
        type: 'bar',
        barWidth: 3,
        barSpacing: 10,
        height: '40',
        barColor: '#3fbcef'});

	$(".spark4").sparkline([5, 6, 7, 9, 9, 5, 3, 2, 2, 4, 6, 7, 5, 6, 7, 9, 9, 5, 3, 2, 2, 4, 6, 7], {
		type : 'line',
		width : '100',
		height : '21',
		lineColor : '#426af1',
		fillColor : 'transparent',
		lineWidth : 2,
		spotColor : 'transparent',
		minSpotColor : 'transparent',
		maxSpotColor : 'transparent'
	});

}(); 