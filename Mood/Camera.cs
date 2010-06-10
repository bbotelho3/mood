using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Mood.GeoCalc;

namespace Mood
{
    public class Camera
    {
        private const float PIdiv180 = (float)Math.PI / 180;

        public Vector3d cameraEye;
        public Vector3d cameraDirection;
        private Vector3d cameraUp;

        public Camera()
        {
            this.cameraEye = new Vector3d(0, 0, 0);
            this.cameraDirection = new Vector3d(0, 0, -1);
            this.cameraUp = new Vector3d(0, 1, 0);
        }
        
        //private float RotatedX;
        //private float RotatedY;
        //private bool ViewDirectionChanged;
        //private Vector3d Position;
        //private Vector3d ViewDirection;

        //public Camera()
        //{
        //    this.RotatedX = this.RotatedY = 0;
        //    this.ViewDirectionChanged = false;

        //    this.Position = new Vector3d(0, 0, 0);
        //    this.ViewDirection = new Vector3d(0, 0, -1);
        //}

        //public void MoveForwards(float distance)
        //{
        //    if (this.ViewDirectionChanged)
        //    {
        //        this.ViewDirection = this.GetViewDir();
        //    }

        //    Vector3d MoveVector = new Vector3d();
        //    MoveVector.X = this.ViewDirection.X * -distance;
        //    MoveVector.Y = this.ViewDirection.Y * -distance;
        //    MoveVector.Z = this.ViewDirection.Z * -distance;
        //    this.Position = Position + MoveVector;
        //}

        //public void RotateX(float angle)
        //{
        //    this.RotatedX += angle;
        //    this.ViewDirectionChanged = true;
        //}

        //public void RotateY(float Angle)
        //{
        //    this.RotatedY += Angle;
        //    this.ViewDirectionChanged = true;
        //}

        //private Vector3d GetViewDir()
        //{
        //    Vector3d Step1 = new Vector3d();
        //    Vector3d Step2 = new Vector3d();
        //    //Rotate around Y-axis:
        //    Step1.X = (float)Math.Cos((this.RotatedY + 90) * PIdiv180);
        //    Step1.Z = (float)-Math.Sin((this.RotatedY + 90) * PIdiv180);
        //    //Rotate around X-axis:
        //    float cosX = (float)Math.Cos(this.RotatedX * PIdiv180);
        //    Step2.X = Step1.X * cosX;
        //    Step2.Z = Step1.Z * cosX;
        //    Step2.Y = (float)Math.Sin(this.RotatedX * PIdiv180);
        //    //Rotation around Z-axis not yet implemented, so:
        //    return Step2;
        //}

        public void MoveFwBw(double step)
        {
            Vector3d vector = cameraDirection - cameraEye;
            cameraEye.X += vector.X * (float)step;
            //cameraEye.Y += vector.Y * (float)step;
            cameraEye.Z += vector.Z * (float)step;
            cameraDirection.X += vector.X * (float)step;
            //cameraDirection.Y+= vector.Y * (float)step;
            cameraDirection.Z += vector.Z * (float)step;
        }

        public void RotateY(double angle)
        {
            Vector3d transl = cameraDirection - cameraEye;
            cameraDirection.X = cameraEye.X +(float)(Math.Cos(angle) * transl.X - Math.Sin(angle) * transl.Z);
            cameraDirection.Z = cameraEye.Z +(float)(Math.Sin(angle) * transl.X + Math.Cos(angle) * transl.Z);
        }

        public void RotateX(double angle)
        {
            //Vector3d ray = cameraDirection - cameraEye;
            //cameraDirection.Y = (float)(Math.Cos(angle) * ray.Y + Math.Sin(angle) * ray.Z);
            //cameraDirection.Z = (float)(-Math.Sin(angle) * ray.Y + Math.Cos(angle) * ray.Z);
        }

        public void Render()
        {
            //Gl.glMatrixMode(Gl.GL_PROJECTION);
            Glu.gluLookAt(cameraEye.X, cameraEye.Y, cameraEye.Z, cameraDirection.X, cameraDirection.Y, cameraDirection.Z, cameraUp.X, cameraUp.Y, cameraUp.Z);
            //Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }

        private static Matrix rotationY(double angle)
        {
            return new Matrix(new double[,]{{Math.Cos(angle),0, Math.Sin(angle),0},
                                            {0,1,0,0},
                                            {-Math.Sin(angle),0,Math.Cos(angle),0},
                                            {0,0,0,1}});
        }

        public Vector3d getCameraEye()
        {
            return this.cameraEye;
        }

        public Vector3d getCameraDirection()
        {
            return this.cameraDirection;
        }
    }
}
