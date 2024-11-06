using Keny3rEngine.Engine.EW;
using System.Numerics;

namespace Keny3rEngine.Actions
{
    internal class Sight
    {
        public static void Look(World world)
        {
            Entity player = world.GetEntity("player");
            Entity room = world.GetEntity(player.GetStringComponent("in"));

            //if the current room is dark, then tell the player and end action
            if (room.HasComponent("dark"))
            {
                Console.WriteLine(room.GetStringComponent("dark"));
            }
            else
            {
                //otherwise describe it as normal
                DescribeRoom(world, room);
            }
        }

        public static void Examine(World world, Entity primary)
        {
            if (world.GetChildren(world.GetEntity("player").GetStringComponent("in")).Contains(primary) || world.GetChildren("player").Contains(primary))
            {
                if (!primary.HasComponent("description"))
                {
                    //if there is no description, then tell this
                    Console.WriteLine("There is nothing special about " + primary.GetStringComponent("name"));
                }
                else
                {
                    //describe the object
                    Console.WriteLine(primary.GetStringComponent("description"));
                    //give information about the items inside it
                    if (primary.HasComponent("lock") && primary.GetStringComponent("lock") == "locked")
                    {
                        Console.WriteLine("It is locked");
                    }
                    else
                    {
                        List<Entity> items = world.GetChildren(primary.GetStringComponent("name"));
                        if (items.Count > 0)
                        {
                            Console.WriteLine("It contains a few items:");
                            items.ForEach(i => Console.WriteLine(i.GetStringComponent("name")));
                        }
                    }
                }
            }
            else
            {
                //if the item is not in the current room
                Console.WriteLine("It is too far away");
            }
        }

        public static void Light(World world, Entity primary)
        {
            Entity player = world.GetEntity("player");
            string roomName = player.GetStringComponent("in");
            Entity room = world.GetEntity(roomName);

            //if it is dark then describe it as normal (since now the player can see here)
            if (room.HasComponent("dark"))
            {
                Console.WriteLine("You light " + primary.GetStringComponent("name"));
                DescribeRoom(world, room);
            }
            else
            {
                //otherwise tell the player that this room is already bright
                Console.WriteLine("You can already see here");
            }
        }

        private static void DescribeRoom(World world, Entity room)
        {
            Console.WriteLine(room.GetStringComponent("description"));
            List<Entity> inventory = world.GetChildren(room.GetStringComponent("name"));
            inventory.Remove(world.GetEntity("player"));

            //give information about the neighbours of the current room
            foreach (string direction in Direction.Directions)
            {
                if (room.HasComponent(direction))
                {
                    Console.WriteLine("You see " + room.GetStringComponent(direction) + " " + direction);
                }
            }

            //give information about the items in the current room
            if (inventory.Count > 0)
            {
                Console.WriteLine("There are a few things here:");
                inventory.ForEach(x => Console.WriteLine(x.GetStringComponent("name")));
            }
        }
    }
}
