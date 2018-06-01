/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const RankingList = require('../mongodb/business/RankingListBusiness')

router.get('/GetRankingListByID', RankingList.GetRankingListByID);
router.get('/DelRankingListByID', RankingList.DelRankingListByID);
router.get('/ListRankingList', RankingList.ListRankingList);
router.post('/CreateRankingList', RankingList.CreateOrUpdateRankingList);
router.post('/UpdateRankingList', RankingList.CreateOrUpdateRankingList);
router.post('/CreateOrUpdateRankingList', RankingList.CreateOrUpdateRankingList);
/*******************************************************************************/
/**client***********************************************************************/
/*******************************************************************************/