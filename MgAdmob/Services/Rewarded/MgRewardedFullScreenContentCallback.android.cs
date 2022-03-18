using Android.Gms.Ads;
using Plugin.MgAdmob.Extensions;
using Plugin.MgAdmob.Interfaces;

namespace Plugin.MgAdmob.Services.Rewarded;

public class MgRewardedFullScreenContentCallback : FullScreenContentCallback
{
   private readonly IMgAdmobImplementation _implementation;

   public MgRewardedFullScreenContentCallback(IMgAdmobImplementation implementation)
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

      _implementation.OnRewardedVideoAdFailedToShow(error.ToMgErrorEventArgs());
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

