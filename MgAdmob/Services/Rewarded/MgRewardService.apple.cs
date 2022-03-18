using System;
using Foundation;
using Google.MobileAds;
using Plugin.MgAdmob.Extensions;
using Plugin.MgAdmob.Implementations;
using Plugin.MgAdmob.Interfaces;
using UIKit;

namespace Plugin.MgAdmob.Services.Rewarded;

internal class MgRewardService : IMgAdService
{
   private IMgAdmobImplementation _implementation;
   private RewardedAd _rewardedAd;

   public MgRewardService()
   {
   }

   public void Init(IMgAdmobImplementation implementation)
   {
      _implementation = implementation;
   }

   public bool IsInitialised => _implementation != null;
   public bool IsLoaded => _rewardedAd != null;


   private void CreateRewardAd(string adUnitId)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }
      
      var request = MgAdmobImplementation.GetRequest();
      
      RewardedAd.Load(adUnitId, request, CompletionHandler);
   }

   private void CompletionHandler(RewardedAd rewardedAd, NSError error)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      if (error != null)
      {
         _implementation.OnRewardedVideoAdFailedToLoad(error.ToMgErrorEventArgs());

         _rewardedAd = null;

         return;
      }

      _rewardedAd = rewardedAd;

      if (!IsLoaded)
      {
         return;
      }

      _rewardedAd.DismissedContent += RewardedAdOnDismissedContent;
      _rewardedAd.FailedToPresentContent += RewardedAdOnFailedToPresentContent;
      _rewardedAd.PresentedContent += RewardedAdOnPresentedContent;
      _rewardedAd.RecordedImpression += RewardedAdOnRecordedImpression;

      _implementation.OnRewardedVideoAdLoaded();
   }
   
   private void UnbindRewardEvents()
   {
      _rewardedAd.DismissedContent -= RewardedAdOnDismissedContent;
      _rewardedAd.FailedToPresentContent -= RewardedAdOnFailedToPresentContent;
      _rewardedAd.PresentedContent -= RewardedAdOnPresentedContent;
      _rewardedAd.RecordedImpression -= RewardedAdOnRecordedImpression;
   }

   private void RewardedAdOnRecordedImpression(object sender, System.EventArgs e)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnRewardedVideoAdImpression();
   }


   private void RewardedAdOnPresentedContent(object sender, System.EventArgs e)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnRewardedVideoAdOpened();
      _implementation.OnRewardedVideoAdCompleted();
   }

   private void RewardedAdOnFailedToPresentContent(object sender, FullScreenPresentingAdWithErrorEventArgs e)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnRewardedVideoAdFailedToShow(e.Error.ToMgErrorEventArgs());

      UnbindRewardEvents();

      _rewardedAd = null;
   }
   
   private void RewardedAdOnDismissedContent(object sender, System.EventArgs e)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnRewardedVideoAdClosed();

      UnbindRewardEvents();

      _rewardedAd = null;
   }

   public void Load(string adUnitId/*, MgRewardedAdOptions options = null*/)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      CreateRewardAd(adUnitId);
   }

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

      var window = UIApplication.SharedApplication.KeyWindow;

      var vc = window.RootViewController;

      while (vc?.PresentedViewController != null)
      {
         vc = vc.PresentedViewController;
      }
         
      _rewardedAd.Present(vc, UserDidEarnRewardHandler);

   }

   private void UserDidEarnRewardHandler()
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnRewarded(_rewardedAd.AdReward.ToMgRewardEventArgs());
   }
}

