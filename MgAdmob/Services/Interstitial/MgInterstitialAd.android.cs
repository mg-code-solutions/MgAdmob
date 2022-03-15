using System;
using Android.Content;
using Android.Gms.Ads.Interstitial;
using Android.Runtime;
using Java.Interop;

namespace Android.Gms.Ads.Hack;

public abstract class MgInterstitialAd : Java.Lang.Object
{
   private static readonly JniPeerMembers Members = new XAPeerMembers("com/google/android/gms/ads/interstitial/InterstitialAd", typeof(InterstitialAd));

   public static unsafe void Load(Context context, string adUnit, AdRequest request, AdLoadCallback callback)
   {
      var intPtr = JNIEnv.NewString(adUnit);

      try
      {
         var ptr = stackalloc JniArgumentValue[4];

         *ptr = new JniArgumentValue(context?.Handle ?? IntPtr.Zero);
         ptr[1] = new JniArgumentValue(intPtr);
         ptr[2] = new JniArgumentValue(request?.Handle ?? IntPtr.Zero);
         ptr[3] = new JniArgumentValue(callback?.Handle ?? IntPtr.Zero);

         Members.StaticMethods.InvokeVoidMethod("load.(Landroid/content/Context;Ljava/lang/String;Lcom/google/android/gms/ads/AdRequest;Lcom/google/android/gms/ads/interstitial/InterstitialAdLoadCallback;)V", ptr);
      }
      finally
      {
         JNIEnv.DeleteLocalRef(intPtr);
         GC.KeepAlive(context);
         GC.KeepAlive(request);
         GC.KeepAlive(callback);
      }
   }
}

