<template>
    <el-card shadow="always" class="main">
        <div slot="header" class="clearfix">
            <span>环境监测</span>
        </div>
        <ECharts class="chart1" :options='WeatherOpetion' auto-resize/>
        
        <ECharts class="chart2"  :options='WeatherOpetion2' auto-resize/>
    </el-card>  
</template>
<script>
import ECharts from "vue-echarts/components/ECharts";
import bar from "echarts/lib/chart/bar";
import line from "echarts/lib/chart/line";
import tooltip from "echarts/lib/component/tooltip";
export default {
  components: {
    ECharts,
    tooltip,
    bar,
    line
  },
  data() {
    return {
      WeatherOpetion2: {
        grid: {
          left: 20,
          top: 20
        },
        title: {
          text: "每日图谱"
        },
        color: ["#67C23A"],
        xAxis: {
          type: "category",
          data: [12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24]
        },
        yAxis: {
          type: "value",
          show: false
        },
        series: {
          data: [50, 60, 70, 120, 200, 180, 160, 120, 100, 70, 50, 40, 38],
          type: "line",
          symbolSize: 10
        }
      },
      WeatherOpetion: {
        grid: {
          left: 20,
          top: 20
        },
        color: ["#67C23A"],
        xAxis: {
          type: "category",
          data: ["1"],
          show: false
        },
        yAxis: {
          type: "value",
          show: false
        },
        legend: {
          data: ["PM2.5", "PM10", "温度", "湿度"]
        },
        series: [
          {
            name: "PM2.5",
            data: [100, "μg/m³"],
            type: "bar",
            stack: "PM2.5总量",
            label: {
              normal: {
                formatter: "PM2.5",
                color: "#909399",
                show: true,
                position: "bottom"
              }
            }
          },
          {
            name: "PM2.5",
            data: [200],
            type: "bar",
            stack: "PM2.5总量",
            itemStyle: {
              normal: {
                color: "#C0C4CC"
              }
            }
          },
          {
            name: "PM10",
            data: [40],
            type: "bar",
            stack: "PM10总量",
            label: {
              normal: {
                formatter: "PM10",
                color: "#909399",
                show: true,
                position: "bottom"
              }
            }
          },
          {
            name: "PM10",
            data: [260],
            type: "bar",
            stack: "PM10总量",
            itemStyle: {
              normal: {
                color: "#C0C4CC"
              }
            }
          },
          {
            name: "温度",
            data: [30],
            type: "bar",
            stack: "温度总量",
            label: {
              normal: {
                formatter: "温度",
                color: "#909399",
                show: true,
                position: "bottom"
              }
            }
          },
          {
            name: "温度",
            data: [270],
            type: "bar",
            stack: "温度总量",
            itemStyle: {
              normal: {
                color: "#C0C4CC"
              }
            }
          },
          {
            name: "湿度",
            data: [200],
            type: "bar",
            stack: "湿度总量",
            label: {
              normal: {
                formatter: "湿度",
                color: "#909399",
                show: true,
                position: "bottom"
              },
              normal2: {
                formatter: "200°",
                color: "#303133",
                show: true,
                position: "inside"
              }
            }
          },
          {
            name: "湿度",
            data: [100],
            type: "bar",
            stack: "湿度总量",
            itemStyle: {
              normal: {
                color: "#C0C4CC"
              }
            }
          }
        ]
      }
    };
  }
};
</script>

<style scoped>
.main {
  margin-top: 15px;
  margin-left: 5px;
  height: 400px;
}
.chart1 {
  height: 100px;
  width: 100%;
}
.chart2 {
  height: 200px;
  width: 100%;
}
</style>
