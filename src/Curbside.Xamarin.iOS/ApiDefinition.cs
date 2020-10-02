using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using UIKit;
using WebKit;

namespace Curbside
{

    [Static]
    partial interface Constants
    {
        // extern NSString *const CSTripTypeCarryOut;
        [Field("CSTripTypeCarryOut", "__Internal")]
        NSString CSTripTypeCarryOut { get; }

        // extern NSString *const CSTripTypeDriveThru;
        [Field("CSTripTypeDriveThru", "__Internal")]
        NSString CSTripTypeDriveThru { get; }

        // extern NSString *const CSTripTypeCurbside;
        [Field("CSTripTypeCurbside", "__Internal")]
        NSString CSTripTypeCurbside { get; }

        // extern NSString *const CSTripTypeDineIn;
        [Field("CSTripTypeDineIn", "__Internal")]
        NSString CSTripTypeDineIn { get; }

		// extern NSString * _Nonnull kCSUserSessionReadyNotificationName;
		[Field ("kCSUserSessionReadyNotificationName", "__Internal")]
		NSString kCSUserSessionReadyNotificationName { get; }
    }

    // @interface CSSession : NSObject
    [Protocol]
	[BaseType(typeof(NSObject))]
	interface CSSession
	{	
        // @property (nonatomic, strong) NSString * trackingIdentifier;
		[NullAllowed, Export("trackingIdentifier", ArgumentSemantic.Strong)]
		string TrackingIdentifier { get; set; }

		// @property (nonatomic, strong) CSUserInfo * userInfo;
		[Export("userInfo", ArgumentSemantic.Strong)]
        CSUserInfo UserInfo { get; set; }

        // @property (readonly, nonatomic) CSSessionState sessionState;
        [Export("sessionState")]
		CSSessionState SessionState { get; }

		[Wrap("WeakDelegate")]
		[NullAllowed]
		CSSessionDelegate Delegate { get; set; }

		// @property (nonatomic, strong) id<CSSessionDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Strong)]
		NSObject WeakDelegate { get; set; }

		// -(void)application:(UIApplication * _Nonnull)application didFinishLaunchingWithOptions:(NSDictionary * _Nullable)launchOptions;
		[Export ("application:didFinishLaunchingWithOptions:")]
		void Application (UIApplication application, [NullAllowed] NSDictionary launchOptions);

		// -(BOOL)addEvent:(CSEvent)event forTrackToken:(NSString * _Nullable)trackToken siteIdentifier:(NSString * _Nullable)siteIdentifier properties:(NSDictionary * _Nullable)properties;
		[Export ("addEvent:forTrackToken:siteIdentifier:properties:")]
		bool AddEvent (CSEvent @event, [NullAllowed] string trackToken, [NullAllowed] string siteIdentifier, [NullAllowed] NSDictionary properties);
	}

	// @protocol CSSessionDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface CSSessionDelegate
	{
		// @optional -(void)session:(CSSession *)session changedState:(CSSessionState)newState;
		[Export("session:changedState:")]
		void ChangedState(CSSession session, CSSessionState newState);
	}
 
	// @interface CSSite : NSObject
    [Protocol]
	[BaseType(typeof(NSObject))]
	interface CSSite : INativeObject
	{
		// @property (readonly, nonatomic) NSString * siteIdentifier;
		[Export("siteIdentifier")]
		string SiteIdentifier { get; }

		// -(id)initWithSiteIdentifier:(NSString *)siteIdentifier;
		[Export("initWithSiteIdentifier:")]
		IntPtr Constructor(string siteIdentifier);

		// @property (readonly, nonatomic) NSArray<CSTripInfo *> * _Nullable tripInfos;
		[NullAllowed, Export ("tripInfos")]
		CSTripInfo[] TripInfos { get; }	
	}

	// @interface CSUserSite : CSSite
    [Protocol]
	[BaseType(typeof(CSSite))]
	interface CSUserSite
	{
		// @property (readonly, nonatomic) CSUserStatus userStatus;
		[Export("userStatus")]
		CSUserStatus UserStatus { get; }

		// @property (readonly, nonatomic) int distanceFromSite;
		[Export("distanceFromSite")]
		int DistanceFromSite { get; }

		// @property (readonly, nonatomic) int estimatedTimeOfArrival;
		[Export ("estimatedTimeOfArrival")]
		int EstimatedTimeOfArrival { get; }

	}
       
	// @interface CSUserSession : CSSession
	[BaseType(typeof(CSSession))]
    interface CSUserSession
	{
		// @property (readonly, nonatomic) NSSet<CSSite *> * _Nonnull sitesToNotifyMonitoringSessionUserOfArrival;
		[Export ("sitesToNotifyMonitoringSessionUserOfArrival")]
		NSSet<CSSite> SitesToNotifyMonitoringSessionUserOfArrival { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		CSUserSessionDelegate Delegate { get; set; }

		// @property (nonatomic, strong) id<CSUserSessionDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Strong)]
		new NSObject WeakDelegate { get; set; }

		// @property (readonly, nonatomic) NSSet<CSSite *> * _Nonnull trackedSites;
		[Export ("trackedSites")]
		NSSet<CSSite> TrackedSites { get; }

		// +(instancetype _Nonnull)createSessionWithUsageToken:(NSString * _Nonnull)usageToken delegate:(id<CSUserSessionDelegate> _Nullable)delegate;
		[Static]
		[Export ("createSessionWithUsageToken:delegate:")]
		CSUserSession createSessionWithUsageToken (string usageToken, [NullAllowed] CSUserSessionDelegate @delegate);

		// +(instancetype _Nonnull)currentSession;
		[Static]
		[Export ("currentSession")]
		CSUserSession CurrentSession { get; }

		// -(void)notifyMonitoringSessionUserOfArrivalAtSite:(CSSite * _Nonnull)site;
		[Export ("notifyMonitoringSessionUserOfArrivalAtSite:")]
		void NotifyMonitoringSessionUserOfArrivalAtSite (CSSite site);

		// -(void)startTripToSiteWithIdentifier:(NSString * _Nonnull)siteID trackToken:(NSString * _Nonnull)trackToken;
		[Export ("startTripToSiteWithIdentifier:trackToken:")]
		void StartTripToSiteWithIdentifier (string siteID, string trackToken);

		// -(void)startUserOnTheirWayTripToSiteWithIdentifier:(NSString * _Nonnull)siteID trackToken:(NSString * _Nonnull)trackToken;
		[Export ("startUserOnTheirWayTripToSiteWithIdentifier:trackToken:")]
		void StartUserOnTheirWayTripToSiteWithIdentifier (string siteID, string trackToken);

		// -(void)startTripToSiteWithIdentifier:(NSString * _Nonnull)siteID trackToken:(NSString * _Nonnull)trackToken etaFromDate:(NSDate * _Nonnull)fromDate toDate:(NSDate * _Nullable)toDate;
		[Export ("startTripToSiteWithIdentifier:trackToken:etaFromDate:toDate:")]
		void StartTripToSiteWithIdentifier (string siteID, string trackToken, NSDate fromDate, [NullAllowed] NSDate toDate);

		// -(void)startTripToSiteWithIdentifier:(NSString * _Nonnull)siteID trackToken:(NSString * _Nonnull)trackToken tripType:(NSString * _Nonnull)tripType;
		[Export ("startTripToSiteWithIdentifier:trackToken:tripType:")]
		void StartTripToSiteWithIdentifier (string siteID, string trackToken, string tripType);

		// -(void)startUserOnTheirWayTripToSiteWithIdentifier:(NSString * _Nonnull)siteID trackToken:(NSString * _Nonnull)trackToken tripType:(NSString * _Nonnull)tripType;
		[Export ("startUserOnTheirWayTripToSiteWithIdentifier:trackToken:tripType:")]
		void StartUserOnTheirWayTripToSiteWithIdentifier (string siteID, string trackToken, string tripType);

		// -(void)startTripToSiteWithIdentifier:(NSString * _Nonnull)siteID trackToken:(NSString * _Nonnull)trackToken etaFromDate:(NSDate * _Nonnull)fromDate toDate:(NSDate * _Nullable)toDate tripType:(NSString * _Nonnull)tripType;
		[Export ("startTripToSiteWithIdentifier:trackToken:etaFromDate:toDate:tripType:")]
		void StartTripToSiteWithIdentifier (string siteID, string trackToken, NSDate fromDate, [NullAllowed] NSDate toDate, string tripType);

		// -(void)updateAllTripsWithUserOnTheirWay:(BOOL)userOnTheirWay;
		[Export ("updateAllTripsWithUserOnTheirWay:")]
		void UpdateAllTripsWithUserOnTheirWay (bool userOnTheirWay);

		// -(void)completeTripToSiteWithIdentifier:(NSString * _Nonnull)siteID trackToken:(NSString * _Nullable)trackToken;
		[Export ("completeTripToSiteWithIdentifier:trackToken:")]
		void CompleteTripToSiteWithIdentifier (string siteID, [NullAllowed] string trackToken);

		// -(void)completeAllTrips;
		[Export ("completeAllTrips")]
		void CompleteAllTrips ();

		// -(void)cancelTripToSiteWithIdentifier:(NSString * _Nonnull)siteID trackToken:(NSString * _Nullable)trackToken;
		[Export ("cancelTripToSiteWithIdentifier:trackToken:")]
		void CancelTripToSiteWithIdentifier (string siteID, [NullAllowed] string trackToken);

		// -(void)cancelAllTrips;
		[Export ("cancelAllTrips")]
		void CancelAllTrips ();

		// -(void)updateTripsFromServer;
		[Export ("updateTripsFromServer")]
		void UpdateTripsFromServer ();

		// -(void)etaToSiteWithIdentifier:(NSString * _Nonnull)siteID fromLocation:(CLLocation * _Nonnull)fromLocation transportationMode:(CSTransportationMode)transportationMode completionHandler:(void (^ _Nonnull)(int))completionHandler;
		[Export ("etaToSiteWithIdentifier:fromLocation:transportationMode:completionHandler:")]
		void EtaToSiteWithIdentifier (string siteID, CLLocation fromLocation, CSTransportationMode transportationMode, Action<int> completionHandler);
	}

	// @protocol CSUserSessionDelegate <CSSessionDelegate>
	[Protocol, Model]
	[BaseType(typeof(CSSessionDelegate))]
	interface CSUserSessionDelegate
	{
		// @optional -(void)session:(CSUserSession * _Nonnull)session canNotifyMonitoringSessionUserAtSite:(CSSite * _Nonnull)site;
		[Export ("session:canNotifyMonitoringSessionUserAtSite:")]
		void CanNotifyMonitoringSessionUserAtSite (CSUserSession session, CSSite site);

		// @optional -(void)session:(CSUserSession * _Nonnull)session userApproachingSite:(CSSite * _Nonnull)site;
		[Export ("session:userApproachingSite:")]
		void UserApproachingSite (CSUserSession session, CSSite site);

		// @optional -(void)session:(CSUserSession * _Nonnull)session userArrivedAtSite:(CSSite * _Nonnull)site;
		[Export ("session:userArrivedAtSite:")]
		void UserArrivedAtSite (CSUserSession session, CSSite site);

		// @optional -(void)session:(CSUserSession * _Nonnull)session encounteredError:(NSError * _Nonnull)error forOperation:(CSUserSessionAction)customerSessionAction;
		[Export ("session:encounteredError:forOperation:")]
		void EncounteredError (CSUserSession session, NSError error, CSUserSessionAction customerSessionAction);

		// @optional -(void)session:(CSUserSession * _Nonnull)session updatedTrackedSites:(NSSet<CSSite *> * _Nonnull)trackedSites;
		[Export ("session:updatedTrackedSites:")]
		void UpdatedTrackedSites (CSUserSession session, NSSet<CSSite> trackedSites);

		// @optional -(void)session:(CSUserSession * _Nonnull)session tripStartedForSite:(CSSite * _Nonnull)site;
		[Export ("session:tripStartedForSite:")]
		void TripStartedForSite (CSUserSession session, CSSite site);
		
    }

    interface ICSUserSessionDelegate { }
    
	// @interface CSUserInfo : NSObject
	[BaseType(typeof(NSObject))]
	interface CSUserInfo : INativeObject
	{
		// @property (nonatomic, strong) NSString * fullName;
		[NullAllowed, Export("fullName", ArgumentSemantic.Strong)]
		string FullName { get; set; }

        // @property (nonatomic, strong) NSString * emailAddress;
		[NullAllowed, Export("emailAddress", ArgumentSemantic.Strong)]
        string EmailAddress { get; set; }

		// @property (nonatomic, strong) NSString * smsNumber;
		[NullAllowed, Export("smsNumber", ArgumentSemantic.Strong)]
        string SMSNumber { get; set; }

		// @property (nonatomic, strong) NSString * vehicleMake;
		[NullAllowed, Export("vehicleMake", ArgumentSemantic.Strong)]
        string VehicleMake { get; set; }

		// @property (nonatomic, strong) NSString * vehicleModel;
		[NullAllowed, Export("vehicleModel", ArgumentSemantic.Strong)]
        string VehicleModel { get; set; }

		// @property (nonatomic, strong) NSString * vehicleLicensePlate;
		[NullAllowed, Export("vehicleLicensePlate", ArgumentSemantic.Strong)]
        string VehicleLicensePlate { get; set; }
	}

	// @interface CSMonitoringNotification : NSObject
	[BaseType (typeof(NSObject))]
	interface CSMonitoringNotification
	{
		// @property (readonly, nonatomic) BOOL monitoringSessionUserAcknowledged;
		[Export ("monitoringSessionUserAcknowledged")]
		bool MonitoringSessionUserAcknowledged { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable monitoringSessionUserAcknowledgeTimestamp;
		[NullAllowed, Export ("monitoringSessionUserAcknowledgeTimestamp")]
		NSDate MonitoringSessionUserAcknowledgeTimestamp { get; }

		// @property (readonly, nonatomic) int monitoringSessionUserEstimatedTimeOfArrival;
		[Export ("monitoringSessionUserEstimatedTimeOfArrival")]
		int MonitoringSessionUserEstimatedTimeOfArrival { get; }

		// @property (readonly, nonatomic) NSString * _Nullable monitoringSessionUserMessage;
		[NullAllowed, Export ("monitoringSessionUserMessage")]
		string MonitoringSessionUserMessage { get; }
	}

	// typedef void (^CSUserStatusesUpdatedHandler)(NSArray<CSUserStatusUpdate *> * _Nonnull);
	delegate void CSUserStatusesUpdatedHandler (CSUserStatusUpdate[] arg0);

	[Protocol, Model]
    [BaseType(typeof(CSSessionDelegate))]
    interface CSMonitoringSessionDelegate
	{
		// @required -(void)session:(CSMonitoringSession * _Nonnull)session encounteredError:(NSError * _Nonnull)error;
		[Export ("session:encounteredError:")]
		void EncounteredError (CSMonitoringSession session, NSError error);
	}

	interface ICSSessionDelegate { }

	// @interface CSMonitoringSession : CSSession
    [Protocol]
	[BaseType(typeof(CSSession))]
	interface CSMonitoringSession
	{
		// @property (readonly, nonatomic) CSSite * _Nullable arrivalSite;
		[NullAllowed, Export ("arrivalSite")]
		CSSite ArrivalSite { get; }

		// @property (copy, nonatomic) CSUserStatusesUpdatedHandler _Nullable statusesUpdatedHandler;
		[NullAllowed, Export ("statusesUpdatedHandler", ArgumentSemantic.Copy)]
		CSUserStatusesUpdatedHandler StatusesUpdatedHandler { get; set; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		CSMonitoringSessionDelegate Delegate { get; set; }

		// @property (nonatomic, strong) id<CSMonitoringSessionDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Strong)]
		NSObject WeakDelegate { get; set; }

		// +(instancetype _Nonnull)createSessionWithAPIKey:(NSString * _Nonnull)apiKey secret:(NSString * _Nonnull)secret delegate:(id<CSMonitoringSessionDelegate> _Nullable)delegate;
		[Static]
		[Export ("createSessionWithAPIKey:secret:delegate:")]
		CSMonitoringSession CreateSessionWithAPIKey (string apiKey, string secret, [NullAllowed] CSMonitoringSessionDelegate @delegate);

		// +(instancetype _Nonnull)currentSession;
		[Static]
		[Export ("currentSession")]
		CSMonitoringSession CurrentSession ();

		// -(void)startMonitoringArrivalsToSiteWithIdentifier:(NSString * _Nonnull)siteID;
		[Export ("startMonitoringArrivalsToSiteWithIdentifier:")]
		void StartMonitoringArrivalsToSiteWithIdentifier (string siteID);

		// -(void)stopMonitoringArrivals;
		[Export ("stopMonitoringArrivals")]
		void StopMonitoringArrivals ();

		// -(void)completeTripForTrackingIdentifier:(NSString * _Nonnull)trackingIdentifier trackTokens:(NSArray<NSString *> * _Nullable)trackTokens;
		[Export ("completeTripForTrackingIdentifier:trackTokens:")]
		void CompleteTripForTrackingIdentifier (string trackingIdentifier, [NullAllowed] string[] trackTokens);

		// -(void)cancelTripForTrackingIdentifier:(NSString * _Nonnull)trackingIdentifier trackTokens:(NSArray<NSString *> * _Nullable)trackTokens;
		[Export ("cancelTripForTrackingIdentifier:trackTokens:")]
		void CancelTripForTrackingIdentifier (string trackingIdentifier, [NullAllowed] string[] trackTokens);
	}
	
    // @interface CSTripInfo : NSObject
    [Protocol]
    [BaseType(typeof(NSObject))]
    interface CSTripInfo
    {
        // @property(nonatomic, readonly)NSString* trackToken;
        [Export("trackToken")]
        string TrackToken { get; }

        // @property(nonatomic, readonly)NSDate* startDate;
        [Export("startDate")]
        NSDate StartDate { get; }

        // @property(nonatomic, readonly)NSString* destID;
        [Export("destID")]
        string DestID { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable etaFromDate;
		[NullAllowed, Export ("etaFromDate")]
		NSDate EtaFromDate { get; }

		// @property (readonly, nonatomic) NSDate * _Nullable etaToDate;
		[NullAllowed, Export ("etaToDate")]
		NSDate EtaToDate { get; }
    }

	// typedef void (^CSUserAcknowledgeStatus)(BOOL);
	delegate void CSUserAcknowledgeStatus(bool arg0);

	// @interface CSUserStatusUpdate : NSObject
    [Protocol]
	[BaseType(typeof(NSObject))]
	interface CSUserStatusUpdate
	{
		// @property (readonly, nonatomic) NSString * trackingIdentifier;
		[Export("trackingIdentifier")]
		string TrackingIdentifier { get; }

		// @property (readonly, nonatomic) CLLocation * location;
		[Export("location")]
		CLLocation Location { get; }

		// @property (readonly, nonatomic) NSDate * lastUpdateTimestamp;
		[Export("lastUpdateTimestamp")]
		NSDate LastUpdateTimestamp { get; }

		// @property (readonly, nonatomic) CSUserStatus userStatus;
		[Export("userStatus")]
		CSUserStatus UserStatus { get; }

        // @property (readonly, nonatomic) CSUserInfo * _Nullable userInfo;
        [Export("userInfo")]
        CSUserInfo UserInfo { get; }

        // @property (readonly, nonatomic) BOOL acknowledgedUser;
        [Export("acknowledgedUser")]
		bool AcknowledgedUser { get; }

		// @property (readonly, nonatomic) int estimatedTimeOfArrival;
		[Export("estimatedTimeOfArrival")]
		int EstimatedTimeOfArrival { get; }

		// @property (readonly, nonatomic) int distanceFromSite;
		[Export("distanceFromSite")]
		int DistanceFromSite { get; }

        // @property (nonatomic, readonly)NSArray<CSTripInfo *> *tripInfos;
        [Export("tripInfos")]
        CSTripInfo[] TripInfos { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull arrivalZone;
		[Export ("arrivalZone")]
		string ArrivalZone { get; }

		// @property (readonly, nonatomic) CSMotionActivity motionActivity;
		[Export ("motionActivity")]
		CSMotionActivity MotionActivity { get; }

        // -(void)monitoringSessionUserAcknowledgesUserWithMessage:(NSString * _Nonnull)ackMessage handler:(CSUserAcknowledgeStatus _Nonnull)acknowledgeStatusHandler;
        [Export("monitoringSessionUserAcknowledgesUserWithMessage:handler:")]
        void MonitoringSessionUserAcknowledgesUserWithMessage(string ackMessage, CSUserAcknowledgeStatus acknowledgeStatusHandler);

        // @property (readonly, nonatomic) NSDate * _Nullable monitoringSessionUserAcknowledgedTimestamp;
        [Export("monitoringSessionUserAcknowledgedTimestamp")]
        NSDate MonitoringSessionUserAcknowledgedTimestamp { get; }

        // @property (readonly, nonatomic) NSString * _Nullable monitoringSessionUserTrackingIdentifier;
        [Export("monitoringSessionUserTrackingIdentifier")]
        string MonitoringSessionUserTrackingIdentifier { get; }
    }

	// @interface CSWKWebView : WKWebView
    [Protocol]
	[BaseType(typeof(WKWebView))]
	interface CSWKWebView
	{		
		// @property (nonatomic, strong) NSString * webViewURLString;
		[Export ("webViewURLString", ArgumentSemantic.Strong)]
		string WebViewURLString { get; set; }
		
		// @property (nonatomic, strong) NSString * authenticationToken;
		[Export("authenticationToken", ArgumentSemantic.Strong)]
		string AuthenticationToken { get; set; }

		[Wrap("WeakLoginDelegate")]
		CSWKWebViewLoginDelegate LoginDelegate { get; set; }

		// @property (assign, nonatomic) id<CSWKWebViewLoginDelegate> loginDelegate;
		[NullAllowed, Export("loginDelegate", ArgumentSemantic.Assign)]
		NSObject WeakLoginDelegate { get; set; }

		[Wrap("WeakAnalyticsDelegate")]
		CSWKWebViewAnalyticsDelegate AnalyticsDelegate { get; set; }

		// @property (assign, nonatomic) id<CSWKWebViewAnalyticsDelegate> analyticsDelegate;
		[NullAllowed, Export("analyticsDelegate", ArgumentSemantic.Assign)]
		NSObject WeakAnalyticsDelegate { get; set; }

		// @property (nonatomic, strong) NSDictionary * requestContext;
		[Export("requestContext", ArgumentSemantic.Strong)]
		NSDictionary RequestContext { get; set; }

		// @property (nonatomic, strong) NSString * crbsWebAction;
		[Export("crbsWebAction", ArgumentSemantic.Strong)]
		string CrbsWebAction { get; set; }

		// @property (nonatomic, strong) CLLocation * userLocation;
		[Export ("userLocation", ArgumentSemantic.Strong)]
		CLLocation UserLocation { get; set; }

		// @property (nonatomic, strong) NSString * userLocationName;
		[Export ("userLocationName", ArgumentSemantic.Strong)]
		string UserLocationName { get; set; }

		// @property (nonatomic) BOOL developmentMode;
		[Export("developmentMode")]
		bool DevelopmentMode { get; set; }

		// -(void)logout;
		[Export("logout")]
		void Logout();

		// -(void)loadPage;
		[Export("loadPage")]
		void LoadPage();
	}

	// @protocol CSWKWebViewLoginDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface CSWKWebViewLoginDelegate
	{
		// @required -(void)csWKWebViewNeedsAuthenticationToken:(CSWKWebView *)webView;
		[Abstract]
		[Export("csWKWebViewNeedsAuthenticationToken:")]
		void CsWKWebViewNeedsAuthenticationToken(CSWKWebView webView);

		// @required -(void)csWKWebViewUserLoggedOut:(CSWKWebView *)webView;
		[Abstract]
		[Export("csWKWebViewUserLoggedOut:")]
		void CsWKWebViewUserLoggedOut(CSWKWebView webView);

		// @required -(void)csWKWebViewTokenValidationFailed:(CSWKWebView *)webView error:(id)errorCode;
		[Abstract]
		[Export("csWKWebViewTokenValidationFailed:error:")]
		void CsWKWebViewTokenValidationFailed(CSWKWebView webView, NSObject errorCode);
	}

	// @protocol CSWKWebViewAnalyticsDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface CSWKWebViewAnalyticsDelegate
	{
		// @required -(void)csWKWebView:(CSWKWebView *)webView taggedEvent:(NSString *)eventName properties:(NSDictionary *)eventProperties;
		[Abstract]
		[Export("csWKWebView:taggedEvent:properties:")]
		void TaggedEvent(CSWKWebView webView, string eventName, NSDictionary eventProperties);

		// @required -(void)csWKWebView:(CSWKWebView *)webView taggedScreen:(NSString *)screenName;
		[Abstract]
		[Export("csWKWebView:taggedScreen:")]
		void TaggedScreen(CSWKWebView webView, string screenName);
	}
}