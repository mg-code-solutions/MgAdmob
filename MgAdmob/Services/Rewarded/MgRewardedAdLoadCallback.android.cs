using System;
using System.ComponentModel;
using System.Diagnostics;
using Android.Runtime;
using Java.Interop;

namespace Android.Gms.Ads.Hack;

[Register("com/google/android/gms/ads/rewarded/RewardedAdLoadCallback", DoNotGenerateAcw = true)]
public abstract class MgRewardedAdLoadCallback : AdLoadCallback
{
   private static readonly JniPeerMembers Members = new XAPeerMembers("com/google/android/gms/ads/rewarded/RewardedAdLoadCallback", typeof(MgRewardedAdLoadCallback));


   // ReSharper disable once InconsistentNaming
   // ReSharper disable once UnusedMember.Local
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

   protected MgRewardedAdLoadCallback(IntPtr javaReference, JniHandleOwnership transfer)
       : base(javaReference, transfer)
   {
   }

   [Register(".ctor", "()V", "")]
   public unsafe MgRewardedAdLoadCallback()
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

   // ReSharper disable once UnusedMember.Local
   private static Delegate GetOnAdLoadedHandler()
   {
      if (_onAdLoadedCallback is null)
      {
         _onAdLoadedCallback = JNINativeWrapper.CreateDelegate(NativeOnAdLoaded);
      }

      return _onAdLoadedCallback;
   }

   private static void NativeOnAdLoaded(IntPtr jniEnv, IntPtr nativeThis, IntPtr nativePtr)
   {
      var callback = GetObject<MgRewardedAdLoadCallback>(jniEnv, nativeThis, JniHandleOwnership.DoNotTransfer);
      var rewardedAd = GetObject<Rewarded.RewardedAd>(nativePtr, JniHandleOwnership.DoNotTransfer);

      callback!.OnRewardedAdLoaded(rewardedAd);
   }

   [Register("onAdLoaded", "(Lcom/google/android/gms/ads/rewarded/RewardedAd;)V", "GetOnAdLoadedHandler")]
   public virtual void OnRewardedAdLoaded(Rewarded.RewardedAd rewardedAd)
   {
   }
}