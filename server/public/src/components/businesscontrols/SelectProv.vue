<template>
<div>
    <el-select
        v-model="selecter.value"
        placeholder="请选择省份"
        @change="SelectChange"
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
  props: ["refdata"],
  computed: {
    ...mapGetters(["getUser"])
  },
  created() {
    this.GetProvs();
  },
  watch: {
    refdata(old) {}
  },
  methods: {
    ...mapActions(["setProv"]),
    SelectChange() {
      this.setProv(this.selecter.value);
    },
    GetProvs() {
      let that = this;
      axios
        .get(`/api/GetProvByUser?UserID=${that.getUser.UserID}`)
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
              this.selecter.value = data[0].value;
              that.setProv(this.selecter.value);
            }
          } else {
            that.$message({
              type: "error",
              message: "获取省份失败!请刷新重试"
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
