var express = require('express');
var router = express.Router();
var user = require('../mongodb/controllers/user');
//var login_controller = require('../controllers/login_controller')

router.param('id', user.load);
router.get('/user', user.list);
router.post('/user', user.create);
router.post('/user/:id', user.update);
router.delete('/user/:id', user.delete);

//router.get('/test', user.test);
/******************************************/
router.post('/login', user.login)
module.exports = router;
