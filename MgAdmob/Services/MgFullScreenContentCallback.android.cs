using Android.Gms.Ads;

namespace Plugin.MgAdmob.Services;

public class MgFullScreenContentCallback : FullScreenContentCallback
{
   private readonly bool _isInterstitial;

   private readonly MgAdmobImplementation _implementation;

   public MgFullScreenContentCallback(MgAdmobImplementation implementation, bool isInterstitial)
   {
      _implementation = implementation;
      _isInterstitial = isInterstitial;
   }
   
   public override void OnAdDismissedFullScreenContent()
   {
      base.OnAdDismissedFullScreenContent();

      if (_isInterstitial)
      {
         _implementation.OnInterstitialClosed();
      }
      else
      {
         _implementation.OnRewardedVideoAdClosed();
      }
   }

   public override void OnAdFailedToShowFullScreenContent(AdError error)
   {
      base.OnAdFailedToShowFullScreenContent(error);

      if (_isInterstitial)
      {
         _implementation.OnInterstitialFailedToShow(error);
      }
      else
      {
         _implementation.OnRewardedVideoAdFailedToShow(error);
      }
   }

   public override void OnAdShowedFullScreenContent()
   {
      base.OnAdShowedFullScreenContent();

      if (_isInterstitial)
      {
         _implementation.OnInterstitialOpened();
      }
      else
      {
         _implementation.OnRewardedVideoAdOpened();
         _implementation.OnRewardedVideoAdCompleted();
      }
   }

   public override void OnAdImpression()
   {
      base.OnAdImpression();

      if (_isInterstitial)
      {
         _implementation.OnInterstitialImpression();
      }
      else
      {
         _implementation.OnRewardedVideoAdImpression();
      }
   }
}

