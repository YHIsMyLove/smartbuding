<template>
    <el-card shadow="always" class="main">
        <div slot="header" class="clearfix">
            <span>日历事件</span>
        </div>
        <el-menu>
            <el-menu-item v-for="(item,index) in Actions" :index="index+''" :key="index">
                <el-col :span="8">
                    <span style="color:#409EFF">{{item.User}}</span>
                </el-col>
                <el-col :span="8">
                    <b>{{item.Title}}</b>
                </el-col>
                <el-col :span="8">
                    <span style="color:#909399">{{item.Time}}</span>
                </el-col>
            </el-menu-item>
        </el-menu>
        <div class="bottom clearfix">
            <el-button @click="link2Actions" type="text" class="button" style="float:right;color:black">更多</el-button>
        </div>
    </el-card>    
</template>
<script>
import moment from "moment";
import util from "../../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters(["getProj"])
  },
  data() {
    return {
      Actions: [
      ]
    };
  },
  created() {
    this.getMettings();
  },
  methods: {
    link2Actions() {
      this.$router.push({ path: "/Meeting" });
    },
    getMettings() {
      let that = this;
      axios
        .get(`/api/GetMeetings?ProjID=${that.getProj.ProjID}&limit=5`)
        .then(res => {
          if (res.data.success) {
            var data = res.data.data.map(i => {
              return {
                User: i.Compere,
                Title: i.MeetingName,
                Time: i.MeetingCreatedAt
              };
            });
            that.Actions = data;
          }
        })
        .catch(err => console.log(err));
    }
  }
};
</script>
<style scoped>
.main {
  margin-top: 15px;
  height: 400px;
}

.el-menu-item.is-active {
  color: #48576a;
}
.el-menu {
  background-color: #fff;
}
.el-col {
  border-bottom: 1px solid;
  border-bottom-color: #c0c4cc;
}
</style>



