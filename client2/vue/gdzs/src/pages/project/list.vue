<template>
    <f7-page>
        <f7-navbar title="项目列表" back-link="Back">
            <f7-subnavbar>
                <f7-row>
                    <f7-col width="50">
                        <input type="text" placeholder="选择省" readonly="readonly" id="select-province"/>
                    </f7-col>
                    <f7-col width="50">
                        <input type="text" placeholder="选择市" readonly="readonly" id="select-province"/>
                    </f7-col>
                </f7-row>
            </f7-subnavbar>
        </f7-navbar>
        <div class="filter-area"></div>
        <div class="content">
            <f7-list>
                <f7-list-item :key="index" v-for="(project,index) in projects" :title="project.name" link @click="onItemClick(project, $event)">
                    <img slot="after" src="static/imgs/项目定位30x30.png" alt="">
                </f7-list-item>
            </f7-list>
        </div>
    </f7-page>
</template>
<script>
export default {
  mounted() {
    // var pickerDevice = this.$f7.picker.create({
    //   inputEl: "#select-province",
    //   cols: [
    //     {
    //       textAlign: "center",
    //       values: [
    //         "iPhone 4",
    //         "iPhone 4S",
    //         "iPhone 5",
    //         "iPhone 5S",
    //         "iPhone 6",
    //         "iPhone 6 Plus",
    //         "iPad 2",
    //         "iPad Retina",
    //         "iPad Air",
    //         "iPad mini",
    //         "iPad mini 2",
    //         "iPad mini 3"
    //       ]
    //     }
    //   ]
    // });
  },
  data() {
    return {
      projects: [
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" },
        { name: "柳州火车站项目" },
        { name: "黄石火车站项目" }
      ]
    };
  },
  methods: {
    onItemClick(item, event) {
      console.log(item);
      this.$f7.views.main.router.back();
    }
  }
};
</script>
<style scoped>
.filter-area {
  position: absolute;
  top: 56px;
  left: 0;
  right: 0;
}
.content {
  padding-bottom: 56px;
}
.subnavbar .row {
  width: 100%;
}
.subnavbar {
  background-color: #fff;
  color: #000;
}
</style>



