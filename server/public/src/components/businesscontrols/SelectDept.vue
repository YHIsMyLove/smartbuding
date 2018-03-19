<template>
    <el-dialog title="选择部门" v-model="dialogVisible" :close-on-click-modal="true">
        <div v-for="i,index in Depts" :key="index">
            <el-col :span="12">{{i.label}}</el-col>
            <el-col :span="12"><el-switch v-model="i.Selected"  active-color="#13ce66" inactive-color="#ff4949"></el-switch></el-col>
        </div>
        <div slot="footer" class="dialog-footer">
            <el-button @click="dialogVisible = false">取 消</el-button>
            <el-button type="primary" @click="editSubmit" >确定</el-button>
        </div>
    </el-dialog>
</template>
<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
import { mapGetters } from "vuex";
import moment from "moment";
export default {
  computed: {
    ...mapGetters(["getProj"])
  },
  props: ["Visible"],
  data() {
    return {
      dialogVisible: this.Visible,
      Depts: [{ DeptName: "", Selected: false }]
    };
  },
  created() {
    this.getDepts();
  },
  watch: {
    Visible(o, n) {
      this.dialogVisible = o;
    }
  },
  methods: {
    editSubmit() {
      this.dialogVisible = false;
      this.$emit("ShowDialog", this.dialogVisible);
    },
    //获取部门资料
    getDepts() {
      let that = this;
      let params = {
        item2: that.getProj
      };
      axios
        .get(`/api/GetDeptByProjID?ProjID=${that.getProj}`)
        .then(res => {
          if (res.data.success) {
            that.Depts = res.data.data;
            console.log(that.Depts);
          }
        })
        .catch(err => console.log(err));
    }
  }
};
</script>
<style scoped>

</style>

