<template>
    <div>
        <el-input
        placeholder="请选择项目"
        suffix-icon="el-icon-search" 
        class="searchProj"
        v-model="SelectProj"
        disabled> 
            <el-button @click="OpenDialog" slot="append" icon="el-icon-search"></el-button>
        </el-input>
        <el-dialog close="dialog" title="请选择项目" v-model="isOpenDialog">
            <div class="nav">
                <div class="center">
                    <el-col :span=8>
                        <el-select v-model="value" placeholder="请选择">
                            <el-option
                            v-for="item in options"
                            :key="item.value"
                            :label="item.label"
                            :value="item.value">
                            </el-option>
                        </el-select>
                    </el-col>
                    <el-col :span=8>
                        <el-select v-model="value" placeholder="请选择">
                            <el-option
                            v-for="item in options"
                            :key="item.value"
                            :label="item.label"
                            :value="item.value">
                            </el-option>
                        </el-select>
                    </el-col>
                    <el-col :span=8>
                        <!-- <el-input
                            placeholder="请选择项目"
                            suffix-icon="el-icon-search" 
                            class="searchProj"> 
                            <el-button slot="append" icon="el-icon-search"></el-button>
                        </el-input> -->
                    </el-col>
                </div>
            </div>
            <div class="ProjLst">
                <el-menu>
                    <!-- <div v-for="item in options" :index="item.value" :key="item.value">
                    </div> -->
                    <el-menu-item v-for="item in options" :index="item.value" :key="item.value">
                        {{item.label}}
                    </el-menu-item>
                </el-menu>
            </div>
        </el-dialog>   
    </div>
</template>

<script>
export default {
  data() {
    return {
      SelectProj: "湖北|黄石「黄石奥体中心建设项目」",
      EditProj: {},
      isOpenDialog: false,

      options: [
        {
          value: "选项1",
          label: "黄金糕"
        },
        {
          value: "选项2",
          label: "双皮奶"
        },
        {
          value: "选项3",
          label: "蚵仔煎"
        },
        {
          value: "选项4",
          label: "龙须面"
        },
        {
          value: "选项5",
          label: "北京烤鸭"
        }
      ],
      value: ""
    };
  },
  methods: {
    OpenDialog() {
      console.log("test");
      this.isOpenDialog = !this.isOpenDialog;
    }
  }
};
</script>


<style >
.searchProj {
  width: 98%;
  border-radius: 20px;
}
.el-input.is-disabled .el-input__inner {
  color: #409eff;
}
.dialog {
  width: 100%;
  height: 100%;
}
.nav {
  width: 100%;
  height: 48px;
}
.ProjLst {
  width: 100%;
  height: 100%;
}
.center {
  margin-left: 20px;
  width: 80%;
}
</style>


