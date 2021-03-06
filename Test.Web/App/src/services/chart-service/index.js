import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import * as am4plugins_timeline from "@amcharts/amcharts4/plugins/timeline";
import DateService from '../date-service';

am4core.useTheme(am4themes_animated);

const service = {
    renderTimelineChart: function (elementId, jsonData, sensorType) {
        var chart = am4core.create(elementId, am4plugins_timeline.CurveChart); 
        chart.data = jsonData; 

        var date = new Date()

        for (var i of jsonData) {

            i.timestampUtc = DateService.formatDate(i.timestampUtc, {
                format: "HH:mm:ss"
            });
        }

        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "timestampUtc";
        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

        var series = chart.series.push(new am4charts.CurvedColumnSeries());
        series.dataFields.valueY = "value";
        series.dataFields.categoryX = "timestampUtc";
        series.columns.template.fillOpacity = 0.5;
        series.columns.template.strokeWidth = 2;
        series.strokeWidth = 3;

        return chart;
    },
    renderChart: function (elementId, jsonData, sensorType) {

        var chart = am4core.create(elementId, am4charts.XYChart);
        chart.cursor = new am4charts.XYCursor();
        
        // Add data
        chart.data = jsonData; //[{
        chart.scrollbarX = new am4core.Scrollbar();
        chart.scrollbarY = new am4core.Scrollbar();

        // Create axes
        var dateAxis = chart.xAxes.push(new am4charts.DateAxis());
        dateAxis.renderer.grid.template.location = 0;
        dateAxis.renderer.minGridDistance = 40;
        dateAxis.baseInterval = {
            "timeUnit": "minute",
            "count": 15
        };

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

        // Zoom events
        valueAxis.events.on("startchanged", valueAxisZoomed);
        valueAxis.events.on("endchanged", valueAxisZoomed);

        function valueAxisZoomed(ev) {
            var start = ev.target.minZoomed;
            var end = ev.target.maxZoomed;
            console.log("New range: " + start + " -- " + end);
        }

        dateAxis.events.on("startchanged", dateAxisChanged);
        dateAxis.events.on("endchanged", dateAxisChanged);

        function dateAxisChanged(ev) {
            var start = new Date(ev.target.minZoomed);
            var end = new Date(ev.target.maxZoomed);
            console.log("New range: " + start + " -- " + end);
        }

        // Create series
        var series = chart.series.push(new am4charts.LineSeries());
        series.tooltipText = "{valueY.value}";
        series.dataFields.valueY = "value";
        series.dataFields.dateX = "timestampUtc";
        series.name = "Temp";

        if (sensorType == "state") {

            //return this.renderTimelineChart(elementId, jsonData, sensorType);
            valueAxis.min = 0;
            valueAxis.max = 1;
            valueAxis.extraMin = 0;
            valueAxis.extraMax = 0;
            valueAxis.renderer.labels.template.disabled = true;
            valueAxis.labelsEnabled = false;

            var closedRange = valueAxis.createSeriesRange(series);
            closedRange.label.disabled = false;
            closedRange.label.text = "Closed";
            closedRange.label.fill = am4core.color("#FF0000");
            closedRange.value = 0
            closedRange.endValue = 0;

            var openedRange = valueAxis.createSeriesRange(series);
            openedRange.label.disabled = false;
            openedRange.label.text = "Opened";
            openedRange.label.fill = am4core.color("#0c0");
            openedRange.value = 1
            openedRange.endValue = 1;
        }

        return chart;
    },
    updateChartData: function (chart, data) {
        data.forEach(value => chart.data.push(value));
        //  chart.data = data;
    }
}

export default service;