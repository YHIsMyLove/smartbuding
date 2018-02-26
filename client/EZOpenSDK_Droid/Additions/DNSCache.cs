//using System;
//using Android.Runtime;
//namespace Javax.Jmdns.Impl
//{
//    public partial class DNSCache : global::Java.Util.AbstractMap
//    {
//        static IntPtr id_entrySet;
//        public override unsafe global::System.Collections.ICollection EntrySet()
//        {
//            if (id_entrySet == IntPtr.Zero)
//                id_entrySet = JNIEnv.GetMethodID(class_ref, "entrySet", "()Ljava/util/Set;");
//            try
//            {

//                if (((object)this).GetType() == ThresholdType)
//                    return global::Android.Runtime.JavaSet.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_entrySet), JniHandleOwnership.TransferLocalRef);
//                else
//                    return global::Android.Runtime.JavaSet.FromJniHandle(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "entrySet", "()Ljava/util/Set;")), JniHandleOwnership.TransferLocalRef);
//            }
//            finally
//            {
//            }
//        }

//        protected internal partial class _CacheEntry : global::Java.Lang.Object, global::Java.Util.IMapEntry
//        {
//            static IntPtr id_getKey;
//            public virtual unsafe global::Java.Lang.Object Key
//            {
//                // Metadata.xml XPath method reference: path="/api/package[@name='javax.jmdns.impl']/class[@name='DNSCache._CacheEntry']/method[@name='getKey' and count(parameter)=0]"
//                [Register("getKey", "()Ljava/lang/String;", "GetGetKeyHandler")]
//                get
//                {
//                    if (id_getKey == IntPtr.Zero)
//                        id_getKey = JNIEnv.GetMethodID(class_ref, "getKey", "()Ljava/lang/String;");
//                    try
//                    {

//                        if (((object)this).GetType() == ThresholdType)
//                            return (Java.Lang.Object)JNIEnv.GetString(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getKey), JniHandleOwnership.TransferLocalRef);
//                        else
//                            return (Java.Lang.Object)JNIEnv.GetString(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "getKey", "()Ljava/lang/String;")), JniHandleOwnership.TransferLocalRef);
//                    }
//                    finally
//                    {
//                    }
//                }
//            }

//            static IntPtr id_getValue;
//            public virtual unsafe global::Java.Lang.Object Value
//            {
//                // Metadata.xml XPath method reference: path="/api/package[@name='javax.jmdns.impl']/class[@name='DNSCache._CacheEntry']/method[@name='getValue' and count(parameter)=0]"
//                [Register("getValue", "()Ljava/util/List;", "GetGetValueHandler")]
//                get
//                {
//                    if (id_getValue == IntPtr.Zero)
//                        id_getValue = JNIEnv.GetMethodID(class_ref, "getValue", "()Ljava/util/List;");
//                    try
//                    {

//                        if (((object)this).GetType() == ThresholdType)
//                            return (Java.Lang.Object)global::Android.Runtime.JavaList<global::Javax.Jmdns.Impl.DNSEntry>.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getValue), JniHandleOwnership.TransferLocalRef);
//                        else
//                            return (Java.Lang.Object)global::Android.Runtime.JavaList<global::Javax.Jmdns.Impl.DNSEntry>.FromJniHandle(JNIEnv.CallNonvirtualObjectMethod(((global::Java.Lang.Object)this).Handle, ThresholdClass, JNIEnv.GetMethodID(ThresholdClass, "getValue", "()Ljava/util/List;")), JniHandleOwnership.TransferLocalRef);
//                    }
//                    finally
//                    {
//                    }
//                }
//            }
//        }
//    }
//}
