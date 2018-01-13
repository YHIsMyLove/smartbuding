// pages/rob_design/rob_design.js
var qcloud = require('../../vendor/wafer2-client-sdk/index')
var config = require('../../config/config')
var util = require('../../utils/util.js')
var app = getApp()
Page({

  /**
   * 页面的初始数据
   */
  data: {
    tunnelStatus: false,
    showTopTips: false,
    timeCellWidth: "33.33333%",
    times: [
      { desc: "9:00", state: 0 },
      { desc: "14:00", state: 1 },
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
      vcode: { valid: true }
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
    var handler = setInterval(function () {
      var time = that.data.canGetVCodeRemainTime - 1
      that.setData({
        canGetVCodeRemainTime: time
      })
      if (time <= 0) {
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

  // 切换信道的按钮
  switchChange: function (e) {
    var checked = e.detail.value

    if (checked) {
      this.openTunnel()
    } else {
      this.closeTunnel()
    }
  },

  openTunnel: function () {
    util.showBusy('信道连接中...')
    // 创建信道，需要给定后台服务地址
    var tunnel = this.tunnel = new qcloud.Tunnel(config.service.tunnelUrl)

    // 监听信道内置消息，包括 connect/close/reconnecting/reconnect/error
    tunnel.on('connect', () => {
      util.showSuccess('信道已连接')
      console.log('WebSocket 信道已连接')
      this.setData({ tunnelStatus: 'connected' })
    })

    tunnel.on('close', () => {
      util.showSuccess('信道已断开')
      console.log('WebSocket 信道已断开')
      this.setData({ tunnelStatus: 'closed' })
    })

    tunnel.on('reconnecting', () => {
      console.log('WebSocket 信道正在重连...')
      util.showBusy('正在重连')
    })

    tunnel.on('reconnect', () => {
      console.log('WebSocket 信道重连成功')
      util.showSuccess('重连成功')
    })

    tunnel.on('error', error => {
      util.showModel('信道发生错误', error)
      console.error('信道发生错误：', error)
    })

    // 监听自定义消息（服务器进行推送）
    tunnel.on('speak', speak => {
      util.showModel('信道消息', speak)
      console.log('收到说话消息：', speak)
    })

    // 打开信道
    tunnel.open()

    this.setData({ tunnelStatus: 'connecting' })
  },

  /**
   * 点击「发送消息」按钮，测试使用信道发送消息
   */
  sendMessage() {
    if (!this.data.tunnelStatus || !this.data.tunnelStatus === 'connected') return
    // 使用 tunnel.isActive() 来检测当前信道是否处于可用状态
    if (this.tunnel && this.tunnel.isActive()) {
      // 使用信道给服务器推送「speak」消息
      this.tunnel.emit('speak', {
        'word': 'I say something at ' + new Date(),
      });
    }
  },

  /**
   * 点击「关闭信道」按钮，关闭已经打开的信道
   */
  closeTunnel() {
    if (this.tunnel) {
      this.tunnel.close();
    }
    util.showBusy('信道连接中...')
    this.setData({ tunnelStatus: 'closed' })
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
    this.openTunnel();
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {
    this.closeTunnel();
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
    console.log("onPullDownRefresh")
  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
    console.log("onReachBottom")
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})