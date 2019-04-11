//
//  CSUserStatusUpdate.h
//  Curbside
//
//  Copyright (c) 2015 Curbside. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <Curbside/CSConstants.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (^CSUserAcknowledgeStatus)(BOOL success);

@class CLLocation;
@class CSUserInfo;
@class CSTripInfo;

/**
 *      CSUserStatusUpdate is a wrapper around a given trackingIdentifier with all open trips for the user
 *      and status information if Curbside Platform has a recent location.
 *
 */
@interface CSUserStatusUpdate : NSObject

/**
 *      The tracking identifier for this status update.
 */
@property (nonatomic, readonly)NSString *trackingIdentifier;

/**
 *      The location of the user. The timestamp property of the CLLocation would be the actual timestamp
 *      for this user's location when it was sent to the server by a CSUserSession for that user. This is the same
 *      value in the lastUpdateTimestamp below. 
 *      
 *      Locations are only populated when the user is close to the site.
 *
 */
@property (nonatomic, readonly, nullable)CLLocation *location;

/**
 *      The timestamp when we last got a location update from the user. If a location was given with this
 *      UserLocationUpdate that will have the same timestamp.
 */
@property (nonatomic, readonly, nullable)NSDate *lastUpdateTimestamp;

/**
 *      Flag indicating whether the user is approaching the site.
 */
@property (nonatomic, readonly)CSUserStatus userStatus;

/**
 *      customerInfo contains the additional attributes, if any, when the original track was done
 *      from the Mobile classes.
 */
@property (nonatomic, readonly, nullable)CSUserInfo *userInfo;

/** 
 *      If webhooks are configured for the Curbside platform, then ARRIVE will make calls to the webhook when
 *      there is a user approaching the site or at the site. If the userStatus is either CSUserStatusArrived or
 *      CSUserStatusArrived and acknowledgedUser is not set, then no one has acknowledged that there is
 *      a user approaching the site or at the site. The ARRIVE system will then aggressively make calls on the webhook
 *      till an monitoring session user has acknowledged the arriving/approaching user.
 *      Use [CSUserStatusUpdate monitoringSessionUserAcknowledgesUserWithMessage:handler:] to tell ARRIVE System that 
 *      the monitoring session user is aware of the user.
 *
 */
@property (nonatomic, readonly)BOOL acknowledgedUser;

/**
 *      The current estimated arrival time of the user. Note this may come in as a separate
 *      CSUserStatusUpdate event. Unit : seconds
 *      Negative value means ETA is unknown. 
 */
@property (nonatomic, readonly)int estimatedTimeOfArrival;

/**
 *      distanceFromSite indicates the distance from site. When the user is beyond a certain theshold
 *      distance, only this value is populated. Unit : meters
 */
@property (nonatomic, readonly)int distanceFromSite;

/**
 *      motionActivity defines the activity the user (device) at the time of the status update. The motion activity is automatically sent
 *      to the ARRIVE server from your application if the following conditions are met :
 *          - The device is capabale of getting CMMotionActivity
 *          - The Application has a plist entry for using the device's motion - NSMotionUsageDescription
 *          - The user has given permission to use the device motion.
 *
 */
@property (nonatomic, readonly)CSMotionActivity motionActivity;

/**
 *      tripInfos returns the CSTripInfo(s) which are currently tracked for this user at this site. 
 */
@property (nonatomic, readonly)NSArray<CSTripInfo *> *tripInfos;

/**
 *      Calling this method when the userStatus is either CSUserStatusArrived or
 *      CSUserStatusArrived, tells the AD system that there is a monitoring session user who is aware of the user approaching.
 *
 *      @note If the userStatus is not one of the above status or acknowledgedUser is true, then this method is a no-op.
 */
- (void)monitoringSessionUserAcknowledgesUserWithMessage:(NSString *)ackMessage handler:(CSUserAcknowledgeStatus)acknowledgeStatusHandler;

/**
 *      The timestamp when the user status was acked
 *
 */
@property (nonatomic, readonly, nullable)NSDate *monitoringSessionUserAcknowledgedTimestamp;

/**
 *      The tracking identifier of the monitoring session user who acked user status.
 *
 */
@property (nonatomic, readonly, nullable)NSString *monitoringSessionUserTrackingIdentifier;

@end

NS_ASSUME_NONNULL_END
