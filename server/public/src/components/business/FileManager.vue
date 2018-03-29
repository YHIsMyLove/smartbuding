<template>
 <el-tabs style="width:100%;">
    <el-tab-pane label="文件管理">
         <section>
            <el-col :span="24" class="toolbar">
                <el-form :inline="true"  class="demo-form-inline">
                    <el-form-item>
                        <el-input  placeholder="图片名称"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button @click="refImgs">查询</el-button>
                        <el-button @click="upload" type="primary">上传</el-button>
                    </el-form-item>
                </el-form>
            </el-col>
            <el-col :span="4" v-for="i,index in tableData" :key="index">
                <el-card class="img-card" :body-style="{ padding: '0px' }">
                    <img style="width:100%,height:100%" :src="i.url" class="image">
                    <div style="padding: 14px;">
                        <span>{{i.key}}</span>
                        <div class="bottom clearfix">
                            <time class="time">{{ i.ctime }}</time>
                            <el-button type="text" class="button">删除</el-button>
                        </div>
                    </div>
                </el-card>
            </el-col>
            <el-col :span="24" class="toolbar" style="padding-bottom:10px;">
                <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[12, 24, 36, 48]"
                    :page-size="currentPageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableDataLength"
                    style="float:right">
                </el-pagination>
            </el-col>
            <el-dialog title="图片上传" v-model="editFormVisible" :close-on-click-modal="true">
                    <el-form :model="editForm" label-width="100px"  ref="editForm">
                        <!-- <el-form-item  label="图片名称" prop="DevID">
                            <el-input :disabled="true" v-model="editForm.ImgName" auto-complete="off"></el-input>
                        </el-form-item> -->
                        <el-form-item label="图片名称">
                            <el-upload
                                class="upload-demo"
                                action="/api/UploadFile"
                                :on-preview="handlePreview"
                                :on-remove="handleRemove"
                                :file-list="fileList2"
                                list-type="picture"
                                accept="image/*"
                                :on-change="handleChange"
                                :on-success="handlesuccess"
                                :on-progress="handleProgress"
                                :on-error="handleerror"
                                :before-upload="beforeAvatarUpload">
                                <el-button size="small" type="primary">点击上传</el-button>
                                <div slot="tip" class="el-upload__tip">只能上传jpg/png文件</div>
                            </el-upload>
                        </el-form-item>
                    </el-form>
                    <div slot="footer" class="dialog-footer">
                        <el-button @click="editFormVisible = false">取 消</el-button>
                        <el-button type="primary" :loading="uploding" @click="editSubmit" >提交</el-button>
                    </div>
                </el-dialog>
         </section>
    </el-tab-pane>
 </el-tabs>
</template>
<script>
import util from "../../common/util";
import NProgress from "nprogress";
import axios from "axios";
import moment from "moment";
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters(["getProj"])
  },
  data() {
    return {
      currentDate: moment().format("YYYY-MM-DD hh:mm:ss"),
      editFormVisible: false,
      editForm: {
        ImgName: ""
      },
      fileList2: [],
      uploding: false,

      currentPage: 0,
      tableData: [],
      tableDataLength: 0,
      currentPageSize: 12,
      startKey: "" //分页需要记下最后一个图片的Key
    };
  },
  created() {
    this.GetImages();
  },
  methods: {
    refImgs() {
      this.GetImages();
    },
    handleCurrentChange() {},
    handleSizeChange() {},
    //上传图片
    upload() {
      this.editFormVisible = true;
    },
    editSubmit() {
      this.editFormVisible = false;
      this.fileList2 = [];
    },
    handleRemove(file, fileList) {
      console.log(file, fileList);
    },
    handlePreview(file) {
      console.log(file);
    },
    handleerror(err) {
      console.log(err);
      this.uploding = false;
      this.$message({
        message: err,
        type: "error"
      });
    },
    handleProgress() {
      this.uploding = true;
    },
    handlesuccess() {
      this.uploding = false;
    },
    handleChange(file, fileList) {
      console.log(fileList);
    },
    beforeAvatarUpload(file) {
      console.log(file.type);
      //const isJPG = file.type === "image/jpeg";
      const isLt2M = file.size / 1024 / 1024 < 50;
      // if (!isJPG) {
      //   this.$message.error("仅允许上传jpeg图片");
      // }
      if (!isLt2M) {
        this.$message.error("上传图片大小不能超过 50MB!");
      }
      //isJPG &&
      return isLt2M;
    },
    //分页获取图片
    GetImages() {
      let that = this;
      let query = {
        startKey: that.startKey,
        pageCount: that.$data.currentPageSize
        // limit: vm.$data.currentPageSize,
        // page: vm.$data.currentPage
      };
      axios
        .get("/api/GetFiles", { params: query })
        .then(res => {
          console.log(res);
          if (res.data.success) {
            that.$data.tableData = res.data.data;
            that.$data.tableDataLength = res.data.meta.count;
            console.log(res.data.data);
            that.startKey =
              that.$data.tableData[that.$data.currentPageSize].key;
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    handleSizeChange(val) {
      this.$data.currentPageSize = val;
      this.GetImages();
    },
    handleCurrentChange(val) {
      that.startKey = that.$data.tableData[that.$data.currentPageSize].key;
      this.$data.currentPage = val; //计算startKey
      this.GetImages();
    }
  }
};
</script>

<style scoped>
.el-carousel__item h3 {
  color: #475669;
  font-size: 14px;
  opacity: 0.75;
  line-height: 200px;
  margin: 0;
}

.el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
  background-color: #d3dce6;
}
.img-card {
  margin: 0 auto;
  width: 85%;
  height: 85%;
  margin-top: 10px;
}
.time {
  font-size: 13px;
  color: #999;
}
.image {
  width: 100%;
  display: block;
}
.button {
  padding: 0;
  float: right;
}
</style>
