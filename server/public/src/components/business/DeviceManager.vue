<template>
    <el-tabs style="width:100%;">
        <el-tab-pane label="权限管理">
            <el-card class="box-card">
                <div slot="header" class="clearfix">
                    <span>设备表</span>
                    <el-button style="float: right; padding: 3px 0" type="text">新增权限</el-button>
                </div>

                <!--data按照指定数组格式传进来就会自动渲染表格数据-->
                <!--v-loading为真时，显示loading动画-->
                <el-table :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:100%">
                    <el-table-column type="index">
                    </el-table-column>
                    <el-table-column prop="DevName" label="设备名称" sortable>
                    </el-table-column>
                    <el-table-column prop="DevClass" label="设备类型" sortable>
                    </el-table-column>
                    <el-table-column prop="DevStatus" label="设备状态" sortable>
                    </el-table-column>
                    <el-table-column prop="DevIp" label="设备ip">
                    </el-table-column>
                    <el-table-column prop="DevPort" label="设备端口">
                    </el-table-column>
                    <el-table-column prop="DevDesc" label="设备备注" width="200">
                    </el-table-column>
                    <el-table-column label="操作" width="100">
                        <template scope="scope">
                            <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
                            <el-button type="text" size="small" @click="handleDel(scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>

                <!--分页-->
                <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
                    <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
                        :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength"
                        style="float:right">
                    </el-pagination>
                </el-col>
            </el-card>


        </el-tab-pane>
    </el-tabs>
</template>
<script>
    export default {
        data() {
            return {
            }
        },
        methods: {
            handleDel: function (row) { },
            handleEdit: function (row) { },
        }
    };
</script>
<style scoped>
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
        clear: both
    }

    .box-card {
        width: 90%;
        margin-top: 2px;
    }

    .el-tag {
        margin: 2px;
    }
</style>