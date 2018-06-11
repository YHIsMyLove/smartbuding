<template>
    <f7-page>
        <f7-navbar title="动火作业申请" back-link="Back"></f7-navbar>
        <f7-block>
            <div class="display-flex align-items-center">
                <img class="img-little" src="static\imgs\个人信息80x80.png" alt="">
                <div class="display-flex" style="flex-direction: column;flex: 1 1 0;margin-left: 16px;">
                    <div>张三</div>
                    <div>待我审批</div>
                </div>
                <div>已拒绝</div>
            </div>
            <div class="my-divider margin-top margin-bottom"></div>
            <f7-row>
                <f7-col width="20">
                    <div class="text-align-right">申请单位：</div>
                </f7-col>
                <f7-col width="80">
                    <div>打桩队</div>
                </f7-col>
                <f7-col width="20">
                    <div class="text-align-right">动火级别：</div>
                </f7-col>
                <f7-col width="80">
                    <div>三级</div>
                </f7-col>
                <f7-col width="20">
                    <div class="text-align-right">动火种类：</div>
                </f7-col>
                <f7-col width="80">
                    <div>电焊</div>
                </f7-col>
                <f7-col width="20">
                    <div class="text-align-right">动火部位：</div>
                </f7-col>
                <f7-col width="80">
                    <div>打桩焊接</div>
                </f7-col>
            </f7-row>
        </f7-block>
        <f7-block>
            <div>动火原因、防火主要安全措施和配备的消防器材：</div>
            <div>1、动火作业应注意周围易燃物。</div>
            <div>2、焊接作业时，做好安全防护措施。</div>
            <div>3、施工现场应配备2个灭火器。</div>
            <div>4、动火前应指定现场安全责任人。</div>
        </f7-block>
        <f7-block>
            <div>现场照片</div>
            <f7-icon f7="add"></f7-icon>
        </f7-block>
        <f7-block>
            <f7-row>
                <f7-col width="10">
                    <img class="img-little" src="static\imgs\个人信息80x80.png" alt="">
                </f7-col>
                <f7-col width="15">
                    <div class="text-align-center">张三</div>
                </f7-col>
                <f7-col width="15">
                    <div class="text-align-center">申请人</div>
                </f7-col>
                <f7-col width="20">
                    <div class="text-align-center"><f7-icon size="16px" color="green" f7="check_round"></f7-icon></div>
                </f7-col>
                <f7-col width="40">
                    <div class="text-align-right">2018-06-06 21:01:00</div>
                </f7-col>
            </f7-row>
            <f7-row>
                <f7-col width="10">
                    <div class="text-align-center"><f7-icon size="16px" color="#fafafa" f7="down"></f7-icon></div>
                </f7-col>
                <f7-col width="90">
                    <div class="my-divider"></div>
                </f7-col>
            </f7-row>
            <f7-row>
                <f7-col width="10">
                    <img class="img-little" src="static\imgs\个人信息80x80.png" alt="">
                </f7-col>
                <f7-col width="15">
                    <div class="text-align-center">张三</div>
                </f7-col>
                <f7-col width="15">
                    <div class="text-align-center">申请人</div>
                </f7-col>
                <f7-col width="20">
                    <div class="text-align-center"><f7-icon size="16px" color="blue" f7="delete_round"></f7-icon></div>
                </f7-col>
                <f7-col width="40">
                    <div class="text-align-right">2018-06-06 21:01:00</div>
                </f7-col>
            </f7-row>
            <f7-row>
                <f7-col width="10">
                    <div class="text-align-center"><f7-icon size="16px" color="#fafafa" f7="down"></f7-icon></div>
                </f7-col>
                <f7-col width="90">
                    <div class="my-divider"></div>
                </f7-col>
            </f7-row>
            <f7-row>
                <f7-col width="10">
                    <img class="img-little" src="static\imgs\个人信息80x80.png" alt="">
                </f7-col>
                <f7-col width="15">
                    <div class="text-align-center">张三</div>
                </f7-col>
                <f7-col width="15">
                    <div class="text-align-center">申请人</div>
                </f7-col>
                <f7-col width="20">
                    <div class="text-align-center"><f7-icon size="16px" color="red" f7="close_round"></f7-icon></div>
                </f7-col>
                <f7-col width="40">
                    <div class="text-align-right">2018-06-06 21:01:00</div>
                </f7-col>
            </f7-row>
        </f7-block>
        <f7-toolbar bottom-md>
            <f7-link href="/especially-task/commit-endorse/">同意签字</f7-link>
            <f7-link href="/especially-task/commit-refuse/">拒绝签字</f7-link>
            <f7-link @click="showDialog()">撤销申请</f7-link>
            <f7-link>转接他人</f7-link>
        </f7-toolbar>
    </f7-page>
</template>
<script>
export default {
    methods:{
        showDialog()
        {
            this.$f7.dialog.confirm("你确定要取消申请吗？", "提示", ()=>{}, ()=>{});
        }
    }
}
</script>

<style scoped>
.page
{
    background-color: #fafafa;
}
.block
{
    background-color: white;
    margin-top: 16px;
    margin-bottom: 16px;
    padding-top: 16px;
    padding-bottom: 16px;
}
div[class*="row"]
{
    align-items: center;
}
</style>

