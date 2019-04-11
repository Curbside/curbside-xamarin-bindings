using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using UIKit;
using WebKit;

namespace Curbside
{

	// @interface CSSession : NSObject
    [Protocol]
	[BaseType(typeof(NSObject))]
	interface CSSession
	{
		// @property (nonatomic, strong) NSString * trackingIdentifier;
		[Export("trackingIdentifier", ArgumentSemantic.Strong)]
		string TrackingIdentifier { get; set; }

		// @property (nonatomic, strong) CSUserInfo * userInfo;
        [Export("userInfo", ArgumentSemantic.Strong)]
        CSUserInfo UserInfo { get; set; }

        // @property (readonly, nonatomic) CSSessionState sessionState;
        [Export("sessionState")]
		CSSessionState SessionState { get; }

		[Wrap("WeakDelegate")]
		CSSessionDelegate Delegate { get; set; }

		// @property (nonatomic, strong) id<CSSessionDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Strong)]
		NSObject WeakDelegate { get; set; }

		// -(void)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions;
		[Export("application:didFinishLaunchingWithOptions:")]
		void Application(UIApplication application, NSDictionary launchOptions);
        
		// -(BOOL)addEvent:(CSEvent)event forTrackToken:(NSString *)trackToken siteIdentifier:(NSString *)siteIdentifier properties:(NSDictionary *)properties;
		[Export("addEvent:forTrackToken:siteIdentifier:properties:")]
		bool AddEvent(CSEvent @event, string trackToken, string siteIdentifier, NSDictionary properties);
	}

    [Protocol, Model]
    [BaseType(typeof(CSSessionDelegate))]
    interface CSMonitoringSessionDelegate
	{
		[Export("encounteredError:")]
		void EncounteredError(NSError error);
	}

	interface ICSSessionDelegate { }

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
	}

	// @interface CSUserSite : CSSite
    [Protocol]
	[BaseType(typeof(CSSite))]
	interface CSUserSite
	{
		// @property (readonly, nonatomic) NSArray * trackTokens;
		[Export("trackTokens")]
		string[] TrackTokens { get; }

		// @property (readonly, nonatomic) CSUserStatus userStatus;
		[Export("userStatus")]
		CSUserStatus UserStatus { get; }

		// @property (readonly, nonatomic) int distanceFromSite;
		[Export("distanceFromSite")]
		int DistanceFromSite { get; }

		// -(id)initWithSiteIdentifier:(NSString *)siteIdentifier trackTokens:(NSArray *)trackTokens;
		[Export("initWithSiteIdentifier:trackTokens:")]
		IntPtr Constructor(string siteIdentifier, string[] trackTokens);
	}
       
	// @interface CSUserSession : CSSession
	[BaseType(typeof(CSSession))]
    interface CSUserSession: CSSession
	{
		// @property (readonly, nonatomic) NSSet * trackedSites;
		[Export("trackedSites")]
		NSSet<CSSite> TrackedSites { get; } //

		[Wrap("WeakDelegate")]
		new CSUserSessionDelegate Delegate { get; set; }

		// @property (readonly, nonatomic) CSSite * sitesToNotifyMonitoringSessionUserOfArrival;
		[Export("sitesToNotifyMonitoringSessionUserOfArrival")]
		NSSet<CSSite> SitesToNotifyMonitoringSessionUserOfArrival { get; } 
      
        // +(instancetype)createSessionWithUsageToken:(NSString *)usageToken delegate:(id<CSMobileSessionDelegate>)delegate;
        [Static]
        [Export("createSessionWithUsageToken:delegate:")]
		CSUserSession WithUsageToken(string usageToken, [NullAllowed]ICSUserSessionDelegate @delegate); 

        // +(instancetype)currentSession;
        [Static]
        [Export("currentSession")]
        CSUserSession CurrentSession { get; } //

		// @property (assign, nonatomic) id<CSUserSessionDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
		new NSObject WeakDelegate { get; set; }

		// -(void)updateTripsFromServer;
		[Export("updateTripsFromServer")]
		void UpdateTripsFromServer();

        // - (void) startTripToSiteWithIdentifier:(NSString*) trackingIdentifier trackToken:(nullable NSString*>) trackToken;
		[Export("startTripToSiteWithIdentifier:trackToken:")]
		void StartTripToSiteWithIdentifier(string siteId, string trackToken);

        //- (void)startTripToSiteWithIdentifier:(NSString *)siteID trackToken:(NSString *)trackToken etaFromDate:(NSDate *)fromDate toDate:(nullable NSDate *)toDate;
        [Export("startTripToSiteWithIdentifier:trackToken:etaFromDate:toDate:")]
        void StartTripToSiteWithIdentifierWithEta(string siteId, string trackToken, NSDate fromDate, [NullAllowed]NSDate toDate);

        // - (void) completeTripToSiteWithIdentifier:(NSString*) trackingIdentifier trackToken:(nullable NSString*>) trackToken;
		[Export("completeTripToSiteWithIdentifier:trackToken:")]
		void CompleteTripToSiteWithIdentifier(string siteId, string trackToken);

        // - (void) cancelTripToSiteWithIdentifier:(NSString*) trackingIdentifier trackToken:(nullable NSString*>) trackToken;
		[Export("cancelTripToSiteWithIdentifier:trackToken:")]
        void CancelTripToSiteWithIdentifier(string siteId, string trackToken);

        // -(void)completeAllTrips;
        [Export("completeAllTrips")]
		void CompleteAllTrips();

		// -(void)cancelAllTrips;
		[Export("cancelAllTrips")]
        void CancelAllTrips();

		//[[CSUserSession currentSession] notifyMonitoringSessionUserOfArrivalAtSite:site];
		// -(void)notifyMonitoringSessionUserOfArrivalAtSite:(CSSite *)site;
		[Export("notifyMonitoringSessionUserOfArrivalAtSite:")]
		void NotifyMonitoringSessionUserOfArrivalAtSite(CSSite site);

        // (void)etaToSiteWithIdentifier:(nonnull NSString *)siteID fromLocation:(nonnull CLLocation *)fromLocation transportationMode:(CSTransportationMode) transportationMode completionHandler:(nonnull void (^)(int))completionHandler
        [Export("etaToSiteWithIdentifier:fromLocation:transportationMode:completionHandler:")]
        void EtaToSiteWithIdentifier(string siteId, CLLocationManager fromLocation, CSTransportationMode transportationMode, Action<int> completionHandler);
    }

	// @protocol CSUserSessionDelegate <CSSessionDelegate>
	[Protocol, Model]
	[BaseType(typeof(CSSessionDelegate))]
	interface CSUserSessionDelegate
	{
		// @optional -(void)tracker:(CSTracker *)tracker userApproachingSite:(CSUserSite *)site;
        [Export("session:userApproachingSite:")]
        void UserApproachingSite(CSUserSession session, CSUserSite site);

		// @optional -(void)tracker:(CSTracker *)tracker userArrivedAtSite:(CSUserSite *)site;
        [Export("session:userArrivedAtSite:")]
        void UserArrivedAtSite(CSUserSession session, CSUserSite site);

        // @optional - (void)tracker:(CSTracker*) tracker encounteredError:(NSError*) error forOperation:(CSTrackerAction) trackerAction;
        [Export("session:encounteredError:forOperation:")]
        void EncounteredError(CSUserSession session, NSError error, CSUserSessionAction trackerAction);

        //- (void)session:(CSUserSession *)session updatedTrackedSites:(NSSet<CSSite *> *)trackedSites;
        [Export("session:updatedTrackedSites:")]
        void UpdatedTrackedSites(CSSession session, NSSet<CSSite> trackedSites);

        // @optional -(void)session:(CSMobileSession *)session canNotifyAssociateAtSite:(CSSite *)site;
        [Export("session:canNotifyAssociateAtSite:")]
        void CanNotifyAssociateAtSite(CSUserSession session, CSUserSite site);

        // -(void)session:(nonnull CSUserSession *)session tripStartedForSite:(nonnull CSSite *)site;
        [Export("session:tripStartedForSite:")]
        void TripStartedForSite(CSUserSession session, CSSite site);
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

	// @interface CSSiteOpsNotification : NSObject
    [Protocol]
	[BaseType(typeof(NSObject))]
	interface CSSiteOpsNotification
	{
		// @property (readonly, nonatomic) BOOL opsAcknowledged;
		[Export("opsAcknowledged")]
		bool OpsAcknowledged { get; }
        
		// @property (readonly, nonatomic) NSDate * opsAcknowledgeTimestamp;
		[Export("opsAcknowledgeTimestamp")]
		NSDate OpsAcknowledgeTimestamp { get; }

		// @property (readonly, nonatomic) int opsEstimatedTimeOfArrival;
		[Export("opsEstimatedTimeOfArrival")]
		int OpsEstimatedTimeOfArrival { get; }
        
		// @property (readonly, nonatomic) NSString * opsMessage;
		[Export("opsMessage")]
		string OpsMessage { get; }
	}

	// @interface CSMonitoringSession : CSSession

    [Protocol]
	[BaseType(typeof(CSSession))]
	interface CSMonitoringSession
	{
		// +(instancetype)createSessionWithAPIKey:(NSString *)apiKey secret:(NSString *)secret delegate:(id<CSSessionDelegate>)delegate;
		[Static]
		[Export("createSessionWithAPIKey:secret:delegate:")]
		CSMonitoringSession CreateSessionWithAPIKey(string apiKey, string secret, CSMonitoringSessionDelegate @delegate);

		// +(instancetype)currentSession;
		[Static]
		[Export("currentSession")]
		CSMonitoringSession CurrentSession { get; }
		// @property (readonly, nonatomic) CSSite * arrivalSite;
        [Export("arrivalSite")]
        CSSite ArrivalSite { get; }

        // @property (copy, nonatomic) CSUserLocationUpdatesAvailableHandler statusesUpdatedHandler;
        [Export("statusesUpdatedHandler", ArgumentSemantic.Copy)]
        CSUserStatusUpdatesAvailableHandler StatusesUpdatedHandler { get; set; }

        [Wrap("WeakDelegate")]
		CSMonitoringSessionDelegate Delegate { get; set; }

        // @property (nonatomic, assign) CSSiteArrivalTrackerDelegate delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // +(instancetype)sharedArrivalTracker;
        [Static]
        [Export("sharedArrivalTracker")]
		CSMonitoringSession SharedArrivalTracker();

        // -(void)stopTrackingArrivals;
        [Export("stopTrackingArrivals")]
        void StopTrackingArrivals();

        // - (void) stopTrackingArrivalForTrackingIdentifier:(NSString*) trackingIdentifier trackTokens:(nullable NSArray<NSString*> *)trackTokens;
        [Export("stopTrackingArrivalForTrackingIdentifier:trackTokens:")]
        void StopTrackingArrivalForTrackingIdentifier(string trackingIdentifier, string[] trackTokens);

        // - (void) cancelTrackingArrivalForTrackingIdentifier:(NSString*) trackingIdentifier trackTokens:(nullable NSArray<NSString*> *)trackTokens;
        [Export("cancelTrackingArrivalForTrackingIdentifier:trackTokens:")]
        void CancelTrackingArrivalForTrackingIdentifier(string trackingIdentifier, string[] trackTokens);
        
		// - (void) startMonitoringArrivalsToSiteWithIdentifier:(NSString*) siteId;
		[Export("startMonitoringArrivalsToSiteWithIdentifier:")]
		void StartMonitoringArrivalsToSiteWithIdentifier(string siteId);

		// - (void) completeTripForTrackingIdentifier:(NSString*) trackingIdentifier trackTokens:(nullable NSArray<NSString*> *)trackTokens;
		[Export("completeTripForTrackingIdentifier:trackTokens:")]
		void CompleteTripForTrackingIdentifier(string trackingIdentifier, string[] trackTokens);
	}



	// typedef void (^CSUserLocationUpdatesAvailableHandler)(NSArray *);
    delegate void CSUserStatusUpdatesAvailableHandler(CSUserStatusUpdate[] updates);

    // @protocol CSSiteArrivalTrackerDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface CSSiteArrivalTrackerDelegate
    {
        // @required -(void)siteArrivalTracker:(CSSiteArrivalTracker*) tracker encounteredError:(NSError*) error;
        [Abstract]
        [Export("siteArrivalTracker:encounteredError:")]
        void SiteArrivalTracker(CSSiteArrivalTracker tracker, NSError error);
    }

    // @interface CSSiteArrivalTracker : NSObject

    [Protocol]
    [BaseType(typeof(NSObject))]
	interface CSSiteArrivalTracker
	{
		// @property (readonly, nonatomic) CSSite * arrivalSite;
		[Export("arrivalSite")]
		CSSite ArrivalSite { get; }

        // @property (copy, nonatomic) CSUserLocationUpdatesAvailableHandler locationUpdateHandler;
		[Export("locationUpdateHandler", ArgumentSemantic.Copy)]
		CSUserStatusUpdatesAvailableHandler LocationUpdateHandler { get; set; }

        [Wrap("WeakDelegate")]
        CSSiteArrivalTrackerDelegate Delegate { get; set; }

        // @property (nonatomic, assign) CSSiteArrivalTrackerDelegate delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // +(instancetype)sharedArrivalTracker;
        [Static]
		[Export("sharedArrivalTracker")]
		CSSiteArrivalTracker SharedArrivalTracker();

        // -(void)stopTrackingArrivals;
        [Export("stopTrackingArrivals")]
        void StopTrackingArrivals();

        // - (void) stopTrackingArrivalForTrackingIdentifier:(NSString*) trackingIdentifier trackTokens:(nullable NSArray<NSString*> *)trackTokens;
        [Export("stopTrackingArrivalForTrackingIdentifier:trackTokens:")]
        void StopTrackingArrivalForTrackingIdentifier(string trackingIdentifier, string[] trackTokens);

        // - (void) cancelTrackingArrivalForTrackingIdentifier:(NSString*) trackingIdentifier trackTokens:(nullable NSArray<NSString*> *)trackTokens;
        [Export("cancelTrackingArrivalForTrackingIdentifier:trackTokens:")]
        void CancelTrackingArrivalForTrackingIdentifier(string trackingIdentifier, string[] trackTokens);
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
    }

	// typedef void (^CSUserAcknowledgeStatus)(BOOL);
	delegate void CSUserAcknowledgeStatus(bool arg0);

	// @interface CSUserLocationUpdate : NSObject
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

		// @property (readonly, nonatomic) CSCustomerInfo * customerInfo;
		[Export("customerInfo")]
		CSUserInfo CustomerInfo { get; }

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

		// -(void)opsAcknowledgesUserWithMessage:(NSString *)ackMessage handler:(CSUserAcknowledgeStatus)acknowledgeStatusHandler;
		[Export("opsAcknowledgesUserWithMessage:handler:")]
		void OpsAcknowledgesUserWithMessage(string ackMessage, CSUserAcknowledgeStatus acknowledgeStatusHandler);

		// @property (readonly, nonatomic) NSDate * opsAcknowledgedTimestamp;
		[Export("opsAcknowledgedTimestamp")]
		NSDate OpsAcknowledgedTimestamp { get; }

		// @property (readonly, nonatomic) NSString * opsTrackingIdentifier;
		[Export("opsTrackingIdentifier")]
		string OpsTrackingIdentifier { get; }
	}

	// @interface CSWebView : UIWebView
    [Protocol]
	[BaseType(typeof(UIWebView))]
	interface CSWebView
	{
		// @property (nonatomic, strong) NSString * oauthToken;
		[Export("oauthToken", ArgumentSemantic.Strong)]
		string OauthToken { get; set; }

		[Wrap("WeakLoginDelegate")]
		CSWebViewLoginDelegate LoginDelegate { get; set; }

		// @property (assign, nonatomic) id<CSWebViewLoginDelegate> loginDelegate;
		[NullAllowed, Export("loginDelegate", ArgumentSemantic.Assign)]
		NSObject WeakLoginDelegate { get; set; }

		[Wrap("WeakAnalyticsDelegate")]
		CSWebViewAnalyticsDelegate AnalyticsDelegate { get; set; }

		// @property (assign, nonatomic) id<CSWebViewAnalyticsDelegate> analyticsDelegate;
		[NullAllowed, Export("analyticsDelegate", ArgumentSemantic.Assign)]
		NSObject WeakAnalyticsDelegate { get; set; }

		// @property (nonatomic, strong) NSDictionary * requestContext;
		[Export("requestContext", ArgumentSemantic.Strong)]
		NSDictionary RequestContext { get; set; }

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

	// @protocol CSWebViewLoginDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface CSWebViewLoginDelegate
	{
		// @required -(void)csWebViewNeedsOauthToken:(CSWebView *)webView;
		[Abstract]
		[Export("csWebViewNeedsOauthToken:")]
		void CsWebViewNeedsOauthToken(CSWebView webView);

		// @required -(void)csWebViewUserLoggedOut:(CSWebView *)webView;
		[Abstract]
		[Export("csWebViewUserLoggedOut:")]
		void CsWebViewUserLoggedOut(CSWebView webView);

		// @required -(void)csWebViewTokenValidationFailed:(CSWebView *)webView error:(id)errorCode;
		[Abstract]
		[Export("csWebViewTokenValidationFailed:error:")]
		void CsWebViewTokenValidationFailed(CSWebView webView, NSObject errorCode);
	}

	// @protocol CSWebViewAnalyticsDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface CSWebViewAnalyticsDelegate
	{
		// @required -(void)csWebView:(CSWebView *)csWebView taggedEvent:(NSString *)eventName properties:(NSDictionary *)eventProperties;
		[Abstract]
		[Export("csWebView:taggedEvent:properties:")]
		void TaggedEvent(CSWebView csWebView, string eventName, NSDictionary eventProperties);

		// @required -(void)csWebView:(CSWebView *)csWebView taggedScreen:(NSString *)screenName;
		[Abstract]
		[Export("csWebView:taggedScreen:")]
		void TaggedScreen(CSWebView csWebView, string screenName);
	}

	// @interface CSWKWebView : WKWebView
    [Protocol]
	[BaseType(typeof(WKWebView))]
	interface CSWKWebView
	{
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