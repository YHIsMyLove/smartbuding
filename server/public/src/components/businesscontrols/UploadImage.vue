<template>
    <div class="main" :style="circle ? circleStyle : baseStyle">
        <el-upload
            action="/api/UploadFile"
            :show-file-list="false"
            :on-success="handleAvatarSuccess"
            :before-upload="beforeAvatarUpload">
            <img v-if="imageUrl" :src="imageUrl" class="avatar" :style="circle ? circleStyle : baseStyle">
            <i v-else class="el-icon-plus avatar-uploader-icon" :style="circle ? circleStyle : baseStyle"></i>
        </el-upload>
    </div>    
</template>

<script>
export default {
  props: ["width", "circle", "value"],
  computed: {
    baseStyle() {
      return {
        width: `${this.width}px`,
        height: `${this.width}px`,
        "line-height": `${this.width}px`
      };
    },
    circleStyle() {
      return {
        width: `${this.width}px`,
        height: `${this.width}px`,
        "line-height": `${this.width}px`,
        "border-radius": "50%"
      };
    }
  },
  data() {
    return {
      imageUrl: ""
    };
  },
  created() {
    this.imageUrl = this.value;
  },
  methods: {
    handleAvatarSuccess(res, file) {
      this.imageUrl = URL.createObjectURL(file.raw);
      this.$emit("UpLoadOver", res.data);
      this.$emit("input", res.data);
    },
    beforeAvatarUpload(file) {
      const isJPG = file.type === "image/jpeg" || file.type === "image/png";
      const isLt2M = file.size / 1024 / 1024 < 2;
      console.log(file.type)
      if (!isJPG) {
        this.$message.error("上传头像图片只能是 JPG或者png 格式!");
      }
      if (!isLt2M) {
        this.$message.error("上传头像图片大小不能超过 2MB!");
      }
      return isJPG && isLt2M;
    }
  }
};
</script>

<style scoped>
.main {
  border: 2px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  width: 64px;
  height: 64px;
}
.main:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 64px;
  height: 64px;
  line-height: 64px;
  text-align: center;
}
.avatar {
  width: 64px;
  height: 64px;
  display: block;
}
</style>


