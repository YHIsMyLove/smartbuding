const mongoose = require('mongoose')
const Schema = mongoose.Schema

// postid|postname|areaid
// --|--|--
// 01|部门经理|01
// 02|造价师|02
//zhiwei
const postSchema = new Schema({
    postid:String,
    postname:String,
    areaid:String
})

postSchema.pre('save', function (next) {
    next()
})
postSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
postSchema.statics = {
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
mongoose.model('Post', postSchema);
