using Contoso.Events.Data;
using Contoso.Events.Documents;
using Contoso.Events.Models;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace Contoso.Events.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly SignInDocumentGenerator _documentGenerator = new SignInDocumentGenerator();

        public override void Run()
        {
			Trace.WriteLine("Queue Run Start");
				
            while (true)
            {
                Thread.Sleep(10000);

                Trace.WriteLine("Queue Run Iteration");

                CheckMessages();
            }
        }

        private void CheckMessages()
        {
            using (EventsContext context = new EventsContext())
            {
                var unprocessedMessages = from message in context.SignInDocuments.Include(s => s.Event.Registrations)
                                          where !message.Generated
                                          select message;

                var messages = unprocessedMessages.ToList();

                if (messages.Any())
                {
                    foreach (var message in messages)
                    {
                        HandleMessage(message);

                        context.SaveChanges();
                    }
                }
            }
        }

        private void HandleMessage(SignInDocument message)
        {
            string title = message.Event.Title;

            var names = from registration in message.Event.Registrations
                         where registration.EventKey == message.EventKey
                         select registration.FirstName + " " + registration.LastName;

            using (MemoryStream stream = _documentGenerator.CreateSignInDocument(title, names))
            {
                byte[] data = stream.ToArray();
                message.Document = data;
                message.Generated = true;
                message.TimeStamp = DateTime.Now;
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            return base.OnStart();
        }
    }
}
