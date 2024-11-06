using Keny3rEngine.Engine;
using Keny3rEngine.Engine.EW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Keny3rEngine.Actions
{
    internal class Movement
    {
        public static void Move(World world, string direction)
        {
            Entity player = world.GetEntity("player");
            Entity room = world.GetEntity(player.GetStringComponent("in"));
            string nextRoomName = room.GetStringComponent(direction);
            switch (nextRoomName)
            {
                case "wall":
                    Console.WriteLine("There is a wall");
                    break;
                case "":
                    Console.WriteLine("Could not move there");
                    break;
                default:
                    movePlayer(world, player, nextRoomName);
                    break;
            }
        }

        public static void MoveTo(World world, Entity primary)
        {
            Entity player = world.GetEntity("player");
            string currentRoomName = world.GetEntity(player.GetStringComponent("in")).GetStringComponent("name");
            string nextRoomName = primary.GetStringComponent("name");

            //handle if we are in the room where the player wants to go
            if (currentRoomName == nextRoomName)
            {
                Console.WriteLine("You are already there");
                return;
            }

            //iterate through all directions and if we find the room where the player wants to go, then move there
            foreach (string direction in Direction.Directions)
            {
                if (primary.GetStringComponent(direction) == currentRoomName)
                {
                    movePlayer(world, player, nextRoomName);
                    return;
                }
            }
            Console.WriteLine("Could not move there");
        }

        public static void MoveBack(World world)
        {
            Entity player = world.GetEntity("player");
            string prevRoomName = player.GetStringComponent("prev_room");
            if (prevRoomName != "")
            {
                movePlayer(world, player, prevRoomName);
            }
            else
            {
                Console.WriteLine("Could not go back");
            }
        }

        private static void movePlayer(World world, Entity player, string nextRoomName)
        {
            string currentRoomName = player.GetStringComponent("in");
            string doorName = getDoorBetween(world, currentRoomName, nextRoomName);
            //if there is a door, then handle it
            if (doorName != "")
            {
                Entity door = world.GetEntity(doorName);
                //if it is locked, then tell the player
                if (door.GetStringComponent("lock") == "locked")
                {
                    Console.WriteLine(doorName + " is locked");
                }
                else
                {
                    //if it is unlocked, then move
                    putPlayerInRoom(player, nextRoomName);
                    Console.WriteLine("Moved to " + nextRoomName + " through " + doorName);
                }
            }
            else
            {
                //if there is no door, then simply move there
                putPlayerInRoom(player, nextRoomName);
                Console.WriteLine("Moved to " + nextRoomName);
            }
        }

        private static void putPlayerInRoom(Entity player, string nextRoom)
        {
            //store prev_room!
            player.AddComponent("prev_room", player.GetStringComponent("in"));
            player.AddComponent("in", nextRoom);
        }

        private static string getDoorBetween(World world, string currentRoomName, string nextRoomName)
        {
            Entity currentRoom = world.GetEntity(currentRoomName);
            foreach (string direction in Direction.Directions)
            {
                if (currentRoom.GetStringComponent(direction) == nextRoomName)
                {
                    return currentRoom.GetStringComponent(direction + " door");
                }
            }
            return "";
        }
    }
}
