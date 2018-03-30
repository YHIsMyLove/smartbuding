var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/business')

router.post("/Login", business.Login)
router.post("/LogOut", business.LogOut)
router.post("/CheckSession", business.checkSession)

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

router.get("/GetUser", business.getUserInfo)
router.get("/GetRole", business.GetRoleByRoleID)
router.get("/GetProv", business.GetProv)



/**
 * GET UserID 通过用户ID获取相关的省份 GetProvByUser
 *  {
      "ProvName": "湖北省",
      "ProvID": "5aa7badbd354825dfc7b84c3"
    }
 * 
 */
router.get("/GetProvByUser", business.GetProvByUser)

router.get("/GetProjByUser", business.GetProjByUser)

/**
 * GET ProvID 通过省份ID获取市区列表 GetCityByProvID
 */
router.get("/GetCityByProvID", business.GetCityByProvID)

router.get("/GetProjByCityID", business.GetProjByCityID)
router.get("/GetProjByProvID", business.GetProjByProvID)

module.exports = router;