<template>
  <f7-page tabs :page-content="false">
    <f7-page-content tab tab-active id="page-project-main">
      <f7-page id="pageProjectMain">
        <f7-navbar :no-shadow="true">
            <f7-nav-left>
                <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" panel-open="left"></f7-link>
            </f7-nav-left>
            <f7-nav-right>
                <f7-link icon-if-ios="f7:menu" icon-if-md="material:crop_free"></f7-link>
            </f7-nav-right>
        </f7-navbar>
        <div class="head display-flex" slot="fixed">
            <div>
                <span class="app-name">工地项目助手</span><span>v1.0</span>
            </div>
            <div class="project-name">
                <f7-link href="/project/list/"><span class="font-little">{{prov.Name}}|{{city.Name}} {{project.Name}}</span></f7-link>
            </div>
        </div>
        <f7-row>
          <f7-col width="33">
              <f7-link @click="gotoCameraListPage">
                  <img src="static/imgs/现场监管.png" alt="">
                  <div>现场管理</div>
              </f7-link>
          </f7-col>
          <f7-col width="33">
              <f7-link href="/workers/main/">
                  <img src="static/imgs/人员监管.png" alt="">
                  <div>人员监管</div>
              </f7-link>
          </f7-col>
          <f7-col width="33">
              <f7-link href="/env/main/">
                  <img src="static/imgs/环境监测.png" alt="">
                  <div>环境监测</div>
              </f7-link>
          </f7-col>
        </f7-row>
        <f7-row>
            <f7-col width="33">
                <f7-link href="/device/main/">
                    <img src="static/imgs/机械设施监控.png" alt="">
                    <div>机械设备监测</div>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/meetings/list/">
                    <img src="static/imgs/日历事件.png" alt="">
                    <div>日历事件</div>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/ranking/main/">
                    <img src="static/imgs/排行.png" alt="">
                    <div>排行榜</div>
                </f7-link>
            </f7-col>
            </f7-row>
            <f7-row>
            <f7-col width="33">
                <f7-link href="/especially-task/main/">
                    <img src="static/imgs/特种作业.png" alt="">
                    <div>特种作业审批</div>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/大事件.png" alt="">
                    <div>大事件</div>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <f7-icon f7="add" size="64" color="gray"></f7-icon>
                </f7-link>
            </f7-col>
        </f7-row>
      </f7-page>
    </f7-page-content>
    <f7-page-content tab id="page-doors-main">
      <f7-page>
        <f7-navbar title="门禁记录">
        </f7-navbar>
        <div class="page-data-filter display-flex justify-content-center" slot="fixed">
          <f7-date-picker></f7-date-picker>
        </div>
        <div id="content">
          <div class="my-divider" style="margin-top: 16px;"></div>
          <div style="text-align:center;padding: 16px 0;">
            <f7-row no-gap>
              <f7-col width="40">
                
              </f7-col>
              <f7-col width="30">
                <div style="border-left:1px solid #ccc;border-right:1px solid #ccc;">
                  <f7-row>
                    <f7-col width="100">950</f7-col>
                    <f7-col width="100">进场次数</f7-col>
                    <f7-col width="100">
                      <div style="height:32px;"></div>
                    </f7-col>
                    <f7-col width="100">453</f7-col>
                    <f7-col width="100">进场人数</f7-col>
                  </f7-row>
                </div>
              </f7-col>
              <f7-col width="30">
                <f7-row>
                  <f7-col width="100">950</f7-col>
                  <f7-col width="100">进场次数</f7-col>
                  <f7-col width="100">
                    <div style="height:32px;"></div>
                  </f7-col>
                  <f7-col width="100">453</f7-col>
                  <f7-col width="100">进场人数</f7-col>
                </f7-row>
              </f7-col>
            </f7-row>
          </div>
          <div class="my-divider" style="margin:0 16px;"></div>
          <p class="text-align-center">今日出场人数934人  注册人数950人</p>
          <div class="my-divider"></div>
          <f7-list>
            <f7-list-item title="统计列表" link="/doors/statistics/"></f7-list-item>
          </f7-list>
          <f7-list>
            <f7-list-item>
              <f7-list-item-cell>
                <f7-list-item-row>
                  <f7-list-item-cell>1#门</f7-list-item-cell>
                  <f7-list-item-cell>陈小春</f7-list-item-cell>
                  <f7-list-item-cell>17:56:16</f7-list-item-cell>
                  <f7-list-item-cell>出场</f7-list-item-cell>
                </f7-list-item-row>
              </f7-list-item-cell>
            </f7-list-item>
            <f7-list-item>
              <f7-list-item-cell>
                <f7-list-item-row>
                  <f7-list-item-cell>2#门</f7-list-item-cell>
                  <f7-list-item-cell>陈小春</f7-list-item-cell>
                  <f7-list-item-cell>08:56:16</f7-list-item-cell>
                  <f7-list-item-cell>进场</f7-list-item-cell>
                </f7-list-item-row>
              </f7-list-item-cell>
            </f7-list-item>
          </f7-list>
        </div>
      </f7-page>
    </f7-page-content>
    <f7-toolbar tabbar bottom-md :no-shadow="true">
      <div class="my-divider" slot="before-inner"></div>
      <f7-link tab-link="#page-project-main">
        <img class="img-little" src="static/imgs/主界面70x70.png" alt="">
      </f7-link>
      <f7-link tab-link="#page-doors-main">
        <img class="img-little" src="static/imgs/门禁70x70.png" alt="">
      </f7-link>
    </f7-toolbar>
  </f7-page>
</template>
<script>
import context from "../service-context.js";
export default {
  data(){
    return {
      project: {},
      prov: {},
      city:{},
    }
  },
  on: {
    pageInit(event, pageData) {
      console.log("home page init");
    },
    pageAfterIn(event, pageData) {
      console.log("home page after in");
      var that = this;
      if (!context.currentUser) {
        if (context.sessionID) {
          var preloader = that.$f7.dialog.preloader("请稍后。。。");
          var url = context.urls.getUserInfoUrl.format(context.sessionID);
          console.log("request '{0}'".format(url));
          that.$f7.request.get(
            url,
            function(data) {
              console.log(data);
              preloader.close(true);
              if (data.success) {
                context.currentUser = data.data;
                that.$f7.views.get("#userMain").router.refreshPage();
                that.initData();
              } else {
                that.gotoLogin();
              }
            },
            function(xhr, status) {
              preloader.close(true);
              that.gotoLogin();
            },
            "json"
          );
        } else {
          that.gotoLogin();
        }
      } else {
        that.initData();
      }
    },
    pageAfterOut(event, pageData) {
      console.log("page after out");
    }
  },
  methods: {
    gotoCameraListPage(){
      cordova.plugins.CameraController.gotoCameraListPage(context.accessToken, null, null);
    },
    gotoLogin() {
      this.$f7.loginScreen.open("#login-screen", true);
    },
    initData() {
      console.log("init data");
      if (context.currentProject) {
        if (
          context.selectedProject &&
          context.selectedProject._id != context.currentProject._id
        ) {
          this.updateProject(context.selectedProject);
          context.selectedProject = undefined;
        }
      } else {
        if (context.selectedProject) {
          this.updateProject(context.selectedProject);
          context.selectedProject = undefined;
        } else {
          this.$f7.views.main.router.navigate("/project/list/");
        }
      }
    },
    updateProject(newProject) {
      context.currentProject = newProject;
      context.currentProv = newProject.Prov;
      context.currentCity = newProject.City;
      this.project = newProject;
      this.prov = newProject.Prov;
      this.city = newProject.City;
    }
  }
};
</script>
<style scoped>
.head {
  position: absolute;
  top: 56px;
  left: 0;
  right: 0;
  height: 100px;
  padding: 16px;
  background-color: #2196f3;
  color: #fff;
  z-index: 500;
  flex-direction: column;
  justify-content: flex-end;
}

.app-name {
  font-size: 1.5em;
}

a {
  display: block;
  color: black;
}

#pageProjectMain
{
  padding-top: 132px;
}
#pageProjectMain img{
  width: 45%;
  height: 45%;
}
#pageProjectMain div[class*="row"]{
  height: 33.3%;
  justify-content: center;
  align-items: center;
}
#pageProjectMain div[class*="col"]{
  text-align: center;
}

.project-name a {
  color: #fff;
}

#img-add {
  color: gray;
}
/* 
.toolbar{
  background-color: #fff;
}
.toolbar a{
  padding-top: 0px;
  padding-bottom: 0px;
} */
</style>
