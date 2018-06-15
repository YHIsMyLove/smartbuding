<template>
  <f7-page>
    <f7-navbar title="门禁记录">
    </f7-navbar>
    <div class="page-data-filter display-flex justify-content-center" slot="fixed">
      <f7-date-picker></f7-date-picker>
    </div>
    <div id="content">
      <div class="my-divider" style="margin-top: 16px;"></div>
      <div style="text-align:center;padding: 16px 0;">
        <f7-row no-gap>
          <f7-col width="40">
            
          </f7-col>
          <f7-col width="30">
            <div style="border-left:1px solid #ccc;border-right:1px solid #ccc;">
              <f7-row>
                <f7-col width="100">
                  <f7-link text="950"></f7-link>
                </f7-col>
                <f7-col width="100">进场人次</f7-col>
                <f7-col width="100">
                  <div style="height:32px;"></div>
                </f7-col>
                <f7-col width="100">453</f7-col>
                <f7-col width="100">出场人次</f7-col>
              </f7-row>
            </div>
          </f7-col>
          <f7-col width="30">
            <f7-row>
              <f7-col width="100">950</f7-col>
              <f7-col width="100">进场人数</f7-col>
              <f7-col width="100">
                <div style="height:32px;"></div>
              </f7-col>
              <f7-col width="100">453</f7-col>
              <f7-col width="100">现场人数</f7-col>
            </f7-row>
          </f7-col>
        </f7-row>
      </div>
      <div class="my-divider" style="margin:0 16px;"></div>
      <p class="text-align-center">今日出场人数934人  注册人数950人</p>
      <div class="my-divider"></div>
      <f7-list>
        <f7-list-item title="统计列表" link="/doors/statistics/"></f7-list-item>
      </f7-list>
      <f7-list>
        <f7-list-item>
          <f7-list-item-cell>
            <f7-list-item-row>
              <f7-list-item-cell>1#门</f7-list-item-cell>
              <f7-list-item-cell>陈小春</f7-list-item-cell>
              <f7-list-item-cell>17:56:16</f7-list-item-cell>
              <f7-list-item-cell>出场</f7-list-item-cell>
            </f7-list-item-row>
          </f7-list-item-cell>
        </f7-list-item>
        <f7-list-item>
          <f7-list-item-cell>
            <f7-list-item-row>
              <f7-list-item-cell>2#门</f7-list-item-cell>
              <f7-list-item-cell>陈小春</f7-list-item-cell>
              <f7-list-item-cell>08:56:16</f7-list-item-cell>
              <f7-list-item-cell>进场</f7-list-item-cell>
            </f7-list-item-row>
          </f7-list-item-cell>
        </f7-list-item>
      </f7-list>
    </div>
  </f7-page>
</template>
<script>
export default {}
</script>
<style scoped>
#content
{
  position: absolute;
  top: 104px;
  left: 0;
  right: 0;
}
</style>

