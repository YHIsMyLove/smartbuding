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
/**client***********************************************************************/
/*******************************************************************************/
{
    path: '/BigNewsElement',
    component: resolve => require(['./components/APIBuilder/BigNewsElement.vue'], resolve),
    name: '大事件管理',
    meta: { redirectAuth: true }
},