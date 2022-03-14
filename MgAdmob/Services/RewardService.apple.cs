using System;
using Foundation;
using Google.MobileAds;
using UIKit;

namespace Plugin.MgAdmob.Services;

internal class RewardService
{
   private readonly MgAdmobImplementation _implementation;
   private RewardedAd _rewardedAd;

   public RewardService(MgAdmobImplementation implementation)
   {
      _implementation = implementation;

   }

   public bool IsLoaded()
   {
      return _rewardedAd != null;
   }


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
      if (error != null)
      {
         _implementation.OnRewardedVideoAdFailedToLoad(error);

         return;
      }

      _rewardedAd = rewardedAd;

      if (rewardedAd == null)
      {
         return;
      }

      _rewardedAd.DismissedContent += RewardedAdOnDismissedContent;
      _rewardedAd.FailedToPresentContent += RewardedAdOnFailedToPresentContent;
      _rewardedAd.PresentedContent += RewardedAdOnPresentedContent;
      _rewardedAd.RecordedImpression += RewardedAdOnRecordedImpression;

      _implementation.OnRewardedVideoAdLoaded();
   }

   private void RewardedAdOnRecordedImpression(object sender, System.EventArgs e)
   {
      _implementation.OnRewardedVideoAdImpression();
   }


   private void RewardedAdOnPresentedContent(object sender, System.EventArgs e)
   {
      _implementation.OnRewardedVideoAdOpened();
      _implementation.OnRewardedVideoAdCompleted();
   }

   private void RewardedAdOnFailedToPresentContent(object sender, FullScreenPresentingAdWithErrorEventArgs e)
   {
      _implementation.OnRewardedVideoAdFailedToShow(e.Error);

      UnbindRewardEvents();

      _rewardedAd = null;
   }

   private void UnbindRewardEvents()
   {
      _rewardedAd.DismissedContent -= RewardedAdOnDismissedContent;
      _rewardedAd.FailedToPresentContent -= RewardedAdOnFailedToPresentContent;
      _rewardedAd.PresentedContent -= RewardedAdOnPresentedContent;
      _rewardedAd.RecordedImpression -= RewardedAdOnRecordedImpression;
   }

   private void RewardedAdOnDismissedContent(object sender, System.EventArgs e)
   {
      _implementation.OnRewardedVideoAdClosed();

      UnbindRewardEvents();

      _rewardedAd = null;
   }

   public void LoadRewardedVideo(string adUnitId, MgRewardedAdOptions options = null)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      CreateRewardAd(adUnitId);
   }

   public void ShowRewardedVideo()
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (!IsLoaded())
      {
         throw new ApplicationException("RewardAd not loaded, call LoadRewardedVideo()");
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
      _implementation.OnRewarded(_rewardedAd.AdReward);
   }
}

