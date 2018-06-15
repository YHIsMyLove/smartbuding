<template>
    <f7-page>
        <f7-navbar title="项目列表" back-link="Back" :no-shadow="true">
          <f7-nav-right>
            <f7-link icon-f7="search_strong" @click="enableSearchbar"></f7-link>
          </f7-nav-right>
          <f7-searchbar 
            :expandable='true'
            id="searchbar"
            search-container="#listProject"
            placeholder="请输入关键字查询"
            @searchbar:enable="onEnable"
            @searchbar:disable="onDisable"></f7-searchbar>
          <f7-subnavbar>
            <select name="" id="">
              <option value="湖北省">湖北省</option>
              <option value="湖南省">湖南省</option>
            </select>
            <select>
              <option value="武汉市">武汉市</option>
              <option value="黄石市">黄石市</option>
            </select>
          </f7-subnavbar>
        </f7-navbar>
        <f7-list class="searchbar-not-found">
          <f7-list-item title="没有数据"></f7-list-item>
        </f7-list>
        <f7-list id="listProject">
            <f7-list-item :key="index" v-for="(project,index) in projects" :title="project.Name" link @click="onItemClick(project, $event)"></f7-list-item>
        </f7-list>
    </f7-page>
</template>
<script>
import context from '../../service-context.js';
export default {
  mounted() {},
  on: {
    pageInit(event, pageData) {
      //alert("page init");
    },
    pageAfterIn(event, pageData) {
      console.log('project list page after in');
      if (!context.currentUser) return;
      this.fetchProjects();
    },
    pageAfterOut(event, pageData) {
      //alert("page after out");
    }
  },
  data() {
    return {
      searchbarDisabled: true,
      projects: []
    };
  },
  methods: {
    onItemClick(item, event) {
      console.log(item);
      context.selectedProject = item;
      this.$f7.views.main.router.back();
    },
    fetchProjects() {
      var that = this;
      var preloader = that.$f7.dialog.preloader("请稍后。。。");
      var url = context.urls.getProjsByUser.format(context.currentUser._id);
      console.log('request ' + url);
      that.$f7.request.get(url, function(data){
        console.log('response: ' + JSON.stringify(data));
        preloader.close(true);
        if (data.success)
        {
          that.projects = data.data;
        }
        else
        {
          that.$f7.dialog.alert(data.msg);
        }
      }, function(xhr, status){
        preloader.close(true);
        that.$f7.dialog.alert("发生错误，请稍后重试！");
        console.log('error: ' + status);
      }, 'json');
    },
    enableSearchbar(){
      this.$f7.searchbar.enable('#searchbar');
    },
    onEnable: function (event) {
      this.searchbarDisabled = false;
    },
    onDisable: function (event) {
      this.searchbarDisabled = true;
    }
  }
};
</script>
<style scoped>
</style>



