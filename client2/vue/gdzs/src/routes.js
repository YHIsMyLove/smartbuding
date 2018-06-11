import HomePage from './pages/home.vue';
import AboutPage from './pages/about.vue';
import FormPage from './pages/form.vue';
import DynamicRoutePage from './pages/dynamic-route.vue';
import NotFoundPage from './pages/not-found.vue';

import PanelLeftPage from './pages/panel-left.vue';
import PanelRightPage from './pages/panel-right.vue';
import ProjectMain from './pages/project/main.vue';
import ProjectList from './pages/project/list.vue';
import AccountMain from './pages/account/main.vue';
import AccountLogin from './pages/account/login.vue';
import DoorsMain from './pages/doors/main.vue';
import DoorsStatistics from './pages/doors/statistics.vue';
import WorkersMain from './pages/workers/main.vue';
import WorkersDetail from './pages/workers/detail.vue';
import MeetingList from './pages/meetings/list.vue';
import MeetingDetail from './pages/meetings/detail.vue';
import RankingMain from './pages/ranking/main.vue';
import EspeciallyTaskMain from './pages/especially_task/main.vue';
import EspeciallyTaskCreateDhzy from './pages/especially_task/dhzy-create.vue';
import EspeciallyTaskCheckDhzy from './pages/especially_task/dhzy-check.vue';
import EspeciallyTaskEndorseCommit from './pages/especially_task/dhzy-endorse-commit.vue'
import EspeciallyTaskRefuseCommit from './pages/especially_task/dhzy-refuse-commit.vue';
import EnvironmentMain from './pages/environment/main.vue';
import DeviceMain from './pages/device/main.vue';
import DeviceDetail from './pages/device/detail.vue';
import CameraList from './pages/cameras/list.vue';
import EventsMain from './pages/events/main.vue';

export default [
  {
    path: '/',
    component: HomePage,
  },
  {
    path: '/panel-left/',
    component: PanelLeftPage,
  },
  {
    path: '/panel-right/',
    component: PanelRightPage,
  },
  {
    path: '/about/',
    component: AboutPage,
  },
  {
    path: '/form/',
    component: FormPage,
  },
  {
    path: '/dynamic-route/blog/:blogId/post/:postId/',
    component: DynamicRoutePage,
  },
  {
    path: '/project/main/',
    component: ProjectMain,
  },
  {
    path: '/project/list/',
    component: ProjectList,
  },
  {
    path: '/account/main/',
    component: AccountMain,
  },
  {
    path: '/doors/main/',
    component: DoorsMain,
  },
  {
    path: '/doors/statistics/',
    component: DoorsStatistics,
  },
  {
    path: '/workers/main/',
    component: WorkersMain,
  },
  {
    path: '/workers/detail/',
    component: WorkersDetail,
  },
  {
    path: '/meetings/list/',
    component: MeetingList,
  },
  {
    path: '/meetings/detail/',
    component: MeetingDetail,
  },
  {
    path: '/ranking/main/',
    component: RankingMain,
  },
  {
    path: '/especially-task/main/',
    component: EspeciallyTaskMain,
  },
  {
    path: '/especially-task/create-dhzy/',
    component: EspeciallyTaskCreateDhzy,
  },
  {
    path: '/especially-task/check-dhzy/',
    component: EspeciallyTaskCheckDhzy,
  },
  {
    path: '/especially-task/commit-endorse/',
    component: EspeciallyTaskEndorseCommit,
  },
  {
    path: '/especially-task/commit-refuse/',
    component: EspeciallyTaskRefuseCommit,
  },
  {
    path: '/env/main/',
    component: EnvironmentMain,
  },
  {
    path: '/device/main',
    component: DeviceMain,
  },
  {
    path: '/device/detail',
    component: DeviceDetail,
  },
  {
    path: '/camera/list/',
    component: CameraList,
  },
  {
    path: '/events/main',
    component: EventsMain,
  },
  {
    path: '/login/',
    component: AccountLogin,
  },
  {
    path: '(.*)',
    component: NotFoundPage,
  }
];
