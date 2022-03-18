using Foundation;
using Google.MobileAds;
using Plugin.MgAdmob.EventArgs;

namespace Plugin.MgAdmob.Extensions;

public static class AppleExtensions
{
   public static MgErrorEventArgs ToMgErrorEventArgs(this NSError error)
   {
      return new MgErrorEventArgs
      {
         ErrorCode = (int)error.Code,
         ErrorMessage = error.LocalizedDescription,
         ErrorDomain = error.Domain
      };

   }

   public static MgRewardEventArgs ToMgRewardEventArgs(this AdReward reward)
   {
      return new MgRewardEventArgs
      {
         RewardAmount = reward.Amount.DoubleValue,
         RewardType = reward.Type
      };

   }
}

