using Cake.Common.Build.TeamCity.Data;

var TARGET = Argument("target", Argument ("t", "Default"));

var RX_ANDROID_VERSION = "1.2.1";
var RX_ANDROID_URL = $"http://search.maven.org/remotecontent?filepath=io/reactivex/rxandroid/{RX_ANDROID_VERSION}/rxandroid-{RX_ANDROID_VERSION}.aar";
var RX_JAVA_VERSION = "1.1.6";
var RX_JAVA_URL = $"http://search.maven.org/remotecontent?filepath=io/reactivex/rxjava/{RX_JAVA_VERSION}/rxjava-{RX_JAVA_VERSION}.jar";

var buildInfo = new TeamCityBuildInfo(Context.Environment);
var buildNumber = string.IsNullOrWhiteSpace(buildInfo.Number) ? "0" : buildInfo.Number;

var NUGET_VERSION = $"1.0.{buildNumber}";

Task("externals").Does(() => 
{
    EnsureDirectoryExists("./externals/");
    DownloadFile(RX_ANDROID_URL, "./externals/rxandroid.aar");
    DownloadFile(RX_JAVA_URL, "./externals/rxjava.jar");
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
        Version = NUGET_VERSION
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

RunTarget (TARGET);