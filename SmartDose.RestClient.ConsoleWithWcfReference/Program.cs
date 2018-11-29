using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using MasterData1000;

namespace SmartDose.RestClient.ConsoleWithWcfReference
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var serviceClient = new ServiceClient("net.tcp://lwdeu08dtk2ph2:10000/MasterData/").RunStart())
            {

                {
                    var query = serviceClient
                            .NewQuery<MasterData1000.Medicine>()
                            .Where(m => m.Name == "med1")
                            .OrderBy(m => m.Manufacturer.Name)
                            .Paging(1, 1000);
                }


                {
                    var result = await serviceClient
                                .NewQuery<MasterData1000.Medicine>()
                                .Where(m => m.Name == "med1")
                                .FirstOrDefaultAsync();
                }

                {
                    var result = await serviceClient
                                .NewQuery<MasterData1000.Medicine>()
                                .Where(m => m.Name == "med1")
                                .ToListAsync();
                }

                // var mew= serviceClient.BuildQuery<Medicine>()
                //var med = await QueryBuilder
                //    .For<Medicine>()
                //    .Where(m => m.Name == "med1")
                //    
                //    .ExecuteReturnAsItemAsync();
            }

            Console.WriteLine("......!");
        }

    }
}
