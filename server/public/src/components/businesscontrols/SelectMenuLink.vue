<template>
  <el-popover
    v-model="showMenuLink"
    placement="right"
    width="400"
    trigger="click"
    @hide="close">
    <div class="main">
        <el-menu @close="close" @select="GetSelectMenuLink">
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
    <el-button slot="reference">选择路由</el-button>
  </el-popover>
</template>
<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
export default {
  data() {
    return {
      showMenuLink: this.value,
      MenuLinks: []
    };
  },
  watch: {
    value(n, o) {
      this.showMenuLink = n;
    }
  },
  props: ["value"],
  created() {
    this.GetMenuLink();
  },
  methods: {
    close() {
      this.$emit("input", false);
    },
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
      this.$emit("GetValue", this.MenuLinks[val].MenuName);
      this.showMenuLink = false;
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

