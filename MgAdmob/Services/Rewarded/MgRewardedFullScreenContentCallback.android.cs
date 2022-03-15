using Android.Gms.Ads;
using Plugin.MgAdmob.Implementations;

namespace Plugin.MgAdmob.Services.Rewarded;

public class MgRewardedFullScreenContentCallback : FullScreenContentCallback
{
   private readonly MgAdmobImplementation _implementation;

   public MgRewardedFullScreenContentCallback(MgAdmobImplementation implementation)
   {
      _implementation = implementation;
   }
   
   public override void OnAdDismissedFullScreenContent()
   {
      base.OnAdDismissedFullScreenContent();

      _implementation.OnRewardedVideoAdClosed();
   }

   public override void OnAdFailedToShowFullScreenContent(AdError error)
   {
      base.OnAdFailedToShowFullScreenContent(error);

      _implementation.OnRewardedVideoAdFailedToShow(error);
   }

   public override void OnAdShowedFullScreenContent()
   {
      base.OnAdShowedFullScreenContent();

      _implementation.OnRewardedVideoAdOpened();
      _implementation.OnRewardedVideoAdCompleted();
   }

   public override void OnAdImpression()
   {
      base.OnAdImpression();

      _implementation.OnRewardedVideoAdImpression();
   }
}

