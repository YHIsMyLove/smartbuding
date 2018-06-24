const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const QualityAcceptanceSchema = new Schema({
    	QAImg:String,
	QARecipient:String,
	QATitle:String,
	QAContent:String,
	QATime:{ type: Date , default: Date.now },
	QAState:{ type: Number , default: 0 },

})

QualityAcceptanceSchema.pre('save', function (next) {
    next()
})
QualityAcceptanceSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
QualityAcceptanceSchema.statics = {
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
mongoose.model('QualityAcceptanceModel', QualityAcceptanceSchema);
