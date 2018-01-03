<template>
    <el-col :span="24" class="toolbar">
        <el-form class="demo-form-inline">
            <el-button @click="newLine" type="primary">新增行</el-button>
            <el-button @click="getTabelData" type="primary">刷新</el-button>
        </el-form>
        <el-tabs style="width:100%;">
            <el-tab-pane label="内容展示">
                <el-table :data="tableData" highlight-current-row v-loading="listLoading" style="width: 100%; height:500px">
                    <div style="width:100%;height:100%" v-for="(item,index) in EditLine" :key="index">
                        <el-table-column :prop="item.Title" :label="item.Title" sortable>
                        </el-table-column>
                    </div>
                    <el-table-column label="操作" width="100">
                        <template scope="scope">
                            <el-button type="text" size="small" @click="handleEdit(scope.row)">编辑</el-button>
                            <el-button type="text" size="small" @click="handleDel(scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </el-tab-pane>
        </el-tabs>
        <el-dialog v-model="editFormVisible" title="新增行">
            <div v-for="(item,index) in EditLine" :key="index">
                <el-col :span='4'>
                    {{item.Title}}
                </el-col>
                <el-col :span='20' v-if="item.Link=='空'">
                    <el-input v-model="item.Value"></el-input>
                </el-col>
                <el-col :span='20' v-else="item.Link=='空'">
                    <el-select v-model="item.Value" placeholder="请选择">
                        <el-option v-for="sitem in selectdata" :key="sitem.value" :label="sitem.label" :value="sitem.value">
                            <span style="float: left">{{ sitem.label }}</span>
                            <span style="float: right; color: #8492a6; font-size: 13px">{{ sitem.value }}</span>
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
    import util from '../../common/util'
    import NProgress from 'nprogress'
    import axios from 'axios'
    import moment from 'moment'
    export default {
        data() {
            return {
                EditLine: [{
                    'Title': '',
                    'Type': 'String',
                    'Link': '',
                    'Value': ''
                }],
                listLoading: false,
                editFormVisible: false,
                selectdata: [{
                    value: 'Beijing',
                    label: '北京'
                }, {
                    value: 'Shanghai',
                    label: '上海'
                }],
                tableData: [],
                tableDataLength: 0,
                // value6: '',
                // SaveData: []
            };
        },
        props: ['Field', 'TabelName'],
        methods: {
            //获取字段数据
            getFieldInfo() {
                if (!this.Field) return
                var SysFieldInfo = JSON.parse(this.Field.SysFieldInfo)
                this.EditLine.splice(0, this.EditLine.length)
                Object.keys(SysFieldInfo).forEach(i => {
                    this.EditLine.push({ Title: i, Type: SysFieldInfo[i].type, Link: SysFieldInfo[i].link })
                })
            },
            //获取查询框数据
            getSelectInfo() {
                axios.get('/api/', {}).then((req, err) => {

                })
            },
            //获取tabel的数据 api/SysTable
            getTabelData() {
                var vm = this;
                var params = {
                    limit: vm.$data.currentPageSize,
                    page: vm.$data.currentPage,
                    SysFieldID: vm.TabelName
                }
                axios.get('/api/SysTable/', { params: params }).then(function (res) {
                    vm.$data.tableData = res.data.data;
                    vm.$data.tableDataLength = res.data.meta.count;
                })
                console.log(vm.$data.tableData)
            },
            handleEdit(row) {

            },
            handleDel(row) {

            },
            newLine() {
                this.editFormVisible = true
            },
            cancelNewline() {
                this.editFormVisible = false
            },
            //上传数据 /api/SysTable/create
            postNewline() {
                //检查字段是否有为空的
                let savedata = `{"SysFieldID":"${this.TabelName}",`
                let index = 0
                this.EditLine.forEach(i => {
                    console.log(i)
                    savedata += `"item${index}":"${i.Value}",`
                    index++
                })
                savedata = savedata.substr(0, savedata.length - 1) + '}'
                // console.log(savedata)
                // console.log(JSON.parse(savedata))
                axios.post('/api/SysTable/create', JSON.parse(savedata)).then((req, err) => {
                    if (err) {
                        this.$message({
                            message: `保存失败：${err}`,
                            type: 'error'
                        });
                        return
                    }
                    if (!req.data.success) {
                        this.$message({
                            message: `保存失败：${req.data.msg}`,
                            type: 'error'
                        });
                        return
                    }
                    this.$message({
                        message: `保存成功`,
                        type: 'success'
                    });
                })
                this.editFormVisible = false
                this.getTabelData()
            }
        },
        watch: {
            Field(curVal, oldVal) {
                this.getFieldInfo()
            },
            TabelName(curVal, oldVal) {
                console.log(curVal)
                this.getTabelData()
            }
        }
    }
</script>

<style scoped>
</style>