var Script = function () {
    $("#sparkline_22").sparkline([102,109,90,120,70,99,110,80,87,50,65,74], {
        type: 'bar',
        height: '58',
        barWidth: 5,
        barSpacing: 2,
        barColor: '#4dd0e1'
    });
        $("#sparkline").sparkline([102,109,90,120,70,99,110,80,87,50,65,74], {
        type: 'bar',
        height: '20',
        barWidth: 3,
        barSpacing: 1,
        barColor: '#444'
    });
    
            $("#sparkline2").sparkline([102,109,90,120,70,99,110,80,87,50,65,74], {
        type: 'bar',
        height: '20',
        barWidth: 3,
        barSpacing: 1,
        barColor: '#c1c1c1'
    });
}();
