using Keny3rEngine.Actions;
using Keny3rEngine.Engine;
using Keny3rEngine.Engine.EW;
using static Keny3rEngine.Engine.Interpreter;

namespace Keny3rEngine
{
    internal class DemoGame : Game
    {
        override public void Init()
        {
            Console.WriteLine("INTERACTIVE FICTION created by Keny3r with <3");
            Console.WriteLine("You wake up in a room without any memories of your past\n" +
                "The only thing you can remember is that your name is Karen\n" +
                "Your head aches and you are still kind of dizzy\n" +
                "It might be a good idea to look around");
            Console.WriteLine();
        }

        protected override Dictionary<string, Executor> CreateActions()
        {
            Dictionary<string, Executor> actions = new ();

            actions["north"] = (w, p, s) => Movement.Move(w, "north");
            actions["south"] = (w, p, s) => Movement.Move(w, "south");
            actions["west"] = (w, p, s) => Movement.Move(w, "west");
            actions["east"] = (w, p, s) => Movement.Move(w, "east");
            actions["up"] = (w, p, s) => Movement.Move(w, "up");
            actions["down"] = (w, p, s) => Movement.Move(w, "down");
            actions["in"] = (w, p, s) => Movement.Move(w, "inside");
            actions["out"] = (w, p, s) => Movement.Move(w, "outside");
            actions["moveto"] = (w, p, s) => Movement.MoveTo(w, p);
            actions["moveback"] = (w, p, s) => Movement.MoveBack(w);

            actions["look"] = (w, p, s) => Sight.Look(w);
            actions["examine"] = (w, p, s) => Sight.Examine(w, p);
            actions["light"] = (w, p, s) => Sight.Light(w, p);

            actions["inventory"] = (w, p, s) => ItemPlacement.Inventory(w);
            actions["take"] = (w, p, s) => ItemPlacement.Take(w, p, s);
            actions["drop"] = (w, p, s) => ItemPlacement.Drop(w, p);

            actions["lock"] = (w, p, s) => Locks.Lock(w, p, s);
            actions["unlock"] = (w, p, s) => Locks.UnLock(w, p, s);

            actions["use"] = (w, p, s) => Use.UseAction(w, p, s, actions);

            return actions;
        }

        protected override Commands CreateCommands()
        {
            Commands commands = new Commands();

            commands.CommandBuilder()
                .SetFuncName("north")
                .AddSynonyms(new List<string> { "go north", "go to north", "walk north", "move north", "n" });
            commands.CommandBuilder()
                .SetFuncName("south")
                .AddSynonyms(new List<string> { "go south", "go to south", "walk south", "move south", "s" });
            commands.CommandBuilder()
                .SetFuncName("west")
                .AddSynonyms(new List<string> { "go west", "go to west", "walk west", "move west", "w" });
            commands.CommandBuilder()
                .SetFuncName("east")
                .AddSynonyms(new List<string> { "go east", "go to east", "walk east", "move east", "e" });

            commands.CommandBuilder()
                .SetFuncName("up")
                .AddSynonyms(new List<string> { "go up", "move up", "u" });
            commands.CommandBuilder()
                .SetFuncName("down")
                .AddSynonyms(new List<string> { "go down", "move down", "d" });
            commands.CommandBuilder()
                .SetFuncName("in")
                .AddSynonyms(new List<string> { "go in", "move in", "go inside" });
            commands.CommandBuilder()
                .SetFuncName("out")
                .AddSynonyms(new List<string> { "go out", "move out", "leave", "go outside" });

            commands.CommandBuilder()
                .SetFuncName("moveto")
                .AddSynonyms(new List<string> { "move to", "walk to", "go to", "go", "enter" })
                .SetMustPrimary(true)
                .AddPrimaryRequirement(new List<string> { "in" });
            commands.CommandBuilder()
                .SetFuncName("moveback")
                .AddSynonyms(new List<string> { "move back", "go back", "walk back", "back", "b" });

            commands.CommandBuilder()
                .SetFuncName("look")
                .AddSynonyms(new List<string> { "look", "look around", "l" });
            commands.CommandBuilder()
                .SetFuncName("examine")
                .AddSynonyms(new List<string> { "look at", "take a look at", "inspect", "x" })
                .SetMustPrimary(true);
            commands.CommandBuilder()
                .SetFuncName("light")
                .AddSynonyms(new List<string> { "light up", "turn on" })
                .SetMustPrimary(true)
                .AddPrimaryRequirement(new List<string> { "lightable" });

            commands.CommandBuilder()
                .SetFuncName("take")
                .AddSynonyms(new List<string> { "pick up", "pickup", "get" })
                .SetMustPrimary(true)
                .AddPrimaryRequirement(new List<string> { "size" });
            commands.CommandBuilder()
                .SetFuncName("drop")
                .AddSynonyms(new List<string> { "throw", "untake", "get rid of" })
                .SetMustPrimary(true)
                .AddPrimaryRequirement(new List<string> { "size" });
            commands.CommandBuilder()
                .SetFuncName("inventory")
                .AddSynonyms(new List<string> { "i", "list items" });

            commands.CommandBuilder()
                .SetFuncName("lock")
                .AddSynonyms(new List<string> { "close" })
                .AddPrimaryRequirement(new List<string> { "lock" })
                .SetConnection("with")
                .AddSecondaryRequirement(new List<string> { "key" });
            commands.CommandBuilder()
                .SetFuncName("unlock")
                .AddSynonyms(new List<string> { "open" })
                .AddPrimaryRequirement(new List<string> { "lock" })
                .SetConnection("with")
                .AddSecondaryRequirement(new List<string> { "key" });

            commands.CommandBuilder()
                .SetFuncName("use")
                .AddSynonyms(new List<string> { "try", "make use of" })
                .AddPrimaryRequirement(new List<string> { "use" });

            return commands;
        }

        protected override Connections CreateConnections()
        {
            Connections connections = new Connections();

            connections.AddConnection(new ("with", new List<string> { "using" }));
            connections.AddConnection(new("in", new List<string> { "on" }));
            connections.AddConnection(new("from", new List<string> { }));

            return connections;
        }

        protected override World CreateWorld()
        {
            World world = new ();

            //god
            world.EntityBuilder("World")
                .AddComponent("description", "Cruel and grey");

            world.EntityBuilder("player")
                .AddComponent("synonyms", new List<string> { "myself" })
                .AddComponent("description", "You look a bit tired and confused\n" +
                "You haven't eaten in days\n" +
                "Maybe you should find some food")
                .AddComponent("in", "The Kitchen");

            //rooms
            world.EntityBuilder("The Kitchen")
                .AddComponent("description", "This room looks messy\n" +
                "Stuff everywhere, like there was a fight here or something")
                .AddComponent("west", "The Bedroom")
                .AddComponent("west door", "door between The Kitchen and The Bedroom")
                .AddComponent("south", "The Hall")
                .AddComponent("in", "World");

            world.EntityBuilder("The Hall")
                .AddComponent("description", "There is blood on the walls, and on the doorknob\n" +
                "This is scary but you still don't remember anything")
                .AddComponent("north", "The Kitchen")
                .AddComponent("outside", "The Garden")
                .AddComponent("outside door", "entrance door")
                .AddComponent("up", "The Cellar")
                .AddComponent("up door", "wooden ladders")
                .AddComponent("in", "World");

            world.EntityBuilder("The Bedroom")
                .AddComponent("description", "You remember sleeping here with your husband\n" +
                "It means that you have a husband\n" +
                "But you can't remember his face\n"
                + "Nor anithing related to him")
                .AddComponent("east", "The Kitchen")
                .AddComponent("east door", "door between The Kitchen and The Bedroom")
                .AddComponent("in", "World");

            world.EntityBuilder("The Cellar")
                .AddComponent("description", "Spider webs cover most of the things here")
                .AddComponent("dark", "The wooden floor creaks as you step on it\n" +
                "It is so dark you can not see a thing")
                .AddComponent("down", "The Hall")
                .AddComponent("down door", "wooden ladders")
                .AddComponent("in", "World");

            world.EntityBuilder("The Garden")
                .AddComponent("description", "The light hurts your eyes as you get out\n" +
                "Blood freezes in your veins as you look at the shed\n" +
                "There is a path of blood leading there")
                .AddComponent("inside", "The Hall")
                .AddComponent("inside door", "entrance door")
                .AddComponent("east", "The Shed")
                .AddComponent("east door", "shed door")
                .AddComponent("in", "World");

            world.EntityBuilder("The Shed")
                .AddComponent("description", "This is a wooden shed where you kept some tools")
                .AddComponent("west", "The Garden")
                .AddComponent("west door", "shed door")
                .AddComponent("in", "World");

            //doors
            world.EntityBuilder("door between The Kitchen and The Bedroom")
                .AddComponent("description", "It creaks a little bit");

            world.EntityBuilder("entrance door")
                .AddComponent("description", "Blood is dripping on the doorknob\n" +
                "Somebody tried to escape here\n" +
                "You remember that you kept a spare key in a box somewhere in the cellar")
                .AddComponent("adjectives", new List<string> { "door", "entrance", "garden" }) 
                .AddComponent("lock", "locked");

            world.EntityBuilder("wooden ladders")
                .AddComponent("description", "A few rungs are missing but it seems safe enough");

            world.EntityBuilder("shed door")
                .AddComponent("description", "This is a wooden door that is somehow attached to the shed");

            //items
            world.EntityBuilder("pot")
                .AddComponent("description", "There is a hole on it")
                .AddComponent("in", "The Kitchen")
                .AddComponent("size", 2);

            world.EntityBuilder("flower")
                .AddComponent("description", "This is a blue flower")
                .AddComponent("in", "The Garden")
                .AddComponent("size", 1);

            world.EntityBuilder("mailbox")
                .AddComponent("description", "This is an old mailbox")
                .AddComponent("adjectives", new List<string> { "old" })
                .AddComponent("lock", "locked")
                .AddComponent("in", "The Garden");

            world.EntityBuilder("blackmail")
                .AddComponent("description", "This letter is written with letters cut from newspaper\n" +
                "No one will believe you!")
                .AddComponent("in", "mailbox")
                .AddComponent("adjectives", new List<string> { "scary" })
                .AddComponent("size", 1);

            world.EntityBuilder("toolbox")
                .AddComponent("in", "The Shed")
                .AddComponent("description", "A rusty toolbox")
                .AddComponent("adjectives", new List<string> { "rusty", "red", "heavy" })
                .AddComponent("size", 3);

            world.EntityBuilder("screwdriver")
                .AddComponent("in", "toolbox")
                .AddComponent("size", 1);

            world.EntityBuilder("hammer")
                .AddComponent("in", "toolbox")
                .AddComponent("size", 1);

            world.EntityBuilder("old key")
                .AddComponent("in", "toolbox")
                .AddComponent("adjectives", new List<string> { "old" })
                .AddComponent("description", "This is an old key\n" +
                "What might it open?")
                .AddComponent("key", "mailbox")
                .AddComponent("size", 1);

            world.EntityBuilder("flashlight")
                .AddComponent("description", "This is a flashlight")
                .AddComponent("in", "The Bedroom")
                .AddComponent("lightable", 1)
                .AddComponent("use", "light")
                .AddComponent("size", 1);

            world.EntityBuilder("spare key")
                .AddComponent("description", "This is the spare key to the entrance door")
                .AddComponent("in", "normal sized box")
                .AddComponent("key", "entrance door")
                .AddComponent("size", 1);

            world.EntityBuilder("picture")
                .AddComponent("description", "This is a picutre of you and your husband\n" +
                "Now you can remember his face!\n" +
                "You drop some tears as you realize he is not with you right now\n" +
                "You feel the urge to find him take over your mind")
                .AddComponent("in", "small box")
                .AddComponent("size", 1);

            world.EntityBuilder("huge box")
                .AddComponent("description", "This is a huge box\n" +
                "You can hardly even move it")
                .AddComponent("adjectives", new List<string> { "huge" })
                .AddComponent("in", "The Cellar");

            world.EntityBuilder("normal sized box")
                .AddComponent("adjectives", new List<string> { "normal" })
                .AddComponent("description", "This is a normal sized box")
                .AddComponent("in", "The Cellar");

            world.EntityBuilder("small box")
                .AddComponent("description", "This is a small box")
                .AddComponent("adjectives", new List<string> { "small" })
                .AddComponent("in", "The Cellar");

            world.EntityBuilder("the dead body of your husband")
                .AddComponent("description", "What??\n" +
                "Could it...\n" +
                "You can not belive that this is your husband\n" +
                "You nearly faint when you realize that this is him\n" +
                "But who did this?\n" +
                "Maybe YOU?")
                .AddComponent("in", "The Shed");

            return world;
        }
    }
}
