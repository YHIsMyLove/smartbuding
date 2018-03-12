<template>
    <el-col :span="24" class="toolbar">
        <el-form label-width="80px">
            <el-form-item label="表中文名">
                <el-input v-model="TableRow.SysTabName"></el-input>
            </el-form-item>
            <el-form-item label="表名">
                <el-input v-model="TableRow.SysTabFieldName"></el-input>
            </el-form-item>
            <el-form-item v-for="item in elFormData" :label="item.name">
                <el-input></el-input>
            </el-form-item>
        </el-form>
        <el-form class="demo-form-inline">
            <el-button type="primary">新增行</el-button>
            <el-button @click="commitEdit" type="primary">提交</el-button>
            <el-button @click="cancelEdit">取消</el-button>
        </el-form>

    </el-col>
</template>
<script>
export default {
  props: ["TableRow", "IsEdit"],
  data() {
    return {};
  },
  methods: {
    //取消编辑
    cancelEdit() {
      this.$emit("cancelEdit");
    },
    //提交修改
    commitEdit() {
      console.log("提交修改");
    }
  },
  computed: {
    elFormData() {
      if (!this.TableRow.SysFieldInfo) {
        return {};
      } else {
        let result = JSON.parse(this.TableRow.SysFieldInfo);
        let datas = [];
        Object.keys(result).forEach(i => {
          let item = {
            name: i,
            type: result[i].type,
            link: result[i].link
          };
          console.log(item.name);
          datas.push(item);
        });
        return datas;
      }
    }
  }
};
</script>
<style scoped>

</style>
