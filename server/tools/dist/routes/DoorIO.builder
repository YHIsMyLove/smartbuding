/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const DoorIO = require('../mongodb/APIBuilder/DoorIOBusiness')

router.get('/GetDoorIOByID', DoorIO.GetDoorIOByID);
router.get('/DelDoorIOByID', DoorIO.DelDoorIOByID);
router.get('/ListDoorIO', DoorIO.ListDoorIO);
router.post('/CreateDoorIO', DoorIO.CreateOrUpdateDoorIO);
router.post('/UpdateDoorIO', DoorIO.CreateOrUpdateDoorIO);
router.post('/CreateOrUpdateDoorIO', DoorIO.CreateOrUpdateDoorIO);
/*******************************************************************************/
/**client***********************************************************************/
/*******************************************************************************/
{
    path: '/DoorIOElement',
    component: resolve => require(['./components/APIBuilder/DoorIOElement.vue'], resolve),
    name: '进出记录管理',
    meta: { redirectAuth: true }
},