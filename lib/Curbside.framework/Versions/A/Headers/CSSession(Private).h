//
//  CSSession(Private).h
//  Curbside
//
//  Created by Radwar on 2/17/15.
//  Copyright (c) 2015 Curbside. All rights reserved.
//

#import "CSSession.h"
#import "CSConstants(Private).h"

const static NSString *kCurbsideVersionString = @"3.26";

@class CSSite;
@class CSLoggedInUser;

@interface CSSession (Private)
+ (void)_completeSetup;

// URL Support
+ (NSString *)tellURLString;
+ (NSString *)mobileClientURLString;
+ (NSString *)siteOpsURLString;
+ (NSString *)siteSearchURLString;
+ (NSString *)siteSearchDashboardURLString;
+ (NSString *)siteEtaURLString;


/**
 *      By default the environment is Production. When developing make sure to use the Sandbox environment. The environment
 *      needs to reflect the app state - if the app is not signed with a distribution profile then this needs to be set to
 *      CSEnvironmentSandbox.
 */
+ (void)setEnvironment:(CSEnvironment)environment;
+ (BOOL)isDashboardSession;

- (void)addToTrackingSite:(CSSite *)site;
- (void)removeFromTrackingSite:(CSSite *)site;
- (NSError *)sessionError;
- (void)_registerSessionIfPossible;

@end

@interface CSSession ()
{
    int _retryCount;
    int _lastRetryInterval;
}

@property (nonatomic, strong)CSLoggedInUser *user;
@property (nonatomic, strong)NSString *deviceToken;
@property (nonatomic, strong)NSString *lastDeviceToken;
@property (nonatomic)CSSessionState sessionState;
@property (nonatomic, strong)NSString *curbsideUsageToken;

+ (CSEnvironment)getCurrentEnvironment;

- (NSDictionary *)eventDictForEvent:(NSInteger)event forTrackToken:(NSString *)trackToken siteIdentifier:(NSString *)siteIdentifier properties:(NSDictionary *)properties;

- (void)addEvent:(NSDictionary *)event forceSend:(BOOL)forceSend;

- (BOOL)addEvent:(CSEvent)event forTrackToken:(NSString *)trackToken siteIdentifier:(NSString *)siteIdentifier properties:(NSDictionary *)properties;

/**
 *      Register's the push notification token for this device with Curbside. The consuming application should register for
 *      push notification. The notifications to register for are :
 *
 *          UIRemoteNotificationTypeBadge | UIRemoteNotificationTypeAlert | UIRemoteNotificationTypeNewsstandContentAvailability
 *
 *      The device token for notifications is then returned in the UIApplication delegate's
 *      application:application didRegisterForRemoteNotificationsWithDeviceToken:deviceToken method. Once this is received on
 *      delegate call this method with the token to properly register the token with Curbside.
 */
- (void)registerDeviceToken:(NSData *)deviceToken;

/**
 *      Informs the SDK for the device token registration.
 */
- (void)handleDidFailToRegisterForRemoteNotifications:(NSError *)error;

/**
 *      When the application receives push notification, forward the push notification to the session to handle.
 *      Returns YES if the push was processed by the SDK (The push was meant for the SDK) and NO otherwise.
 */
- (BOOL)handlePushNotificationDictionary:(NSDictionary *)dictionary;

- (void)_applicationBecameActive;
@end
