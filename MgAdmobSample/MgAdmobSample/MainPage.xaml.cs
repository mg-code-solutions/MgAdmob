using System;
using Plugin.MgAdmob;
using Plugin.MgAdmob.Enums;
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
            CrossMgAdmob
               .Current
               .GetAdService(MgAdServiceType.Interstitial)?
               .Load("ca-app-pub-3940256099942544/1033173712");
         }

         if (Device.RuntimePlatform == Device.iOS)
         {
            CrossMgAdmob
               .Current
               .GetAdService(MgAdServiceType.Interstitial)?
               .Load("ca-app-pub-3940256099942544/4411468910");
         }
      }

      private void RewardVideoAdButtonClicked(object sender, EventArgs e)
      {
         if (Device.RuntimePlatform == Device.Android)
         {
            CrossMgAdmob
               .Current
               .GetAdService(MgAdServiceType.Interstitial)?
               .Load("ca-app-pub-3940256099942544/5224354917");
         }

         if (Device.RuntimePlatform == Device.iOS)
         {
            CrossMgAdmob
               .Current
               .GetAdService(MgAdServiceType.RewardVideo)?
               .Load("ca-app-pub-3940256099942544/1712485313");
         }

      }

      private void MgBannerAdView_OnAdFailedToLoad(object sender, MgErrorEventArgs e)
      {
         Console.WriteLine($"Code = {e.ErrorCode}: {e.ErrorMessage} ({e.ErrorDomain})");
      }
   }
}
