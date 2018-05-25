<template>
<el-row class="panel">
    <el-col :span="24">
        <NavBar :iscollapse='iscollapse' @ChangeCollapse='changeMenu' />
    </el-col>
<el-col :span="24" class="panel-center">
    <aside class="left-aside">
        <div class="panel-User">
        <el-col :span="8">
        <div class="user-img">
        <img style="border-radius: 50%;vertical-align: middle;widht:32px;height:32px" src="../assets/user.png" alt="user">
        </div>
        </el-col>
        <el-col :span="8">
        <div style="margin-top:12px">
        <span style="font-size:15px">
        <b>用户1</b></span>
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
        <el-menu :collapse='iscollapse' :default-active="$route.path" :default-openeds="openedArr" collapse-transition class="aside-menu" theme="dark" unique-opened router>
        <template v-for="(item,index) in $router.options.routes" v-if="!item.hidden">
        <el-submenu
        :key="index"
        :index="index+''"
        v-if="!item.leaf"
        >
        <template slot="title">
        <i :class="item.iconCls"></i>{{item.name}}
        </template>

        <el-menu-item
        :key="child.path"
        v-for="child in item.children"
        v-if="!child.hidden"
        :index="child.path"
        >
        {{child.name}}
        </el-menu-item>
        </el-submenu>
        <el-menu-item
        :key="item.children[0].path"
        v-if="item.leaf&&item.children.length>0"
        :index="item.children[0].path"
        >
        <i :class="item.iconCls"></i>{{item.children[0].name}}</el-menu-item>
        </template>

        </el-menu>
    </aside>
    <section :class="contentStyle">
        <div class="grid-content bg-purple-light">
        <el-col
        :span="24"
        class="panel-inner-top"
        >
        <strong>{{$route.name}}</strong>
        <el-breadcrumb
        separator="/"
        class="bread-crumb"
        >
        <el-breadcrumb-item
        :to="{ path: '/' }"
        v-if="$route.path!='/'"
        >首页</el-breadcrumb-item>
        <el-breadcrumb-item v-if="$route.path!='/'">{{$route.matched[0].name}}</el-breadcrumb-item>
        <el-breadcrumb-item>{{$route.name}}</el-breadcrumb-item>
        </el-breadcrumb>
        </el-col>
        <el-col
        :span="24"
        class="panel-inner-bottom"
        >
        <router-view></router-view>
        </el-col>
        </div>
    </section>
</el-col>
</el-row>
<el-col :span="24" class="panel-center">
    <aside class="left-aside">
        <div class="panel-User">
        <el-col :span="8">
        <div class="user-img">
        <img style="border-radius: 50%;vertical-align: middle;widht:32px;height:32px" src="../assets/user.png" alt="user">
        </div>
        </el-col>
        <el-col :span="8">
        <div style="margin-top:12px">
        <span style="font-size:15px">
        <b>用户1</b></span>
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
        <el-menu :collapse='iscollapse' :default-active="$route.path" :default-openeds="openedArr" collapse-transition class="aside-menu"  theme="dark" unique-opened router>
        <template v-for="(item,index) in $router.options.routes" v-if="!item.hidden">
        <el-submenu :key="index" :index="index+''" v-if="!item.leaf">
        <template slot="title">
        <i :class="item.iconCls"></i>{{item.name}}
        </template>

        <el-menu-item
        :key="child.path"
        v-for="child in item.children"
        v-if="!child.hidden"
        :index="child.path"
        >
        {{child.name}}
        </el-menu-item>
        </el-submenu>
        <el-menu-item
        :key="item.children[0].path"
        v-if="item.leaf&&item.children.length>0"
        :index="item.children[0].path"
        >
        <i :class="item.iconCls"></i>{{item.children[0].name}}</el-menu-item>
        </template>

        </el-menu>
    </aside>
    <section :class="contentStyle">
        <div class="grid-content bg-purple-light">
            <el-col
            :span="24"
            class="panel-inner-top"
            >
            <strong>{{$route.name}}</strong>
            <el-breadcrumb
            separator="/"
            class="bread-crumb"
            >
            <el-breadcrumb-item
            :to="{ path: '/' }"
            v-if="$route.path!='/'"
            >首页</el-breadcrumb-item>
            <el-breadcrumb-item v-if="$route.path!='/'">{{$route.matched[0].name}}</el-breadcrumb-item>
            <el-breadcrumb-item>{{$route.name}}</el-breadcrumb-item>
            </el-breadcrumb>
            </el-col>
            <el-col
            :span="24"
            class="panel-inner-bottom"
            >
            <router-view></router-view>
            </el-col>
        </div>
    </section>
</el-col>
</el-row>
</template>

<script>
import NavBar from "../businesscontrols/NavBar";
export default {
  components: {
    NavBar
  },
  data() {
    return {
      iscollapse: true,
      showList: [],
      openedArr: [],
      currentPath: ""
    };
  },
  computed: {
    contentStyle() {
      return this.iscollapse ? "panel-c-c2" : "panel-c-c";
    }
  },
  methods: {
    changeMenu() {
      this.iscollapse = !this.iscollapse;
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

.panel-center {
  background: #324057;
  position: absolute;
  top: 60px;
  bottom: 0px;
  overflow: hidden;
}

.left-aside {
  width: 230px;
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
