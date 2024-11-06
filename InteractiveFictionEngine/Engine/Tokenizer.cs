using Keny3rEngine.Engine.EW;

namespace Keny3rEngine.Engine
{
    internal class Tokenizer
    {
        Dictionary<int, Dictionary<string, string>> synonyms;
        World world;
        Commands commands;
        Connections connections;

        public Tokenizer(World world, Commands commands, Connections connections)
        {
            this.world = world;
            this.commands = commands;
            this.connections = connections;
        }

        public void Init()
        {
            synonyms = new ();

            foreach (Entity entity in world.GetEntities())
            {
                if (entity.HasComponent("synonyms"))
                {
                    string entityName = entity.GetStringComponent("name");
                    List<string> entitySynonyms = entity.GetStringListComponent("synonyms");

                    entitySynonyms.ForEach(s => AddSynonym(s, entityName));
                }
            }

            foreach (Command command in commands.CommandsList)
            {
                foreach (string synonym in command.Synonyms)
                {
                    AddSynonym(synonym, command.FuncName);
                }
            }

            foreach (Connection connection in connections.ConnectionsList)
            {
                foreach (string synonym in connection.Synonyms)
                {
                    AddSynonym(synonym, connection.Name);
                }
            }
        }

        private void AddSynonym(string synonym, string of)
        {
            int length = synonym.Split(" ").Length;
            if (!synonyms.ContainsKey(length))
            {
                synonyms[length] = new();
            }
            synonyms[length][synonym] = of;
        }

        public List<string> Tokenize(string input)
        {
            input = input == "" ? "invalidaction" : input.ToLower();

            input = wrapInSpaces(input);

            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }

            for (int i = synonyms.Keys.Max(); i > 0; i--)
            {
                if (synonyms.ContainsKey(i))
                {
                    foreach (string synonym in synonyms[i].Keys)
                    {
                        input = input.Replace(wrapInSpaces(synonym), wrapInSpaces(synonyms[i][synonym]));
                    }
                }
            }

            List<string> tokens = input.Substring(1, input.Length - 2).Split(' ').ToList();

            return tokens;
        }

        private string wrapInSpaces(string s)
        {
            return " " + s + " ";
        }
    }
}
