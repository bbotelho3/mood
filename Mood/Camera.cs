using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Mood
{
    public class Camera
    {
        private const float PIdiv180 = (float)Math.PI / 180;

        private float RotatedX;
        private float RotatedY;
        private bool ViewDirectionChanged;
        private Vector3d Position;
        private Vector3d ViewDirection;

        public Camera()
        {
            this.RotatedX = this.RotatedY = 0;
            this.ViewDirectionChanged = false;

            this.Position = new Vector3d(0, 0, 0);
            this.ViewDirection = new Vector3d(0, 0, -1);
        }

        public void MoveForwards(float distance)
        {
            if (this.ViewDirectionChanged)
            {
                this.ViewDirection = this.GetViewDir();
            }

            Vector3d MoveVector = new Vector3d();
            MoveVector.X = this.ViewDirection.X * -distance;
            MoveVector.Y = this.ViewDirection.Y * -distance;
            MoveVector.Z = this.ViewDirection.Z * -distance;
            this.Position = Position + MoveVector;
            //AddF3dVectorToVector(&Position, &MoveVector);
        }

        public void RotateX(float angle)
        {
            this.RotatedX += angle;
            this.ViewDirectionChanged = true;
        }

        public void RotateY(float Angle)
        {
            this.RotatedY += Angle;
            this.ViewDirectionChanged = true;
        }

        private Vector3d GetViewDir()
        {
            Vector3d Step1 = new Vector3d();
            Vector3d Step2 = new Vector3d();
            //Rotate around Y-axis:
            Step1.X = (float)Math.Cos((this.RotatedY + 90) * PIdiv180);
            Step1.Z = (float)-Math.Sin((this.RotatedY + 90) * PIdiv180);
            //Rotate around X-axis:
            float cosX = (float)Math.Cos(this.RotatedX * PIdiv180);
            Step2.X = Step1.X * cosX;
            Step2.Z = Step1.Z * cosX;
            Step2.Y = (float)Math.Sin(this.RotatedX * PIdiv180);
            //Rotation around Z-axis not yet implemented, so:
            return Step2;
        }

        public void Render()
        {
            Gl.glRotatef(-this.RotatedX, 1, 0, 0);
            Gl.glRotatef(-this.RotatedY, 0, 1, 0);
            Gl.glRotatef(0, 0, 0, 1);
            Gl.glTranslatef(-this.Position.X, -this.Position.Y, -this.Position.Z);
        }
    }
}
