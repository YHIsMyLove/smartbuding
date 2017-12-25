<template>
    <el-tabs style="width:100%;" @tab-click="handleClick" @tab-remove="handleRemove">

        <el-carousel :interval="4000" type="card" height="200px">
            <el-carousel-item v-for="(item,index) in CaseShowInfo" :key="index">
                <!-- <h3>{{ item.Title }}</h3> -->
                <img :src="item.ImagePath" alt="" style="width:100%; height:100%;">
            </el-carousel-item>
        </el-carousel>

        <el-tab-pane label="案例展示">
            <el-col :span="8" v-for="(o, index) in CaseShowInfo" :key="index" :offset="0">
                <el-card :body-style="{ padding: '0px' }">
                    <img :src="o.ImagePath" alt="" class="image">
                    <div style="padding: 20px;">
                        <span>{{o.Title}}</span>
                        <div class="bottom clearfix">
                            <time class="time">{{ o.Address }}</time>
                            <el-button type="text" class="button">操作按钮</el-button>
                        </div>
                    </div>
                </el-card>
            </el-col>
        </el-tab-pane>
    </el-tabs>
</template>
<script>
    export default {
        data() {
            return {
                currentDate: '',
                CaseShowInfo: [{
                    Title: '测试1',
                    ImagePath: 'https://wx.qlogo.cn/mmopen/vi_32/X2m0EnlKoUMQsKE0OBSJyqOt8tgibkYxlcaP8YLial5gmygcvALCosrOrff6tibEKg7KL1IdLlQ6mmxqOxeWScxbA/0',
                    User: 'DannyJones',
                    Address: '武汉市风度柏林',
                }, {
                    Title: '测试2',
                    ImagePath: 'https://wx.qlogo.cn/mmopen/vi_32/X2m0EnlKoUMQsKE0OBSJyqOt8tgibkYxlcaP8YLial5gmygcvALCosrOrff6tibEKg7KL1IdLlQ6mmxqOxeWScxbA/0',
                    User: 'DannyJones',
                    Address: '武汉市风度柏林',
                }, {
                    Title: '测试3',
                    ImagePath: 'https://wx.qlogo.cn/mmopen/vi_32/X2m0EnlKoUMQsKE0OBSJyqOt8tgibkYxlcaP8YLial5gmygcvALCosrOrff6tibEKg7KL1IdLlQ6mmxqOxeWScxbA/0',
                    User: 'DannyJones',
                    Address: '武汉市风度柏林',
                },
                ]
            }
        },
        methods: {
            handleRemove(tab) {
                console.log(tab);
            },
            handleClick(tab) {
                console.log(tab);
            }
        }
    };
</script>

<style>
    /* .el-carousel__item h3 {
        color: #475669;
        font-size: 14px;
        opacity: 0.75;
        line-height: 200px;
        margin: 0;
    } */

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
</style>