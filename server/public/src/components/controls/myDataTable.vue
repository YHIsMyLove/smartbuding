<template>
<div id='main'>
  <el-table border fit stripe :data="TableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">
    <el-table-column type="index" label="编号" width="85" /> 
    <el-table-column :prop="item.prop" :label="item.label" v-for="item in TabelField" />
    <el-table-column label="操作" width="100">
        <template scope="scope">
            <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
            <el-button type="text" size="small" @click="handleDel(scope.row)">删除</el-button>
        </template>
    </el-table-column>
  </el-table>
  <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
          :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength"
          style="float:right">
      </el-pagination>
  </el-col>
</div>
</template>
 
<script>
import util from "../../common/util";
import NProgress from "nprogress";
export default {
  props: ["TableData", "TabelField"],
  data() {
    return {
      listLoading: false,
      currentPage: 1,
      currentPageSize: 10,
      tableDataLength: 0
    };
  },
  created() {
    NProgress.start();
    console.log("init ");
    NProgress.done();
  },
  methods: {
    handleEdit(row) {
      this.$emit("EditRow", row);
    },
    handleDel(row) {
      var _this = this;
      _this
        .$confirm("确认提交吗？", "提示", {})
        .then(() => {
          NProgress.start();
          this.$emit("DelRow", row);
          NProgress.done();
        })
        .catch(err => {
          console.log(err);
        });
    },
    handleSizeChange(val) {
      NProgress.start();
      this.$data.currentPageSize = val;
      this.$emit("RefTable");
      NProgress.done();
    },
    handleCurrentChange(val) {
      NProgress.start();
      this.$data.currentPage = val;
      this.$emit("RefTable");
      NProgress.done();
    }
  }
};
</script>
<style scoped>
.main {
  width: 100%;
  height: 100%;
}
</style>