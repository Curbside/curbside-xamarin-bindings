//
//  CSUserSessionDelegate.h
//  Curbside
//
//  Copyright © 2017 Curbside. All rights reserved.
//

NS_ASSUME_NONNULL_BEGIN

@class CSUserSession;
@class CSSite;

/**
 *      Different actions taken on the CSUserSession. These actions are used to annotate the cause of an error
 *      when the delegate session:encounteredError:forOperation is called.
 *
 */
typedef NS_ENUM(NSInteger, CSUserSessionAction) {
    CSUserSessionActionUpdateTrackedSites = 1,
    CSUserSessionActionStartTrack,
    CSUserSessionActionStopTrack,
    CSUserSessionActionCancelTrack,
    CSUserSessionActionUpdateLocations,
    CSUserSessionActionNotifyMonitoringSessionUser,
    CSUserSessionActionEtaToSite
};


/**
 *      The CSUserSession's delegate object will get various callbacks when events happen in the CSUserSession.
 */
@protocol CSUserSessionDelegate <CSSessionDelegate>

@optional

/**
 *      This method is on the CSUserSession delegate after app initialization and resume when the user
 *      is near a site where the associate can be notified of the user arrival.
 *
 */
- (void)session:(CSUserSession *)session canNotifyMonitoringSessionUserAtSite:(CSSite *)site;

/**
 *      If a delegate is set and the method is implemented, then the delegate will receive a callback when
 *      the user is approaching a site which is currently tracked for a trip.
 */
- (void)session:(CSUserSession *)session userApproachingSite:(CSSite *)site;

/**
 *      If a delegate is set and the method is implemented, then the delegate will receive a callback when
 *      the user has arrived at a site which is currently tracked for a trip.
 */
- (void)session:(CSUserSession *)session userArrivedAtSite:(CSSite *)site;

/**
 *      If a delegate is set and there was a error completing an action, the delegate will get
 *      callback with the error reason.
 */
- (void)session:(CSUserSession *)session encounteredError:(NSError *)error forOperation:(CSUserSessionAction)customerSessionAction;

/**
 *      If a delegate is set and the trackedSites are updated, the delegate will get this callback. The tracked
 *      sites are updated on launch when a trackngIdentfier is set or when the application code explicitly calls the updateTripsFromServer
 *      method.
 *
 */
- (void)session:(CSUserSession *)session updatedTrackedSites:(NSSet<CSSite *> *)trackedSites;

/**
 *      If a delegate is set and the method is implemented you will get a callback when a trip has been successfully started.
 *
 */
- (void)session:(CSUserSession *)session tripStartedForSite:(CSSite *)site;

/**
 *      If a delegate is set and the method is implemented you will get a callback the mockTrip has successfully started.
 *      method.
 *
 */
- (void)session:(CSUserSession *)session mockTripStartedForSite:(CSSite *)site;

/**
 *      If a delegate is set and the method is implemented you will get a callback on errors while simulating the mock trip.
 *      method.
 *
 */
- (void)session:(CSUserSession *)session mockTripFailedForSite:(CSSite *)site error:(NSError *)error;

/**
 *      If a delegate is set and the method is implemented you will get regular heartbeats when location are sent up to the server.
 *      method.
 *
 */
- (void)session:(CSUserSession *)session mockTripSentLocationForSite:(CSSite *)site;

/**
 *      If a delegate is set and the method is implemented you will get a callback when the mock trip is successfully cancelled.
 *      method.
 *
 */
- (void)session:(CSUserSession *)session mockTripCancelledForSite:(CSSite *)site;

@end

NS_ASSUME_NONNULL_END
