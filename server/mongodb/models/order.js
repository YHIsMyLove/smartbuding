const mongoose = require('mongoose')
const Schema = mongoose.Schema;

const Order = new Schema({
    OrderID: String,
    UserID: String,
    OrderNum: String,
    GoodsID: String,
    OrderPrice: String
})

Order.pre('save', function (next) {
    next()
})

Order.methods = {
    updateAndSave: function () {
        return this.save();
    }
}

Order.statics = {
    fetch: function () {

    },
    findById: function () {

    },
    load: function (id) {
        return this.findOne({ _id: id })
            .exec();
    },
    list: function (options) {
        const criteria = options.criteria || {};
        const page = options.page || 0;
        const limit = options.limit || 30;
        return this.find({})//criteria
            .sort({ createdAt: -1 })
            .limit(limit)
            .skip(limit * page)
            .exec();
    }
}

mongoose.model('Order', Order);