<template>
    <f7-page>
      <f7-navbar :title="navbarTitle" back-link="Back" :no-shadow="true"></f7-navbar>
      <f7-tabs>
        <f7-tab id="page-contacts" tab-active @tab:show="onTabContactsShow" @tab:hide="onTabContactsHide">
          <f7-toolbar>
            <select name="" id="">
              <option value="设计部">设计部</option>
              <option value="施工部">施工部</option>
            </select>
            <select>
              <option value="打桩组">打桩组</option>
              <option value="电焊组">电焊组</option>
            </select>
          </f7-toolbar>
          <f7-list>
            <f7-list-item v-for="(contact, key) in contacts" :key="key" :title="contact.UserName" link @click="showDetail(contact, $event)"></f7-list-item>
          </f7-list>
        </f7-tab>
        <f7-tab id="page-location" @tab:show="onTabLocationShow" @tab:hide="onTabLocationHide">
          <div id="allmap"></div>
        </f7-tab>
      </f7-tabs>
      <f7-toolbar tabbar labels bottom-md>
        <f7-link :icon="linkContactsClass" text="通讯录" tab-link="#page-contacts"></f7-link>
        <f7-link :icon="linkLocationClass" text="人员定位" tab-link="#page-location"></f7-link>
      </f7-toolbar>
    </f7-page>
</template>
<script>
import context from "../../service-context.js";
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
  on: {
    pageInit(event, pageData) {
      console.log("workers page init");
      this.fetchContacts();
    },
    pageAfterIn(event, pageData) {
      console.log("workers page after in");
    },
    pageAfterOut(event, pageData) {
      //alert("page after out");
    }
  },
  data() {
    return {
      //   contacts: {
      //     A: [
      //       "Aaron",
      //       "Abbie",
      //       "Adam",
      //       "Adele",
      //       "Agatha",
      //       "Agnes",
      //       "Albert",
      //       "Alexander"
      //     ],
      //     B: ["Bailey", "Barclay", "Bartolo", "Bellamy", "Belle", "Benjamin"],
      //     C: [
      //       "Caiden",
      //       "Calvin",
      //       "Candy",
      //       "Carl",
      //       "Cherilyn",
      //       "Chester",
      //       "Chloe"
      //     ],
      //     V: ["Vladimir", "Hehe"]
      //   }
      contacts: [],
      linkContactsClass: "link-contacts-highlight",
      linkLocationClass: "link-location",
      navbarTitle: '通讯录'
    };
  },
  methods: {
    fetchContacts() {
      var that = this;
      var url = context.urls.getUsersByProjIDUrl.format(
        context.currentProject._id
      );
      console.log("request: " + url);
      var preloader = that.$f7.dialog.preloader("请稍后。。。");
      that.$f7.request.get(
        url,
        function(data) {
          console.log("response: " + JSON.stringify(data));
          preloader.close(true);
          if (data.success) {
            that.contacts = data.data;
          } else {
            that.$f7.dialog.alert(data.msg);
          }
        },
        function(xhr, status) {
          preloader.close(true);
          console.error("error " + status);
          that.$f7.dialog.alert("网络忙，请稍后重试！");
        },
        "json"
      );
    },
    showDetail(contact, event) {
      console.log(contact);
      context.currentContact = contact;
      this.$f7.views.main.router.navigate("/workers/detail/");
    },
    onTabContactsShow() {
      this.linkContactsClass = "link-contacts-highlight";
      this.navbarTitle = '通讯录';
    },
    onTabContactsHide() {
      this.linkContactsClass = "link-contacts";
    },
    onTabLocationShow() {
      this.linkLocationClass = "link-location-highlight";
      this.navbarTitle = "人员定位";
    },
    onTabLocationHide() {
      this.linkLocationClass = "link-location";
    }
  }
};
</script>
<style scoped>
select {
  margin: 0 16px;
}
.md .toolbar:after {
  height: 1px;
  background-color: #ccc;
}

.md .toolbar {
  background-color: #fff;
  color: rgba(0, 0, 0);
}

.md .tabbar a.tab-link.tab-link-active {
  color: black;
}

.md .toolbar a {
  color: rgba(0, 0, 0, 0.5);
}
#allmap {
  position: absolute;
  top: 56px;
  left: 0;
  right: 0;
  bottom: 56px;
}
</style>
