<template>
  <f7-page>
    <f7-navbar>
        <f7-nav-left>
          <f7-link icon-if-ios="f7:menu" icon-if-md="material:menu" panel-open="left"></f7-link>
        </f7-nav-left>
        <f7-nav-right>
          <f7-link icon-if-ios="f7:menu" icon-if-md="material:crop_free"></f7-link>
        </f7-nav-right>
        <f7-subnavbar>
          <p>xxxxxxxxxxxxxxxxxxxxxxx</p>
        </f7-subnavbar>
    </f7-navbar>
  </f7-page>
</template>
<script>
export default {}
</script>
<style scoped>
  .md .subnavbar
  {
    height: 200px;
  }
  .subnavbar-inner
  {
    width:100%;
    height: 100%;
  }
</style>
