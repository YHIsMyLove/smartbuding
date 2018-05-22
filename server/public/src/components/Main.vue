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
        // { id: "chartColumn", label: "柱状图", checked: false },
      ],
      currentDate: new Date()
      // chartColumn: null,
    };
  },
  mounted: function() {
    // var _this = this;
    // //基于准备好的dom，初始化echarts实例
    // this.chartColumn = echarts.init(document.getElementById("chartColumn"));
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