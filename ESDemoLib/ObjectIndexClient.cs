using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Fonlow.Demo.Contracts;
using System.Diagnostics;

namespace ESDemoLib
{
    public abstract class ObjectIndexClient<T> where T : class
    {
        IndexName EntityIndexName;

        const string ES_URI = "http://localhost:9200";

        ConnectionSettings settings;

        public ElasticClient Client { get; private set; }

        public bool IndexReady { get; private set; }


        public ObjectIndexClient(Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null)
        {
            CreateClient(selector);
        }

        object indexReadyLock = new object();

        void CreateClient(Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null)
        {
            lock(indexReadyLock)
            {
                if (IndexReady)
                    return;


                var uri = new Uri(ES_URI);
                var typeOfEntity = typeof(T);
                var defaultIndexType = typeOfEntity.ToString().ToLower();

                settings = new ConnectionSettings(uri);
                settings.DefaultIndex(defaultIndexType);
                settings.MapDefaultTypeNames(m =>
                {
                    m.Add(typeOfEntity, defaultIndexType);
                });

                Client = new ElasticClient(settings);
                EntityIndexName = new IndexName() { Name = defaultIndexType, Type = typeOfEntity };
                var response = Client.CreateIndex(EntityIndexName, selector);
                if (response.IsValid)
                {
                    IndexReady = true;
                    System.Diagnostics.Trace.TraceInformation($"Index {EntityIndexName} created.");
                }
                else
                {
                    if (response.ServerError.Error.Type == "index_already_exists_exception")
                    {
                        IndexReady = true;
                        System.Diagnostics.Debug.WriteLine($"Index {EntityIndexName} had been created.");
                    }
                    else
                    {
                        IndexReady = false;
                        System.Diagnostics.Trace.TraceError(response.DebugInformation);
                    }
                }
            }
        }

    }


}
