// 申请动火作业审批页面
<template>
    <f7-page>
        <f7-navbar title="申请动火作业" back-link="Back" :no-shadow="true"></f7-navbar>
        <f7-list inline-labels no-hairlines-md>
            <f7-list-item>
                <f7-label>申请单位</f7-label>
                <f7-input type="text" placeholder="请输入"></f7-input>
            </f7-list-item>

            <f7-list-item>
                <f7-label>动火级别</f7-label>
                <f7-input type="select" placeholder="请选择">
                    <option value="1">级别一</option>
                    <option value="2">级别二</option>
                </f7-input>
            </f7-list-item>

            <f7-list-item>
                <f7-label>动火种类</f7-label>
                <f7-input type="select" placeholder="请选择">
                    <option value="1">种类一</option>
                    <option value="2">种类二</option>
                </f7-input>
            </f7-list-item>

            <f7-list-item>
                <f7-label>动火部位</f7-label>
                <f7-input type="select" placeholder="请选择">
                    <option value="1">部位一</option>
                    <option value="2">部位二</option>
                </f7-input>
            </f7-list-item>

            <f7-list-item>
                <f7-label>开始时间</f7-label>
                <f7-input type="datetime-local" placeholder="请选择"></f7-input>
            </f7-list-item>

            <f7-list-item>
                <f7-label>结束时间</f7-label>
                <f7-input type="datetime-local" placeholder="请选择"></f7-input>
            </f7-list-item>

            <f7-list-item>
                <f7-label>动火原因、防火主要安全措施和配备的消防器材</f7-label>
                <f7-input type="textarea" resizable placeholder="请输入"></f7-input>
            </f7-list-item>

            <f7-list-item>
                <f7-label>现场照片</f7-label>
                <f7-button icon-f7="add" sheet-open="#actions"></f7-button>
            </f7-list-item>

            <f7-list-item>
                <f7-label>审批人</f7-label>
                <f7-button icon-f7="add" href="/especially-task/test-params/"></f7-button>
            </f7-list-item>

            <f7-list-item>
                <f7-label>监管人</f7-label>
                <f7-button icon-f7="add" href="/workers/"></f7-button>
            </f7-list-item>

        </f7-list>
        <!-- Actions -->
        <f7-actions id="actions">
            <!-- Actions Group -->
            <f7-actions-group>
                <!-- Actions Label -->
                <f7-actions-label>上传照片</f7-actions-label>
                <!-- Actions Buttons -->
                <f7-actions-button>拍照</f7-actions-button>
                <f7-actions-button>从相册选择</f7-actions-button>
            </f7-actions-group>
            <!-- Another Group -->
            <f7-actions-group>
                <!-- Cancel/Close Button -->
                <f7-actions-button color="red" bold>取消</f7-actions-button>
            </f7-actions-group>
        </f7-actions>
        <f7-toolbar bottom-md>
            <f7-link>签字提交</f7-link>
            <f7-link>撤销申请</f7-link>
        </f7-toolbar>
    </f7-page>
</template>
<script>
export default {
    on: {
        pageAfterIn(event, pageData) {
            console.log(event);
            console.log(pageData);
            console.log("contact detail page after in");
        },
    }
}
</script>

<style scoped>
.md .toolbar::after {
    height: 0;
}
</style>

