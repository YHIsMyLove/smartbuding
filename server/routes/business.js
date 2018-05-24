var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/business')

/***************************************************************************************/
/*用户相关业务***************************************************************************/
/***************************************************************************************/
const USER_BUSINESS = require('../mongodb/business/Users_Business.js')
router.post("/Login", USER_BUSINESS.Login)
router.post("/LogOut", USER_BUSINESS.LogOut)
router.post("/CheckSession", USER_BUSINESS.checkSession)
router.post("/CreateUser", USER_BUSINESS.create)
router.get("/GetUser", USER_BUSINESS.getUserInfo)
router.get("/GetProvByUser", USER_BUSINESS.GetProvByUser)
router.get("/GetProjByUser", USER_BUSINESS.GetProjByUser)
/***************************************************************************************/
/*会议相关业务***************************************************************************/
/***************************************************************************************/
const MEETING_BUSINESS = require('../mongodb/business/Meeting_Business.js')
router.post("/InsertOrUpdateMeeting", MEETING_BUSINESS.InsertOrUpdateMeeting)
router.post("/InsertOrUpdateMeetingContent", MEETING_BUSINESS.InsertOrUpdateMeetingContent)
router.get("/GetMeetings", MEETING_BUSINESS.GetMeetings)
router.get("/GetMeetingContentByMeetingID", MEETING_BUSINESS.GetMeetingContentByMeetingID)
router.get("/APP/GetMeetingContents", MEETING_BUSINESS.GetMeetingContents_APP)
router.get("/APP/GetMeetings", MEETING_BUSINESS.GetMeetings_APP)
/***************************************************************************************/
/*门禁业务相关***************************************************************************/
/***************************************************************************************/
const DOORIOINFO = require('../mongodb/business/DoorIO_Business')
router.post('/SetDoorIOInfo', DOORIOINFO.SetDoorIOInfo)
/***************************************************************************************/
/***************************************************************************************/
/***************************************************************************************/
// const TP_BUSINESS = require('../mongodb/business/TP_Business.js')
// TP_BUSINESS.SAVEORUPDATEUSER()
/***************************************************************************************/
/***************************************************************************************/
/***************************************************************************************/
router.post("/InsertOrDelUserProj", business.InsertOrDelUserProj)
router.post("/InsertOrDelUserDept", business.InsertOrDelUserDept)
router.post("/InsertOrDelUserRole", business.InsertOrDelUserRole)
router.get("/GetDeptTreeByProjID", business.GetDeptTreeByProjID)
router.get("/GetDeptByProjID", business.GetDeptByProjID)
router.get("/GetUserByProjID", business.GetUserByProjID)
router.get("/GetUserByDeptID", business.GetUserByDeptID)
router.get("/GetUserByRoleID", business.GetUserByRoleID)
router.get("/GetYSDevs", business.GetYSDevs)
router.post("/UploadFile", business.UploadFile)
router.get("/GetFiles", business.GetFiles)
router.get("/GetFileKeyByIndex", business.GetFileKeyByIndex)
router.get("/GetRole", business.GetRole)
router.get("/GetProv", business.GetProv)
router.get("/GetCityByProvID", business.GetCityByProvID)
router.get("/GetProjByCityID", business.GetProjByCityID)
router.get("/GetProjByProvID", business.GetProjByProvID)
module.exports = router;