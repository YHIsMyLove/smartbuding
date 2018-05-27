<template>
    <f7-page>
        <f7-navbar title="日历事件" back-link="Back"></f7-navbar>
        <f7-list>
            <f7-list-group :key="key1" v-for="(group, key1) in meetings">
                <f7-list-item :title="key1" group-title></f7-list-item>
                <f7-list-item :key="key2" v-for="(meeting, key2) in group" :title="meeting.name" link="/meetings/detail/" :after="meeting.date" :badge="meeting.noReading" badge-color="red"></f7-list-item>
            </f7-list-group>
        </f7-list>
    </f7-page>
</template>
<script>
export default {
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
      }
    };
  }
};
</script>

