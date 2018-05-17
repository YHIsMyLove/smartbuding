<template>
	<el-row class="panel">

		<el-col :span="24" class="panel-top">
			<el-col :span="20">
				<span class="logo-txt">
					<i>工地项目助手</i>
				</span>
			</el-col>
			<el-col :span="4">
				<el-tooltip class="item tip-logout" effect="dark" content="退出" placement="bottom">
					<i class="fa fa-sign-out" aria-hidden="true" v-on:click="logout"></i>
				</el-tooltip>
			</el-col>
		</el-col>

		<el-col :span="24" class="panel-center">
			<aside class="left-aside">
				<el-menu :default-active="$route.path" :default-openeds="openedArr" class="aside-menu" @open="handleopen" @close="handleclose"
				 @select="handleselect" theme="dark" unique-opened router>
					<template v-for="(item,index) in $router.options.routes" v-if="!item.hidden">
						<el-submenu :index="index+''" v-if="!item.leaf">
							<template slot="title">
								<i :class="item.iconCls"></i>{{item.name}}</template>
							<el-menu-item v-for="child in item.children" v-if="!child.hidden" :index="child.path">
								{{child.name}}
							</el-menu-item>
						</el-submenu>
						<el-menu-item v-if="item.leaf&&item.children.length>0" :index="item.children[0].path">
							<i :class="item.iconCls"></i>{{item.children[0].name}}</el-menu-item>
					</template>
				</el-menu>
			</aside>
	
			<section class="panel-c-c">
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
              <!-- <transition name="fade" mode="out-in">
                <keep-alive :include="cachedViews">
                  <router-view></router-view>
                </keep-alive>
              </transition> -->
					</el-col>
				</div>
			</section>
		</el-col>

	</el-row>
</template>

<script>
export default {
  data() {
    return {
      showList: [],
      openedArr: [],
      currentPath: ""
    };
  },
  methods: {
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
.panel {
  position: absolute;
  top: 0px;
  bottom: 0px;
  width: 100%;
}

.panel-top {
  height: 60px;
  line-height: 60px;
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
</style>