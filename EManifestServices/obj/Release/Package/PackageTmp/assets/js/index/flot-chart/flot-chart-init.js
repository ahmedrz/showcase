
// combine chart
$(function() {
    var data24Hours = [
        [0, 201],
        [1, 520],
        [2, 337],
        [3, 261],
        [4, 157],
        [5, 78],
        [6, 58],
        [7, 48],
        [8, 54],
        [9, 38],
        [10, 88],
        [11, 214],
        [12, 364],
        [13, 449],
        [14, 558],
        [15, 282],
        [16, 379],
        [17, 429],
        [18, 518],
        [19, 470],
        [20, 330],
        [21, 245],
        [22, 358],
        [23, 74]
    ];
    var data48Hours = [
        [0, 245],
        [1, 492],
        [2, 538],
        [3, 332],
        [4, 234],
        [5, 143],
        [6, 147],
        [7, 63],
        [8, 64],
        [9, 43],
        [10, 486],
        [11, 201],
        [12, 315],
        [13, 397],
        [14, 612],
        [15, 281],
        [16, 370],
        [17, 279],
        [18, 425],
        [19, 53],
        [20, 122],
        [21, 355],
        [22, 340],
        [23, 801]
    ];
    var dataDifference = [
        [23, 727],
        [22, 128],
        [21, 110],
        [20, 92],
        [19, 172],
        [18, 63],
        [17, 150],
        [16, 592],
        [15, 12],
        [14, 246],
        [13, 52],
        [12, 149],
        [11, 123],
        [10, 2],
        [9, 325],
        [8, 10],
        [7, 15],
        [6, 89],
        [5, 65],
        [4, 77],
        [3, 271],
        [2, 401],
        [1, 72],
        [0, 156]
    ];
    var ticks = [
        [0, "22h"],
        [1, ""],
        [2, "00h"],
        [3, ""],
        [4, "02h"],
        [5, ""],
        [6, "04h"],
        [7, ""],
        [8, "06h"],
        [9, ""],
        [10, "08h"],
        [11, ""],
        [12, "10h"],
        [13, ""],
        [14, "12h"],
        [15, ""],
        [16, "14h"],
        [17, ""],
        [18, "16h"],
        [19, ""],
        [20, "18h"],
        [21, ""],
        [22, "20h"],
        [23, ""]
    ];
    var data = [{
        label: "Last 24 Hours",
        data: data24Hours,
        lines: {
            show: true,
            fill: true
        },
        points: {
            show: true
        }
    }, {
        label: "Last 48 Hours",
        data: data48Hours,
        lines: {
            show: true
        },
        points: {
            show: true
        }
    }, {
        label: "Difference",
        data: dataDifference,
        bars: {
            show: true
        }
    }];
    var options = {
        xaxis: {
            ticks: ticks
        },
        series: {
            shadowSize: 0
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#e5ebf8",
            borderWidth: 1,
            borderColor: "#e5ebf8"
        },
        colors: ["#FF518A", "#36a2f5", "#34bfa3"],
        tooltip: true,
        tooltipOpts: {
            defaultTheme: false
        },
        legend: {
            labelBoxBorderColor: "#000000",
            container: $("#legendcontainer26"),
            noColumns: 0
        }
    };
    var plot = $.plot($("#combine-chart #combine-chart-container"),
            data, options);
});
