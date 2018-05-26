<template>
    <div class="main">
        <el-col :span="4" v-for="(item,index) in StatisticalItems" :key="index">
            <el-card shadow="always">
                <span class="item1">{{item.Count}}</span><span class="item2">({{item.Unit}})</span>
                <br>
                <span class="item2">{{item.Name}}</span>
            </el-card>
        </el-col>
    </div>
</template>
<script>
export default {
  data() {
    return {
      StatisticalItems: [
        { Name: "安全施工", Unit: "天", Count: 10 },
        { Name: "剩余工期", Unit: "天", Count: 1240 },
        { Name: "工地人数", Unit: "人", Count: 896 },
        { Name: "在场人数", Unit: "人", Count: 542 },
        { Name: "离场人数", Unit: "人", Count: 354 },
        { Name: "总设备数", Unit: "台", Count: 248 }
      ]
    };
  }
};
</script>
<style scoped>
.main {
  margin-top: 15px;
  height: 80px;
}
.item1 {
  font-size: 25px;
  color: #409eff;
}
.item2 {
  font-size: 15px;
  color: #909399;
}
</style>
