using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using Android.Gms.Ads.Interstitial;
using Plugin.MgAdmob.Implementations;
using Xamarin.Forms.Platform.Android;

namespace Plugin.MgAdmob.Services.Interstitial;

public class MgInterstitialService : MgInterstitialAdLoadCallback
{
   private InterstitialAd _interstitialAd;

   private readonly MgAdmobImplementation _implementation;

   public MgInterstitialService(MgAdmobImplementation implementation)
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

   public bool IsLoaded => _interstitialAd != null;



   public void ShowInterstitial()
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (!IsLoaded)
      {
         throw new ApplicationException($"Interstitial Ad not loaded, call {nameof(LoadInterstitial)}() first");
      }

      _interstitialAd.Show(Android.App.Application.Context.GetActivity());

      _interstitialAd = null;
   }

   public override void OnInterstitialAdLoaded(InterstitialAd interstitialAd)
   {
      base.OnInterstitialAdLoaded(interstitialAd);

      _interstitialAd = interstitialAd;

      _interstitialAd.FullScreenContentCallback = new MgInterstitialFullScreenContentCallback(_implementation);

      _implementation.OnInterstitialLoaded();
   }

   public override void OnAdFailedToLoad(LoadAdError error)
   {
      base.OnAdFailedToLoad(error);

      _implementation.OnInterstitialFailedToLoad(error);

      _interstitialAd = null;
   }
}

