<template>
  <f7-page tabs :page-content="false">
    <f7-navbar title="My App"></f7-navbar>
    <f7-page-content tab tab-active id="view-project-main">Tab 1 Content ...</f7-page-content>
    <f7-page-content tab id="view-doors-main">Tab 2 Content ...</f7-page-content>
    <f7-toolbar tabbar labels bottom-md>
      <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" text="项目管理" tab-link="#view-project-main"></f7-link>
      <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" text="门禁记录" tab-link="#view-doors-main"></f7-link>
    </f7-toolbar>
  </f7-page>
</template>
<script>
export default {};
</script>
<style scoped>
</style>
