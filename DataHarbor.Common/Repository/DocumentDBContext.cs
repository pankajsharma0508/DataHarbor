using Raven.Client.Documents;
using System.Security.Cryptography.X509Certificates;

namespace DataHarbor.Repository
{
    public class DocumentDBContext
    {
        private static readonly Lazy<IDocumentStore> documentStore = new Lazy<IDocumentStore>(() =>
        {
            var certPath = "C:\\RavenDB\\certificate\\A\\cluster.server.certificate.dataharbor.pfx";
            var certificate = new X509Certificate2(certPath);

            var store = new DocumentStore
            {
                Urls = new[] { "https://a.dataharbor.ravendb.community" }, // Your RavenDB server URL
                Database = "Mailbox-Declarations",
                Certificate = certificate
            };
            store.Initialize();
            return store;
        });

        public static IDocumentStore DocumentStore => documentStore.Value;
    }
}
