const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const MechanicalSchema = new Schema({
    	mechanicalImg:String,
	mechanicalName:String,
	mechanicalState:{ type: String , default: '正常' },
	mechanicalUser:String,
	mechanicalUserPhone:String,

})

MechanicalSchema.pre('save', function (next) {
    next()
})
MechanicalSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
MechanicalSchema.statics = {
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
mongoose.model('MechanicalModel', MechanicalSchema);
