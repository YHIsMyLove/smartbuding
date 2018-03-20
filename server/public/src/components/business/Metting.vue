<template>
 <el-tabs v-model="activeTabName" style="width:100%;" @tab-click="changeActive">
      <el-tab-pane name="metting"  label="会议管理">
          <section>
            <el-col :span="24" class="toolbar">
                <el-form :inline="true"  class="demo-form-inline">
                    <el-form-item>
                        <el-input  placeholder="会议管理"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button @click="refresh">查询</el-button>
                        <el-button @click="newMetting" type="primary">新建会议</el-button>
                    </el-form-item>
                </el-form>
            </el-col>
            <el-col :span="24">

               <template>
                    <el-table border fit stripe :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">   
                        <el-table-column type="index" label="编号" width="85">
                        </el-table-column>
                        <el-table-column  prop="MettingName" label="会议名称" >
                        </el-table-column>
                        <el-table-column prop="Compere" label="主持人" >
                        </el-table-column>
                        <el-table-column prop="MettingCreatedAt" label="会议时间" >
                        </el-table-column>
                        <el-table-column label="操作" width="80">
                            <template scope="scope">
                                <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
                                <!-- <el-button type="text" size="small" @click="handleDel(scope.row)">删除</el-button> -->
                            </template>
                        </el-table-column>
                    </el-table>
                </template>

                <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
                    <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
                        :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength"
                        style="float:right">
                    </el-pagination>
                </el-col>

                <el-dialog title="新建会议" v-model="editFormVisible" :close-on-click-modal="true">
                    <el-form :model="editForm" label-width="80px" :rules="editFormRules" ref="editForm">
                      <el-form-item label="会议名称" prop="MettingName">
                            <el-input :disabled="editForm._id"  v-model="editForm.MettingName" auto-complete="off"></el-input>
                        </el-form-item>
                        <el-form-item label="主持人" prop="Compere">
                            <el-input :disabled="editForm._id"  v-model="editForm.Compere" auto-complete="off"></el-input>
                            <!-- <el-cascader 
                              v-model="curCompere" 
                              :show-all-levels="false" 
                              :options="dept_Users_options" 
                              style="width:100%"  
                              change-on-select 
                              @change="handleItemChange"/> -->
                        </el-form-item>
                        <el-form-item label="会议时间" prop="MettingCreatedAt">
                            <el-date-picker :disabled="true" style="width:100%" placeholder="选择日期时间" type="datetime" v-model="editForm.MettingCreatedAt"></el-date-picker>
                        </el-form-item>
                    </el-form>
                    <div slot="footer" class="dialog-footer">
                        <el-button @click="editFormVisible = false">取 消</el-button>
                        <el-button type="primary" @click="editSubmit" :loading="editLoading">提交</el-button>
                    </div>
                </el-dialog>

            </el-col>
          </section>
      </el-tab-pane>
      <el-tab-pane v-if="editForm.MettingName" name="content" label="内容管理">

        <section>
            <el-col :span="24" class="toolbar">
                <el-form :inline="true"  class="demo-form-inline">
                    <el-form-item>
                        <el-input  placeholder="内容管理"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <!-- <el-button @click="refresh">查询</el-button>-->
                        <el-button @click="newContent" type="primary">新建内容</el-button> 
                    </el-form-item>
                </el-form>
            </el-col>
            <div v-for="i,index in tabContent" :key="index">
              <el-card class="box-card">
              <div slot="header" class="clearfix">
                <span>
                    <el-input style="float: left; width:30%" v-model="i.label"></el-input>
                </span>
                <el-button  @click="delcontent(index)" style="float: right; padding: 3px 2px" type="text">删除</el-button>
                <el-button  @click="Send2Person" style="float: right; padding: 3px 2px" type="text">推送</el-button>
              </div>
                <el-input type="textarea" v-model="i.content"  placeholder="输入内容"></el-input>
              </el-card>
            </div>

            <SelectDept :Visible="Visible" @cancelEdit="cancelEdit" @submitEdit="submitEdit"/>

        </section>

      </el-tab-pane>
 </el-tabs>
</template>
<script>
import SelectDept from "../businesscontrols/SelectDept.vue";
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import { mapGetters } from "vuex";
import moment from "moment";
export default {
  components: {
    SelectDept: SelectDept
  },
  computed: {
    ...mapGetters(["getProj"])
  },
  data() {
    var checkCompere = (rule, value, callback) => {
      if (this.editForm.Compere) {
        return callback();
      } else {
        return callback(new Error("请选择主持人"));
      }
    };
    return {
      Visible: false,
      tabContent: [
        {
          label: "test",
          content: "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊"
        },
        {
          label: "test1",
          content:
            "水电费供电所覆盖和第三方好人发帖后居然同意让他与肉体与肉体与肉体与肉体与人体在徐州宣传部V型从正射"
        }
      ],
      activeTabName: "metting",
      editLoading: false,
      tableData: [],
      dept_Users_options: [],
      editForm: {
        MettingCreatedAt: moment().format("YYYY-MM-DD hh:mm:ss"),
        MettingName: "",
        Compere: ""
      },
      curCompere: [],
      editFormRules: {
        curCompere: [
          {
            type: "array",
            required: true,
            validator: checkCompere,
            trigger: "change"
          }
        ],
        MettingName: [
          { required: true, message: "请输入会议名称", trigger: "blur" }
        ]
      },
      editFormVisible: false,
      listLoading: false,
      currentPageSize: 0,
      handleSizeChange: 0,
      currentPage: 0,
      tableDataLength: 0
    };
  },
  created() {
    this.getMetting();
  },
  methods: {
    //取消编辑
    cancelEdit() {
      this.Visible = false;
    },
    //提交编辑
    submitEdit() {
      this.Visible = false;
    },
    Send2Person() {
      this.Visible = true;
    },
    delcontent(row) {
      console.log(row);
      this.$confirm("是否删除该内容", "系统提示", {
        type: "warning"
      }).then(() => {
        this.tabContent.splice(row, 1);
      });
    },
    //新建内容
    newContent() {
      this.tabContent.push({
        label: "会议内容",
        content: ""
      });
    },
    //更换激活tab
    changeActive(tab) {
      if (this.activeTabName == tab.name) {
        this.editForm.MettingName = "";
      }
    },
    newMetting() {
      this.editFormVisible = true;
      this.editForm = {
        MettingName: "",
        MettingCreatedAt: moment().format("YYYY-MM-DD hh:mm:ss")
      };
      this.getDeptData();
    },
    //提交
    editSubmit() {
      let that = this;
      that.$refs.editForm.validate(valid => {
        if (!valid) {
          this.editFormVisible = false;
          return;
        }
        that.$confirm("确认提交吗？", "提示", {}).then(() => {
          that.editLoading = true;
          NProgress.start();
          axios
            .post("/api/InsertOrUpdateMetting", that.editForm)
            .then(res => {
              if (res.data.success) {
                that.$message({
                  message: "提交成功",
                  type: "success"
                });
                that.getMetting();
              } else {
                that.$message({
                  message: "提交失败",
                  type: "error"
                });
              }
              that.editFormVisible = false;
              that.editLoading = false;
              NProgress.done();
            })
            .catch(err => {
              that.editFormVisible = false;
              that.editLoading = false;
              NProgress.done();
            });

          that.editFormVisible = false;
          that.editLoading = false;
          NProgress.done();
        });
      });
    },
    //编辑
    handleEdit(row) {
      this.editForm._id = row._id;
      this.editForm.MettingName = row.MettingName;
      this.editForm.MettingCreatedAt = row.MettingCreatedAt;
      this.editForm.Compere = row.Compere;

      //this.editFormVisible = true;
      //this.getDeptData();

      this.activeTabName = "content";
    },
    handleDel(row) {},
    handleCurrentChange() {},
    //选择主持人[部门/人员关联]
    handleItemChange(val) {
      // if (val.length > 1) {
      //   this.editForm.Compere = val[1];
      //   return;
      // }
      // this.editForm.Compere = "";
      // let that = this;
      // let cur_val = val[0];
      // let url = `/api/GetUserByDeptID?item1=${cur_val.id}`;
      // let params = {
      //   isEdit: "false",
      //   DeptID: cur_val.id,
      //   ProjID: that.getProj,
      //   limit: that.$data.currentPageSize,
      //   page: that.$data.currentPage
      // };
      // axios
      //   .get(url, { params: params })
      //   .then(res => {
      //     if (res.data.success) {
      //       let cur = that.dept_Users_options.filter(
      //         i => i.value.id == cur_val.id
      //       )[0].children;
      //       let tmpdata = res.data.data
      //         .map(i => {
      //           return {
      //             label: i.UserName,
      //             ID: i.UserID,
      //             value: i._id,
      //             type: "user"
      //           };
      //         })
      //         .forEach(item => {
      //           if (cur.filter(i => i.label == item.label).length == 0)
      //             cur.push(item);
      //         });
      //     } else {
      //     }
      //   })
      //   .catch(err => console.log(err));
    },
    //查询
    refresh() {
      this.getMetting();
    },
    getDeptData() {
      let that = this;
      let params = {
        item2: that.getProj
      };
      axios
        .get(`/api/GetDeptByProjID?ProjID=${that.getProj}`)
        .then(res => {
          if (res.data.success) {
            that.dept_Users_options = res.data.data;
            that.getMetting();
          }
        })
        .catch(err => console.log(err));
    },
    getMetting() {
      let that = this;
      let params = {
        item2: that.getProj
      };
      axios
        .get(`/api/GetMettings?ProjID=${that.getProj}`)
        .then(res => {
          if (res.data.success) {
            that.$data.tableData = res.data.data;
            that.$data.tableDataLength = res.data.meta.count;
          }
        })
        .catch(err => console.log(err));
    }
  }
};
</script>

<style scoped>
.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}

.box-card {
  width: 98%;
  padding-top: 5px;
  margin-top: 5px;
}
</style>
