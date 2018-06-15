<template>
  <f7-page>
    <f7-navbar :no-shadow="true">
      <f7-nav-left v-if="isTabProject">
          <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" panel-open="left"></f7-link>
      </f7-nav-left>
      <f7-nav-title>{{navbarTitle}}</f7-nav-title>
      <f7-nav-right v-if="isTabProject">
          <f7-link icon-if-ios="f7:menu" icon-if-md="material:crop_free"></f7-link>
      </f7-nav-right>
    </f7-navbar>
    <f7-tabs>
        <f7-tab id="page-project-main" @tab:show="onTabProjectShow" @tab:hide="onTabProjectHide">
          <div class="head display-flex">
              <div>
                  <span class="app-name">工地项目助手</span><span>v1.0</span>
              </div>
              <div class="project-name">
                  <f7-link icon="link-project-info" href="/project/list/"><span class="font-little">{{prov.Name}}|{{city.Name}} {{project.Name}}</span></f7-link>
              </div>
          </div>
          <div id="links">
            <f7-row>
              <f7-col width="33">
                  <f7-link @click="gotoCameraListPage">
                      <img src="static/imgs/现场监管.png" alt="">
                      <div>现场管理</div>
                  </f7-link>
              </f7-col>
              <f7-col width="33">
                  <f7-link href="/workers/">
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
                    <f7-link href="/events/main/">
                        <img src="static/imgs/大事件.png" alt="">
                        <div>大事件</div>
                    </f7-link>
                </f7-col>
                <f7-col width="33">
                    
                </f7-col>
            </f7-row>
          </div>
        </f7-tab>
        <f7-tab id="page-doors-main" tab-active @tab:show="onTabDoorsShow" @tab:hide="onTabDoorsHide">
          <f7-toolbar>
            <div class="display-flex justify-content-center align-items-center" style="width:100%">
              <input type="date" v-model="selectedDate"/>
            </div>
          </f7-toolbar>
          <div id="content">
              <div class="my-divider" style="margin-top: 16px;"></div>
              <f7-row id="statistics" no-gap>
                <f7-col width="40">
                  <div id="chart" style="width:120px;height:120px;margin:0 auto;"></div>
                </f7-col>
                <f7-col width="30">
                  <div style="border-left:1px solid #ccc;border-right:1px solid #ccc;">
                    <f7-row>
                      <f7-col width="100">
                        <f7-link text="950" href="/doors/statistics/"></f7-link>
                      </f7-col>
                      <f7-col width="100">进场人次</f7-col>
                      <f7-col width="100">
                        <div style="height:32px;"></div>
                      </f7-col>
                      <f7-col width="100">
                        <f7-link text="453" href="/doors/statistics/"></f7-link>
                      </f7-col>
                      <f7-col width="100">出场人次</f7-col>
                    </f7-row>
                  </div>
                </f7-col>
                <f7-col width="30">
                  <f7-row>
                    <f7-col width="100">
                      <f7-link text="950" href="/doors/statistics/"></f7-link>
                    </f7-col>
                    <f7-col width="100">进场人数</f7-col>
                    <f7-col width="100">
                      <div style="height:32px;"></div>
                    </f7-col>
                    <f7-col width="100">
                      <f7-link text="453" href="/doors/statistics/"></f7-link>
                    </f7-col>
                    <f7-col width="100">现场人数</f7-col>
                  </f7-row>
                </f7-col>
              </f7-row>
              <div class="my-divider" style="margin:0 16px;"></div>
              <p class="text-align-center">今日出场人数934人  注册人数950人</p>
              <f7-list>
                <f7-list-item title="统计列表" link="/doors/statistics/"></f7-list-item>
              </f7-list>
              <f7-list>
                <f7-list-item v-for="(state, index) in doorStates" :key="index">
                  <div class="display-flex justify-content-space-between wmp">
                    <div>{{state.doorNum}}#门</div>
                    <f7-link :text="state.who" href="/workers/detail/"></f7-link>
                    <div>{{state.time}}</div>
                    <div>{{state.desc}}</div>
                  </div>
                </f7-list-item>
              </f7-list>
            </div>
        </f7-tab>
      </f7-tabs>
    <f7-toolbar tabbar bottom-md labels>
      <f7-link :icon="linkProjectClass" text="项目" tab-link="#page-project-main"></f7-link>
      <f7-link :icon="linkDoorsClass" text="门禁" tab-link="#page-doors-main"></f7-link>
    </f7-toolbar>
  </f7-page>
</template>
<script>
import context from "../service-context.js";
// 引入基本模板
let echarts = require("echarts/lib/echarts");
// 引入柱状图组件
require("echarts/lib/chart/pie");
// 引入提示框和title组件
require("echarts/lib/component/tooltip");
require("echarts/lib/component/title");
export default {
  mounted() {
    // 基于准备好的dom，初始化echarts实例
    var chart = echarts.init(document.getElementById("chart"));
    var option = {
      legend: {
        orient: "vertical",
        x: "left",
        data: ["直接访问", "邮件营销", "联盟广告", "视频广告", "搜索引擎"]
      },
      series: [
        {
          name: "访问来源",
          type: "pie",
          radius: ["80%", "100%"],
          avoidLabelOverlap: false,
          label: {
            normal: {
              show: false,
              position: "center"
            },
            emphasis: {
              show: false,
              textStyle: {
                fontSize: "30",
                fontWeight: "bold"
              }
            }
          },
          labelLine: {
            normal: {
              show: false
            }
          },
          data: [
            { value: 934, name: "出勤人数" },
            { value: 16, name: "缺勤人数" }
          ]
        }
      ]
    };
    chart.setOption(option);
  },
  data() {
    return {
      project: {},
      prov: {},
      city: {},
      doorStates: [
        {doorNum: '1', who: '张三', time: "2018-06-10 08:30", desc: "进场"},
        {doorNum: '2', who: '李四', time: "2018-06-10 08:31", desc: "进场"},
      ],
      linkProjectClass: "link-project-highlight",
      linkDoorsClass: "link-doors",
      selectedDate: "",
      navbarTitle: '',
      isTabProject: true
    };
  },
  on: {
    pageInit(event, pageData) {
      console.log("home page init");
      var now = new Date();
      var year = now.getFullYear();
      var month = (now.getMonth() + 1);
      if (month < 10)
        month = '0' + month;
      var day = now.getDate();
      if (day < 10)
        day = '0' + month;
      this.selectedDate = `${year}-${month}-${day}`;
      console.log(this.selectedDate);
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
    gotoCameraListPage() {
      cordova.plugins.CameraController.gotoCameraListPage(
        context.accessToken,
        null,
        null
      );
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
    },
    onTabProjectShow() {
      // console.log('on project tab show');
      this.linkProjectClass = "link-project-highlight";
      this.navbarTitle = '';
      this.isTabProject = true;
    },
    onTabProjectHide() {
      // console.log('on project tab hide');
      this.linkProjectClass = "link-project";
    },
    onTabDoorsShow() {
      // console.log('on doors tab show');
      this.linkDoorsClass = "link-doors-highlight";
      this.navbarTitle = '门禁记录'
      this.isTabProject = false;
    },
    onTabDoorsHide() {
      // console.log('on doors tab hide');
      this.linkDoorsClass = "link-doors";
    }
  }
};
</script>
<style scoped>
.head {
  position: absolute;
  left: 0;
  right: 0;
  top: 56px;
  height: 100px;
  padding: 16px;
  background-color: #2196f3;
  color: #fff;
  flex-direction: column;
  justify-content: flex-end;
}

#links {
  position: absolute;
  left: 0;
  top: 188px;
  right: 0;
  bottom: 56px;
  text-align: center;
}

.app-name {
  font-size: 1.5em;
}

#links a {
  display: block;
  color: black;
}
#links img {
  width: 45%;
  height: 45%;
}
#links div[class*="row"] {
  height: 33.3%;
  justify-content: center;
  align-items: center;
}

.project-name a {
  color: #fff;
}

#img-add {
  color: gray;
}

.md .toolbar:after {
  height: 1px;
  background-color: #ccc;
}

.md .toolbar {
  background-color: #fff;
  color: rgba(0, 0, 0);
}

.md .tabbar a.tab-link.tab-link-active {
  color: black;
}

.md .toolbar a {
  color: rgba(0, 0, 0, 0.5);
}

#statistics {
  align-items: center;
  padding: 16px;
  text-align: center;
}
#statistics a {
  font-size: 16px;
}
</style>
