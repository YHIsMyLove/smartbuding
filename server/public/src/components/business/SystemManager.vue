<template>
	<el-tabs style="width:100%;">
		<el-tab-pane label="区域管理">
            <el-button @click="openDialog" type="text">新增角色</el-button>
            <myDialog :title="DialogTitle" :editForm="DialogEditForm" :visible='DialogVisible' v-on:closedialog="DialogVisible=false"></myDialog>
            <myDataTable :TableData="TableData" :TabelField="TabelField"></myDataTable>
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
      test: "fuck",
      DialogTitle: "测试一下",
      DialogEditForm: {
        test1: {
          label: "测试1",
          type: "String",
          name: "test1",
          data: "1233456"
        },
        test2: { label: "测试2", type: "Bool", name: "test2", data: true },
        test3: {
          label: "测试3",
          type: "Data",
          name: "test3",
          data: "1992-08-28"
        }
      },
      TabelField: [
        { prop: "t", label: "头像" },
        { prop: "t1", label: "工号" },
        { prop: "t2", label: "名字" },
        { prop: "t3", label: "性别" },
        { prop: "t5", label: "角色" }
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
    }
  }
};
</script>
