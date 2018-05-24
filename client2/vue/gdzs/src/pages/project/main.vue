<template>
  <f7-page page-content="false">
    <f7-navbar>
        <f7-nav-left>
            <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" panel-open="left"></f7-link>
        </f7-nav-left>
        <f7-nav-right>
            <f7-link icon-if-ios="f7:menu" icon-if-md="material:crop_free"></f7-link>
        </f7-nav-right>
    </f7-navbar>
    <div class="head" slot="fixed">
        <div class="head-content display-flex justify-content-left">
            <div class="flex-shrink-0">Item 1</div>
            <div class="align-self-center">Item 2</div>
            <div class="align-self-flex-end">Item 3</div>
        </div>
    </div>
    <div class="content">
        <div class="display-flex  justify-content-space-around align-items-center">
            <div class="flex-shrink-0">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/现场监管.png" alt="">
                    <p>现场管理</p>
                </f7-link>
            </div>
            <div class="flex-shrink-5">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/人员监管.png" alt="">
                    <p>人员监管</p>
                </f7-link>
            </div>
            <div class="flex-shrink-10">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/环境监测.png" alt="">
                    <p>环境监测</p>
                </f7-link>
            </div>
        </div>
        <!-- <f7-row>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/现场监管.png" alt="">
                    <p>现场管理</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/人员监管.png" alt="">
                    <p>人员监管</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/环境监测.png" alt="">
                    <p>环境监测</p>
                </f7-link>
            </f7-col>
        </f7-row>
        <f7-row>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/机械设施监控.png" alt="">
                    <p>机械设备监测</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/日历事件.png" alt="">
                    <p>日历事件</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/排行.png" alt="">
                    <p>排行榜</p>
                </f7-link>
            </f7-col>
        </f7-row>
        <f7-row>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/特种作业.png" alt="">
                    <p>特种作业审批</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/大事件.png" alt="">
                    <p>大事件</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/现场监管.png" alt="">
                </f7-link>
            </f7-col>
        </f7-row> -->
    </div>
  </f7-page>
</template>
<script>
export default {}
</script>
<style scoped>
    .head
    {
        position: absolute;
        top: 56px;
        left: 0;
        right: 0;
        height: 150px;
        background-color: #2196f3;
        color: #fff;
        z-index: 500;
    }

    .head-content
    {
        height: 100%;
    }

    .content
    {
        position: absolute;
        top: 206px;
        bottom: 56px;
        left: 0;
        right: 0;
        text-align: center;
    }

    a
    {
        display: block;
    }

    .content div[class*="col"]
    {
        text-align: center;
        margin-top: 10px;
    }
</style>

