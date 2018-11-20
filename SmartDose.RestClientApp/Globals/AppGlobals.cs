using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using SmartDose.RestClient;
using SmartDose.Core;
using SmartDose.WcfClient;
using SmartDose.Core.Extensions;

namespace SmartDose.RestClientApp.Globals
{
    public static class AppGlobals
    {
        public static void Init()
        {
            Configuration.Load();
            if (!Configuration.FileExists)
                Configuration.Save();
            var done = WcfClientGlobals.CopyWcfClientAssembliesToAppExecutionDirectory();
            "WcfClients copied".LogInformation();
            done.Copied.ForEach(d => d.LogInformation());
            "WcfClients not copied".LogInformation();
            done.NotCopied.ForEach(d => d.LogInformation());
        }

        public static class Configuration
        {
            public static string FileName => Path.Combine(CoreGlobals.DataBinDirectory, $"{CoreGlobals.AppName}.json");

            public static ConfigurationData Data = new ConfigurationData();

            public static bool FileExists
                => File.Exists(FileName);

            public static void Load()
            {
                try
                {
                    if (FileExists)
                        Data = File.ReadAllText(FileName).ToExpandableObjectFromJson<ConfigurationData>();
                    else
                    {
                        Data.UrlV1 = "http://localhost:6040/SmartDose";
                        // http://localhost:6040/SmartDose/Customers


                        Data.UrlV2 = "http://localhost:56040/SmartDose/V2.0";
                        // http://localhost:56040/SmartDose/V2.0/MasterData/Customers
                        // http://localhost:56040/SmartDose/V2.0/swagger/docs/v2

                        Data.UrlTimeSpan = TimeSpan.FromSeconds(100);

                        Data.WcfTimeSpan = TimeSpan.FromSeconds(100);

                        Data.WcfClients = new System.Collections.Generic.List<WcfItem>
                        {
                            new WcfItem
                            {
                                Group= "Server[Business]",
                                ConnectionString="net.tcp://localhost:10000/MasterData/",
                                ConnectionStringUse= "net.tcp://localhost:10000/MasterData/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server[Business]",
                                ConnectionString="net.tcp://localhost:10000/Settings/",
                                ConnectionStringUse= "net.tcp://localhost:10000/Settings/",
                                Active=true
                            },
                              new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9000/SDMC/",
                                ConnectionStringUse= "net.tcp://localhost:9000/SDMC/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9002/MasterData/",
                                ConnectionStringUse= "net.tcp://localhost:9002/MasterData/",
                                Active=true
                            },
                             new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9003/TrayHandling/",
                                ConnectionStringUse= "net.tcp://localhost:9003/TrayHandling/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9004/CanisterHandling/",
                                ConnectionStringUse= "net.tcp://localhost:9004/CanisterHandling/",
                                Active=true
                            },
                              new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9005/Reporting/",
                                ConnectionStringUse= "net.tcp://localhost:9005/Reporting/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9006/Inventory/",
                                ConnectionStringUse= "net.tcp://localhost:9006/Inventory/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9008/Production/",
                                ConnectionStringUse= "net.tcp://localhost:9008/Production/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9009/Settings/",
                                ConnectionStringUse= "net.tcp://localhost:9009/Settings/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9013/PouchDesignHandling/",
                                ConnectionStringUse= "net.tcp://localhost:9013/PouchDesignHandling/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9014/Deblistering/",
                                ConnectionStringUse= "net.tcp://localhost:9014/Deblistering/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9015/DualInspection/",
                                ConnectionStringUse= "net.tcp://localhost:9015/DualInspection/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9016/HardwareHandling/",
                                ConnectionStringUse= "net.tcp://localhost:9016/HardwareHandling/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9017/PouchSequenceRuleHandling/",
                                ConnectionStringUse= "net.tcp://localhost:9017/PouchSequenceRuleHandling/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9022/Production/",
                                ConnectionStringUse= "net.tcp://localhost:9022/Production/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9024/Inventory/",
                                ConnectionStringUse= "net.tcp://localhost:9024/Inventory/",
                                Active=true
                            },
                            new WcfItem
                            {
                                Group= "Server",
                                ConnectionString="net.tcp://localhost:9021/MasterData/",
                                ConnectionStringUse= "net.tcp://localhost:9021/MasterData/",
                                Active=true
                            }
                        };
                    }

                    RestClientGlobals.UrlV1 = Data.UrlV1;
                    RestClientGlobals.UrlV2 = Data.UrlV2;
                    RestClientGlobals.UrlTimeSpan = Data.UrlTimeSpan;
                }
                catch (Exception ex)
                {
                    Data = new ConfigurationData();
                    Debug.WriteLine(ex);
                }
            }

            public static void Save()
            {
                try
                {
                    File.WriteAllText(FileName, Data.ToJsonFromExpandableObject());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

        }
    }
}
