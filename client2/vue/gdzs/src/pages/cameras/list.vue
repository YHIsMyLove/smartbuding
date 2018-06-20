<template>
    <f7-page>
        <f7-navbar title="现场管理" back-link="Back"></f7-navbar>
        <f7-list>
        <f7-list-group>
            <f7-list-item title="塔区" group-title></f7-list-item>
            <f7-list-item title="C6C" link>
                <img class="img-middle" src="static/imgs/logo.png" alt="" slot="media">
            </f7-list-item>
            <f7-list-item title="DS-2CD3T20D-15" link>
                <img class="img-middle" src="static/imgs/logo.png" alt="" slot="media">
            </f7-list-item>
        </f7-list-group>
        </f7-list>
    </f7-page>
</template>
<script>
import context from '../../service-context.js';
export default {
  on: {
    pageInit(event, pageData) {
      console.log('camera list page init');
      this.fetchCameras();
    },
    pageAfterIn(event, pageData) {
      console.log('camera list page after in');
      // this.fetchProjects();
    },
    pageAfterOut(event, pageData) {
      //alert("page after out");
    }
  },
  data(){
    return{
      cameras: []
    }
  },
  methods:{
    fetchCameras(){
      var that = this;
      var preloader = that.$f7.dialog.preloader("请稍后。。。");
      var url = context.urls.getCamerasUrl;
      console.log(`request url: ${url}`);
      that.$f7.request.get(url, function(data){
        console.log(`reponse: ${JSON.stringify(data)}`);
        if (data.success)
        {
          that.cameras = data.data;
        }
        else
        {
          console.error(`error ${data.msg}`);
          that.$f7.dialog.alert(data.msg);
        }
      },function(xhr, status){
        preloader.close(true);
        console.error(`error ${status}`);
        that.$f7.dialog.alert("网络忙，请稍后重试！");
      }, 'json')
    }
  }
}
</script>
<style scoped>
</style>


