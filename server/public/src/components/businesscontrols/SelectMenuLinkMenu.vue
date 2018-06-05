<template>
    <div class="main">
        <el-menu @select="GetSelectMenuLink">
            <el-menu-item   v-for="(item,index) in MenuLinks"
                            :key="index" :index="index.toString()">
                <template>
                    <i style="float: left" :class="item.MenuIcon"/>
                    <span style="float: left">{{ item.MenuName }}</span>
                    <span style="float: right; color: #8492a6; font-size: 13px">{{ item.MenuPath }}</span>
                </template>
            </el-menu-item>
        </el-menu>
    </div>
</template>
<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
export default {
  data() {
    return {
      MenuLinks: []
    };
  },
  created() {
    this.GetMenuLink();
  },
  methods: {
    GetMenuLink() {
      let that = this;
      NProgress.start();
      axios
        .get(`/api/ListMenuLink`)
        .then(res => {
          if (res.data.success) {
            let data = res.data.data;
            that.MenuLinks = data;
          }
        })
        .catch(err => {});
      NProgress.done();
    },
    GetSelectMenuLink(val) {
      this.$emit("GetMenuLink", this.MenuLinks[val]);
    }
  }
};
</script>
<style scoped>
.el-menu {
  padding-top: 5px;
  background-color: #fff;
  margin: 0 auto;
  border: 1 solid;
}
.el-menu-item {
  border-bottom: 1px solid;
  border-bottom-color: #c0c4cc;
}
</style>


