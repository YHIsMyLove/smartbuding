<template>
    <el-card shadow="always" class="main">
        <el-badge v-for="(item,index) in Links" :key="index" :value="item.BadgeCount" class="item badgeItem">
            <template class="templateitem">
                <div><el-button @click="LinkTo(index)" class="LinkItem" type="primary" icon="el-icon-delete" /></div>
                <div class="fontitem"> {{item.Name}}</div>
            </template>
        </el-badge>
    </el-card>    
</template>

<script>
export default {
  data() {
    return {
      Links: [
        { Name: "考勤记录", BadgeCount: 10, Link: "" },
        { Name: "特种审批", BadgeCount: 11, Link: "" },
        { Name: "日历事件", BadgeCount: 2, Link: "" },
        { Name: "设备报警", BadgeCount: 3, Link: "" },
        { Name: "系统公告", BadgeCount: 5, Link: "" },
        { Name: "排行榜", BadgeCount: 0, Link: "" }
      ]
    };
  },
  methods: {
    LinkTo(index) {
      console.log(this.Links[index].Name);
    }
  }
};
</script>
<style scoped>
.badgeItem {
  width: 80px;
  height: 60px;
}
.LinkItem {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  margin-left: 44px;
}
.fontitem {
  width: 80px;
  margin-left: 22px;
  text-align: center;
}
</style>


