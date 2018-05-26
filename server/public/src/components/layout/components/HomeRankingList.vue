<template>
    <el-card shadow="always" class="main">
        <div slot="header" class="clearfix">
            <span>当月红黑榜</span>
        </div>
        <ECharts class="chart1" :options='RankingListData' />
    </el-card>
</template>

<script>
import ECharts from "vue-echarts/components/ECharts";
export default {
  components: {
    ECharts
  },
  data() {
    return {
      RankingListData: {
        grid: {
          left: 20,
          top: 20
        },
        xAxis: {
          type: "value",
          min: 0,
          max: 6
        },
        yAxis: {
          type: "value",
          min: -10,
          max: 10
        },
        series: [
          {
            data: [
              [1, 8, "张三"],
              [2, 7, "李四"],
              [3, 6, "王五"],
              [4, 4, "刘六"],
              [5, 1, "杨七"]
            ],
            type: "bar",
            label: {
              normal: {
                formatter: p => {
                  return p.value[2];
                },
                color: "#909399",
                show: true,
                position: "top"
              }
            }
          },
          {
            data: [
              [1, -8, "张三"],
              [2, -7, "李四"],
              [3, -6, "王五"],
              [4, -4, "刘六"],
              [5, -1, "杨七"]
            ],
            type: "bar",
            label: {
              normal: {
                formatter: p => {
                  return p.value[2];
                },
                color: "#909399",
                show: true,
                position: "bottom"
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
  width: 100%;
  height: 350px;
}
</style>


