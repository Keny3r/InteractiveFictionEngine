using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Engine
{
    internal class Connections
    {
        public List<Connection> ConnectionsList;

        public Connections()
        {
            ConnectionsList = new ();
        }

        public void AddConnection(Connection connection)
        {
            ConnectionsList.Add(connection);
        }

        public bool Contains(string word)
        {
            foreach (Connection c in ConnectionsList)
            {
                if (c.Name == word)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
