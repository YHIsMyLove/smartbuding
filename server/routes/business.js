var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/business')

router.post("/login", business.login)
router.get("/GetProv", business.GetProv)
router.get("/GetCityByProvID", business.GetCityByProvID)
router.get("/GetProjByCityID", business.GetProjByCityID)
router.post("/InsertOrDelUserProj", business.InsertOrDelUserProj)
router.get("/GetDeptByProjID", business.GetDeptByProjID)
router.get("/GetUserByProjID", business.GetUserByProjID)
router.get("/GetUserByDeptID", business.GetUserByDeptID)

module.exports = router;