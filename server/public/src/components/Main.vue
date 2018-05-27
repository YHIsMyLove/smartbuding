<template>
  <section class="main">
    <el-row>
      <el-col :span="24">
        <HomeRoutesLink/>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <HomeStatistical/>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <HomeApprove/>
      </el-col>
      <el-col :span="12" >
        <HomeRankingList/>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="14">
        <HomeActions/>
      </el-col>
      <el-col :span="10">
        <!-- <HomeWeather/> -->
        <test/>
      </el-col>
    </el-row>
  </section>
</template>

<script>
import HomeRoutesLink from "./layout/components/HomeRoutesLink.vue";
import HomeStatistical from "./layout/components/HomeStatistical.vue";
import HomeApprove from "./layout/components/HomeApprove.vue";
import HomeActions from "./layout/components/HomeActions.vue";
import HomeWeather from "./layout/components/HomeWeather.vue";
import HomeRankingList from "./layout/components/HomeRankingList.vue";
import test from "./layout/components/test.vue";
export default {
  components: {
    HomeRoutesLink,
    HomeStatistical,
    HomeApprove,
    HomeActions,
    HomeWeather,
    HomeRankingList,
    test
  },
  data() {
    return {};
  },
  created() {}
};
</script>