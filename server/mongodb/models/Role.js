const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//角色表
const roleSchema = new Schema({
    roleid:String,
    rolename:String
})

roleSchema.pre('save', function (next) {
    next()
})
roleSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
roleSchema.statics = {
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
mongoose.model('Role', roleSchema);
