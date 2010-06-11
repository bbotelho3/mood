using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.FreeGlut;
using System.Drawing;

namespace Mood
{
    class Sphere : WorldObject, IMoveable, IHitable, IShootable
    {
        public Vector3d Center;
        private Color color;
        private double radius;

        public Sphere(Vector3d center, double radius, Color color)
        {
            Center = center;
            this.radius = radius;
            this.color = color;
        }

        public bool HitTest(IMoveable obj)
        {
            Vector3d a = new Vector3d(Center.X + (float)radius, Center.Y + (float)radius, Center.Z + (float)radius);
            Vector3d b = new Vector3d(Center.X - (float)radius, Center.Y - (float)radius, Center.Z - (float)radius);

            double dist = Geometry.linePointDist(a, b, obj.GetPosition(), true);

            if (dist < 0.1d)
            {
                return true;
            }

            a = new Vector3d(Center.X + (float)radius, Center.Y + (float)radius, Center.Z - (float)radius);
            b = new Vector3d(Center.X - (float)radius, Center.Y + (float)radius, Center.Z + (float)radius);

            dist = Geometry.linePointDist(a, b, obj.GetPosition(), true);

            if (dist < 0.1d)
            {
                return true;
            }

            return false;
        }

        public Vector3d GetPosition()
        {
            return Center;
        }

        public void SetPosition(Vector3d position)
        {
            Center = position;
        }

        public override void Draw()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(color.R, color.G, color.B);
            Gl.glTranslatef(Center.X, Center.Y, Center.Z);
            Glut.glutSolidSphere(radius, 20, 20);
            Gl.glPopMatrix();
        }

        public void Move(Vector3d direction)
        {
            float step = 0.5f;

            Vector3d vector = direction - Center;
            Center.X += vector.X * (float)step;
            //cameraEye.Y += vector.Y * (float)step;
            Center.Z += vector.Z * (float)step;
        }

        public bool ShootTest(Laser laser)
        {
            double dist = Geometry.linePointDist(laser.A, laser.B, Center, true);

            return dist < radius;
        }

        public void Die()
        {
            color = Color.White;
        }
    }
}
