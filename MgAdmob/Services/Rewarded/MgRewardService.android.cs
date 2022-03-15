using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using Android.Gms.Ads.Rewarded;
using Plugin.MgAdmob.Implementations;
using Xamarin.Forms.Platform.Android;

namespace Plugin.MgAdmob.Services.Rewarded;

public class MgRewardService : MgRewardedAdLoadCallback, IOnUserEarnedRewardListener
{
   private RewardedAd _rewardedAd;
   private readonly MgAdmobImplementation _implementation;

   public MgRewardService(MgAdmobImplementation implementation)
   {
      _implementation = implementation;
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

   public void LoadRewardVideo(string adUnitId)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      CreateRewardAd(adUnitId);
   }

   public bool IsLoaded => _rewardedAd != null;

   public void ShowReward()
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (!IsLoaded)
      {
         throw new ApplicationException($"Reward Ad not loaded, call {nameof(LoadRewardVideo)}() first");
      }

      _rewardedAd.Show(Android.App.Application.Context.GetActivity(), this);

      _rewardedAd = null;
   }

   public override void OnAdFailedToLoad(LoadAdError error)
   {
      base.OnAdFailedToLoad(error);

      _implementation.OnRewardedVideoAdFailedToLoad(error);

      _rewardedAd = null;
   }

   public override void OnRewardedAdLoaded(RewardedAd rewardedAd)
   {
      base.OnRewardedAdLoaded(rewardedAd);

      _rewardedAd = rewardedAd;

      if (!IsLoaded)
      {
         return;
      }

      _rewardedAd.FullScreenContentCallback = new MgRewardedFullScreenContentCallback(_implementation);

      _implementation.OnRewardedVideoAdLoaded();
   }

   public void OnUserEarnedReward(IRewardItem rewardItem)
   {
      _implementation.OnRewarded(rewardItem);
   }
}

