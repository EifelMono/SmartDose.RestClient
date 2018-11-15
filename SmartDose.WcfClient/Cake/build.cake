#addin "Cake.Figlet"

using System.Xml.Linq;

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var connectionString = Argument("connection", "net.tcp://localhost:10000/MasterData/");

var assemblyFramework = Argument("assemblyframework", "netstandard2.0");


var wcfClientDirectory= MakeAbsolute(new DirectoryPath("."));
var connectionName= ConnectionStringToConnectionName(connectionString);
var connectionDirectory= MakeAbsolute(wcfClientDirectory.Combine(new DirectoryPath(connectionName)));
var connectionCsproj= new FilePath(System.IO.Path.Combine(connectionDirectory.ToString(), $"{connectionName}.csproj"));



// Setup(ctx =>
// {
// });

// Teardown(ctx =>
// {
// });

string ConnectionStringToConnectionName(string connectionString)
{
   var result= connectionString.Split(new[] { "//"}, StringSplitOptions.None)[1].Replace(":", "_").Replace("/", "_");
   if (result.EndsWith("_"))
      result= result.Substring(0, result.Length - 1);
   return result;
}

void GotoConnectionDirectory()
  => System.IO.Directory.SetCurrentDirectory(connectionDirectory.ToString());

void GotoWcfClientDirectory()
  => System.IO.Directory.SetCurrentDirectory(wcfClientDirectory.ToString());


void DotNet(string args)
{
   StartProcess("dotnet", new  ProcessSettings {
      Arguments= args,
      WorkingDirectory= connectionDirectory
   });
   GotoWcfClientDirectory();
}

void EnsureDirectoryDelete(string dir)
{
   if (DirectoryExists(dir))
         DeleteDirectory(dir, new DeleteDirectorySettings {
            Recursive= true,
            Force= true
         });
}

void EnsureDirectoryDelete(DirectoryPath dir)
   => EnsureDirectoryDelete(dir.ToString());


//<ItemGroup>
//  <PackageReference Include="dotnet-svcutil" Version="1.*" />
// </ItemGroup>
void ProjectChangeSvcUtilToCli()
{
   var xDoc = XDocument.Load(connectionCsproj.ToString());
   foreach(var x in xDoc.Descendants("PackageReference"))
   {
      if (x.Attribute("Include").Value == "dotnet-svcutil")
      {
         x.Name= "DotNetCliToolReference";
      }
   }
   xDoc.Save(connectionCsproj.ToString());
}

void ProjectChangeTargetFramework()
{
   var xDoc = XDocument.Load(connectionCsproj.ToString());
   foreach(var x in xDoc.Descendants("TargetFramework"))
      x.Value= assemblyFramework;
   xDoc.Save(connectionCsproj.ToString());
}

void  AddReferencecsExpandableObjectConverter()
{
   // [TypeConverter(typeof(ExpandableObjectConverter))]
   var referencecsFileName= System.IO.Path.Combine(connectionDirectory.ToString(), "ServiceReference1", "Reference.cs");
   var sb= new StringBuilder();
   sb.AppendLine("using System.ComponentModel;");
   foreach(var l in System.IO.File.ReadAllLines(referencecsFileName))
   {
      if (l.Contains("public class") || l.Contains("public partial class"))
         sb.AppendLine("    [TypeConverter(typeof(ExpandableObjectConverter))]");
      sb.AppendLine(l);
   } 
   System.IO.File.WriteAllText(referencecsFileName,  sb.ToString());
}

Task("SetupDirectory")
  .Does(()=> {
     EnsureDirectoryDelete(connectionDirectory);
     EnsureDirectoryExists(connectionDirectory);
  });

Task("SetupProject")
  .IsDependentOn("SetupDirectory")
  .Does(()=> {
      DotNet("new console");
  });

Task("SetupSvcUtil")
  .IsDependentOn("SetupProject")
  .Does(()=> {
     DotNet("add package dotnet-svcutil");
     ProjectChangeSvcUtilToCli();
     ProjectChangeTargetFramework();
     DotNet("restore");
  });

Task("SetupFromWcfService")
  .IsDependentOn("SetupSvcUtil")
  .Does(()=> {
     DotNet($"svcutil {connectionString} -n *,{connectionName}");
     AddReferencecsExpandableObjectConverter();
  });

Task("BuildProject")
  .IsDependentOn("SetupFromWcfService")
  .Does(()=> {
     DotNet("restore");
     DotNet($"build -c {configuration}");
  });

Task("Info")
.Does(() => {
   Console.ForegroundColor= ConsoleColor.Black;
   Console.BackgroundColor= ConsoleColor.White;
   Console.WriteLine(new string ('-', 100));
   Console.WriteLine(connectionString);
   Console.WriteLine(connectionName);
   Console.WriteLine(connectionDirectory);
   Console.WriteLine(connectionCsproj);
   Console.WriteLine(assemblyFramework);
   Console.WriteLine(configuration);
   Console.WriteLine(new string ('-', 100));
   Console.ForegroundColor= ConsoleColor.White;
   Console.BackgroundColor= ConsoleColor.Black;
});

Task("Cleanup")
.Does(() => {
   var objDirectory= connectionDirectory.Combine(new DirectoryPath("obj"));
   EnsureDirectoryDelete(objDirectory);
   var binDirectory= connectionDirectory.Combine(new DirectoryPath("bin"));
   var debugDirectory= binDirectory.Combine(new DirectoryPath("Debug"));
   EnsureDirectoryDelete(debugDirectory);
   var releaseDirectory= binDirectory.Combine(new DirectoryPath("Release"));
   foreach(var dir in GetDirectories(releaseDirectory.ToString()))
   {
      var name= System.IO.Path.GetFileName(dir.ToString()).ToLower();
      if (name != assemblyFramework)
         EnsureDirectoryDelete(name);
   }
   foreach(var file in GetFiles($"{releaseDirectory}/**/*"))
   {
      if (System.IO.Path.GetExtension(file.ToString()).ToLower()!=".dll")
         DeleteFile(file);
   }
});

Task("Default")
   .IsDependentOn("Info")
   .IsDependentOn("BuildProject")
   .IsDependentOn("Cleanup")
.Does(() => {

});

Task("Defaults")
.Does(() => {
   foreach(var l in System.IO.File.ReadAllLines("connections.txt"))
   {
      connectionString= l;
      wcfClientDirectory= MakeAbsolute(new DirectoryPath("."));
      connectionName= ConnectionStringToConnectionName(connectionString);
      connectionDirectory= MakeAbsolute(wcfClientDirectory.Combine(new DirectoryPath(connectionName)));
      connectionCsproj= new FilePath(System.IO.Path.Combine(connectionDirectory.ToString(), $"{connectionName}.csproj"));
      RunTarget("Default");
   }
});

try {
   RunTarget(target);
}
catch(Exception)
{
   Information("");
   Information(new string ('-', 100));
   Error(connectionName);
   Information(new string ('-', 100));
   Information(connectionString);
   Information(connectionDirectory);
   Information(connectionCsproj);
   Information(assemblyFramework);
   Information(configuration);
   Information(new string ('-', 100));
}

