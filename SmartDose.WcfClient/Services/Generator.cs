using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.WcfClient.Services
{
    public class Generator
    {
    }

    /*
    C:\ProgramData\Rowa\Bin\SmartDose.RestClientApp\WcfClients
    echo net.tcp://lwdeu08dtk2ph2:10000/MasterData/
    remove last backslash 
    md net_tcp___lwdeu08dtk2ph2_10000_MasterData
    cd net_tcp___lwdeu08dtk2ph2_10000_MasterData
    dotnet new console
    dotnet add package dotnet-svcutil
    rename in csproj PackageReference=> DotNetCliToolReference 
    rename Framework => net471
    dotnet restore
    dotnet svcutil net.tcp://127.0.0.1:9002/MasterData -n *,net_tcp___lwdeu08dtk2ph2_10000_MasterData
    Add (ExpandableObjectConverter) and more????
    dotnet build
    path depends <TargetFramework>netcoreapp2.1</TargetFramework> of csproj
    bin \debug\netcoreapp2.1\net_tcp___lwdeu08dtk2ph2_10000_MasterData.dll
    */
}
