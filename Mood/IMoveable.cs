using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{
    public interface IMoveable
    {
        Vector3d GetPosition();

        void SetPosition(Vector3d position);

        void Move(Vector3d direction);
    }
}
