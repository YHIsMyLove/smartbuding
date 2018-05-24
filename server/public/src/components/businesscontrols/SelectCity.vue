<template>
<div>
    <el-select
        v-model="selecter.value"
        placeholder="请选择城市"
    >
        <el-option
            v-for="item in selecter.datas"
            :key="item.value"
            :label="item.label"
            :value="item.value"
        >
            </el-option>
            </el-select>
</div>
</template>


<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
import { mapGetters, mapActions } from "vuex";
export default {
  computed: {
    CurrentProvID() {
      return this.$store.state.Proj.ProvID;
    }
  },
  watch: {
    CurrentProvID(val) {
      this.GetCitys();
    }
  },
  methods: {
    ...mapActions(["setCity"]),
    GetCitys() {
      let that = this;
      axios
        .get(`/api/GetCityByProvID?ProvID=${that.CurrentProvID}`)
        .then(res => {
          if (res.data.success) {
            var data = res.data.data.map(i => {
              return {
                value: i._id,
                label: i.Name
              };
            });
            that.selecter.datas = data;
            if (data.length > 0) {
              that.selecter.value = data[0].value;
              that.setCity(that.selecter.value);
              this.$emit("ChangeCity", that.selecter.value);
            }
          } else {
            that.$message({
              type: "error",
              message: "获取城市失败!请刷新重试"
            });
          }
        })
        .catch(err => console.log(err));
    }
  },
  data() {
    return {
      selecter: {
        datas: [],
        value: ""
      }
    };
  }
};
</script>
