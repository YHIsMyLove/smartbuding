const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const DoorIOSchema = new Schema({
    	IOTime:{ type: Date , default: Date.now },
	IOType:{ type: String , default: '进' },
	UserID:String,
	ProjID:String,

})

DoorIOSchema.pre('save', function (next) {
    next()
})
DoorIOSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
DoorIOSchema.statics = {
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
mongoose.model('DoorIOModel', DoorIOSchema);
