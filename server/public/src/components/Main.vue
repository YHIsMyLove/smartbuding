<template>        
  <section class="chart">
    <el-row>
      <el-col :span="24">
          <el-checkbox v-model="i.checked" v-for="i in showList" :key="i.id" :label="i.label">
          </el-checkbox>
      </el-col>
    </el-row>

    <el-row>
        <el-col :span="24" v-for="(i,index) in showList" :key="index" v-show="i.checked">
          <el-card :body-style="{ padding: '0px' }">
            <div :id="i.id" style="width:100%; height:600px;"></div>
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
      showList: [
        { id: "chartColumn", label: "柱状图", checked: false },
        { id: "chartBar", label: "条状图", checked: false },
        { id: "chartLine", label: "线状图", checked: false },
        { id: "chartPie", label: "饼图", checked: false },
        { id: "chartCustom", label: "自定义", checked: true }
      ],
      currentDate: new Date(),
      chartColumn: null,
      chartBar: null,
      chartLine: null,
      chartCustom: null,
      chartPie: null
    };
  },
  mounted: function() {
    var _this = this;
    //基于准备好的dom，初始化echarts实例
    this.chartColumn = echarts.init(document.getElementById("chartColumn"));
    this.chartBar = echarts.init(document.getElementById("chartBar"));
    this.chartLine = echarts.init(document.getElementById("chartLine"));
    this.chartPie = echarts.init(document.getElementById("chartPie"));

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
          data: [20, 10, 12, 23, 45, 65]
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

    /************************************************************************ */
    /************************************************************************ */
    /************************************************************************ */

    this.chartCustom = echarts.init(document.getElementById("chartCustom"));

    this.chartCustom.setOption({
      title: {
        text: "动物基因",
        subtext: "动物基因曲线图"
      },
      dataZoom: {
        show: true,
        start: 0,
        end: 10
      },
      calculable: true,
      xAxis: [
        {
          type: "value",
          splitNumber: 10
        }
      ],
      yAxis: [
        {
          type: "value"
        }
      ],
      series: []
    });

    //异步加载数据
    let allcount = 1000;
    let data = [];
    for (let index = 2; index < allcount + 2; index++) {
      let item = {
        name: "jy" + index,
        type: "line",
        smooth: true,
        symbolSize: 0,
        data: (function() {
          let lst = [];
          lst.push([index - 2, 0]);
          lst.push([
            index - 1,
            Math.random() > 0.5 ? Math.random() * 10 : -Math.random() * 10
          ]);
          lst.push([index, 0]);
          return lst;
        })()
      };
      data.push(item);
    }

    let data2 = [];
    for (let index = 2; index < allcount + 2; index++) {
      data2.push([index - 2, 0]);
      data2.push([
        index - 1,
        Math.random() > 0.5 ? Math.random() * 10 : -Math.random() * 10
      ]);
      data2.push([index, 0]);
    }

    //console.log(data);
    // this.chartCustom.setOption({
    //   series: data
    // });

    this.chartCustom.setOption({
      series: [
        {
          name: "jy",
          type: "line",
          smooth: true,
          symbolSize: 0,
          data: data2
        }
      ]
    });

    /************************************************************************ */
    /************************************************************************ */
    /************************************************************************ */
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