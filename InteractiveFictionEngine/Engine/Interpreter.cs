using Keny3rEngine.Engine.EW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Engine
{
    internal class Interpreter
    {
        public delegate void Executor(World world, Entity primary, Entity secondary);
        Dictionary<string, Executor> actions;

        World world;

        public Interpreter(World world, Dictionary<string, Executor> actions)
        {
            this.world = world;
            this.actions = actions;
        }

        public void Interpret(List<Parsed> parsed)
        {
            foreach (Parsed p in parsed)
            {
                if (actions.ContainsKey(p.Action))
                {
                    actions[p.Action](world, p.Primary, p.Secondary);
                }
            }
        }

        public void SetActions(Dictionary<string, Executor> actions)
        {
            this.actions = actions;
        }
    }
}
