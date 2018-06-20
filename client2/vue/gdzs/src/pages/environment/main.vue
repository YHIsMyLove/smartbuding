<template>
    <f7-page>
        <f7-navbar title="环境监测" back-link="Back"></f7-navbar>
        <f7-tabs>
            <f7-tab id="tab1" tab-active>
                <div id="chart-pm-2-5" style="width:100%;height:100%;"></div>
            </f7-tab>
            <f7-tab id="tab2" @tab:show="initChartPm1_0">
                <div id="chart-pm-1-0" style="width:100%;height:100%;"></div>
            </f7-tab>
            <f7-tab id="tab3" @tab:show="initChartSd">
                <div id="chart-sd" style="width:100%;height:100%;"></div>
            </f7-tab>
            <f7-tab id="tab4" @tab:show="initChartTemp">
                <div id="chart-temp" style="width:100%;height:100%;"></div>
            </f7-tab>
            <f7-tab id="tab5" @tab:show="initChartNoise">
                <div id="chart-noise" style="width:100%;height:100%;"></div>
            </f7-tab>
            <f7-tab id="tab6" @tab:show="initChartWindPower">
                <div id="chart-wind-power" style="width:100%;height:100%;"></div>
            </f7-tab>
            <f7-tab id="tab7" @tab:show="initChartWindDir">
                <div id="chart-wind-dir" style="width:100%;height:100%;"></div>
            </f7-tab>
            <f7-tab id="tab8" @tab:show="initChartWindSpeed">
                <div id="chart-wind-speed" style="width:100%;height:100%;"></div>
            </f7-tab>
        </f7-tabs>
        <div slot="fixed" id="tabs">
            <f7-row :no-gap="true">
                <f7-col width="25">
                    <div class="border-right border-top">
                        <f7-link tab-link="#tab1" tab-link-active @click="refreshChartPm2_5()">
                            <img class="img-little" src="static/imgs/PM2点5.png" alt="">
                            <div>155.0</div>
                        </f7-link>
                    </div>
                </f7-col>
                <f7-col width="25">
                    <div class="border-right border-top">
                        <f7-link tab-link="#tab2" @click="refreshChartPm1_0()">
                            <img class="img-little" src="static/imgs/PM10.png" alt="">
                            <div>155.0</div>
                        </f7-link>
                    </div>
                </f7-col>
                <f7-col width="25">
                    <div class="border-right border-top">
                        <f7-link tab-link="#tab3" @click="refreshChartSd()">
                            <img class="img-little" src="static/imgs/湿度.png" alt="">
                            <div>155.0</div>
                        </f7-link>
                    </div>
                </f7-col>
                <f7-col width="25">
                    <div class="border-top">
                        <f7-link tab-link="#tab4" @click="refreshChartTemp()">
                            <img class="img-little" src="static/imgs/温度.png" alt="">
                            <div>155.0</div>
                        </f7-link>
                    </div>
                </f7-col>
                <f7-col width="25">
                    <div class="border-right border-top">
                        <f7-link tab-link="#tab5" @click="refreshChartNoise()">
                            <img class="img-little" src="static/imgs/噪音.png" alt="">
                            <div>155.0</div>
                        </f7-link>
                    </div>
                </f7-col>
                <f7-col width="25">
                    <div class="border-right border-top">
                        <f7-link tab-link="#tab6" @click="refreshChartWindPower()">
                            <img class="img-little" src="static/imgs/风力.png" alt="">
                            <div>155.0</div>
                        </f7-link>
                    </div>
                </f7-col>
                <f7-col width="25">
                    <div class="border-right border-top">
                        <f7-link tab-link="#tab7" @click="refreshChartWindDir()">
                            <img class="img-little" src="static/imgs/风向.png" alt="">
                            <div>155.0</div>
                        </f7-link>
                    </div>
                </f7-col>
                <f7-col width="25">
                    <div class="border-top">
                        <f7-link tab-link="#tab8" @click="refreshChartWindSpeed()">
                            <img class="img-little" src="static/imgs/风速.png" alt="">
                            <div>155.0</div>
                        </f7-link>
                    </div>
                </f7-col>
            </f7-row>
        </div>
    </f7-page>
</template>
<script>
// 引入基本模板
let echarts = require("echarts/lib/echarts");
// 引入柱状图组件
require("echarts/lib/chart/bar");
// 引入提示框和title组件
require("echarts/lib/component/tooltip");
require("echarts/lib/component/title");
var chartPm2_5;
var chartPm1_0;
var chartSd;
var chartTemp;
var chartNoise;
var chartWindPower;
var chartWindDir;
var chartWindSpeed;
export default {
  data() {
    return {
      chartPm2_5Initialized: false,
      chartPm1_0Initialized: false,
      chartSdInitialized: false,
      chartTempInitialized: false,
      chartNoiseInitialized: false,
      chartWindPowerInitialized: false,
      chartWindDirInitialized: false,
      chartWindSpeedInitialized: false,
    };
  },
  mounted() {
    this.initChartPm2_5();
  },
  methods: {
    initChartPm2_5() {
      if (this.chartPm2_5Initialized) return;
      this.chartPm2_5Initialized = true;
      chartPm2_5 = echarts.init(document.getElementById("chart-pm-2-5"));
      chartPm2_5.setOption({
        title: { text: "PM2.5" },
        tooltip: {},
        xAxis: {
          data: ["9AM", "10AM", "11AM", "12AM", "1PM", "2PM", "3PM", "4PM"]
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 68, 10, 10, 20, 10, 20]
          }
        ]
      });
    },
    initChartPm1_0() {
      if (this.chartPm1_0Initialized) return;
      this.chartPm1_0Initialized = true;
      chartPm1_0 = echarts.init(document.getElementById("chart-pm-1-0"));
      chartPm1_0.setOption({
        title: { text: "PM10" },
        tooltip: {},
        xAxis: {
          data: ["9AM", "10AM", "11AM", "12AM", "1PM", "2PM", "3PM", "4PM"]
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 68, 10, 10, 20, 10, 20]
          }
        ]
      }, true);
    },
    initChartSd() {
      if (this.chartSdInitialized) return;
      this.chartSdInitialized = true;
      chartSd = echarts.init(document.getElementById("chart-sd"));
      chartSd.setOption({
        title: { text: "湿度" },
        tooltip: {},
        xAxis: {
          data: ["9AM", "10AM", "11AM", "12AM", "1PM", "2PM", "3PM", "4PM"]
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 68, 10, 10, 20, 10, 20]
          }
        ]
      });
    },
    initChartTemp() {
      if (this.chartTempInitialized) return;
      this.chartTempInitialized = true;
      chartTemp = echarts.init(document.getElementById("chart-temp"));
      chartTemp.setOption({
        title: { text: "温度" },
        tooltip: {},
        xAxis: {
          data: ["9AM", "10AM", "11AM", "12AM", "1PM", "2PM", "3PM", "4PM"]
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 68, 10, 10, 20, 10, 20]
          }
        ]
      });
    },
    initChartNoise() {
      if (this.chartNoiseInitialized) return;
      this.chartNoiseInitialized = true;
      chartNoise = echarts.init(document.getElementById("chart-noise"));
      chartNoise.setOption({
        title: { text: "噪音" },
        tooltip: {},
        xAxis: {
          data: ["9AM", "10AM", "11AM", "12AM", "1PM", "2PM", "3PM", "4PM"]
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 68, 10, 10, 20, 10, 20]
          }
        ]
      });
    },
    initChartWindPower() {
      if (this.chartWindPowerInitialized) return;
      this.chartWindPowerInitialized = true;
      chartWindPower = echarts.init(document.getElementById("chart-wind-power"));
      chartWindPower.setOption({
        title: { text: "风力" },
        tooltip: {},
        xAxis: {
          data: ["9AM", "10AM", "11AM", "12AM", "1PM", "2PM", "3PM", "4PM"]
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 68, 10, 10, 20, 10, 20]
          }
        ]
      });
    },
    initChartWindDir() {
      if (this.chartWindDirInitialized) return;
      this.chartWindDirInitialized = true;
      chartWindDir = echarts.init(document.getElementById("chart-wind-dir"));
      chartWindDir.setOption({
        title: { text: "风向" },
        tooltip: {},
        xAxis: {
          data: ["9AM", "10AM", "11AM", "12AM", "1PM", "2PM", "3PM", "4PM"]
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 68, 10, 10, 20, 10, 20]
          }
        ]
      });
    },
    initChartWindSpeed() {
      if (this.chartWindSpeedInitialized) return;
      this.chartWindSpeedInitialized = true;
      chartWindSpeed = echarts.init(document.getElementById("chart-wind-speed"));
      chartWindSpeed.setOption({
        title: { text: "风速" },
        tooltip: {},
        xAxis: {
          data: ["9AM", "10AM", "11AM", "12AM", "1PM", "2PM", "3PM", "4PM"]
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 68, 10, 10, 20, 10, 20]
          }
        ]
      });
    }
  }
};
</script>

<style scoped>
a {
  display: block;
  padding: 16px;
}
div[class*="col"] {
  text-align: center;
}
.tab-link-active {
  background-color: #ccc;
}
.border-right a {
  border-right: 1px solid #ccc;
}
.border-top a {
  border-top: 1px solid #ccc;
}
#tabs {
  position: absolute;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 500px;
}
.page {
  padding-bottom: 185px;
}
.tabs {
  height: 100%;
}
.tabs .tab {
  height: 100%;
}
</style>

