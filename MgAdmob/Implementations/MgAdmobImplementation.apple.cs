using System.Collections.Generic;
using System.Linq;
using Foundation;
using Google.MobileAds;
using Plugin.MgAdmob.Enums;


namespace Plugin.MgAdmob.Implementations;

public class MgAdmobImplementation : MgAdmobImplementationBase
{
   public MgAdmobImplementation()
   {
   }

   public static Request GetRequest()
   {
      var request = Request.GetDefaultRequest();


      var addExtra = false;
      var dict = new Dictionary<string, string>();

      MobileAds
         .SharedInstance
         .RequestConfiguration
         .TagForChildDirectedTreatment(CrossMgAdmob.Current.TagForChildDirectedTreatment == MgTagForChildDirectedTreatment.TreatmentTrue);

      MobileAds
         .SharedInstance
         .RequestConfiguration
         .TagForUnderAgeOfConsent(CrossMgAdmob.Current.TagForUnderAgeOfConsent == MgTagForUnderAgeOfConsent.ConsentTrue);

      MobileAds
         .SharedInstance
         .RequestConfiguration.MaxAdContentRating = CrossMgAdmob.Current.GetAdContentRatingString();

      if (CrossMgAdmob.Current.TestDevices != null)
      {
         MobileAds.SharedInstance.RequestConfiguration.TestDeviceIdentifiers = CrossMgAdmob.Current.TestDevices.ToArray();
      }

      if (!CrossMgAdmob.Current.UsePersonalisedAds)
      {
         dict.Add(new NSString("npa"), new NSString("1"));

         addExtra = true;
      }

      if (CrossMgAdmob.Current.UseRestrictedDataProcessing)
      {
         dict.Add(new NSString("rdp"), new NSString("1"));

         addExtra = true;
      }

      if (CrossMgAdmob.Current.ComplyWithFamilyPolicies)
      {
         //request.Tag(CrossMTAdmob.Current.ComplyWithFamilyPolicies);
         dict.Add(new NSString("max_ad_content_rating"), new NSString("G"));
         addExtra = true;
      }

      if (!addExtra)
      {
         return request;
      }

      var extras = new Extras
      {
         AdditionalParameters = NSDictionary.FromObjectsAndKeys(dict.Values.ToArray(), dict.Keys.ToArray())
      };

      request.RegisterAdNetworkExtras(extras);

      return request;
   }
   
   public override string GetAdContentRatingString()
   {
      return MaxAdContentRating switch
      {
         MgMaxAdContentRating.RatingG => "GADMaxAdContentRatingGeneral",
         MgMaxAdContentRating.RatingPg => "GADMaxAdContentRatingParentalGuidance",
         MgMaxAdContentRating.RatingT => "GADMaxAdContentRatingTeen",
         MgMaxAdContentRating.RatingMa => "GADMaxAdContentRatingMatureAudience",
         _ => "GADMaxAdContentRatingGeneral"
      };
   }


}
