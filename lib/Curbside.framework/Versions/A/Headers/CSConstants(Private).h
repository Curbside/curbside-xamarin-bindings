//
//  CSConstants(Private).h
//  Curbside
//
//  Created by Radwar on 3/3/17.
//  Copyright © 2017 Curbside. All rights reserved.
//

#import "CSConstants.h"

typedef NS_ENUM(NSInteger, CSEventPrivate) {
    CSEventStartTrack = CSEventInternal + 1,
    CSEventStopTrack,
    CSEventCancelTrack,
    CSEventTrackingIdentifierSet,
    CSEventTrackingIdentifierUnset,
    CSEventApplicationLaunched,
    CSEventApplicationResumed,
    CSEventApplicationBackgrounded,
    CSEventApplicationTerminated,
    CSEventApplicationBackgroundStateChanged,
    
    // CAP Internal Events
    CSEventCAPSiteNameFetchFailed,
    CSEventCAPFetchLoyaltyCardError,
    CSEventCAPFetchRetailerError,
    CSEventCAPPickupFetchError,
    CSEventCAPPickupTasksFetch,
    CSEventCAPTaskActionError,
    CSEventCAPPickupTransferredToCustomer,
    CSEventCAPCancelPickupError,
    CSEventCAPCustomerTransferError,
    CSEventCAPFinishedTask,
    CSEventCAPBagsRetrieved,
    CSEventCAPProductScanError,
    CSEventCAPOfflineEvent,
    CSEventCAPLoginValidationError,
    CSEventCAPPickupPassScan,
    CSEventCAPFetchADLocationsError,
    CSEventCAPOpsAckWithNoSessionTrackingIdentifier,
    CSEventCAPFetchedAndSetSite,
    CSEventCAPLoginError,
    CSEventCAPCSSiteOpsAPIRequesterBadResponse,
    CSEventCAPSiteFetchZeroSites,
    CSEventCAPPickupPassScanError,
    CSEventARRIVEFetchZeroSites,
};

typedef NS_ENUM(NSInteger, CSEnvironment) {
    CSEnvironmentProduction = 1,
    CSEnvironmentSandbox = 2,
    CSEnvironmentLocalHost,
    CSEnvironmentQ1,
    CSEnvironmentStaging,
    CSEnvironmentDashboardProduction,
    CSEnvironmentDashboardStaging,
};

typedef NS_ENUM(NSInteger, CSLocationUpdateMode) {
    CSLocationUpdateModeOff = 0,
    CSLocationUpdateModeSignificant,
    CSLocationUpdateModeContinous,
};
