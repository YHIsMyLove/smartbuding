<template>
	<el-tabs style="width:100%;">
		<el-tab-pane label="区域管理">
            <!-- <section>-->
                <el-col :span="24" class="toolbar">
                    <el-form :inline="true" :model="formInline" class="demo-form-inline">
                        <el-form-item>
                        <el-input v-model="formInline.user" placeholder="姓名"></el-input>
                        </el-form-item>
                        <el-form-item>
                        <el-button @click='getUserList'>查询</el-button>
                        <el-button @click="openDialog" type="text">新增角色</el-button>
                        </el-form-item>
                    </el-form>
                </el-col> 
                <template>
                    <myDialog   :title="DialogTitle" 
                            :editForm="DialogEditForm" 
                            :visible='DialogVisible' 
                            @closedialog="DialogVisible = false" 
                            @commitdialog="dialogresult"/>
                    <myDataTable :TableData="TableData" :TabelField="TabelField" @EditRow="EditRow" @DelRow="DelRow"></myDataTable>
                </template>
            <!-- </section> -->
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
        { prop: "t", label: "头像", type: "String" },
        { prop: "t1", label: "工号", type: "String" },
        { prop: "t2", label: "名字", type: "String" },
        { prop: "t3", label: "性别", type: "Bool" },
        { prop: "t5", label: "角色", type: "String" }
      ],
      TableData: [
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" },
        { t: "hhh", t1: "asdasd", t2: "asdas", t3: "sdf", t5: "sdg" }
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
