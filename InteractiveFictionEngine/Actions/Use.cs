using Keny3rEngine.Engine.EW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Keny3rEngine.Engine.Interpreter;

namespace Keny3rEngine.Actions
{
    internal class Use
    {
        public static void UseAction(World world, Entity primary, Entity secondary, Dictionary<string, Executor> actions)
        {
            List<Entity> inventory = world.GetChildren("player");
            if (!inventory.Contains(primary))
            {
                Console.WriteLine("You can only use items in your inventory");
                return;
            }
            string useAction = primary.GetStringComponent("use");
            if (actions.ContainsKey(useAction))
            {
                actions[useAction](world, primary, secondary);
            }
            else
            {
                Console.WriteLine("This item is not usable\n" +
                    "Try to specify what you want to do with it");
            }
        }
    }
}
