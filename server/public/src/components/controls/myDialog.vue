<template >
    <el-dialog :title="title" v-model="visible" :close-on-click-modal="true">
        <el-form :model="editForm" label-width="80px" ref="editForm">
            <el-form-item :label="editForm[item].label" prop="name" v-for="item in Object.keys(editForm)" >
                <el-input v-if="editForm[item].type == 'String'" v-model="editForm[item].data"></el-input>
                <el-switch v-else-if="editForm[item].type == 'Bool'" v-model="editForm[item].data"></el-switch>
                <el-date-picker v-else-if="editForm[item].type == 'Data'" v-model="editForm[item].data" type="date" placeholder="选择日期"></el-date-picker>
            </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
            <el-button @click="closedialog">取 消</el-button>
            <el-button type="primary" :loading="editLoading">确定</el-button>
        </div>
    </el-dialog>
</template>
<script>
export default {
  props: ["title", "editForm", "visible"],
  data() {
    return {
      editLoading: false
    };
  },
  methods: {
    closedialog() {
      this.$emit("closedialog", this.visible);
    }
  }
};
</script>

<style scoped>
.el-form-item {
  margin-bottom: 1px;
}
</style>