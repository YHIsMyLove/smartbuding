//using System;
//using Android.Runtime;
//namespace Javax.Jmdns.Impl
//{
//    public partial class JmDNSImpl : global::Javax.Jmdns.JmDNS, global::Javax.Jmdns.Impl.IDNSStatefulObject, global::Javax.Jmdns.Impl.IDNSTaskStarter
//    {
//        partial class ServiceTypeEntry : global::Java.Util.AbstractMap, global::Java.Lang.ICloneable
//        {
//            static IntPtr id_entrySet;
//            public override unsafe global::System.Collections.ICollection EntrySet()
//            {
//                if (id_entrySet == IntPtr.Zero)
//                    id_entrySet = JNIEnv.GetMethodID(class_ref, "entrySet", "()Ljava/util/Set;");
//                try
//                {

//                    if (((object)this).GetType() == ThresholdType)
//                        return global::Android.Runtime.JavaSet.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_entrySet), JniHandleOwnership.TransferLocalRef);
//                    else
//                        return global::Android.Runtime.JavaSet.FromJniHandle(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "entrySet", "()Ljava/util/Set;")), JniHandleOwnership.TransferLocalRef);
//                }
//                finally
//                {
//                }
//            }
//        }
//    }
//}
