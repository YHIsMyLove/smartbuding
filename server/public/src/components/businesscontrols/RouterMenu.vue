<template>
    <el-menu :collapse='iscollapse2' 
                :default-active="$route.path" 
                :default-openeds="openedArr" 
                collapse-transition 
                class="aside-menu"  
                theme="dark" 
                unique-opened 
                router>
        <template v-for="(item,index) in $router.options.routes" v-if="!item.hidden">
            <el-submenu :key="index" :index="index.toString()">
                <template slot="title">
                    <i :class="item.iconCls"/>
                    {{item.name}}
                </template>
                <el-menu-item :key="child.path"
                                v-for="child in item.children"
                                v-if="!child.hidden"
                                :index="child.path">
                    <template slot="title">
                        <i :class="child.iconCls" style="font-size:16px"/>
                        {{child.name}}
                    </template>
                </el-menu-item>
            </el-submenu>
        </template>
    </el-menu>
</template>

<script>
export default {
  props: ["iscollapse"],
  data() {
    return {
      iscollapse2: this.iscollapse,
      openedArr: []
    };
  },
  watch: {
    iscollapse(nval, oval) {
      this.iscollapse2 = nval;
    }
  },
  created() {
    //这里判断权限
    this.$router.options.routes.forEach(element => {
      if (element.children) {
        element.hidden = false;
        element.children.forEach(i => {
          i.hidden = false;
        });
      }
    });
  },
  methods: {}
};
</script>

<style scoped>
.el-submenu [class^="fa"] {
  margin-right: 24px;
}

.el-menu--collapse {
  width: 60px;
}

i {
  margin-right: 24px;
  font-size: 24px;
}
</style>


