<template>
    <f7-page>
        <f7-navbar title="统计列表" back-link="Back"></f7-navbar>
        <f7-toolbar>
            <div class="display-flex align-items-center wmp padding-left padding-right">
                <f7-input type="date"></f7-input>
                <div style="flex: 1 1 0"></div>
                <f7-input type="select">
                    <option value="设计部">设计部</option>
                    <option value="运营部">运营部</option>
                </f7-input>
                <f7-input type="select">
                    <option value="打桩队">打桩队</option>
                    <option value="电焊队">电焊队</option>
                </f7-input>
            </div>
        </f7-toolbar>
        <div style="padding:16px 16px 16px 16px;">
            <f7-row>
                <f7-col width="100">
                    <div>部门：工程部 班组：打桩队</div>
                </f7-col>
                <f7-col width="40">
                    <div>共计人数：5(人)</div>
                </f7-col>
                <f7-col width="60">
                    <div style="text-align:right">共计时长：1588(分钟)</div>
                </f7-col>
            </f7-row>
        </div>
        <div class="my-divider"></div>
        <f7-list>
            <f7-list-item>
                <div class="mp">
                    <div class="display-flex align-items-center">
                        <img class="img-little" src="static/imgs/个人信息头像180x180.png" alt="">
                        <div class="font-middle" style="margin-left: 8px;">张三</div>
                        <div class="my-tag font-little" style="margin-left: 8px">组长</div>
                        <div style="flex: 1 1 0"></div>
                        <div class="font-little">时长：202(分钟)</div>
                    </div>
                    <div class="my-divider" style="margin: 8px 0;"></div>
                    <div class="font-little text-align-right" style="flex-direction: column">
                        <div>
                            进场时间：2017-04-28 08:36
                        </div>
                        <div>
                            出场时间：2017-04-28 17:36
                        </div>
                    </div>
                </div>
            </f7-list-item>
        </f7-list>
    </f7-page>
</template>
<script>
export default {
    data() {
        return {
            
        }
    }
}
</script>
<style scoped>
.md .toolbar:after {
  height: 1px;
  background-color: #ccc;
}

.md .toolbar {
  background-color: #fff;
  color: rgba(0, 0, 0);
}
.md .item-input-wrap {
    min-height: 0px;
}
</style>

