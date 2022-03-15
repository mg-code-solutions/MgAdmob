using System;
using Plugin.MgAdmob.EventArgs;
using Xamarin.Forms;

namespace Plugin.MgAdmob.Controls;

public class MgBannerAdView : View
{
   public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(nameof(AdUnitId), typeof(string), typeof(MgBannerAdView));

   public string AdUnitId
   {
      get => (string)GetValue(AdUnitIdProperty);
      set => SetValue(AdUnitIdProperty, value);
   }

   public event EventHandler AdClicked;
   public event EventHandler AdClosed;
   public event EventHandler AdImpression;
   public event EventHandler AdOpened;
   public event EventHandler<MgErrorEventArgs> AdFailedToLoad;
   public event EventHandler AdLoaded;

   public void OnAdClicked(object sender, System.EventArgs e)
   {
      AdClicked?.Invoke(sender, e);
   }

   public void OnAdClosed(object sender, System.EventArgs e)
   {
      AdClosed?.Invoke(sender, e);
   }

   public void OnAdImpression(object sender, System.EventArgs e)
   {
      AdImpression?.Invoke(sender, e);
   }

   public void OnAdOpened(object sender, System.EventArgs e)
   {
      AdOpened?.Invoke(sender, e);
   }

   public void OnAdFailedToLoad(object sender, MgErrorEventArgs e)
   {
      AdFailedToLoad?.Invoke(sender, e);
   }

   public void OnAdLoaded(object sender, System.EventArgs e)
   {
      AdLoaded?.Invoke(sender, e);
   }

}

