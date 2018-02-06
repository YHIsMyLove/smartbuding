<template>
    <el-tabs style="width:100%;" @tab-click="handleClick" @tab-remove="handleRemove">
        <el-tab-pane label="上传案例">
            <el-form label-width="80px">
                <el-form-item label="案例名称" prop="name">
                    <el-input auto-complete="off"></el-input>
                </el-form-item>
                <el-form-item label="案例说明">
                    <el-input type="textarea"></el-input>
                </el-form-item>
                <el-form-item label="VR路径" prop="name">
                    <el-input>
                        <template slot="prepend">Http://</template>
                    </el-input>
                </el-form-item>
                <el-form-item label="VR预览" prop="name">
                    <el-upload name='file' id='file' class="file" drag 
                    action="/api/case/upload" 
                    limit=1 
                    >
                        <i class="el-icon-upload"></i>
                        <div class="el-upload__text">将文件拖到此处，或
                            <em>点击上传</em>
                        </div>
                        <div class="el-upload__tip" slot="tip">只能上传jpg/png文件，且不超过500kb</div>
                    </el-upload>
                </el-form-item>

            </el-form>
            <div class="submit_tool">
                <el-button>取 消</el-button>
                <el-button type="primary">上传</el-button>
            </div>
        </el-tab-pane>
        <el-tab-pane label="案例展示">
            <el-carousel :interval="4000" type="card" height="300px">
                <el-carousel-item v-for="(item,index) in CaseShowTitleInfo" :key="index">
                    <img :src="item.ImagePath" alt="" style="width:100%; height:100%;">
                </el-carousel-item>
            </el-carousel>
            <el-col :span="8" v-for="(o, index) in CaseShowInfo" :key="index" :offset="0">
                <el-card :body-style="{ padding: '0px' }">
                    <img :src="o.ImagePath" alt="" class="image">
                    <div style="padding: 20px;">
                        <span>{{o.Title}}</span>
                        <div class="bottom clearfix">
                            <time class="time">{{ o.Address }}</time>
                            <el-button type="text" class="button">编辑 </el-button>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
                <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 40]"
                    :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength" style="float:right">
                </el-pagination>
            </el-col>
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
                tableDataLength: 0,
                currentPage: 1,
                currentPageSize: 10,
                currentDate: '',
                CaseShowInfo: [],
                CaseShowTitleInfo: [],
                uploadHeaders: { "Content-Type": "multipart/form-data" }
            }
        },
        methods: {
            handleRemove(tab) {
                console.log(tab);
            },
            handleClick(tab) {
                console.log(tab);
            },
            getCaseShow() {
                var vm = this;
                var params = {
                    limit: vm.$data.currentPageSize,
                    page: vm.$data.currentPage
                }
                axios.get('/api/caseshow', { params: params }).then(function (res) {
                    vm.$data.CaseShowInfo = res.data.data;
                    vm.$data.tableDataLength = res.data.meta.count;
                    console.log(res.data.data)
                })
            },
            handleSizeChange(val) {
                this.$data.currentPageSize = val;
                this.getCaseShow();
            },
            handleCurrentChange(val) {
                this.$data.currentPage = val;
                this.getCaseShow();
            }
        },
        created: function () {
            this.getCaseShow()
        },
        computed: {
            caseshow4title: function () {
                return this.getCaseShow()
            }
        }
    };
</script>
<style scoped>
    .el-carousel__item:nth-child(2n) {
        background-color: #99a9bf;
    }

    .el-carousel__item:nth-child(2n+1) {
        background-color: #d3dce6;
    }

    .time {
        font-size: 13px;
        color: #999;
    }

    .bottom {
        margin-top: 13px;
        line-height: 12px;
    }

    .button {
        padding: 0;
        float: right;
    }

    .image {
        width: 100%;
        display: block;
    }

    .clearfix:before,
    .clearfix:after {
        display: table;
        content: "";
    }

    .clearfix:after {
        clear: both
    }

    .submit_tool {
        float: right
    }

    .upload-demo {
        margin: 0 auto,
    }
</style>