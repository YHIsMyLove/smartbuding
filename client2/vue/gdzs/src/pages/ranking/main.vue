<template>
    <f7-page tabs :page-content="false">
        <f7-navbar title="排行榜" back-link="Back"></f7-navbar>
        <f7-toolbar tabbar>
            <f7-link text="排行榜" tab-link="#ranking"></f7-link>
            <f7-link text="红榜" tab-link="#redRanking"></f7-link>
            <f7-link text="黑榜" tab-link="#blackRanking"></f7-link>
        </f7-toolbar>
        <f7-page-content id="ranking" tab tab-active>
            <div class="padding">
                <f7-row>
                    <f7-col width="20">
                        <img id="img-head" src="static/imgs/个人信息头像180x180.png" alt="">
                    </f7-col>
                    <f7-col width="80">
                        <f7-row>
                            <f7-col width="100">
                                <div class="display-flex align-items-center">
                                    <span class="font-big">张三</span>
                                    <span class="my-tag font-little">第四名</span>
                                </div>
                            </f7-col>
                            <f7-col width="100">
                                <div class="display-flex justify-content-space-between align-items-center">
                                    <div class="">积分：110</div>
                                    <f7-link icon-f7="heart_fill" icon-color="red">(30)</f7-link>
                                    <!-- <img class="flex-shrink-0" src="static/imgs/赞30x30.png" alt="">
                                    <div class="flex-shrink-0">(30)</div> -->
                                </div>
                            </f7-col>
                        </f7-row>
                    </f7-col>
                </f7-row>
            </div>
            <div>
                <f7-list media-list>
                    <f7-list-item title="陈小春" after="积分：310">
                        <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="" slot="media">
                        <div class="align-right" slot="footer">
                            <f7-link icon-f7="heart_fill" icon-color="red">(06)</f7-link>
                        </div>
                    </f7-list-item>
                    <f7-list-item title="陈小春" after="积分：310">
                        <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="" slot="media">
                        <div class="align-right" slot="footer">
                            <f7-link icon-f7="heart_fill" icon-color="red">(06)</f7-link>
                        </div>
                    </f7-list-item>
                </f7-list>
            </div>
        </f7-page-content>
        <f7-page-content id="redRanking" tab>
            <div>
                <div class="my-divider" style="margin-bottom:8px"></div>
                <div class="display-flex justify-content-space-between align-items-center padding-left padding-right">
                    <div>2018年5月24日</div>
                    <div class="display-flex align-items-center">
                        <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="" slot="media">
                        <div>陈小春</div>
                    </div>
                </div>
                <div class="my-divider" style="margin-top:8px"></div>
                <div class="padding-left padding-right">
                    <div class="display-flex justify-content-space-between align-items-center">
                        <p><span class="text-color-red">安全文明奖</span>(ID:01668)</p>
                        <p>积分+10</p>
                    </div>
                    <p>阿斯顿发生发达时代发生的方式发送到发送到发送到发送到发送打个电话给返回法国恢复扶桑岛国损公肥私</p>
                    <div class="align-right">
                        <f7-link icon-f7="heart_fill" icon-color="red">(06)</f7-link>
                    </div>
                </div>
                <div class="my-divider" style="margin:8px 0"></div>
            </div>
            <div>
                <div class="display-flex justify-content-space-between align-items-center padding-left padding-right">
                    <div>2018年5月24日</div>
                    <div class="display-flex align-items-center">
                        <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="" slot="media">
                        <div>陈小春</div>
                    </div>
                </div>
                <div class="my-divider" style="margin-top:8px"></div>
                <div class="padding-left padding-right">
                    <div class="display-flex justify-content-space-between align-items-center">
                        <p><span class="text-color-red">安全文明奖</span>(ID:01668)</p>
                        <p>积分+10</p>
                    </div>
                    <p>阿斯顿发生发达时代发生的方式发送到发送到发送到发送到发送打个电话给返回法国恢复扶桑岛国损公肥私</p>
                    <div class="align-right">
                        <f7-link icon-f7="heart_fill" icon-color="red">(06)</f7-link>
                    </div>
                </div>
                <div class="my-divider" style="margin:8px 0"></div>
            </div>
            <!-- <f7-list>
                <f7-list-item>
                    <f7-list-item-cell>
                        <div class="display-flex justify-content-space-between align-items-center">
                            <div>2018年5月24日</div>
                            <div class="display-flex align-items-center">
                                <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="" slot="media">
                                <div>陈小春</div>
                            </div>
                        </div>
                        <div class="my-divider" style="margin-top:8px"></div>
                        <div class="display-flex justify-content-space-between align-items-center">
                            <p><span class="text-color-red">安全文明奖</span>(ID:01668)</p>
                            <p>积分+10</p>
                        </div>
                        <p>阿斯顿发生发达时代发生的方式发送到发送到发送到发送到发送打个电话给返回法国恢复扶桑岛国损公肥私</p>
                        <div class="align-right">
                            <f7-link icon-f7="heart_fill" icon-color="red">(06)</f7-link>
                        </div>
                    </f7-list-item-cell>
                </f7-list-item>
                <f7-list-item>
                    <f7-list-item-cell>
                        <div class="display-flex justify-content-space-between align-items-center">
                            <div>2018年5月24日</div>
                            <div class="display-flex align-items-center">
                                <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="" slot="media">
                                <div>陈小春</div>
                            </div>
                        </div>
                        <div class="my-divider" style="margin-top:8px"></div>
                        <div class="display-flex justify-content-space-between align-items-center">
                            <p><span class="text-color-red">安全文明奖</span>(ID:01668)</p>
                            <p>积分+10</p>
                        </div>
                        <p>阿斯顿发生发达时代发生的方式发送到发送到发送到发送到发送打个电话给返回法国恢复扶桑岛国损公肥私</p>
                        <div class="align-right">
                            <f7-link icon-f7="heart_fill" icon-color="red">(06)</f7-link>
                        </div>
                    </f7-list-item-cell>
                </f7-list-item>
            </f7-list> -->
        </f7-page-content>
        <f7-page-content id="blackRanking" tab>
            <div>
                <div class="my-divider" style="margin-bottom:8px"></div>
                <div class="display-flex justify-content-space-between align-items-center padding-left padding-right">
                    <div>2018年5月24日</div>
                    <div class="display-flex align-items-center">
                        <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="" slot="media">
                        <div>陈小春</div>
                    </div>
                </div>
                <div class="my-divider" style="margin-top:8px"></div>
                <div class="padding-left padding-right">
                    <div class="display-flex justify-content-space-between align-items-center">
                        <p><span class="text-color-red">安全文明奖</span>(ID:01668)</p>
                        <p>积分+10</p>
                    </div>
                    <p>阿斯顿发生发达时代发生的方式发送到发送到发送到发送到发送打个电话给返回法国恢复扶桑岛国损公肥私</p>
                    <div class="align-right">
                        <f7-link icon-f7="heart_fill" icon-color="red">(06)</f7-link>
                    </div>
                </div>
                <div class="my-divider" style="margin:8px 0"></div>
            </div>
        </f7-page-content>
    </f7-page>
</template>
<script>
export default {
    data(){
        return {
            
        }
    }
}
</script>
<style scoped>
#img-head
{
    width: 100%;
}
</style>


