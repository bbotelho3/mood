using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{
    class World
    {
        public List<WorldObject> Objects { get; set; }

        public World()
        {
            Objects = new List<WorldObject>();
        }

        public WorldObject HitTest(IMoveable mvObj)
        {
            foreach (WorldObject obj in Objects)
            {
                if (obj != mvObj && obj.HitTest(mvObj))
                {
                    return obj;
                }
            }

            return null;
        }
    }
}
