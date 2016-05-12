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
    public class EntityIndexClient : ObjectIndexClient<Entity>
    {
        private static readonly Lazy<EntityIndexClient> lazy = new Lazy<EntityIndexClient>(() => new EntityIndexClient());

        public static EntityIndexClient Instance { get { return lazy.Value; } }

        EntityIndexClient()
            : base(cid => cid.Mappings(ms => ms
                 .Map<Entity>(m => m.AutoMap()
                    .Properties(ps => ps
                    .String(s => s.Name(c => c.Id).NotAnalyzed())
                     )
                 )
                 ))
        {
        }

    }

}
