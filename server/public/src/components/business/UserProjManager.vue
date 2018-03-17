<template>
  <div>
    <el-tabs style="width:100%;">
      <el-tab-pane label="用户项目管理">
        <section>
          <el-col :span="24" class="toolbar">
            <el-form :inline="true" :model="formInline" class="demo-form-inline">
              <el-form-item label="区域选择">
                <el-cascader :options="prov_city_options" change-on-select @change="handleItemChange"></el-cascader>
              </el-form-item>
              <el-form-item label="人员搜索">
                <el-input placeholder="工号/姓名"></el-input>
              </el-form-item>
              <el-form-item >
                <el-button round>查询</el-button>
              </el-form-item>
              <el-form-item >
                 <el-button @click="changeEdit" type="primary" round>{{isEditText}}</el-button>
              </el-form-item>
            </el-form>
          </el-col>
          <el-col :span="8">
            <el-card class="box-card" :body-style="{ padding: '0px' }">
              <div slot="header" class="clearfix">
                <span>项目列表</span>
              </div>
              <el-menu :default-active="default_active" @select="handleselect" >
                <el-menu-item v-for="i,index in proj_data" :index="(index+'')">{{i.label}}</el-menu-item>
              </el-menu>
            </el-card>
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
                  <el-switch v-model="scope.row.UserInProj" @change="changeUserProj(scope.row)"></el-switch>
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
  computed: {
    ...mapGetters(["getProj"])
  },
  data() {
    return {
      curCityID: -1,
      curProjID: -1,
      isEdit: false,
      isEditText: "查看模式",
      default_active: "0",
      prov_city_options: [],
      proj_data: [],
      tableUserData: [],
      formInline: {
        user: ""
      },
      listLoading: false,
      currentPage: 0,
      tableDataLength: 0,
      currentPageSize: 0,
      curentDeptID: -1
    };
  },
  created() {
    //this.getUserData();
    this.getProvData();
  },
  methods: {
    //设置用户项目
    changeUserProj(row) {
      let that = this;
      console.log(row._id);
      let query = {
        UserID: row._id,
        ProjID: that.curProjID,
        insertOrDel: row.UserInProj ? "insert" : "del"
      };
      axios
        .post("/api/InsertOrDelUserProj", query)
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
    //选择项目
    handleselect(index) {
      if (this.proj_data) {
        this.curProjID = this.proj_data[index].value.id;
        this.getUserData();
      }
    },
    //省市项目级联选择
    handleItemChange(val) {
      this.curCityID = -1;
      let cur_val = val[0];
      let that = this;
      let url = `/api/GetCityByProvID?item1=${cur_val.id}`;
      axios
        .get(url)
        .then(res => {
          if (res.data.success) {
            let cur = that.prov_city_options.filter(
              i => i.value.id == cur_val.id
            )[0].children;
            res.data.data.forEach(item => {
              if (cur.filter(i => i.label == item.label).length == 0)
                cur.push(item);
            });
            //完成后 查询项目列表
            let firstcity = val[1];
            if (!firstcity) return;
            this.curCityID = firstcity.id;
            if (firstcity) {
              axios
                .get(`/api/GetProjByCityID?item1=${firstcity.id}`)
                .then(res => {
                  if (res.data.success) {
                    that.proj_data = res.data.data;
                    that.default_active = "0";
                    if (that.proj_data[0]) {
                      that.curProjID = that.proj_data[0].value.id;
                    } else {
                      that.isEdit = false;
                      that.isEditText = "查看模式";
                      this.curProjID = -1;
                    }
                    that.getUserData();
                  }
                })
                .catch(err => {
                  console.log(err);
                });
            } else {
              that.isEdit = false;
              that.isEditText = "查看模式";
              that.getUserData();
            }
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    //性别显示转换
    formatSex: function(row, column) {
      return row.UserSex == 1 ? "男" : row.UserSex == 0 ? "女" : "未知";
    },
    handleEdit(row) {},
    handleDel(row) {},
    handleSizeChange() {},
    handleCurrentChange() {},
    //切换编辑/查看模式
    changeEdit() {
      if (this.isEdit == false && this.curCityID == -1) {
        this.$message({
          message: "请先选择项目",
          type: "error"
        });
      } else {
        this.isEdit = !this.isEdit;
        this.isEditText = this.isEdit ? "编辑模式" : "查看模式";
        this.getUserData();
      }
    },
    //获取人员资料
    getUserData() {
      let that = this;
      let url = `/api/GetUserByProjID`;
      let params = {
        limit: that.$data.currentPageSize,
        page: that.$data.currentPage,
        ProjID: that.curProjID,
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
    },
    //获取省份资料
    getProvData() {
      let that = this;
      axios
        .get("/api/GetProv")
        .then(res => {
          if (res.data.success) {
            that.prov_city_options = res.data.data;
          }
        })
        .catch(err => console.log(err));
    }
  }
};
</script>
<style scoped>

</style>