Curbside SDK for Xamarin
========================

This is the source code to Xamarin bindings for the Curbside SDK.  Currently the bindings are created for Xamarin.iOS and Xamarin.Android projects.

There are also some simple bindings for `io.reactivex.rxandroid` which is a dependency of Curbside's Android SDK.

## Building

There is a [Cake](https://cakebuild.net) build script which handles building the project and the nuget package.

The Xamarin.iOS bindings need to be built on a mac.

You can build everything by running:

```
sh build.sh
```

