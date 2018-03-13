<template>
	<el-tabs v-model="activeName" style="width:100%;">
		<el-tab-pane label="自定义表管理" name='customTableManager'>
      <section>
          <el-col :span="24" class="toolbar">
          <el-form :inline="true"  class="demo-form-inline">
              <el-form-item>
                  <el-input  placeholder="表名"></el-input>
              </el-form-item>
              <el-form-item>
              <el-button >查询</el-button>
              </el-form-item>
              <el-form-item>
              <el-button @click="newtabel">新增自定义表</el-button>
              </el-form-item>
          </el-form>
      </el-col>
      </section>
      <template>
          <myDataTable  :TableData="TableData" 
                        :TableDataLength="TableDataLength"
                        :TabelField="TabelField"
                        :ModelName = "ModelName" 
                        :SystemEditRow="SystemEditRow"
                        @EditRow="EditRow" 
                        @DelRow="DelRow"
                        @RefTableData="RefTableData"></myDataTable>
      </template> 
		</el-tab-pane>
    <el-tab-pane label="自定义字段管理" name="customFieldManager">
      <SystemManager_Fields :IsEdit="IsEdit" :TableRow="CurTableRow" @cancelEdit="cancelEdit" @newLine="newLine" @delLine="delLine" @commitEdit="commitEdit"/>
    </el-tab-pane>
	</el-tabs>
</template>
<script>
import myDataTable from "../controls/myDataTable.vue";
import myDialog from "../controls/myDialog.vue";
import SystemManager_Fields from "../business/SystemManager_Fields.vue";
export default {
  components: {
    myDataTable: myDataTable,
    myDialog: myDialog,
    SystemManager_Fields: SystemManager_Fields
  },
  data() {
    return {
      IsEdit: false,
      CurTableRow: {},
      SystemEditRow: false,
      activeName: "customTableManager",
      ModelName: "SysField",
      TabelField: [
        { prop: "SysTabFieldName", label: "表名", type: "String" },
        { prop: "SysTabName", label: "描述", type: "String" },
        { prop: "SysFieldInfo", label: "自定义信息", type: "String" }
      ],
      TableData: [],
      TableDataLength: 0,
      DialogVisible: false,
      editLoading: false
    };
  },
  methods: {
    //取消编辑
    cancelEdit() {
      this.activeName = "customTableManager";
      this.CurTableRow = {};
      this.IsEdit = false;
    },
    //新增表、字段
    newtabel() {
      this.activeName = "customFieldManager";
      this.CurTableRow = {
        SysTabFieldName: "",
        SysTabName: "",
        SysFieldInfo: [{ name: "", desc: "", type: "String", link: "空" }]
      };
      this.IsEdit = false;
    },
    //新增字段
    newLine(rowdata) {
      if (this.IsEdit) {
        this.$message({
          message: "暂未支持",
          type: "error"
        });
      } else {
        this.CurTableRow.SysFieldInfo = rowdata;
      }
    },
    //编辑行
    EditRow(row) {
      this.activeName = "customFieldManager";
      this.CurTableRow = row;
      this.IsEdit = true;
    },
    //删除字段
    delLine(index) {
      this.CurTableRow.SysFieldInfo.splice(index, 1);
    },
    //提交数据
    commitEdit(rowdata) {
      this.activeName = "customTableManager";
      if (!this.IsEdit) {
        console.log(rowdata);
        this.CurTableRow.SysFieldInfo = JSON.stringify(rowdata);
        this.TableData.push(this.CurTableRow);
        this.CurTableRow = [];
      }
    },
    //刷新表
    RefTableData(data, count) {
      //data
      this.TableData = data;
      this.TableDataLength = count;
    },
    //删除行
    DelRow(row) {},
    //create or update
    Commitdialog(result) {
      this.DialogVisible = false;
    }
  }
};
</script>

<style lang="scss" scoped>
.toolbar .el-form-item {
  margin-bottom: 10px;
}

.toolbar {
  background: #fff;
  padding-top: 10px;
}
</style>