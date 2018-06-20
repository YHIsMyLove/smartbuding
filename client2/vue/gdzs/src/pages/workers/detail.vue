<template>
    <f7-page>
        <f7-navbar title="信息" back-link="Back" :no-shadow="true"></f7-navbar>
        <div id="base-info-head" class="padding">
            <f7-row>
                <f7-col width="100">
                    <img id="head-img" src="static/imgs/个人信息头像180x180.png" alt="">
                </f7-col>
                <f7-col width="100">
                    <div class="font-middle">{{contact.UserName}}</div>
                </f7-col>
                <f7-col width="100">
                    <f7-label>四川省羽玲建筑劳务有限公司</f7-label>
                </f7-col>
            </f7-row>
            <div class="padding-top display-flex justify-content-center my-buttons normal-buttons">
                <f7-link>
                    <img src="static/imgs/信息100x100.png" alt="">
                    <div>信息</div>
                </f7-link>
                <f7-link @click="call">
                    <img src="static/imgs/通话100x100.png" alt="">
                    <div>呼叫</div>
                </f7-link>
                <f7-link href="/workers/location/">
                    <img src="static/imgs/人员定位100x100.png" alt="">
                    <div>定位</div>
                </f7-link>
            </div>
        </div>
        <div id="base-info-center">
            <div class="my-divider"></div>
            <f7-row>
                <f7-col width="50">
                    <f7-label>部门</f7-label>
                    <div>工程部</div>
                </f7-col>
                <f7-col width="50">
                    <f7-label>班组</f7-label>
                    <div>打桩队</div>
                </f7-col>
            </f7-row>
            <div class="my-divider"></div>
            <f7-row>
                <f7-col width="50">
                    <f7-label>工种</f7-label>
                    <div>电焊工</div>
                </f7-col>
                <f7-col width="50">
                    <f7-label>工号</f7-label>
                    <div>007</div>
                </f7-col>
            </f7-row>
            <div class="my-divider"></div>
            <f7-row>
                <f7-col width="50">
                    <f7-label>身份证</f7-label>
                    <div>420223199009129878</div>
                </f7-col>
                <f7-col width="50">
                    <f7-label>手机</f7-label>
                    <div>{{contact.UserPhoneNum}}</div>
                </f7-col>
            </f7-row>
            <div class="my-divider"></div>
            <f7-row>
                <f7-col width="100"><f7-label>资格证书</f7-label></f7-col>
                <f7-col width="100">
                    <f7-swiper pagination>
                        <f7-swiper-slide>
                            <img src="static/imgs/安全防护70x70.png" alt="">
                        </f7-swiper-slide>
                        <f7-swiper-slide>
                            <img src="static/imgs/安全防护70x70.png" alt="">
                        </f7-swiper-slide>
                        <f7-swiper-slide>
                            <img src="static/imgs/安全防护70x70.png" alt="">
                        </f7-swiper-slide>
                    </f7-swiper>
                </f7-col>
            </f7-row>
            <f7-list>
                <f7-list-item title="查看考勤" link="/workers/detail/attendance/"></f7-list-item>
            </f7-list>
        </div>
    </f7-page>
</template>
<script>
import context from "../../service-context.js";
export default {
  on: {
    pageInit(event, pageData) {
      console.log("contact detail page init");
      console.log("contact: " + context.currentContact);
      this.contact = context.currentContact;
    },
    pageAfterIn(event, pageData) {
      console.log("contact detail page after in");
    },
    pageAfterOut(event, pageData) {
      //alert("page after out");
    }
  },
  data() {
    return {
      contact: {},
    };
  },
  methods: {
      call() {
          cordova.plugins.PhoneCaller.call(this.contact.UserPhoneNum);
      }
  }
};
</script>
<style scoped>
#head-img {
  width: 64px;
}
#base-info-head {
  background-color: #fafafa;
  text-align: center;
}
#base-info-center div[class*="row"] {
  padding: 16px;
}
.my-buttons a {
  margin: 0 10px;
  color: black;
}
.md .toolbar:after {
  height: 0px;
}
div.list {
    margin: 0px;
}
</style>

