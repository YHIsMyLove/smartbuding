<template>
    <el-tabs style="width:100%;">
        <el-tab-pane label="内容展示">
            <!-- {{Field}} -->
            <el-table highlight-current-row v-loading="listLoading" style="width: 100%; height:500px">
                <el-table-column type="index" width="80">
                </el-table-column>
                <div style="width:100%;height:100%" v-for="(item,index) in EditLine" :key="index">
                    <el-table-column :label="item.Title" sortable>
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
        <el-tab-pane label="新增内容">
        </el-tab-pane>
        <el-tab-pane label="关联管理">
            现阶段由开发者自己实现
        </el-tab-pane>
    </el-tabs>
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
                    'Type': 'String'
                }],
                listLoading: false,
            };
        },
        props: ['Field'],
        methods: {
            getFieldInfo() {
                //console.log('获取信息')
                if (!this.Field) return
                // console.log(this.Field)
                // console.log(this.Field.SysFieldInfo)
                // console.log('test------------------------------')
                // console.log(JSON.parse(this.Field.SysFieldInfo))
                var SysFieldInfo = JSON.parse(this.Field.SysFieldInfo)
                this.EditLine.splice(0, this.EditLine.length)
                Object.keys(SysFieldInfo).forEach(i => {
                    this.EditLine.push({ Title: i, Type: SysFieldInfo[i] })
                })
            },
            handleEdit(row) {

            },
            handleDel(row) {

            },
            // FieldChange(val) {
            //     this.$emit('testchange', val);
            // }
        },
        watch: {
            Field(curVal, oldVal) {
                //console.log('Field发生变化->变化前:' + oldVal + '------------变化后:' + curVal)
                this.getFieldInfo()
            }
        }
    }
</script>

<style scoped>
</style>