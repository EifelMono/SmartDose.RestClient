#region Arguments
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var slnFilePath= new FilePath("SmartDose.RestClient.sln");
var nugetsPath= new DirectoryPath($"./nugets");
var nugetConfigurationZipFile= new FilePath($"./nugets/nuget.{configuration}.zip");
var nugetsConfigurationPath= new DirectoryPath($"./nugets/{configuration}");
var appConfigurationZipFile= new FilePath($"./nugets/app.{configuration}.zip");
var appConfigurationPath= new DirectoryPath($"./SmartDose.RestClientApp/bin/{configuration}");
#endregion

#region Setup/TeadDown
Setup(ctx =>
{
   Information("Executed BEFORE the first task...");
});

Teardown(ctx =>
{
   Information("Executed AFTER the last task.");
});
#endregion

#region Tasks
Task("Clean")
    .Does(() => {
        EnsureDirectoryExists(nugetsPath);
        CleanDirectory(nugetsPath);
});
Task("Restore")
    .Does(() => {
        NuGetRestore(slnFilePath);
});

Task("Build")
    .Does(() => {
        EnsureDirectoryExists(nugetsPath);
        MSBuild (slnFilePath, new MSBuildSettings {
                                    Verbosity = Verbosity.Quiet,
                                    ToolVersion = MSBuildToolVersion.VS2017,
                                    Configuration = configuration,
                                    ArgumentCustomization = args=>args.Append("/p:WarningLevel=0").Append("/t:Build")
        });
});

Task("Zip")
    .Does(() => {
        if (FileExists(nugetConfigurationZipFile))
            DeleteFile(nugetConfigurationZipFile);
        Zip(nugetsConfigurationPath, nugetConfigurationZipFile);
        if (FileExists(appConfigurationZipFile))
            DeleteFile(appConfigurationZipFile);
        Zip(appConfigurationPath, appConfigurationZipFile);
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Zip")
    .Does(() => {
        Information("SmartDose.RestClient!");
        if (TeamCity.IsRunningOnTeamCity)
            Information("Build on TeamCity");
});
#endregion

RunTarget(target);