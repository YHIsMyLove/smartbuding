<template>
    <el-card shadow="always" class="main">
        <div slot="header" class="clearfix">
            <span>环境监测</span>
        </div>
        <div id="chart3"></div>
        <div id="chart2"></div>
    </el-card>  
</template>
<script>
export default {
  mounted() {
    let chart3 = this.$echarts.init(document.getElementById("chart3"));
    chart3.setOption({
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
    });

    let chart2 = this.$echarts.init(document.getElementById("chart2"));
    chart2.setOption({
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
    });
  },
  data() {
    return {};
  }
};
</script>

<style scoped>
.main {
  margin-top: 15px;
  margin-left: 5px;
  height: 400px;
}
#chart3 {
  height: 100px;
  width: 100%;
}
#chart2 {
  height: 200px;
  width: 100%;
}
</style>
