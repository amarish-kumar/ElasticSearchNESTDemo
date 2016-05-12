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

    public class CompaniesManager : ICompaniesManager
    {
        public string Create(Company p)
        {
            if (p.Id != Guid.Empty)
                throw new ArgumentException("New Entity being added must not have ID defined.");

            p.Id = Guid.NewGuid();

            var response = EntityIndexClient.Instance.Client.Index(p);
            return response.Id;
        }

        public Company Read(string id)
        {
            var response = EntityIndexClient.Instance.Client.Get<Company>(new DocumentPath<Company>(new Id(id)));
            if (response.IsValid && response.Found)
            {
                return response.Source;
            }

            Trace.TraceWarning(response.DebugInformation);
            return null;
        }

        public bool Update(Company p)
        {
            if (p.Id == Guid.Empty)
                throw new ArgumentException("New company being updated must have ID defined.");

            var response = EntityIndexClient.Instance.Client.Index(p);
            if (response.IsValid)
                return true;

            Trace.TraceWarning(response.DebugInformation);
            return false;
        }

        public bool Delete(string id)
        {
            var response = EntityIndexClient.Instance.Client.Delete<Company>(new DocumentPath<Company>(new Id(id)));
            if (response.IsValid)
                return true;

            Trace.TraceWarning(response.DebugInformation);
            return false;

        }
    }
}
