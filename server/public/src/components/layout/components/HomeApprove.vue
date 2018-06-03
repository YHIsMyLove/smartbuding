<template>
    <el-card shadow="always" class="main">
        <el-tabs class="tabs" type="card" v-model="CurrentItem">
            <el-tab-pane :key="index" v-for="(item,index) in ApproveItems" 
                         :label="item.Name" :name="index+''"> 
                <template>
                    <el-menu>
                        <el-menu-item v-for="(menuitem,menuindex) in item.Items" :index="menuindex+''" 
                                      :key="menuindex">
                            <el-col :span="8">
                                <span style="color:#409EFF">{{menuitem.User}}</span>
                            </el-col>
                            <el-col :span="8">
                                <b>{{menuitem.Title}}</b>
                            </el-col>
                            <el-col :span="8">
                                <span style="color:#909399">{{menuitem.Time}}</span>
                            </el-col>
                        </el-menu-item>
                    </el-menu>
                </template>
            </el-tab-pane>  
        </el-tabs>
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
      CurrentItem: "0",
      ApproveItems: [
        {
          Name: "待处理审批",
          Items: [
            {
              User: "张三",
              Title: "测试审批",
              Time: moment().format("YYYY-MM-DD hh:mm")
            },
            {
              User: "张三",
              Title: "测试审批",
              Time: moment().format("YYYY-MM-DD hh:mm")
            }
          ],
          Count: 2
        },
        {
          Name: "已通过审批",
          Count: 1,
          Items: [
            {
              User: "张三",
              Title: "测试审批",
              Time: moment().format("YYYY-MM-DD hh:mm")
            }
          ]
        },
        {
          Name: "已拒绝审批",
          Count: 1,
          Items: [
            {
              User: "李四",
              Title: "测试审批",
              Time: moment().format("YYYY-MM-DD hh:mm")
            }
          ]
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
.tabs {
  height: 320px;
}
.el-menu {
  background-color: #fff;
}
.el-col {
  border-bottom: 1px solid;
  border-bottom-color: #c0c4cc;
}
.el-menu-item.is-active {
  color: #48576a;
}
</style>


