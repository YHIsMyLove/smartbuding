<template>
    <f7-page>
        <f7-navbar title="三级动火作业申请" back-link="Back"></f7-navbar>
        <f7-list no-hairlines-md>
            <f7-list-item>
                <f7-input type="textarea" resizable placeholder="请输入拒绝理由"></f7-input>
            </f7-list-item>
        </f7-list>
        <f7-button back fill color="red">确认拒绝</f7-button>
    </f7-page>
</template>
<style scoped>
a[class*="button-fill"]
{
    position: absolute;
    left: 0;
    right: 0;
    bottom: 0;
}
</style>

