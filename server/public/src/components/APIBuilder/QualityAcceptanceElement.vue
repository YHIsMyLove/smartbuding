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
                     :data="QualityAcceptanceData.tabledata"
                     v-loading="QualityAcceptanceData.loading">
                <el-table-column type="index" label="编号" width="85">
                </el-table-column>
                
								<el-table-column width='64' label="图片" >
                        <template slot-scope="scope">
                            <img :src="scope.row.QAImg" style="width:100%;height:100%;">
                        </template>
                    </el-table-column>
								<el-table-column prop="QARecipient_Name" label="接收人" >
                    </el-table-column>
								<el-table-column prop="QATitle" label="标题" >
                    </el-table-column>
								<el-table-column prop="QAContent" label="内容" >
                    </el-table-column>
								<el-table-column prop="QATime" label="时间" >
                    </el-table-column>
								<el-table-column prop="QAState" label="状态" >
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
            <el-dialog :title="QualityAcceptanceDialog.title" 
                       :close-on-click-modal="true"
                        v-model="QualityAcceptanceDialog.visible">
                <el-form :model="CurrentData.data"  
                         :rules="CurrentData.rules" 
                          label-width="80px" ref="editForm">
                        
												<el-form-item label="图片" prop="QAImg">
                    		<UploadImage :width=64 v-model="CurrentData.data.QAImg" />
                    </el-form-item><el-form-item label="接收人" prop="QARecipient">
                        <SelectUser @SelectUser="SelectUser"/>                
                    </el-form-item><el-form-item label="标题" prop="QATitle">
                            		<el-input  v-model="CurrentData.data.QATitle" auto-complete="off"></el-input>
                            </el-form-item><el-form-item label="内容" prop="QAContent">
                            		<el-input  v-model="CurrentData.data.QAContent" auto-complete="off"></el-input>
                            </el-form-item><el-form-item label="时间" prop="QATime">
                            		<el-date-picker   style="width:100%" placeholder="选择日期时间" 
                            		type="datetime" v-model="CurrentData.data.QATime"></el-date-picker>
                            </el-form-item><el-form-item label="状态" prop="QAState">
                            		<el-input  v-model="CurrentData.data.QAState" auto-complete="off"></el-input>
                            </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="QualityAcceptanceDialog.visible = false">取 消</el-button>
                    <el-button type="primary" @click="submit" :loading="QualityAcceptanceDialog.loading">提交</el-button>
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
        title: "质量验收",
        currentPage:1,
        currentPageSize:10,
        tableDataLength:10
      },
      QualityAcceptanceData: {
        tabledata: [],
        loading: false
      },
      QualityAcceptanceDialog: {
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
    SelectUser(val){                        
                        this.CurrentData.data.QARecipient=val._id
                        this.CurrentData.data.QARecipient_Name=val.UserName
                        console.log(val)
                    },
    /***********************************/
    updateData() {
      let that = this;
      var params = {
        limit: that.$data.PageInfo.currentPageSize,
        page: that.$data.PageInfo.currentPage
      };
      NProgress.start();
      that.QualityAcceptanceData.loading = true;
      axios
        .get(`/api/ListQualityAcceptance`, { params: params })
        .then(res => {
          if (res.data.success) {
            that.$data.QualityAcceptanceData.tabledata = res.data.data;
            that.$data.PageInfo.tableDataLength = res.data.meta.count;
          }
        })
        .catch(err => {});
      that.QualityAcceptanceData.loading = false;
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
        
				QAImg : '',
				QARecipient_Name : '',
				QARecipient : '',
				QATitle : '',
				QAContent : '',
				QATime : '',
				QAState : '0',
				
      };
      this.QualityAcceptanceDialog.visible = true;
    },
    handleEdit(row) {
      this.CurrentData.data = {
        _id : row._id,
        
				QAImg : row.QAImg,
				QARecipient_Name : row.QARecipient_Name,
				QARecipient : row.QARecipient,
				QATitle : row.QATitle,
				QAContent : row.QAContent,
				QATime : row.QATime,
				QAState : row.QAState,
				
      };
      this.QualityAcceptanceDialog.visible = true;
    },
    submit() {
      var that = this;
      that.$refs.editForm.validate(valid => {
        if (!valid) return;
        that.$confirm("确定提交吗?", "提示", {}).then(() => {
          that.QualityAcceptanceDialog.loading = true;
          axios
            .post(`/api/createOrUpdateQualityAcceptance`, that.CurrentData.data)
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
              that.QualityAcceptanceDialog.loading = false;
              this.QualityAcceptanceDialog.visible = false;
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
            .get(`/api/DelQualityAcceptanceByID?id=${row._id}`)
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


