// pages/rob_design/rob_design.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    timeCellWidth: "33.33333%",
    times: [
      { desc: "12:00", state: -1 },
      { desc: "14:00", state: 0 },
      { desc: "16:00", state: 1 },
      { desc: "18:00", state: 1 },
      { desc: "20:00", state: 1 }
    ],
    styles: ['北欧风格', '地中海'],
    styleIndex: 0,
    houseImagePath: "../../images/avatarIcon.svg",
  },

  rob: function (e) {
    console.log("用户提交了，参数为：", e.detail.value);
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
      }
    })
  },

  styleChanged: function (e) {
    this.setData({
      styleIndex: e.detail.value
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
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