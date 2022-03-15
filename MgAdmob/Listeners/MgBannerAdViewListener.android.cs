﻿using System;
using Android.Gms.Ads;
using Plugin.MgAdmob.EventArgs;

namespace Plugin.MgAdmob.Listeners;

public class MgBannerAdViewListener : AdListener
{
   public event EventHandler AdClicked;
   public event EventHandler AdClosed;
   public event EventHandler AdImpression;
   public event EventHandler AdOpened;
   public event EventHandler<MgErrorEventArgs> AdFailedToLoad;
   public event EventHandler AdLoaded;

   public override void OnAdClicked()
   {
      base.OnAdClicked();
      AdClicked?.Invoke(this, null);
   }

   public override void OnAdClosed()
   {
      base.OnAdClosed();
      AdClosed?.Invoke(this, null);
   }

   public override void OnAdImpression()
   {
      base.OnAdImpression();
      AdImpression?.Invoke(this, null);
   }

   public override void OnAdOpened()
   {
      base.OnAdOpened();
      AdOpened?.Invoke(this, null);
   }

   public override void OnAdFailedToLoad(LoadAdError adError)
   {
      base.OnAdFailedToLoad(adError);

      var errorMessage = "Unknown error";

      switch (adError.Code)
      {
         case AdRequest.ErrorCodeInternalError:
            errorMessage = "Internal error, an invalid response was received from the ad server.";
            break;
         case AdRequest.ErrorCodeInvalidRequest:
            errorMessage = "Invalid ad request, possibly an incorrect ad unit ID was given.";
            break;
         case AdRequest.ErrorCodeNetworkError:
            errorMessage = "The ad request was unsuccessful due to network connectivity.";
            break;
         case AdRequest.ErrorCodeNoFill:
            errorMessage = "The ad request was successful, but no ad was returned due to lack of ad inventory.";
            break;
      }

      AdFailedToLoad?.Invoke(this, new MgErrorEventArgs { ErrorCode = adError.Code, ErrorMessage = errorMessage });
   }
   
   public override void OnAdLoaded()
   {
      base.OnAdLoaded();
      AdLoaded?.Invoke(this, null);
   }
}

