using Contoso.Events.Data;
using Contoso.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Events.ViewModels
{
    public class EventsGridViewModel
    {
        public EventsGridViewModel(IPAddress server)
        {
            IPAddress = server;

            try
            {
                using (EventsContext context = new EventsContext(server))
                {
                    this.Events = context.Events.OrderByDescending(e => e.StartTime).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }
        }

        public IPAddress IPAddress { get; private set; }

        public IList<Event> Events { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}