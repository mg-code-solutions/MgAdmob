using Android.Gms.Ads;
using Android.Gms.Ads.Rewarded;
using Plugin.MgAdmob.EventArgs;

namespace Plugin.MgAdmob.Extensions;

public static class AdErrorExtensions
{
   public static MgErrorEventArgs ToMgErrorEventArgs(this AdError error)
   {
      return new MgErrorEventArgs
      {
         ErrorCode = (int)error.Code,
         ErrorMessage = error.Message,
         ErrorDomain = error.Domain
      };

   }

   public static MgErrorEventArgs ToMgErrorEventArgs(this LoadAdError error)
   {
      return new MgErrorEventArgs
      {
         ErrorCode = error.Code,
         ErrorMessage = error.Message,
         ErrorDomain = error.Domain
      };

   }

   public static MgRewardEventArgs ToMgRewardEventArgs(this IRewardItem reward)
   {
      return new MgRewardEventArgs
      {
         RewardAmount = reward.Amount,
         RewardType = reward.Type
      };

   }
}

