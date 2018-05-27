<template>
    <f7-page tabs :page-content="false">
        <f7-navbar title="特种作业审批" back-link="Back">
            <f7-nav-right>
                <f7-link icon-if-ios="f7:menu" icon-if-md="material:add"></f7-link>
            </f7-nav-right>
        </f7-navbar>
        <f7-page-content id="create-by-me" tab tab-active>
            <f7-list media-list>
                <f7-list-item link="#">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <span>张三</span>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div>
                            <span>一级动火作业申请</span>
                        </div>
                    </div>
                </f7-list-item>
                <f7-list-item link="#">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <span>张三</span>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div>
                            <span>一级动火作业申请</span>
                        </div>
                    </div>
                </f7-list-item>
            </f7-list>
        </f7-page-content>
        <f7-page-content id="check-by-me" tab>
            <f7-list media-list>
                <f7-list-item link="#">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <span>张三</span>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div>
                            <span>一级动火作业申请</span>
                        </div>
                    </div>
                </f7-list-item>
                <f7-list-item link="#">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <span>张三</span>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div>
                            <span>一级动火作业申请</span>
                        </div>
                    </div>
                </f7-list-item>
            </f7-list>
        </f7-page-content>
        <f7-page-content id="send-to-me" tab>
            <f7-list media-list>
                <f7-list-item link="#">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <span>张三</span>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div>
                            <span>一级动火作业申请</span>
                        </div>
                    </div>
                </f7-list-item>
                <f7-list-item link="#">
                    <div slot="inner">
                        <div class="my-divider" style="margin:8px 0;"></div>
                        <div class="display-flex justify-content-flex-start align-items-center">
                            <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                            <span>张三</span>
                            <div style="flex: auto;text-align:right">
                                <span>申请时间：2018-05-23 08:32</span>
                            </div>
                        </div>
                    </div>
                    <div slot="after">
                        <span style="color:#0f0a">已通过</span>
                    </div>
                    <div class="display-flex justify-content-start align-items-center" slot="before-title">
                        <img class="img-little" src="static/imgs/动火作业70x70.png" alt="">
                        <div>
                            <span>一级动火作业申请</span>
                        </div>
                    </div>
                </f7-list-item>
            </f7-list>
        </f7-page-content>
        <f7-toolbar tabbar>
            <f7-link text="我发起的" tab-link="#create-by-me"></f7-link>
            <f7-link text="我审批的" tab-link="#check-by-me"></f7-link>
            <f7-link text="抄送我的" tab-link="#send-to-me"></f7-link>
        </f7-toolbar>
    </f7-page>
</template>
