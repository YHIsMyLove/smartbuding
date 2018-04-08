
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Java.Lang;
using SmartConstructionSite.Core.Events.Services;
using SmartConstructionSite.Core.Events.Models;
using SmartConstructionSite.Core.Common;
using System.Linq;

namespace SmartConstructionSite.Droid
{
    [Service(Label = "MeetingService")]
    [IntentFilter(new string[] { "com.yourname.MeetingService" })]
    public class MeetingService : IntentService
    {
        public const string ActionNewMeeting = "cn.yooling.scs.SmartConstructionSite.NewMeeting";

        IBinder binder;
        private bool running;
        private MeetingReceiver meetingReceiver;
        private EventService eventService = new EventService();

        protected override void OnHandleIntent(Intent intent)
        {
            if (running) return;
            running = true;

            meetingReceiver = new MeetingReceiver();
            IntentFilter intentFilter = new IntentFilter(ActionNewMeeting);
            RegisterReceiver(meetingReceiver, intentFilter);

            Task<Core.Common.Result<IList<Meeting>>> task = eventService.FetchLatestEvent();
            while (running)
            {
                if (task.IsCompleted || task.IsCanceled || task.IsFaulted)
                {
                    if (task.IsCompleted)
                    {
                        if (!task.Result.HasError)
                        {
                            var newMeetings = task.Result.Model
                                                  .Where((meeting) => { return meeting.MeetingCreatedAt > ServiceContext.Instance.LatestMeetingTime; })
                                                  .ToList();
                            if (newMeetings.Count != 0)
                            {
                                newMeetings.Sort((x, y) =>
                                {
                                    if (x.MeetingCreatedAt > y.MeetingCreatedAt)
                                        return -1;
                                    else if (x.MeetingCreatedAt == y.MeetingCreatedAt)
                                        return 0;
                                    else
                                        return 1;
                                });
                                ServiceContext.Instance.LatestMeetingTime = newMeetings[0].MeetingCreatedAt;
                                StringBuilder strBuilder = new StringBuilder();
                                for (int i = 0; i < newMeetings.Count; i++)
                                {
                                    strBuilder.Append(newMeetings[i].MeetingName);
                                    if (i != newMeetings.Count - 1)
                                        strBuilder.Append("\n");
                                }

                                Notification.Builder builder = new Notification.Builder(this);
                                builder.SetSmallIcon(Resource.Drawable.icon);
                                builder.SetTicker($"{newMeetings.Count}条新会议\n");
                                builder.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis());
                                builder.SetContentTitle("新的会议");
                                builder.SetContentText(strBuilder.ToString());

                                Intent intent1 = new Intent(ActionNewMeeting);
                                PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, intent1, PendingIntentFlags.CancelCurrent);
                                builder.SetContentIntent(pendingIntent);//设置点击过后跳转的activity  
                                /*builder.setDefaults(Notification.DEFAULT_SOUND);//设置声音 
                                builder.setDefaults(Notification.DEFAULT_LIGHTS);//设置指示灯 
                                builder.setDefaults(Notification.DEFAULT_VIBRATE);//设置震动*/
                                builder.SetDefaults(NotificationDefaults.All);//设置全部  

                                Notification notification = builder.Build();//4.1以上用.build();  
                                notification.Flags |= NotificationFlags.AutoCancel;// 点击通知的时候cancel掉  
                                NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);
                                manager.Notify(1, notification);
                            }
                        }
                        try
                        {
                            Thread.Sleep(3000);
                        }
                        catch(InterruptedException){}
                    }
                    task = eventService.FetchLatestEvent();
                }
            }
        }

		public override void OnDestroy()
		{
            base.OnDestroy();
            running = false;
            UnregisterReceiver(meetingReceiver);
		}

		public override IBinder OnBind(Intent intent)
        {
            binder = new MeetingServiceBinder(this);
            return binder;
        }
    }

    public class MeetingServiceBinder : Binder
    {
        readonly MeetingService service;

        public MeetingServiceBinder(MeetingService service)
        {
            this.service = service;
        }

        public MeetingService GetMeetingService()
        {
            return service;
        }
    }
}
