const mongoose = require("mongoose")
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const RankingList = mongoose.model('RankingListModel')
const UserModel = mongoose.model('User')
const moment = require('moment')


var getRankingOrder = async (req, res) => {
    let query = {
        startDate: new Date(req.query.startDate),
        endDate: new Date(req.query.endDate),
        ProjID: req.query.ProjID,
        limit: req.query.limit || 10
    }
    try {
        let result = await RankingList.aggregate([
            { $match: { RankingListTime: { $gte: query.startDate, $lte: query.endDate } } },
            { $match: { ProjID: query.ProjID } },
            { $group: { _id: '$UserID', total: { $sum: '$RankingListScore' } } },
            { $sort: { total: -1 } },
            {
                $lookup: {
                    from: 'users',
                    localField: '_id',
                    foreignField: 'UserID',
                    as: 'UserDoc'
                }
            }
        ]).limit(query.limit)
        result = result.map(item => {
            return {
                UserID: item.UserDoc[0].UserID,
                UserName: item.UserDoc[0].UserName,
                _id: item.UserDoc[0]._id,
                Score: item.total
            }
        })
        //console.log(result)
        res.send(msg.genSuccessMsg('查询成功', result))
    } catch (error) {
        res.send(msg.genFailedMsg('查询失败->' + error))
    }
}

var getRankingList = async (req, res) => {
    try {
        let query = {
            redorblack: req.query.redorblack,
            ProjID: req.query.ProjID
        }
        let match = query.redorblack
            ? { $match: { RankingListScore: { $lte: 0 } } }
            : { $match: { RankingListScore: { $gte: 0 } } }
        let result = await RankingList.aggregate([
            // { $match: { ProjID: query.ProjID } },
            match,
            {
                $lookup: {
                    from: 'users',
                    localField: 'UserID',
                    foreignField: 'UserID',
                    as: 'UserDoc'
                }
            }
        ])

        console.log(result)

        result = result.map(item => {
            return {
                UserID: item.UserDoc[0].UserID,
                UserName: item.UserDoc[0].UserName,
                _id: item.UserDoc[0]._id,
                Score: item.RankingListScore,
                Title: item.RankingListTitle,
                Content: item.RankingListContent,
                Time: item.RankingListTime
            }
        })
        res.send(msg.genSuccessMsg("获取成功", result))
    } catch (error) {
        res.send(msg.genFailedMsg("获取失败->" + error))
    }


}

exports.GetRankingOrder = getRankingOrder
exports.GetRankingList = getRankingList