<template>
    <div class="navbar">
        <div :class="LoggBoxStyle">
            <div class="Logo">
                <img src="../../assets/helmet.png" alt="helmet">
            </div>
        </div>
        <div class="ProjName">
            <i @click="changeMenu" :class="MenuIcon"></i>
            <span class="logo-txt">		<b>项目助手</b>后台管理系统    	</span>
        </div>
        <div class="SearchBar">
            <NavSearchProjControl/>
        </div>
    </div>
</template>
<script>
import NavSearchProjControl from "./NavSearchProjControl";

export default {
  props: ["iscollapse"],
  data() {
    return {};
  },
  methods: {
    changeMenu() {
      this.$emit("ChangeCollapse");
    }
  },
  components: {
    NavSearchProjControl
  },
  computed: {
    MenuIcon() {
      return this.iscollapse ? "el-icon-menu" : "el-icon-minus";
    },
    LoggBoxStyle() {
      return this.iscollapse ? "LogoBox" : "LogoBox-big";
    }
  }
};
</script>

<style scoped>
.navbar {
  height: 60px;
  width: 100%;
  background: #324057;
  color: #c0ccda;
}
.LogoBox {
  width: 60px;
  height: 100%;
  background: #1f2d3d;
  float: left;
}
.LogoBox-big {
  width: 230px;
  height: 100%;
  background: #1f2d3d;
  float: left;
}
.Logo {
  width: 46px;
}
.Logo img {
  margin-left: 12px;
  margin-top: 12px;
  border-radius: 50%;
  width: 32px;
  height: 32px;
}

.ProjName {
  margin-left: 15px;
  margin-top: 15px;
  height: 100%;
  float: left;
}

.SearchBar {
  right: 230px;
  margin-left: 12px;
  margin-top: 12px;
  width: 30%;
  float: right;
}
</style>
