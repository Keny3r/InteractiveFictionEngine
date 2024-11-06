using Keny3rEngine.Engine.EW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Actions
{
    internal class Locks
    {
        public static void Lock(World world, Entity primary, Entity secondary)
        {
            LockMechanism(world, primary, secondary, false);
        }

        public static void UnLock(World world, Entity primary, Entity secondary)
        {
            LockMechanism(world, primary, secondary, true);
        }

        private static void LockMechanism(World world, Entity primary, Entity secondary, bool isUnlocking)
        {
            string lockName = primary.GetStringComponent("name");

            if (!primary.HasComponent("lock"))
            {
                Console.WriteLine("It does not have a lock on it");
                return;
            }

            if (primary.GetStringComponent("lock") == "open")
            {
                Console.WriteLine(lockName + " is already unlocked");
                return;
            }

            //if the player tells what item to use, then use that item
            if (secondary != null)
            {
                if (secondary.GetStringComponent("key") == lockName)
                {
                    primary.AddComponent("lock", isUnlocking ? "open" : "locked");
                    Console.WriteLine(isUnlocking ? "Unlocked " : "Locked " + lockName);
                    return;
                }
                else
                {
                    Console.WriteLine(secondary.GetStringComponent("name") + " is not the key to " + lockName);
                    return;
                }
            }
            //otherwise try to find the key in the player inventory
            else
            {
                List<Entity> items = world.GetChildren("player");
                foreach (Entity item in items)
                {
                    if (item.GetStringComponent("key") == lockName)
                    {
                        primary.AddComponent("lock", isUnlocking ? "open" : "locked");
                        Console.WriteLine(isUnlocking ? "Unlocked " : "Locked " + lockName);
                        return;
                    }
                }
                Console.WriteLine("You do not have the key to " + lockName);
            }
        }
    }
}
