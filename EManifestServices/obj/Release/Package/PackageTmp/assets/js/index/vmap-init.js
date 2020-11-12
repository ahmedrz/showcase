var cityAreaData = [
    500.70,
    410.16,
    210.69,
    120.17,
    64.31,
    150.35,
    130.22,
    120.71,
    300.32
]
$('#world-map').vectorMap({
    map: 'world_mill_en',
    scaleColors: ['#C8EEFF', '#0071A4'],
    normalizeFunction: 'polynomial',
    focusOn: {
        x: 5,
        y: 1,
        scale:.85
    },
    zoomOnScroll: false,
    zoomMin: 0.65,
    hoverColor: false,
    regionStyle: {
        initial: {
            fill: '#70b6ff',
            "fill-opacity": 1,
            stroke: '#d5d9dc',
            "stroke-width": 0,
            "stroke-opacity": 0
        },
        hover: {
            "fill-opacity": 0.6
        }
    },
    markerStyle: {
        initial: {
            fill: '#004892 ',
            stroke: 'rgba(212,204,227,.8)',
            "fill-opacity": 1,
            "stroke-width": 6,
            "stroke-opacity": 0.8,
            r: 3
        },
        hover: {
            stroke: 'rgba(212,204,227,1)',
            "stroke-width": 10
        },
        selected: {
            fill: 'blue'
        },
        selectedHover: {}
    },
    backgroundColor: '#ffffff',
    markers: [

        {
            latLng: [61.524010, 105.318756],
            name: 'Russia'
        }, {
            latLng: [40.463669, -3.749220],
            name: 'Spain'
        }, {
            latLng: [48.379433, 31.165581],
            name: 'Ukraine'
        }, {
            latLng: [7.946527, -1.023194],
            name: 'Ghana'
        }, {
            latLng: [21.7679, 78.8718],
            name: 'India'
        }

    ],
    series: {
        markers: [{
            attribute: 'r',
            scale: [3, 7],
            values: cityAreaData
        }]
    }
});