/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const Mechanical = require('../mongodb/APIBuilder/MechanicalBusiness')

router.get('/GetMechanicalByID', Mechanical.GetMechanicalByID);
router.get('/DelMechanicalByID', Mechanical.DelMechanicalByID);
router.get('/ListMechanical', Mechanical.ListMechanical);
router.post('/CreateMechanical', Mechanical.CreateOrUpdateMechanical);
router.post('/UpdateMechanical', Mechanical.CreateOrUpdateMechanical);
router.post('/CreateOrUpdateMechanical', Mechanical.CreateOrUpdateMechanical);
/*******************************************************************************/
/**client***********************************************************************/
/*******************************************************************************/
{
    path: '/MechanicalElement',
    component: resolve => require(['./components/APIBuilder/MechanicalElement.vue'], resolve),
    name: '机械设备检测管理',
    meta: { redirectAuth: true }
},