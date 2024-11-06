using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Engine.EW
{
    internal class EntityBuilder
    {
        Entity entity;

        public EntityBuilder CreateEntity()
        {
            entity = new Entity();
            return this;
        }

        public EntityBuilder AddComponent(string name, int component)
        {
            entity.AddComponent(name, component);
            return this;
        }

        public EntityBuilder AddComponent(string name, string component)
        {
            entity.AddComponent(name, component);
            return this;
        }

        public EntityBuilder AddComponent(string name, List<int> component)
        {
            entity.AddComponent(name, component);
            return this;
        }

        public EntityBuilder AddComponent(string name, List<string> component)
        {
            entity.AddComponent(name, component);
            return this;
        }

        public Entity GetResult()
        {
            return entity;
        }
    }
}
