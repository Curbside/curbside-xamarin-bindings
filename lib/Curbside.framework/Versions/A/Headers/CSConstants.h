//
//  CSConstants.h
//  Curbside
//
//  Copyright © 2016 Curbside. All rights reserved.
//

#ifndef CSConstants_h
#define CSConstants_h


#endif /* CSConstants_h */

#import <Foundation/Foundation.h>

/**
 *      Describes the current state of the session
 *      @note The session has to be in CSSessionStateValid state to be able to use session specific methods for starting trips or monitoring 
 *      sites.
 */
typedef NS_ENUM(NSInteger, CSSessionState) {
    /**
     * Initial state. Validation not complete.
     */
    CSSessionStateUsageTokenNotSet = 0,
    /**
     * API Key/Secret or usage token used is invalid
     */
    CSSessionStateInvalidKeys,
    /**
     * API Key/Secret or usage token have been validated and there is no tracking identifier set
     */
    CSSessionStateAuthenticated,
    /**
     * API Key/Secret or usage token have been validated and there is a valid tracking identifier assigned. The session has to be in this state to be able to use session specific methods for starting trips or monitoring sites.
     */
    CSSessionStateValid,
    /**
     * Unable to contact server for validation. SDK will retry to make a valid connection.
     */
    CSSessionStateNetworkError,
};

/**
 *      The events are optional and used to provide more context to Arrival
 *      The events can be sent by the consumer to indicate the life cycle of the trackToken through the retailer system.
 *      This will be used to build a rich context of the entire Arrival from start to finish.
 */
typedef NS_ENUM(NSInteger, CSEvent) {
    /**
     * This indicates that the site is ready for the user. The order/reservation represented by the siteIdentifier and trackToken is ready
     */
    CSEventUserReady = 1,
    
    /**
     * This indicates that there was a problem with the order/reservation with the given trackToken and siteIdentifier. e.g. The requested items
     * were unavailable or there was a error payment. This event indicates that the user need to take action to resolve the issue.
     */
    CSEventUserIsssueRaised,
    
    /**
     * This indicates that the user resolved the issue which was raised. This is followed by a previous CSEventUserIsssueRaised event.
     */
    CSEventUserIssueResolved,

    /**
     * Once the monitoring session user gets a user arrived notification, this event indicates that the monitoring session user met with the user. e.g.
     * This event can be sent when the user is verified by the monitoring session user - PickupPass scanned.
     */
    CSEventLocatedUserAtSite,
    
    /**
     * Once the monitoring session user gets a user arrived notification and was unable to meet the user - false positive, this event can be sent
     * so that we can mark in the Arrival platform that the notification was a false alert.
     */
    CSEventFailedToLocateUserAtSite,
    
    /**
     * Internal Use Only
     */
    CSEventInternal,
};

/**
 *      The below error codes define different reasons why a method call to the SDK failed.
 */
typedef NS_ENUM(NSInteger, CSErrorCode) {
    // CSSession error codes
    /**
     *  The application have not authorized location services.
     */
    CSErrorCodeLocationNotAuthorized = 1,

    /**
     *  The application have the background app refresh disabled at the time of the SDK method call.
     */
    CSErrorCodeBackgroundAppRefreshDenied,
    
    /**
     *  The trackingIdentifier was not set prior to making a call in the SDK which requires it set
     *  or the trackingIdentifier length > 36
     */
    CSErrorCodeTrackingIdentifierInvalid,
    
    /**
     *  An invalid site instance was given to start tracking for the CSTracker
     */
    CSErrorCodeInvalidSiteInstance,
    
    /**
     *  Start track called with site with no trackTokens in it.
     */
    CSErrorCodeNoTrackTokensForSite,
    
    /**
     *  The API key and secret is not set in the CSMonitoringSession. The CSMonitoringSession was not initialized using its
     *  class method which takes the API key and secret.
     */
    CSErrorCodeAPIKeySecretNotSet,

    /**
     *  The usageToken is not set on the CSUserSession. The CSUserSession was not initialized using its class
     *  method which takes in the usageToken
     */
    CSErrorCodeUsageTokenNotSet,

    /**
     * Network error talking to Curbside servers
     */
    CSErrorCodeNetworkError,
    
    /**
     *  The session has not been authenticated and/or a trackingIdentifier set.
     */
    CSErrorCodeNotAuthenticated,
    
    /**
     *  The application/bundle identifier from which the request was sent is not recognized by the ARRIVE
     *  platform for the account. Please use the dashboard to add the bundle identifier of the app in the account.
     */
    CSErrorCodeUnknownAppID,

    /**
     *  The site for which the action was requested is either not available under the current app family or that
     *  site does not exist in the Curbside Platform. Please use the ARRIVE dashboard to add the site.
     */
    CSErrorCodeInvalidSite,

    /**
     *  The application or account has exceeded the number of requests and the request is throttled.
     */
    CSErrorCodeRequestThrottled,
    
    /**
     *  The application is not authorized to use the Curbside Platform. The API key/secret or usageToken is invalid or 
     *  the app-id is not reconginized by the Curbside Platform. Please make sure that the API key/secret or usageToken is valid
     *  and the app-id has been added to the account in ARRIVE Dashboard.
     */
    CSErrorCodeUnauthorized,

    /**
     *  The number of trips has exceeded the maximum permitted for the Curbside Platform account.
     */
    CSErrorCodeTripLimitExceeded,
    
    /**
     *  The number of sites being tracked for the given user has exceeded the maximum number of sites.
     */
    CSErrorCodeTooManySites,
    
    /**
     *  The trackToken has previously been used in a completed or cancelled trip.
     */
    CSErrorCodeTrackTokenAlreadyUsed,
    
    /**
     *  A location is considered invalid if it meets at least one of the following criteria:
     *    location is nil
     *    Its latitude is greater than 90 degrees or less than -90 degrees.
     *    Its longitude is greater than 180 degrees or less than -180 degrees.
     */
    CSErrorCodeInvalidLocation,

    CSErrorCodeUnknown,
};

/**
 *      These enum define what the Curbside Platform know about the user
 */
typedef NS_ENUM(NSInteger, CSUserStatus) {
    /**
     *  Unknown status
     */
    CSUserStatusUnknown = 0,
    
    /**
     *  The user is moving and Curbside Platform have received a status update from the user recently.
     */
    CSUserStatusInTransit,
    
    /**
     *  The user is approaching the site.
     */
    CSUserStatusApproaching,
    
    /**
     *  The user has arrived at the site.
     */
    CSUserStatusArrived,
    
    /**
     *  The user has explicitly informed that the user is at the site.
     */
    CSUserStatusUserInitiatedArrived,
};

/**
 *      These enum define what the user (device) activity is when a CSUserStatusUpdate is given.
 */
typedef NS_ENUM(NSInteger, CSMotionActivity) {
    /**
     *  Unknown status
     */
    CSMotionActivityUnknown = 0,
    
    /**
     *  The user (device) is moving and inside a vehicle.
     */
    CSMotionActivityInVehicle,
    
    /**
     *  The user (device) is moving and on a bycles.
     */
    CSMotionActivityOnBicycle,
    
    /**
     *  The user (device) is moving and on foot. The user maybe walking or running.
     */
    CSMotionActivityOnFoot,

    /**
     *  The user (device) is not moving and stationary.
     */
    CSMotionActivityStill,
};

/**
 *      Use transportation mode when requesting estimated arrival time to site.
 */
typedef NS_ENUM(NSInteger, CSTransportationMode) {
    /**
     *  Calculate eta based on driving conditions
     */
    CSTransportationModeDriving = 0,
    
    /**
     *  Calculate eta based on walking conditions
     */
    CSTransportationModeWalking,
};

