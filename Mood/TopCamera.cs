using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{
    public class TopCamera : Camera
    {
        public TopCamera() : base()
        {
            this.rotatedXAngle = 90;
            this.cameraEye = new Vector3d(0, 3.5f, 0);
        }

        public override void RotateY(double angle)
        {
            return;
        }

        public override void RotateX(double angle)
        {
            return;
        }
    }    
}
