<template>
    <el-dialog title="选择部门" v-model="Visible" :close-on-click-modal="true">

          <el-form :inline="true" class="demo-form-inline">
            <el-form-item v-for="(i,index) in Depts" :key="index" :label="i.label">
              <el-switch v-model="i.Selected"  active-color="#13ce66" inactive-color="#ff4949"/>
            </el-form-item>
          </el-form>
     
        <div slot="footer" class="dialog-footer">
            <el-button @click="cancelEdit">取 消</el-button>
            <el-button type="primary" @click="submitEdit" >确定</el-button>
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
      Depts: []
    };
  },
  created() {
    this.getDepts();
  },
  methods: {
    cancelEdit() {
      this.$emit("cancelEdit");
      this.Depts.forEach(element => {
        element.Selected = false;
      });
    },
    submitEdit() {
      let result = this.Depts.filter(i => i.Selected).map(i => {
        return { DeptID: i.value.id, DeptName: i.label };
      });
      this.$emit("submitEdit", result);
    },
    //获取部门资料
    getDepts(callback) {
      let that = this;
      axios
        .get(`/api/GetDeptByProjID?ProjID=${that.getProj.ProjID}`)
        .then(res => {
          if (res.data.success) {
            that.Depts = res.data.data;
            callback();
          }
        })
        .catch(err => console.log(err));
    },
    //选中部门
    UpdateSelected(depts) {
      this.getDepts(() => {
        if (depts) {
          this.Depts.forEach(i => {
            if (depts.filter(item => item.DeptID == i.value.id).length > 0) {
              i.Selected = true;
            }
          });
        }
      });
    }
  }
};
</script>
<style scoped>
.deptGroup {
  margin-top: 2px;
}
</style>

