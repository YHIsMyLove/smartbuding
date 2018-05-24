<template>
  <f7-page>
    <div slot="fixed">
        <f7-row>
          <f7-col>
            <img src="static/imgs/logo.png">
          </f7-col>
        </f7-row>
        <f7-row>
          <f7-col>
            张三
          </f7-col>
        </f7-row>
    </div>
    <f7-list>
      <f7-list-item link="/account/folder/" title="文件夹" view="#view-project-main" panel-close>
        <f7-icon if-ios="f7:home" if-md="material:home" slot="media"></f7-icon>
      </f7-list-item>
      <f7-list-item link="/account/favourites/" title="收藏夹" view="#view-project-main" panel-close>
        <f7-icon if-ios="f7:home" if-md="material:home" slot="media"></f7-icon>
      </f7-list-item>
      <f7-list-item link="/account/settings/" title="设置" view="#view-project-main" panel-close>
        <f7-icon if-ios="f7:home" if-md="material:home" slot="media"></f7-icon>
      </f7-list-item>
      <f7-list-item link="/account/help/" title="帮助" view="#view-project-main" panel-close>
        <f7-icon if-ios="f7:home" if-md="material:home" slot="media"></f7-icon>
      </f7-list-item>
    </f7-list>
    <f7-list media-list>
</f7-list>
  </f7-page>
</template>
<script>
export default {}
</script>
