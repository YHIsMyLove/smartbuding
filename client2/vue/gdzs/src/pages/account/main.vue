<template>
  <f7-page>
    <div class="display-flex align-items-center" style="padding: 56px 16px 16px 16px" slot="fixed">
      <img src="static/imgs/个人信息80x80.png">
      <div class="display-flex justify-content-center" style="flex-direction:column;padding-left:8px">
        <div class="display-flex align-items-center">
          <span class="font-big">{{user.UserName}}</span>
          <span class="my-tag">工程部</span>
        </div>
        <div class="display-flex align-items-center">
          <span class="font-middle">经理</span>
        </div>
      </div>
    </div>
    <f7-list>
      <f7-list-item link="/account/favourites/" title="收藏夹" view="#main-view" panel-close>
        <f7-icon f7="folder_fill" slot="media"></f7-icon>
      </f7-list-item>
      <f7-list-item link="/account/settings/" title="设置" view="#main-view" panel-close>
        <f7-icon f7="gear_fill" slot="media"></f7-icon>
      </f7-list-item>
      <f7-list-item link="/account/settings/" title="系统更新" view="#main-view" panel-close>
        <f7-icon f7="reload_round_fill" slot="media"></f7-icon>
      </f7-list-item>
      <f7-list-item link="/account/help/" title="帮助" view="#main-view" panel-close>
        <f7-icon f7="help_fill" slot="media"></f7-icon>
      </f7-list-item>
    </f7-list>
    <f7-list media-list>
</f7-list>
  </f7-page>
</template>
<script>
import context from '../../service-context.js'
export default {
  on: {
    pageInit(event, pageData) {
      //alert("page init");
    },
    pageAfterIn(event, pageData) {
      console.log('user main page after in');
      if (!context.currentUser) return;
      this.user = context.currentUser;
    },
    pageAfterOut(event, pageData) {
      //alert("page after out");
    }
  },
  data(){
    return {
      user: {}
    }
  }
}
</script>
