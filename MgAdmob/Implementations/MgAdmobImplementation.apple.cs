using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Google.MobileAds;
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
   public event EventHandler InterstitialImpression;
   public event EventHandler<MgErrorEventArgs> InterstitialFailedToShow;
   public event EventHandler<MgErrorEventArgs> InterstitialFailedToLoad;


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


   public virtual void OnInterstitialLoaded() => InterstitialLoaded?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnInterstitialOpened() => InterstitialOpened?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnInterstitialClosed() => InterstitialClosed?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnInterstitialImpression() => InterstitialImpression?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnInterstitialFailedToShow(NSError error) => InterstitialFailedToShow?.Invoke(this, new MgErrorEventArgs { ErrorCode = (int)error.Code, ErrorDomain = error.Domain, ErrorMessage = error.LocalizedDescription });
   public virtual void OnInterstitialFailedToLoad(NSError error) => InterstitialFailedToLoad?.Invoke(this, new MgErrorEventArgs { ErrorCode = (int)error.Code, ErrorDomain = error.Domain, ErrorMessage = error.LocalizedDescription });


   public virtual void OnRewardedVideoAdLoaded() => RewardedVideoAdLoaded?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewarded(AdReward adReward) => Rewarded?.Invoke(this, new MgRewardEventArgs { RewardAmount = adReward.Amount.DoubleValue, RewardType = adReward.Type });
   public virtual void OnRewardedVideoAdClosed() => RewardedVideoAdClosed?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdCompleted() => RewardedVideoAdCompleted?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdImpression() => RewardedVideoAdImpression?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdFailedToLoad(NSError error) => RewardedVideoAdFailedToLoad?.Invoke(this, new MgErrorEventArgs { ErrorCode = (int)error.Code, ErrorDomain = error.Domain, ErrorMessage = error.LocalizedDescription });
   public virtual void OnRewardedVideoAdFailedToShow(NSError error) => RewardedVideoAdFailedToShow?.Invoke(this, new MgErrorEventArgs { ErrorCode = (int)error.Code, ErrorDomain = error.Domain, ErrorMessage = error.LocalizedDescription });
   public virtual void OnRewardedVideoAdOpened() => RewardedVideoAdOpened?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoStarted() => RewardedVideoStarted?.Invoke(this, System.EventArgs.Empty);
   public virtual void OnRewardedVideoAdLeftApplication() => RewardedVideoAdLeftApplication?.Invoke(this, System.EventArgs.Empty);

   public bool IsEnabled { get; set; } = true;
   public string AdUnitId { get; set; }
   public bool UsePersonalisedAds { get; set; } = false;
   public bool UseRestrictedDataProcessing { get; set; } = true;
   public bool ComplyWithFamilyPolicies { get; set; } = true;
   public MgTagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; } = MgTagForChildDirectedTreatment.TreatmentUnspecified;
   public MgTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; } = MgTagForUnderAgeOfConsent.ConsentUnspecified;
   public MgMaxAdContentRating MaxAdContentRating { get; set; } = MgMaxAdContentRating.RatingG;
   public List<string> TestDevices { get; set; }


   private readonly MgInterstitialService _interstitialService;
   private readonly MgRewardService _rewardService;
   
   public MgAdmobImplementation()
   {
      _interstitialService = new MgInterstitialService(this);
      _rewardService = new MgRewardService(this);
   }

   public static Request GetRequest()
   {
      var request = Request.GetDefaultRequest();


      var addExtra = false;
      var dict = new Dictionary<string, string>();

      MobileAds.SharedInstance.RequestConfiguration.TagForChildDirectedTreatment(CrossMgAdmob.Current.TagForChildDirectedTreatment == MgTagForChildDirectedTreatment.TreatmentTrue);
      MobileAds.SharedInstance.RequestConfiguration.TagForUnderAgeOfConsent(CrossMgAdmob.Current.TagForUnderAgeOfConsent == MgTagForUnderAgeOfConsent.ConsentTrue);
      MobileAds.SharedInstance.RequestConfiguration.MaxAdContentRating = CrossMgAdmob.Current.GetAdContentRatingString();

      if (CrossMgAdmob.Current.TestDevices != null)
      {
         MobileAds.SharedInstance.RequestConfiguration.TestDeviceIdentifiers = CrossMgAdmob.Current.TestDevices.ToArray();
      }

      if (!CrossMgAdmob.Current.UsePersonalisedAds)
      {
         dict.Add(new NSString("npa"), new NSString("1"));

         addExtra = true;
      }

      if (CrossMgAdmob.Current.UseRestrictedDataProcessing)
      {
         dict.Add(new NSString("rdp"), new NSString("1"));

         addExtra = true;
      }

      if (CrossMgAdmob.Current.ComplyWithFamilyPolicies)
      {
         //request.Tag(CrossMTAdmob.Current.ComplyWithFamilyPolicies);
         dict.Add(new NSString("max_ad_content_rating"), new NSString("G"));
         addExtra = true;
      }

      if (!addExtra)
      {
         return request;
      }

      var extras = new Extras
      {
         AdditionalParameters = NSDictionary.FromObjectsAndKeys(dict.Values.ToArray(), dict.Keys.ToArray())
      };

      request.RegisterAdNetworkExtras(extras);

      return request;
   }

   public bool IsInterstitialLoaded()
   {
      return _interstitialService.IsLoaded();
   }

   public void LoadInterstitial(string adUnitId)
   {
      _interstitialService.LoadInterstitial(adUnitId);
   }

   public void ShowInterstitial()
   {
      _interstitialService.ShowInterstitial();
   }

   public bool IsRewardedVideoLoaded()
   {
      return _rewardService.IsLoaded();
   }

   public void LoadRewardedVideo(string adUnitId/*, MgRewardedAdOptions options = null*/)
   {
      _rewardService.LoadRewardedVideo(adUnitId/*, options*/);
   }

   public void ShowRewardedVideo()
   {
      _rewardService.ShowRewardedVideo();
   }

   public string GetAdContentRatingString()
   {
      return MaxAdContentRating switch
      {
         MgMaxAdContentRating.RatingG => "GADMaxAdContentRatingGeneral",
         MgMaxAdContentRating.RatingPg => "GADMaxAdContentRatingParentalGuidance",
         MgMaxAdContentRating.RatingT => "GADMaxAdContentRatingTeen",
         MgMaxAdContentRating.RatingMa => "GADMaxAdContentRatingMatureAudience",
         _ => "GADMaxAdContentRatingGeneral"
      };
   }

   
}
