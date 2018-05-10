<template>
    <el-tabs v-model="activeName" style="width:100%;" @tab-click="handleClick">
        <el-tab-pane label="用户配置" name='userManager'>
          <el-row>
            <el-col :span="8">
              <el-card class="box-card">
                <div slot="header" class="clearfix">
                  <span>用户信息</span>
                </div>
                <el-form  label-width="80px">
                  <el-form-item label="当前区域">
                    {{getProj}}
                  </el-form-item>
                  <el-form-item label="角色">
                      <el-tag>测试用户</el-tag>
                      <el-tag>管理员</el-tag>
                  </el-form-item>
                  <el-form-item label="选择区域">
                      <el-cascader :options="prov_city_options" change-on-select @change="handleItemChange"></el-cascader>
                  </el-form-item>
                </el-form>
              </el-card>
            </el-col>
            <el-col :span="8"> 
              <el-card class="boox-card">
                 <div slot="header" class="clearfix">
                  <span>用户配置</span>
                </div>

              </el-card>
            </el-col>
          </el-row>
         
        </el-tab-pane>
        <el-tab-pane name="BusinessManager" label="自定义表管理">
            <section>
                <el-col :span="24" class="toolbar">
                    <el-form :inline="true" :model="formInline" class="demo-form-inline">
                        <el-form-item>
                            <el-input v-model="formInline.user" placeholder="表名"></el-input>
                        </el-form-item>
                        <el-form-item>
                            <el-button @click='getSysFieldList'>查询</el-button>
                            <el-button @click="newCustomTable">新增自定义表</el-button>
                        </el-form-item>
                    </el-form>
                </el-col>
                <template>
                    <el-table border fit stripe :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">   
                        <el-table-column label="编号" type="index" width="85">
                        </el-table-column>
                        <el-table-column prop="SysTabName" label="表名">
                        </el-table-column>
                        <el-table-column prop="SysTabDesc" label="描述">
                        </el-table-column>
                        <el-table-column prop="SysFieldInfo" label="自定义字段">
                        </el-table-column>
                        <el-table-column label="操作" width="150">
                            <template scope="scope">
                                <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
                                <el-button type="text" size="small" @click="handleDel(scope.row)">删除</el-button>
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
            </section>
        </el-tab-pane>
        <el-tab-pane name="BusinessInfo" label="自定义字段管理">
            <el-collapse v-model="activeCollapseNames">
                <el-collapse-item title="表结构管理" name="1">
                    <el-form label-width="80px">
                        <el-form-item label="表名称">
                            <el-input v-model='BusinessTitle'></el-input>
                        </el-form-item>
                        <el-form-item label="表描述">
                            <el-input v-model='BusinessTitleDesc'></el-input>
                        </el-form-item>
                        <el-form-item label="字段信息" v-for="(item,index) in EditLine" :key='index'>
                            <el-col :span='7'>
                                <el-input placeholder="字段名称" v-model='item.Title'></el-input>
                            </el-col>
                            <el-col :span='7'>
                                <el-input placeholder="字段描述" v-model="item.Desc"></el-input>
                            </el-col>
                            <el-col :span='4'>
                                <el-select v-model="item.Link" placeholder="关联字段">
                                    <el-option v-for="item in options_Fields" :key="item.value" :label="item.label" :value="item.value">
                                    </el-option>
                                </el-select>
                            </el-col>
                            <el-col :span='4'>
                                <el-select v-model="item.Type" placeholder="字段类型">
                                    <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
                                    </el-option>
                                </el-select>
                            </el-col>
                            <el-col :span='2'>
                                <el-button type="danger" @click="onDelLine(index)">删除</el-button>
                            </el-col>
                        </el-form-item>
                        <el-form-item>
                            <el-button type="primary" @click="onAddLine">新增行</el-button>
                            <el-button type="primary" @click="onSubmit">提交</el-button>
                            <el-button @click="onCancel">取消</el-button>
                        </el-form-item>
                    </el-form>
                </el-collapse-item>
                <el-collapse-item title="表内容管理" name="2">
                    <SysFieldManager_Child :TabelName="BusinessTitle" :TableDesc="BusinessTitleDesc" :Field='currentField' />
                </el-collapse-item>
            </el-collapse>
        </el-tab-pane>
    </el-tabs>
</template>

<script>
import { mapGetters } from "vuex";
import { mapActions } from "vuex";
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import moment from "moment";
import SysFieldManager_Child from "./SysFieldManager_Child.vue";

export default {
  computed: {
    // 使用对象展开运算符将 getters 混入 computed 对象中
    ...mapGetters([
      "getProj"
      // ...
    ])
  },
  data() {
    return {
      prov_city_options: [],
      activeCollapseNames: "1",
      activeName: "userManager",
      formInline: {
        user: ""
      },
      tableData: [],
      tableDataLength: 0,
      listLoading: false,
      currentPage: 1,
      currentPageSize: 10,
      options: [
        { value: "String", label: "字符类型" },
        { value: "Number", label: "数值类型" },
        { value: "Date", label: "日期类型" }
      ],
      options_Fields: [{ key: "none", value: "空" }],
      currentTabelName: "",
      EditLine: [
        {
          Title: "",
          Desc: "",
          Type: "String",
          Link: "空"
        }
      ],
      BusinessTitle: "",
      BusinessTitleDesc: "",
      currentField: "",
      currentFieldNames: [],

      prov_city_options: [],
      proj_data: [],
      curProjID: ""
    };
  },
  created: function() {
    this.getSysFieldList();
    this.getAllFieldList();
    this.getProvData();
  },
  methods: {
    ...mapActions(["setProj"]),
    //切换区域
    handleItemChange(val) {
      this.curCityID = -1;
      let cur_val = val[0];
      let that = this;
      let url = `/api/GetCityByProvID?ProvID=${cur_val.id}`;
      axios
        .get(url)
        .then(res => {
          if (res.data.success) {
            let cur = that.prov_city_options.filter(
              i => i.value.id == cur_val.id
            )[0].children;
            res.data.data
              .map(i => {
                return {
                  label: i.Name,
                  value: i.data
                };
              })
              .forEach(item => {
                if (cur.filter(i => i.label == item.label).length == 0)
                  cur.push(item);
              });
            let firstcity = val[1];
            if (!firstcity) return;
            this.curCityID = firstcity.id;
            if (firstcity) {
              axios
                .get(`/api/GetProjByCityID?CityID=${firstcity.id}`)
                .then(res => {
                  if (res.data.success) {
                    that.proj_data = res.data.data;
                    that.default_active = "0";
                    if (that.proj_data[0]) {
                      that.curProjID = that.proj_data[0].value.id;
                      //that.setProj(that.curProjID);
                    } else {
                      this.curProjID = -1;
                    }
                  }
                })
                .catch(err => {
                  console.log(err);
                });
            } else {
              that.getUserData();
            }
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    //获取省份资料
    getProvData() {
      let that = this;
      axios
        .get("/api/GetProv")
        .then(res => {
          if (res.data.success) {
            that.prov_city_options = res.data.data;
          }
        })
        .catch(err => console.log(err));
    },
    //删除记录
    handleDel: function(row) {
      var _this = this;
      this.$confirm("确认删除该记录吗?", "提示", {
        type: "warning"
      })
        .then(() => {
          _this.listLoading = true;
          NProgress.start();
          //console.log(row._id)
          axios
            .post(`/api/SysField/delete`, { id: row._id })
            .then(function(res) {
              if (res.data.success) {
                _this.$message({
                  message: "删除成功",
                  type: "success"
                });
                _this.listLoading = false;
                NProgress.done();
                _this.getSysFieldList();
              } else {
                _this.$message({
                  message: "删除失败",
                  type: "error"
                });
              }
            });
        })
        .catch(() => {});
    },
    //显示新增界面
    handleAdd: function() {
      this.activeName = "BusinessInfo";
      this.BusinessTitle = "";
      this.BusinessTitleDesc = "";
    },
    //获取字段列表
    getSysFieldList: function() {
      try {
        var vm = this;
        var params = {
          limit: vm.$data.currentPageSize,
          page: vm.$data.currentPage
        };
        axios.get("/api/SysField/", { params: params }).then(function(res) {
          vm.$data.tableData = res.data.data;
          vm.$data.tableDataLength = res.data.meta.count;
          // console.log(vm.$data.tableData);
        });
        axios.get("/api/SysField/AllFieldName").then(function(res) {
          vm.$data.currentFieldNames = res.data.data;
        });
      } catch (error) {
        console.log(error);
      }
    },
    //获取所有字段名
    getAllFieldList: function() {
      try {
        var vm = this;
        axios.get("/api/SysField/AllFieldName").then(
          function(res) {
            this.currentFieldNames = res.data.data;
            this.options_Fields = [{ key: "none", value: "空" }];
            res.data.data.forEach(i => {
              this.options_Fields.push({
                key: i.SysTabName,
                value: i.SysTabName
              });
            });
          }.bind(this)
        );
      } catch (error) {
        console.log(error);
      }
    },
    handleSizeChange(val) {
      this.$data.currentPageSize = val;
      this.getSysFieldList();
    },
    handleCurrentChange(val) {
      this.$data.currentPage = val;
      this.getSysFieldList();
    },

    //编辑当前行的自定义表
    handleEdit: function(row) {
      try {
        this.activeName = "BusinessInfo";
        this.BusinessTitle = row.SysTabName;
        this.BusinessTitleDesc = row.SysTabDesc;
        this.currentField = row;
        var SysFieldInfo = JSON.parse(row.SysFieldInfo);
        this.EditLine.splice(0, this.EditLine.length);

        Object.keys(SysFieldInfo).forEach(i => {
          this.EditLine.push({
            Title: i,
            Desc: SysFieldInfo[i].desc,
            Type: SysFieldInfo[i].type,
            Link: SysFieldInfo[i].link
          });
        });
      } catch (error) {
        console.log(error);
      }
    },
    //提交自定义表表单
    onSubmit() {
      //1. 验证数据完整性
      if (this.BusinessTitle === "") {
        this.$message({
          message: "请输入业务信息名称",
          type: "error"
        });
        return;
      }
      if (this.EditLine.filter(i => i.Title === "").length > 0) {
        this.$message({
          message: "请输入业务信息标题",
          type: "error"
        });
        return;
      }
      //2. 组织数据
      var jsonStr = "{";
      this.EditLine.forEach(element => {
        jsonStr += `"${element.Title}":{"type":"${element.Type}","desc":"${
          element.Desc
        }","link":"${element.Link}"},`;
      });
      jsonStr = jsonStr.substr(0, jsonStr.length - 1) + "}";
      //3. 请求保存
      axios
        .post("/api/SysField/create", {
          SysTabName: this.BusinessTitle,
          SysTabDesc: this.BusinessTitleDesc,
          SysFieldInfo: jsonStr
        })
        .then((res, err) => {
          if (err) {
            this.$message({
              message: `保存失败：${err}`,
              type: "error"
            });
            return;
          }
          this.$message({
            message: `保存成功`,
            type: "success"
          });
        });
      this.activeName = "BusinessManager";
      //4. 刷新
      this.getSysFieldList();
    },
    //添加新行
    onAddLine() {
      if (this.EditLine.filter(i => i.Title === "").length > 0) {
        this.$message({
          message: "请输入业务信息标题",
          type: "error"
        });
      } else {
        this.EditLine.push({ Title: "", Type: "String" });
      }
    },
    //取消
    onCancel() {
      this.activeName = "BusinessManager";
      this.EditLine.splice(1, this.EditLine.length - 1);
    },
    handleClick() {},
    //删除行
    onDelLine(index) {
      if (this.EditLine.length === 1) return;
      this.EditLine.splice(index, 1);
    },
    //新增自定义表
    newCustomTable() {
      try {
        this.activeName = "BusinessInfo";
        this.activeCollapseNames = "1";
        this.BusinessTitle = "";
        this.BusinessTitleDesc = "";
        this.EditLine = [
          {
            Title: "",
            Desc: "",
            Type: "String",
            Link: "空"
          }
        ];
      } catch (error) {
        console.log(error);
      }
    }
  },
  components: {
    SysFieldManager_Child
  }
};
</script>

<style lang="scss" scoped>
.toolbar .el-form-item {
  margin-bottom: 10px;
}

.toolbar {
  background: #fff;
  padding-top: 10px;
}

.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}

.box-card {
  width: 480px;
}
</style>