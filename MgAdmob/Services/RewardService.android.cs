using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using Android.Gms.Ads.Rewarded;
using Xamarin.Forms.Platform.Android;

namespace Plugin.MgAdmob.Services;

   public class RewardService : MgRewardedAdLoadCallback, IOnUserEarnedRewardListener
   {
      private RewardedAd _rewardedAd;
      private readonly MgAdmobImplementation _implementation;

      public RewardService(MgAdmobImplementation implementation)
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

      public void LoadReward(string adUnitId)
      {
         if (!CrossMgAdmob.Current.IsEnabled)
         {
            return;
         }

         CreateRewardAd(adUnitId);
      }

      public bool IsLoaded()
      {
         return _rewardedAd != null;
      }

      public void ShowReward()
      {
         if (!CrossMgAdmob.Current.IsEnabled)
         {
            return;
         }

         if (_rewardedAd != null)
         {
            _rewardedAd.Show(Android.App.Application.Context.GetActivity(), this);

            _rewardedAd = null;
         }
         else
         {
            throw new ApplicationException("Reward Ad not loaded, call LoadReward() first");
         }
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

         _rewardedAd.FullScreenContentCallback = new MgFullScreenContentCallback(_implementation, false);

         _implementation.OnRewardedVideoAdLoaded();
      }

      public void OnUserEarnedReward(IRewardItem rewardItem)
      {
         _implementation.OnRewarded(rewardItem);
      }
   }

