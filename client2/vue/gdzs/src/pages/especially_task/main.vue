<template>
    <f7-page tabs :page-content="false">
        <f7-navbar title="特种作业审批" back-link="Back" :no-shadow="true">
            <f7-nav-right>
                <f7-link icon-if-ios="f7:menu" icon-if-md="material:add" popover-open="#popover"></f7-link>
                <f7-popover id="popover">
                    <f7-list>
                        <f7-button :key="index" v-for="(btn, index) in buttons" :text="btn.text" :popover-close="true" :href="btn.url"></f7-button>
                    </f7-list>
                </f7-popover>
            </f7-nav-right>
        </f7-navbar>
        <f7-page-content id="create-by-me" tab tab-active>
            <f7-list media-list>
                <f7-list-item link="/especially-task/check-dhzy/">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <div style="margin-left: 8px;">张三</div>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div style="margin-left: 8px;">一级动火作业申请</div>
                    </div>
                </f7-list-item>
                <f7-list-item link="/especially-task/check-dhzy/">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <div style="margin-left: 8px;">张三</div>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#f00">已拒绝</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div style="margin-left: 8px;">一级动火作业申请</div>
                    </div>
                </f7-list-item>
            </f7-list>
        </f7-page-content>
        <f7-page-content id="check-by-me" tab>
            <f7-list media-list>
                <f7-list-item link="/especially-task/check-dhzy/">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <div style="margin-left: 8px;">张三</div>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div style="margin-left: 8px;">一级动火作业申请</div>
                    </div>
                </f7-list-item>
                <f7-list-item link="/especially-task/check-dhzy/">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <div style="margin-left: 8px;">张三</div>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#f00">已拒绝</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div style="margin-left: 8px;">一级动火作业申请</div>
                    </div>
                </f7-list-item>
            </f7-list>
        </f7-page-content>
        <f7-page-content id="send-to-me" tab>
            <f7-list media-list>
                <f7-list-item link="/especially-task/check-dhzy/">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <div style="margin-left: 8px;">张三</div>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div style="margin-left: 8px;">一级动火作业申请</div>
                    </div>
                </f7-list-item>
                <f7-list-item link="/especially-task/check-dhzy/">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <div style="margin-left: 8px;">张三</div>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#f00">已拒绝</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div style="margin-left: 8px;">一级动火作业申请</div>
                    </div>
                </f7-list-item>
            </f7-list>
        </f7-page-content>
        <f7-toolbar tabbar>
            <f7-link text="我发起的" tab-link="#create-by-me"></f7-link>
            <f7-link text="我审批的" tab-link="#check-by-me"></f7-link>
            <f7-link text="抄送我的" tab-link="#send-to-me"></f7-link>
        </f7-toolbar>
    </f7-page>
</template>
<script>
export default {
  data() {
    return {
      buttons: [
        { text: "申请动火作业审批", url: "/especially-task/create-dhzy/" },
        { text: "申请受限作业审批", url: "#" },
        { text: "申请起重吊装作业审批", url: "#" },
        { text: "申请高处作业审批", url: "#" },
        { text: "申请脚手架拆卸作业审批", url: "#" },
        { text: "申请安全防护拆除作业审批", url: "#" }
      ]
    };
  },
  methods: {
    onBtnClick(text) {
      console.log(text);
    }
  }
};
</script>
<style scoped>
a[class*="button"] {
  text-align: left;
  color: #000;
  /* border-top: 1px solid #ccc; */
}
.md .toolbar::after {
    height: 0;
}
</style>


