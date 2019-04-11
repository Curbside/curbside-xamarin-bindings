//
//  CSSession.h
//  Curbside
//
//  Copyright (c) 2015 Curbside. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <Curbside/CSConstants.h>

NS_ASSUME_NONNULL_BEGIN

@protocol CSSessionDelegate;
@class CSUserInfo;

/**
 *      This is an abstract class which defines the base session methods. Use the CSUserSession for the transmit app and the
 *      CSMonitoringSession for monitor applications.
 *
 *      Every application integrating the ARRIVE SDK need to have a valid session in the application. The session must
 *      be created in the UIApplication delegate's application:didFinishLaunchingWithOptions: method with a valid key/secret or usage token.
 *      The session should have a valid trackingIdentifier set for the user. This trackingIdentifier is what uniquely identifies the signed in user
 *      to the Curbside platform. In the transmit/customer apps the trackingIdentifier is used to tag user information sent upstream to the ARRIVE
 *      platform. In the monitoring app this is used to identify the user who is monitoring arrivals at the site and send notifications as needed.
 *
 */
@interface CSSession : NSObject

/**
 *      trackingIdentifier for the user who is logged into the device. This may be nil when the app is started, but as the
 *      user logs into the app, make sure this value is set. trackingIdentifier needs to be set to use session specific methods for starting 
 *      trips or monitoring sites. This identifier will be persisted across application restarts.
 * 
 *      When the user logs out, set this to nil, which will inturn end the user session or monitoring session.
 *
 *      @note The maximum length of the trackingIdentifier is 36 characters.
 *
 */
@property (nonatomic, strong, nullable)NSString *trackingIdentifier;

/**
 *      userInfo has more attributes about the user e.g. full name, email and sms number. If the user has signed into the application
 *      use the userInfo to set additional user attributes. If this is set in the CSUserSession before starting a trip, then the information is
 *      echoed back in the CSMonitoringSession for the user to give additional context for the trip. 
 *
 */
@property (nonatomic, strong, nullable)CSUserInfo *userInfo;

/**
 *      The session has to be in CSSessionStateValid to be able to use session specific methods for starting trips or monitoring sites.
 */
@property (nonatomic, readonly)CSSessionState sessionState;

/**
 *       The delegate object for the session. The session calls methods on this delegate as the session state changes.
 */
@property (nonatomic, strong, nullable) id<CSSessionDelegate> delegate;


/**
 *      Informs the SDK that the application did finish launching. Make sure that you call this method from your application
 *      delegate's application:application didFinishLaunchingWithOptions:launchOptions
 *      method.
 */
- (void)application:(UIApplication *)application didFinishLaunchingWithOptions:(nullable NSDictionary *)launchOptions;

// Event Support

/**
 *      Add the given event for the siteIdentifier and trackToken combination.
 *      properties - can be nil or custom properties from the transmit app. 
 *      returns YES if it successfully added the event. 
 */
- (BOOL)addEvent:(CSEvent)event forTrackToken:(nullable NSString *)trackToken siteIdentifier:(nullable NSString *)siteIdentifier properties:(nullable NSDictionary *)properties;

@end

/**
 *      @protocol CSSessionDelegate
 *      
 */
@protocol CSSessionDelegate <NSObject>

@optional

/**
 *      delegate is informed when the session state has changed
 */
- (void)session:(CSSession *)session changedState:(CSSessionState)newState;

@end

NS_ASSUME_NONNULL_END
