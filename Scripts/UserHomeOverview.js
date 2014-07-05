$(function () {
    //Draw spending by type chart.
    $('#spent-pie-container').highcharts({
        chart: {
            plotBackgroundColor: '#333',
            plotBorderWidth: null,
            plotShadow: false,
            backgroundColor: '#333'
        },
        title: {
            style: "color:#DDDDDD",
            text: 'Spending By Transaction Type'
            
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b> <br/> <b>\u00a3{point.y:.0f}</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %  <br/> \u00a3{point.y:.0f}',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || '#DDD'
                    }
                }
            }
        },
        series: [{
            type: 'pie',
            name: 'Spending By Type',
            data: window.spentChartData
        }],
        pane: { background: '#333' },
        credits: {enabled: false}
    });

    //Draw earning by type chart.
    $('#earned-pie-container').highcharts({
        chart: {
            plotBackgroundColor: '#333',
            plotBorderWidth: null,
            plotShadow: false,
            backgroundColor: '#333'
        },
        title: {
            style: "color:#DDDDDD",
            text: 'Earning By Transaction Type'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b> <br/> <b>\u00a3{point.y:.0f}</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %  <br/> \u00a3{point.y:.0f}',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || '#DDD'
                    }
                }
            }
        },
        series: [{
            type: 'pie',
            name: 'Earning By Type',
            data: window.earnedChartData
        }],
        pane: { background: '#333' },
        credits: { enabled: false }
    });

    //Draw ballance over time chart.
    $('#ballance-over-time-graph-container').highcharts({
        chart: {
            type: 'spline',
            plotBackgroundColor: '#333',
            backgroundColor: '#333'
        },
        title: {
            text: 'Bank Ballance Over Time',
            style: "color:#DDDDDD"
        },
        xAxis: {
            type: 'datetime',
            dateTimeLabelFormats: {
                month: '%e %b %Y',
            },
            title: {
                text: 'Date'
            }
        },
        yAxis: {
            title: {
                text: 'Ballance'
            },
            min: 0
        },
        tooltip: {
            headerFormat: '<b>{series.name}</b><br>',
            pointFormat: '{point.x:%e %b %Y}: \u00a3{point.y:.2f}'
        },
        labels: {
            style: {
                color: '#DDDDDD'
            }
        },
        series: [{
            name: 'Ballance over time',
            data: window.ballanceOverTimeChartData
        }],
        credits: { enabled: false }
    });
});