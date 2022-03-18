using System;
using Foundation;
using Google.MobileAds;
using Plugin.MgAdmob.Extensions;
using Plugin.MgAdmob.Implementations;
using Plugin.MgAdmob.Interfaces;
using UIKit;

namespace Plugin.MgAdmob.Services.Interstitial;

internal class MgInterstitialService : IMgAdService
{
   private InterstitialAd _interstitialAd;
   private IMgAdmobImplementation _implementation;

   public MgInterstitialService()
   {
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

      if (!IsLoaded)
      {
         throw new ApplicationException($"Interstitial Ad not loaded, call {nameof(Load)}() first");
      }

      var window = UIApplication.SharedApplication.KeyWindow;
      var vc = window.RootViewController;

      while (vc?.PresentedViewController != null)
      {
         vc = vc.PresentedViewController;
      }

      if (_interstitialAd.CanPresent(vc, out _))
      {
         _interstitialAd.Present(vc);
      }
   }
   
   public void Load(string adUnit)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (_interstitialAd != null)
      {
         _interstitialAd.DismissedContent -= AdOnDismissedContent;
         _interstitialAd.DismissingContent -= AdOnDismissingContent;
         _interstitialAd.FailedToPresentContent -= AdOnFailedToPresentContent;
         _interstitialAd.PresentedContent -= AdOnPresentedContent;
         _interstitialAd.RecordedClick -= AdOnRecordedClick;
         _interstitialAd.RecordedImpression -= AdOnRecordedImpression;

         _interstitialAd = null;
      }

      var request = MgAdmobImplementation.GetRequest();

      InterstitialAd.Load(adUnit, request, AdCompletionHandler);
   }


   private void AdCompletionHandler(InterstitialAd interstitialAd, NSError error)
   {
      if (!IsInitialised)
      {
         _interstitialAd = null;

         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      if (error != null)
      {
         _implementation.OnInterstitialFailedToLoad(error.ToMgErrorEventArgs());

         _interstitialAd = null;

         return;
      }

      _interstitialAd = interstitialAd;

      _interstitialAd.DismissedContent += AdOnDismissedContent;
      _interstitialAd.DismissingContent += AdOnDismissingContent;
      _interstitialAd.FailedToPresentContent += AdOnFailedToPresentContent;
      _interstitialAd.PresentedContent += AdOnPresentedContent;
      _interstitialAd.RecordedClick += AdOnRecordedClick;
      _interstitialAd.RecordedImpression += AdOnRecordedImpression;

      _implementation.OnInterstitialLoaded();
   }

   private void AdOnRecordedImpression(object sender, System.EventArgs e)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnInterstitialImpression();
   }

   private void AdOnRecordedClick(object sender, System.EventArgs e)
   {
      Console.WriteLine("----------> InterstitialAdOnRecordedClick");
   }

   private void AdOnPresentedContent(object sender, System.EventArgs e)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnInterstitialOpened();
   }

   private void AdOnFailedToPresentContent(object sender, FullScreenPresentingAdWithErrorEventArgs e)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnInterstitialFailedToShow(e.Error.ToMgErrorEventArgs());
   }

   private void AdOnDismissingContent(object sender, System.EventArgs e)
   {
   }

   private void AdOnDismissedContent(object sender, System.EventArgs e)
   {
      if (!IsInitialised)
      {
         throw new ApplicationException($"Service not initialised, call {nameof(Init)}() first");
      }

      _implementation.OnInterstitialClosed();
   }
   
}

