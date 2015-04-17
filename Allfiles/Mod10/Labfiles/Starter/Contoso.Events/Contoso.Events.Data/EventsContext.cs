using Contoso.Events.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Net;

namespace Contoso.Events.Data
{
    public class EventsContext : DbContext
    {
        static EventsContext()
        {
            Database.SetInitializer<EventsContext>(new EventsContextInitializer());
        }

        public EventsContext(IPAddress server)
           : base(GetConnectionStringFromIPAddress(server))
        { }

        private static string GetConnectionStringFromIPAddress(IPAddress server)
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = server.ToString(),
                InitialCatalog = "Contoso.Test",
                UserID = "dbuser",
                Password = "TestPa$$w0rd"
            }.ToString();
        }

        public DbSet<Event> Events { get; set; }
    }
}