const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const DevicesSchema = new Schema({
    	DevID:String,
	DevName:String,
	DevDesc:String,
	DevIP:{ type: String , default: '192.168.1.1' },
	DevPort:{ type: Number , default: 8888 },
	DevState$:{ type: String , default: '在线' },
	DevClass:String,
	ProjID:String,

})

DevicesSchema.pre('save', function (next) {
    next()
})
DevicesSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
DevicesSchema.statics = {
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
mongoose.model('DevicesModel', DevicesSchema);
