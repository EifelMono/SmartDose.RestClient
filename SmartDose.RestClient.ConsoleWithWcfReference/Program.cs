using System;
using System.Threading;
using System.Threading.Tasks;
using SmartDose.Core.Extensions;

namespace SmartDose.RestClient.ConsoleWithWcfReference
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var serviceClient = new MasterData1000.ServiceClient("net.tcp://localhost:10000/MasterData/"))
            {
                serviceClient.OnClientEvent += (e) =>
                {
                    Console.WriteLine($"event=>{e.ToString()}");
                };

                while (!serviceClient.IsOpened)
                {
                    Thread.Sleep(100);
                    Console.WriteLine("not Connected");
                }
                Console.WriteLine("Connected");

                Console.WriteLine("g => MedicinesGetMedcineByIdentifierAsync(\"1\")");
                Console.WriteLine("m => Query Medicine");
                Console.WriteLine("c => Query Customer");
                Console.WriteLine("e => exit");
                bool running = true;
                while (running)
                {
                    var key = Console.ReadKey();
                    switch (key.KeyChar)
                    {
                        case 'e':
                        case 'E':
                            running = false;
                            break;
                        case 'g':
                        case 'G':
                            {
                                Console.WriteLine("MedicinesGetMedcineByIdentifierAsync");
                                if (await serviceClient.MedicinesGetMedcineByIdentifierAsync("1") is var med && med.IsOk)
                                {
                                    Console.WriteLine($"MedicinesGetMedcineByIdentifierAsync Data={med.Data.ToJson()}");
                                }
                                else
                                    Console.WriteLine($"MedicinesGetMedcineByIdentifierAsync Error Result='{med.Status}' ({med.StatusAsInt})");
                                break;
                            }
                        case 'm':
                        case 'M':
                            {
                                Console.WriteLine("Query Medicine");
                                if (await serviceClient
                                            .NewQuery<MasterData1000.Medicine>()
                                            .Where(m => m.Name == "med1")
                                            .FirstOrDefaultAsync() is var med && med.IsOk)
                                {
                                    Console.WriteLine($"Query medicine Data={med.Data.ToJson()}");
                                }
                                else
                                    Console.WriteLine($"Query medicine Error Result='{med.Status}' ({med.StatusAsInt})");
                                break;
                            }
                        case 'c':
                        case 'C':
                            {
                                Console.WriteLine("Query Customer");
                                if (await serviceClient
                                            .NewQuery<MasterData1000.Customer>()
                                            .Where(m => m.Name == "2dd")
                                            .FirstOrDefaultAsync() is var med && med.IsOk)
                                {
                                    Console.WriteLine($"Query Customer Data={med.Data.ToJson()}");
                                }
                                else
                                    Console.WriteLine($"Query Customer Error Result='{med.Status}' ({med.StatusAsInt})");
                                break;
                            }
                    }
                }
                Console.WriteLine("THE END!");
            }
        }
    }
}
