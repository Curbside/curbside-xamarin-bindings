//
//  CSMonitoringSession.h
//  Curbside
//
//  Copyright (c) 2015 Curbside. All rights reserved.
//

#import <Curbside/Curbside.h>
@class CSUserStatusUpdate;
@class CSSite;
@class CSMonitoringSession;

NS_ASSUME_NONNULL_BEGIN

/**
 *      This handler is used to notify that there are status updates for the users. The update array
 *      are instances of CSUserStatus.
 */
typedef void (^CSUserStatusesUpdatedHandler)(NSArray<CSUserStatusUpdate *> *updates);

#pragma mark -

/**
 *      CSMonitoringSessionDelegate will get various callbacks when events happen in the CSUserSession.
 */
@protocol  CSMonitoringSessionDelegate <CSSessionDelegate>

@required

/**
 *      If a delegate is set and there was a error the delegate will get
 *      callback with the error reason.
 */
- (void)session:(CSMonitoringSession *)session encounteredError:(NSError *)error;

@end

#pragma mark -

/**
 *      This class is used to monitor a site for users arrival. Once the session is created with the apiKey and secret
 *      you can identify the user to the session by setting the trackingIdentifier and then be able to start monitoring arrivals to a 
 *      site. 
 *
 */
@interface CSMonitoringSession : CSSession

/**
 *      The site which the this instance is currently monitoring.
 *
 */
@property (nonatomic, readonly, nullable) CSSite *arrivalSite;

/**
 *      The update handler where the user status updates are sent to the consuming application.
 */
@property (nonatomic, copy, nullable)CSUserStatusesUpdatedHandler statusesUpdatedHandler;

/**
 *      The CSMonitoringSession delegate where state changes and error states are sent. 
 */
@property (nonatomic, strong, nullable)id <CSMonitoringSessionDelegate>delegate;

/**
 *      Consumers should be using this method to create a CSMonitoringSession with their apiKey and secret. 
 *      The state of the session can be checked using the sessionState property. The delegate is also notified of sessionState changes  
 *      by the delegate callback.
 *
 *      Note: The usage token is scoped to the Production and Sandbox. Make sure to use the production usage token when deploying
 *      your app to the Apple App Store.
 *
 */
+ (instancetype)createSessionWithAPIKey:(NSString *)apiKey secret:(NSString *)secret delegate:(nullable id<CSMonitoringSessionDelegate>)delegate;

/**
 *      This method returns the CSMonitoringSession initialized by createSessionWithAPIKey:secret:delegate: If there was no 
 *      call made with createSessionWithAPIKey:secret:delegate: then this will return nil.
 */
+ (instancetype)currentSession;


#pragma mark Monitoring Users
/**
 *      This subscribes to user arrival and status updates to the site defined by arrivalSite.
 *
 *      If an error occurs because of an invalid session state, permissions or authentication with the ARRIVE server,
 *      the CSSiteArrivalTrackerDelegate will be informed with the reason as to why startTrackingArrivalsForSite: failed.
 */
- (void)startMonitoringArrivalsToSiteWithIdentifier:(NSString *)siteID;

/**
 *      This unsubscribes to user status updates.
 */
- (void)stopMonitoringArrivals;

#pragma mark Ending/Canceling User trips

/**
 *      Completes the trip(s) identified by the trackToken(s) and trackIdentifier to this site.
 *      Call this method when the trip(s) for the given trackToken(s) is/are completed by the user. If the trackTokens is nil, then all
 *      trips to this site for the user will be marked complete.
 */
- (void)completeTripForTrackingIdentifier:(NSString *)trackingIdentifier trackTokens:(nullable NSArray<NSString *> *)trackTokens;

/**
 *      Cancels the trip(s) identified by the trackToken(s) and trackIdentifier to this site.
 *      Call this method when the trip(s) for the given trackToken(s) is/are cancelled by the user. If the trackTokens is nil, then all
 *      trips to this site for the user will be canceled.
 
 */
- (void)cancelTripForTrackingIdentifier:(NSString *)trackingIdentifier trackTokens:(nullable NSArray<NSString *> *)trackTokens;

@end

NS_ASSUME_NONNULL_END
