//
//  CSMonitoringNotification.h
//  Curbside
//
//  Copyright (c) 2015 Curbside. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 *      This class represents the information which is entered by the user in a CSMonitoringSession
 *      responding to a notification event from the customer in the CSUserSession.
 *
 */
@interface CSMonitoringNotification  : NSObject

/**
 *      Whether the request was acknowledged by the monitoring session user.
 */
@property (nonatomic, readonly)BOOL monitoringSessionUserAcknowledged;

/**
 *      Timestamp of when the request was acknowledged.
 */
@property (nonatomic, readonly, nullable)NSDate *monitoringSessionUserAcknowledgeTimestamp;

/**
 *      ETA in seconds, if the Monitoring user entered a value
 *
 */
@property (nonatomic, readonly)int monitoringSessionUserEstimatedTimeOfArrival;

/**
 *      Message from Monitoring user if one was entered.
 *
 */
@property (nonatomic, readonly, nullable)NSString *monitoringSessionUserMessage;

@end

NS_ASSUME_NONNULL_END
