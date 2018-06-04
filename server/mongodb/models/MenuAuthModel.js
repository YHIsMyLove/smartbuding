const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const MenuAuthSchema = new Schema({
    	MenuID:String,
	RoleID:String,
	ProjID:String,

})

MenuAuthSchema.pre('save', function (next) {
    next()
})
MenuAuthSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
MenuAuthSchema.statics = {
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
mongoose.model('MenuAuthModel', MenuAuthSchema);
