<template>
    <div class="main">
        <el-dialog :title="dialogInfo.title" 
                   v-model="dialogInfo.visible" 
                   :close-on-click-modal="true">
            <el-form >

            </el-form>
        </el-dialog>
    </div>    
</template>

<script>
export default {
  props: [Title],
  data() {
    return {
      dialogInfo: {
        title: "测试弹框",
        visible: false
      }
    };
  }
};
</script>
<style scoped>
.main {
}
</style>



