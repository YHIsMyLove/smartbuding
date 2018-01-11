<template>
    <el-tabs v-model="activeName" style="width:100%;" @tab-click="handleClick">
        <el-tab-pane name="BusinessManager" label="字段管理">
            <section>
                <el-col :span="24" class="toolbar">
                    <el-form :inline="true" :model="formInline" class="demo-form-inline">
                        <el-form-item>
                            <el-input v-model="formInline.user" placeholder="字段名称"></el-input>
                        </el-form-item>
                        <el-form-item>
                            <el-button @click='getSysFieldList'>查询</el-button>
                        </el-form-item>
                    </el-form>
                </el-col>
                <template>
                    <el-table :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:500px">
                        <el-table-column type="index" width="80">
                        </el-table-column>
                        <el-table-column prop="SysTabName" label="表名称" width="180" sortable>
                        </el-table-column>
                        <el-table-column prop="SysFieldInfo" label="自定义字段" width="500" sortable>
                        </el-table-column>
                        <el-table-column label="操作" width="100">
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
        <el-tab-pane name="BusinessInfo" label="字段详情">
            <el-collapse>
                <el-collapse-item title="表结构管理" name="1">
                    <el-form label-width="80px">
                        <el-form-item label="表名称">
                            <el-input v-model='BusinessTitle'></el-input>
                        </el-form-item>
                        <el-form-item label="字段信息" v-for="(item,index) in EditLine" :key='index'>
                            <el-col :span='7'>
                                <el-input placeholder="字段名称" v-model='item.Title'></el-input>
                            </el-col>
                            <el-col :span='7'>
                                <el-input placeholder="字段别名"></el-input>
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
                    <SysFieldManager_Child :TabelName="BusinessTitle" :Field='currentField' />
                    <!-- @FieldChange="childFieldChange(currentField)" /> -->
                </el-collapse-item>
            </el-collapse>
        </el-tab-pane>
    </el-tabs>
</template>

<script>
    import util from '../../common/util'
    import NProgress from 'nprogress'
    import axios from 'axios'
    import moment from 'moment'
    import SysFieldManager_Child from './SysFieldManager_Child.vue'
    export default {
        data() {
            return {
                activeName: 'BusinessManager',
                currentid: '',
                formInline: {
                    user: ''
                },
                tableData: [],
                tableDataLength: 0,
                listLoading: false,
                currentPage: 1,
                currentPageSize: 10,
                options: [{
                    value: 'String',
                    label: '字符类型'
                }, {
                    value: 'Number',
                    label: '数值类型'
                }, {
                    value: 'Date',
                    label: '日期类型'
                }],
                options_Fields: [{
                    key: 'none',
                    value: '空'
                }, {
                    key: 'UserID',
                    value: '用户ID'
                }, {
                    key: 'ShowCaseID',
                    value: '案例展示ID'
                }],
                currentTabelName: '',
                EditLine: [{
                    'Title': '',
                    'Type': 'String',
                    'Link': 'none'
                }],
                BusinessTitle: '',
                currentField: '',
                currentFieldNames: []
            }
        },
        created: function () {
            this.getSysFieldList()
            this.getAllFieldList()
        },
        methods: {
            //删除记录
            handleDel: function (row) {
                var _this = this;
                this.$confirm('确认删除该记录吗?', '提示', {
                    type: 'warning'
                }).then(() => {
                    _this.listLoading = true;
                    NProgress.start();
                    //console.log(row._id)
                    axios.post(`/api/SysField/delete`, { id: row._id }).then(function (res) {
                        if (res.data.success) {
                            _this.$message({
                                message: '删除成功',
                                type: 'success'
                            });
                            _this.listLoading = false;
                            NProgress.done();
                            _this.getSysFieldList()
                        } else {
                            _this.$message({
                                message: '删除失败',
                                type: 'error'
                            });
                        }
                    })
                }).catch(() => {

                });
            },
            //显示编辑界面
            handleEdit: function (row) {
                this.activeName = 'BusinessInfo'
                this.BusinessTitle = row.SysTabName
                this.currentField = row
                var SysFieldInfo = JSON.parse(row.SysFieldInfo)
                this.EditLine.splice(0, this.EditLine.length)
                Object.keys(SysFieldInfo).forEach(i => {
                    this.EditLine.push({ Title: i, Type: SysFieldInfo[i].type, Link: SysFieldInfo[i].link })
                })
                console.log("当前编辑" + JSON.stringify(this.EditLine))
            },
            //显示新增界面
            handleAdd: function () {
                this.activeName = 'BusinessInfo'
                this.BusinessTitle = ''
            },
            //获取字段列表
            getSysFieldList: function () {
                var vm = this;
                var params = {
                    limit: vm.$data.currentPageSize,
                    page: vm.$data.currentPage
                }
                axios.get('/api/SysField/', { params: params }).then(function (res) {
                    vm.$data.tableData = res.data.data;
                    vm.$data.tableDataLength = res.data.meta.count;
                })
                axios.get('/api/SysField/AllFieldName').then(function (res) {
                    vm.$data.currentFieldNames = res.data.data
                })
            },
            //获取所有字段名
            getAllFieldList: function () {
                var vm = this;
                axios.get('/api/SysField/AllFieldName').then(function (res) {
                    this.currentFieldNames = res.data.data
                    this.options_Fields = [{
                        key: 'none',
                        value: '空'
                    }, {
                        key: 'User',
                        value: '用户表'
                    }, {
                        key: 'ShowCase',
                        value: '案例展示表'
                    }]
                    res.data.data.forEach(i => {
                        //console.log(JSON.stringify(i))
                        this.options_Fields.push({ key: i.SysTabName, value: i.SysTabName })
                    })

                }.bind(this))
            },
            handleSizeChange(val) {
                this.$data.currentPageSize = val;
                this.getSysFieldList();
            },
            handleCurrentChange(val) {
                this.$data.currentPage = val;
                this.getSysFieldList();
            },
            //提交表单
            onSubmit() {
                if (this.BusinessTitle === '') {
                    this.$message({
                        message: '请输入业务信息名称',
                        type: 'error'
                    });
                    return
                }
                //console.log('保存时' + this.EditLine)
                if (this.EditLine.filter(i => i.Title === '').length > 0) {
                    this.$message({
                        message: '请输入业务信息标题',
                        type: 'error'
                    });
                    return
                }
                var jsonStr = '{'
                this.EditLine.forEach(element => {
                    jsonStr += `"${element.Title}":{"type":"${element.Type}","link":"${element.Link}"},`
                });
                jsonStr = jsonStr.substr(0, jsonStr.length - 1) + '}'
                axios.post('/api/SysField/create', { SysTabName: this.BusinessTitle, SysFieldInfo: jsonStr })
                    .then((res, err) => {
                        if (err) {
                            this.$message({
                                message: `保存失败：${err}`,
                                type: 'error'
                            });
                            return
                        }
                        this.$message({
                            message: `保存成功`,
                            type: 'success'
                        });
                    })
                this.activeName = 'BusinessManager'
            },
            onAddLine() {
                if (this.EditLine.filter(i => i.Title === '').length > 0) {
                    this.$message({
                        message: '请输入业务信息标题',
                        type: 'error'
                    });
                } else {
                    //console.log('新增行')
                    this.EditLine.push({ 'Title': '', 'Type': 'String' })
                }
            },
            onCancel() {
                this.activeName = 'BusinessManager'
                this.currentid = ''
                this.EditLine.splice(1, this.EditLine.length - 1)
                this.EditLine[0].Title = ''
                //console.log('取消')
            },
            handleClick() {
                //console.log(this.currentid === '' ? '新增' : '编辑')
            },
            onDelLine(index) {
                //console.log(index)
                if (this.EditLine.length === 1) return
                this.EditLine.splice(index, 1)
                //console.log('删除后' + this.EditLine)
            }
        },
        components: {
            SysFieldManager_Child
        }
    }
</script>

<style lang="scss" scoped>
    .toolbar .el-form-item {
        margin-bottom: 10px;
    }

    .toolbar {
        background: #fff;
        padding-top: 10px;
    }
</style>