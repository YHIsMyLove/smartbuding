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
      <SystemManager_Fields :TableRow="CurTableRow" @cancelEdit="cancelEdit"/>
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
    },
    //新增表、字段
    newtabel() {
      this.activeName = "customFieldManager";
      this.CurTableRow = { SysTabFieldName: "", SysTabName: "" };
    },
    //刷新表
    RefTableData(data, count) {
      this.TableData = data;
      this.TableDataLength = count;
    },
    //编辑行
    EditRow(row) {
      this.activeName = "customFieldManager";
      this.CurTableRow = row;
    },
    //删除行
    DelRow(row) {
      console.log(row);
    },
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