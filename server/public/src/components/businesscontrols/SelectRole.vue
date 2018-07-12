<template>
    <el-card class="box-card">
        <div v-if="IsShowTitle" slot="header" class="clearfix">
            <span>{{ShowTitle}}</span>
        </div>
        <el-table @row-click="GetSelectRole" 
        :show-header="ShowTableHead || false" 
        :data="Roles" 
        style="width: 100%">
             <el-table-column prop="label" label="角色名"/>
             <el-table-column prop="desc" label="备注"/>
             <el-table-column v-if="ShowSwitch" label="操作">
                <template scope="scope">
                <el-switch v-model="scope.row.selected" 
                            @change="chnageSwitch(scope.row)"/>
                </template>
             </el-table-column>
        </el-table>
    </el-card>
</template>
<script>
import axios from "axios";
import util from "../../common/util";
import NProgress from "nprogress";
import { mapGetters } from "vuex";
export default {
  props: ["ShowTitle", "ShowTableHead", "ShowSwitch", "Refash", "RefashBy"],
  watch: {
    Refash(n, o) {
      if (this.IsRefashBy != undefined) {
        this._IsRefashBy = n;
      }
      this.GetRoles();
    }
  },
  computed: {
    ...mapGetters(["getProj"]),
    IsShowTitle() {
      return this.ShowTitle || false;
    },
    IsShowOpen() {
      return this.ShowOpen || false;
    },
    IsRefashBy() {
      return this.RefashBy == undefined ? "" : `by${this.RefashBy}`;
    }
  },
  data() {
    return {
      Roles: [],
      showID: false,
      CurrentRole: {},
      _IsRefashBy: ""
    };
  },
  created() {
    this.GetRoles();
  },
  methods: {
    GetRoles() {
      let that = this;
      axios
        .get(
          `/api/GetRole${that.IsRefashBy}?ProjID=${that.getProj.ProjID}${
            this._IsRefashBy == undefined ? "" : `&MenuID=${this._IsRefashBy}`
          }`
        )
        .then(res => {
          if (res.data.success) {
            that.Roles = res.data.data;
          }
        })
        .catch(err => {});
    },
    GetSelectRole(val) {
      this.CurrentRole = this.Roles[val];
      this.$emit("GetSelectRole", this.CurrentRole);
    },
    chnageSwitch(row) {
      if (!this.ShowSwitch) return;
      let that = this;
      console.log(row.selected)
      axios
        .post(`/api/UpdateOrDel${that.IsRefashBy}`, {
          MenuID: that._IsRefashBy,
          ProjID: that.getProj.ProjID,
          RoleID: row.value,
          Selected: row.selected
        })
        .then(res => {})
        .catch(err => {});
    }
  }
};
</script>
<style scoped>
.el-table {
  border: 0;
}
</style>


