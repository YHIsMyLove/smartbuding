<template>
    <f7-page>
        <f7-navbar title="人员定位" back-link="Back" :no-shadow="true"></f7-navbar>
        <div id="map"></div>
    </f7-page>
</template>
<script>
export default {
  mounted() {
    console.log("location page mounted");
    // 百度地图API功能
    var map = new BMap.Map("map"); // 创建Map实例
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
      "static/imgs/人员定位地图图标70x70.png",
      new BMap.Size(70, 70)
    );
    myIcon.setImageSize(new BMap.Size(32, 32));
    var marker = new BMap.Marker(pt, { icon: myIcon }); // 创建标注
    // marker.addEventListener("click", this.openSheet);
    map.addOverlay(marker); // 将标注添加到地图中

    pt = new BMap.Point(116.517, 39.809);
    myIcon = new BMap.Icon(
      "static/imgs/人员定位地图图标70x70.png",
      new BMap.Size(70, 70)
    );
    myIcon.setImageSize(new BMap.Size(32, 32));
    marker = new BMap.Marker(pt, { icon: myIcon }); // 创建标注
    // marker.addEventListener("click", this.openSheet);
    map.addOverlay(marker); // 将标注添加到地图中
  },
  methods: {}
};
</script>
<style scoped>
#map {
  width: 100%;
  height: 100%;
  overflow: hidden;
  margin: 0;
  font-family: "微软雅黑";
}
#sheet {
  background-color: #fafafa;
  height: 380px;
}
.block {
  background-color: white;
}
.block a {
  margin-left: 16px;
}
#top {
  margin-top: 0px;
  margin-bottom: 0px;
}
#bottom {
  margin-top: 16px;
  margin-bottom: 16px;
}
</style>