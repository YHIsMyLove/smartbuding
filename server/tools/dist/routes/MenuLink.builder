/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const MenuLink = require('../mongodb/APIBuilder/MenuLinkBusiness')

router.get('/GetMenuLinkByID', MenuLink.GetMenuLinkByID);
router.get('/DelMenuLinkByID', MenuLink.DelMenuLinkByID);
router.get('/ListMenuLink', MenuLink.ListMenuLink);
router.post('/CreateMenuLink', MenuLink.CreateOrUpdateMenuLink);
router.post('/UpdateMenuLink', MenuLink.CreateOrUpdateMenuLink);
router.post('/CreateOrUpdateMenuLink', MenuLink.CreateOrUpdateMenuLink);
/*******************************************************************************/
/**client***********************************************************************/
/*******************************************************************************/
{
    path: '/MenuLinkElement',
    component: resolve => require(['./components/APIBuilder/MenuLinkElement.vue'], resolve),
    name: '路由表管理',
    meta: { redirectAuth: true }
},