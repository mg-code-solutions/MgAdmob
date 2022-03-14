using System;
using System.Collections.Generic;
using Plugin.MgAdmob.Enums;
using Plugin.MgAdmob.EventArgs;

namespace Plugin.MgAdmob.Interfaces;

public interface IMgAdmob
{
   bool IsEnabled { get; set; }
   string AdUnitId { get; set; }
   bool UsePersonalizedAds { get; set; }
   bool UseRestrictedDataProcessing { get; set; }
   bool ComplyWithFamilyPolicies { get; set; }
   MgTagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; }
   MgTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; }
   MgMaxAdContentRating MaxAdContentRating { get; set; }
   List<string> TestDevices { get; set; }

   bool IsInterstitialLoaded();
   void LoadInterstitial(string adUnitId);
   void ShowInterstitial();
   bool IsRewardedVideoLoaded();
   public void LoadRewardedVideo(string adUnitId, MgRewardedAdOptions options = null);
   void ShowRewardedVideo();
   string GetAdContentRatingString();
   
   event EventHandler InterstitialLoaded;
   event EventHandler InterstitialOpened;
   event EventHandler InterstitialClosed;
   event EventHandler InterstitialImpression;
   event EventHandler<MgAdmobEventArgs> InterstitialFailedToShow;
   event EventHandler<MgAdmobEventArgs> InterstitialFailedToLoad;

   event EventHandler<MgAdmobEventArgs> Rewarded;
   event EventHandler RewardedVideoAdClosed;
   event EventHandler<MgAdmobEventArgs> RewardedVideoAdFailedToLoad;
   event EventHandler<MgAdmobEventArgs> RewardedVideoAdFailedToShow;
   event EventHandler RewardedVideoAdLeftApplication;
   event EventHandler RewardedVideoAdLoaded;
   event EventHandler RewardedVideoAdOpened;
   event EventHandler RewardedVideoStarted;
   event EventHandler RewardedVideoAdCompleted;
   event EventHandler RewardedVideoAdImpression;
}