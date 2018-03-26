<template>
    <el-tabs style="width:100%;">
        <el-tab-pane label="萤石设备管理">
            <section>
                <el-col :span="24" class="toolbar">
                    <el-form :inline="true" :model="formInline" class="demo-form-inline">
                        <el-form-item>
                            <el-input v-model="formInline.user" placeholder="设备名称"></el-input>
                        </el-form-item>
                        <el-form-item>
                            <el-button @click='getYSCameraList'>查询</el-button>
                            <!-- <el-button type="primary" round @click="newCustomTable">新增用户</el-button> -->
                        </el-form-item>
                    </el-form>
                </el-col>

                <template>
                    <el-table border fit stripe :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">   
                        <el-table-column type="index" label="编号" width="85">
                        </el-table-column>
                        <el-table-column prop="DevID" label="设备ID" >
                        </el-table-column>
                        <el-table-column prop="DevName" label="设备名称" >
                        </el-table-column>
                        <el-table-column prop="DevStatus" label="在线状态" :formatter="formatOnLine" >
                        </el-table-column>
                        <el-table-column prop="DevType" label="设备类型" >
                        </el-table-column>
                        <el-table-column prop="DevVersion" label="设备版本">
                        </el-table-column>
                        <el-table-column width="120" label="系统设备库">
                            <template scope="scope">
                                <el-switch v-model="scope.row.InSytemDevs"></el-switch>
                            </template>
                        </el-table-column>
                        <el-table-column label="操作" width="80">
                            <template scope="scope">
                                <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </template>

                <!--分页-->
                <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
                    <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
                        :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength"
                        style="float:right">
                    </el-pagination>
                </el-col>

                <el-dialog :title="editFormTtile" v-model="editFormVisible" :close-on-click-modal="true">
                    <el-form :model="editForm" label-width="100px" :rules="editFormRules" ref="editForm">
                        <el-form-item  label="设备ID" prop="DevID">
                            <el-input :disabled="true" v-model="editForm.DevID" auto-complete="off"></el-input>
                        </el-form-item>
                        <el-form-item label="设备名称" prop="DevName">
                            <el-input :disabled="true"  v-model="editForm.DevName" auto-complete="off"></el-input>
                        </el-form-item>
                        <el-form-item label="设备类型" prop="DevType">
                            <el-input :disabled="true" v-model="editForm.DevType" auto-complete="off"></el-input>
                        </el-form-item>
                        <el-form-item label="设备版本" prop="DevVersion">
                            <el-input :disabled="true" v-model="editForm.DevVersion" auto-complete="off"></el-input>
                        </el-form-item>
                        <el-form-item label="系统设备名称" prop="DevName">
                            <el-input v-model="editForm.DevSystemName" auto-complete="off"></el-input>
                        </el-form-item>
                        <el-form-item label="加入系统库" prop="InSytemDevs">
                            <el-switch ></el-switch>
                        </el-form-item>
                    </el-form>
                    <div slot="footer" class="dialog-footer">
                        <el-button @click="editFormVisible = false">取 消</el-button>
                        <el-button type="primary" @click="editSubmit" :loading="editLoading">{{btnEditText}}</el-button>
                    </div>
                </el-dialog>

            </section>
        </el-tab-pane>
    </el-tabs>
</template>

<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import moment from "moment";
import { mapGetters } from "vuex";
export default {
  computed: {
    // 使用对象展开运算符将 getters 混入 computed 对象中
    ...mapGetters([
      "getProj",
      "getYSToken"
      // ...
    ])
  },
  data() {
    return {
      formInline: {
        user: ""
      },
      //编辑界面显是否显示
      editFormVisible: false,
      //编辑界面标题
      editFormTtile: "编辑",
      //编辑界面数据
      editForm: {
        id: 0,
        DevID: 0,
        DevName: "",
        DevType: "",
        DevVersion: "",
        DevSystemName: ""
      },
      editLoading: false,
      btnEditText: "提 交",
      editFormRules: {
        UserPwd: [{ required: true, message: "请输入密码", trigger: "blur" }],
        UserName: [{ required: true, message: "请输入姓名", trigger: "blur" }],
        UserPhoneNum: [
          { required: true, message: "请输入手机号", trigger: "blur" }
        ]
      },
      tableData: [],
      tableDataLength: 0,
      listLoading: false,
      currentPage: 1,
      currentPageSize: 10
    };
  },
  created: function() {
    this.getYSCameraList();
    //根据区域查询部门,职位表...
  },
  methods: {
    //显示编辑界面
    handleEdit: function(row) {
      this.editFormVisible = true;
      this.editFormTtile = "编辑";
      this.editForm.id = row._id;
      this.editForm.DevID = row.DevID;
      this.editForm.DevName = row.DevName;
      this.editForm.DevSystemName = row.DevName;
      this.editForm.DevType = row.DevType;
      this.editForm.DevVersion = row.DevVersion;
    },
    //编辑
    editSubmit: function() {
      var _this = this;
      _this.$refs.editForm.validate(valid => {
        if (!valid) return;
        _this.$confirm("确认提交吗？", "提示", {}).then(() => {
          _this.editLoading = true;
          NProgress.start();
          _this.btnEditText = "提交中";

          let url = _this.editForm.id
            ? `/api/user/${_this.editForm.id}`
            : "/api/user";
          axios.post(url, _this.editForm).then(function(res) {
            if (res.data.success) {
              _this.$message({
                message: "提交成功",
                type: "success"
              });
              _this.editFormVisible = false;
              _this.getYSCameraList();
            } else {
              _this.$message({
                message: "提交失败",
                type: "error"
              });
            }
            _this.btnEditText = "提 交";
            _this.editLoading = false;
            NProgress.done();
          });
        });
      });
    },
    //在线状态显示转换
    formatOnLine: function(row, column) {
      return row.DevStatus == 1
        ? "在线"
        : row.DevStatus == 0 ? "不在线" : "未知";
    },
    //获取用户列表
    getYSCameraList: function() {
      var that = this;
      that.listLoading = true;
      NProgress.start();
      var params = {
        limit: that.$data.currentPageSize,
        page: that.$data.currentPage,
        token: that.getYSToken
      };
      //console.log(that.getYSToken)
      axios.get("/api/GetYSDevs", { params: params }).then(function(res) {
        that.$data.tableData = res.data.data;
        that.$data.tableDataLength = res.data.meta.count;
        that.listLoading = false;
        NProgress.done();
      });
    },
    handleSizeChange(val) {
      this.$data.currentPageSize = val;
      this.getYSCameraList();
    },
    handleCurrentChange(val) {
      this.$data.currentPage = val;
      this.getYSCameraList();
    }
  }
};
</script>

<style lang="scss" scoped>
.toolbar .el-form-item {
  margin-bottom: 10px;
}

.toolbar {
  background: #fff;
  padding-top: 10px;
}
</style>