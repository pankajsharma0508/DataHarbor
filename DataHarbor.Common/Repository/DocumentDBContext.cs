﻿using Raven.Client.Documents;

namespace DataHarbor.Repository
{
    public class DocumentDBContext
    {
        private static readonly Lazy<IDocumentStore> documentStore = new Lazy<IDocumentStore>(() =>
        {
            var store = new DocumentStore
            {
                Urls = new[] { "http://127.0.0.1:8080" }, // Your RavenDB server URL
                Database = "Mailbox-Declarations",
            };
            store.Initialize();
            return store;
        });

        public static IDocumentStore DocumentStore => documentStore.Value;
    }
}
