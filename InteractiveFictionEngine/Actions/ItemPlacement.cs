using Keny3rEngine.Engine.EW;

namespace Keny3rEngine.Actions
{
    internal class ItemPlacement
    {
        public static void Take(World world, Entity primary, Entity secondary)
        {
            List<Entity> inventory = world.GetChildren("player");
            if (inventory.Contains(primary))
            {
                Console.WriteLine("This item is already in your inventory");
                return;
            }

            List<Entity> entitiesInRoom = world.GetChildren(world.GetEntity("player").GetStringComponent("in"));
            if (!( entitiesInRoom.Contains(primary) || entitiesInRoom.Contains(secondary) ))
            {
                Console.WriteLine("It is too far");
                return;
            }
            
            bool isInRoom = primary.GetStringComponent("in") == world.GetEntity("player").GetStringComponent("in");
            bool isDeep = secondary != null && primary.GetStringComponent("in") == secondary.GetStringComponent("name");
            if (isInRoom)
            {
                primary.AddComponent("in", "player");
                Console.WriteLine(primary.GetStringComponent("name") + " taken");
            }
            else if (isDeep)
            {
                if (secondary.HasComponent("lock") && secondary.GetStringComponent("lock") == "locked")
                {
                    Console.WriteLine(secondary.GetStringComponent("name") + " is locked");
                }
                else
                {
                    primary.AddComponent("in", "player");
                    Console.WriteLine(primary.GetStringComponent("name") + " taken");
                }
            }
        }

        public static void Drop(World world, Entity primary)
        {
            List<Entity> inventory = world.GetChildren("player");
            if (inventory.Contains(primary))
            {
                primary.AddComponent("in", world.GetEntity("player").GetStringComponent("in"));
                Console.WriteLine(primary.GetStringComponent("name") + " dropped");
            }
            else
            {
                Console.WriteLine("You can not drop this item");
            }
        }

        public static void Inventory(World world)
        {
            List<Entity> inventory = world.GetChildren("player");
            if (inventory.Count > 0)
            {
                Console.WriteLine("You carry a few items:");
                inventory.ForEach(i => { Console.WriteLine(i.GetStringComponent("name")); });
            }
            else
            {
                Console.WriteLine("Your inventory is empty");
            }
        }
    }
}
