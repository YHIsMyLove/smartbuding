var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/business')

router.post("/login", business.login)
router.get("/GetDeptByProjID", business.GetDeptByProjID)

module.exports = router;