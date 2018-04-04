var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/business')
const USER_BUSINESS =require('../mongodb/business/Users_Business.js')

router.post("/Login", USER_BUSINESS.Login)
router.post("/LogOut", USER_BUSINESS.LogOut)
router.post("/CheckSession", USER_BUSINESS.checkSession)
router.get("/GetUser", USER_BUSINESS.getUserInfo)
router.get("/GetProvByUser", USER_BUSINESS.GetProvByUser)
router.get("/GetProjByUser", USER_BUSINESS.GetProjByUser)


router.post("/InsertOrDelUserProj", business.InsertOrDelUserProj)
router.post("/InsertOrDelUserDept", business.InsertOrDelUserDept)
router.post("/InsertOrDelUserRole", business.InsertOrDelUserRole)
router.get("/GetDeptTreeByProjID", business.GetDeptTreeByProjID)
router.get("/GetDeptByProjID", business.GetDeptByProjID)
router.get("/GetUserByProjID", business.GetUserByProjID)
router.get("/GetUserByDeptID", business.GetUserByDeptID)
router.get("/GetUserByRoleID", business.GetUserByRoleID)

router.post("/InsertOrUpdateMeeting", business.InsertOrUpdateMeeting)
router.post("/InsertOrUpdateMeetingContent", business.InsertOrUpdateMeetingContent)
router.get("/GetMeetings", business.GetMeetings)
router.get("/GetMeetingContentByMeetingID", business.GetMeetingContentByMeetingID)


router.get("/GetYSDevs", business.GetYSDevs)


router.post("/UploadFile", business.UploadFile)

router.get("/GetFiles", business.GetFiles)
router.get("/GetFileKeyByIndex", business.GetFileKeyByIndex)

router.get("/GetRole", business.GetRoleByRoleID)
router.get("/GetProv", business.GetProv)


router.get("/GetCityByProvID", business.GetCityByProvID)
router.get("/GetProjByCityID", business.GetProjByCityID)
router.get("/GetProjByProvID", business.GetProjByProvID)

/*********************************************************************************************** */
/*********************************************************************************************** */
/*********************************************************************************************** */
router.get("/APP/GetMeetingContents", business.GetMeetingContents_APP)
router.get("/APP/GetMeetings", business.GetMeetings_APP)

module.exports = router;