<template>
  <el-popover
    v-model="showMenuLink"
    placement="right"
    width="400"
    trigger="click"
    @hide="close">
    <SelectMenuLinkMenu @GetMenuLink="GetMenuLink"/>
    <el-button slot="reference">选择路由</el-button>
  </el-popover>
</template>
<script>
import SelectMenuLinkMenu from "./SelectMenuLinkMenu.vue";
export default {
  components: { SelectMenuLinkMenu },
  data() {
    return {
      showMenuLink: this.value
    };
  },
  watch: {
    value(n, o) {
      this.showMenuLink = n;
    }
  },
  props: ["value"],
  methods: {
    close() {
      this.$emit("input", false);
    },
    GetMenuLink(val) {
      this.$emit("GetMenuLink", val);
      this.$emit("GetValue", val.MenuName);
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

