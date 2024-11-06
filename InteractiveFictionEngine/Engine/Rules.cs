using Keny3rEngine.Engine.EW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Engine
{
    internal class Rules
    {
        public delegate void Rule(World world);
        List<Rule> rules;

        public Rules()
        {
            rules = new ();
        }

        public void AddRule(Rule rule)
        {
            rules.Add(rule);
        }

        public void ExecuteRules(World world)
        {
            rules.ForEach(r => r(world));
        }
    }
}
