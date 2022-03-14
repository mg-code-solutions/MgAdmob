using System;
using System.ComponentModel;
using System.Diagnostics;
using Android.Runtime;
using Java.Interop;

namespace Android.Gms.Ads.Hack;

[Register("com/google/android/gms/ads/interstitial/InterstitialAdLoadCallback", DoNotGenerateAcw = true)]
public abstract class MgInterstitialAdLoadCallback : AdLoadCallback
{
   private static readonly JniPeerMembers Members = new XAPeerMembers("com/google/android/gms/ads/interstitial/InterstitialAdLoadCallback", typeof(MgInterstitialAdLoadCallback));

   /*internal*/
   private static IntPtr ClassRef => Members.JniPeerType.PeerReference.Handle;

   [DebuggerBrowsable(DebuggerBrowsableState.Never)]
   [EditorBrowsable(EditorBrowsableState.Never)]
   public override JniPeerMembers JniPeerMembers => Members;

   [DebuggerBrowsable(DebuggerBrowsableState.Never)]
   [EditorBrowsable(EditorBrowsableState.Never)]
   protected override IntPtr ThresholdClass => Members.JniPeerType.PeerReference.Handle;

   [DebuggerBrowsable(DebuggerBrowsableState.Never)]
   [EditorBrowsable(EditorBrowsableState.Never)]
   protected override Type ThresholdType => Members.ManagedPeerType;

   protected MgInterstitialAdLoadCallback(IntPtr javaReference, JniHandleOwnership transfer)
       : base(javaReference, transfer)
   {
   }

   [Register(".ctor", "()V", "")]
   public unsafe MgInterstitialAdLoadCallback()
       : base(IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
   {
      if (Handle != IntPtr.Zero)
      {
         return;
      }

      SetHandle(Members.InstanceMethods.StartCreateInstance("()V", GetType(), null).Handle, JniHandleOwnership.TransferLocalRef);

      Members.InstanceMethods.FinishCreateInstance("()V", this, null);
   }

   private static Delegate _onAdLoadedCallback;

   private static Delegate GetOnAdLoadedHandler()
   {
      if (_onAdLoadedCallback is null)
      {
         _onAdLoadedCallback = JNINativeWrapper.CreateDelegate(n_onAdLoaded);
      }

      return _onAdLoadedCallback;
   }

   private static void n_onAdLoaded(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
   {
      var callback = GetObject<MgInterstitialAdLoadCallback>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
      var interstitialAd = GetObject<Interstitial.InterstitialAd>(native_p0, JniHandleOwnership.DoNotTransfer);

      callback!.OnInterstitialAdLoaded(interstitialAd);
   }

   [Register("onAdLoaded", "(Lcom/google/android/gms/ads/interstitial/InterstitialAd;)V", "GetOnAdLoadedHandler")]
   public virtual void OnInterstitialAdLoaded(Interstitial.InterstitialAd interstitialAd)
   {
   }
}

