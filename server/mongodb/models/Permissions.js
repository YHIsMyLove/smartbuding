const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//权限表
const permissionsSchema = new Schema({
    RoleID: String,
    DevID: String,
})

permissionsSchema.pre('save', function (next) {
    next()
})
permissionsSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
permissionsSchema.statics = {
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
mongoose.model('Permissions', permissionsSchema);
