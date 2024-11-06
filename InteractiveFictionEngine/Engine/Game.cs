using Keny3rEngine.Engine.EW;
using static Keny3rEngine.Engine.Interpreter;

namespace Keny3rEngine.Engine
{
    internal class Game
    {
        World world;
        Tokenizer tokenizer;
        Parser parser;
        Interpreter interpreter;
        Rules rules;

        bool shallRun = true;

        public Game()
        {
            world = CreateWorld();
            Commands commands = CreateCommands();
            Connections connections = CreateConnections();

            rules = CreateRules();

            tokenizer = new (world, commands, connections);
            parser = new Parser(world, commands, connections);
            interpreter = new (world, CreateActions());
        }

        protected virtual World CreateWorld()
        {
            return new ();
        }

        protected virtual Rules CreateRules()
        {
            return new ();
        }

        protected virtual Dictionary<string, Executor> CreateActions()
        {
            return new ();
        }

        protected virtual Commands CreateCommands()
        {
            return new ();
        }

        protected virtual Connections CreateConnections()
        {
            return new ();
        }

        public void DeleteEntity(string name)
        {
            world.DeleteEntity(name);
        }

        virtual public void Init()
        {

        }

        public void Start()
        {
            tokenizer.Init();
            Init();
            while (shallRun)
            {
                Update();
            }
        }

        public void Stop()
        {
            shallRun = false;
        }

        private void Update()
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            List<string> tokens = tokenizer.Tokenize(input);
            List<Parsed> parsed = parser.Parse(tokens);
            interpreter.Interpret(parsed);
            rules.ExecuteRules(world);
            Console.WriteLine();
        }
    }
}
