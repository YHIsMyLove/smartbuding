var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/business')

router.post("/Login", business.Login)
router.post("/CheckSession", business.checkSession)
router.get("/GetUser", business.getUserInfo)
router.get("/GetProv", business.GetProv)
router.get("/GetRole", business.GetRoleByRoleID)
router.get("/GetCityByProvID", business.GetCityByProvID)
router.get("/GetProjByCityID", business.GetProjByCityID)
router.post("/InsertOrDelUserProj", business.InsertOrDelUserProj)
router.post("/InsertOrDelUserDept", business.InsertOrDelUserDept)
router.post("/InsertOrDelUserRole", business.InsertOrDelUserRole)
router.get("/GetDeptTreeByProjID", business.GetDeptTreeByProjID)
router.get("/GetDeptByProjID", business.GetDeptByProjID)
router.get("/GetUserByProjID", business.GetUserByProjID)
router.get("/GetUserByDeptID", business.GetUserByDeptID)
router.get("/GetUserByRoleID", business.GetUserByRoleID)

router.post("/InsertOrUpdateMetting", business.InsertOrUpdateMetting)
router.post("/InsertOrUpdateMeetingContent", business.InsertOrUpdateMeetingContent)
router.get("/GetMettings", business.GetMettings)
router.get("/GetMeetingContentByMeetingID", business.GetMeetingContentByMeetingID)


router.get("/GetYSDevs", business.GetYSDevs)


router.post("/UploadFile", business.UploadFile)
router.get("/GetFiles", business.GetFiles)
router.get("/GetFileKeyByIndex", business.GetFileKeyByIndex)

module.exports = router;