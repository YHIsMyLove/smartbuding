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
            <el-button type="text" class="button" style="float:right;color:black">更多</el-button>
        </div>
    </el-card>    
</template>
<script>
import moment from "moment";
export default {
  data() {
    return {
      Actions: [
        {
          User: "张三",
          Title: "XXXX会议",
          Time: moment().format("YYYY-MM-DD hh:mm")
        },
        {
          User: "张三",
          Title: "XXXX会议",
          Time: moment().format("YYYY-MM-DD hh:mm")
        },
        {
          User: "张三",
          Title: "XXXX会议",
          Time: moment().format("YYYY-MM-DD hh:mm")
        },
        {
          User: "张三",
          Title: "XXXX会议",
          Time: moment().format("YYYY-MM-DD hh:mm")
        },
        {
          User: "张三",
          Title: "XXXX会议",
          Time: moment().format("YYYY-MM-DD hh:mm")
        }
      ]
    };
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



