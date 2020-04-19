using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using mydaemon;
using System.Collections.Generic;
using InfluxDB.Client;
using InfluxDB.Client.Writes;
using InfluxDB.Client.Api.Domain;
using BatteryDialog;

// TODO: configure this
// https://www.app-metrics.io/getting-started/
//
namespace DreamStream.Service
{
    class Program
    {
        private static readonly char[] Token = "H7BIrtdhFSiqnOTn_1scnYi2YbAuVcwGeil19dVw3Z1CTaPv_U5tfIO7I5qhn2N3k8K4aT3k9-bJgUWeHjVCnA==".ToCharArray();

        public static void Main(string[] args)
        {
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:9999", Token);

            using (var writeApi = influxDBClient.GetWriteApi())
            {
        
                var batteryReader = new DaemonService(writeApi);
                batteryReader.start();
                Console.ReadKey();
            }
        }
    }
}
