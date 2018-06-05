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

            <el-col :span="8">
              <el-card>
                <div slot="header" class="clearfix">
                  <span>选择路由</span>
                </div>
                <SelectMenuLinkMenu @GetMenuLink="GetMenuLink"/>
              </el-card>
            </el-col>
            
            <el-col :span="16">
                <SelectRole RefashBy="MenuAuth" 
                           :Refash="RefashRole" 
                           :ShowTitle="RoleTitle" 
                           :ShowSwitch="true"/>
            </el-col>

        </el-tab-pane>
    </el-tabs>
</template>
<script>
import SelectMenuLinkMenu from "../businesscontrols/SelectMenuLinkMenu.vue";
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import { mapGetters, mapActions } from "vuex";
export default {
  components: { SelectMenuLinkMenu },
  computed: { ...mapGetters(["getProj"]) },
  data() {
    return {
      RefashRole: false,
      RoleTitle: "选择角色",
      show: false,
      pageData: {},
      PageInfo: {
        title: "路由权限表",
        currentPage: 1,
        currentPageSize: 10,
        tableDataLength: 10
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
    //this.updateData();
  },
  methods: {
    GetMenuLink(menu) {
      this.RefashRole = menu._id;
    },
    GetValue(val) {
      this.CurrentData.data.MenuID = val;
    },
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
      // this.$data.currentPage = val;
      // this.updateData();
    },
    handleSizeChange(val) {
      // this.$data.currentPageSize = val;
      // this.updateData();
    },
    /***********************************/
    newdata() {
      this.CurrentData.data = {
        MenuID: "",
        RoleID: "",
        ProjID: this.getProj.ProjID
      };
      this.MenuAuthDialog.visible = true;
    },
    handleEdit(row) {
      this.CurrentData.data = {
        _id: row._id,

        MenuID: row.MenuID,
        RoleID: row.RoleID,
        ProjID: this.getProj.ProjID
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
                that.$message({
                  message: "提交失败:" + res.data.msg,
                  type: "error"
                });
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
            .catch(err => {
              console.log(err);
            });
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


