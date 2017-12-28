var express = require('express');
var router = express.Router();
var user = require('../mongodb/controllers/user');
var caseshow = require('../mongodb/controllers/caseshow');
//var login_controller = require('../controllers/login_controller')

router.param('id', user.load);
router.get('/user', user.list);
router.post('/user', user.create);
router.post('/user/:id', user.update);
router.delete('/user/:id', user.delete);

router.get('/caseshow', caseshow.GetCase)
router.post('/caseshow/:id', caseshow.GetCaseByUserID)
router.delete('/caseshow/:id', caseshow.DelCaseByID)
router.post('/caseshow', caseshow.SaveCase)

/******************************************/
module.exports = router;
