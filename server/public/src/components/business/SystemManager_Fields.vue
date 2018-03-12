<template>
    <el-col :span="24" class="toolbar">
        <el-form label-width="80px">
            <el-form-item label="表名">
                <el-input v-model="TableRow.SysTabFieldName"></el-input>
            </el-form-item>
            <el-form-item label="描述">
                <el-input v-model="TableRow.SysTabName"></el-input>
            </el-form-item>
            <el-form-item v-for="item,index in elFormData" :key="index" label="字段信息">
                <el-col :span="7">
                    <el-input placeholder="字段名称" v-model="item.name"></el-input>
                </el-col>
                <el-col :span="7">
                    <el-input placeholder="字段描述" v-model="item.name"></el-input>
                </el-col>
                <el-col :span="4">
                    <el-select v-model="item.link" placeholder="关联字段">
                        <el-option v-for="item in options_Fields" :key="item.value" :label="item.label" :value="item.value">
                        </el-option>
                    </el-select>
                </el-col>
                 <el-col :span="4">
                    <el-select v-model="item.type" placeholder="字段类型">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
                        </el-option>
                    </el-select>
                </el-col> 
                <el-col :span="2">
                    <el-button type="danger" @click="onDelLine(index)">删除</el-button>
                </el-col>
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
    return {
      options_Fields: [{ key: "none", value: "空" }],
      options: [
        { value: "String", label: "字符类型" },
        { value: "Number", label: "数值类型" },
        { value: "Date", label: "日期类型" }
      ]
    };
  },
  methods: {
    //取消编辑
    cancelEdit() {
      this.$emit("cancelEdit");
    },
    //提交修改
    commitEdit() {
      console.log("提交修改");
    },
    //删除字段
    onDelLine(index) {
      console.log("删除字段" + index);
    }
  },
  computed: {
    elFormData() {
      if (!this.TableRow.SysFieldInfo) {
        let result = [{ name: "", type: "String", link: "空" }];
        console.log(result);
        return result;
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
