<template>
	<el-tabs style="width:100%;">
		<el-tab-pane label="区域管理">
           
               <section>
                     <el-col :span="24" class="toolbar">
                    <el-form :inline="true"  class="demo-form-inline">
                        <el-form-item>
                            <el-input  placeholder="名称"></el-input>
                        </el-form-item>
                        <el-form-item>
                        <el-button >查询</el-button>
                        </el-form-item>
                        <el-form-item>
                        <!-- <el-button @click="openDialog" >新增角色</el-button> -->
                        </el-form-item>
                    </el-form>
                </el-col>
                </section>

                <template>
                    <myDialog   :title="DialogTitle" 
                                :editForm="DialogEditForm" 
                                :visible='DialogVisible' 
                                @closedialog="DialogVisible = false" 
                                @commitdialog="dialogresult"/>

                    <myDataTable :TableData="TableData" :TabelField="TabelField" @EditRow="EditRow" @DelRow="DelRow"></myDataTable>
                </template> 
		</el-tab-pane>
	</el-tabs>
</template>
<script>
import myDataTable from "../controls/myDataTable.vue";
import myDialog from "../controls/myDialog.vue";
export default {
  components: {
    myDataTable: myDataTable,
    myDialog: myDialog
  },
  data() {
    return {
      DialogTitle: "测试一下",
      DialogResult: {},
      DialogEditForm: {
        data0: { prop: "t", label: "头像", type: "String" },
        data1: { prop: "t1", label: "工号", type: "String" },
        data2: { prop: "t2", label: "名字", type: "String" },
        data3: { prop: "t3", label: "性别", type: "Bool" },
        data4: { prop: "t5", label: "角色", type: "String" }
      },
      TabelField: [
        { prop: "SysTabName", label: "名称", type: "String" },
        { prop: "SysTabFieldName", label: "字段", type: "String" },
        { prop: "SysFieldInfo", label: "自定义信息", type: "String" }
      ],
      TableData: [
        { SysTabName: "hhh", SysTabFieldName: "asdasd", SysFieldInfo: "asdas" },
        { SysTabName: "hhh", SysTabFieldName: "asdasd", SysFieldInfo: "asdas" },
        { SysTabName: "hhh", SysTabFieldName: "asdasd", SysFieldInfo: "asdas" },
        { SysTabName: "hhh", SysTabFieldName: "asdasd", SysFieldInfo: "asdas" },
        { SysTabName: "hhh", SysTabFieldName: "asdasd", SysFieldInfo: "asdas" }
      ],
      DialogVisible: false,
      editLoading: false
    };
  },
  methods: {
    openDialog() {
      this.DialogVisible = true;

      console.log("open dialog");
    },
    //编辑行
    EditRow(row, rowdata) {
      this.DialogVisible = true;
      this.DialogEditForm = rowdata;
    },
    //删除行
    DelRow(row) {},
    dialogresult(result) {
      this.DialogVisible = false;
      console.log(result);
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