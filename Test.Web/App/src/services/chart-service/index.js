import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

const service = {
    renderChart: function (elementId, jsonData) {

        am4core.useTheme(am4themes_animated);

        var chart = am4core.create(elementId, am4charts.XYChart);
        chart.cursor = new am4charts.XYCursor();
        for (var i of jsonData) {
            i.timestampUtc = new Date(i.timestampUtc);
        }

        // Add data
        chart.data = jsonData; //[{

        // Create axes
        var dateAxis = chart.xAxes.push(new am4charts.DateAxis());
        dateAxis.baseInterval = {
            "timeUnit": "minute",
            "count": 1
        };
        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

        // Create series
        var series = chart.series.push(new am4charts.LineSeries());
        series.tooltipText = "{valueY.value}";
        series.dataFields.valueY = "value";
        series.dataFields.dateX = "timestampUtc";
        series.name = "Temp";
    }
}

export default service;