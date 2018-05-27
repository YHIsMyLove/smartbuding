<template>
    <f7-page tabs :page-content="false">
        <f7-navbar title="日历事件" back-link="Back"></f7-navbar>
        <f7-page-content id="records" tab tab-active>
            <div class="display-flex justify-content-space-between padding">
                <div>会议主持：张三</div>
                <div>2018/05/26</div>
            </div>
            <f7-list>
                <f7-list-item :key="index" v-for="(record, index) in records">
                    <f7-list-item-cell>
                        <f7-list-item-row>
                            <f7-list-item-cell>{{record.content}}</f7-list-item-cell>
                        </f7-list-item-row>
                        <f7-list-item-row>
                            <f7-list-item-cell>
                                <f7-link @click="changeFavourite(record, $event)">
                                    <img :src="record.isFavourite ? 'static/imgs/收藏30x30.png' : 'static/imgs/收藏2_30x30.png'" alt="">
                                </f7-link>
                            </f7-list-item-cell>
                            <f7-list-item-cell id="cell-depart">执行部门：{{record.execteDepart}}</f7-list-item-cell>
                        </f7-list-item-row>
                    </f7-list-item-cell>
                </f7-list-item>
            </f7-list>
        </f7-page-content>
        <f7-page-content id="photos" tab></f7-page-content>
        <f7-toolbar tabbar>
            <f7-link text="会议既要" tab-link="#records"></f7-link>
            <f7-link text="会议照片" tab-link="#photos"></f7-link>
        </f7-toolbar>
    </f7-page>
</template>
<script>
export default {
  data() {
    return {
      records: [
        {
          content:
            "不知不觉，一学期又要结束了。这一学期，作为小学二年级的班主任来说，更多的是欢乐。对于一个二年级班主任来说，教育侧重点应该是对学生的道德教育。我更侧重于为孩子营造一个可以自由思考的空间，一个快乐的学习氛围。下面就我的工作做如下汇报：",
          execteDepart: "经营部",
          isFavourite: true
        },
        {
          content:
            "不知不觉，一学期又要结束了。这一学期，作为小学二年级的班主任来说，更多的是欢乐。对于一个二年级班主任来说，教育侧重点应该是对学生的道德教育。我更侧重于为孩子营造一个可以自由思考的空间，一个快乐的学习氛围。下面就我的工作做如下汇报：",
          execteDepart: "经营部 工程部",
          isFavourite: false
        },
        {
          content:
            "不知不觉，一学期又要结束了。这一学期，作为小学二年级的班主任来说，更多的是欢乐。对于一个二年级班主任来说，教育侧重点应该是对学生的道德教育。我更侧重于为孩子营造一个可以自由思考的空间，一个快乐的学习氛围。下面就我的工作做如下汇报：",
          execteDepart: "经营部 工程部",
          isFavourite: true
        },
        {
          content:
            "不知不觉，一学期又要结束了。这一学期，作为小学二年级的班主任来说，更多的是欢乐。对于一个二年级班主任来说，教育侧重点应该是对学生的道德教育。我更侧重于为孩子营造一个可以自由思考的空间，一个快乐的学习氛围。下面就我的工作做如下汇报：",
          execteDepart: "经营部 工程部",
          isFavourite: false
        }
      ]
    };
  },
  methods: {
      changeFavourite(record, event)
      {
        //   console.log(record.isFavourite);
        record.isFavourite = !record.isFavourite;
      }
  }
};
</script>
<style scoped>
#cell-depart {
  text-align: right;
}
</style>

