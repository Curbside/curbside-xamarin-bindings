//
//  CSSite.h
//  Curbside
//
//  Copyright (c) 2015 Curbside. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreLocation/CoreLocation.h>
#import <Curbside/CSConstants.h>
@class CSTripInfo;

NS_ASSUME_NONNULL_BEGIN

/**
 *    The CSSite object defines a site object identified by the siteIdentifier. In the CSUserSession the site would have
 *    tripInfos for trip which has been started, distance to the site and the userStatus.
 *
 *    @note All sites need to be previously loaded in the ARRIVE dashboard. Site identifiers are prefixed with your account id and it 
 *    is best-practice to use the same site identifiers you use. For example if a venue has a unique id of 1337 in your system it would 
 *    have a site identifier equals to hardwaredepot_1337 in ARRIVE
 */
@interface CSSite : NSObject

/**
 *      The unique site identifier identifiying a particular site.
 */
@property (nonatomic, readonly)NSString *siteIdentifier;

/**
 *      Designated initializer for this class.
 *
 *      Initializes a instance of the CSSite with a identifier.
 */
- (id)initWithSiteIdentifier:(NSString *)siteIdentifier;


#pragma mark CSUserSession Support
/**
 *      Each element of the tripInfos, CSTripInfo, has the details of the trip which is being tracked for this site.
 */
@property (nonatomic, readonly, nullable)NSArray<CSTripInfo *> *tripInfos;

/**
 *      Flag indicating whether the user is approaching the site.
 */
@property (nonatomic, readonly)CSUserStatus userStatus;

/**
 *      distanceFromSite indicates the distance from site. When the user is beyond a certain theshold
 *      distance, only this value is populated. Unit : meters
 */
@property (nonatomic, readonly)int distanceFromSite;

/**
 *      The current estimated arrival time from site. Negative value means ETA is unknown.
 *      Unit : seconds
 */
@property (nonatomic, readonly)int estimatedTimeOfArrival;

@end

NS_ASSUME_NONNULL_END
