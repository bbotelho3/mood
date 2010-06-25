using System.Drawing;
using Tao.OpenGl;

namespace Mood
{
    public class Wall : WorldObject, IHitable
    {
        public Vector3d A { get; set; }
        public Vector3d B { get; set; }
        public Vector3d C { get; set; }
        public Vector3d D { get; set; }

        public double Height { get; set; }

        private Color color;

        private Texture texture;

        public Wall(Vector3d a, Vector3d b, Vector3d c, Vector3d d)
        {
            A = a;
            B = b;
            C = c;
            D = d;

            Height = 1d;

            color = Color.White;

            BoundingBox = new BoundingBox(A, B, C, D);
        }

        public Wall(Vector3d a, Vector3d b, Vector3d c, Vector3d d, Color color)
            : this(a, b, c, d)
        {
            this.color = color;
        }

        public Wall(Vector3d a, Vector3d b, Vector3d c, Vector3d d, Texture texture)
            : this(a, b, c, d)
        {
            this.texture = texture;
        }

        public BoundingBox BoundingBox { get; set; }

        public bool HitTest(IHitable obj)
        {
            bool hit = Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.C, obj.BoundingBox.A, true) < 0.1d;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.C, obj.BoundingBox.B, true) < 0.1d;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.C, obj.BoundingBox.C, true) < 0.1d;
            hit = hit || Geometry.LinePointDistance(this.BoundingBox.A, this.BoundingBox.C, obj.BoundingBox.D, true) < 0.1d;
            //double dist = Geometry.LinePointDistance(A, C, obj.GetPosition(), true);
            return hit;
        }

        public override void Draw()
        {
            if (texture == null)
            {
                DrawColor();
            }
            else
            {
                DrawTexture();
            }

        }

        public void DrawTexture()
        {
            Gl.glPushMatrix();
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.id);
            Gl.glColor3f(Color.White.R, Color.White.G, Color.White.B);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3f(A.X, A.Y, A.Z);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3f(B.X, B.Y, B.Z);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3f(C.X, C.Y, C.Z);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3f(D.X, D.Y, D.Z);
            Gl.glEnd();
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
            Gl.glPopMatrix();
        }

        public void DrawColor()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(color.R, color.G, color.B);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3f(A.X, A.Y, A.Z);
            Gl.glVertex3f(B.X, B.Y, B.Z);
            Gl.glVertex3f(C.X, C.Y, C.Z);
            Gl.glVertex3f(D.X, D.Y, D.Z);
            Gl.glEnd();
            Gl.glPopMatrix();
        }
    }
}
