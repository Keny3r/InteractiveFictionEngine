namespace Keny3rEngine.Engine.EW
{
    internal class Entity
    {
        List<int> intComponents;
        List<string> stringComponents;
        List<List<int>> intListComponents;
        List<List<string>> stringListComponents;

        Dictionary<string, int> components;

        public Entity()
        {
            intComponents = new();
            stringComponents = new();
            intListComponents = new();
            stringListComponents = new();
            components = new();
        }

        //Adders
        public void AddComponent(string name, int component)
        {
            components[name] = intComponents.Count;
            intComponents.Add(component);
        }

        public void AddComponent(string name, string component)
        {
            components[name] = stringComponents.Count;
            stringComponents.Add(component);
        }

        public void AddComponent(string name, List<int> component)
        {
            components[name] = intListComponents.Count;
            intListComponents.Add(component);
        }

        public void AddComponent(string name, List<string> component)
        {
            components[name] = stringListComponents.Count;
            stringListComponents.Add(component);
        }

        //Getters
        public int GetIntComponent(string name)
        {
            if (!components.ContainsKey(name))
            {
                return -1;
            }
            return intComponents[components[name]];
        }

        public string GetStringComponent(string name)
        {
            if (!components.ContainsKey(name))
            {
                return "";
            }
            return stringComponents[components[name]];
        }

        public List<int> GetIntListComponent(string name)
        {
            if (!components.ContainsKey(name))
            {
                return new ();
            }
            return intListComponents[components[name]];
        }

        public List<string> GetStringListComponent(string name)
        {
            if (!components.ContainsKey(name))
            {
                return new ();
            }
            return stringListComponents[components[name]];
        }

        public bool HasComponent(string name)
        {
            return components.ContainsKey(name);
        }
    }
}
