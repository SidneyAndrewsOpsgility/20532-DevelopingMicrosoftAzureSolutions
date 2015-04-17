using Contoso.Events.Models;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Contoso.Events.ViewModels
{
    public class HotelsViewModel
    {
        public HotelsViewModel()
        {
            LoadData();
        }

        /// TODO: Exercise 10.3: Connecting a WCF Service to a Service Bus Namespace 
        private void LoadData()
        {
            List<Hotel> eventsList = new List<Hotel>();

            try
            {
                eventsList.AddRange(GetHotels());
            }
            catch (TimeoutException) { eventsList = null; }
            catch (EndpointNotFoundException) { eventsList = null; }
			
			Hotels = eventsList;
        }

        private static IEnumerable<Hotel> GetHotels()
        {
		return Enumerable.Empty<Hotel>();
        }

        public List<Hotel> Hotels { get; set; }
    }
}