<template>
 <el-tabs style="width:100%;">
      <el-tab-pane label="会议内容管理">
            <section>
                <el-col :span="24" class="toolbar">
                    <el-form :inline="true"  class="demo-form-inline">
                        <el-form-item>
                            <el-select v-model="CurMetting" placeholder="请选择">
                                <el-option
                                    v-for="item in Metting_Options"
                                    :key="item._id"
                                    :label="item.MettingName"
                                    :value="item._id">
                                    <span style="float: left">{{ item.MettingName }}</span>
                                    <span style="float: right; color: #8492a6; font-size: 13px">主持人->{{ item.Compere }}</span>
                                </el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item>
                            <el-input  placeholder="查询内容"></el-input>
                        </el-form-item>
                        <el-form-item>
                            <el-button @click="Search">查询</el-button>
                        </el-form-item>
                        <el-form-item>
                            <el-button @click="Search" type="primary">新建内容</el-button>
                        </el-form-item>
                    </el-form>
                </el-col>

                <template>
                    <el-table border fit stripe :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">   
                        <el-table-column type="index" label="编号" width="85">
                        </el-table-column>
                        <el-table-column prop="MettingName" label="内容" >
                        </el-table-column>
                        <el-table-column width="150" prop="Compere" label="执行部门" >
                        </el-table-column>
                        <el-table-column label="操作" width="80">
                            <template scope="scope">
                                <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </template>

                <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
                    <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
                        :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength"
                        style="float:right">
                    </el-pagination>
                </el-col>

                <el-dialog title="新建会议" v-model="editFormVisible" :close-on-click-modal="true">
                    <el-form :model="editForm" label-width="80px" :rules="editFormRules" ref="editForm">
                        <el-form-item label="会议名称" prop="MettingName">
                            <el-input :disabled="editForm._id"  v-model="editForm.MettingName" auto-complete="off"></el-input>
                        </el-form-item>
                        <el-form-item label="会议时间" prop="MettingCreatedAt">
                            <el-date-picker style="width:100%" placeholder="选择日期时间" type="datetime" v-model="editForm.MettingCreatedAt"></el-date-picker>
                        </el-form-item>
                    </el-form>
                    <div slot="footer" class="dialog-footer">
                        <el-button @click="editFormVisible = false">取 消</el-button>
                        <el-button type="primary" :loading="editLoading">提交</el-button>
                    </div>
                </el-dialog>

            </section>
      </el-tab-pane>
 </el-tabs>
</template>
<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import { mapGetters } from "vuex";
import moment from "moment";
export default {
  computed: {
    ...mapGetters(["getProj"])
  },
  data() {
    return {
      editFormRules: [],
      Metting_Options: [],
      CurMetting: "",
      tableData: [],
      editForm: {},
      editFormVisible: false,
      listLoading: false,
      currentPageSize: 0,
      handleSizeChange: 0,
      currentPage: 0,
      tableDataLength: 0,
      editLoading: false
    };
  },
  created() {
    this.getMetting();
  },
  methods: {
    handleItemChange() {},
    handleCurrentChange() {},
    handleEdit() {},
    Search() {},
    //获取会议
    getMetting() {
      let that = this;
      let params = {
        item2: that.getProj
      };
      axios
        .get(`/api/GetMettings?ProjID=${that.getProj}`)
        .then(res => {
          if (res.data.success) {
            console.log(res.data.data);
            that.Metting_Options = res.data.data;
            if (that.Metting_Options.length > 0) {
              that.CurMetting = that.Metting_Options[0]._id;
            }
          }
        })
        .catch(err => console.log(err));
    },
    //获取会议内容
    getMettingMinutes() {
      if (!this.CurMetting) return
      
    }
  }
};
</script>

<style scoped>

</style>
