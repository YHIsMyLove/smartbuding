<template>
  <f7-page tabs :page-content="false">
    <f7-page-content tab tab-active id="page-project-main">
      <project-main></project-main>
    </f7-page-content>
    <f7-page-content tab id="page-doors-main">
      <doors-main></doors-main>
    </f7-page-content>
    <f7-toolbar tabbar labels bottom-md>
      <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" text="项目管理" tab-link="#page-project-main"></f7-link>
      <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" text="门禁记录" tab-link="#page-doors-main"></f7-link>
    </f7-toolbar>
  </f7-page>
</template>
<script>
export default {};
</script>
<style scoped>
</style>
