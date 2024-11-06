using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Engine
{
    internal class Command
    {
        public string FuncName;
        public List<string> Synonyms;
        public bool MustPrimary;
        public List<string> PrimaryComponents;
        public bool MustSecondary;
        public string Connection;
        public List<string> SecondaryComponents;

        public Command()
        {
            MustPrimary = false;
            MustSecondary = false;
            Synonyms = new ();
            PrimaryComponents = new ();
            SecondaryComponents = new ();
        }
    }
}
