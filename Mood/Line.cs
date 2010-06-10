using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tao.OpenGl;

namespace Mood
{
    class Line : WorldObject
    {
        public Vector3d A;
        public Vector3d B;
        private Color color;

        public Line(Vector3d a, Vector3d b)
        {
            A = a;
            B = b;

            color = Color.White;
        }

        public Line(Vector3d a, Vector3d b, Color color)
            : this(a, b)
        {
            this.color = color;
        }

        public override void Draw()
        {
            Gl.glPushMatrix();
            Gl.glColor3f(color.R, color.G, color.B);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(A.X, A.Y, A.Z);
            Gl.glVertex3f(B.X, B.Y, B.Z);
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        //// Draws a line between (x1,y1) - (x2,y2) with a start thickness of t1 and
        //// end thickness t2.
        //void DrawLine(float x1, float y1, float x2, float y2, float t1, float t2)
        //{
        //    float angle = Math.Atan2(y2 - y1, x2 - x1);
        //    float t2sina1 = t1 / 2 * Math.Sin(angle);
        //    float t2cosa1 = t1 / 2 * Math.Cos(angle);
        //    float t2sina2 = t2 / 2 * Math.Sin(angle);
        //    float t2cosa2 = t2 / 2 * Math.Cos(angle);

        //    glBegin(GL_TRIANGLES);
        //    glVertex2f(x1 + t2sina1, y1 - t2cosa1);
        //    glVertex2f(x2 + t2sina2, y2 - t2cosa2);
        //    glVertex2f(x2 - t2sina2, y2 + t2cosa2);
        //    glVertex2f(x2 - t2sina2, y2 + t2cosa2);
        //    glVertex2f(x1 - t2sina1, y1 + t2cosa1);
        //    glVertex2f(x1 + t2sina1, y1 - t2cosa1);
        //    glEnd();
        //}

        public override bool HitTest(IMoveable obj)
        {
            return false;
        }
    }
}
