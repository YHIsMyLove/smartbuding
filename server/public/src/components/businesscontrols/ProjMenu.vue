<template>
    <el-card class="main">
        <div v-if="IsShowTitle"  slot="header" class="clearfix">
          <span >项目列表</span>
        </div>
        <el-menu @select="SelectProjChange">
            <el-menu-item v-for="item in ProjData" :index="item._id" :key="item._id">
              {{item.label}}
            </el-menu-item>
        </el-menu>
    </el-card>
</template>
<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
import { mapActions } from "vuex";
export default {
  props: ["CityID", "ShowTitle", "SetVuex"],
  computed: {
    CurrentCityID() {
      return this.CityID;
    },
    IsShowTitle() {
      return this.ShowTitle || false;
    },
    IsSetVuex() {
      return this.SetVuex || false;
    }
  },
  watch: {
    CurrentCityID(val) {
      this.getProj();
    }
  },
  data() {
    return {
      ProjData: [],
    };
  },
  created() {
    this.getProj();
  },
  methods: {
    ...mapActions(["setProj"]),
    SelectProjChange(index) {
      var proj = this.ProjData.filter(i => i._id == index)[0];
      this.$emit("SelectProjChange", {
        ProjID: proj._id,
        ProjName: proj.Name,
        CityName: proj.City.Name,
        ProvName: proj.Prov.Name
      });
      if (this.IsSetVuex) {
        this.setProj({
          ProjID: proj._id,
          ProjName: proj.Name,
          CityName: proj.City.Name,
          ProvName: proj.Prov.Name
        });
      }
    },
    getProj() {
      let that = this;
      axios
        .get(`/api/GetProjByCityID?CityID=${that.CurrentCityID}`)
        .then(res => {
          if (res.data.success) {
            that.ProjData = res.data.data;
            if (that.ProjData.length > 0) {
              if (that.CityID && that.IsSetVuex) {
                this.setProj({
                  ProjID: that.ProjData[0]._id,
                  ProjName: that.ProjData[0].Name,
                  CityName: that.ProjData[0].City.Name,
                  ProvName: that.ProjData[0].Prov.Name
                });
              }
            }
          } else {
            that.$message({
              type: "error",
              message: "获取项目失败!请刷新重试"
            });
          }
        })
        .catch(err => console.log(err));
    }
  }
};
</script>

<style scoped>
.main {
  margin-top: 5px;
  margin-bottom: 5px;
}
.el-menu {
  padding-top: 5px;
  background-color: #fff;
  margin: 0 auto;
  border: 1 solid;
}
.text {
  font-size: 14px;
}
.item {
  margin-bottom: 18px;
}
.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}
</style>
