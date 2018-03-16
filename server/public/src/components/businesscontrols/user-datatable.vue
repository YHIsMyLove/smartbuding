<template>
  <div style="width:100%">
        <el-table border fit stripe :data="tableUserData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">
            <el-table-column type="index" label="编号" width="85">
            </el-table-column>
            <el-table-column prop="UserID" label="账号">
            </el-table-column>
            <el-table-column prop="UserName" label="姓名">
            </el-table-column>
            <el-table-column prop="UserSex" label="性别" :formatter="formatSex">
            </el-table-column>
            <el-table-column prop="UserAge" label="年龄">
            </el-table-column>
            <el-table-column prop="UserPhoneNum" label="手机号">
            </el-table-column>
            <el-table-column v-if="isEdit" label="成员" width="100">
            <template scope="scope">
                <el-switch v-model="scope.row.UserInDept"></el-switch>
            </template>
            </el-table-column>
        </el-table>
        <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
            <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
            :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength" style="float:right">
            </el-pagination>
        </el-col>
  </div>
</template>
<script>
export default {
  data() {
    return {
      tableUserData: [],
      listLoading: false,
      currentPage: 0,
      currentPageSize: 0,
      tableDataLength: 0
    };
  },
  methods: {
    //性别显示转换
    formatSex: function(row, column) {
      return row.UserSex == 1 ? "男" : row.UserSex == 0 ? "女" : "未知";
    },
    handleCurrentChange() {},
    handleSizeChange() {}
  }
};
</script>
<style scoped>

</style>


