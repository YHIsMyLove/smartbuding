<template>
    <f7-page tabs :page-content="false">
        <f7-navbar title="通讯录" back-link="Back"></f7-navbar>
        <f7-page-content id="page-contacts" tab tab-active>
            <!-- <workers-contacts></workers-contacts> -->
            <!-- <f7-list contacts-list>
                <f7-list-group :key="key1" v-for="(group, key1) in contacts">
                    <f7-list-item :title="key1" group-title></f7-list-item>
                    <f7-list-item :key="key2" v-for="(name, key2) in group" :title="name" link="/workers/detail/"></f7-list-item>
                </f7-list-group>
            </f7-list> -->
            <f7-list>
                <f7-list-item v-for="(contact, key) in contacts" :key="key" :title="contact.UserName" link @click="showDetail(contact, $event)"></f7-list-item>
            </f7-list>
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
<script>
import context from "../../service-context.js";
export default {
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
      contacts: []
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
        'json'
      );
    },
    showDetail(contact, event)
    {
        console.log(contact);
        context.currentContact = contact;
        this.$f7.views.main.router.navigate('/workers/detail/');
    }
  }
};
</script>
