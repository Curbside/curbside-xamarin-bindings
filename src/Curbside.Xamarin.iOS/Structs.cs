using System;
using CoreLocation;
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
        UserReady = 1,
        UserIsssueRaised,
        UserIssueResolved,
        LocatedUserAtSite,
        FailedToLocateUserAtSite,
        Internal
    }

	[Native]
	public enum CSErrorCode : long
	{
        LocationNotAuthorized = 1,
        BackgroundAppRefreshDenied,
        TrackingIdentifierInvalid,
        InvalidSiteInstance,
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
        TrackTokenAlreadyUsed,
        InvalidLocation,
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
    public enum CSMotionActivity : long
    {
        Unknown = 0,
        InVehicle,
        OnBicycle,
        OnFoot,
        Still
    }

    [Native]
    public enum CSTransportationMode : long
    {
        Driving = 0,
        Walking
    }

    [Native]
    public enum CSUserSessionAction : long
    {
        UpdateTrackedSites = 1,
        StartTrack,
        StopTrack,
        CancelTrack,
        UpdateLocations,
        NotifyMonitoringSessionUser,
        EtaToSite
    }

    public enum CLAuthorizationStatus
    {
        NotDetermined = 0,
        Restricted,
        Denied,
        AuthorizedAlways,
        AuthorizedWhenInUse,
        Authorized = AuthorizedAlways
    }

    public enum CLDeviceOrientation
    {
        Unknown = 0,
        Portrait,
        PortraitUpsideDown,
        LandscapeLeft,
        LandscapeRight,
        FaceUp,
        FaceDown
    }

    [Native]
    public enum CLActivityType : long
    {
        Other = 1,
        AutomotiveNavigation,
        Fitness,
        OtherNavigation,
        Airborne
    }
}
