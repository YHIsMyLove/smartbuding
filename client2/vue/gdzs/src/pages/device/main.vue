<template>
    <f7-page>
        <f7-navbar title="机械设备监测" back-link="Back" :no-shadow="true"></f7-navbar>
        <div id="allmap"></div>
        <f7-sheet id="sheet">
            <f7-block id="top">
                <div class="text-align-center" style="width:100%">
                    <img class="img-middle" src="static/imgs/机械设备监测地图图标70x70.png" alt="">
                    <div>11#号塔机</div>
                </div>
                <div class="my-divider"></div>
                <div class="text-align-center font-middle text-color-red">载重警报</div>
                <div class="text-align-center">2017-4-28 20:47</div>
                <div class="my-divider"></div>
                <div class="display-flex align-items-center" style="padding: 8px 0;">
                    <div style="flex: 1 1 0">责任人</div>
                    <f7-chip text="未查看" color="red"></f7-chip>
                    <f7-link icon-f7="phone_round"></f7-link>
                </div>
                <div class="my-divider"></div>
                <div class="display-flex align-items-center" style="padding: 8px 0;">
                    <div style="flex: 1 1 0">驾驶员</div>
                    <f7-chip text="已查看" color="green"></f7-chip>
                    <f7-link icon-f7="phone_round"></f7-link>
                </div>
                <div class="my-divider"></div>
                <div class="display-flex align-items-center" style="padding: 8px 0;">
                    <div style="flex: 1 1 0">指挥员</div>
                    <f7-chip text="已查看" color="green"></f7-chip>
                    <f7-link icon-f7="phone_round"></f7-link>
                </div>
            </f7-block>
            <f7-block id="bottom">
                <div class="display-flex align-items-center justify-content-center" style="padding: 8px 0">
                    <img src="static/imgs/机械设备租约信号灯30x30.png" alt="">
                    <div style="margin-left: 8px;">设备租用时间：2017-01-01至2017-12-31</div>
                </div>
            </f7-block>
            <f7-button id="btn" fill color="red" big href="/device/detail/" @click="closeSheet()">查看详情</f7-button>
        </f7-sheet>
    </f7-page>
</template>
<script>
export default {
  mounted() {
    // 百度地图API功能
    var map = new BMap.Map("allmap"); // 创建Map实例
    map.centerAndZoom(new BMap.Point(116.404, 39.915), 11); // 初始化地图,设置中心点坐标和地图级别
    //添加地图类型控件
    // map.addControl(
    //   new BMap.MapTypeControl({
    //     mapTypes: [BMAP_NORMAL_MAP]
    //   })
    // );
    map.setCurrentCity("北京"); // 设置地图显示的城市 此项是必须设置的
    // map.enableScrollWheelZoom(true); //开启鼠标滚轮缩放

    var pt = new BMap.Point(116.417, 39.909);
    var myIcon = new BMap.Icon(
      "static/imgs/机械设备监测地图图标2_70x70.png",
      new BMap.Size(70, 70)
    );
    myIcon.setImageSize(new BMap.Size(32, 32));
    var marker = new BMap.Marker(pt, { icon: myIcon }); // 创建标注
    marker.addEventListener("click", this.openSheet);
    map.addOverlay(marker); // 将标注添加到地图中

    pt = new BMap.Point(116.517, 39.809);
    myIcon = new BMap.Icon(
      "static/imgs/机械设备监测地图图标70x70.png",
      new BMap.Size(70, 70)
    );
    myIcon.setImageSize(new BMap.Size(32, 32));
    marker = new BMap.Marker(pt, { icon: myIcon }); // 创建标注
    marker.addEventListener("click", this.openSheet);
    map.addOverlay(marker); // 将标注添加到地图中
  },
  methods:
  {
      openSheet()
      {
          this.$f7.sheet.open("#sheet", true);
      },
      closeSheet()
      {
          this.$f7.sheet.close("#sheet", true);
      }
  }
};
</script>
<style scoped>
#allmap {
  width: 100%;
  height: 100%;
  overflow: hidden;
  margin: 0;
  font-family: "微软雅黑";
}
#sheet
{
    background-color: #fafafa;
    height: 380px;
}
.block
{
    background-color: white;
}
.block a
{
    margin-left: 16px;
}
#top
{
    margin-top: 0px;
    margin-bottom: 0px;
}
#bottom
{
    margin-top: 16px;
    margin-bottom: 16px;
}
div.chip {
    font-size: 12px;
    height: 24px;
}

#btn {
    position: absolute;
    bottom: 0;
    left: 0;
    right: 0;
}
</style>

