var express = require('express');
var router = express.Router();
var user = require('../mongodb/controllers/user');
var caseshow = require('../mongodb/controllers/caseshow');
var SysField = require('../mongodb/controllers/SysField');
var SysTable = require('../mongodb/controllers/SysTable');
//var login_controller = require('../controllers/login_controller')

router.param('id', user.load);
router.get('/user', user.list);
router.post('/user', user.create);
router.post('/user/checklogin', user.checklogin);
router.post('/user/:id', user.update);
router.delete('/user/:id', user.delete);
router.post('/login', user.login);

router.get('/caseshow', caseshow.GetCase)
router.post('/caseshow/:id', caseshow.GetCaseByUserID)
router.delete('/caseshow/:id', caseshow.DelCaseByID)
router.post('/caseshow', caseshow.SaveCase)

router.get('/SysField', SysField.list)
router.post('/SysField/getbyid', SysField.getbyid)
router.post('/SysField/create', SysField.create)
router.post('/SysField/delete', SysField.delete)
router.get('/SysField/AllFieldName', SysField.GetAllFieldName)

router.get('/SysTable', SysTable.list)
router.post('/SysTable/getbyid', SysTable.getbyid)
router.post('/SysTable/update', SysTable.update)
router.post('/SysTable/create', SysTable.create)
router.post('/SysTable/delete', SysTable.delete)
/******************************************/
module.exports = router;
