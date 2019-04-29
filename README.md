Curbside SDK for Xamarin
========================

The Curbside ARRIVE SDK delivers reliable arrival prediction. Battle-tested by millions of customer arrivals in real world commerce, ARRIVE sends an accurate alert before arrival and gives you true visibility into customer ETA and dwell time. ARRIVE hooks easily into a mobile commerce infrastructure and on-site apps or POS for notifications.

This is the source code to Xamarin bindings for the Curbside SDK.  Currently the bindings are created for Xamarin.iOS and Xamarin.Android projects.

There are also some simple bindings for `io.reactivex.rxandroid` which is a dependency of Curbside's Android SDK.

There are sample reference apps [under samples](https://github.com/Curbside/curbside-xamarin-bindings/tree/master/samples) built using the Xamarin bindings for iOS and Android

[Curbside Xamarin Bindings Nuget package](https://www.nuget.org/packages/Curbside/) 

## Building

There is a [Cake](https://cakebuild.net) build script which handles building the project and the nuget package.

The Xamarin.iOS bindings need to be built on a mac.

You can build everything by running:

```
sh build.sh
```

## Arrive SDK Prerequisite
Register for an account at [Curbside Platform](https://dashboard.curbside.com). Sign in and do the following:
* Generate a [usage token](https://dashboard.curbside.com/account?accessTab=tokens&accountTab=access)
* Create a [site](https://dashboard.curbside.com/account?accountTab=sites)

## Running
* Grant permission to use location services
* Add a tracking identifier
* Add a track token
* Add a [site identifier](https://dashboard.curbside.com/account?accountTab=sites) that was created on the Curbside Platform.
* Start track
* Go on a test drive

## View User Trip
* Check [ARRIVE dashboard](https://dashboard.curbside.com) to see your current location and to note that your status changed from In-Transit to Arrived.
* Use the monitor app to view detailed information about your arrival.

## Quick Start Guides
- [iOS App Quick Start](https://developer.curbside.com/en/docs/getting-started/quickstart-ios-app/)
- [Android App Quick Start](https://developer.curbside.com/en/docs/getting-started/quickstart-android-app/)
The documentation is mostly in Objective-C, Swift and Java, but the Xamarin/C# APIs are almost 1:1 with the native APIs,so the
docs should still be quite useful.


## License
See the repo root directory for licensing information.
