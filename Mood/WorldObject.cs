using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{
    public abstract class WorldObject
    {
        public abstract bool HitTest(IMoveable obj);

        public abstract void Draw();
    }
}
