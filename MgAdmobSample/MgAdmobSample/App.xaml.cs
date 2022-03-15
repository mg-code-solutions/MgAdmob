using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.MgAdmob;
using Plugin.MgAdmob.Enums;
using Plugin.MgAdmob.EventArgs;
using Xamarin.Forms;

namespace MgAdmobSample
{
   public partial class App : Application
   {
      public App()
      {
         InitializeComponent();

         MainPage = new MainPage();

         InitMgAdmob();
      }

      private static void InitMgAdmob()
      {
         CrossMgAdmob.Current.TagForChildDirectedTreatment = MgTagForChildDirectedTreatment.TreatmentUnspecified;
         CrossMgAdmob.Current.TagForUnderAgeOfConsent = MgTagForUnderAgeOfConsent.ConsentUnspecified;
         CrossMgAdmob.Current.MaxAdContentRating = MgMaxAdContentRating.RatingG;
         CrossMgAdmob.Current.UsePersonalisedAds = true;
         CrossMgAdmob.Current.ComplyWithFamilyPolicies = true;
         CrossMgAdmob.Current.UseRestrictedDataProcessing = true;


         if (Device.RuntimePlatform == Device.Android)
         {
            CrossMgAdmob.Current.TestDevices = new List<string> { "B819BB7017336F55227B783DA10DCA00" };
         }

         if (Device.RuntimePlatform == Device.iOS)
         {
            CrossMgAdmob.Current.TestDevices = new List<string> { "7751cbfe52a595ea6b3bf5733dee4d4d" };
         }
      }

      private void BindAdMobEvents()
      {
         CrossMgAdmob.Current.InterstitialLoaded += OnInterstitialLoaded;
         CrossMgAdmob.Current.InterstitialClosed += OnInterstitialClosed;
         CrossMgAdmob.Current.InterstitialOpened += OnInterstitialOpened;
         CrossMgAdmob.Current.InterstitialImpression += OnInterstitialImpression;
         CrossMgAdmob.Current.InterstitialFailedToLoad += OnInterstitialFailedToLoad;
         CrossMgAdmob.Current.InterstitialFailedToShow += OnInterstitialFailedToShow;


         CrossMgAdmob.Current.RewardedVideoAdLoaded += OnRewardedVideoAdLoaded;
         CrossMgAdmob.Current.RewardedVideoAdClosed += OnRewardedVideoAdClosed;
         CrossMgAdmob.Current.RewardedVideoAdFailedToLoad += OnRewardedVideoAdFailedToLoad;
         CrossMgAdmob.Current.RewardedVideoAdFailedToShow += OnRewardedVideoAdFailedToShow;
         CrossMgAdmob.Current.RewardedVideoAdCompleted += OnRewardedVideoAdCompleted;
         CrossMgAdmob.Current.RewardedVideoAdLeftApplication += OnRewardedVideoAdLeftApplication;
         CrossMgAdmob.Current.RewardedVideoAdOpened += OnRewardedVideoAdOpened;
         CrossMgAdmob.Current.RewardedVideoStarted += OnRewardedVideoStarted;
         CrossMgAdmob.Current.RewardedVideoAdImpression += OnRewardedVideoAdImpression;

         CrossMgAdmob.Current.Rewarded += OnRewarded;
      }

      private void UnbindAdMobEvents()
      {
         CrossMgAdmob.Current.InterstitialLoaded -= OnInterstitialLoaded;
         CrossMgAdmob.Current.InterstitialClosed -= OnInterstitialClosed;
         CrossMgAdmob.Current.InterstitialOpened -= OnInterstitialOpened;
         CrossMgAdmob.Current.InterstitialImpression -= OnInterstitialImpression;
         CrossMgAdmob.Current.InterstitialFailedToLoad -= OnInterstitialFailedToLoad;
         CrossMgAdmob.Current.InterstitialFailedToShow -= OnInterstitialFailedToShow;

         CrossMgAdmob.Current.RewardedVideoAdLoaded -= OnRewardedVideoAdLoaded;
         CrossMgAdmob.Current.RewardedVideoAdClosed -= OnRewardedVideoAdClosed;
         CrossMgAdmob.Current.RewardedVideoAdFailedToLoad -= OnRewardedVideoAdFailedToLoad;
         CrossMgAdmob.Current.RewardedVideoAdFailedToShow -= OnRewardedVideoAdFailedToShow;
         CrossMgAdmob.Current.RewardedVideoAdCompleted -= OnRewardedVideoAdCompleted;
         CrossMgAdmob.Current.RewardedVideoAdLeftApplication -= OnRewardedVideoAdLeftApplication;
         CrossMgAdmob.Current.RewardedVideoAdOpened -= OnRewardedVideoAdOpened;
         CrossMgAdmob.Current.RewardedVideoStarted -= OnRewardedVideoStarted;
         CrossMgAdmob.Current.RewardedVideoAdImpression -= OnRewardedVideoAdImpression;

         CrossMgAdmob.Current.Rewarded -= OnRewarded;
      }

      private void OnRewardedVideoAdImpression(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnRewardedVideoAdImpression");
      }

      private void OnRewarded(object sender, MgRewardEventArgs e)
      {
         Debug.WriteLine($"--------> OnRewarded: RewardType = {e.RewardType}, RewardAmount = {e.RewardAmount}");
      }

      private void OnRewardedVideoStarted(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnRewardedVideoStarted");
      }

      private void OnRewardedVideoAdOpened(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnRewardedVideoAdOpened");
      }

      private void OnRewardedVideoAdLeftApplication(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnRewardedVideoAdLeftApplication");
      }

      private void OnRewardedVideoAdCompleted(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnRewardedVideoAdCompleted");
      }

      private void OnRewardedVideoAdFailedToLoad(object sender, MgErrorEventArgs e)
      {
         Debug.WriteLine($"--------> OnRewardedVideoAdFailedToLoad: Code = {e.ErrorCode}: {e.ErrorMessage} ({e.ErrorDomain})");
      }


      private void OnRewardedVideoAdFailedToShow(object sender, MgErrorEventArgs e)
      {
         Debug.WriteLine($"--------> OnRewardedVideoAdFailedToShow: Code = {e.ErrorCode}: {e.ErrorMessage} ({e.ErrorDomain})");
      }

      private void OnRewardedVideoAdClosed(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnRewardedVideoAdClosed");
      }

      private void OnRewardedVideoAdLoaded(object sender, EventArgs e)
      {
         CrossMgAdmob.Current.ShowRewardedVideo();
      }
      

      private void OnInterstitialFailedToShow(object sender, MgErrorEventArgs e)
      {
         Debug.WriteLine($"--------> OnInterstitialFailedToShow: Code = {e.ErrorCode}: {e.ErrorMessage} ({e.ErrorDomain})");

      }

      private void OnInterstitialFailedToLoad(object sender, MgErrorEventArgs e)
      {
         Debug.WriteLine($"--------> OnInterstitialFailedToLoad: Code = {e.ErrorCode}: {e.ErrorMessage} ({e.ErrorDomain})");

      }

      private void OnInterstitialImpression(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnInterstitialImpression");
      }
      
      private void OnInterstitialOpened(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnInterstitialOpened");
      }

      private void OnInterstitialClosed(object sender, EventArgs e)
      {
         Debug.WriteLine("--------> OnInterstitialClosed");
      }

      private void OnInterstitialLoaded(object sender, EventArgs e)
      {
         CrossMgAdmob.Current.ShowInterstitial();
      }

      
      protected override void OnStart()
      {
         BindAdMobEvents();
      }

      protected override void OnSleep()
      {
         UnbindAdMobEvents();
      }

      protected override void OnResume()
      {
         BindAdMobEvents();
      }
   }
}
