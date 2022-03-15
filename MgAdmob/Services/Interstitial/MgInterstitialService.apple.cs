using System;
using Foundation;
using Google.MobileAds;
using Plugin.MgAdmob.Implementations;
using UIKit;

namespace Plugin.MgAdmob.Services.Interstitial;

internal class MgInterstitialService
{
   private InterstitialAd _interstitialAd;
   private readonly MgAdmobImplementation _implementation;

   public MgInterstitialService(MgAdmobImplementation implementation)
   {
      _implementation = implementation;
   }

   public void LoadInterstitial(string adUnit)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (_interstitialAd != null)
      {
         _interstitialAd.DismissedContent -= InterstitialAdOnDismissedContent;
         _interstitialAd.DismissingContent -= InterstitialAdOnDismissingContent;
         _interstitialAd.FailedToPresentContent -= InterstitialAdOnFailedToPresentContent;
         _interstitialAd.PresentedContent -= InterstitialAdOnPresentedContent;
         _interstitialAd.RecordedClick -= InterstitialAdOnRecordedClick;
         _interstitialAd.RecordedImpression -= InterstitialAdOnRecordedImpression;

         _interstitialAd = null;
      }

      var request = MgAdmobImplementation.GetRequest();

      InterstitialAd.Load(adUnit, request, InterstitialAdCompletionHandler);
   }

   private void InterstitialAdCompletionHandler(InterstitialAd interstitialAd, NSError error)
   {
      if (error != null)
      {
         _implementation.OnInterstitialFailedToLoad(error);

         _interstitialAd = null;

         return;
      }

      _interstitialAd = interstitialAd;

      _interstitialAd.DismissedContent += InterstitialAdOnDismissedContent;
      _interstitialAd.DismissingContent += InterstitialAdOnDismissingContent;
      _interstitialAd.FailedToPresentContent += InterstitialAdOnFailedToPresentContent;
      _interstitialAd.PresentedContent += InterstitialAdOnPresentedContent;
      _interstitialAd.RecordedClick += InterstitialAdOnRecordedClick;
      _interstitialAd.RecordedImpression += InterstitialAdOnRecordedImpression;

      _implementation.OnInterstitialLoaded();
   }

   private void InterstitialAdOnRecordedImpression(object sender, System.EventArgs e)
   {
      _implementation.OnInterstitialImpression();
   }

   private void InterstitialAdOnRecordedClick(object sender, System.EventArgs e)
   {
      Console.WriteLine("----------> InterstitialAdOnRecordedClick");
   }

   private void InterstitialAdOnPresentedContent(object sender, System.EventArgs e)
   {
      _implementation.OnInterstitialOpened();
   }

   private void InterstitialAdOnFailedToPresentContent(object sender, FullScreenPresentingAdWithErrorEventArgs e)
   {
      _implementation.OnInterstitialFailedToShow(e.Error);
   }

   private void InterstitialAdOnDismissingContent(object sender, System.EventArgs e)
   {
   }

   private void InterstitialAdOnDismissedContent(object sender, System.EventArgs e)
   {
      _implementation.OnInterstitialClosed();
   }

   public void ShowInterstitial()
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (!IsLoaded)
      {
         throw new ApplicationException("Interstitial Ad not loaded, call LoadInterstitial() first");
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

   public bool IsLoaded => _interstitialAd != null;
}

