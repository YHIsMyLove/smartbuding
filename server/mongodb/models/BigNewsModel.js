const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const BigNewsSchema = new Schema({
    	BgiNewsImg:String,
	BgiNewsTitle:String,
	BgiNewsContent:String,
	ProjID:String,

})

BigNewsSchema.pre('save', function (next) {
    next()
})
BigNewsSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
BigNewsSchema.statics = {
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
mongoose.model('BigNewsModel', BigNewsSchema);
