using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using Android.Gms.Ads.Interstitial;
using Xamarin.Forms.Platform.Android;

namespace Plugin.MgAdmob.Services;

public class InterstitialService : MgInterstitialAdLoadCallback
{
   private InterstitialAd _interstitialAd;

   private readonly MgAdmobImplementation _implementation;

   public InterstitialService(MgAdmobImplementation implementation)
   {
      _implementation = implementation;
   }

   private void CreateInterstitialAd(string adUnit)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      var context = Android.App.Application.Context;

      var requestBuilder = MgAdmobImplementation.GetRequest();

      MgInterstitialAd.Load(context, adUnit, requestBuilder.Build(), this);
   }

   public void LoadInterstitial(string adUnit)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      CreateInterstitialAd(adUnit);
   }

   public bool IsLoaded()
   {
      return _interstitialAd != null;
   }

   public void ShowInterstitial()
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (_interstitialAd != null)
      {
         _interstitialAd.Show(Android.App.Application.Context.GetActivity());

         _interstitialAd = null;
      }
      else
      {
         throw new ApplicationException("Interstitial Ad not loaded, call LoadInterstitial() first");
      }
   }

   public override void OnInterstitialAdLoaded(InterstitialAd interstitialAd)
   {
      base.OnInterstitialAdLoaded(interstitialAd);

      _interstitialAd = interstitialAd;

      _interstitialAd.FullScreenContentCallback = new MgFullScreenContentCallback(_implementation, true);
      
      _implementation.OnInterstitialLoaded();
   }
   
   public override void OnAdFailedToLoad(LoadAdError error)
   {
      base.OnAdFailedToLoad(error);

      _implementation.OnInterstitialFailedToLoad(error);

      _interstitialAd = null;
   }
}

