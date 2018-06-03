<template>
  <section class="main">
    <div class="left-body">
      <el-col :span="24">
          <HomeRoutesLink/>
      </el-col>
      <el-col :span="24">
          <HomeStatistical/>
      </el-col>
      <el-col :span="14">
          <HomeApprove/>
<<<<<<< HEAD
      </el-col>
      <el-col :span="10">
          <HomeRankingList/>
      </el-col>
      <el-col :span="16">
          <HomeActions/>
      </el-col>
      <el-col :span="8">
          <HomeWeather/>
      </el-col>
=======
      </el-col>
      <el-col :span="10">
          <HomeRankingList/>
      </el-col>
      <el-col :span="16">
          <HomeActions/>
      </el-col>
      <el-col :span="8">
          <HomeWeather/>
      </el-col>
>>>>>>> 72bef902ba622ae0f489c5b3a7807f336d63243e
    </div>
    <div class="right-body">
      <el-col :span="24">
        <HomeDefult/>
<<<<<<< HEAD
      </el-col>
      <el-col :span="24">
        <MenuUser/>
        <UploadImage :width=64
                      v-model="tmpImage" 
                      :circle="true" @UpLoadOver="UpLoadOver"/>
        <SelectUser @SelectedUser="SelectedUser"/>
        <ProjMenu :ShowTitle="true" @SelectProjChange="SelectProjChange"/>
      </el-col>
=======
      </el-col>
      <el-col :span="24">
        <MenuUser/>
      </el-col>
>>>>>>> 72bef902ba622ae0f489c5b3a7807f336d63243e
    </div>
    
  </section>
</template>

<script>
import HomeRoutesLink from "./layout/components/HomeRoutesLink.vue";
import HomeStatistical from "./layout/components/HomeStatistical.vue";
import HomeApprove from "./layout/components/HomeApprove.vue";
import HomeActions from "./layout/components/HomeActions.vue";
import HomeWeather from "./layout/components/HomeWeather.vue";
import HomeRankingList from "./layout/components/HomeRankingList.vue";
import HomeDefult from "./layout/components/HomeDefult.vue";
import MenuUser from "./businesscontrols/MenuUser.vue";
<<<<<<< HEAD
import SelectUser from "./businesscontrols/SelectUser.vue";
import ProjMenu from "./businesscontrols/ProjMenu.vue";
=======
>>>>>>> 72bef902ba622ae0f489c5b3a7807f336d63243e
export default {
  components: {
    HomeRoutesLink,
    HomeStatistical,
    HomeApprove,
    HomeActions,
    HomeWeather,
    HomeRankingList,
<<<<<<< HEAD
    HomeDefult,
    MenuUser,
    // UploadImage,
    SelectUser,
    ProjMenu
  },
  data() {
    return {
      tmpImage:""
        // "http://justtest-1251712170.file.myqcloud.com/static/xiejie_N_x150.png"
    };
  },
  methods: {
    SelectedUser(user) {
      console.log(user);
    },
    UpLoadOver(url) {
      console.log(url);
    },
    SelectProjChange(val) {
      console.log(val);
    }
  },
=======
    HomeDefult,MenuUser
  },
  data() {
    return {};
  },
>>>>>>> 72bef902ba622ae0f489c5b3a7807f336d63243e
  created() {}
};
</script>
<style scoped>
.left-body {
  width: 70%;
  float: left;
}
.right-body {
  width: 30%;
  float: right;
}
</style>
