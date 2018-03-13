<template >
    <el-dialog :title="title" v-model="myvisible" :close-on-click-modal="true">
        <el-form :model="editForm" label-width="150px" ref="editForm">
            <el-form-item :label="editForm[item].label" :prop="editForm[item].prop" v-for="item in Object.keys(editForm)" >
                <el-input v-if="editForm[item].type == 'String'" v-model="editForm[item].data"></el-input>
                <el-switch v-else-if="editForm[item].type == 'Bool'" v-model="editForm[item].data"></el-switch>
                <el-date-picker v-else-if="editForm[item].type == 'Data'" v-model="editForm[item].data" type="date" placeholder="选择日期"></el-date-picker>
            </el-form-item>       
        </el-form>
        <div slot="footer" class="dialog-footer">
            <el-button @click="closedialog">取 消</el-button>
            <el-button type="primary" :loading="editLoading" @click="commitdialog">确定</el-button>
        </div>
    </el-dialog>
</template>
<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
export default {
  props: ["title", "editForm", "visible", "ModelName"], //, "DialogData", "DialogField"
  data() {
    return {
      editLoading: false,
      myvisible: this.visible
    };
  },
  methods: {
    closedialog() {
      this.$emit("closedialog", this.visible);
    },
    commitdialog() {
      let that = this;
      this.editLoading = true;
      let result = "{";
      Object.keys(this.editForm).forEach(i => {
        result += `"${this.editForm[i].prop}":"${this.editForm[i].data}",`;
      });
      result = result.substr(0, result.length - 1) + "}";
      this.$emit("commitdialog", result);
      /********************************************************************* */
      let url = `/api/${that.ModelName}`;
      axios
        .post(url, JSON.parse(result))
        .then(res => {
          if (res.data.success) {
            that.$message({
              message: "提交成功",
              type: "success"
            });
          } else {
            that.$message({
              message: "提交失败",
              type: "error"
            });
          }
          that.editLoading = false;
        })
        .catch(err => {
          that.editLoading = false;
          console.log(err);
        });
      /********************************************************************* */
    }
  },
  watch: {
    visible(on, from) {
      this.myvisible = on;
    },
    myvisible(on, from) {
      if (!on) this.closedialog();
    }
  }
};
</script>

<style scoped>
.el-form-item {
  margin-bottom: 1px;
}
</style>