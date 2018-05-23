<template>
  <section class="main">
    <el-row>
      <el-col :span="24">
        <div class="head">
        </div>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <div class="approve"></div>
      </el-col>
      <el-col :span="12">
        <div class="normal"></div>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="18">
        <div class="action"></div>
      </el-col>
      <el-col :span="6">
        <div class="weather"></div>
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
.main {
}
.head {
  background: #e6a23c;
  height: 100px;
  margin: 5px;
}
.approve {
  background: #67c23a;
  height: 380px;
  margin: 5px;
}
.normal {
  background: #f56c6c;
  height: 380px;
  margin: 5px;
}
.action {
  background: #ebeef5;
  height: 280px;
  margin: 5px;
}
.weather {
  background: #909399;
  height: 280px;
  margin: 5px;
}
</style>