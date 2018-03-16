<template>
  <div>
    <el-tabs style="width:100%;">
      <el-tab-pane label="部门管理">
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
                  <el-switch v-model="scope.row.UserInDept"></el-switch>
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
      isEditText: "编辑模式",
      isEdit: true,
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
      curentDeptID: -1
    };
  },
  created() {
    this.getDeptsData();
    this.getUserData();
  },
  computed: {
    // 使用对象展开运算符将 getters 混入 computed 对象中
    ...mapGetters([
      "getProj"
      // ...
    ])
  },
  methods: {
    //性别显示转换
    formatSex: function(row, column) {
      return row.UserSex == 1 ? "男" : row.UserSex == 0 ? "女" : "未知";
    },
    handleEdit(row) {},
    handleDel(row) {},
    handleSizeChange() {},
    handleCurrentChange() {},
    //树节点切换事件
    selectDeptTreeChange(node) {
      console.log(node);
      this.curentDeptID = node.ID;
    },
    //过滤树节点
    filterNode(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    //切换编辑/查看模式
    changeEdit() {
      this.isEdit = !this.isEdit;
      this.isEditText = this.isEdit ? "编辑模式" : "查看模式";
    },
    //获取部门数据
    getDeptsData() {
      let that = this;
      that.listLoading = true;
      NProgress.start();
      console.log(this.getProj);
      axios
        .get(`/api/GetDeptByProjID?ProjID=${that.getProj}`)
        .then(res => {
          that.listLoading = false;
          NProgress.done();
          if (res.data.success) {
            console.log(res.data.data);
            that.treeDeptData = res.data.data;
          } else {
            console.log(res.data);
          }
        })
        .catch(err => {
          console.log(err);
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
        DeptID: that.curentDeptID,
        isEdit: that.isEdit
      };
      console.log("*****************************************" + params.DeptID);
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