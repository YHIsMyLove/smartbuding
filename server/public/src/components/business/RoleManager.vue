<template>

    <div>

        <el-tabs style="width:100%;">
            <el-tab-pane label="权限管理">
                <el-card class="box-card">
                    <div slot="header" class="clearfix">
                        <span>角色表</span>
                        <el-button style="float: right; padding: 3px 0" @click="newTag" type="text">新增角色</el-button>
                    </div>
                    <el-tag v-for="tag in RoleTags" :key="tag.name" closable :type="tag.type" @close="delTag(tag)">
                        {{tag.name}}
                    </el-tag>
                </el-card>
                <el-dialog title="新增角色" v-model="editFormVisible" :close-on-click-modal="true">
                    <el-form :model="editForm" label-width="80px" ref="editForm">
                        <el-form-item label="角色名" prop="name">
                            <el-input auto-complete="off"></el-input>
                        </el-form-item>
                    </el-form>
                    <div slot="footer" class="dialog-footer">
                        <el-button @click="editFormVisible = false">取 消</el-button>
                        <el-button type="primary" :loading="editLoading">确定</el-button>
                    </div>
                </el-dialog>

                <el-card class="box-card">
                    <div slot="header" class="clearfix">
                        <span>角色设置</span>
                        <el-button style="float: right; padding: 3px 0" @click="newTag" type="text">新增角色</el-button>
                    </div>

                    <el-row>
                        <el-col :span="6">
                            <el-menu default-active="1-1">
                                <el-menu-item v-for="(tag,i) in RoleTags" :key="i" :index="tag.index" @click="changeMenu">{{tag.name}}</el-menu-item>
                            </el-menu>
                        </el-col>
                        <el-col :span="18">
                            <el-input placeholder="输入关键字进行过滤" v-model="filterText">
                            </el-input>
                            <el-tree accordion :filter-node-method="filterNode" ref="UserTree" :data="UserData" show-checkbox :props="defaultProps" @node-click="handleNodeClick"></el-tree>
                        </el-col>
                    </el-row>
                </el-card>

            </el-tab-pane>
        </el-tabs>
    </div>

</template>
<script>
    export default {
        data() {
            return {
                filterText: '',
                PermissionsActiveName: '1',
                RoleTags: [
                    { name: '普通员工', type: 'success', index: '1-1' },
                    { name: '经理', type: 'info', index: '1-2' },
                    { name: '操作员', type: 'warning', index: '1-3' },
                    { name: '管理员', type: 'danger', index: '1-4' },
                ],
                UserData: [
                    {
                        id: 1,
                        label: '市场部',
                        children: [{
                            id: 4,
                            label: '王五',
                        }, {
                            id: 4,
                            label: '张三',
                        }, {
                            id: 4,
                            label: '李四',
                        }]
                    }, {
                        id: 2,
                        label: '研发部',
                        children: [{
                            id: 4,
                            label: '赵七',
                        }, {
                            id: 4,
                            label: '孙八',
                        }, {
                            id: 4,
                            label: '李九',
                        }]
                    }, {
                        id: 3,
                        label: '项目部',
                        children: [{
                            id: 4,
                            label: '杨十',
                        }, {
                            id: 4,
                            label: '周十一',
                        }, {
                            id: 4,
                            label: '何十二',
                        }]
                    }
                ],
                defaultProps: {
                    children: 'children',
                    label: 'label',
                },
                editLoading: false,
                editFormVisible: false,
                editForm: {
                    tagName: '',
                    tagType: '',
                    tagDesc: ''
                }
            }
        },
        watch: {
            filterText(val) {
                this.$refs.UserTree.filter(val);
            }
        },
        methods: {
            changeMenu() {
                console.log('更新')
            },
            filterNode(value, data) {
                if (!value) return true;
                return data.label.indexOf(value) !== -1;
            },
            delTag: function (id) {
                console.log(this.RoleTags.indexOf(id))
                let index = this.RoleTags.indexOf(id)
                this.RoleTags.splice(index, 1)
            },
            newTag: function () {
                console.log('新增角色')
                this.editFormVisible = true
            },
            handleNodeClick: function () {

            }
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