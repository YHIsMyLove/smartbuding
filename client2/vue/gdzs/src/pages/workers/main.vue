<template>
    <f7-page tabs :page-content="false">
        <f7-page-content id="page-contacts" tab tab-active>
            <workers-contacts></workers-contacts>
        </f7-page-content>
        <f7-page-content id="page-location" tab>
            <workers-location></workers-location>
        </f7-page-content>
        <f7-toolbar tabbar labels bottom-md>
            <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" text="通讯录" tab-link="#page-contacts"></f7-link>
            <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" text="人员定位" tab-link="#page-location"></f7-link>
        </f7-toolbar>
    </f7-page>
</template>
