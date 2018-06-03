/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const MenuAuth = require('../mongodb/APIBuilder/MenuAuthBusiness')

router.get('/GetMenuAuthByID', MenuAuth.GetMenuAuthByID);
router.get('/DelMenuAuthByID', MenuAuth.DelMenuAuthByID);
router.get('/ListMenuAuth', MenuAuth.ListMenuAuth);
router.post('/CreateMenuAuth', MenuAuth.CreateOrUpdateMenuAuth);
router.post('/UpdateMenuAuth', MenuAuth.CreateOrUpdateMenuAuth);
router.post('/CreateOrUpdateMenuAuth', MenuAuth.CreateOrUpdateMenuAuth);
/*******************************************************************************/
/**client***********************************************************************/
/*******************************************************************************/
{
    path: '/MenuAuthElement',
    component: resolve => require(['./components/APIBuilder/MenuAuthElement.vue'], resolve),
    name: '路由权限表管理',
    meta: { redirectAuth: true }
},