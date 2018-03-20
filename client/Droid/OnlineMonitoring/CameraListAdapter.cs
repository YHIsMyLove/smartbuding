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
using Android.Text;
using Com.Bumptech.Glide;
using Com.Videogo.Openapi.Bean;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    class CameraListAdapter : BaseAdapter
    {

        Context context;
        //List<EZDeviceInfo> devices;
        //List<string> groups;
        List<object> datas = new List<object>();
        Dictionary<string, IList<EZDeviceInfo>> deviceDic = new Dictionary<string, IList<EZDeviceInfo>>();

        public CameraListAdapter(Context context)
        {
            this.context = context;
        }

        //public void SetCameras(IList<EZDeviceInfo> devices)
        //{
        //    this.devices = new List<EZDeviceInfo>(devices);
        //    NotifyDataSetChanged();
        //}

        public void AddCameras(string group, params EZDeviceInfo[] devices)
        {
            if (deviceDic.ContainsKey(group))
            {
                foreach (var item in devices)
                {
                    deviceDic[group].Add(item);
                }
            }
            else
            {
                deviceDic.Add(group, devices);
            }
            datas.Clear();
            foreach (var item in deviceDic)
            {
                datas.Add(item.Key);
                datas.AddRange(item.Value);
            }
            NotifyDataSetChanged();
        }

		public override bool IsEnabled(int position)
		{
            return !deviceDic.ContainsKey(datas[position].ToString());
		}

		public override Java.Lang.Object GetItem(int position)
        {
            return datas.Count == 0 ? null : (Java.Lang.Object)datas[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            CameraListAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as CameraListAdapterViewHolder;

            object data = datas[position];
            bool isGroup = deviceDic.ContainsKey(datas[position].ToString());

            if (holder == null)
            {
                holder = new CameraListAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();

                if (isGroup)
                {
                    view = inflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
                    view.Tag = holder;
                    holder.TextViewName = (TextView)view;
                    holder.TextViewName.SetTextColor(Android.Graphics.Color.Rgb(54, 112, 212));
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.camera_list_item, parent, false);
                    view.Tag = holder;
                    holder.TextViewName = view.FindViewById<TextView>(Resource.Id.textViewName);
                    holder.ImageViewSnapshot = view.FindViewById<ImageView>(Resource.Id.imageViewSnapshot);
                }
            }

            if (isGroup)
            {
                //it is group
                holder.TextViewName.Text = data.ToString();
            }
            else
            {
                //it is device
                EZDeviceInfo device = (EZDeviceInfo)data;
                holder.TextViewName.Text = device.DeviceName;
                String imageUrl = device.DeviceCover;
                if (!TextUtils.IsEmpty(imageUrl))
                {
                    //Glide.With(context).Load(imageUrl).Placeholder().into(viewHolder.iconIv);
                    //Glide.With(context).Load(imageUrl).Into(holder.ImageViewSnapshot);
                }
            }
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count {
            get {
                return datas.Count;
            }
        }

    }

    class CameraListAdapterViewHolder : Java.Lang.Object
    {
        public TextView TextViewName { get; set; }

        public ImageView ImageViewSnapshot { get; set; }
    }
}