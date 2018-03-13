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
                    <el-input placeholder="字段描述" v-model="item.desc"></el-input>
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
            <el-button @click="newLine" type="primary">新增行</el-button>
            <el-button @click="commitEdit" type="primary">提交</el-button>
            <el-button @click="cancelEdit">取消</el-button>
        </el-form>
    </el-col>
</template>
<script>
import util from "../../common/util";
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
    //新增行
    newLine() {
      if (this.elFormData.length >= 10) {
        this.$message({
          message: "最多允许建立10行数据",
          type: "error"
        });
      } else {
        let rowdata = this.elFormData;
        let result = { name: "", desc: "", type: "String", link: "空" };
        rowdata.push(result);
        console.log(rowdata);
        this.$emit("newLine", rowdata);
      }
    },
    //提交修改
    commitEdit() {
      this.$confirm("确定提交吗?", "系统提示", {})
        .then(() => {
          let rowdata = this.elFormData;
          this.$emit("commitEdit", rowdata);
        })
        .catch(e => {});
    },
    //删除字段
    onDelLine(index) {
      //todo 判断总行数只有1条时不允许删除
      if (this.elFormData.length == 1) {
        this.$message({
          message: "最少需要保留一行数据不允许删除",
          type: "error"
        });
      } else {
        this.$confirm("确定删除吗?", "系统提示", {})
          .then(() => {
            this.$emit("delLine", index);
          })
          .catch(e => {});
      }
    }
  },
  computed: {
    elFormData() {
      if (!this.TableRow.SysFieldInfo) return;
      if (!this.IsEdit) {
        let result = this.TableRow.SysFieldInfo;
        let datas = [];
        Object.keys(result).forEach(i => {
          let item = {
            name: result[i].name,
            desc: result[i].desc,
            type: result[i].type,
            link: result[i].link
          };
          datas.push(item);
        });
        return datas;
      } else {
        let allfields = JSON.parse(this.TableRow.SysFieldInfo);
        let datas = [];
        allfields.forEach(result => {
          datas.push(result);
        });
        return datas;
      }
    }
  }
};
</script>
<style scoped>

</style>
