<template>
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
                    <span>权限表</span>
                    <el-button style="float: right; padding: 3px 0" type="text">新增权限</el-button>
                </div>
                <el-collapse v-model="PermissionsActiveName" accordion>
                    <el-collapse-item title="权限1" name="1">
                        <div>与现实生活一致：与现实生活的流程、逻辑保持一致，遵循用户习惯的语言和概念；</div>
                        <div>在界面中一致：所有的元素和结构需保持一致，比如：设计样式、图标和文本、元素的位置等。</div>
                    </el-collapse-item>
                    <el-collapse-item title="反馈 Feedback" name="2">
                        <div>控制反馈：通过界面样式和交互动效让用户可以清晰的感知自己的操作；</div>
                        <div>页面反馈：操作后，通过页面元素的变化清晰地展现当前状态。</div>
                    </el-collapse-item>
                </el-collapse>
            </el-card>

        </el-tab-pane>
    </el-tabs>
</template>
<script>
    export default {
        data() {
            return {
                PermissionsActiveName: '1',
                RoleTags: [
                    { name: '普通员工', type: 'success' },
                    { name: '经理', type: 'info' },
                    { name: '操作员', type: 'warning' },
                    { name: '管理员', type: 'danger' },
                ],
                editLoading: false,
                editFormVisible: false,
                editForm: {
                    tagName: '',
                    tagType: '',
                    tagDesc: ''
                }
            }
        },
        methods: {
            delTag: function (id) {
                console.log(this.RoleTags.indexOf(id))
                let index = this.RoleTags.indexOf(id)
                this.RoleTags.splice(index, 1)
            },
            newTag: function () {
                console.log('新增角色')
                this.editFormVisible = true
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