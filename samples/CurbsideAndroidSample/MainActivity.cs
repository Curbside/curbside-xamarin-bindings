using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using System.Linq;
using Firebase;
using Curbside;
using System;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;

[assembly: UsesFeature("android.hardware.location.gps")]

namespace CurbsideAndroidSample
{
	[Activity(Label = "CurbsideAndroidSample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : AppCompatActivity
	{
        static readonly string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        TextView msgText;

        const int PERMISSION_REQUEST_CODE = 1001;

		protected override void OnCreate(Bundle savedInstanceState)
		{
            Log.Debug(TAG, "google app id: " + GetString(Resource.String.google_app_id));

            base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);
            msgText = FindViewById<TextView>(Resource.Id.msgText);

            var logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);
            logTokenButton.Click += delegate {
                Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
            };

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }

            IsPlayServicesAvailable();

            var notification = CreateNotificationChannel();

            if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted
			    && ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted) 
            {
				var permissions = new[] { Android.Manifest.Permission.AccessFineLocation, Android.Manifest.Permission.AccessCoarseLocation };
				ActivityCompat.RequestPermissions(this, permissions, PERMISSION_REQUEST_CODE);
			}

            // TokyoJoe's Token
            Curbside.CSUserSession.Init(this, new Curbside.TokenCurbsideCredentialProvider("e445b0b497694445084e2c91624092529938d11fe91ad546bdee1b377d51b850"));

			Curbside.CSUserSession.Instance.RegisterTrackingIdentifier("test");
            Curbside.CSUserInfo userInfo = new Curbside.CSUserInfo("test", "test@test.com", "55555555", "NAP789", "Tesla", "S");
            Curbside.CSUserSession.Instance.SetUserInfo(userInfo);
           
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");
            if(notification !=null)
            {
                Curbside.CSUserSession.Instance.SetNotificationForForegroundService(notification);
            }
            Curbside.CSUserSession.Instance.StartTripToSiteWithIdentifier("tokyojoes_0", r);
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
		{
			if (requestCode == PERMISSION_REQUEST_CODE) 
            {
				if (grantResults.Any(gr => gr == Android.Content.PM.Permission.Denied))
					Toast.MakeText(this, "Permission for Location was Denied", ToastLength.Long).Show();
			}

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        public Notification CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return null;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
            return new Notification.Builder(this,CHANNEL_ID)
            .SetContentTitle("com.olo.curbsidetest")
            .SetContentText("Notification Object for Foreground Service")
            .SetSmallIcon(Resource.Drawable.notification_bg_low)
            .SetOngoing(true)
            .Build();

        }
    }
}

