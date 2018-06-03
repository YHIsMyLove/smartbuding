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
                    <i :class="item.iconCls"/>{{item.name}}
                </template>
                <el-menu-item :key="child.path"
                                v-for="child in item.children"
                                v-if="!child.hidden"
                                :index="child.path">
                    <template slot="title">
                        <i :class="child.iconCls"/>
                    </template>
                    {{child.name}}
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
      console.log(nval);
      this.iscollapse2 = nval;
    }
  },
  created() {
    console.log(this.$router.options.routes);
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
</style>


