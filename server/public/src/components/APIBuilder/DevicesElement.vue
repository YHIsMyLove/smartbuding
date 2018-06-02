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
                     :data="DevicesData.tabledata"
                     v-loading="DevicesData.loading">
                <el-table-column type="index" label="编号" width="85">
                </el-table-column>
                
								<el-table-column prop="DevID" label="设备ID" >
                </el-table-column>
								<el-table-column prop="DevName" label="设备名称" >
                </el-table-column>
								<el-table-column prop="DevDesc" label="设备描述" >
                </el-table-column>
								<el-table-column prop="DevIP" label="设备IP" >
                </el-table-column>
								<el-table-column prop="DevPort" label="设备端口" >
                </el-table-column>
								<el-table-column prop="DevState$" label="设备状态" >
                </el-table-column>
								<el-table-column prop="DevClass" label="设备类型" >
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
            <el-dialog :title="DevicesDialog.title" 
                       :close-on-click-modal="true"
                        v-model="DevicesDialog.visible">
                <el-form :model="CurrentData.data"  
                         :rules="CurrentData.rules" 
                          label-width="80px" ref="editForm">
                        
												<el-form-item label="设备ID" prop="DevID">
                        		<el-input  v-model="CurrentData.data.DevID" auto-complete="off"></el-input>
                        </el-form-item><el-form-item label="设备名称" prop="DevName">
                        		<el-input  v-model="CurrentData.data.DevName" auto-complete="off"></el-input>
                        </el-form-item><el-form-item label="设备描述" prop="DevDesc">
                        		<el-input  v-model="CurrentData.data.DevDesc" auto-complete="off"></el-input>
                        </el-form-item><el-form-item label="设备IP" prop="DevIP">
                        		<el-input  v-model="CurrentData.data.DevIP" auto-complete="off"></el-input>
                        </el-form-item><el-form-item label="设备端口" prop="DevPort">
                        		<el-input  v-model="CurrentData.data.DevPort" auto-complete="off"></el-input>
                        </el-form-item><el-form-item label="设备状态" prop="DevState$">
                        		<el-input  disabled  v-model="CurrentData.data.DevState$" auto-complete="off"></el-input>
                        </el-form-item><el-form-item label="设备类型" prop="DevClass">
                        		<el-input  v-model="CurrentData.data.DevClass" auto-complete="off"></el-input>
                        </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="DevicesDialog.visible = false">取 消</el-button>
                    <el-button type="primary" @click="submit" :loading="DevicesDialog.loading">提交</el-button>
                </div>
            </el-dialog>
        </el-tab-pane>
    </el-tabs>
</template>
<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import { mapGetters, mapActions } from 'vuex';
export default {
  computed: {...mapGetters(['getProj'])},
  data() {
    return {
      pageData: {},
      PageInfo: {
        title: "设备管理",
        currentPage:1,
        currentPageSize:10,
        tableDataLength:10
      },
      DevicesData: {
        tabledata: [],
        loading: false
      },
      DevicesDialog: {
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
      that.DevicesData.loading = true;
      axios
        .get(`/api/ListDevices`, { params: params })
        .then(res => {
          if (res.data.success) {
            that.$data.DevicesData.tabledata = res.data.data;
            that.$data.PageInfo.tableDataLength = res.data.meta.count;
          }
        })
        .catch(err => {});
      that.DevicesData.loading = false;
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
        
				DevID : '',
				DevName : '',
				DevDesc : '',
				DevIP : '192.168.1.1',
				DevPort : '8888',
				DevState$ : '在线',
				DevClass : '',
				ProjID : this.getProj.ProjID,
				
      };
      this.DevicesDialog.visible = true;
    },
    handleEdit(row) {
      this.CurrentData.data = {
        _id : row._id,
        
				DevID : row.DevID,
				DevName : row.DevName,
				DevDesc : row.DevDesc,
				DevIP : row.DevIP,
				DevPort : row.DevPort,
				DevState$ : row.DevState$,
				DevClass : row.DevClass,
				ProjID : this.getProj.ProjID,
				
      };
      this.DevicesDialog.visible = true;
    },
    submit() {
      var that = this;
      that.$refs.editForm.validate(valid => {
        if (!valid) return;
        that.$confirm("确定提交吗?", "提示", {}).then(() => {
          that.DevicesDialog.loading = true;
          axios
            .post(`/api/createOrUpdateDevices`, that.CurrentData.data)
            .then(res => {
              if (res.data.success) {
                //success
                that.updateData();
              } else {
                //error
              }
              that.DevicesDialog.loading = false;
              this.DevicesDialog.visible = false;
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
            .get(`/api/DelDevicesByID?id=${row._id}`)
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


