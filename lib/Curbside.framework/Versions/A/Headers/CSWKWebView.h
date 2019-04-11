//
//  CSWKWebView.h
//  Curbside
//
//  Created by Radwar on 5/16/17.
//  Copyright © 2017 Curbside. All rights reserved.
//

#import <UIKit/UIKit.h>
@import WebKit;

@protocol CSWKWebViewLoginDelegate;
@protocol CSWKWebViewAnalyticsDelegate;

/**
 *      This class is used to load the Web content from the shopcurbide web portal.
 *      ** Note **
 *          1. The navigation delegate for this class cannot be changed. The delegate for this is a internal Curbside class
 *          2. Instantiate the retailer specific subclass for CSWKWebView. Please contact Curbside to get your retailer specific subclass.
 *          3. Authentication token callbacks will be sent to the loginDelegate.
 */

@interface CSWKWebView : WKWebView

/**
 *      The authenticationToken which will be used used by Curbside Backend Server to authenticate and retrieve customer
 *      info.
 */
@property (nonatomic, strong) NSString *authenticationToken;

/**
 *       The delegate object responsible for handling the request for SSO login natively in the app.
 */
@property (nonatomic, assign) id<CSWKWebViewLoginDelegate> loginDelegate;


/**
 *       The delegate object responsible for handling analytics events.
 */
@property (nonatomic, assign) id<CSWKWebViewAnalyticsDelegate> analyticsDelegate;

/**
 *       This dictionary is consumed by the Javascript which gets loaded in the CSWKWebView.
 */
@property (nonatomic, strong) NSDictionary *requestContext;

/**
 *      crbsWebAction is the raw string which was used to open the app via tapping on a deeplink:// link 
 *      or this can also represent the URN which describes where to go inside the white label solution
 */
@property (nonatomic, strong) NSString *crbsWebAction;

/**
 *      Enabling developmentMode will point the webview to the in-development mode version of the retailer site. 
 *      By default this is false and the webview will load the production version of the site
 *
 */
@property (nonatomic)BOOL developmentMode;

/**
 *      calling logout will reset previously set authenticationToken and delete and persistent data, if any, from the client device
 */
- (void)logout;

- (void)loadPage;

@end

/**
 *      @protocol CSWKWebViewLoginDelegate
 *
 */
@protocol CSWKWebViewLoginDelegate <NSObject>

@required

/**
 *      delegate is informed that the CSWKWebView requires a authenticationToken. This is called on the loginDelegate when
 *      the user takes an action in the CSWKWebView which requires the user to be logged in. The native app can now push
 *      the SSO Login viewcontroller  on top of the viewcontroller which is showing the CSWKWebView. Once the user has
 *      successfully logged in and has a token, it can then then pop the SSO login viewcontroller and set the
 *      new token on the CSWKWebView using the webview.authenticationToken method.
 */
- (void)csWKWebViewNeedsAuthenticationToken:(CSWKWebView *)webView;

/**
 *      delegate is informed that the CSWKWebView the user has clicked the 'Logout' button in the WebView. The native
 *      client would then want to do a proper logout and invalidate the current authenticationToken;
 */
- (void)csWKWebViewUserLoggedOut:(CSWKWebView *)webView;

/**
 *      delegate is informed that the validation of the authenticationToken has failed validation on the backend.
 */
- (void)csWKWebViewTokenValidationFailed:(CSWKWebView *)webView error:(id)errorCode;

@end

/**
 *      @protocol CSWKWebViewAnalyticsDelegate
 *
 */
@protocol CSWKWebViewAnalyticsDelegate <NSObject>

@required


/**
 *      delegate is informed that the CSWKWebView has tagged an event with eventName and properties eventProperties.
 *      Note that eventProperties may be nil.
 */
- (void)csWKWebView:(CSWKWebView *)webView taggedEvent:(NSString *)eventName properties:(NSDictionary *)eventProperties;

/**
 *      delegate is informed that the CSWKWebView has tagged a screen with name screenName;
 */
- (void)csWKWebView:(CSWKWebView *)webView taggedScreen:(NSString *)screenName;

@end
