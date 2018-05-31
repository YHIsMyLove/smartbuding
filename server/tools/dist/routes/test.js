/*******************************************************************************/
const test = require('../mongodb/business/testBusiness')

router.get('/GettestByID', test.GettestByID);
router.get('/DeltestByID', test.DeltestByID);
router.get('/Listtest', test.Listtest);
router.post('/Createtest', test.CreateOrUpdatetest);
router.post('/Updatetest', test.CreateOrUpdatetest);
router.post('/CreateOrUpdatetest', test.CreateOrUpdatetest);
/*******************************************************************************/