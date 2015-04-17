using Contoso.Events.Data;
using Contoso.Events.Models;
using System;
using System.Linq;
using System.Data.Entity;

namespace Contoso.Events.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel(string eventKey)
        {
            using (EventsContext context = new EventsContext())
            {
                this.Event = context.Events.SingleOrDefault(e => e.EventKey == eventKey);
                this.Document = context.SignInDocuments.OrderByDescending(d => d.TimeStamp).FirstOrDefault(d => d.EventKey == eventKey);
            }
        }

        public Event Event { get; set; }

        public SignInDocument Document { get; set; }
    }
}