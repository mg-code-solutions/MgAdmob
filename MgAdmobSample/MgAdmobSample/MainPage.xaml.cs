using System;
using Plugin.MgAdmob;
using Plugin.MgAdmob.EventArgs;
using Xamarin.Forms;

namespace MgAdmobSample
{
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      private void InterstitialButtonClicked(object sender, EventArgs e)
      {
         if (Device.RuntimePlatform == Device.Android)
         {
            CrossMgAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
         }

         if (Device.RuntimePlatform == Device.iOS)
         {
            CrossMgAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/4411468910");
         }
      }

      private void RewardVideoAdButtonClicked(object sender, EventArgs e)
      {
         if (Device.RuntimePlatform == Device.Android)
         {
            CrossMgAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/5224354917");
         }

         if (Device.RuntimePlatform == Device.iOS)
         {
            CrossMgAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/1712485313");
         }

      }

      private void MgBannerAdView_OnAdFailedToLoad(object sender, MgErrorEventArgs e)
      {
         Console.WriteLine($"Code = {e.ErrorCode}: {e.ErrorMessage} ({e.ErrorDomain})");
      }
   }
}
