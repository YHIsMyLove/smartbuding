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
                     :data="MechanicalData.tabledata"
                     v-loading="MechanicalData.loading">
                <el-table-column type="index" label="编号" width="85">
                </el-table-column>
                
								<el-table-column width='64' label="图片" >
                        <template slot-scope="scope">
                            <img :src="scope.row.mechanicalImg" style="width:100%;height:100%;">
                        </template>
                    </el-table-column>
								<el-table-column prop="mechanicalName" label="机器名" >
                    </el-table-column>
								<el-table-column prop="mechanicalState" label="机器状态" >
                    </el-table-column>
								<el-table-column prop="mechanicalUser" label="管理员" >
                    </el-table-column>
								<el-table-column prop="mechanicalUserPhone" label="管理员电话" >
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
            <el-dialog :title="MechanicalDialog.title" 
                       :close-on-click-modal="true"
                        v-model="MechanicalDialog.visible">
                <el-form :model="CurrentData.data"  
                         :rules="CurrentData.rules" 
                          label-width="80px" ref="editForm">
                        
												<el-form-item label="图片" prop="mechanicalImg">
                    		<UploadImage :width=64 v-model="CurrentData.data.mechanicalImg" />
                    </el-form-item><el-form-item label="机器名" prop="mechanicalName">
                            		<el-input  v-model="CurrentData.data.mechanicalName" auto-complete="off"></el-input>
                            </el-form-item><el-form-item label="机器状态" prop="mechanicalState">
                            		<el-input  v-model="CurrentData.data.mechanicalState" auto-complete="off"></el-input>
                            </el-form-item><el-form-item label="管理员" prop="mechanicalUser">
                            		<el-input  v-model="CurrentData.data.mechanicalUser" auto-complete="off"></el-input>
                            </el-form-item><el-form-item label="管理员电话" prop="mechanicalUserPhone">
                            		<el-input  v-model="CurrentData.data.mechanicalUserPhone" auto-complete="off"></el-input>
                            </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="MechanicalDialog.visible = false">取 消</el-button>
                    <el-button type="primary" @click="submit" :loading="MechanicalDialog.loading">提交</el-button>
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
      show:false,
      pageData: {},
      PageInfo: {
        title: "机械设备检测",
        currentPage:1,
        currentPageSize:10,
        tableDataLength:10
      },
      MechanicalData: {
        tabledata: [],
        loading: false
      },
      MechanicalDialog: {
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
      that.MechanicalData.loading = true;
      axios
        .get(`/api/ListMechanical`, { params: params })
        .then(res => {
          if (res.data.success) {
            that.$data.MechanicalData.tabledata = res.data.data;
            that.$data.PageInfo.tableDataLength = res.data.meta.count;
          }
        })
        .catch(err => {});
      that.MechanicalData.loading = false;
      NProgress.done();
    },
    handleCurrentChange(val) {
      this.$data.PageInfo.currentPage = val;
      this.updateData();
    },
    handleSizeChange(val) {
      this.$data.PageInfo.currentPageSize = val;
      this.updateData();
    },
    /***********************************/
    newdata() {
      this.CurrentData.data = {
        
				mechanicalImg : '',
				mechanicalName : '',
				mechanicalState : '正常',
				mechanicalUser : '',
				mechanicalUserPhone : '',
				
      };
      this.MechanicalDialog.visible = true;
    },
    handleEdit(row) {
      this.CurrentData.data = {
        _id : row._id,
        
				mechanicalImg : row.mechanicalImg,
				mechanicalName : row.mechanicalName,
				mechanicalState : row.mechanicalState,
				mechanicalUser : row.mechanicalUser,
				mechanicalUserPhone : row.mechanicalUserPhone,
				
      };
      this.MechanicalDialog.visible = true;
    },
    submit() {
      var that = this;
      that.$refs.editForm.validate(valid => {
        if (!valid) return;
        that.$confirm("确定提交吗?", "提示", {}).then(() => {
          that.MechanicalDialog.loading = true;
          axios
            .post(`/api/createOrUpdateMechanical`, that.CurrentData.data)
            .then(res => {
              if (res.data.success) {
                //success
                that.updateData();
              } else {
                //error
                that.$message({
                  message: "提交失败:" + res.data.msg,
                  type: "error"
                });
              }
              that.MechanicalDialog.loading = false;
              this.MechanicalDialog.visible = false;
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
            .get(`/api/DelMechanicalByID?id=${row._id}`)
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


