var express = require('express');
var router = express.Router();
const business = require('../mongodb/business/TP_Business')

router.post('/SaveOrUpdateUser/', business.SAVEORUPDATEUSER)
router.post('/SaveOrUpdateDev/', business.SAVEORUPDATEDEV)
router.post('/SaveDoorIO/', business.SAVEDOORIO)