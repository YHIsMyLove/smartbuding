<template>
<div id='main'>
  <el-table border fit stripe :data="TableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">
    <el-table-column type="index" label="编号" width="85" /> 
    <el-table-column :prop="item.prop" :label="item.label" :key="index" v-for="(item,index) in TabelField" />
    <el-table-column label="操作" width="100">
        <template scope="scope">
            <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
            <el-button type="text" size="small" @click="handleDel(scope.row)">删除</el-button>
        </template>
    </el-table-column>
  </el-table>
  <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
          :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="TableDataLength"
          style="float:right">
      </el-pagination>
  </el-col>
</div>
</template>
 
<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
export default {
  props: [
    "TableData",
    "TableDataLength",
    "TabelField",
    "ModelName",
    "SystemEditRow"
  ],
  data() {
    return {
      listLoading: false,
      currentPage: 1,
      currentPageSize: 10
    };
  },
  created() {
    this.getTabelData();
  },
  methods: {
    getTabelData(dosomethings) {
      let that = this;
      var params = {
        limit: that.$data.currentPageSize,
        page: that.$data.currentPage
      };
      NProgress.start();
      this.listLoading = true;
      that.$emit("RefTable");
      this.listLoading = false;
      NProgress.done();
    },
    systemEdit(row) {
      this.listLoading = true;
      try {
        let index = 0;
        let str = `{`;
        Object.keys(row).forEach(item => {
          str += `"data${index}":{`;
          this.TabelField.forEach(ti => {
            if (ti.prop == item) {
              str += `"prop":"${item}","label":"${ti.label}","type":"${
                ti.type
              }",`;
            }
          });
          str += `"data":"${row[item]}"},`;
          index++;
        });
        str = str.substr(0, str.length - 1) + "}";
        this.$emit("EditRow", row, JSON.parse(str));
        this.listLoading = false;
      } catch (error) {
        this.listLoading = false;
      }
    },
    handleEdit(row) {
      if (this.SystemEditRow) this.systemEdit(row);
      this.$emit("EditRow", row);
    },
    handleDel(row) {
      var _this = this;
      _this
        .$confirm("确定删除吗?", "系统提示", {})
        .then(() => {
          NProgress.start();
          _this.$emit("DelRow", row);
          NProgress.done();
        })
        .catch(err => {
          console.log(err);
          NProgress.done();
        });
    },
    handleSizeChange(val) {
      this.$data.currentPageSize = val;
      this.$emit("RefTable");
    },
    handleCurrentChange(val) {
      this.$data.currentPage = val;
      this.$emit("RefTable");
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