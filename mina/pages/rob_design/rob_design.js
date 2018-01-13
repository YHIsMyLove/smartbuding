// pages/rob_design/rob_design.js
var app = getApp()
Page({

  /**
   * 页面的初始数据
   */
  data: {
    showTopTips: false,
    timeCellWidth: "33.33333%",
    times: [
      { desc: "12:00", state: -1 },
      { desc: "14:00", state: 0 },
      { desc: "16:00", state: 1 },
      { desc: "18:00", state: 1 },
      { desc: "20:00", state: 1 }
    ],
    houseStyles: ['北欧风格', '地中海'],
    houseStyleIndex: 0,
    houses: ['月亮湾', '天商城'],
    houseIndex: 0,
    houseImagePath: "../../images/avatarIcon.svg",
    region: ["湖北省", "黄石市", "黄石港区"],
    isOnsiteService: false,
    canGetVCode: true,
    canGetVCodeRemainTime: 30,
    validation: {
      name: { valid: true },
      mobile: { valid: true },
      house: { valid: true },
      houseStyle: { valid: true },
      houseArea: { valid: true },
      region: { valid: true },
      vcode: {valid: true}
    }
  },

  rob: function (e) {
    console.log("用户提交了，参数为：", e.detail.value);
    var that = this
    if (!this.WxValidate.checkForm(e)) {
      var validation = {
        name: { valid: true },
        mobile: { valid: true },
        house: { valid: true },
        houseStyle: { valid: true },
        houseArea: { valid: true },
        region: { valid: true },
        vcode: { valid: true }
      }
      this.WxValidate.errorList.map(function (item) {
        validation[item.param].valid = false
      })
      this.setData({
        validation: validation,
        showTopTips: true
      })
      setTimeout(function () {
        that.setData({
          showTopTips: false
        });
      }, 3000);
      return false
    }
    this.setData({ submitHidden: false })
  },

  chooseImage: function (e) {
    var that = this;
    wx.chooseImage({
      count: 1,
      sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
      sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
      success: function (res) {
        // 返回选定照片的本地文件路径列表，tempFilePath可以作为img标签的src属性显示图片
        that.setData({
          houseImagePath: res.tempFilePaths[0]
        });
        app.globalData.selectedHouseImage = ""
      }
    })
  },

  previewImage: function (e) {
    if (this.data.isOnsiteService) return
    wx.previewImage({
      current: e.currentTarget.id, // 当前显示图片的http链接
      urls: [this.data.houseImagePath] // 需要预览的图片http链接列表
    })
  },

  styleChanged: function (e) {
    this.setData({
      styleIndex: e.detail.value
    })
  },

  houseChanged: function (e) {
    this.setData({
      houseIndex: e.detail.value
    })
  },

  changeOnsiteService: function (e) {
    this.setData({
      isOnsiteService: !this.data.isOnsiteService
    })
  },

  regionChange: function (e) {
    console.log('picker发送选择改变，携带值为', e.detail.value)
    this.setData({
      region: e.detail.value
    })
  },

  getVCode: function (e) {
    var that = this
    that.setData({
      canGetVCode: false
    })
    that.data.canGetVCodeRemainTime = 30
    var handler = setInterval(function(){
      var time = that.data.canGetVCodeRemainTime - 1
      that.setData({
        canGetVCodeRemainTime: time
      })
      if (time <= 0){
        clearInterval(handler)
        that.setData({
          canGetVCode: true
        })
      }
    }, 1000)
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.WxValidate = app.wxValidate(
      {
        name: {
          required: true,
          minlength: 2,
          maxlength: 10,
        },
        mobile: {
          required: true,
          tel: true,
        },
        house: {
          required: true,
          minlength: 2,
          maxlength: 100,
        },
        region: {
          required: true,
          minlength: 2,
          maxlength: 100,
        },
        houseStyle: {
          required: true,
          minlength: 2,
          maxlength: 100,
        },
        houseArea: {
          required: true
        },
        vcode: {
          required: true
        }
      }
      , {
        name: {
          required: '请填写您的姓名',
        },
        mobile: {
          required: '请填写您的手机号',
        },
        house: {
          required: '请选择小区',
        },
        region: {
          required: '请选择地区',
        },
        houseStyle: {
          required: '请选择装修风格'
        },
        houseArea: {
          required: '请填写房屋面积'
        },
        vcode: {
          required: '请填写短信验证码'
        }
      }
    )

    var width = 100.0 / this.data.times.length;
    console.debug("time cell width is ", width);
    this.setData({ timeCellWidth: width + "%" });
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    if (app.globalData.selectedHouseImage != "") {
      this.setData({
        houseImagePath: app.globalData.selectedHouseImage
      })
    }
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})