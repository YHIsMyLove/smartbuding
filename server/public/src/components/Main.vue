<template>        
  <section class="chart">
    <el-row>
      <el-col :span="24">
          <el-checkbox-group v-model="checkList">
            <el-checkbox v-model="checkList" v-for="i in showList" :key="i.id" :label="i.label"></el-checkbox>
          </el-checkbox-group>
      </el-col>
    </el-row>

    <el-row>
        <el-col :span="12" v-for="i in showList" v-show="checkList.filter(item=>item == i.label).length > 0">
          <el-card :body-style="{ padding: '0px' }">
            <div :id="i.id" style="width:100%; height:400px;"></div>
            <div style="padding: 14px;">
              <div class="bottom clearfix">
                <time class="time">{{ currentDate }}</time>
                <el-button type="text" class="button">操作按钮</el-button>
              </div>
            </div>
          </el-card>
        </el-col>
    </el-row>
    
  </section>
</template>

<script>
import echarts from "echarts";
export default {
  data() {
    return {
      checkList: ["柱状图", "条状图", "线状图", "饼图"],
      showList: [
        { id: "chartColumn", label: "柱状图" },
        { id: "chartBar", label: "条状图" },
        { id: "chartLine", label: "线状图" },
        { id: "chartPie", label: "饼图" }
      ],
      currentDate: new Date(),
      chartColumn: null,
      chartBar: null,
      chartLine: null,
      chartPie: null
    };
  },
  mounted: function() {
    var _this = this;
    //基于准备好的dom，初始化echarts实例
    this.chartColumn = echarts.init(document.getElementById("chartColumn"));
    console.log("3");
    this.chartBar = echarts.init(document.getElementById("chartBar"));
    console.log("4");
    this.chartLine = echarts.init(document.getElementById("chartLine"));
    console.log("5");
    this.chartPie = echarts.init(document.getElementById("chartPie"));
    console.log("6");

    this.chartColumn.setOption({
      title: { text: "Column Chart" },
      tooltip: {},
      xAxis: {
        data: ["衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子"]
      },
      yAxis: {},
      series: [
        {
          name: "销量",
          type: "bar",
          data: [5, 20, 36, 10, 10, 20]
        }
      ]
    });

    this.chartBar.setOption({
      title: {
        text: "Bar Chart",
        subtext: "数据来自网络"
      },
      tooltip: {
        trigger: "axis",
        axisPointer: {
          type: "shadow"
        }
      },
      legend: {
        data: ["2011年", "2012年"]
      },
      grid: {
        left: "3%",
        right: "4%",
        bottom: "3%",
        containLabel: true
      },
      xAxis: {
        type: "value",
        boundaryGap: [0, 0.01]
      },
      yAxis: {
        type: "category",
        data: ["巴西", "印尼", "美国", "印度", "中国", "世界人口(万)"]
      },
      series: [
        {
          name: "2011年",
          type: "bar",
          data: [18203, 23489, 29034, 104970, 131744, 630230]
        },
        {
          name: "2012年",
          type: "bar",
          data: [19325, 23438, 31000, 121594, 134141, 681807]
        }
      ]
    });

    this.chartLine.setOption({
      title: {
        text: "Line Chart"
      },
      tooltip: {
        trigger: "axis"
      },
      legend: {
        data: ["邮件营销", "联盟广告", "搜索引擎"]
      },
      grid: {
        left: "3%",
        right: "4%",
        bottom: "3%",
        containLabel: true
      },
      xAxis: {
        type: "category",
        boundaryGap: false,
        data: ["周一", "周二", "周三", "周四", "周五", "周六", "周日"]
      },
      yAxis: {
        type: "value"
      },
      series: [
        {
          name: "邮件营销",
          type: "line",
          stack: "总量",
          data: [120, 132, 101, 134, 90, 230, 210]
        },
        {
          name: "联盟广告",
          type: "line",
          stack: "总量",
          data: [220, 182, 191, 234, 290, 330, 310]
        },
        {
          name: "搜索引擎",
          type: "line",
          stack: "总量",
          data: [820, 932, 901, 934, 1290, 1330, 1320]
        }
      ]
    });

    this.chartPie.setOption({
      title: {
        text: "Pie Chart",
        subtext: "纯属虚构",
        x: "center"
      },
      tooltip: {
        trigger: "item",
        formatter: "{a} <br/>{b} : {c} ({d}%)"
      },
      legend: {
        orient: "vertical",
        left: "left",
        data: ["直接访问", "邮件营销", "联盟广告", "视频广告", "搜索引擎"]
      },
      series: [
        {
          name: "访问来源",
          type: "pie",
          radius: "55%",
          center: ["50%", "60%"],
          data: [
            { value: 335, name: "直接访问" },
            { value: 310, name: "邮件营销" },
            { value: 234, name: "联盟广告" },
            { value: 135, name: "视频广告" },
            { value: 1548, name: "搜索引擎" }
          ],
          itemStyle: {
            emphasis: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: "rgba(0, 0, 0, 0.5)"
            }
          }
        }
      ]
    });
  }
};
</script>

<style scoped>
.chart {
  width: 100%;
  float: left;
}
.el-col {
  padding: 30px 20px;
}
.time {
  font-size: 13px;
  color: #999;
}

.bottom {
  margin-top: 13px;
  line-height: 12px;
}

.button {
  padding: 0;
  float: right;
}

.image {
  width: 100%;
  display: block;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}

.clearfix:after {
  clear: both;
}
</style>