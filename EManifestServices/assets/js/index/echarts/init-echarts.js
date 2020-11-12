$(document).ready(function() {


    <!--basic area echarts init-->

       var dom = document.getElementById("b-area");
       var myChart = echarts.init(dom);

       var app = {};
       option = null;
       option = {
           color: ['#eac459','#FF518A', '#34bfa3' ],

           tooltip : {
               trigger: 'axis'
           },
           legend: {
               data:['Laptop','Mobile','Watch']
           },

           calculable : true,
           xAxis : [
               {
                   type : 'category',
                   boundaryGap : false,
                   data : ['Mon','Tue','Wed','Thu','Fri','Sat','Sun']
               }
           ],
           yAxis : [
               {
                   type : 'value'
               }
           ],
           series : [
               {
                   name:'Laptop',
                   type:'line',
                   smooth:true,
                   itemStyle: {normal: {areaStyle: {type: 'default'}}},
                   data:[10, 12, 21, 54, 260, 830, 710]
               },
               {
                   name:'Mobile',
                   type:'line',
                   smooth:true,
                   itemStyle: {normal: {areaStyle: {type: 'default'}}},
                   data:[30, 182, 434, 791, 390, 30, 10]
               },
               {
                   name:'Watch',
                   type:'line',
                   smooth:true,
                   itemStyle: {normal: {areaStyle: {type: 'default'}}},
                   data:[1320, 1132, 601, 234, 120, 90, 20]
               }
           ]
       };


       if (option && typeof option === "object") {
           myChart.setOption(option, false);
       }


      $('.menu-toggler').on('click',function() {
          setTimeout(function(){
         myChart.resize();
     })
       });
});
