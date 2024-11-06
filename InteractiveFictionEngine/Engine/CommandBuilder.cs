using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Engine
{
    internal class CommandBuilder
    {
        Command command;

        public CommandBuilder CreateCommand()
        {
            command = new();
            return this;
        }

        public Command GetResult()
        {
            return command;
        }

        public CommandBuilder SetFuncName(string name)
        {
            command.FuncName = name;
            return this;
        }

        public CommandBuilder AddSynonyms(List<string> synonyms)
        {
            command.Synonyms.AddRange(synonyms);
            return this;
        }

        public CommandBuilder SetMustPrimary(bool must)
        {
            command.MustPrimary = must;
            return this;
        }

        public CommandBuilder AddPrimaryRequirement(List<string> requirements)
        {
            command.PrimaryComponents.AddRange(requirements);
            return this;
        }

        public CommandBuilder SetMustSecondary(bool must)
        {
            command.MustSecondary = must;
            return this;
        }

        public CommandBuilder SetConnection(string connection)
        {
            command.Connection = connection;
            return this;
        }

        public CommandBuilder AddSecondaryRequirement(List<string> requirements)
        {
            command.SecondaryComponents.AddRange(requirements);
            return this;
        }
    }
}
