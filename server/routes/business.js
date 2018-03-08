var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/business')

router.post("/login",business.login)
router.post("/getProjsbyUser",business.getProjsbyUser)

module.exports = router;