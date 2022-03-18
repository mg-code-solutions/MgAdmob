using System;
using Plugin.MgAdmob.Enums;
using Plugin.MgAdmob.Implementations;
using Plugin.MgAdmob.Interfaces;
using Plugin.MgAdmob.Services.Interstitial;
using Plugin.MgAdmob.Services.Rewarded;

namespace Plugin.MgAdmob;

public static class CrossMgAdmob
{
   private static readonly Lazy<IMgAdmobImplementation> Implementation = new(CreateMgAdmob, System.Threading.LazyThreadSafetyMode.PublicationOnly);

   /// <summary>
   /// Gets if the plugin is supported on the current platform.
   /// </summary>
   public static bool IsSupported => Implementation.Value != null;

   /// <summary>
   /// Current plugin implementation to use
   /// </summary>
   public static IMgAdmobImplementation Current
   {
      get
      {
         var ret = Implementation.Value;

         if (ret == null)
         {
            throw new NotImplementedException
            (
               "This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation."
            );
         }

         return ret;
      }
   }

   private static IMgAdmobImplementation CreateMgAdmob()
   {
#if NETSTANDARD1_0 || NETSTANDARD2_0
      return null;
#else
#pragma warning disable IDE0022 // Use expression body for methods
      var imp = new MgAdmobImplementation();

      imp.RegisterAdService(MgAdServiceType.Interstitial, new MgInterstitialService());
      imp.RegisterAdService(MgAdServiceType.RewardVideo, new MgRewardService());

      return imp;
#pragma warning restore IDE0022 // Use expression body for methods
#endif
   }
   
}
