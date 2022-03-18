using Android.Gms.Ads;
using Android.OS;
using Google.Ads.Mediation.Admob;
using Plugin.MgAdmob.Enums;

namespace Plugin.MgAdmob.Implementations;

public class MgAdmobImplementation : MgAdmobImplementationBase
{
   public MgAdmobImplementation()
   {
   }

   public static AdRequest.Builder GetRequest()
   {
      var addBundle = false;
      var bundleExtra = new Bundle();
      var requestBuilder = new AdRequest.Builder();
      var configuration = new RequestConfiguration.Builder();

      

      if (CrossMgAdmob.Current.TestDevices != null)
      {
         configuration = configuration.SetTestDeviceIds(CrossMgAdmob.Current.TestDevices);
      }

      if (!CrossMgAdmob.Current.UsePersonalisedAds)
      {
         bundleExtra.PutString("npa", "1");
         addBundle = true;
      }

      if (CrossMgAdmob.Current.UseRestrictedDataProcessing)
      {
         bundleExtra.PutString("rdp", "1");
         addBundle = true;
      }

      MobileAds.RequestConfiguration = configuration
         .SetTagForChildDirectedTreatment((int)CrossMgAdmob.Current.TagForChildDirectedTreatment)
         .SetTagForUnderAgeOfConsent((int)CrossMgAdmob.Current.TagForUnderAgeOfConsent)
         .SetMaxAdContentRating(CrossMgAdmob.Current.GetAdContentRatingString())
         .Build();

      if (addBundle)
      {
         requestBuilder = requestBuilder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra);
      }

      return requestBuilder;
   }
   
   public override string GetAdContentRatingString()
   {
      return MaxAdContentRating switch
      {
         MgMaxAdContentRating.RatingG => "G",
         MgMaxAdContentRating.RatingPg => "PG",
         MgMaxAdContentRating.RatingT => "T",
         MgMaxAdContentRating.RatingMa => "MA",
         _ => string.Empty
      };
   }
}

