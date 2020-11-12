var Script = function () {
    $("#sparkline_2").sparkline([0, 5, 6, 10, 9, 12, 4, 9, 4, 2, 7, 9, 6, 2], {
              type: 'bar',
              height: '78',
              barWidth: '5',
              resize: true,
              barSpacing: '15',              
        barColor: '#A768F3'
    });

}();
