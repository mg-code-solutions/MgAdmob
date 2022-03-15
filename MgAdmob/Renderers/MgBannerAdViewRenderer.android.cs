using System;
using Android.Gms.Ads;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Plugin.MgAdmob.Controls;
using Plugin.MgAdmob.Implementations;
using Plugin.MgAdmob.Listeners;
using Plugin.MgAdmob.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Context = Android.Content.Context;


[assembly: ExportRenderer(typeof(MgBannerAdView), typeof(MgBannerAdViewRenderer))]
namespace Plugin.MgAdmob.Renderers;

public class MgBannerAdViewRenderer : ViewRenderer<MgBannerAdView, AdView>
{
   private string _adUnitId = string.Empty;
   private AdView _adView;

   public MgBannerAdViewRenderer(Context context) 
      : base(context)
   {
   }

   private void CreateNativeControl(MgBannerAdView mgBannerAdView, string adUnitId)
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (_adView != null)
      {
         return;
      }

      _adUnitId = !string.IsNullOrEmpty(adUnitId) 
         ? adUnitId 
         : CrossMgAdmob.Current.AdUnitId;

      if (string.IsNullOrEmpty(_adUnitId))
      {
         Console.WriteLine("You must set the adsID before using it");
      }

      var listener = new MgBannerAdViewListener();

      listener.AdClicked += mgBannerAdView.OnAdClicked;
      listener.AdClosed += mgBannerAdView.OnAdClosed;
      listener.AdImpression += mgBannerAdView.OnAdImpression;
      listener.AdOpened += mgBannerAdView.OnAdOpened;
      listener.AdFailedToLoad += mgBannerAdView.OnAdFailedToLoad;
      listener.AdLoaded += mgBannerAdView.OnAdLoaded;
      
      _adView = new AdView(Context)
      {
         AdSize = GetAdSize(),
         AdUnitId = _adUnitId,
         AdListener = listener,
         LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
      };

      var requestBuilder = MgAdmobImplementation.GetRequest();

      _adView.LoadAd(requestBuilder.Build());
   }

   protected override void OnElementChanged(ElementChangedEventArgs<MgBannerAdView> e)
   {
      base.OnElementChanged(e);

      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (Control != null)
      {
         return;
      }

      CreateNativeControl(e.NewElement, e.NewElement.AdUnitId);

      SetNativeControl(_adView);
   }

   private AdSize GetAdSize()
   {
      var outMetrics = new DisplayMetrics();
      using var display = Context?.GetSystemService(Android.Content.Context.WindowService).JavaCast<IWindowManager>();

      display?.DefaultDisplay?.GetMetrics(outMetrics);

      var adWidth = (int)(outMetrics.WidthPixels / outMetrics.Density);

      return AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSize(Context, adWidth);
   }
}
