/*******************************************************************************/
/**server***********************************************************************/
/*******************************************************************************/
const RankingList = require('../mongodb/APIBuilder/RankingListBusiness')

router.get('/GetRankingListByID', RankingList.GetRankingListByID);
router.get('/DelRankingListByID', RankingList.DelRankingListByID);
router.get('/ListRankingList', RankingList.ListRankingList);
router.post('/CreateRankingList', RankingList.CreateOrUpdateRankingList);
router.post('/UpdateRankingList', RankingList.CreateOrUpdateRankingList);
router.post('/CreateOrUpdateRankingList', RankingList.CreateOrUpdateRankingList);
/*******************************************************************************/
/**client***********************************************************************/
/*******************************************************************************/
{
    path: '/RankingListElement',
    component: resolve => require(['./components/APIBuilder/RankingListElement.vue'], resolve),
    name: '红黑榜管理',
    meta: { redirectAuth: true }
},