<template>
    <el-tabs class="main">
        <el-tab-pane :label="PageInfo.title">
            <el-col :span=24>
                <el-form :inline="true" :model="pageData" class="demo-form-inline">
                    <el-form-item>
                        <el-input v-model="pageData.user" placeholder="姓名"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button @click='updateData'>查询</el-button>
                        <el-button type="primary" round @click="newdata">新增</el-button>
                    </el-form-item>
                </el-form>
            </el-col>
            <el-table border fit stripe 
                     :data="MenuAuthData.tabledata"
                     v-loading="MenuAuthData.loading">
                <el-table-column type="index" label="编号" width="85">
                </el-table-column>
                
								<el-table-column prop="MenuID" label="路由ID" >
                    </el-table-column>
								<el-table-column prop="RoleID" label="角色ID" >
                    </el-table-column>
								
                <el-table-column label="操作" width="150">
                    <template scope="scope">
                        <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
                        <el-button type="text" size="small" @click="handleDel(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
                <el-pagination @size-change="handleSizeChange" 
                               @current-change="handleCurrentChange" 
                               :current-page="PageInfo.currentPage" 
                               :page-size="PageInfo.currentPageSize" 
                               :total="PageInfo.tableDataLength"
                               :page-sizes="[10, 20, 30, 40]"
                               layout="total, sizes, prev, pager, next, jumper" 
                               style="float:right">
                </el-pagination>
            </el-col>
            <el-dialog :title="MenuAuthDialog.title" 
                       :close-on-click-modal="true"
                        v-model="MenuAuthDialog.visible">
                <el-form :model="CurrentData.data"  
                         :rules="CurrentData.rules" 
                          label-width="80px" ref="editForm">
                        
												<el-form-item label="路由ID" prop="MenuID">
                            		<el-input  v-model="CurrentData.data.MenuID" auto-complete="off"></el-input>
                            </el-form-item><el-form-item label="角色ID" prop="RoleID">
                            		<el-input  v-model="CurrentData.data.RoleID" auto-complete="off"></el-input>
                            </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="MenuAuthDialog.visible = false">取 消</el-button>
                    <el-button type="primary" @click="submit" :loading="MenuAuthDialog.loading">提交</el-button>
                </div>
            </el-dialog>
        </el-tab-pane>
    </el-tabs>
</template>
<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";

export default {
  
  data() {
    return {
      pageData: {},
      PageInfo: {
        title: "路由权限表",
        currentPage:1,
        currentPageSize:10,
        tableDataLength:10
      },
      MenuAuthData: {
        tabledata: [],
        loading: false
      },
      MenuAuthDialog: {
        title: "",
        visible: false,
        loading: false
      },
      CurrentData: {
        rules: {},
        data: {}
      }
    };
  },
  created() {
    this.updateData();
  },
  methods: {
    /***********************************/
    updateData() {
      let that = this;
      var params = {
        limit: that.$data.PageInfo.currentPageSize,
        page: that.$data.PageInfo.currentPage
      };
      NProgress.start();
      that.MenuAuthData.loading = true;
      axios
        .get(`/api/ListMenuAuth`, { params: params })
        .then(res => {
          if (res.data.success) {
            that.$data.MenuAuthData.tabledata = res.data.data;
            that.$data.PageInfo.tableDataLength = res.data.meta.count;
          }
        })
        .catch(err => {});
      that.MenuAuthData.loading = false;
      NProgress.done();
    },
    handleCurrentChange(val) {
      this.$data.currentPage = val;
      this.updateData();
    },
    handleSizeChange(val) {
      this.$data.currentPageSize = val;
      this.updateData();
    },
    /***********************************/
    newdata() {
      this.CurrentData.data = {
        
				MenuID : '',
				RoleID : '',
				
      };
      this.MenuAuthDialog.visible = true;
    },
    handleEdit(row) {
      this.CurrentData.data = {
        _id : row._id,
        
				MenuID : row.MenuID,
				RoleID : row.RoleID,
				
      };
      this.MenuAuthDialog.visible = true;
    },
    submit() {
      var that = this;
      that.$refs.editForm.validate(valid => {
        if (!valid) return;
        that.$confirm("确定提交吗?", "提示", {}).then(() => {
          that.MenuAuthDialog.loading = true;
          axios
            .post(`/api/createOrUpdateMenuAuth`, that.CurrentData.data)
            .then(res => {
              if (res.data.success) {
                //success
                that.updateData();
              } else {
                //error
              }
              that.MenuAuthDialog.loading = false;
              this.MenuAuthDialog.visible = false;
            })
            .catch(err => {});
        });
      });
    },
    handleDel(row) {
      var that = this;
      that
        .$confirm("确认删除该记录吗?", "提示", {
          type: "warning"
        })
        .then(() => {
          axios
            .get(`/api/DelMenuAuthByID?id=${row._id}`)
            .then(res => {
              if (res.data.success) {
                //success
                that.updateData();
              } else {
                //error
              }
            })
            .catch(err => {console.log(err)});
        })
        .catch(err => {});
    }
  }
};
</script>

<style scoped>
.main {
  width: 100%;
}
</style>


