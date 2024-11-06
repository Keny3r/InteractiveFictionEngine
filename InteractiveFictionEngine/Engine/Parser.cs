using Keny3rEngine.Engine.EW;
using System.Globalization;

namespace Keny3rEngine.Engine
{
    internal class Parser
    {
        Commands commands;
        Connections connections;
        List<string> unused;
        List<string> pronouns;

        World world;

        const string PRONOUN_ENTITY_NAME = "pronoun_entity_name";

        public Parser(World world, Commands commands, Connections connections)
        {
            this.commands = commands;
            this.world = world;
            this.connections = connections;
            unused = fillFromFile("unused.txt");
            pronouns = fillFromFile("pronouns.txt");
        }

        private List<string> fillFromFile(string filename)
        {
            List<string> result = new List<string>();

            string path = Path.PARSER_CONFIG_PATH + filename;
            using (StreamReader sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public List<Parsed> Parse(List<string> tokens)
        {
            List<Parsed> parsed = new();
            List<List<string>> acts = makeActs(tokens);

            acts.ForEach(act => parsed.Add(parseAct(act)));

            Entity objectOfSentence = new ();
            foreach (Parsed p in parsed)
            {
                if (p.Primary != null)
                {
                    if (p.Primary.GetStringComponent("name") == PRONOUN_ENTITY_NAME)
                    {
                        p.Primary = objectOfSentence;
                    }
                    else
                    {
                        objectOfSentence = p.Primary;
                    }
                }
            }

            List<Parsed> result = new ();
            foreach (Parsed p in parsed)
            {
                if (commands.IsValidParsed(p))
                {
                    result.Add(p);
                }
            }

            return result;
        }

        private List<List<string>> makeActs(List<string> tokens)
        {
            List<List<string>> acts = new();
            List<string> currentAct = new();

            foreach (var token in removeUnused(tokens))
            {
                if (token == "and")
                {
                    acts.Add(currentAct);
                    currentAct = new();
                }
                else
                {
                    currentAct.Add(token);
                }
            }
            acts.Add(currentAct);

            return acts;
        }

        private List<string> removeUnused(List<string> tokens)
        {
            List<string> result = new List<string>();
            tokens.ForEach(token => { if (!unused.Contains(token)) result.Add(token); });
            return result;
        }

        private Parsed parseAct(List<string> act)
        {
            Parsed parsed = parseAction(act);

            List<string> primaryDescription = new ();
            List<string> secondaryDescription = new();
            List<string> description = primaryDescription;
            foreach (string word in act)
            {
                if (connections.Contains(word))
                {
                    parsed.Connection = word;
                    description = secondaryDescription;
                }
                else
                {
                    description.Add(word);
                }
            }
            parsed.Primary = primaryDescription.Count > 0 ? getEntity(primaryDescription) : null;
            parsed.Secondary = secondaryDescription.Count > 0 ? getEntity(secondaryDescription) : null;

            return parsed;
        }

        private Parsed parseAction(List<string> act)
        {
            Parsed parsed = new();
            parsed.Action = act.First();

            string action = "";
            int actionSize = 0;

            for (int i = 0; i < act.Count; i++)
            {
                action += act[i];
                string funcName = commands.GetFunction(action);
                if (funcName != null)
                {
                    parsed.Action = action;
                    actionSize = i;
                }
                action += " ";
            }

            act.RemoveRange(0, actionSize + 1);
            return parsed;
        }

        private Entity getEntity(List<string> description)
        {
            Entity mostLikelyEntity = null;
            int maxLikeliness = 0;

            //handle pronouns
            if (pronouns.Contains(description.Last()))
            {
                return new EntityBuilder()
                    .CreateEntity()
                    .AddComponent("name", PRONOUN_ENTITY_NAME)
                    .GetResult();
            }

            foreach (Entity entity in world.GetEntities())
            {
                int likeliness = 0;

                //if the name is exactly the same
                string entityName = entity.GetStringComponent("name").ToLower();
                if (entityName == description.Last())
                {
                    likeliness += 10;
                }

                //if the description is in the name
                if (entityName.Contains(description.Last()))
                {
                    likeliness += 1;
                }

                //if the description contains the name
                description.ForEach(desc => { if (entityName == desc) likeliness += 1; });
                
                //if the description contains adjectives
                if (entity.HasComponent("adjectives"))
                {
                    entity.GetStringListComponent("adjectives").ForEach(
                        adjective => { if (description.Contains(adjective.ToLower())) likeliness += 1; });
                }

                //if there was matching and the entity is in the current room, then make it more likely
                if (likeliness > 0 && isInCurrentRoom(world, entity))
                {
                    likeliness += 2;
                }

                if (likeliness > maxLikeliness)
                {
                    mostLikelyEntity = entity;
                    maxLikeliness = likeliness;
                }
            }

            return mostLikelyEntity;
        }

        private bool isInCurrentRoom(World world, Entity entity)
        {
            Entity player = world.GetEntity("player");
            List<Entity> stuffInCurrentRoom = world.GetSubTree(player.GetStringComponent("in"));
            return stuffInCurrentRoom.Contains(entity);
        }
    }
}
