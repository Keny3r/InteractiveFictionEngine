using Keny3rEngine.Engine.EW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keny3rEngine.Rules
{
    internal class Health
    {
        public static void Death(World world)
        {
            foreach (Entity entity in world.GetEntities())
            {
                if (entity.HasComponent("hp") && entity.GetIntComponent("hp") <= 0)
                {
                    string entityName = entity.GetStringComponent("name");
                    string placeName = entity.GetStringComponent("in");

                    world.EntityBuilder("Corpse of " + entityName)
                        .AddComponent("description", "It was once thrilling with life\n" +
                        "now it is just a pile of meat and bones")
                        .AddComponent("in", placeName)
                        .AddComponent("size", 2);
                    entity.AddComponent("in", "overworld");
                }
            }
        }
    }
}
