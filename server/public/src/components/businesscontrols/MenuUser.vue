<template>
    <div class="main">
        <el-col :span="8">
            <img class="headimg" src="../../assets/user.png" alt="UserHeadImage">
        </el-col>
        <el-col :span="8">
            <div class="userinfo">
                <b style="color:#DCDFE6">{{UserData.Name}}</b>
                <br>
                <span class="userpost">{{UserData.Post}}</span>
            </div>
        </el-col>
        <el-col :span="8">
            <div class="userinfo">
                <el-tag type="success">
                    {{UserData.Role}}
                </el-tag>
            </div>
        </el-col>
        <el-col :span="24">
            <hr style="margin-top: 15px;height:.5px;border:none;border-top:1px solid #ffff;" />
        </el-col>
    </div>    
</template>
<script>
import { mapGetters, mapActions } from "vuex";
export default {
  computed: {
    ...mapGetters(["getProj"]),
    UserData() {
      return {
        Name: this.$store.state.User.UserName,
        Role: "管理员",
        Post: "经理"
      };
    }
  },
  data() {
    return {};
  }
};
</script>

<style scoped>
.main {
  margin-top: 15px;
  margin-left: 5px;
  height: 80px;
  width: 230px;
  background: #324057;
}
.headimg {
  border-radius: 50%;
  width: 36px;
  height: 36px; 
  margin-left: 9px;
  margin-top: 9px;
}
.userinfo {
  margin-left: 9px;
  margin-top: 9px;
}
.userpost {
  color: #ebeef5;
  font-size: 11px;
}
</style>


