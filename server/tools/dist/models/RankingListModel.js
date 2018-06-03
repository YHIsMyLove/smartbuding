const mongoose = require('mongoose')
const Schema = mongoose.Schema;

//用户表
const RankingListSchema = new Schema({
    	UserID:String,
	RankingListTitle:String,
	RankingListContent:String,
	RankingListScore:Number,
	ProjID:String,
	RankingListTime:{ type: Date , default: Date.now }
})

RankingListSchema.pre('save', function (next) {
    next()
})
RankingListSchema.methods = {
    updateAndSave: function () {
        return this.save();
    }
}
RankingListSchema.statics = {
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
mongoose.model('RankingListModel', RankingListSchema);
