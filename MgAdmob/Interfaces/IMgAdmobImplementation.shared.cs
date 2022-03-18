using System;
using System.Collections.Generic;
using Plugin.MgAdmob.Enums;
using Plugin.MgAdmob.EventArgs;


namespace Plugin.MgAdmob.Interfaces;

public interface IMgAdmobImplementation
{
   event EventHandler InterstitialLoaded;
   event EventHandler InterstitialOpened;
   event EventHandler InterstitialClosed;
   event EventHandler InterstitialImpression;
   event EventHandler<MgErrorEventArgs> InterstitialFailedToShow;
   event EventHandler<MgErrorEventArgs> InterstitialFailedToLoad;
   event EventHandler<MgRewardEventArgs> Rewarded;
   event EventHandler RewardedVideoAdClosed;
   event EventHandler<MgErrorEventArgs> RewardedVideoAdFailedToLoad;
   event EventHandler<MgErrorEventArgs> RewardedVideoAdFailedToShow;
   event EventHandler RewardedVideoAdLeftApplication;
   event EventHandler RewardedVideoAdLoaded;
   event EventHandler RewardedVideoAdOpened;
   event EventHandler RewardedVideoStarted;
   event EventHandler RewardedVideoAdCompleted;
   event EventHandler RewardedVideoAdImpression;
   
   bool IsEnabled { get; set; }
   string AdUnitId { get; set; }
   bool UsePersonalisedAds { get; set; }
   bool UseRestrictedDataProcessing { get; set; }
   bool ComplyWithFamilyPolicies { get; set; }
   MgTagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; }
   MgTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; }
   MgMaxAdContentRating MaxAdContentRating { get; set; }
   List<string> TestDevices { get; set; }
   
   void OnInterstitialLoaded();
   void OnInterstitialOpened();
   void OnInterstitialClosed();
   void OnInterstitialImpression();
   void OnInterstitialFailedToShow(MgErrorEventArgs error);
   void OnInterstitialFailedToLoad(MgErrorEventArgs error);
   void OnRewardedVideoAdLoaded();
   void OnRewardedVideoAdOpened();
   void OnRewarded(MgRewardEventArgs adReward);
   void OnRewardedVideoAdClosed();
   void OnRewardedVideoAdCompleted();
   void OnRewardedVideoAdImpression();
   void OnRewardedVideoAdFailedToLoad(MgErrorEventArgs error);
   void OnRewardedVideoAdFailedToShow(MgErrorEventArgs error);


   bool RegisterAdService(MgAdServiceType type, IMgAdService service);
   IMgAdService GetAdService(MgAdServiceType type);

   string GetAdContentRatingString();
}