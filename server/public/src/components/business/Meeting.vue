<template>
 <el-tabs v-model="activeTabName" style="width:100%;" @tab-click="changeActive">
      <el-tab-pane name="meeting"  label="会议管理">
          <section>
            <el-col :span="24" class="toolbar">
                <el-form :inline="true"  class="demo-form-inline">
                    <el-form-item>
                        <el-input  placeholder="会议管理"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button @click="refresh">查询</el-button>
                        <el-button @click="newMeeting" type="primary">新建会议</el-button>
                    </el-form-item>
                </el-form>
            </el-col>
            <el-col :span="24">

               <template>
                    <el-table border fit stripe :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">   
                        <el-table-column type="index" label="编号" width="85">
                        </el-table-column>
                        <el-table-column  prop="MeetingName" label="会议名称" >
                        </el-table-column>
                        <el-table-column prop="Compere" label="主持人" >
                        </el-table-column>
                        <el-table-column prop="MeetingCreatedAt" label="会议时间" >
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
                      <el-form-item label="会议名称" prop="MeetingName">
                            <el-input :disabled="editForm._id"  v-model="editForm.MeetingName" auto-complete="off"></el-input>
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
                        <el-form-item label="会议时间" prop="MeetingCreatedAt">
                            <el-date-picker :disabled="true" style="width:100%" placeholder="选择日期时间" type="datetime" v-model="editForm.MeetingCreatedAt"></el-date-picker>
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
      <el-tab-pane v-if="editForm.MeetingName" name="content" label="内容管理">

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
                <span>{{i._id=='-1'?'未上传':'已上传'}}</span>
              </div>
                <el-input type="textarea" v-model="i.content"  placeholder="输入内容"></el-input>
                <el-tag type="success" v-for="item in i.Depts">{{item.DeptName}}</el-tag>
                
                <el-button  style="float: right;" type="info" @click="submitContent(index)" >提交</el-button>
                <el-button  @click="Send2Person(index)" style="float: right;" type="warning">推送部门</el-button>
                <!-- <el-button  @click="delcontent(index)" style="float: right;" type="warning">删除</el-button> -->
              </el-card>
            </div>
            <SelectDept :Visible="Visible" @cancelEdit="cancelEdit" @submitEdit="submitEdit" ref="dept_select"/>
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
      tableData: [], //会议列表
      tabContent: [], //会议内容列表
      activeTabName: "meeting",
      editLoading: false,
      dept_Users_options: [],
      editForm: {
        MeetingCreatedAt: moment().format("YYYY-MM-DD hh:mm:ss"),
        MeetingName: "",
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
        MeetingName: [
          { required: true, message: "请输入会议名称", trigger: "blur" }
        ]
      },
      editFormVisible: false,
      listLoading: false,
      currentPageSize: 0,
      handleSizeChange: 0,
      currentPage: 0,
      tableDataLength: 0,
      curIndex4Meeting: 0
    };
  },
  created() {
    this.getMeeting();
  },
  methods: {
    //取消编辑
    cancelEdit() {
      this.Visible = false;
    },
    //提交编辑
    submitEdit(depts) {
      this.Visible = false;
      this.tabContent[this.curIndex4Meeting].Depts = depts;
    },
    //提交会议内容
    submitContent(index) {
      let that = this;
      if (!this.tabContent[index].Depts) {
        that.$message({
          message: "请选择推送的部门!",
          type: "error"
        });
        return;
      }

      //1. 验证部门,内容是否为空
      //2. 组装数据
      //3. post提交
      let MeetingMinutes = {
        _id: this.tabContent[index]._id,
        MeetingID: this.editForm._id,
        MeetingTitle: this.tabContent[index].label,
        Content: this.tabContent[index].content,
        Depts: this.tabContent[index].Depts.map(i => {
          return { DeptID: i.DeptID, DeptName: i.DeptName };
        })
      };
      axios
        .post("/api/InsertOrUpdateMeetingContent", MeetingMinutes)
        .then(res => {
          if (res.data.success) {
            //提交成功
            that.$message({
              message: "提交成功",
              type: "success"
            });
            that.getMeetingContent();
          } else {
            that.$message({
              message: "提交失败",
              type: "error"
            });
            //提交失败
          }
        })
        .catch(err => console.log(err));
    },
    //打开部门选择
    Send2Person(index) {
      this.Visible = true;
      this.curIndex4Meeting = index;
      //获取当前选择的部门
      let curselectdepts = this.tabContent[this.curIndex4Meeting].Depts;
      this.$refs.dept_select.UpdateSelected(curselectdepts);
    },
    delcontent(row) {
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
        content: "",
        _id: -1
      });
    },
    //更换激活tab
    changeActive(tab) {
      if (this.activeTabName == tab.name) {
        this.editForm.MeetingName = "";
      }
    },
    newMeeting() {
      this.editFormVisible = true;
      this.editForm = {
        MeetingName: "",
        MeetingCreatedAt: moment().format("YYYY-MM-DD hh:mm:ss")
      };
      this.getDeptData();
    },
    //提交会议
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
            .post("/api/InsertOrUpdateMeeting", that.editForm)
            .then(res => {
              if (res.data.success) {
                that.$message({
                  message: "提交成功",
                  type: "success"
                });
                that.getMeeting();
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
    //编辑会议内容
    handleEdit(row) {
      this.editForm._id = row._id;
      this.editForm.MeetingName = row.MeetingName;
      this.editForm.MeetingCreatedAt = row.MeetingCreatedAt;
      this.editForm.Compere = row.Compere;
      this.activeTabName = "content";

      this.getMeetingContent();
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
      this.getMeeting();
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
            that.getMeeting();
          }
        })
        .catch(err => console.log(err));
    },
    //获取会议
    getMeeting() {
      let that = this;
      let params = {
        item2: that.getProj
      };
      axios
        .get(`/api/GetMeetings?ProjID=${that.getProj}`)
        .then(res => {
          if (res.data.success) {
            that.$data.tableData = res.data.data;
            that.$data.tableDataLength = res.data.meta.count;
          }
        })
        .catch(err => console.log(err));
    },
    //获取会议内容
    getMeetingContent() {
      let that = this;
      axios
        .get(`/api/GetMeetingContentByMeetingID?MeetingID=${that.editForm._id}`)
        .then(res => {
          if (res.data.success) {
            //获取会议内容
            that.tabContent = res.data.data.map(i => {
              return {
                label: i.MeetingTitle,
                content: i.Content,
                Depts: i.Depts,
                _id: i._id
              };
            });
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
.el-tag {
  margin-left: 1px;
}
</style>
