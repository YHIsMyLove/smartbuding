var express = require('express');
var router = express.Router();
const user = require('../mongodb/controllers/user')
const Device = require('../mongodb/controllers/Device')
const SysField = require('../mongodb/controllers/SysField')
const SysTable = require('../mongodb/controllers/SysTable')


router.param('id', user.load);
router.get('/user', user.list);
router.get('/user/checkID', user.checkID);
router.post('/user', user.create);
router.post('/user/checklogin', user.checklogin);
router.post('/user/:id', user.update);
router.delete('/user/:id', user.delete);

router.get('/Device', Device.list);

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


/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const RankingList = require('../mongodb/APIBuilder/RankingListBusiness')

const RankingListExt = require('../mongodb/APIBuilder_Ext/RankingListBusiness')

router.get('/GetRankingListByID', RankingList.GetRankingListByID);
router.get('/DelRankingListByID', RankingList.DelRankingListByID);
router.get('/ListRankingList', RankingList.ListRankingList);
router.post('/CreateRankingList', RankingList.CreateOrUpdateRankingList);
router.post('/UpdateRankingList', RankingList.CreateOrUpdateRankingList);
router.post('/CreateOrUpdateRankingList', RankingList.CreateOrUpdateRankingList);

router.get('/GetRankingOrder', RankingListExt.GetRankingOrder);

router.get('/GetRankingList', RankingListExt.GetRankingList);
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
const MenuAuth = require('../mongodb/APIBuilder/MenuAuthBusiness')

router.get('/GetMenuAuthByID', MenuAuth.GetMenuAuthByID);
router.get('/DelMenuAuthByID', MenuAuth.DelMenuAuthByID);
router.get('/ListMenuAuth', MenuAuth.ListMenuAuth);
router.get('/GetRolebyMenuAuth', MenuAuth.GetRolebyMenuAuth);
router.get('/GetMenuByUser', MenuAuth.GetMenuByUser);
router.post('/CreateMenuAuth', MenuAuth.CreateOrUpdateMenuAuth);
router.post('/UpdateMenuAuth', MenuAuth.CreateOrUpdateMenuAuth);
router.post('/CreateOrUpdateMenuAuth', MenuAuth.CreateOrUpdateMenuAuth);
router.post('/UpdateOrDelbyMenuAuth', MenuAuth.UpdateOrDelbyMenuAuth);
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
const DoorIO = require('../mongodb/APIBuilder/DoorIOBusiness')

router.get('/GetDoorIOByID', DoorIO.GetDoorIOByID);
router.get('/DelDoorIOByID', DoorIO.DelDoorIOByID);
router.get('/ListDoorIO', DoorIO.ListDoorIO);
router.post('/CreateDoorIO', DoorIO.CreateOrUpdateDoorIO);
router.post('/UpdateDoorIO', DoorIO.CreateOrUpdateDoorIO);
router.post('/CreateOrUpdateDoorIO', DoorIO.CreateOrUpdateDoorIO);
/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const BigNews = require('../mongodb/APIBuilder/BigNewsBusiness')

router.get('/GetBigNewsByID', BigNews.GetBigNewsByID);
router.get('/DelBigNewsByID', BigNews.DelBigNewsByID);
router.get('/ListBigNews', BigNews.ListBigNews);
router.post('/CreateBigNews', BigNews.CreateOrUpdateBigNews);
router.post('/UpdateBigNews', BigNews.CreateOrUpdateBigNews);
router.post('/CreateOrUpdateBigNews', BigNews.CreateOrUpdateBigNews);
/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const QualityAcceptance = require('../mongodb/APIBuilder/QualityAcceptanceBusiness')

router.get('/GetQualityAcceptanceByID', QualityAcceptance.GetQualityAcceptanceByID);
router.get('/DelQualityAcceptanceByID', QualityAcceptance.DelQualityAcceptanceByID);
router.get('/ListQualityAcceptance', QualityAcceptance.ListQualityAcceptance);
router.post('/CreateQualityAcceptance', QualityAcceptance.CreateOrUpdateQualityAcceptance);
router.post('/UpdateQualityAcceptance', QualityAcceptance.CreateOrUpdateQualityAcceptance);
router.post('/CreateOrUpdateQualityAcceptance', QualityAcceptance.CreateOrUpdateQualityAcceptance);
/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const Approve = require('../mongodb/business/Approve_Business')

router.post('/CreateApprove', Approve.CreateApprove)
router.post('/OptionApprove', Approve.OptionApprove)
router.get('/ListApprove', Approve.ListApprove)
/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
module.exports = router;
