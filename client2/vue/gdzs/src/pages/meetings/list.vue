<template>
    <f7-page>
        <f7-navbar title="日历事件" back-link="Back" :no-shadow="true">
          <f7-nav-right>
            <f7-link icon-f7="search_strong" @click="enableSearchbar"></f7-link>
          </f7-nav-right>
          <f7-searchbar 
            :expandable='true'
            id="searchbar"
            search-container="#listMeeting"
            placeholder="请输入关键字查询"
            @searchbar:enable="onEnable"
            @searchbar:disable="onDisable"></f7-searchbar>
        </f7-navbar>
        <f7-toolbar>
          <div class="display-flex justify-content-center align-items-center" style="width:100%">
            <input type="date" v-model="selectedDate"/>
          </div>
        </f7-toolbar>
        <!-- <f7-list>
            <f7-list-group :key="key1" v-for="(group, key1) in meetings">
                <f7-list-item :title="key1" group-title></f7-list-item>
                <f7-list-item :key="key2" v-for="(meeting, key2) in group" :title="meeting.name" link="/meetings/detail/" :after="meeting.date" :badge="meeting.noReading" badge-color="red"></f7-list-item>
            </f7-list-group>
        </f7-list> -->
        <f7-list class="searchbar-not-found">
          <f7-list-item title="没有数据"></f7-list-item>
        </f7-list>
        <f7-list id="listMeeting">
          <f7-list-item v-for="(meeting, key) in meetings" :key="key" :title="meeting.MeetingName" :after="meeting.MeetingCreatedAt" link @click="showDetail(meeting, $event)">
            <div slot="after-end" class="padding-left"><f7-badge v-if="meeting.RelationalCount != 0" color="red">{{meeting.RelationalCount}}</f7-badge></div>
          </f7-list-item>
        </f7-list>
    </f7-page>
</template>
<script>
import context from '../../service-context.js';
export default {
  on: {
    pageInit(event, pageData) {
      console.log("meeting list page init");
      this.fetchMeetings();
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
      meetings: {
        "2018年3月": [
          { name: "三月项目成本核算会议", date: "2018/03/12", noReading: 2 },
          { name: "三月项目成本核算会议", date: "2018/03/06", noReading: 2 },
          { name: "三月项目成本核算会议", date: "2018/03/01", noReading: 2 }
        ],
        "2018年2月": [
          { name: "三月项目成本核算会议", date: "2018/02/06", noReading: 2 },
          { name: "三月项目成本核算会议", date: "2018/02/01", noReading: 2 },
          { name: "三月项目成本核算会议", date: "2018/02/24", noReading: 2 }
        ]
      },
      searchbarDisabled: false
    };
  },
  methods: {
    fetchMeetings() {
      var that = this;
      var preloader = that.$f7.dialog.preloader("请稍后。。。");
      var url = context.urls.getMeetingsUrl.format(context.currentUser._id, context.currentProject._id);
      console.log(`request url ${url}`);
      that.$f7.request.get(url, function(data){
        console.log(`response: ${JSON.stringify(data)}`);
        preloader.close(true);
        if (data.success)
        {
          that.meetings = data.data;
          // var map = new Map();
          // data.data.forEach(element => {
          //   var date = new Date(element.MeetingCreatedAt);
          //   var year = date.getFullYear();
          //   var month = date.getMonth();
          //   var day = date.getDay();
          //   var group = `${year}年${month}月${day}日`;
          //   if (map.has(group))
          //   {
          //     map.get(group).push(element);
          //   }
          //   else
          //   {
          //     var items = [element];
          //     map.set(group, items);
          //   }
          // });
          // that.meetings = JSON.stringify(map);
          // console.log(that.meetings);
        }
        else{
          console.error(data.msg);
          that.$f7.dialog.alert(data.msg);
        }
      }, function(xhr, status){
        preloader.close(true);
        console.error(`error ${status}`);
        that.$f7.dialog.alert("网络忙，请稍后重试！");
      },'json');
    },
    showDetail(meeting, event){
      console.log('selected meeting: ' + meeting);
      context.currentMeeting = meeting;
      this.$f7.views.main.router.navigate({url:'/meetings/detail/?meetingId=' + meeting._id});
      this.$f7.searchbar.disable('#searchbar');
    },
    enableSearchbar(){
      this.$f7.searchbar.enable('#searchbar');
    },
    onEnable: function (event) {
      this.searchbarDisabled = false;
    },
    onDisable: function (event) {
      this.searchbarDisabled = true;
    }
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
</style>


