/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const Devices = require('../mongodb/APIBuilder/DevicesBusiness')

router.get('/GetDevicesByID', Devices.GetDevicesByID);
router.get('/DelDevicesByID', Devices.DelDevicesByID);
router.get('/ListDevices', Devices.ListDevices);
router.post('/CreateDevices', Devices.CreateOrUpdateDevices);
router.post('/UpdateDevices', Devices.CreateOrUpdateDevices);
router.post('/CreateOrUpdateDevices', Devices.CreateOrUpdateDevices);
/*******************************************************************************/
/**client***********************************************************************/
/*******************************************************************************/
{
    path: '/DevicesElement',
    component: resolve => require(['./components/APIBuilder/DevicesElement.vue'], resolve),
    name: '设备管理管理',
    meta: { redirectAuth: true }
},