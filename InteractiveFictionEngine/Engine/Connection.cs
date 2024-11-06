using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Engine
{
    internal class Connection
    {
        public string Name;
        public List<string> Synonyms;

        public Connection(string name, List<string> synonyms = null)
        {
            Name = name;
            Synonyms = synonyms == null ? new () : synonyms;
        }
    }
}
