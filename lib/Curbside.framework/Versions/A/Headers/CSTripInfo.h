//
//  CSTripInfo.h
//  Curbside
//
//  Copyright © 2017 Curbside. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 *      CSTripInfo gives information about the trip to the site.
 */

@interface CSTripInfo : NSObject

/**
 *      The trackToken which was used to start the trip.
 */
@property (nonatomic, readonly)NSString *trackToken;

/**
 *      The startDate when this trip was started.
 */
@property (nonatomic, readonly)NSDate *startDate;

/**
 *      The destID is a debug identifier which uniquely identifies this trip in the Curbside platform.
 */
@property (nonatomic, readonly)NSString *destID;

/**
 *      If the trip was started with the user's intent to arrive at a given time or intent to arrive in a time window, 
 *      etaFromDate will be populated with the date.
 */
@property (nullable, nonatomic, readonly)NSDate *etaFromDate;

/**
 *      If the trip was started with the user's intent to arrive in a time window, etaToDate will be populated with the toDate.
 */
@property (nullable, nonatomic, readonly)NSDate *etaToDate;

@end

NS_ASSUME_NONNULL_END
