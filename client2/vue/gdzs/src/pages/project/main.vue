<template>
  <f7-page>
    <f7-navbar>
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
            <f7-link href="/project/list/">柳州项目</f7-link>
        </div>
    </div>
    <div class="content">
        <f7-row>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/现场监管.png" alt="">
                    <p>现场管理</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/workers/main/">
                    <img src="static/imgs/人员监管.png" alt="">
                    <p>人员监管</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/环境监测.png" alt="">
                    <p>环境监测</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/机械设施监控.png" alt="">
                    <p>机械设备监测</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/meetings/list/">
                    <img src="static/imgs/日历事件.png" alt="">
                    <p>日历事件</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/ranking/main/">
                    <img src="static/imgs/排行.png" alt="">
                    <p>排行榜</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/especially-task/main/">
                    <img src="static/imgs/特种作业.png" alt="">
                    <p>特种作业审批</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/大事件.png" alt="">
                    <p>大事件</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <f7-icon f7="add" size="64" color="gray"></f7-icon>
                </f7-link>
            </f7-col>
        </f7-row>
    </div>
  </f7-page>
</template>
<script>
export default {
  methods: {
    scan() {
      cordova.plugins.barcodeScanner.scan(
        function(result) {
          alert(
            "We got a barcode\n" +
              "Result: " +
              result.text +
              "\n" +
              "Format: " +
              result.format +
              "\n" +
              "Cancelled: " +
              result.cancelled
          );
        },
        function(error) {
          alert("Scanning failed: " + error);
        },
        {
          preferFrontCamera: true, // iOS and Android
          showFlipCameraButton: true, // iOS and Android
          showTorchButton: true, // iOS and Android
          torchOn: true, // Android, launch with the torch switched on (if available)
          saveHistory: true, // Android, save scan history (default false)
          prompt: "Place a barcode inside the scan area", // Android
          resultDisplayDuration: 500, // Android, display scanned text for X ms. 0 suppresses it entirely, default 1500
          formats: "QR_CODE,PDF_417", // default: all but PDF_417 and RSS_EXPANDED
          orientation: "landscape", // Android only (portrait|landscape), default unset so it rotates with the device
          disableAnimations: true, // iOS
          disableSuccessBeep: false // iOS and Android
        }
      );
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

.content {
  padding: 132px 0 0 0;
  text-align: center;
}

.app-name {
  font-size: 1.5em;
}

a {
  display: block;
}

.content div[class*="col"] {
  text-align: center;
  margin-top: 10px;
}

.content img {
  width: 64px;
  height: 64px;
}

.project-name a {
  color: #fff;
}

#img-add {
  color: gray;
}
</style>

