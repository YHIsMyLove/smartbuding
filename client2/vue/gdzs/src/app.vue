<template>
  <!-- App -->
  
  <div id="app">
    <!-- Statusbar -->
    <f7-statusbar></f7-statusbar>
    <!-- Left Panel -->
    <f7-panel left reveal>
      <f7-view url="/account/main/"></f7-view>
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
          <f7-login-screen-title>登录</f7-login-screen-title>
          <f7-list form>
            <f7-list-item>
              <f7-label>用户名</f7-label>
              <f7-input name="username" placeholder="Username" type="text"></f7-input>
            </f7-list-item>
            <f7-list-item>
              <f7-label>密码</f7-label>
              <f7-input name="password" type="password" placeholder="Password"></f7-input>
            </f7-list-item>
          </f7-list>
          <f7-list>
            <f7-list-button title="登录" login-screen-close></f7-list-button>
            <f7-block-footer>
              <p>Click Sign In to close Login Screen</p>
            </f7-block-footer>
          </f7-list>
        </f7-page>
      </f7-view>
    </f7-login-screen>
  </div>
</template>

<script>
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
    }
  }
  // directive: {
  //   onDeviceReady: {
  //     bind(el) {
  //       document.addEventListener(
  //         "deviceready",
  //         () => {
  //           console.log("device ready");
  //         },
  //         false
  //       );
  //     }
  //   },
  //   onBackPressed: {
  //     bind(el) {
  //       document.addEventListener(
  //         "backbutton",
  //         () => {
  //           console.log("back pressed");
  //           if (this.$f7.views.main.history.length > 1)
  //             this.$f7.views.main.router.back();
  //         },
  //         false
  //       );
  //     }
  //   }
  // }
};
</script>
<style scoped>
</style>
