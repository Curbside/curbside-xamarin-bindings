using System;
using Curbside;
using Foundation;
using UIKit;

namespace CurbsideiOSSample
{
	[Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate, ICSUserSessionDelegate
	{
		public override UIWindow Window { get; set; }
        private static CurbsideTrackerDelegate _curbsideTrackerDelegate = new CurbsideTrackerDelegate();

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
            // Rakuten Ready Demo Token
            var sdkSession = CSUserSession.WithUsageToken("dfbe9fb7ad7c659ebd256626a325daca7c6045c8cd3d8d10fcc644b511d82d63", null);
            sdkSession.Application(application, launchOptions ?? new NSDictionary());

            CSUserSession.CurrentSession.TrackingIdentifier = "hello12345";

            _curbsideTrackerDelegate.TrackerEncounteredError += OnTrackerEncounteredError;
            CSUserSession.CurrentSession.Delegate = _curbsideTrackerDelegate;

            CSUserInfo userInfo = new CSUserInfo()
            {
                EmailAddress = "john.smith@example.com",
                FullName = "John Smith",
                SMSNumber = "8883308304",
                VehicleMake = "Tesla", 
                VehicleModel = "Model S",
                VehicleLicensePlate = "NAP789"
            };
            CSUserSession.CurrentSession.UserInfo = userInfo;

            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");
            CSUserSession.CurrentSession.StartTripToSiteWithIdentifier("rakutenreadydemo_100", r);

            CSUserSession.CurrentSession.UpdateTripsFromServer();
          
			return true;
		}

        void OnTrackerEncounteredError(object sender, NSError error)
        {
            System.Diagnostics.Debug.WriteLine(error.Description);
        }

        private class CurbsideTrackerDelegate : CSUserSessionDelegate
        {
            public event EventHandler<NSError> TrackerEncounteredError;

            public override void  EncounteredError(CSUserSession session, NSError error, CSUserSessionAction trackerAction)
            {
                TrackerEncounteredError?.Invoke(this, error);
            }

            public override void CanNotifyAssociateAtSite(CSUserSession session, CSUserSite site)
            {
                System.Diagnostics.Debug.WriteLine(site.Description.ToString());

            }

            public override void ChangedState(CSSession session, CSSessionState newState)
            {
                System.Diagnostics.Debug.WriteLine(newState.ToString());

            }
            public override void UpdatedTrackedSites(CSSession session, NSSet<CSSite> trackedSites)
            {
            }
        }
	}
}