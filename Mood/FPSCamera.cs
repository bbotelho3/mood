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
            Vector3d vector = cameraDirection - cameraEye;

            float ySum = vector.Y * (float)step;
            float xSum = vector.X * (float)step;
            float zSum = vector.Z * (float)step;

            //float xPerc = (Math.Abs(xSum) / (Math.Abs(xSum) + Math.Abs(zSum)));
            //float zPerc = 1 - xPerc;

            //if (vector.X <= 0)
            //{
            //    xSum += xPerc * ySum;
            //}
            //else
            //{
            //    xSum -= xPerc * ySum;
            //}

            //if (vector.Z <= 0)
            //{
            //    zSum += zPerc * ySum;
            //}
            //else
            //{
            //    zSum -= zPerc * ySum;
            //}

            cameraEye.X += xSum; 
            cameraEye.Z += zSum; 
            cameraDirection.X += xSum;
            cameraDirection.Z += zSum; 
        }

        //public override void MoveFwBw(double step)
        //{
        //    Vector3d vector = cameraDirection - cameraEye;

        //    //cameraEye.Y += vector.Y * (float)step;
        //    //cameraDirection.Y += vector.Y * (float)step;

        //    float ySum = vector.Y * (float)step;
        //    float xSum = vector.X * (float)step;
        //    float zSum = vector.Z * (float)step;

        //    float xMult = ((Math.Abs(xSum) / (Math.Abs(xSum) + Math.Abs(zSum)) * Math.Abs(ySum)) + Math.Abs(xSum)) / vector.X;
        //    float zMult = ((Math.Abs(zSum) / (Math.Abs(xSum) + Math.Abs(zSum)) * Math.Abs(ySum)) + Math.Abs(zSum)) / vector.Z;
        //    //float zMult = 1;// (100 * zSum) / (xSum + zSum) * ySum;

        //    cameraEye.X += xMult * vector.X;
        //    cameraEye.Z += zMult * vector.Z;
        //    cameraDirection.X += xMult * vector.X;
        //    cameraDirection.Z += zMult * vector.Z;
        //}

    }
}
