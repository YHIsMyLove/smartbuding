<template>
    <el-tabs style="width:100%;">
        <el-tab-pane label="部门管理">
            <el-row>
                <el-col :span="6">
                    <el-card class="box-card">
                        <div slot="header" class="clearfix">
                            <span>组织架构树</span>
                            <!-- <el-button style="float: right; padding: 3px 0" type="text">操作按钮</el-button> -->
                        </div>
                        <el-input placeholder="输入关键字进行过滤" v-model="filterText">
                        </el-input>
                        <el-tree :filter-node-method="filterNode" :data="depttree" default-expand-all node-key="id" ref="tree" highlight-current
                            :props="defaultProps" @node-click="handleNodeClick">
                        </el-tree>
                    </el-card>
                </el-col>
                <el-col :span="18">
                    <el-card class="box-card">
                        <div slot="header" class="clearfix">
                            <span>组织架构管理</span>
                            <!-- <el-button style="float: right; padding: 3px 0" type="text">操作按钮</el-button> -->
                        </div>
                        <el-form ref="form" :model="form" label-width="80px">
                            <el-form-item label="活动名称">
                                <el-input></el-input>
                            </el-form-item>
                            <el-form-item label="活动区域">
                                <el-select placeholder="请选择活动区域">
                                    <el-option label="区域一" value="shanghai"></el-option>
                                    <el-option label="区域二" value="beijing"></el-option>
                                </el-select>
                            </el-form-item>
                            <el-form-item label="活动时间">
                                <el-col :span="11">
                                    <el-date-picker type="date" placeholder="选择日期" style="width: 100%;"></el-date-picker>
                                </el-col>
                                <el-col class="line" :span="2">-</el-col>
                                <el-col :span="11">
                                    <el-time-picker type="fixed-time" placeholder="选择时间" style="width: 100%;"></el-time-picker>
                                </el-col>
                            </el-form-item>
                        </el-form>
                    </el-card>

                </el-col>
            </el-row>
        </el-tab-pane>
        <el-tab-pane label="职位管理">

        </el-tab-pane>
    </el-tabs>
</template>
<script>
    export default {
        watch: {
            filterText(val) {
                this.$refs.tree.filter(val);
            }
        },
        data() {
            return {
                filterText: '',
                depttree: [
                    {
                        id: 1, label: 'Root', children: [{
                            id: 2, label: '总经办'
                        },
                        {
                            id: 3, label: '市场部', children: [
                                { id: 5, label: '军工组' },
                                { id: 6, label: '气象组' }
                            ]
                        },
                        { id: 4, label: '研发部' }]
                    },
                ]
            }
        },
        methods: {
            filterNode(value, data) {
                if (!value) return true;
                return data.label.indexOf(value) !== -1;
            },
            handleNodeClick(data) {
                console.log(data)
            }
        }
    }
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
        width: 95%;
    }
</style>