// pages/select-house-image/select-house-image.js
var app = getApp()
Page({

  /**
   * 页面的初始数据
   */
  data: {
    houses: ['月亮湾', '天商城'],
    houseIndex: 0,
    region: ["湖北省", "黄石市", "黄石港区"],
    houseImages: ["../../images/test_resources/timg.jpeg", "../../images/test_resources/timg-2.jpeg", "../../images/test_resources/timg.jpeg"],
    selectedIndex: -1
  },

  houseChanged: function (e) {
    this.setData({
      houseIndex: e.detail.value
    })
  },

  regionChange: function (e) {
    console.log('picker发送选择改变，携带值为', e.detail.value)
    this.setData({
      region: e.detail.value
    })
  },

  doSelect: function (e) {
    // console.log(e.detail.value)
    var index = e.currentTarget.dataset.index
    if (index == this.data.selectedIndex)
      index = -1
    this.setData({
      selectedIndex: index
    })
    if (index != -1) {
      app.globalData.selectedHouseImage = this.data.houseImages[this.data.selectedIndex]
    }
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
  
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