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
        protected const float PIdiv180 = (float)Math.PI / 180;

        public Vector3d cameraEye;
        public Vector3d cameraDirection;
        private Vector3d cameraUp;

        protected double rotatedYAngle;
        protected double rotatedXAngle;
        protected Vector3d firstCameraDirection;

        public Camera()
        {
            this.cameraEye = new Vector3d(0, 0, 0);
            this.cameraDirection = new Vector3d(0, 0, -1);
            this.cameraUp = new Vector3d(0, 1, 0);

            this.firstCameraDirection = new Vector3d(cameraDirection);
            this.rotatedXAngle = this.rotatedYAngle = 0;
        }

        public virtual void MoveFwBw(double step)
        {
            //double angleY = this.rotatedYAngle * PIdiv180;

            //Vector3d trans = firstCameraDirection * rotationY(angleY);
            //Vector3d dir = cameraEye + trans;

            Vector3d vector = cameraDirection - cameraEye;
            cameraEye.X += vector.X * (float)step;
            cameraEye.Y += vector.Y * (float)step;
            cameraEye.Z += vector.Z * (float)step;
            cameraDirection.X += vector.X * (float)step;
            cameraDirection.Y += vector.Y * (float)step;
            cameraDirection.Z += vector.Z * (float)step;
        }

        public virtual void RotateY(double angle)
        {
            this.rotatedYAngle += angle;
            //Vector3d transl = cameraDirection - cameraEye;
            //cameraDirection.X = cameraEye.X +(float)(Math.Cos(angle) * transl.X - Math.Sin(angle) * transl.Z);
            //cameraDirection.Z = cameraEye.Z +(float)(Math.Sin(angle) * transl.X + Math.Cos(angle) * transl.Z);
        }

        public virtual void RotateX(double angle)
        {
            this.rotatedXAngle += angle;
            //Vector3d ray = cameraDirection - cameraEye;
            //cameraDirection.Y = cameraEye.Y + (float)(Math.Cos(angle) * ray.Y + Math.Sin(angle) * ray.Z);
            //cameraDirection.Z = cameraEye.Z + (float)(-Math.Sin(angle) * ray.Y + Math.Cos(angle) * ray.Z);
        }

        public void Render()
        {
            double angleY = this.rotatedYAngle * PIdiv180;
            double angleX = this.rotatedXAngle * PIdiv180;

            Vector3d trans = firstCameraDirection * (rotationX(angleX) * rotationY(angleY));

            cameraDirection = cameraEye + trans;

            //Gl.glMatrixMode(Gl.GL_PROJECTION);
            Glu.gluLookAt(cameraEye.X, cameraEye.Y, cameraEye.Z, cameraDirection.X, cameraDirection.Y, cameraDirection.Z, cameraUp.X, cameraUp.Y, cameraUp.Z);
            //Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }

        protected virtual Matrix rotationY(double angle)
        {
            return new Matrix(new double[,]{{Math.Cos(angle),0, Math.Sin(angle),0},
                                            {0,1,0,0},
                                            {-Math.Sin(angle),0,Math.Cos(angle),0},
                                            {0,0,0,1}});
            //cos 0 sen 0
            //0 1 0 0
            //−sen 0 cos 0
            //0 0 0 1
        }

        protected virtual Matrix rotationX(double angle)
        {
            return new Matrix(new double[,]{{1,0,0,0},
                                            {0,Math.Cos(angle),- Math.Sin(angle),0},                                            
                                            {0,Math.Sin(angle),Math.Cos(angle),0},
                                            {0,0,0,1}});
            //1 0 0 0
            //0 cos −sen 0
            //0 sen cos 0
            //0 0 0 1
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
