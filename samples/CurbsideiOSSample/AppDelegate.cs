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
            // TokyoJoe's Token
            var sdkSession = CSUserSession.WithUsageToken("e445b0b497694445084e2c91624092529938d11fe91ad546bdee1b377d51b850", null);
            sdkSession.Application(application, launchOptions ?? new NSDictionary());

            CSUserSession.CurrentSession.TrackingIdentifier = "test";

            _curbsideTrackerDelegate.TrackerEncounteredError += OnTrackerEncounteredError;
            CSUserSession.CurrentSession.Delegate = _curbsideTrackerDelegate;

            CSUserInfo userInfo = new CSUserInfo()
            {
                EmailAddress = "test@test.com",
                FullName = "test",
                SMSNumber = "66666666",
                VehicleMake = "BMW", 
                VehicleModel = "530",
                VehicleLicensePlate = "JKL89"
            };
            CSUserSession.CurrentSession.UserInfo = userInfo;

            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");
            CSUserSession.CurrentSession.StartTripToSiteWithIdentifier("tokyojoes_0", r);

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