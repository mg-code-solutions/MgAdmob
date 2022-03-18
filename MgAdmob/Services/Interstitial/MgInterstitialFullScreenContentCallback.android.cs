using Android.Gms.Ads;
using Plugin.MgAdmob.Extensions;
using Plugin.MgAdmob.Interfaces;

namespace Plugin.MgAdmob.Services.Interstitial;

public class MgInterstitialFullScreenContentCallback : FullScreenContentCallback
{
   private readonly IMgAdmobImplementation _implementation;

   public MgInterstitialFullScreenContentCallback(IMgAdmobImplementation implementation)
   {
      _implementation = implementation;
   }
   
   public override void OnAdDismissedFullScreenContent()
   {
      base.OnAdDismissedFullScreenContent();

      _implementation.OnInterstitialClosed();
   }

   public override void OnAdFailedToShowFullScreenContent(AdError error)
   {
      base.OnAdFailedToShowFullScreenContent(error);

      _implementation.OnInterstitialFailedToShow(error.ToMgErrorEventArgs());
   }

   public override void OnAdShowedFullScreenContent()
   {
      base.OnAdShowedFullScreenContent();

      _implementation.OnInterstitialOpened();
   }

   public override void OnAdImpression()
   {
      base.OnAdImpression();

      _implementation.OnInterstitialImpression();
   }
}

