using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{
    public class Player : IHitable
    {
        public Point3d Position { get; set; }

        public Player()
        {
            Position = new Point3d();
        }

        public BoundingBox BoundingBox
        {
            get
            {
                return new BoundingBox(Position + new Point3d(-0.2f, 0f, 0.2f), Position + new Point3d(0.2f, 0f, 0.2f), Position + new Point3d(-0.2f, 0f, -0.2f), Position + new Point3d(0.2f, 0f, -0.2f));
            }
            set
            {
                return;
            }
        }

        // HitTest não é executado para player pois ele não é parte do mundo.
        public bool HitTest(IHitable obj)
        {
            return true;
        }
    }
}
