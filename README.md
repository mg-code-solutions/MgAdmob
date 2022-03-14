# MgAdmob plugin for Xamarin (Android & iOS)

Utilise Google Admob Ads (banners, interstitial, and rewarded videos) in your Xamarin Projects (Android and iOS)

## Setup
* Available on Nuget: TODO
* Install in your .NetStandard project and Android/iOS projects

#### This plugin supports:
* Xamarin.Android
* Xamarin.iOS

## How to use MgAdmob

 TODO

### To add a banner in your project

Two options are available for adding banner ads to your app

#### 1) XAML

Remember to replace ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy with your Ad Unit Id from Google Admob

```csharp
<controls:MgAdView AdUnitId="ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy"/>
```

Add this line in your XAML:
```csharp
xmlns:controls="clr-namespace:Plugin.MgAdmob.Controls;assembly=Plugin.MgAdmob"
```

Banner Ids can be customised for Android and iOS, use the OnPlatform-Property as shown in the example below (test Ad Unit Ids shown below):
```csharp
<controls:MgBannerAdView 	
	AdUnitId="{OnPlatform Android='ca-app-pub-3940256099942544/6300978111', iOS='ca-app-pub-3940256099942544/2934735716'}"
	/>
```

Alternatively, for all banners in your app you can set the AdUnitId property via a <Style> entry in App.xaml (similar to HeightRequest as explained below)

#### 2) Code
```csharp
MgBannerAdView ads = new MgBannerAdView();
```

### Ad Unit Ids

When testing, use the following Ad Unit Ids, provided by Google. When releasing to production, replace the test Ad Unit Ids with your own Ids:

#### Banner Ad Test Ids

```csharp
Android: ca-app-pub-3940256099942544/6300978111
iOS: ca-app-pub-3940256099942544/2934735716
```

#### Interstitial Ad Test Ids

```csharp
Android: ca-app-pub-3940256099942544/1033173712
iOS: ca-app-pub-3940256099942544/4411468910
```

#### Interstitial Ad Test Ids

```csharp
Android: ca-app-pub-3940256099942544/5224354917
iOS: ca-app-pub-3940256099942544/6978759866
```

## Styling the MgBannerAdView control in App.xaml

** NB: Banner ads are somewhat particular about their sizing. If banners ads are not displaying, try defaulting the HeightRequest by adding the following style you your app.xaml:**

```csharp
<Style TargetType="MgBannerAdView">
    <Setter Property="HeightRequest">
        <Setter.Value>
            <x:OnIdiom Phone="60" Tablet="90"/>
        </Setter.Value>
    </Setter>
</Style>
```

## Properties

### Global Properties

#### IsEnabled 
	
(default: true): true / false - enables or disables the loading / displaying of ads
	
#### AdUnitId 
	
(default: null): Ad Unit Id to be used for all MgBannerAdViews, can be overridden by setting the AdUnitId on the MgBannerAdView control
	
#### UsePersonalisedAds
	
(default: false): true / false - used to influence whether Google Ads uses personalised ads or generic ads

#### UseRestrictedDataProcessing

(default: true): true / false - TODO
	
#### ComplyWithFamilyPolicies

(default: true): true / false - specify whether Google Ads should comply with Family Policies
	
#### TagForChildDirectedTreatment

(default: TreatmentUnspecified): MgTagForChildDirectedTreatment enum - TODO
	
#### TagForUnderAgeOfConsent

(default: ConsentUnspecified): MgTagForUnderAgeOfConsent enum - TODO
	
#### MaxAdContentRating
	
(default: RatingG): MgMaxAdContentRating enum - maximum rating that displayed ads can be
	
#### TestDevices

(default: empty list): list of string entries representing test device ids

Global properties can be used as shown below:
	
```csharp
CrossMgAdmob.Current.TagForChildDirectedTreatment = MgTagForChildDirectedTreatment.TreatmentUnspecified;
CrossMgAdmob.Current.TagForUnderAgeOfConsent = MgTagForUnderAgeOfConsent.ConsentUnspecified;
CrossMgAdmob.Current.MaxAdContentRating = MgMaxAdContentRating.RatingG;
CrossMgAdmob.Current.UsePersonalizedAds = false;
CrossMgAdmob.Current.ComplyWithFamilyPolicies = true;
CrossMgAdmob.Current.UseRestrictedDataProcessing = true;
```

### MgBannerAdView Properties
	
MgBannerAdView allows you to set the Ad Unit Id to specify the ads to load:

AdUnitId: Set this to the Ad Unit Id from Google AdMob

## How to Use MgAdMob
	
### Interstitial ads

To load an Interstitial Ad, use the following (replacing xx-xxx-xxx-xxxxxxxxxxxxxxxxx/xxxxxxxxxx with your Ad Unit Id from Google Admob):
```csharp
CrossMgAdmob.Current.LoadInterstitial("xx-xxx-xxx-xxxxxxxxxxxxxxxxx/xxxxxxxxxx");
```

Once loaded, an Interstitial Ad can be displayed as shown below:
```csharp
CrossMgAdmob.Current.ShowInterstitial();
```

**NB: Intersitial Ads may take some time to load: to avoid UX delays, load the ad early in the program flow and then show the ad at the appropriate time later**

### Rewarded video ads

To load a Reqard Video Ad, use the following (replacing xx-xxx-xxx-xxxxxxxxxxxxxxxxx/xxxxxxxxxx with your Ad Unit Id from Google Admob):
```csharp
CrossMgAdmob.Current.LoadRewardedVideo("xx-xxx-xxx-xxxxxxxxxxxxxxxxx/xxxxxxxxxx");
```

Once loaded, a Rewarded Video Ad can be displayed as shown below:
```csharp
CrossMgAdmob.Current.ShowRewardedVideo();
```

**NB: Reward Video Ads may take some time to load: to avoid UX delays, load the ad early in the program flow and then show the ad at the appropriate time later**

## Events
	
### MgBannerAdView

```csharp
AdClicked
AdClosed
AdImpression
AdOpened
AdFailedToLoad
AdLoaded
```

### Interstitial Ads

```csharp
InterstitialLoaded
InterstitialClosed
InterstitialOpened
InterstitialImpression
InterstitialFailedToLoad
InterstitialFailedToShow
```

### Rewarded Video Ads

```csharp
RewardedVideoAdLoaded
RewardedVideoAdClosed
RewardedVideoAdFailedToLoad
RewardedVideoAdFailedToShow
RewardedVideoAdCompleted
RewardedVideoAdLeftApplication
RewardedVideoAdOpened
RewardedVideoStarted
Rewarded
```

## Important Configuration

### Code

Remember to include the MgAdmob library with this code (usually added automatically):

```csharp
using Plugin.MgAdmob;
```

### XAML

Add the following to any XAML file you wish to use MgAdmob in
	
```csharp
xmlns:controls="clr-namespace:Plugin.MgAdmob.Controls;assembly=Plugin.MgAdmob"
```

### Android

The Mobile Ads SDK must be initialised before use. This can be done by calling MobileAds.Initialize(ApplicationContext) in the OnCreate() method for your MainActivity.cs:

```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
   TabLayoutResource = Resource.Layout.Tabbar;
   ToolbarResource = Resource.Layout.Toolbar;

   base.OnCreate(savedInstanceState);            
	
   // Initialilse Mobile Ads
   MobileAds.Initialize(ApplicationContext);
	
   Xamarin.Forms.Forms.Init(this, savedInstanceState); 
   LoadApplication(new App());
}
```

Add the following to AppManifest.xml (between the <application></application> tags):

```csharp
<meta-data android:name="com.google.android.gms.ads.APPLICATION_ID" android:value="ca-app-pub-1224013205445908~5102979875" />
<activity android:name="com.google.android.gms.ads.AdActivity" android:configChanges="keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize" android:theme="@android:style/Theme.Translucent" />
```

Also, select the following permissions to the Android project properties:

ACCESS_NETWORK_STATE
INTERNET

Alternative, add the following entries directly to AndroidManifest.xml (after the <application></application> tags):
```csharp
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.INTERNET" />
```

If your Ads are not being displayed in the Android Emulator, make sure the Emulator was created with Google APIs selected, otherwise you'll find this message in your Debugger Console:

[GooglePlayServicesUtil] Google Play Store is missing.

### iOS:

The Mobile Ads SDK must be initialised before use. This can be done by calling MobileAds.SharedInstance.Start() in the FinishedLaunching() method for your AppDelegate.cs:

```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
   // Initialilse Mobile Ads
   MobileAds.SharedInstance.Start(CompletionHandler);

   global::Xamarin.Forms.Forms.Init();
   LoadApplication(new App());

   return base.FinishedLaunching(app, options);
}

private void CompletionHandler(InitializationStatus status)
{
}
```

Edit your info.plist, and add the the following keys (remembering to replace ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy with your project id from Google Admob):

```csharp
<key>GADApplicationIdentifier</key>
<string>ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy</string>
<key>GADIsAdManagerApp</key>
<true/>
<key>SKAdNetworkItems</key>
<array>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>cstr6suwn9.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>4fzdc2evr5.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>2fnua5tdw4.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>ydx93a7ass.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>5a6flpkh64.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>p78axxw29g.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>v72qych5uu.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>c6k4g5qg8m.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>s39g8k73mm.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>3qy4746246.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>3sh42y64q3.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>f38h382jlk.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>hs6bdukanm.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>prcb7njmu6.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>v4nxqhlyqp.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>wzmmz9fp6w.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>yclnxrl5pm.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>t38b2kh725.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>7ug5zh24hu.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>9rd848q2bz.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>n6fk4nfna4.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>kbd757ywx3.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>9t245vhmpl.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>4468km3ulz.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>2u9pt9hc89.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>8s468mfl3y.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>av6w8kgt66.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>klf5c3l5u5.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>ppxm28t8ap.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>424m5254lk.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>uw77j35x4d.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>578prtvx9j.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>4dzt52r2t5.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>e5fvkxwrpn.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>8c4e2ghe7u.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>zq492l623r.skadnetwork</string>
	</dict>
	<dict>
		<key>SKAdNetworkIdentifier</key>
		<string>3qcr597p9d.skadnetwork</string>
	</dict>
</array> 
```

To ustilise Google Admob on iOS, you must either build on a Mac machine or be paired to a Mac when building your project (i.e. Visual Studio on Windows)
