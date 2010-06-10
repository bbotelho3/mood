using System.Drawing;
using Tao.OpenGl;

namespace Mood
{
    public class Wall : WorldObject
    {
        public Vector3d A { get; set; }
        public Vector3d B { get; set; }
        public Vector3d C { get; set; }
        public Vector3d D { get; set; }

        public double Height { get; set; }

        private Color color;

        public Wall(Vector3d a, Vector3d b, Vector3d c, Vector3d d)
        {
            A = a;
            B = b;
            C = c;
            D = d;

            Height = 1d;

            color = Color.White;
        }

        public Wall(Vector3d a, Vector3d b, Vector3d c, Vector3d d, Color color)
            : this(a, b, c, d)
        {
            this.color = color;
        }

        public override bool HitTest(IMoveable player)
        {
            double dist = Geometry.linePointDist(A, C, player.GetPosition(), true);

            return dist < 0.1d;
        }

        public override void Draw()
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
