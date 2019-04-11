//
//  CSUserSession.h
//  Curbside
//
//  Copyright (c) 2015 Curbside. All rights reserved.
//

#import <Curbside/Curbside.h>

NS_ASSUME_NONNULL_BEGIN

@class CSSite;
@class CSMonitoringNotification;
@class CLLocation;

/**
 *      Notification which is posted when the CSUserSession is properly initialized with previously set trips and ready to go. All previously set
 *      trips will be present at this time. Note this notification is posted at app launch and if there was a trackingIdentfier set previously.
 */
extern  NSString *kCSUserSessionReadyNotificationName;

#pragma mark -

/**
 *      This class is the heart of the transmit application. Once the session is created with the usageToken
 *      you can identify the user to the session by setting the trackingIdentifier and then be able to start/complete/cancel trips to different
 *      sites which are predefined on the ARRIVE platform.
 *
 */
@interface CSUserSession : CSSession

/**
 *      The sites which can be currently (at the time of the call) notified that the user has arrived at the site.
 *
 */
@property (nonatomic, readonly) NSSet<CSSite *> *sitesToNotifyMonitoringSessionUserOfArrival;

/**
 *       The delegate object for the CSUserSession.
 */
@property (nonatomic, strong, nullable) id<CSUserSessionDelegate> delegate;

/**
 *      This returns all the currently tracked CSSite(s) for the signed in user.
 */
@property (nonatomic, readonly) NSSet<CSSite *> *trackedSites;


/**
 *      Consumers should be using this method to create a new session with their usageToken. The state of the session
 *      can be checked using the sessionState property. The delegate is also notified of sessionState changes  by the
 *      delegate callback.
 *
 *      Note: The usage token is scoped to the Production and Sandbox. Make sure to use the production usage token when deploying
 *      your app to the Apple App Store.
 *
 */
+ (instancetype)createSessionWithUsageToken:(NSString *)usageToken delegate:(nullable id<CSUserSessionDelegate>)delegate;

/**
 *      This method returns the CSUserSession initialized by createSessionWithUsageToken:delegate:.
 *
 */
+ (instancetype)currentSession;

/**
 *      Call this method to notify the user in the CSMonitoringSession of the arrival of this user 
 *      at the site.
 */
- (void)notifyMonitoringSessionUserOfArrivalAtSite:(CSSite *)site;

#pragma mark Trip Support

#pragma mark Start Trip
/**
 *      Start a trip tracking the user to the site identified by the siteID. Call this method when
 *      the application thinks its appropriate to start tracking the user eg. Order is ready to be picked up at
 *      the site. This information is persisted across relaunch.
 *
 *      If an error occurs because of an invalid session state, permissions or authentication with the ARRIVE server,
 *      the CSUserSessionDelegate will be informed with the reason as to why startTripToSiteWithIdentifier: failed.
 */
- (void)startTripToSiteWithIdentifier:(NSString *)siteID trackToken:(NSString *)trackToken;

/**
 *      Start a trip tracking the user to the site identified by the siteID for the user's intent of arrival at a given time or a time window.
 *      Call this method when the application thinks its appropriate to start tracking the user.
 *
 *      If the user's intent is to arrive at the site at a given time, then populate the fromDate with the arrival time. e.g. Pick up 
 *      a order at 6.00pm. Set the fromDate to be 6.00pm
 *
 *      If the user's intent is to arrive at the site at a given time window, then populate the fromDate and toDate with the time window.
 *
 *      If an error occurs because of an invalid session state, permissions or authentication with the ARRIVE server,
 *      the CSUserSessionDelegate will be informed with the reason as to why startTripToSiteWithIdentifier: failed.
 */
- (void)startTripToSiteWithIdentifier:(NSString *)siteID trackToken:(NSString *)trackToken etaFromDate:(NSDate *)fromDate toDate:(nullable NSDate *)toDate;

#pragma mark Complete Trip
/**
 *      Completes the trip for the user to the site identified by the siteID with the given trackToken.
 *       If no trackToken is specified, then *all*   trips to this site  will be completed.
 *
 *      @note Do not call this when the user logs out, instead set the trackingIdentifier to nil when the user logs out.
 */
- (void)completeTripToSiteWithIdentifier:(NSString *)siteID trackToken:(nullable NSString *)trackToken;

/**
 *      This method would complete all trips for this user across all devices.
 *
 *      @note Do not call this when the user logs out, instead set the trackingIdentifier to nil when the user logs out.
 */
- (void)completeAllTrips;

#pragma mark Cancel Trip
/**
 *      Cancels the trip for the user to the given site identified by the siteID with the given trackToken. 
 *      If no trackToken is set, then *all* trips to this site are cancelled.
 *
 *      @note Do not call this when the user logs out, instead set the trackingIdentifier to nil when the user logs out.
 */
- (void)cancelTripToSiteWithIdentifier:(NSString *)siteID trackToken:(nullable NSString *)trackToken;

/**
 *      This method will cancels all trips for all sites for the user.
 *
 *      @note Do not call this when the user logs out, instead set the trackingIdentifier to nil when the user logs out.
 */
- (void)cancelAllTrips;

#pragma mark Update Trips from Server
/**
 *      This method would update the ARRIVE SDK with the current state of tracked trips for the user from the server.
 */
- (void)updateTripsFromServer;

#pragma mark - ETA

/**
 *      This method will calculate the estimated time of arrival to a site. Negative value means ETA is unknown.
 *      Unit : seconds
 */
- (void)etaToSiteWithIdentifier:(NSString *)siteID fromLocation:(CLLocation *)fromLocation transportationMode:(CSTransportationMode)transportationMode completionHandler:(void (^)(int estimatedTimeOfArrival))completionHandler;

#pragma mark Mock Trip Support
/**
 *      This method simulates a mock trip to the site. It does the following :
 *          - startTripToSiteWithIdentifier: with the given site
 *          - Gets a route from the given startLocation to the site
 *          - Sends up the location on the route to the site at regular intervals to ARRIVE Server
 *
 *      @note You must cancel the trip using cancelMockTrip, once you are done. You can only do one Mock trip at a time. 
 */
- (void)startMockTripToSiteWithIdentifier:(NSString *)siteID fromLocation:(CLLocation *)startLocation;

/**
 *      This method cancels the mock trip started by the startMockTripToSite:fromLocation: method
 *
 *      @note You must call this method if you have started a MockTrip using startMockTripToSiteWithIdentifier:fromLocation:
 */
- (void)cancelMockTrip;

@end

NS_ASSUME_NONNULL_END
