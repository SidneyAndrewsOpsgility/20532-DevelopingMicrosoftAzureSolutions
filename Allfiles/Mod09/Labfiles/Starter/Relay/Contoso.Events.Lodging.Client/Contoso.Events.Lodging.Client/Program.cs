using Contoso.Events.Models;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Events.Lodging.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var hotels = GetHotels();

            foreach (var hotel in hotels)
            {
                Console.WriteLine(hotel.Name);
            }

            Console.ReadKey();
        }

        private static IEnumerable<Hotel> GetHotels()
        {
            Uri serviceUri = new Uri("net.tcp://localhost:8000/lodging", UriKind.Absolute);

            ChannelFactory<ILodgingService> cf = new ChannelFactory<ILodgingService>(
                new NetTcpBinding(),
                new EndpointAddress(serviceUri)
            );

            ILodgingService ch = cf.CreateChannel();
            return ch.GetHotels();
        }
    }
}