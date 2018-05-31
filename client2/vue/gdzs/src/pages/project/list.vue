<template>
    <f7-page>
        <f7-navbar title="项目列表" back-link="Back">
            <f7-subnavbar>
              <f7-input type="select" placeholder="选择省">
                <option value="湖北省">湖北省</option>
                <option value="湖南省">湖南省</option>
              </f7-input>
              <f7-icon f7="down"></f7-icon>
              <f7-input type="select" placeholder="选择市">
                <option value="武汉市">武汉市</option>
                <option value="黄石市">黄石市</option>
              </f7-input>
              <f7-icon f7="down"></f7-icon>
              <f7-searchbar
                disable-link-text="Cancel"
                placeholder="Search in items"
                :clear-button="true"
              ></f7-searchbar>
            </f7-subnavbar>
        </f7-navbar>
        <f7-list>
            <f7-list-item :key="index" v-for="(project,index) in projects" :title="project.name" link @click="onItemClick(project, $event)">
                <img slot="after" src="static/imgs/项目定位30x30.png" alt="">
            </f7-list-item>
        </f7-list>
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
.subnavbar .row {
  width: 100%;
}
.subnavbar {
  background-color: #fff;
  color: #000;
}
#searchbar {
  flex: auto;
}
select
{
  /* margin-top: 20px; */
}
</style>



