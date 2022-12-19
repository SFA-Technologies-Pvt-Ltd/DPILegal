using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_NewPiechart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        StringBuilder sb = new StringBuilder();

 sb.Append("google.charts.load('current', {");
   sb.Append("packages: ['corechart']");
   sb.Append("}).then(function () {");
   sb.Append("var data = google.visualization.arrayToDataTable([");
     sb.Append("['Label', 'Value'],");
     sb.Append("['Billed', 19],");
     sb.Append("['Paid Up', 9],");
     sb.Append("['Not Billed', 2],");
     sb.Append("['Ready for Review', 15],");
     sb.Append("['Not Paid Up', 1]");
     sb.Append("]);");
   sb.Append("var options = {");
       sb.Append("chartArea: {");
       sb.Append("left: 12,");
       sb.Append("top: 12,");
       sb.Append("width: '85%'");
     sb.Append("},");
     sb.Append("colors: ['#3366cc', '#dc3912', '#ff9900', '#109618', '#990099', '#f44336', '#e91e63', '#9c27b0', '#673ab7', '#3f51b5', '#2196f3', '#03a9f4', '#00bcd4', '#009688', '#4caf50', '#8bc34a', '#cddc39', '#ffeb3b', '#ffc107', '#ff9800', '#ff5722', '#795548', '#9e9e9e', '#607d8b', '#000000', '#ffffff'],");
     sb.Append("legend: {");
      sb.Append(" position: 'labeled'");
     sb.Append("}");
  sb.Append(" };");

   sb.Append("var container = document.getElementById('chart_div');");
   sb.Append("var chart = new google.visualization.PieChart(container);");
   sb.Append("var drawCount = 0;");
   sb.Append("var drawMax = 100;");

     sb.Append("google.visualization.events.addListener(chart, 'ready', function () {");
     sb.Append("var observer = new MutationObserver(function () {");
     sb.Append("var svg = container.getElementsByTagName('svg');");
     sb.Append("if (svg.length > 0) {");
     sb.Append("var legend = getLegend(svg[0]);");
    
     sb.Append("if (legend.length !== data.getNumberOfRows()) {");
    
     sb.Append("options.height = parseFloat(svg[0].getAttribute('height')) + 32;");
     sb.Append("drawCount++;");
     sb.Append("if (drawCount < drawMax) {");
     sb.Append("chart.draw(data, options);");
           sb.Append("}");
         sb.Append("} else {");
          // change legend marker colors
          sb.Append(" var colorIndex = 0;");
             sb.Append("legend.forEach(function (legendMarker) {");
             sb.Append("legendMarker.path.setAttribute('stroke', options.colors[colorIndex]);");
             sb.Append("if (legendMarker.hasOwnProperty('circle')) {");
             sb.Append("legendMarker.circle.setAttribute('fill', options.colors[colorIndex]);");
             sb.Append("}");
             sb.Append("colorIndex++;");
             sb.Append("if (colorIndex > options.colors.length) {");
             sb.Append("  colorIndex = 0;");
             sb.Append("}");
          sb.Append(" });");
         sb.Append("}");
      sb.Append(" }");
    sb.Append(" });");
     sb.Append("observer.observe(container, {");
       sb.Append("childList: true,");
      sb.Append(" subtree: true");
    sb.Append(" });");
   sb.Append("});");

  // get array of legend markers -- {path: pathElement, circle: circleElement}
   sb.Append("function getLegend(svg) {");
    sb.Append("var legend = [];");
    sb.Append("Array.prototype.forEach.call(svg.childNodes, function(child) {");
    sb.Append("var group = child.getElementsByTagName('g');");
    sb.Append("Array.prototype.forEach.call(group, function(subGroup) {");
    sb.Append("var path = subGroup.getElementsByTagName('path');");
    sb.Append("if (path.length > 0) {");
    sb.Append("if (path[0].getAttribute('fill') === 'none') {");
    sb.Append("var legendMarker = {");
    sb.Append("path: path[0]");
    sb.Append("};");
    sb.Append("var circle = subGroup.getElementsByTagName('circle');");
    sb.Append("if (circle.length > 0) {");
    sb.Append("legendMarker.circle = circle[0];");
    sb.Append("}");
    sb.Append("legend.push(legendMarker);");
    sb.Append("}");
    sb.Append("}");
    sb.Append("});");
    sb.Append("});");
    sb.Append("return legend;");
    sb.Append("}");
    sb.Append("chart.draw(data, options);");
    sb.Append("});");

   // chart_div.InnerHtml = sb.ToString();
    }
}