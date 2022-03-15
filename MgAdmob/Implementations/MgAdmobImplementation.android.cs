using System;
using System.Collections.Generic;
using Android.Gms.Ads;
using Android.Gms.Ads.Rewarded;
using Android.OS;
using Google.Ads.Mediation.Admob;
using Plugin.MgAdmob.Enums;
using Plugin.MgAdmob.EventArgs;
using Plugin.MgAdmob.Interfaces;
using Plugin.MgAdmob.Rewarded;
using Plugin.MgAdmob.Services.Interstitial;
using Plugin.MgAdmob.Services.Rewarded;

namespace Plugin.MgAdmob.Implementations;

public class MgAdmobImplementation : IMgAdmob
{
   public event EventHandler InterstitialLoaded;
   public event EventHandler InterstitialOpened;
   public event EventHandler InterstitialClosed;
   public event EventHandler<MgErrorEventArgs> InterstitialFailedToShow;
   public event EventHandler<MgErrorEventArgs> InterstitialFailedToLoad;
   public event EventHandler InterstitialImpression;

   public event EventHandler<MgRewardEventArgs> Rewarded;
   public event EventHandler RewardedVideoAdClosed;
   public event EventHandler<MgErrorEventArgs> RewardedVideoAdFailedToLoad;
   public event EventHandler<MgErrorEventArgs> RewardedVideoAdFailedToShow;
   public event EventHandler RewardedVideoAdLeftApplication;
   public event EventHandler RewardedVideoAdLoaded;
   public event EventHandler RewardedVideoAdOpened;
   public event EventHandler RewardedVideoStarted;
   public event EventHandler RewardedVideoAdCompleted;
   public event EventHandler RewardedVideoAdImpression;
   
   public bool IsEnabled { get; set; } = true;
   public string AdUnitId { get; set; }
   public bool UsePersonalisedAds { get; set; } = false;
   public bool UseRestrictedDataProcessing { get; set; } = true;
   public bool ComplyWithFamilyPolicies { get; set; } = true;
   public MgTagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; } = MgTagForChildDirectedTreatment.TreatmentUnspecified;
   public MgTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; } = MgTagForUnderAgeOfConsent.ConsentUnspecified;
   public MgMaxAdContentRating MaxAdContentRating { get; set; } = MgMaxAdContentRating.RatingG;
   public List<string> TestDevices { get; set; }
   
   public virtual void OnInterstitialLoaded() => InterstitialLoaded?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnInterstitialOpened() => InterstitialOpened?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnInterstitialClosed() => InterstitialClosed?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnInterstitialFailedToShow(AdError error) => InterstitialFailedToShow?.Invoke(this, new MgErrorEventArgs() { ErrorCode = error.Code, ErrorMessage = error.Message, ErrorDomain = error.Domain });
   public virtual void OnInterstitialFailedToLoad(AdError error) => InterstitialFailedToLoad?.Invoke(this, new MgErrorEventArgs() { ErrorCode = error.Code, ErrorMessage = error.Message, ErrorDomain = error.Domain });
   public virtual void OnInterstitialImpression() => InterstitialImpression?.Invoke(this, System.EventArgs.Empty);
   
   public virtual void OnRewardedVideoAdLoaded() => RewardedVideoAdLoaded?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdOpened() => RewardedVideoAdOpened?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdClosed() => RewardedVideoAdClosed?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdCompleted() => RewardedVideoAdCompleted?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdImpression() => RewardedVideoAdImpression?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdFailedToShow(AdError error) => RewardedVideoAdFailedToShow?.Invoke(this, new MgErrorEventArgs { ErrorCode = error.Code, ErrorMessage = error.Message, ErrorDomain = error.Domain });
   public virtual void OnRewardedVideoAdFailedToLoad(AdError error) => RewardedVideoAdFailedToLoad?.Invoke(this, new MgErrorEventArgs { ErrorCode = error.Code, ErrorMessage = error.Message, ErrorDomain = error.Domain });
   public virtual void OnRewarded(IRewardItem rewardItem) => Rewarded?.Invoke(this, new MgRewardEventArgs { RewardType = rewardItem.Type, RewardAmount = rewardItem.Amount });
   
   private readonly MgInterstitialService _interstitialService;
   private readonly MgRewardService _rewardService;
   
   public MgAdmobImplementation()
   {
      _interstitialService = new MgInterstitialService(this);
      _rewardService = new MgRewardService(this);
   }

   public static AdRequest.Builder GetRequest()
   {
      var addBundle = false;
      var bundleExtra = new Bundle();
      var requestBuilder = new AdRequest.Builder();
      var configuration = new RequestConfiguration.Builder();

      

      if (CrossMgAdmob.Current.TestDevices != null)
      {
         configuration = configuration.SetTestDeviceIds(CrossMgAdmob.Current.TestDevices);
      }

      if (!CrossMgAdmob.Current.UsePersonalisedAds)
      {
         bundleExtra.PutString("npa", "1");
         addBundle = true;
      }

      if (CrossMgAdmob.Current.UseRestrictedDataProcessing)
      {
         bundleExtra.PutString("rdp", "1");
         addBundle = true;
      }

      MobileAds.RequestConfiguration = configuration
         .SetTagForChildDirectedTreatment((int)CrossMgAdmob.Current.TagForChildDirectedTreatment)
         .SetTagForUnderAgeOfConsent((int)CrossMgAdmob.Current.TagForUnderAgeOfConsent)
         .SetMaxAdContentRating(CrossMgAdmob.Current.GetAdContentRatingString())
         .Build();

      if (addBundle)
      {
         requestBuilder = requestBuilder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra);
      }

      return requestBuilder;
   }

   public bool IsInterstitialLoaded => _interstitialService.IsLoaded;

   public void LoadInterstitial(string adUnitId)
   {
      _interstitialService.LoadInterstitial(adUnitId);
   }

   public void ShowInterstitial()
   {
      _interstitialService.ShowInterstitial();
   }

   public bool IsRewardedVideoLoaded => _rewardService.IsLoaded;

   public void LoadRewardedVideo(string adUnitId/*, MgRewardedAdOptions options = null*/)
   {
      _rewardService.LoadRewardVideo(adUnitId);
   }

   public void ShowRewardedVideo()
   {
      _rewardService.ShowReward();
   }

   public string GetAdContentRatingString()
   {
      return MaxAdContentRating switch
      {
         MgMaxAdContentRating.RatingG => "G",
         MgMaxAdContentRating.RatingPg => "PG",
         MgMaxAdContentRating.RatingT => "T",
         MgMaxAdContentRating.RatingMa => "MA",
         _ => string.Empty
      };
   }
}

