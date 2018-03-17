var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/business')

router.post("/login", business.login)
router.get("/GetProv", business.GetProv)
router.get("/GetRole", business.GetRoleByRoleID)
router.get("/GetCityByProvID", business.GetCityByProvID)
router.get("/GetProjByCityID", business.GetProjByCityID)
router.post("/InsertOrDelUserProj", business.InsertOrDelUserProj)
router.post("/InsertOrDelUserDept", business.InsertOrDelUserDept)
router.post("/InsertOrDelUserRole", business.InsertOrDelUserRole)
router.get("/GetDeptByProjID", business.GetDeptByProjID)
router.get("/GetUserByProjID", business.GetUserByProjID)
router.get("/GetUserByDeptID", business.GetUserByDeptID)
router.get("/GetUserByRoleID", business.GetUserByRoleID)


router.get("/GetYSDevs", business.GetYSDevs)

module.exports = router;