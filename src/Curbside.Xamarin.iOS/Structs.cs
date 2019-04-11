using System;
using ObjCRuntime;

namespace Curbside
{
	
	[Native]
	public enum CSSessionState : long
	{
		UsageTokenNotSet = 0,
		InvalidKeys,
		Authenticated,
		Valid,
		NetworkError
	}

	[Native]
	public enum CSEvent : long
	{
		CustomerReady = 1,
		CustomerIsssueRaised,
		CustomerIssueResolved,
		SiteOpsLocatedCustomer,
		SiteOpsFailedtoLocateCustomer,
		Internal
	}

	[Native]
	public enum CSErrorCode : long
	{
		InvalidSiteInstance = 1,
        NoTrackTokensForSite,
        APIKeySecretNotSet,
        UsageTokenNotSet,
        NetworkError,
        NotAuthenticated,
        UnknownAppID,
        InvalidSite,
        RequestThrottled,
        Unauthorized,
        TripLimitExceeded,
        TooManySites,
        Unknown
	}

	[Native]
	public enum CSUserStatus : long
	{
		Unknown = 0,
		InTransit,
		Approaching,
		Arrived,
		UserInitiatedArrived
	}

    [Native]
	public enum CSUserSessionAction : long
    {
        UpdateTrackedSites = 1,
        StartTrack,
        StopTrack,
        CancelTrack,
        UpdateLocations,
        NotifySiteOps
    }

    [Native]
    public enum CSTransportationMode : long
    {
        Driving = 0,
        Walking
    }
}
