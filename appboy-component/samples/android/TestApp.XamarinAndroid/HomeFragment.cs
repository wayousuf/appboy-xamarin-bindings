﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AndroidX.Fragment.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using Com.Appboy;
using Com.Appboy.UI;
using Com.Appboy.UI.Inappmessage;
using Com.Appboy.Models;
using Com.Appboy.Enums;
using Com.Appboy.Enums.Inappmessage;
using Com.Appboy.Models.Outgoing;

namespace com.appboy.xamarinsample
{
  public class HomeFragment : Fragment
  {
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      if (container == null)
      {
        return null;
      }
      AppboyFirebaseMessagingService appboyFirebaseMessagingService = new AppboyFirebaseMessagingService();

      View view = inflater.Inflate (Resource.Layout.HomeFragment, container, false);

      // Change User
      view.FindViewById<Button> (Resource.Id.changeUserButton).Click += delegate {
        String userId = "myUser" + (new Random().Next());
        Appboy.GetInstance(Activity).ChangeUser(userId);
        view.FindViewById<TextView> (Resource.Id.userId).Text = "UserId: " + userId;
      };

      // Set User Properties
      view.FindViewById<Button> (Resource.Id.userPropertyButton).Click += delegate {
        Appboy.GetInstance(Activity).CurrentUser.SetCountry("USA");
        Appboy.GetInstance(Activity).CurrentUser.SetDateOfBirth(1987, Month.September, 21);
        Appboy.GetInstance(Activity).CurrentUser.SetEmail("brian@braze.com");
        Appboy.GetInstance(Activity).CurrentUser.SetFirstName("Brian");
        Appboy.GetInstance(Activity).CurrentUser.SetLastName("Wheeler");
        Appboy.GetInstance(Activity).CurrentUser.SetHomeCity("Mountain View");
        Appboy.GetInstance(Activity).CurrentUser.SetPhoneNumber("5555555555");
        Appboy.GetInstance(Activity).CurrentUser.SetCustomUserAttribute("customAttributeKey", true);
        Appboy.GetInstance(Activity).CurrentUser.SetEmailNotificationSubscriptionType(NotificationSubscriptionType.OptedIn);
        Appboy.GetInstance(Activity).CurrentUser.SetGender(Gender.Male);
        Appboy.GetInstance(Activity).CurrentUser.SetAttributionData(new AttributionData("a", "b", "c", "d"));
      };

      // Events and purchases
      view.FindViewById<Button> (Resource.Id.eventsAndPurchasesButton).Click += delegate {
        Appboy.GetInstance(Activity).LogCustomEvent("myCustomEvent");
        Appboy.GetInstance(Activity).LogPurchase("myProduct", "USD", new Java.Math.BigDecimal(3.50));
      };

      // Social
      view.FindViewById<Button> (Resource.Id.trackSocialDataButton).Click += delegate {

        /*
           FacebookUser(
            String facebookId,
            String firstName,
            String lastName,
            String email,
            String bio,
            String cityName,
            // Gender is an Appboy enum.
            // Specify either Gender.MALE or Gender.FEMALE.
            Gender gender,
            Integer numberOfFriends,
            // Names of pages the user likes.
            Collection<String> likes,
            // mm/dd/yyyy format.
            String birthday
          )
        **/
        FacebookUser facebookUser = new FacebookUser("708379", "Test", "User", "test@appboy.com", "Test",
          "Testtown", Gender.Male, new Java.Lang.Integer(100), new String[]{"Cats", "Dogs"}, "06/17/1987");
        Appboy.GetInstance(Activity).CurrentUser.SetFacebookData(facebookUser);

        /*
        TwitterUser(
          Integer twitterUserId,
          String twitterHandle,
          String name,
          String description,
          Integer followerCount,
          Integer followingCount,
          Integer tweetCount,
          String profileImageUrl
        )
        **/
        TwitterUser twitterUser = new TwitterUser(new Java.Lang.Integer(6253282), "Test", "User", "Tester",  new Java.Lang.Integer(100), new Java.Lang.Integer(100), 
          new Java.Lang.Integer(100), "https://si0.twimg.com/profile_images/2685532587/fa47382ad67a0135acc62d4c6b49dbdc_bigger.jpeg");
        Appboy.GetInstance(Activity).CurrentUser.SetTwitterData(twitterUser);
      };

      // Content Cards
      view.FindViewById<Button> (Resource.Id.launchContentCardsButton).Click += delegate {
        FragmentTransaction fragmentTransaction = Activity.SupportFragmentManager.BeginTransaction ();
        AppboyContentCardsFragment contentCards = new AppboyContentCardsFragment();
        fragmentTransaction.Replace (Resource.Id.root, contentCards, contentCards.Class.ToString ());
        fragmentTransaction.AddToBackStack (contentCards.Class.ToString ());
        fragmentTransaction.Commit ();
      };

      // Feed
      view.FindViewById<Button> (Resource.Id.launchNewsFeedButton).Click += delegate {
        FragmentTransaction fragmentTransaction = Activity.SupportFragmentManager.BeginTransaction();
        AppboyFeedFragment feedFragment = new AppboyFeedFragment();
        fragmentTransaction.Replace(Resource.Id.root, feedFragment, feedFragment.Class.ToString());
        fragmentTransaction.AddToBackStack(feedFragment.Class.ToString());
        fragmentTransaction.Commit();   
      };

      // In-App Message
      view.FindViewById<Button> (Resource.Id.addInAppMessageButton).Click += delegate {
        InAppMessageSlideup slideup = new InAppMessageSlideup();
        slideup.Message = "This is the message";
        slideup.SetClickAction(ClickAction.Uri, Android.Net.Uri.Parse("http://appboy.com"));
        AppboyInAppMessageManager.Instance.AddInAppMessage(slideup);
      };

      // Request Data Flush
      view.FindViewById<Button> (Resource.Id.dataFlushButton).Click += delegate {
        Appboy.GetInstance(Activity).RequestImmediateDataFlush();
      };

      // Wipe Data
      view.FindViewById<Button> (Resource.Id.wipeDataButton).Click += delegate {
        Appboy.WipeData(Activity);
      };

      // Disable SDK
      view.FindViewById<Button> (Resource.Id.disableSDKButton).Click += delegate {
        Appboy.DisableSdk(Activity);
      };

      // Enable SDK
      view.FindViewById<Button> (Resource.Id.enableSDKButton).Click += delegate {
        Appboy.EnableSdk(Activity);
      };
				
      return view;
    }
  }
}
