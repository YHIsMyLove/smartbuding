<template>
  <el-popover
  v-model="showIcon"
    placement="right"
    width="400"
    trigger="click"
    @hide="close">
        <div class="main">
            <el-col :span="2" v-for="(item,index) in iconLst" :key="index">
                <i :class="item" @click="selectIcon(item)" />
            </el-col>
        </div>
        <el-button slot="reference">选择图标</el-button>
  </el-popover>
</template>
<script>
export default {
  data() {
    return {
      showIcon: this.value,
      iconLst: [
        " iconfont icon-pause",
        " iconfont icon-filedone",
        " iconfont icon-rest",
        " iconfont icon-rocket",
        " iconfont icon-fund",
        " iconfont icon-image",
        " iconfont icon-dash",
        " iconfont icon-solution",
        " iconfont icon-file-zip",
        " iconfont icon-left",
        " iconfont icon-folder",
        " iconfont icon-man",
        " iconfont icon-enter",
        " iconfont icon-align-center",
        " iconfont icon-shop",
        " iconfont icon-sort-descending",
        " iconfont icon-delete",
        " iconfont icon-frown",
        " iconfont icon-qrcode",
        " iconfont icon-pointmap",
        " iconfont icon-deploymentunit",
        " iconfont icon-lock",
        " iconfont icon-sliders",
        " iconfont icon-file-image",
        " iconfont icon-video",
        " iconfont icon-sound",
        " iconfont icon-earth",
        " iconfont icon-tag",
        " iconfont icon-unlock",
        " iconfont icon-hourglass",
        " iconfont icon-link",
        " iconfont icon-code",
        " iconfont icon-customerservice",
        " iconfont icon-idcard",
        " iconfont icon-api",
        " iconfont icon-file",
        " iconfont icon-close",
        " iconfont icon-sort-ascending",
        " iconfont icon-moneycollect",
        " iconfont icon-compass",
        " iconfont icon-message",
        " iconfont icon-file-text",
        " iconfont icon-filesearch",
        " iconfont icon-table",
        " iconfont icon-tags",
        " iconfont icon-shopping",
        " iconfont icon-heart",
        " iconfont icon-down",
        " iconfont icon-timeout",
        " iconfont icon-trophy",
        " iconfont icon-interation",
        " iconfont icon-home",
        " iconfont icon-accountbook",
        " iconfont icon-gold",
        " iconfont icon-highlight",
        " iconfont icon-wrench",
        " iconfont icon-pic-center",
        " iconfont icon-alert",
        " iconfont icon-funnelplot",
        " iconfont icon-file-word",
        " iconfont icon-attachment",
        " iconfont icon-fileprotect",
        " iconfont icon-Batchfolding",
        " iconfont icon-location",
        " iconfont icon-copyright",
        " iconfont icon-bulb",
        " iconfont icon-login",
        " iconfont icon-calendar",
        " iconfont icon-indent",
        " iconfont icon-bg-colors",
        " iconfont icon-boxplot",
        " iconfont icon-fullscreen-exit",
        " iconfont icon-download",
        " iconfont icon-logout",
        " iconfont icon-check",
        " iconfont icon-stock",
        " iconfont icon-eye",
        " iconfont icon-unlike",
        " iconfont icon-block",
        " iconfont icon-experiment",
        " iconfont icon-folder-add",
        " iconfont icon-right",
        " iconfont icon-Pound",
        " iconfont icon-diff",
        " iconfont icon-clouddownload-o",
        " iconfont icon-heatmap",
        " iconfont icon-font-size",
        " iconfont icon-barcode1",
        " iconfont icon-safetycertificate",
        " iconfont icon-arrowright",
        " iconfont icon-securityscan",
        " iconfont icon-meh",
        " iconfont icon-control",
        " iconfont icon-share",
        " iconfont icon-error",
        " iconfont icon-wallet",
        " iconfont icon-book",
        " iconfont icon-codelibrary",
        " iconfont icon-audit",
        " iconfont icon-car",
        " iconfont icon-issuesclose",
        " iconfont icon-database",
        " iconfont icon-pic-right",
        " iconfont icon-poweroff",
        " iconfont icon-doubleleft",
        " iconfont icon-like",
        " iconfont icon-italic",
        " iconfont icon-file-exclamation",
        " iconfont icon-bell",
        " iconfont icon-cluster",
        " iconfont icon-adduser",
        " iconfont icon-cloudserver-o",
        " iconfont icon-fork",
        " iconfont icon-doubleright",
        " iconfont icon-orderedlist",
        " iconfont icon-infomation",
        " iconfont icon-menu",
        " iconfont icon-laptop",
        " iconfont icon-flag",
        " iconfont icon-printer",
        " iconfont icon-setting",
        " iconfont icon-rise",
        " iconfont icon-shrink",
        " iconfont icon-phone",
        " iconfont icon-question",
        " iconfont icon-file-unknown",
        " iconfont icon-outdent",
        " iconfont icon-dashboard",
        " iconfont icon-pic-left",
        " iconfont icon-propertysafety",
        " iconfont icon-bold",
        " iconfont icon-Dollar",
        " iconfont icon-verticalleft",
        " iconfont icon-build",
        " iconfont icon-areachart",
        " iconfont icon-arrowdown",
        " iconfont icon-layout",
        " iconfont icon-select",
        " iconfont icon-filter",
        " iconfont icon-linechart",
        " iconfont icon-transaction",
        " iconfont icon-align-right",
        " iconfont icon-smile",
        " iconfont icon-sever",
        " iconfont icon-camera",
        " iconfont icon-monitor",
        " iconfont icon-save",
        " iconfont icon-skin",
        " iconfont icon-line",
        " iconfont icon-exclaimination",
        " iconfont icon-USB",
        " iconfont icon-cloudsync-o",
        " iconfont icon-contacts",
        " iconfont icon-reload",
        " iconfont icon-font-colors",
        " iconfont icon-arrowup",
        " iconfont icon-Import",
        " iconfont icon-scan",
        " iconfont icon-swap",
        " iconfont icon-star",
        " iconfont icon-unorderedlist",
        " iconfont icon-number",
        " iconfont icon-notification",
        " iconfont icon-key",
        " iconfont icon-barchart",
        " iconfont icon-wifi",
        " iconfont icon-redo",
        " iconfont icon-appstore",
        " iconfont icon-file-pdf",
        " iconfont icon-line-height",
        " iconfont icon-disconnect",
        " iconfont icon-detail",
        " iconfont icon-piechart",
        " iconfont icon-edit",
        " iconfont icon-medicinebox",
        " iconfont icon-small-dash",
        " iconfont icon-CI",
        " iconfont icon-calendar-check",
        " iconfont icon-up",
        " iconfont icon-align-left",
        " iconfont icon-sync",
        " iconfont icon-scissor",
        " iconfont icon-branches",
        " iconfont icon-export",
        " iconfont icon-strikethrough",
        " iconfont icon-colum-height",
        " iconfont icon-deleteteam",
        " iconfont icon-user",
        " iconfont icon-plus",
        " iconfont icon-upload",
        " iconfont icon-minus",
        " iconfont icon-team",
        " iconfont icon-barcode",
        " iconfont icon-radarchart",
        " iconfont icon-trademark",
        " iconfont icon-file-ppt",
        " iconfont icon-snippets",
        " iconfont icon-cloudupload-o",
        " iconfont icon-verticalright",
        " iconfont icon-arrawsalt",
        " iconfont icon-YUAN",
        " iconfont icon-file-copy",
        " iconfont icon-read",
        " iconfont icon-percentage",
        " iconfont icon-search",
        " iconfont icon-bank",
        " iconfont icon-fall",
        " iconfont icon-mobile",
        " iconfont icon-deleteuser",
        " iconfont icon-totop",
        " iconfont icon-calculator",
        " iconfont icon-EURO",
        " iconfont icon-creditcard",
        " iconfont icon-file-add",
        " iconfont icon-project",
        " iconfont icon-pushpin",
        " iconfont icon-cloud",
        " iconfont icon-column-width",
        " iconfont icon-fullscreen",
        " iconfont icon-file-markdown",
        " iconfont icon-arrowleft",
        " iconfont icon-carryout",
        " iconfont icon-underline",
        " iconfont icon-addteam",
        " iconfont icon-file-excel",
        " iconfont icon-reloadtime",
        " iconfont icon-container",
        " iconfont icon-gateway",
        " iconfont icon-undo",
        " iconfont icon-mr"
      ]
    };
  },
  watch: {
    value(n, o) {
      console.log(n);
      this.showIcon = n;
    }
  },
  props: ["value"],
  methods: {
    close() {
      this.$emit("input", false);
    },
    selectIcon(val) {
      this.$emit("GetValue", val);
      this.showIcon = false;
    }
  }
};
</script>
<style scoped>
i {
  font-size: 24px;
  margin-left: 16px;
  margin-top: 16px;
}
i:hover {
  border: 3px solid;
  border-color: #409eff;
}
</style>

