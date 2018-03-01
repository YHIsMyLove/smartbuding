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
using Com.Videogo.Openapi.Bean;
using Android.Text;
using Com.Bumptech.Glide;

namespace SmartConstructionSite.Droid.OnlineMonitoring
{
    class CameraListAdapter : BaseAdapter
    {

        Context context;
        List<EZDeviceInfo> devices;

        public CameraListAdapter(Context context)
        {
            this.context = context;
        }

        public void SetCameras(IList<EZDeviceInfo> devices)
        {
            this.devices = new List<EZDeviceInfo>(devices);
            NotifyDataSetChanged();
        }

        public void AddCameras(params EZDeviceInfo[] devices)
        {
            if (this.devices == null)
                this.devices = new List<EZDeviceInfo>();
            this.devices.AddRange(devices);
            NotifyDataSetChanged();
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return (devices == null || devices.Count == 0) ? null : devices[position];
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

            if (holder == null)
            {
                holder = new CameraListAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.camera_list_item, parent, false);
                view.Tag = holder;
                holder.TextViewName = view.FindViewById<TextView>(Resource.Id.textViewName);
                holder.ImageViewSnapshot = view.FindViewById<ImageView>(Resource.Id.imageViewSnapshot);
            }

            EZDeviceInfo device = devices[position];
            holder.TextViewName.Text = device.DeviceName;
            String imageUrl = device.DeviceCover;
            if (!TextUtils.IsEmpty(imageUrl))
            {
                //Glide.With(context).Load(imageUrl).Placeholder().into(viewHolder.iconIv);
                Glide.With(context).Load(imageUrl).Into(holder.ImageViewSnapshot);
            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count {
            get {
                return devices == null ? 0 : devices.Count;
            }
        }

    }

    class CameraListAdapterViewHolder : Java.Lang.Object
    {
        public TextView TextViewName { get; set; }

        public ImageView ImageViewSnapshot { get; set; }
    }
}