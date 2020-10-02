using Android.App;
using Android.Widget;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using System.Linq;
using System;
using Android.Gms.Common;
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

            // Rakuten Ready Demo Token
            Curbside.CSUserSession.Init(this, new Curbside.TokenCurbsideCredentialProvider("dfbe9fb7ad7c659ebd256626a325daca7c6045c8cd3d8d10fcc644b511d82d63"));
			Curbside.CSUserSession.Instance.RegisterTrackingIdentifier("hello12345");
            Curbside.CSUserInfo userInfo = new Curbside.CSUserInfo("John Smith", "john.smith@example.com", "8883308304", "NAP789", "Tesla", "Model S");
            Curbside.CSUserSession.Instance.SetUserInfo(userInfo);
           
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");
            if(notification !=null)
            {
                Curbside.CSUserSession.Instance.SetNotificationForForegroundService(notification);
            }
            Curbside.CSUserSession.Instance.StartTripToSiteWithIdentifier("rakutenreadydemo_100", r);
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
            .SetContentTitle("com.curbside.test")
            .SetContentText("Notification Object for Foreground Service")
            .SetSmallIcon(Resource.Drawable.notification_bg_low)
            .SetOngoing(true)
            .Build();

        }
    }
}

