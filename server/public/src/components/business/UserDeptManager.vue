<template>
  <div>
    <el-tabs style="width:100%;">
      <el-tab-pane label="用户部门管理">
        <section>
          <el-col :span="24" class="toolbar">
            <el-form :inline="true" :model="formInline" class="demo-form-inline">
              <el-form-item>
                <el-button @click="changeEdit" type="primary" round>{{isEditText}}</el-button>
              </el-form-item>
            </el-form>
          </el-col>
          <el-col :span="8">
            <el-input placeholder="输入关键字进行过滤" v-model="filterText">
            </el-input>
            <el-tree accordion default-expand-all :expand-on-click-node="expand" highlight-current @current-change="selectDeptTreeChange"
              class="filter-tree" :data="treeDeptData" :props="defaultProps" :filter-node-method="filterNode" ref="deptTree">
            </el-tree>
          </el-col>
          <el-col :span="16">
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
                  <el-switch v-model="scope.row.UserInDept" @change="changeUserDept(scope.row)"></el-switch>
                </template>
              </el-table-column>
            </el-table>
            <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
              <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
                :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength" style="float:right">
              </el-pagination>
            </el-col>
          </el-col>
        </section>
      </el-tab-pane>
    </el-tabs>
  </div>

</template>
<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      formInline: {
        user: ""
      },
      isEditText: "查看模式",
      isEdit: false,
      expand: false,
      filterText: "",
      treeDeptData: [],
      defaultProps: {
        children: "children",
        label: "label"
      },
      tableUserData: [],
      listLoading: false,
      currentPage: 0,
      tableDataLength: 0,
      currentPageSize: 0,
      curDeptID: -1
    };
  },
  created() {
    this.getDeptsData();
  },
  computed: {
    // 使用对象展开运算符将 getters 混入 computed 对象中
    ...mapGetters([
      "getProj"
      // ...
    ])
  },
  methods: {
    //设置用户部门
    changeUserDept(row) {
      console.log(row);

      let that = this;
      let query = {
        UserID: row._id,
        ProjID: that.getProj,
        DeptID: that.curDeptID,
        insertOrDel: row.UserInDept ? "insert" : "del"
      };
      axios
        .post("/api/InsertOrDelUserDept", query)
        .then(res => {
          if (res.data.success) {
            that.$message({
              message: `[${row.UserName}]设置成功`,
              type: "success"
            });
          } else {
            console.log(res.data);
          }
        })
        .catch(err => console.log(err));
    },
    //切换编辑/查看模式
    changeEdit() {
      this.isEdit = !this.isEdit;
      this.isEditText = this.isEdit ? "编辑模式" : "查看模式";
      this.getUserData();
    },
    //树节点切换事件
    selectDeptTreeChange(node) {
      this.curDeptID = node.ID;
      this.getUserData();
    },
    //过滤树节点
    filterNode(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    //性别显示转换
    formatSex: function(row, column) {
      return row.UserSex == 1 ? "男" : row.UserSex == 0 ? "女" : "未知";
    },
    handleEdit(row) {},
    handleDel(row) {},
    handleSizeChange() {},
    handleCurrentChange() {},
    //获取部门数据
    getDeptsData() {
      let that = this;
      that.listLoading = true;
      NProgress.start();
      axios
        .get(`/api/GetDeptByProjID?ProjID=${that.getProj}`)
        .then(res => {
          if (res.data.success) {
            that.treeDeptData = res.data.data;
            //this.curDeptID = this.treeDeptData[0].ID;
            this.getUserData();
          } else {
            //获取失败
          }
          that.listLoading = false;
          NProgress.done();
        })
        .catch(err => {
          console.log(err);

          that.listLoading = false;
          NProgress.done();
        });
    },
    //获取人员资料
    getUserData() {
      let that = this;
      let url = `/api/GetUserByDeptID`;
      let params = {
        limit: that.$data.currentPageSize,
        page: that.$data.currentPage,
        ProjID: that.getProj,
        DeptID: that.curDeptID,
        isEdit: that.isEdit
      };
      that.listLoading = true;
      NProgress.start();
      axios
        .get(url, { params: params })
        .then(res => {
          if (res.data.success) {
            that.$data.tableUserData = res.data.data;
            that.$data.tableDataLength = res.data.meta.count;
            that.listLoading = false;
            NProgress.done();
          } else {
            that.$data.tableUserData = [];
            that.$data.tableDataLength = 0;
            that.listLoading = false;
            NProgress.done();
          }
        })
        .catch(err => console.log(err));
    }
  },
  watch: {
    filterText(val) {
      this.$refs.deptTree.filter(val);
    }
  }
};
</script>
<style scoped>

</style>