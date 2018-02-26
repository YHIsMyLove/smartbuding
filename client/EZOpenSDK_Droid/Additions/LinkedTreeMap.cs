//using System;
//using Android.Runtime;
//namespace Com.Google.Gson.Internal
//{
//    public sealed partial class LinkedTreeMap : global::Java.Util.AbstractMap, global::Java.IO.ISerializable
//    {
//        static IntPtr id_entrySet;
//        public override unsafe global::System.Collections.ICollection EntrySet()
//        {
//            if (id_entrySet == IntPtr.Zero)
//                id_entrySet = JNIEnv.GetMethodID(class_ref, "entrySet", "()Ljava/util/Set;");
//            try
//            {
//                return global::Android.Runtime.JavaSet.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_entrySet), JniHandleOwnership.TransferLocalRef);
//            }
//            finally
//            {
//            }
//        }
//    }
//}
