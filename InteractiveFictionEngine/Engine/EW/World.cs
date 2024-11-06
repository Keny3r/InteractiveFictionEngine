using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Engine.EW
{
    internal class World
    {
        Dictionary<string, Entity> entities;
        EntityBuilder entityBuilder;

        public World()
        {
            entities = new ();
            entityBuilder = new ();
        }

        public EntityBuilder EntityBuilder(string name)
        {
            entities[name] = entityBuilder
                .CreateEntity()
                .AddComponent("name", name)
                .GetResult();
            return entityBuilder;
        }

        public void DeleteEntity(string name)
        {
            entities.Remove(name);
        }

        public List<Entity> GetEntities()
        {
            return entities.Values.ToList();
        }

        public Entity GetEntity(string name)
        {
            if (entities.ContainsKey(name))
            {
                return entities[name];
            }
            return null;
        }

        public List<Entity> GetChildren(string name)
        {
            List<Entity> children = new ();

            foreach(Entity child in GetEntities())
            {
                if (child.HasComponent("in") && child.GetStringComponent("in") == name)
                {
                    children.Add(child);
                }
            }

            return children;
        }

        public List<Entity> GetSubTree(string name)
        {
            List<Entity> subTree = new();
            List<Entity> children = GetChildren(name);

            while(children.Count > 0)
            {
                subTree.AddRange(children);
                List<Entity> newChildren = new ();
                foreach (Entity child in children)
                {
                    newChildren.AddRange(GetChildren(child.GetStringComponent("name")));
                }
                children = newChildren;
            }

            return subTree;
        }
    }
}
