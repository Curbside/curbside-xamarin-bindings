//
//  CSUserInfo.h
//  Curbside
//
//  Copyright © 2017 Curbside. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

/**
 *      The CSUserInfo object is used to define more attributes about the user
 *
 */

@interface CSUserInfo : NSObject

/**
 *      fullName of the user.
 */
@property (nonatomic, strong, nullable)NSString *fullName;

/**
 *      email address of the user.
 */
@property (nonatomic, strong, nullable)NSString *emailAddress;

/**
 *      SMS phone number 
 */
@property (nonatomic, strong, nullable)NSString *smsNumber;

/**
 *      Vehicle info - Make
 */
@property (nonatomic, strong, nullable)NSString *vehicleMake;

/**
 *      Vehicle info - Model
 */
@property (nonatomic, strong, nullable)NSString *vehicleModel;

/**
 *      Vehicle info - License Plate
 */
@property (nonatomic, strong, nullable)NSString *vehicleLicensePlate;

@end

NS_ASSUME_NONNULL_END
