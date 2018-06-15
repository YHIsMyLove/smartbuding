<template>
    <f7-page>
        <f7-navbar title="考勤信息" back-link="Back" :no-shadow="true"></f7-navbar>
        <f7-toolbar>
            <div class="display-flex justify-content-center align-items-center" style="width:100%">
                <input type="date" v-model="selectedDate"/>
            </div>
        </f7-toolbar>
        <div id="attendance-info-head" class="display-flex align-items-center padding">
            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
            <div style="margin-left: 8px;" class="font-middle">张三</div>
            <div style="flex: 1 1 0"></div>
            <div class="text-align-right">共记时长：600(分钟)</div>
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
    </f7-page>
</template>
<script>
export default {
  data() {
    return {
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
.md .toolbar:after {
  height: 1px;
  background-color: #ccc;
}

.md .toolbar {
  background-color: #fff;
  color: rgba(0, 0, 0);
}
#attendance-info-head {
  background-color: #fafafa;
}
</style>
