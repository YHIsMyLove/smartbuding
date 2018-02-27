const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const userSchema = new Schema({
    UserID: String,
    UserName: String,
    UserSex: Number,
    UserAge: Number,
    UserBirth: Date,
    UserAddr: String,
    UserHeadImg: String,
    UserPhoneNum: Number,
    UserCreatedAt: { type: Date, default: Date.now },
    UserCardID: String,
    UserAreaid: String,//json数组   
    UserRoles: String,//json数组
})

userSchema.pre('save', function (next) {
    next()
})
userSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
userSchema.statics = {
    fetch: function () { },
    findById: function () { },
    load: function (id) {
        return this.findOne({ _id: id }).exec();
    },
    list: function (options) {
        const criteria = options.criteria || {};
        const page = options.page || 0;
        const limit = options.limit || 30;
        return this.find({})
            .sort({ createdAt: -1 })
            .limit(limit)
            .skip(limit * page)
            .exec();
    }
}
mongoose.model('User', userSchema);
