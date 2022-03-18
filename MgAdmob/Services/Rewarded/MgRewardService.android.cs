using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using Android.Gms.Ads.Rewarded;
using Plugin.MgAdmob.Extensions;
using Plugin.MgAdmob.Implementations;
using Plugin.MgAdmob.Interfaces;
using Xamarin.Forms.Platform.Android;

namespace Plugin.MgAdmob.Services.Rewarded;

public class MgRewardService : MgRewardedAdLoadCallback, IOnUserEarnedRewardListener, IMgAdService
{
   private RewardedAd _rewardedAd;
   private IMgAdmobImplementation _implementation;

   public MgRewardService()
   {
   }

   private void CreateRewardAd(string adUnitId)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      var context = Android.App.Application.Context;
      var requestBuilder = MgAdmobImplementation.GetRequest();

      MgRewardedAd.Load(context, adUnitId, requestBuilder.Build(), this);
   }

   public void Load(string adUnitId)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      CreateRewardAd(adUnitId);
   }

   public void Init(IMgAdmobImplementation implementation)
   {
      _implementation = implementation;
   }

   public bool IsInitialised => _implementation != null;

   public bool IsLoaded => _rewardedAd != null;

   public void Show()
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (!IsLoaded)
      {
         throw new ApplicationException($"Reward Ad not loaded, call {nameof(Load)}() first");
      }

      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _rewardedAd.Show(Android.App.Application.Context.GetActivity(), this);

      _rewardedAd = null;
   }

   public override void OnAdFailedToLoad(LoadAdError error)
   {
      base.OnAdFailedToLoad(error);
      
      _rewardedAd = null;

      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation?.OnRewardedVideoAdFailedToLoad(error.ToMgErrorEventArgs());
   }

   public override void OnRewardedAdLoaded(RewardedAd rewardedAd)
   {
      base.OnRewardedAdLoaded(rewardedAd);

      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _rewardedAd = rewardedAd;

      if (!IsLoaded)
      {
         return;
      }

      _rewardedAd.FullScreenContentCallback = new MgRewardedFullScreenContentCallback(_implementation);

      _implementation?.OnRewardedVideoAdLoaded();
   }

   public void OnUserEarnedReward(IRewardItem rewardItem)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnRewarded(rewardItem.ToMgRewardEventArgs());
   }
}

