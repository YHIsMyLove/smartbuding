<template>
    <f7-page>
        <f7-navbar title="统计列表" back-link="Back"></f7-navbar>
        <div class="page-data-filter display-flex justify-content-space-around" slot="fixed">
            <f7-date-picker></f7-date-picker>
            <div class="display-flex">
                <f7-data-picker></f7-data-picker>
                <f7-data-picker></f7-data-picker>
            </div>
        </div>
        <div style="padding:64px 16px 16px 16px;">
            <f7-row>
                <f7-col width="100">
                    <div>部门：工程部 班组：打桩队</div>
                </f7-col>
                <f7-col width="40">
                    <div>共计人数：5（人）</div>
                </f7-col>
                <f7-col width="60">
                    <div style="text-align:right">共计时长：1588（分钟）</div>
                </f7-col>
            </f7-row>
        </div>
        <div class="my-divider"></div>
        <div style="border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;padding:16px;margin:8px 0;">
            <f7-row>
                <f7-col width="20">
                    <img style="width:100%" src="static/imgs/个人信息头像180x180.png" alt="">
                </f7-col>
                <f7-col width="80">
                    <div class="display-flex justify-content-space-between align-items-center">
                        <div class="display-flex align-items-center">
                            <span class="font-big">张三</span>
                            <span class="my-tag font-little">组长</span>
                        </div>
                        <div class="display-flex justify-content-end align-items-center">工号：007</div>
                    </div>
                    <div class="display-flex justify-content-space-between">
                        <div>单位：四川羽灵建筑劳务有限公司</div>
                        <div>时长：202（分钟）</div>
                    </div>
                </f7-col>
            </f7-row>
            <div class="my-divider"></div>
            <p class="text-align-right">进场时间：2017-04-28 08:36 出场时间：2017-04-28 17:36</p>
        </div>
        <div style="border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;padding:16px;margin:8px 0;">
            <f7-row>
                <f7-col width="20">
                    <img style="width:100%" src="static/imgs/个人信息头像180x180.png" alt="">
                </f7-col>
                <f7-col width="80">
                    <div class="display-flex justify-content-space-between align-items-center">
                        <div class="display-flex align-items-center">
                            <span class="font-big">张三</span>
                            <span class="my-tag font-little">组长</span>
                        </div>
                        <div class="display-flex justify-content-end align-items-center">工号：007</div>
                    </div>
                    <div class="display-flex justify-content-space-between">
                        <div>单位：四川羽灵建筑劳务有限公司</div>
                        <div>时长：202（分钟）</div>
                    </div>
                </f7-col>
            </f7-row>
            <div class="my-divider"></div>
            <p class="text-align-right">进场时间：2017-04-28 08:36 出场时间：2017-04-28 17:36</p>
        </div>
    </f7-page>
</template>
<script>
export default {
    
}
</script>
<style scoped>
</style>

