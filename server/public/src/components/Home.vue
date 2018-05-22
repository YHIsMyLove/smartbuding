<template>
	<el-row class="panel">
		<el-col :span="24" class="panel-top">
      <el-col :span="1">
        <div style="margin-top:4px;margin-left:12px">
          <img style="border-radius: 50%;vertical-align: middle;widht:32px;height:32px" src="../assets/helmet.png" alt="helmet">
        </div>
      </el-col>
			<el-col :span="17" >
				<span class="logo-txt">
					<i><b>项目助手</b>后台管理系统    </i>
				</span>
        <i @click="changeMenu" :class="MenuIcon"></i>
			</el-col>
      <el-col :span="5">
          <NavSearchProjControl/>
      </el-col>
      <el-col :span="1">
          <el-button type="danger" icon="el-icon-delete" style="border-radius:50%"/>
      </el-col>
		</el-col>

		<el-col :span="24" class="panel-center">
			<aside  class="left-aside">
        <div class="panel-User">
          <el-col :span="8">
            <div class="user-img">
              <img style="border-radius: 50%;vertical-align: middle;widht:32px;height:32px" src="../assets/user.png" alt="user">
            </div>
          </el-col>
          <el-col :span="8">
            <div style="margin-top:12px">
              <span style="font-size:15px">
              <b>王思宇</b></span>
              <br>
              <span style="font-size:10px;color:#C0C4CC">经理</span>
            </div>
          </el-col>
          <el-col :span="8">
            <div style="margin-top:12px">
              <el-tag type="info"> 工程师</el-tag>
            </div>
          </el-col>
          <el-col :span="24">
            <hr style="height:.5px;border:none;border-top:1px solid #ffff;" />
          </el-col>
        </div>
				<el-menu 
          :collapse='iscollapse' 
          :default-active="$route.path" 
          :default-openeds="openedArr"
          collapse-transition 
          class="aside-menu" 
          @open="handleopen" 
          @close="handleclose"
          @select="handleselect" 
          theme="dark" 
          unique-opened 
          router>
					<template v-for="(item,index) in $router.options.routes" v-if="!item.hidden">
						<el-submenu :key="index" :index="index+''" v-if="!item.leaf">
							<template slot="title">
								<i :class="item.iconCls"></i>{{item.name}}</template>
							<el-menu-item :key="child.path" v-for="child in item.children" v-if="!child.hidden" :index="child.path">
								{{child.name}}
							</el-menu-item>
						</el-submenu>
						<el-menu-item :key="item.children[0].path" v-if="item.leaf&&item.children.length>0" :index="item.children[0].path">
							<i :class="item.iconCls"></i>{{item.children[0].name}}</el-menu-item>
					</template>
				</el-menu>
			</aside>
			<section :class="contentStyle">
				<div class="grid-content bg-purple-light">
					<el-col :span="24" class="panel-inner-top">
						<strong>{{$route.name}}</strong>
						<el-breadcrumb separator="/" class="bread-crumb">
							<el-breadcrumb-item :to="{ path: '/' }" v-if="$route.path!='/'">首页</el-breadcrumb-item>
							<el-breadcrumb-item v-if="$route.path!='/'">{{$route.matched[0].name}}</el-breadcrumb-item>
							<el-breadcrumb-item>{{$route.name}}</el-breadcrumb-item>
						</el-breadcrumb>
					</el-col>
					<el-col :span="24" class="panel-inner-bottom">
						  <router-view></router-view>
					</el-col>
				</div>
			</section>
		</el-col>

	</el-row>
</template>

<script scoped>
import NavSearchProjControl from "./businesscontrols/NavSearchProjControl";
export default {
  components: { NavSearchProjControl: NavSearchProjControl },
  data() {
    return {
      projcascader: {
        options: [
          {
            label: "江苏",
            cities: []
          },
          {
            label: "浙江",
            cities: []
          }
        ],
        props: {
          value: "label",
          children: "cities"
        }
      },
      iscollapse: true,
      showList: [],
      openedArr: [],
      currentPath: ""
    };
  },
  computed: {
    contentStyle() {
      return this.iscollapse ? "panel-c-c2" : "panel-c-c";
    },
    MenuIcon() {
      return this.iscollapse ? "el-icon-menu" : "el-icon-minus";
    },
    logoStyle() {
      return this.iscollapse ? "logoStyle-small" : "logoStyle-normal";
    }
  },
  methods: {
    //选择项目   test
    ProjSelectChange(val) {
      setTimeout(_ => {
        if (
          val.indexOf("江苏") > -1 &&
          !this.projcascader.options[0].cities.length
        ) {
          this.projcascader.options[0].cities = [
            {
              label: "南京"
            }
          ];
        } else if (
          val.indexOf("浙江") > -1 &&
          !this.projcascader.options[1].cities.length
        ) {
          this.projcascader.options[1].cities = [
            {
              label: "杭州"
            }
          ];
        }
      }, 300);
    },
    changeMenu() {
      this.iscollapse = !this.iscollapse;
    },
    onSubmit() {},
    handleopen() {},
    handleclose() {},
    handleselect: function(a, b) {},
    logout: function() {
      var _this = this;
      this.$confirm("确认退出吗?", "提示", {
        type: "warning"
      })
        .then(() => {
          _this.$router.replace("/login");
        })
        .catch(() => {});
    }
  }
};
</script>

<style lang="scss" scoped>
.logoStyle-small {
  width: 46px;
  background: #324057;
}
.logoStyle-normal {
  width: 230px;
  background: #324057;
}

.panel {
  position: absolute;
  top: 0px;
  bottom: 0px;
  width: 100%;
}

.panel-top {
  height: 60px;
  // line-height: 60px;
  background: #1f2d3d;
  color: #c0ccda;
}

.panel-center {
  background: #324057;
  position: absolute;
  top: 60px;
  bottom: 0px;
  overflow: hidden;
}

.logo-txt {
  margin-left: 10px;
  font-size: 20px;
  i {
    color: #20a0ff;
    font-style: normal;
  }
  a {
    font-size: 14px;
    margin-left: 5px;
    color: #fff;
    text-decoration: none;
    &:hover {
      color: #ddd;
    }
  }
}

.left-aside {
  width: 230px;
  .aside-menu {
  }
}

.panel-c-c2 {
  background: #f1f2f7;
  position: absolute;
  right: 0px;
  top: 0px;
  bottom: 0px;
  left: 60px;
  overflow-y: scroll;
  padding: 20px;
  .panel-inner-top {
    margin-bottom: 15px;
    .bread-crumb {
      height: 22px;
      line-height: 22px;
      float: right;
    }
    strong {
      width: 200px;
      float: left;
      color: #475669;
    }
  }
  .panel-inner-bottom {
    background-color: #fff;
    box-sizing: border-box;
    padding: 15px;
  }
}
.panel-c-c {
  background: #f1f2f7;
  position: absolute;
  right: 0px;
  top: 0px;
  bottom: 0px;
  left: 230px;
  overflow-y: scroll;
  padding: 20px;
  .panel-inner-top {
    margin-bottom: 15px;
    .bread-crumb {
      height: 22px;
      line-height: 22px;
      float: right;
    }
    strong {
      width: 200px;
      float: left;
      color: #475669;
    }
  }
  .panel-inner-bottom {
    background-color: #fff;
    box-sizing: border-box;
    padding: 15px;
  }
}

.logout {
  background-size: contain;
  width: 20px;
  height: 20px;
  float: left;
}

.logo {
  width: 40px;
  float: left;
  margin: 10px 10px 10px 18px;
}

.tip-logout {
  float: right;
  margin-right: 20px;
  padding-top: 5px;
}

.tip-logout i {
  cursor: pointer;
}

.admin {
  color: #c0ccda;
  margin-left: 20px;
  i {
    margin-right: 5px;
  }
}
.el-cascader__label {
  line-height: 60px !important;
}
.panel-User {
  height: 80px;
  color: #ddd;
}
.user-img {
  margin-top: 12px;
  margin-left: 12px;
}

.el-submenu [class^="fa"] {
  margin-right: 24px;
}

.el-menu--collapse {
  width: 60px;
}
</style>