﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SmartConstructionSite.Core.EnvMonitor.Views
{
    public partial class EnvMonitorMainPage : ContentPage
    {
        private static readonly string html = @"<!DOCTYPE html>"
            + "<html>"
            +   "<head>"
            +       "<meta charset=\"utf-8\">"
            +       "<title>ECharts</title>"
            +       "<script>"
            +       "</script>"
            +   "</head>"
            +   "<body>"
            +       "<div id = \"main\" style=\"width: 600px;height:400px;\" ></div>"
            +       "<script type= \"text/javascript\">"
            +           "var myChart = echarts.init(document.getElementById('main'));"

            +           "var option = {"
            +               "title: {"
            +                   "text: 'ECharts 入门示例'"
            +               "},"
            +               "tooltip: {},"
            +               "legend: {"
            +                   "data:['销量']"
            +               "},"
            +               "xAxis: {"
            +                   "data: [\"衬衫\",\"羊毛衫\",\"雪纺衫\",\"裤子\",\"高跟鞋\",\"袜子\"]"
            +               "},"
            +               "yAxis: {},"
            +               "series: [{"
            +                   "name: '销量',"
            +                   "type: 'bar',"
            +                   "data: [5, 20, 36, 10, 10, 20]"
            +               "}]"
            +           "};"

            +           "myChart.setOption(option);"
            +       "</script>"
            +   "</body>"
            + "</html>";
        public EnvMonitorMainPage()
        {
            InitializeComponent();
            var htmlWebViewSource = new HtmlWebViewSource();
            htmlWebViewSource.Html = @"";
            chartContainer.Source = htmlWebViewSource;
        }
    }
}
