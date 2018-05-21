
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartConstructionSite.Droid
{
    [BroadcastReceiver]
    public class MeetingReceiver : BroadcastReceiver
    {
        private Handler handler = new Handler();

        public override void OnReceive(Context context, Intent intent)
        {
            if (IsForeground(context, typeof(MainActivity).FullName))
            {
                context.SendBroadcast(new Intent(MainActivity.ActionMeetingListPage));
            }
            else
            {
                Intent intent1 = new Intent(context, typeof(MainActivity));
                intent1.SetAction(MainActivity.ActionMeetingListPage);
                context.StartActivity(intent1);
            }
        }

        public static bool IsForeground(Context context, string className)
        {
            if (context == null || Android.Text.TextUtils.IsEmpty(className))
            {
                return false;
            }

            ActivityManager mgr = (ActivityManager)context.GetSystemService(Context.ActivityService);
            IList<ActivityManager.RunningTaskInfo> list = mgr.GetRunningTasks(1);
            if (list != null && list.Count > 0)
            {
                ComponentName cpn = list[0].TopActivity;
                if (className.Substring(className.LastIndexOf(".") + 1).Equals(cpn.ClassName.Substring(cpn.ClassName.LastIndexOf(".") + 1)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
