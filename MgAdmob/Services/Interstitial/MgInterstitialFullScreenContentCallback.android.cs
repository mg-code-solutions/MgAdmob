using Android.Gms.Ads;
using Plugin.MgAdmob.Implementations;

namespace Plugin.MgAdmob.Services.Interstitial;

public class MgInterstitialFullScreenContentCallback : FullScreenContentCallback
{
   private readonly MgAdmobImplementation _implementation;

   public MgInterstitialFullScreenContentCallback(MgAdmobImplementation implementation)
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

      _implementation.OnInterstitialFailedToShow(error);
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

