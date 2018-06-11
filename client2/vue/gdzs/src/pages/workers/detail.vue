<template>
    <f7-page tabs :page-content="false">
        <f7-navbar title="信息" back-link="Back"></f7-navbar>
        <f7-page-content id="base-info" tab tab-active>
            <div id="base-info-head" class="padding">
                <f7-row>
                    <f7-col width="100">
                        <img id="head-img" src="static/imgs/个人信息头像180x180.png" alt="">
                    </f7-col>
                    <f7-col width="100">
                        <span>{{contact.UserName}}</span>
                    </f7-col>
                    <f7-col width="100">
                        <span>四川省羽玲建筑劳务有限公司</span>
                    </f7-col>
                </f7-row>
                <div class="display-flex justify-content-center my-buttons normal-buttons">
                    <f7-link>
                        <img src="static/imgs/信息100x100.png" alt="">
                        <div>信息</div>
                    </f7-link>
                    <f7-link>
                        <img src="static/imgs/通话100x100.png" alt="">
                        <div>呼叫</div>
                    </f7-link>
                    <f7-link>
                        <img src="static/imgs/人员定位100x100.png" alt="">
                        <div>定位</div>
                    </f7-link>
                </div>
            </div>
            <div id="base-info-center">
                <div class="my-divider"></div>
                <f7-row>
                    <f7-col width="50">
                        <div>部门</div>
                        <div>工程部</div>
                    </f7-col>
                    <f7-col width="50">
                        <div>班组</div>
                        <div>打桩队</div>
                    </f7-col>
                </f7-row>
                <div class="my-divider"></div>
                <f7-row>
                    <f7-col width="50">
                        <div>工种</div>
                        <div>电焊工</div>
                    </f7-col>
                    <f7-col width="50">
                        <div>工号</div>
                        <div>007</div>
                    </f7-col>
                </f7-row>
                <div class="my-divider"></div>
                <f7-row>
                    <f7-col width="50">
                        <div>身份证</div>
                        <div>420223199009129878</div>
                    </f7-col>
                    <f7-col width="50">
                        <div>手机</div>
                        <div>{{contact.UserPhoneNum}}</div>
                    </f7-col>
                </f7-row>
                <div class="my-divider"></div>
                <f7-row>
                    <f7-col width="100">资格证书</f7-col>
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
            </div>
        </f7-page-content>
        <f7-page-content id="attendance-info" tab>
            <div id="attendance-info-head" class="padding">
                <div class="display-flex justify-content-space-between align-items-center">
                    <div class="">
                        <input type="text" readonly="readonly" id="select-date" value="2018-05-26"/>
                    </div>
                    <div class="">
                        <div class="display-flex align-items-center">
                            <img id="head-img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <div>张三</div>
                        </div>
                    </div>
                </div>
                <div class="display-flex justify-content-space-between align-items-center">
                    <div class="">单位：四川羽灵建筑劳务有限公司</div>
                    <div class="">工号：007</div>
                </div>
                <div class="display-flex justify-content-space-between">
                    <div class="">
                        <div>班组：打桩队</div>
                        <div>工种：电焊工</div>
                    </div>
                    <div class="">共记时长：600（分钟）</div>
                </div>
            </div>
            <div>
                <f7-list media-list>
                    <f7-list-item :key="key" v-for="(item, key) in dailyList" :title="item.date">
                        <div slot="after">时长：{{item.time}}（分钟）</div>
                        <div slot="footer">
                            <f7-row>
                                <f7-col width="50">
                                    <div>进场时间：{{item.timeIn}}</div>
                                </f7-col>
                                <f7-col width="50">
                                    <div class="text-align-right">出场时间：{{item.timeOut}}</div>
                                </f7-col>
                            </f7-row>
                        </div>
                    </f7-list-item>
                </f7-list>
            </div>
        </f7-page-content>
        <f7-toolbar tabbar>
            <f7-link text="基本信息" tab-link="#base-info"></f7-link>
            <f7-link text="考勤信息" tab-link="#attendance-info"></f7-link>
        </f7-toolbar>
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
      dailyList: [
        { date: "2018/05/25", timeIn: "08:23", timeOut: "15:30", time: 202 },
        { date: "2018/05/25", timeIn: "08:23", timeOut: "15:30", time: 202 },
        { date: "2018/05/25", timeIn: "08:23", timeOut: "15:30", time: 202 }
      ]
    };
  }
};
</script>
<style scoped>
#head-img {
  width: 96px;
}
#head-img-little {
  width: 32px;
}
#base-info-head {
  background-color: #fafafa;
  text-align: center;
}
#attendance-info-head {
  background-color: #fafafa;
}
#base-info-center div[class*="row"] {
  padding: 16px;
}
.my-buttons a {
  margin: 0 10px;
}
</style>

