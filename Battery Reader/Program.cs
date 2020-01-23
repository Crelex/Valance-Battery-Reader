using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using mydaemon;

namespace DreamStream.Service
{
    class Program
    {
        public static void Main(string[] args)
        {
            var batteryReader = new DaemonService();
            batteryReader.start();
            Console.ReadKey();

        }
    }
}
