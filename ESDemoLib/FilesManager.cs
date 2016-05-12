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
    public class FileIndexClient : ObjectIndexClient<MediaFile>
    {
        private static readonly Lazy<FileIndexClient> lazy = new Lazy<FileIndexClient>(() => new FileIndexClient());

        public static FileIndexClient Instance { get { return lazy.Value; } }

        FileIndexClient()
            : base(cid => cid.Mappings(ms => ms
                 .Map<MediaFile>(m => m.AutoMap()
                    .Properties(ps=>ps
                    .Binary(b=> b.Name(c=>c.Content))//The default mapping is string, but we want binary.
                    .String(s=>s.Name(c=>c.Id).NotAnalyzed())
                    .String(s=>s.Name(c=>c.Name).NotAnalyzed())
                     )
                 )
                 ))
        {
        }
    }


    public class FilesManager : IFilesManager
    {
        public string Create(MediaFile p)
        {
            if (p.Id != Guid.Empty)
                throw new ArgumentException("New file being added must not have ID defined.");

            p.Id = Guid.NewGuid();

            var response = FileIndexClient.Instance.Client.Index(p);
            return response.Id;
        }

        public MediaFile Read(string id)
        {
            var response = FileIndexClient.Instance.Client.Get<MediaFile>(new DocumentPath<MediaFile>(new Id(id)));
            if (response.IsValid && response.Found)
            {
                return response.Source;
            }

            Trace.TraceWarning(response.DebugInformation);
            return null;
        }

        public bool Update(MediaFile p)
        {
            if (p.Id == Guid.Empty)
                throw new ArgumentException("New file being updated must have ID defined.");

            var response = FileIndexClient.Instance.Client.Index(p);
            if (response.IsValid)
                return true;

            Trace.TraceWarning(response.DebugInformation);
            return false;
        }

        public bool Delete(string id)
        {
            var response = FileIndexClient.Instance.Client.Delete<MediaFile>(new DocumentPath<MediaFile>(new Id(id)));
            if (response.IsValid)
                return true;

            Trace.TraceWarning(response.DebugInformation);
            return false;

        }
    }
}
