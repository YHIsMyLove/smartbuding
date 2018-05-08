
const mongoose = require("mongoose")

const Meeting = mongoose.model('Meeting')
const MeetingMinutes = mongoose.model('MeetingMinutes')
const msg = require("../../utils/message")
const SystemConfig = require('../../config/config')
const moment = require('moment')
const UserSession = mongoose.model('UserSession')
const SysTable = mongoose.model('SysTable')
const User = mongoose.model('User')
const UserDept = mongoose.model('UserDept')
const UserProj = mongoose.model('UserProj')


/**
 * GET 获取会议信息
 * @param {*} req {ProjID,[page,limit]} []分页数据选填
 * @param {*} res 
 */
exports.GetMeetings = async (req, res) => {
    let query = {
        ProjID: req.query.ProjID,
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    if (!query.ProjID) return res.send(msg.genFailedMsg('项目ID不允许为空'))
    try {
        let result = await Meeting.list(query)

        let data = result.map(i => {
            return {
                _id: i._id,
                MeetingName: i.MeetingName,
                MeetingCreatedAt: moment(i.MeetingCreatedAt).format('YYYY-MM-DD hh:mm:ss'),
                Compere: i.Compere
            }
        })
        let count = await Meeting.count()
        res.send(msg.genSuccessMsg('查询成功', data, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('查询失败->' + error))
    }
}

exports.DelMeetingByID = async (req, res) => {

}


/**
 * GET 获取会议内容
 * @param {*} req {MeetingID}
 * @param {*} res 
 */
exports.GetMeetingContentByMeetingID = async (req, res) => {
    let query = {
        MeetingID: req.query.MeetingID
    }
    try {
        let result = await MeetingMinutes.find(query)
        res.send(msg.genSuccessMsg('获取成功', result))
    } catch (error) {
        res.send(msg.genFailedMsg('获取失败' + error))
    }
}

/**
 * POST 插入或者修改会议信息
 * @param {*} req (Meeting:{} 带_id是更新,不带是新增)
 * @param {*} res 
 */
exports.InsertOrUpdateMeeting = async (req, res) => {
    try {
        if (req.body._id == undefined) {
            let meeting = new Meeting(req.body)
            await meeting.updateAndSave();
        } else {
            let meeting = req.meeting
            meeting = Object.assign(Meeting, req.body);
            await meeting.updateAndSave();
        }
        res.send(msg.genSuccessMsg('保存成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('保存失败->' + error))
    }
}

/**
 * POST 插入或者修改会议内容信息
 * @param {*} req (MeetingMinutes:{} _id==-1是更新,不带是新增)
 * @param {*} res 
 */
exports.InsertOrUpdateMeetingContent = async (req, res) => {
    try {
        if (req.body._id == '-1') {
            req.body._id = undefined
            let content = new MeetingMinutes(req.body)
            await content.updateAndSave();
        } else {
            console.log(req.body)
            await MeetingMinutes.updateOne({ _id: req.body._id }, req.body).exec()

        }
        res.send(msg.genSuccessMsg('保存成功'))
    } catch (error) {
        res.send(msg.genFailedMsg('保存失败->' + error))
    }
}


/**
 * GET 获取会议内容信息
 * @param {*} req {MeetingID}
 * @param {*} res 
 */
exports.GetMeetingContents_APP = async (req, res) => {
    let query = {
        MeetingID: req.query.MeetingID
    }
    try {
        let result = await MeetingMinutes.find(query)
        let deptids = await UserDept.find({ UserID: req.query.UserID }).select('DeptID')
        let depts = deptids.map(i => i.DeptID)
        console.log(depts)
        let data = result.map(i => {
            return {
                _id: i._id,
                MeetingID: i.MeetingID,
                MeetingTitle: i.MeetingTitle,
                Content: i.Content,
                Depts: i.Depts.map(dept => {
                    return {
                        _id: dept.DeptID,
                        Name: dept.DeptName
                    }
                }),
                IsRelational: i.Depts.filter(d => depts.indexOf(d.DeptID) >= 0).length > 0
            }
        })
        res.send(msg.genSuccessMsg('获取成功', data))
    } catch (error) {
        res.send(msg.genFailedMsg('获取失败' + error))
    }
}

/**
 * GET 获取会议信息
 * @param {*} req {ProjID,[page,limit]} []分页数据选填
 * @param {*} res 
 */
exports.GetMeetings_APP = async (req, res) => {
    let query = {
        ProjID: req.query.ProjID,
        page: parseInt(req.query.page) - 1,
        limit: parseInt(req.query.limit)
    }
    if (!query.ProjID) return res.send(msg.genFailedMsg('项目ID不允许为空'))
    try {
        let result = await Meeting.list(query)

        let deptids = await UserDept.find({ UserID: req.query.UserID }).select('DeptID')
        let depts = deptids.map(i => i.DeptID)
        console.log(depts)

        let meetingids = result.map(i => i._id)
        let result2 = await MeetingMinutes.find({ MeetingID: { $in: meetingids } }).select("Depts")

        let count2 = 0;
        result2.forEach(i => {
            if (i.Depts.filter(d => depts.indexOf(d.DeptID) >= 0).length > 0) {
                count2++
            }
        })

        let data = result.map(i => {
            return {
                _id: i._id,
                MeetingName: i.MeetingName,
                MeetingCreatedAt: moment(i.MeetingCreatedAt).format('YYYY-MM-DD hh:mm:ss'),
                Compere: i.Compere,
                RelationalCount: count2
            }
        })

        let count = await Meeting.count()
        res.send(msg.genSuccessMsg('查询成功', data, { count: count }))
    } catch (error) {
        res.send(msg.genFailedMsg('查询失败->' + error))
    }
}


getProvAndCity_byProj = async (ProjID) => {
    return new Promise(async (res, rej) => {
        let cityid = await SysTable.findOne({ _id: ProjID })
        let cityinfo = await SysTable.findOne({ _id: cityid.item1 })
        let provinfo = await SysTable.findOne({ _id: cityinfo.item1 })
        try {
            let result = {
                City: {
                    _id: cityinfo._id,
                    Name: cityinfo.item0
                },
                Prov: {
                    _id: provinfo._id,
                    Name: provinfo.item0
                }
            }
            res(result)
        } catch (error) {
            rej(error)
        }
    })
}

/**
 * @api {POST} /api/SetMeetingReaded 
 * @apiname 设置会议以读未读状态
 * @apiGroup Meeting
 * @apiParam {String} MeetingID 会议ID.
 */
exports.SetMeetingReaded = async (req, res) => {
    let query = {
        _id: req.query.MeetingID
    }
    try {
        await Meeting.update(query, { IsReaded: 1 })
        res.send(msg.genSuccessMsg('更新成功!'))
    } catch (error) {
        res.send(msg.genFailedMsg('设置已读失败->' + error))
    }
}