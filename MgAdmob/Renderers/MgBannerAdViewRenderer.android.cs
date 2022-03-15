using System;
using System.ComponentModel;
using Android.Content.Res;
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
   private AdView _adView;

   public MgBannerAdViewRenderer(Context context) 
      : base(context)
   {
   }

   private void CreateView()
   {
      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (_adView != null)
      {
         return;
      }

      var adUnitId = !string.IsNullOrEmpty(Element.AdUnitId) && !string.IsNullOrWhiteSpace(Element.AdUnitId)
         ? Element.AdUnitId
         : !string.IsNullOrEmpty(CrossMgAdmob.Current.AdUnitId) && !string.IsNullOrWhiteSpace(CrossMgAdmob.Current.AdUnitId)
            ? CrossMgAdmob.Current.AdUnitId
            : null;

      if (adUnitId == null)
      {
         var msg = string
            .Format
            (
               "Either {0}.{1} or {2}.{3}.{4} must be set before displaying an Ad",
               nameof(MgBannerAdView),
               nameof(MgBannerAdView.AdUnitId),
               nameof(CrossMgAdmob),
               nameof(CrossMgAdmob.Current),
               nameof(CrossMgAdmob.Current.AdUnitId)
            );

         throw new ApplicationException(msg);
      }

      var listener = new MgBannerAdViewListener();

      listener.AdClicked += Element.OnAdClicked;
      listener.AdClosed += Element.OnAdClosed;
      listener.AdImpression += Element.OnAdImpression;
      listener.AdOpened += Element.OnAdOpened;
      listener.AdFailedToLoad += Element.OnAdFailedToLoad;
      listener.AdLoaded += Element.OnAdLoaded;
      
      _adView = new AdView(Context)
      {
         AdSize = GetAdSize(),
         AdUnitId = Element.AdUnitId,
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

      if (_adView is not { AdListener: MgBannerAdViewListener listener })
      {
         return;
      }

      if (e.OldElement != null)
      {
         var o = e.OldElement;

         listener.AdClicked -= o.OnAdClicked;
         listener.AdClosed -= o.OnAdClosed;
         listener.AdImpression -= o.OnAdImpression;
         listener.AdOpened -= o.OnAdOpened;
         listener.AdFailedToLoad -= o.OnAdFailedToLoad;
         listener.AdLoaded -= o.OnAdLoaded;
      }

      if (e.NewElement != null)
      {
         var o = e.NewElement;

         listener.AdClicked += o.OnAdClicked;
         listener.AdClosed += o.OnAdClosed;
         listener.AdImpression += o.OnAdImpression;
         listener.AdOpened += o.OnAdOpened;
         listener.AdFailedToLoad += o.OnAdFailedToLoad;
         listener.AdLoaded += o.OnAdLoaded;
      }

   }
   

   protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
   {
      base.OnElementPropertyChanged(sender, e);

      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      System.Diagnostics.Debug.WriteLine($"---------> {nameof(MgBannerAdViewRenderer)}.{nameof(OnElementPropertyChanged)}(): e.PropertyName = {e.PropertyName}");

      if (Element == null)
      {
         return;
      }

      if (Element != null && Control == null)
      {
         if (Element.Width < 0 || Element.Height < 0 || string.IsNullOrEmpty(Element.AdUnitId) || string.IsNullOrWhiteSpace(Element.AdUnitId))
         {
            return;
         }

         CreateView();

         SetNativeControl(_adView);

         return;
      }

      if (Control == null)
      {
         return;
      }

      if (e.PropertyName != nameof(MgBannerAdView.AdUnitId))
      {
         return;
      }

      var adUnitId = !string.IsNullOrEmpty(Element.AdUnitId) && !string.IsNullOrWhiteSpace(Element.AdUnitId)
         ? Element.AdUnitId
         : !string.IsNullOrEmpty(CrossMgAdmob.Current.AdUnitId) && !string.IsNullOrWhiteSpace(CrossMgAdmob.Current.AdUnitId)
            ? CrossMgAdmob.Current.AdUnitId
            : null;

      if (adUnitId == null)
      {
         var msg = string
            .Format
            (
               "Either {0}.{1} or {2}.{3}.{4} must be set before displaying an Ad",
               nameof(MgBannerAdView),
               nameof(MgBannerAdView.AdUnitId),
               nameof(CrossMgAdmob),
               nameof(CrossMgAdmob.Current),
               nameof(CrossMgAdmob.Current.AdUnitId)
            );

         throw new ApplicationException(msg);
      }

      Control.AdUnitId = adUnitId;

      System.Diagnostics.Debug.WriteLine($"---------> {nameof(MgBannerAdViewRenderer)}.{nameof(OnElementPropertyChanged)}: Control.AdUnitId = {Control.AdUnitId}");
   }

   private AdSize GetAdSize()
   {
//      using var display = Context?.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
      
//      // ReSharper disable once JoinDeclarationAndInitializer
//      int adWidth;

//#if MONOANDROID10_0
//      using var outMetrics = new DisplayMetrics();
      
//      display?.DefaultDisplay?.GetMetrics(outMetrics);
      
//      adWidth = (int)Math.Min(Element.Width, outMetrics.WidthPixels / outMetrics.Density);
//#else
//      //var density = ((Resources?.Configuration?.DensityDpi ?? 160) / 160f);
//      //var displayWidth = display?.CurrentWindowMetrics.Bounds.Width() ?? 0;

//      var displayWidth = Resources?.DisplayMetrics?.WidthPixels ?? 0d;
//      var density = Resources?.DisplayMetrics?.Density ?? 1f;

//      if (displayWidth < 1)
//      {
//         displayWidth = Element.Width;
//      }

//      if (density <= 0)
//      {
//         density = 1;
//      }

//      adWidth = (int)Math.Min(Element.Width, displayWidth / density);
//#endif

      var displayWidth = Resources?.DisplayMetrics?.WidthPixels ?? 0d;
      var density = Resources?.DisplayMetrics?.Density ?? 1f;

      if (displayWidth < 1)
      {
         displayWidth = Element.Width;
      }

      if (density <= 0)
      {
         density = 1;
      }

      var adWidth = (int)Math.Min(Element.Width, displayWidth / density);

      return AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSize(Context, adWidth);
   }
}
