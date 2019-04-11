//
//  CSWebView.h
//  Curbside
//
//  Copyright © 2015 Curbside. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol CSWebViewLoginDelegate;
@protocol CSWebViewAnalyticsDelegate;

/**
 *      This class is used to load the Web content from the shopcurbide web portal. 
 *      ** Note ** 
 *          1. The delegate for this class cannot be changed. The delegate for this is a internal Curbside class
 *          2. Instantiate the retailer specific subclass for CSWebView. Please contact Curbside to get your retailer specific subclass.
 *          3. OAuth callbacks will be sent to the loginDelegate.
 */

@interface CSWebView : UIWebView

/**
 *      The oauthToken which will be used used by Curbside Backend Server to authenticate and retrieve customer
 *      info from retailer backend server.
 */
@property (nonatomic, strong) NSString *oauthToken;

/**
 *       The delegate object responsible for handling the request for SSO login natively in the app.
 */
@property (nonatomic, assign) id<CSWebViewLoginDelegate> loginDelegate;


/**
 *       The delegate object responsible for handling analytics events.
 */
@property (nonatomic, assign) id<CSWebViewAnalyticsDelegate> analyticsDelegate;

/**
 *       This dictionary is consumed by the Javascript which gets loaded in the CSWebView.
 */
@property (nonatomic, strong) NSDictionary *requestContext;

/**
 *      Enabling developmentMode will point the webview to the in-development mode version of the retailer site.
 *      By default this is false and the webview will load the production version of the site
 *
 */
@property (nonatomic)BOOL developmentMode;

/**
 *      Calling logout will reset previously set oauthToken and delete persistent data, if any, from the client device
 */
- (void)logout;

- (void)loadPage;

@end

/**
 *      @protocol CSWebViewLoginDelegate
 *
 */
@protocol CSWebViewLoginDelegate <NSObject>

@required

/**
 *      delegate is informed that the CSWebView requires a oauthToken. This is called on the loginDelegate when
 *      the user takes an action in the CSWebView which requires the user to be logged in. The native app can now push
 *      the SSO Login viewcontroller  on top of the viewcontroller which is showing the CSWebView. Once the user has
 *      successfully logged in and has a token, it can then then pop the SSO login viewcontroller and set the
 *      new token on the CSWebView using the webview.oauthToken method.
 */
- (void)csWebViewNeedsOauthToken:(CSWebView *)webView;

/**
 *      delegate is informed that the CSWebView the user has clicked the 'Logout' button in the WebView. The native
 *      client would then want to do a proper logout and invalidate the current oauthToken;
 */
- (void)csWebViewUserLoggedOut:(CSWebView *)webView;

/**
 *      delegate is informed that the validation of the oauthToken has failed validation on the backend. 
 */
- (void)csWebViewTokenValidationFailed:(CSWebView *)webView error:(id)errorCode;

@end

/**
 *      @protocol CSWebViewAnalyticsDelegate
 *
 */
@protocol CSWebViewAnalyticsDelegate <NSObject>

@required


/**
 *      delegate is informed that the CSWebView has tagged an event with eventName and properties eventProperties.
 *      Note that eventProperties may be nil.
 */
- (void)csWebView:(CSWebView *)csWebView taggedEvent:(NSString *)eventName properties:(NSDictionary *)eventProperties;

/**
 *      delegate is informed that the CSWebView has tagged a screen with name screenName;
 */
- (void)csWebView:(CSWebView *)csWebView taggedScreen:(NSString *)screenName;

@end
