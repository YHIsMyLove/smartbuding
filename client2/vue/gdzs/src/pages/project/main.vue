<template>
  <f7-page>
    <f7-navbar>
        <f7-nav-left>
            <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" panel-open="left"></f7-link>
        </f7-nav-left>
        <f7-nav-right>
            <f7-link icon-if-ios="f7:menu" icon-if-md="material:crop_free"></f7-link>
        </f7-nav-right>
    </f7-navbar>
    <div class="head display-flex padding" slot="fixed">
        <div>
            <span class="app-name">项目助手</span><span>v1.0</span>
        </div>
        <div class="project-name">
            <f7-link href="/project/list/">柳州项目</f7-link>
        </div>
    </div>
    <div class="content">
        <f7-row>
            <f7-col width="33">
                <f7-link href="/cameras/main/">
                    <img src="static/imgs/现场监管.png" alt="">
                    <p>现场管理</p>
                </f7-link>
            </f7-col>
            <f7-col width="33">
                <f7-link href="/workers/main/">
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
                    <!-- <img id="img-add" src="static/imgs/创建70x70.png" alt=""> -->
                    <f7-icon f7="add" size="64" color="gray"></f7-icon>
                </f7-link>
            </f7-col>
        </f7-row>
    </div>
  </f7-page>
</template>
<script>
export default {};
</script>
<style scoped>
.head {
  position: absolute;
  top: 56px;
  left: 0;
  right: 0;
  height: 150px;
  background-color: #2196f3;
  color: #fff;
  z-index: 500;
  flex-direction: column;
  justify-content:flex-end;
}

.head-content {
  height: 100%;
}

.content {
  position: absolute;
  top: 238px;
  bottom: 56px;
  left: 0;
  right: 0;
  text-align: center;
}

.app-name
{
    font-size: 1.5em;
}

a {
  display: block;
}

.content div[class*="col"] {
  text-align: center;
  margin-top: 10px;
}

.content img
{
    width: 64px;
    height: 64px;
}

.project-name a
{
    color: #fff;
}

#img-add
{
    color: gray;
}
</style>

