<template>
  <!-- App -->
  
  <div id="app">
    <!-- Statusbar -->
    <f7-statusbar></f7-statusbar>
    <!-- Left Panel -->
    <f7-panel left reveal>
      <f7-view id="userMain" url="/account/main/"></f7-view>
    </f7-panel>
    <!-- Main View -->
    <f7-view id="main-view" url="/" main></f7-view>
    <!-- <f7-views tabs>
      <f7-view id="view-project-main" url="/project/main/" main tab active></f7-view>
      <f7-view id="view-doors-main" url="/doors/main/" tab></f7-view>
      <f7-toolbar tabbar labels bottom-md>
        <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" text="项目管理" tab-link="#view-project-main"></f7-link>
        <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" text="门禁记录" tab-link="#view-doors-main"></f7-link>
      </f7-toolbar>
    </f7-views> -->
    <!-- Popup -->
    <f7-popup id="project-list">
      <f7-view url="/project/list/">
        <!-- <f7-page>
          <f7-navbar title="Popup">
            <f7-nav-right>
              <f7-link popup-close>Close</f7-link>
            </f7-nav-right>
          </f7-navbar>
          <f7-block>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Neque, architecto. Cupiditate laudantium rem nesciunt numquam, ipsam. Voluptates omnis, a inventore atque ratione aliquam. Omnis iusto nemo quos ullam obcaecati, quod.</f7-block>
        </f7-page> -->
      </f7-view>
    </f7-popup>
    <!-- Login Screen -->
    <f7-login-screen id="login-screen">
      <f7-view>
        <f7-page login-screen>
          <div class="display-flex justify-content-center content padding">
            <div class="text-align-center margin-top margin-bottom">
              <img style="position:relative;top:-56px" class="img-big" src="static/imgs/LOGO240x240.png" alt="">
            </div>
            <input id="inputUsername" class="bordered-input margin-bottom" name="username" placeholder="请输入用户名" type="text" value="lbgongfu">
            <input id="inputPwd" class="bordered-input margin-bottom" name="username" placeholder="请输入用户名" type="password" value="lbgongfu">
            <f7-button round big fill color="white" @click="login" text-color="blue" text="登陆"></f7-button>
          </div>
        </f7-page>
      </f7-view>
    </f7-login-screen>
  </div>
</template>

<script>
import context from "./service-context.js";
export default {
  mounted() {
    if (this.$device.cordova) {
      document.addEventListener(
        "deviceready",
        () => {
          console.log("device ready");
        },
        false
      );
      document.addEventListener(
        "backbutton",
        () => {
          console.log("back pressed");
          if (this.$f7.views.main.history.length > 1)
            this.$f7.views.main.router.back();
          else navigator.app.exitApp();
        },
        false
      );
    }
  },
  data() {
    return {
      username: "lbgongfu",
      password: "123456"
    };
  },
  computed: {
    isiOS() {
      return this.$theme.ios;
    },

    isMaterial() {
      return this.$theme.md;
    }
  },
  methods: {
    onDeviceReady() {
      console.log("device ready");
    },
    onBackPressed() {
      console.log("back pressed");
      if (this.$f7.views.main.history.length > 1)
        this.$f7.views.main.router.back();
      else this.$f7.exitApp();
    },
    login() {
      var preloader = this.$f7.dialog.preloader("请稍后。。。");
      var url = context.urls.loginUrl;
      var params = { UserID: this.username, UserPwd: this.password };
      console.log(`request url: ${url}, params: ${params}`);
      var that = this;
      this.$f7.request.post(
        url,
        params,
        function(data) {
          console.log(`response: ${data}`);
          preloader.close(true);
          if (data.success)
          {
            that.$f7.loginScreen.close("#login-screen", true);
            context.isLogin = true;
            context.accessToken = data.data.YSToken;
            context.sessionID = data.data.SessionID;
            that.$f7.views.main.router.refreshPage();
          }
          else
          {
            console.error(data.msg);
            that.$f7.dialog.alert(data.msg, "错误");
          }
        },
        function(xhr, status) {
          console.error(`error status: ${status}`)
          preloader.close(true);
          that.$f7.dialog.alert(`网络忙，请稍后重试！[status: ${status}]`);
        },
        "json"
      );
    }
  }
};
</script>
<style scoped>
#app{
  font-size: 12px;
}
.page-content .login-screen-content{
  background-color: #2196f3;
}
.content{
  flex-direction: column;
  position: absolute;
  left: 0;
  right: 0;
  top: 0;
  bottom: 0;
  background-color: #2196f3;
}
.bordered-input{
  border:1px solid white;
  border-radius: 50px;
  color: white;
  padding: 12px 12px 12px 60px;
  font-size: 1.5em;
}
</style>
