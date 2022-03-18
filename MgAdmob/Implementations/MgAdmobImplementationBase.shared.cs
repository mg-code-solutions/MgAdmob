using System;
using System.Collections.Generic;
using Plugin.MgAdmob.Enums;
using Plugin.MgAdmob.EventArgs;
using Plugin.MgAdmob.Interfaces;

namespace Plugin.MgAdmob.Implementations
{
   public abstract class MgAdmobImplementationBase : IMgAdmobImplementation
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
      public virtual void OnInterstitialFailedToShow(MgErrorEventArgs error) => InterstitialFailedToShow?.Invoke(this, error);
      public virtual void OnInterstitialFailedToLoad(MgErrorEventArgs error) => InterstitialFailedToLoad?.Invoke(this, error);


      public virtual void OnRewardedVideoAdLoaded() => RewardedVideoAdLoaded?.Invoke(this, System.EventArgs.Empty);
      public virtual void OnRewarded(MgRewardEventArgs reward) => Rewarded?.Invoke(this, reward);
      public virtual void OnRewardedVideoAdClosed() => RewardedVideoAdClosed?.Invoke(this, System.EventArgs.Empty);
      public virtual void OnRewardedVideoAdCompleted() => RewardedVideoAdCompleted?.Invoke(this, System.EventArgs.Empty);
      public virtual void OnRewardedVideoAdImpression() => RewardedVideoAdImpression?.Invoke(this, System.EventArgs.Empty);
      public virtual void OnRewardedVideoAdFailedToLoad(MgErrorEventArgs error) => RewardedVideoAdFailedToLoad?.Invoke(this, error);
      public virtual void OnRewardedVideoAdFailedToShow(MgErrorEventArgs error) => RewardedVideoAdFailedToShow?.Invoke(this, error);
      public virtual void OnRewardedVideoAdOpened() => RewardedVideoAdOpened?.Invoke(this, System.EventArgs.Empty);

      //public virtual void OnRewardedVideoStarted() => RewardedVideoStarted?.Invoke(this, System.EventArgs.Empty);
      //public virtual void OnRewardedVideoAdLeftApplication() => RewardedVideoAdLeftApplication?.Invoke(this, System.EventArgs.Empty);

      public bool IsEnabled { get; set; } = true;
      public string AdUnitId { get; set; }
      public bool UsePersonalisedAds { get; set; } = false;
      public bool UseRestrictedDataProcessing { get; set; } = true;
      public bool ComplyWithFamilyPolicies { get; set; } = true;
      public MgTagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; } = MgTagForChildDirectedTreatment.TreatmentUnspecified;
      public MgTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; } = MgTagForUnderAgeOfConsent.ConsentUnspecified;
      public MgMaxAdContentRating MaxAdContentRating { get; set; } = MgMaxAdContentRating.RatingG;
      public List<string> TestDevices { get; set; }
      

      private readonly Dictionary<MgAdServiceType, IMgAdService> _adServices = new();

      public bool RegisterAdService(MgAdServiceType type, IMgAdService service)
      {
         if (_adServices.ContainsKey(type))
         {
            if (service == null)
            {
               _adServices.Remove(type);
            }
            else
            {
               _adServices[type] = service;
               
               service.Init(this);

               return true;
            }
         }

         if (service == null)
         {
            return false;
         }

         _adServices.Add(type, service);

         service.Init(this);

         return _adServices.ContainsKey(type);
      }

      public IMgAdService GetAdService(MgAdServiceType type)
      {
         return _adServices.ContainsKey(type) 
            ? _adServices[type] 
            : null;
      }

      public virtual string GetAdContentRatingString()
      {
         throw new NotImplementedException();
      }

   }
}
