<template>
    <el-tabs style="width:100%;">
        <el-tab-pane label="部门管理">
            <el-row>
                <el-col :span="6">
                    <el-card class="box-card">
                        <div slot="header" class="clearfix">
                            <span>组织架构树</span>
                        </div>
                        <el-input placeholder="输入关键字进行过滤" v-model="filterText">
                        </el-input>
                        <el-tree :expand-on-click-node="false" :filter-node-method="filterNode" :data="depttree" default-expand-all node-key="id"
                            ref="tree" highlight-current @node-click="handleNodeClick">
                        </el-tree>
                    </el-card>
                </el-col>
                <el-col :span="18">
                    <el-card class="box-card">
                        <div slot="header" class="clearfix">
                            <span>组织架构管理</span>
                        </div>
                        <el-form ref="form" label-width="80px">
                            <el-form-item label="父节点">
                                <el-input :disabled="true" v-model='currentNode.label'></el-input>
                            </el-form-item>
                            <el-form-item label="部门ID">
                                <el-input :disabled="true" v-model='newDeptNode.id'></el-input>
                            </el-form-item>
                            <el-form-item label="部门名称">
                                <el-input v-model='newDeptNode.label'></el-input>
                            </el-form-item>

                        </el-form>
                        <div>
                            <el-button type="primary" @click="addNewDept" :loading="editLoading">新增部门</el-button>
                            <el-button @click="cancelAddDept">取 消</el-button>
                        </div>
                    </el-card>

                </el-col>
            </el-row>
        </el-tab-pane>
        <el-tab-pane label="职位管理">
            <el-card class="box-card">
                <div slot="header" class="clearfix">
                    <span>职位表</span>
                    <el-button style="float: right; padding: 3px 0" @click="newTag" type="text">新增职位</el-button>
                </div>
                <el-tag v-for="tag in PostTags" :key="tag.name" closable :type="tag.type" @close="delTag(tag)">
                    {{tag.name}}
                </el-tag>
            </el-card>

            <el-card class="box-card">
                <div slot="header" class="clearfix">
                    <span>职位管理</span>
                </div>
                <el-form ref="form" label-width="80px">
                    <el-form-item label="职位ID">
                        <el-input :disabled="true" v-model='newPost.postid'></el-input>
                    </el-form-item>
                    <el-form-item label="职位名称">
                        <el-input v-model='newPost.postname'></el-input>
                    </el-form-item>
                    <el-form-item label="部门类型">
                        <el-input v-model='newPost.posttype'></el-input>
                    </el-form-item>
                </el-form>
                <div>
                    <el-button type="primary" @click="addNewDept" :loading="editLoading">新增部门</el-button>
                    <el-button @click="cancelAddDept">取 消</el-button>
                </div>
            </el-card>
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
                depttree: [{ id: "1", label: 'Root', children: [] }],
                currentNode: {},
                newDeptNode: {
                    label: '',
                    id: 0,
                    children: []
                },
                editLoading: false,
                listLoading: false,
                PostTags: [
                    { name: '精算师', type: 'success', index: '1-1' },
                    { name: '部门经理', type: 'info', index: '1-2' },
                    { name: '项目经理', type: 'warning', index: '1-3' },
                    { name: '预算师', type: 'danger', index: '1-4' },
                ],
                newPost: {
                    postid: '',
                    postname: '',
                    posttype: ''
                }
            }
        },
        created() {
            this.newPost.postid = this.PostTags.length + 1
        },
        methods: {
            newTag() { },
            delTag() { },
            computeNewID() {
                if (this.currentNode.children) {
                    this.newDeptNode.id = this.currentNode.id + '-' + (this.currentNode.children.length + 1)
                } else {
                    this.newDeptNode.id = this.currentNode.id + '-1'
                }
            },
            addNewDept() {
                var item = { id: this.newDeptNode.id, label: this.newDeptNode.label, children: [] }
                if (!this.currentNode.children) {
                    this.$set(this.currentNode, 'children', []);
                }
                this.currentNode.children.push(item);
                this.newDeptNode = { id: '', label: '', children: [] }
                this.computeNewID()
            },
            cancelAddDept() { },
            filterNode(value, data) {
                if (!value) return true;
                return data.label.indexOf(value) !== -1;
            },
            handleNodeClick(data) {
                this.currentNode = data
                this.computeNewID()
            },
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