using System;
using System.ComponentModel;
using Google.MobileAds;
using Plugin.MgAdmob.Controls;
using Plugin.MgAdmob.EventArgs;
using Plugin.MgAdmob.Implementations;
using Plugin.MgAdmob.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MgBannerAdView), typeof(MgBannerAdViewRenderer))]
namespace Plugin.MgAdmob.Renderers;

public class MgBannerAdViewRenderer : ViewRenderer<MgBannerAdView, BannerView>
{
   //private string _adUnitId = string.Empty;
   private BannerView _bannerView;
   private UIViewController _controller;

   private void CreateView()
   {

      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (_controller == null)
      {
         _controller = GetVisibleViewController();

         if (_controller == null)
         {
            return;
         }
      }

      if (_bannerView != null)
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


      var width = (int)Math.Min(Element.Width, UIScreen.MainScreen.Bounds.Size.Width);

      System.Diagnostics.Debug.WriteLine($"---------> {nameof(MgBannerAdViewRenderer)}.{nameof(CreateView)}(): width = {width}, {Element.Width}, {UIScreen.MainScreen.Bounds.Size.Width}");

      _bannerView = new BannerView
      {
         AdUnitId = adUnitId,
         AdSize = AdSizeCons.GetCurrentOrientationAnchoredAdaptiveBannerAdSize(width),
         RootViewController = _controller
      };

      _bannerView.AdReceived += Element.OnAdLoaded;
      _bannerView.ClickRecorded += Element.OnAdClicked;
      _bannerView.ImpressionRecorded += Element.OnAdImpression;

      _bannerView.ReceiveAdFailed += (sender, args) =>
         Element
            .OnAdFailedToLoad
            (
               sender,
               new MgErrorEventArgs
               {
                  ErrorCode = (int)args.Error.Code,
                  ErrorMessage = args.Error.LocalizedDescription,
                  ErrorDomain = args.Error.Domain
               }
            );

      _bannerView.ScreenDismissed += Element.OnAdClosed;
      _bannerView.WillPresentScreen += Element.OnAdOpened;


      var request = MgAdmobImplementation.GetRequest();

      _bannerView.LoadRequest(request);
   }
   
   protected override void OnElementChanged(ElementChangedEventArgs<MgBannerAdView> e)
   {
      base.OnElementChanged(e);

      if (!CrossMgAdmob.Current.IsEnabled)
      {
         return;
      }

      if (_bannerView != null)
      {
         return;
      }

      if (Control != null)
      {
         return;
      }

      if (_controller != null)
      {
         return;
      }

      _controller = GetVisibleViewController();
   }

   private static UIViewController GetVisibleViewController()
   {
      UIViewController rootController;

      try
      {
         rootController = UIApplication
            .SharedApplication
            .Delegate
            .GetWindow()
            .RootViewController;

         if (rootController == null)
         {
            return null;
         }

      }
      catch (NullReferenceException)
      {
         return null;
      }

      if (rootController.PresentedViewController is UINavigationController uiNavigationController)
      {
         return uiNavigationController.VisibleViewController;
      }

      if (rootController.PresentedViewController is UITabBarController uiTabBarController)
      {
         return uiTabBarController.SelectedViewController;
      }


      if (rootController.PresentedViewController == null)
      {
         return rootController;
      }

      return rootController.PresentedViewController;
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

         if (_bannerView != null)
         {
            SetNativeControl(_bannerView);
         }

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

      System.Diagnostics.Debug.WriteLine($"---------> {nameof(MgBannerAdViewRenderer)}.{nameof(OnElementPropertyChanged)}(): Control.AdUnitId = {Control.AdUnitId}");
   }
}

