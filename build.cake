#addin nuget:?package=SharpZipLib
#addin nuget:?package=Cake.Compression

using Cake.Common.Build.TeamCity.Data;

var target = Argument("target", Argument ("t", "Default"));

var buildNumber = EnvironmentVariable("BUILD_BUILDNUMBER") ?? "0";
var nugetVersion = $"3.2.{buildNumber}";

var externalVersions = new
{
    RxAndroid = "1.2.1",
    RxJava = "1.1.6",
    CurbsideIos = "3.27",
    CurbsideAndroid = "3.2.4"
};

Task("externals").Does(() => 
{
    EnsureDirectoryExists("./externals/");
    CleanDirectories("./externals");

    EnsureDirectoryExists("./tmp/");
    CleanDirectories("./tmp");

    DownloadFile(
        $"http://search.maven.org/remotecontent?filepath=io/reactivex/rxandroid/{externalVersions.RxAndroid}/rxandroid-{externalVersions.RxAndroid}.aar", 
        "./externals/rxandroid.aar");

    DownloadFile(
        $"http://search.maven.org/remotecontent?filepath=io/reactivex/rxjava/{externalVersions.RxJava}/rxjava-{externalVersions.RxJava}.jar", 
        "./externals/rxjava.jar");

    DownloadFile(
        $"http://cs-web-downloads.s3-website-us-west-2.amazonaws.com/SDKs/curbside-android-sdk-release.{externalVersions.CurbsideAndroid}.aar",
        "./externals/curbside-android-sdk.aar");

    DownloadFile(
        $"http://cs-web-downloads.s3-website-us-west-2.amazonaws.com/SDKs/Curbside.framework.v{externalVersions.CurbsideIos}.tar.gz",
        "./tmp/curbside-ios.tar.gz");
    GZipUncompress("./tmp/curbside-ios.tar.gz", "./tmp/curbside-ios");
    CopyDirectory("./tmp/curbside-ios/Curbside/Curbside.framework", "./externals/Curbside.framework");
});

Task("libs").IsDependentOn("externals").Does(() => 
{
    NuGetRestore("./Curbside.Xamarin.Bindings.sln");
    MSBuild("./Curbside.Xamarin.Bindings.sln", c => c.Configuration = "Release");
});

Task("nuget").IsDependentOn("libs").Does(() => 
{
    NuGetPack("./Curbside.Xamarin.nuspec", new NuGetPackSettings 
    {
        Version = nugetVersion
    });

    NuGetPack("./Curbside.Xamarin.nuspec", new NuGetPackSettings 
    {
        Version = $"{nugetVersion}-beta"
    });
});

Task("clean").Does(() => 
{
    if (DirectoryExists("./externals/"))
        DeleteDirectory("./externals", true);

    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
});

Task("Default").IsDependentOn("nuget");

RunTarget(target);