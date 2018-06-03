<template>
    <div class="main">
        <el-cascader :options="depts" 
                     @active-item-change="handleItemChange"
                     @change = "SelectUser"
                      placeholder="请选择用户">
        </el-cascader>
    </div>
</template>

<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
import { mapGetters, mapActions } from "vuex";
export default {
  computed: {
    ...mapGetters(["getProj"]),
    CurrentProj() {
      return this.$store.state.Proj;
    }
  },
  data() {
    return {
      depts: [],
      users: [],
      CurrentDept: {},
      props: {
        value: "label",
        children: "children"
      }
    };
  },
  created() {
    this.getDepts();
  },
  methods: {
    SelectUser(val) {
      if (val.length == 2) this.$emit("SelectedUser", val[1]);
    },
    handleItemChange(val) {
      this.CurrentDept = this.depts.filter(i => i._id == val[0].id)[0];
      this.getUsers();
    },
    getUsers() {
      let that = this;
      NProgress.start();
      axios
        .get(`/api/GetUsersByDeptID?DeptID=${that.CurrentDept._id}`)
        .then(res => {
          if (res.data.success) {
            var data = res.data.data.map(i => {
              return {
                label: i.UserName,
                value: i._id
              };
            });
            that.CurrentDept.children = data;
          } else {
            //error
          }
          NProgress.done();
        })
        .catch(err => {});
    },
    getDepts() {
      let that = this;
      NProgress.start();
      axios
        .get(`/api/GetDeptByProjID?ProjID=${that.getProj.ProjID}`)
        .then(res => {
          if (res.data.success) {
            this.depts = res.data.data;
          } else {
            //error
          }
          NProgress.done();
        })
        .catch(err => {});
    }
  }
};
</script>
<style scoped>
</style>


