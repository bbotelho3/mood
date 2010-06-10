using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{
    public class Player : IMoveable
    {
        public Vector3d Position { get; set; }

        public Player()
        {
            Position = new Vector3d();
        }

        public void Move(Vector3d direction)
        {
        }

        public Vector3d GetPosition()
        {
            return Position;
        }

        public void SetPosition(Vector3d position)
        {
            Position = position;
        }
    }
}
