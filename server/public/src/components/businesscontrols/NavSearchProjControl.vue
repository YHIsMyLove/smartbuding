<template>
    <div>
        <el-input placeholder="请选择项目" suffix-icon="el-icon-search" class="searchProj" v-model="SelectProj" disabled>
            <el-button @click="OpenDialog" slot="append" icon="el-icon-search"></el-button>
        </el-input>
        <el-button class="LogoutStyle" @click="Logout" type="danger" icon="el-icon-delete"></el-button>
        <el-dialog close="dialog" title="请选择项目" v-model="isOpenDialog">
            <div class="nav">
                <div class="center">
                    <el-col :span=8>
                        <SelectProv/>
                    </el-col>
                    <el-col :span=8>
                        <SelectCity/>
                    </el-col>
                    <el-col :span=8>
                    </el-col>
                </div>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import SelectCity from "./SelectCity.vue";
import SelectProv from "./SelectProv.vue";
export default {
  components: {
    SelectCity,
    SelectProv
  },
  data() {
    return {
      SelectProj: "湖北|黄石「黄石奥体中心建设项目」",
      EditProj: {},
      isOpenDialog: false
    };
  },
  methods: {
    Logout() {
      var _this = this;
      this.$confirm("确认退出吗?", "提示", {
        type: "warning"
      })
        .then(() => {
          _this.$router.replace("/login");
        })
        .catch(() => {});
    },
    OpenDialog() {
      this.isOpenDialog = !this.isOpenDialog;
    }
  }
};
</script>

<style>
.searchProj {
  width: calc(100% - 66px);
}
.LogoutStyle {
  border-radius: 50%;
  float: right;
  margin-right: 30px;
}

.el-input__inner {
  /* border-radius: 25px; */
}
.el-input-group__append {
  border-bottom-right-radius: 25px;
  border-top-right-radius: 25px;
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

el-menu {
  line-height: normal;
}
</style>
