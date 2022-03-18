using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using Android.Gms.Ads.Interstitial;
using Plugin.MgAdmob.Extensions;
using Plugin.MgAdmob.Implementations;
using Plugin.MgAdmob.Interfaces;
using Xamarin.Forms.Platform.Android;

namespace Plugin.MgAdmob.Services.Interstitial;

public class MgInterstitialService : MgInterstitialAdLoadCallback, IMgAdService
{
   private InterstitialAd _interstitialAd;

   private IMgAdmobImplementation _implementation;

   public MgInterstitialService()
   {
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

   public void Load(string adUnit)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      CreateInterstitialAd(adUnit);
   }

   public void Init(IMgAdmobImplementation implementation)
   {
      _implementation = implementation;
   }

   public bool IsInitialised => _implementation != null;

   public bool IsLoaded => _interstitialAd != null;



   public void Show()
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      if (!IsLoaded)
      {
         throw new ApplicationException($"Interstitial Ad not loaded, call {nameof(Load)}() first");
      }
      
      _interstitialAd.Show(Android.App.Application.Context.GetActivity());

      _interstitialAd = null;
   }

   public override void OnInterstitialAdLoaded(InterstitialAd interstitialAd)
   {
      base.OnInterstitialAdLoaded(interstitialAd);

      if (!IsInitialised)
      {
         _implementation = null;

         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _interstitialAd = interstitialAd;

      _interstitialAd.FullScreenContentCallback = new MgInterstitialFullScreenContentCallback(_implementation);

      _implementation.OnInterstitialLoaded();
   }

   
   public override void OnAdFailedToLoad(LoadAdError error)
   {
      base.OnAdFailedToLoad(error);

      _interstitialAd = null;

      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnInterstitialFailedToLoad(error.ToMgErrorEventArgs());
   }
}

