using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mood
{
    public class FPSCamera : Camera
    {
        public override void RotateX(double angle)
        {
            if (this.rotatedXAngle + angle < 90 &&
                this.rotatedXAngle + angle > -90)
            {
                base.RotateX(angle);
            }
        }

        public override void MoveFwBw(double step)
        {
            double angle = rotatedYAngle * PIdiv180;

            Point3d dir = firstCameraDirection * rotationY(angle);
            dir += cameraEye;
            
            Point3d vector = dir - cameraEye;

            float ySum = vector.Y * (float)step;
            float xSum = vector.X * (float)step;
            float zSum = vector.Z * (float)step;

            cameraEye.X += xSum; 
            cameraEye.Z += zSum; 
            cameraDirection.X += xSum;
            cameraDirection.Z += zSum; 
        }
    }
}
