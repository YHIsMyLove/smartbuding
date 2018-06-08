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
                     :data="BigNewsData.tabledata"
                     v-loading="BigNewsData.loading">
                <el-table-column type="index" label="编号" width="85">
                </el-table-column>
                
								<el-table-column width='64' label="照片" >
                        <template slot-scope="scope">
                            <img :src="scope.row.BgiNewsImg" style="width:100%;height:100%;">
                        </template>
                    </el-table-column>
								<el-table-column prop="BgiNewsTitle" label="大事件标题" >
                    </el-table-column>
								<el-table-column prop="BgiNewsContent" label="大事件内容" >
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
            <el-dialog :title="BigNewsDialog.title" 
                       :close-on-click-modal="true"
                        v-model="BigNewsDialog.visible">
                <el-form :model="CurrentData.data"  
                         :rules="CurrentData.rules" 
                          label-width="80px" ref="editForm">
                        
												<el-form-item label="照片" prop="BgiNewsImg">
                    		<UploadImage :width=64 v-model="CurrentData.data.BgiNewsImg" />
                    </el-form-item><el-form-item label="大事件标题" prop="BgiNewsTitle">
                            		<el-input  v-model="CurrentData.data.BgiNewsTitle" auto-complete="off"></el-input>
                            </el-form-item><el-form-item label="大事件内容" prop="BgiNewsContent">
                            		<el-input  v-model="CurrentData.data.BgiNewsContent" auto-complete="off"></el-input>
                            </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="BigNewsDialog.visible = false">取 消</el-button>
                    <el-button type="primary" @click="submit" :loading="BigNewsDialog.loading">提交</el-button>
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
      show:false,
      pageData: {},
      PageInfo: {
        title: "大事件",
        currentPage:1,
        currentPageSize:10,
        tableDataLength:10
      },
      BigNewsData: {
        tabledata: [],
        loading: false
      },
      BigNewsDialog: {
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
      that.BigNewsData.loading = true;
      axios
        .get(`/api/ListBigNews`, { params: params })
        .then(res => {
          if (res.data.success) {
            that.$data.BigNewsData.tabledata = res.data.data;
            that.$data.PageInfo.tableDataLength = res.data.meta.count;
          }
        })
        .catch(err => {});
      that.BigNewsData.loading = false;
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
        
				BgiNewsImg : '',
				BgiNewsTitle : '',
				BgiNewsContent : '',
				ProjID : this.getProj.ProjID,
				
      };
      this.BigNewsDialog.visible = true;
    },
    handleEdit(row) {
      this.CurrentData.data = {
        _id : row._id,
        
				BgiNewsImg : row.BgiNewsImg,
				BgiNewsTitle : row.BgiNewsTitle,
				BgiNewsContent : row.BgiNewsContent,
				ProjID : this.getProj.ProjID,
				
      };
      this.BigNewsDialog.visible = true;
    },
    submit() {
      var that = this;
      that.$refs.editForm.validate(valid => {
        if (!valid) return;
        that.$confirm("确定提交吗?", "提示", {}).then(() => {
          that.BigNewsDialog.loading = true;
          axios
            .post(`/api/createOrUpdateBigNews`, that.CurrentData.data)
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
              that.BigNewsDialog.loading = false;
              this.BigNewsDialog.visible = false;
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
            .get(`/api/DelBigNewsByID?id=${row._id}`)
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


