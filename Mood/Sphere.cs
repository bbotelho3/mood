using System.Drawing;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Mood
{
    class Sphere : WorldObject, IMoveable, IHitable, IShootable
    {
        private Vector3d center;
        private double radius;
        private Color color;

        public Sphere(Vector3d center, double radius, Color color)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;

            IsDead = false;

            Vector3d a = new Vector3d(center.X - (float)radius, center.Y, center.Z + (float)radius);
            Vector3d b = new Vector3d(center.X + (float)radius, center.Y, center.Z + (float)radius);
            Vector3d c = new Vector3d(center.X - (float)radius, center.Y, center.Z - (float)radius);
            Vector3d d = new Vector3d(center.X + (float)radius, center.Y, center.Z - (float)radius);

            BoundingBox = new BoundingBox(a, b, c, d);
        }

        public bool HitTest(IHitable obj)
        {
            double tol = 0.05d;

            bool hit = Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.B, obj.BoundingBox.A, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.B, obj.BoundingBox.B, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.B, obj.BoundingBox.C, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.B, obj.BoundingBox.D, true) < tol;

            hit = hit || Geometry.LinePointDistance(this.BoundingBox.C, this.BoundingBox.D, obj.BoundingBox.A, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.C, this.BoundingBox.D, obj.BoundingBox.B, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.C, this.BoundingBox.D, obj.BoundingBox.C, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.C, this.BoundingBox.D, obj.BoundingBox.D, true) < tol;

            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.C, obj.BoundingBox.A, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.C, obj.BoundingBox.B, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.C, obj.BoundingBox.C, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.C, obj.BoundingBox.D, true) < tol;

            hit = hit || Geometry.LinePointDistance(this.BoundingBox.B, this.BoundingBox.D, obj.BoundingBox.A, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.B, this.BoundingBox.D, obj.BoundingBox.B, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.B, this.BoundingBox.D, obj.BoundingBox.C, true) < tol;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.B, this.BoundingBox.D, obj.BoundingBox.D, true) < tol;

            return hit;
        }

        public Vector3d GetPosition()
        {
            return center;
        }

        public void SetPosition(Vector3d position)
        {
            center = position;
        }

        public BoundingBox BoundingBox { get; set; }

        public override void Draw()
        {
            Gl.glPushMatrix();
            Gl.glColor4f(color.R, color.G, color.B, (float)(color.A / 255f));
            Gl.glTranslatef(center.X, center.Y, center.Z);
            Glut.glutSolidSphere(radius, 20, 20);
            Gl.glPopMatrix();
        }

        public void Move(Vector3d direction)
        {
            float step = 0.5f;

            center.X += direction.X * (float)step;
            center.Z += direction.Z * (float)step;

            Vector3d a = new Vector3d(center.X - (float)radius, center.Y, center.Z + (float)radius);
            Vector3d b = new Vector3d(center.X + (float)radius, center.Y, center.Z + (float)radius);
            Vector3d c = new Vector3d(center.X - (float)radius, center.Y, center.Z - (float)radius);
            Vector3d d = new Vector3d(center.X + (float)radius, center.Y, center.Z - (float)radius);

            BoundingBox = new BoundingBox(a, b, c, d);
        }

        public bool ShootTest(Laser laser)
        {
            double dist = Geometry.LinePointDistance(laser.A, laser.B, center, true);

            return dist < radius;
        }

        public void Die()
        {
            color = Color.FromArgb((int)(255 * 0.2f), color.R, color.G, color.B);

            IsDead = true;
        }

        public bool IsDead { get; set; }
    }
}
