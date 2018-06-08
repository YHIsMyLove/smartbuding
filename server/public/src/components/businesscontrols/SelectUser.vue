<template>
    <div class="main">
        <el-cascader :options="depts" 
                     @active-item-change="handleItemChange"
                     @change = "SelectUser"
                      placeholder="请选择用户"
                      v-model="CurrentValue">
        </el-cascader>
    </div>
</template>

<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
import { mapGetters, mapActions } from "vuex";
export default {
  props: ["value"],
  computed: {
    ...mapGetters(["getProj"]),
    CurrentProj() {
      return this.$store.state.Proj;
    }
  },
  data() {
    return {
      CurrentValue: [],
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
      if (val.length == 2) {
        let cuser = this.users.filter(i => i._id == val[1]).map(i => {
          return {
            UserName: i.UserName,
            _id: i._id
          };
        })[0];
        this.$emit("input", cuser);
        this.$emit("SelectUser", cuser);
      }
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
            that.users = res.data.data;
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


