<template>
    <el-col :span="24" class="toolbar">
        <el-form class="demo-form-inline">
            <el-button @click="newLine" type="primary">新增行</el-button>
            <el-button @click="getTabelData" type="primary">刷新</el-button>
        </el-form>
        <el-tabs style="width:100%;">

            <el-table border fit stripe :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">  
                <div style="width:100%;height:100%" v-for="(item,index) in EditLine" :key="index">
                    <el-table-column :prop="item.Title" :label="item.Title">
                    </el-table-column>
                </div>
                <el-table-column label="操作" width="150">
                    <template scope="scope">
                        <!-- <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button> -->
                        <el-button type="text" size="small" @click="handleDel(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-tabs>

        <el-dialog v-model="editFormVisible" title="新增行">
            <div v-for="(item,index) in EditLine" :key="index">
                <el-col :span='4'>
                    {{item.Title}}
                </el-col>
                <el-col :span='20' v-if="item.Link=='空'">
                    <el-input v-model="item.Value"></el-input>
                </el-col>
                <el-col :span='20' v-else="item.Link!='空'">
                    <el-select style="width:100%" v-model="item.Value" filterable placeholder="请输入关键词" :loading="loading">
                        <el-option v-for="item2 in item.ItemLinks" :key="item2._id" :label="item2._id" :value="item2._id">
                            <div v-for="(item3,index) in Object.keys(item2)" :key="index">
                                <span v-if="(item3!='SysFieldID'&&item3!='_id')&&index%2==0" style="float: left;">{{ item2[item3] }}</span>
                                <span v-else-if="(item3!='SysFieldID'&&item3!='_id')&&index%2!=0" style="float: right; color: #8492a6; ">{{ item2[item3] }}
                                    <span v-if="index!=Object.keys(item2).length-1"> &nbsp;|&nbsp; </span>
                                </span>
                            </div>
                        </el-option>
                    </el-select>
                </el-col>
                &nbsp;
            </div>
            <div slot="footer" class="dialog-footer">
                <el-button @click="cancelNewline">取消</el-button>
                <el-button @click="postNewline" type="primary">提交</el-button>
            </div>
        </el-dialog>

    </el-col>
</template>

<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import moment from "moment";
export default {
  data() {
    return {
      EditLine: [
        {
          Title: "",
          Desc: "",
          Type: "String",
          Link: "",
          Value: "",
          ItemLinks: []
        }
      ],
      listLoading: false,
      editFormVisible: false,
      tableData: [],
      tableDataLength: 0,
      list: [],
      loading: false
    };
  },
  props: ["Field", "TabelName", "TableDesc"],
  methods: {
    //获取字段数据
    getFieldInfo() {
      try {
        if (!this.Field) return;
        var SysFieldInfo = JSON.parse(this.Field.SysFieldInfo);
        this.EditLine.splice(0, this.EditLine.length);
        Object.keys(SysFieldInfo).forEach(i => {
          var line = {
            Title: i,
            Desc: SysFieldInfo[i].desc,
            Type: SysFieldInfo[i].type,
            Link: SysFieldInfo[i].link
          };
          this.EditLine.push(line);
          if (SysFieldInfo[i].link !== "空") {
            switch (SysFieldInfo[i].link) {
              case "用户表":
                break;
              default:
                axios
                  .get(`/api/SysTable?SysFieldID=${SysFieldInfo[i].link}`)
                  .then(
                    function(res) {
                      line.ItemLinks = res.data.data;
                      console.log(line);
                    }.bind(line)
                  );
                break;
            }
          }
        });
      } catch (error) {
        console.log(error);
      }
    },
    //获取tabel的数据 api/SysTable
    getTabelData() {
      var vm = this;
      var params = {
        limit: vm.$data.currentPageSize,
        page: vm.$data.currentPage,
        SysFieldID: vm.TabelName
      };
      axios.get("/api/SysTable/", { params: params }).then(function(res) {
        vm.$data.tableData = res.data.data;
        vm.$data.tableDataLength = res.data.meta.count;
      });
    },
    handleEdit(row) {},
    //删除系统表内容
    handleDel(row) {
      this.$confirm("确认删除该记录吗?", "提示", {
        type: "warning"
      }).then(() => {
        var that = this;
        var jsonstr = `{"_id":"${row._id}"}`;
        var query = JSON.parse(jsonstr);
        axios
          .post("/api/SysTable/delete", query)
          .then(res => {
            console.log(res.data)
            if (res.data.success) {
              that.getTabelData();
            }
          })
          .catch(err => console.log(err));
      });
    },
    newLine() {
      this.editFormVisible = true;
    },
    cancelNewline() {
      this.editFormVisible = false;
    },
    //上传数据自定义字段数据
    postNewline() {
      let savedata = `{"SysFieldID":"${this.TabelName}",`;
      let index = 0;
      this.EditLine.forEach(i => {
        savedata += `"item${index}":"${i.Value}",`;
        index++;
      });
      savedata = savedata.substr(0, savedata.length - 1) + "}";
      console.log("保存" + savedata);
      axios
        .post("/api/SysTable/create", JSON.parse(savedata))
        .then((req, err) => {
          if (err) {
            this.$message({
              message: `保存失败：${err}`,
              type: "error"
            });
            return;
          }
          if (!req.data.success) {
            this.$message({
              message: `保存失败：${req.data.msg}`,
              type: "error"
            });
            return;
          }
          this.$message({
            message: `保存成功`,
            type: "success"
          });
        });
      this.editFormVisible = false;
      this.getTabelData();
      this.getFieldInfo();
    }
  },
  watch: {
    Field(curVal, oldVal) {
      this.getFieldInfo();
    },
    TabelName(curVal, oldVal) {
      this.getTabelData();
    }
  }
};
</script>

<style scoped>
</style>