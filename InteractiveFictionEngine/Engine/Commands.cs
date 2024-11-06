namespace Keny3rEngine.Engine
{
    internal class Commands
    {
        public List<Command> CommandsList;
        CommandBuilder commandBuilder;

        public Commands()
        {
            CommandsList = new ();
            commandBuilder = new ();
        }

        public CommandBuilder CommandBuilder()
        {
            CommandsList.Add(commandBuilder.CreateCommand().GetResult());
            return commandBuilder;
        }

        public string GetFunction(string action)
        {
            foreach (Command command in CommandsList)
            {
                if (command.FuncName == action)
                {
                    return command.FuncName;
                }
            }

            return null;
        }

        public Command GetCommand(string func)
        {
            foreach (Command command in CommandsList)
            {
                if (command.FuncName == func)
                {
                    return command;
                }
            }

            return null;
        }

        public bool IsValidParsed(Parsed parsed)
        {
            Command command = GetCommand(parsed.Action);

            if (command == null)
            {
                Console.WriteLine("Can not recognise this verb");
                return false;
            }

            bool isValid = true;

            isValid &= !command.MustPrimary || parsed.Primary != null;
            isValid &= !command.MustSecondary || (parsed.Secondary != null && parsed.Connection == command.Connection);

            if (command.MustPrimary)
            {
                if (parsed.Primary == null)
                {
                    Console.WriteLine("Can not recognise this object");
                    return false;
                }
                foreach (string component in command.PrimaryComponents)
                {
                    isValid &= parsed.Primary.HasComponent(component);
                    if (!isValid)
                    {
                        Console.WriteLine("It is not a good idea to " + parsed.Action + " " + parsed.Primary.GetStringComponent("name"));
                        return false;
                    }
                }
            }

            if (command.MustSecondary)
            {
                if (parsed.Secondary == null)
                {
                    Console.WriteLine("Can not recognise this object");
                    return false;
                }
                foreach (string component in command.SecondaryComponents)
                {
                    isValid &= parsed.Secondary.HasComponent(component);
                    if (!isValid)
                    {
                        Console.WriteLine("Can not recognise " + parsed.Connection + " what you want to " + parsed.Secondary.GetStringComponent("name"));
                        return false;
                    }
                }
            }

            return isValid;
        }
    }
}
