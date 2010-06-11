using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using System.Drawing;

namespace Mood
{
    class Weapon : WorldObject
    {
        public Vector3d A;
        public Vector3d B;

        public float height;

        public Texture texture;

        public Weapon(Vector3d a, Vector3d b)
        {
            A = a;
            B = b;

            height = -0.4f;
        }

        public Weapon(Vector3d a, Vector3d b, Texture texture)
            : this(a, b)
        {
            this.texture = texture;
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

        // Draws a line between (x1,y1) - (x2,y2) with a start thickness of t1 and
        // end thickness t2.
        void DrawLine(float x1, float y1, float x2, float y2, float t1, float t2)
        {
            float angle = (float)Math.Atan2(y2 - y1, x2 - x1);
            float t2sina1 = t1 / 2 * (float)Math.Sin(angle);
            float t2cosa1 = t1 / 2 * (float)Math.Cos(angle);
            float t2sina2 = t2 / 2 * (float)Math.Sin(angle);
            float t2cosa2 = t2 / 2 * (float)Math.Cos(angle);

            Gl.glPushMatrix();
            Gl.glColor3f(Color.Blue.R, Color.Blue.G, Color.Blue.B);
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3f(x1 + t2sina1, height, y1 - t2cosa1);
            Gl.glVertex3f(x2 + t2sina2, height, y2 - t2cosa2);
            Gl.glVertex3f(x2 - t2sina2, height, y2 + t2cosa2);
            Gl.glVertex3f(x2 - t2sina2, height, y2 + t2cosa2);
            Gl.glVertex3f(x1 - t2sina1, height, y1 + t2cosa1);
            Gl.glVertex3f(x1 + t2sina1, height, y1 - t2cosa1);
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        public void DrawColor()
        {
            DrawLine(A.X, A.Z, B.X, B.Z, 0.05f, 0.05f);
        }

        void DrawLineTexture(float x1, float y1, float x2, float y2, float t1, float t2)
        {
            float angle = (float)Math.Atan2(y2 - y1, x2 - x1);
            float t2sina1 = t1 / 2 * (float)Math.Sin(angle);
            float t2cosa1 = t1 / 2 * (float)Math.Cos(angle);
            float t2sina2 = t2 / 2 * (float)Math.Sin(angle);
            float t2cosa2 = t2 / 2 * (float)Math.Cos(angle);

            Gl.glPushMatrix();
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.id);
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3f(x1 + t2sina1, height, y1 - t2cosa1);


            Gl.glVertex3f(x2 + t2sina2, height, y2 - t2cosa2);

            Gl.glVertex3f(x2 - t2sina2, height, y2 + t2cosa2);

            Gl.glVertex3f(x2 - t2sina2, height, y2 + t2cosa2);

            Gl.glVertex3f(x1 - t2sina1, height, y1 + t2cosa1);

            Gl.glVertex3f(x1 + t2sina1, height, y1 - t2cosa1);

            Gl.glEnd();
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
            Gl.glPopMatrix();
        }

        public void DrawTexture()
        {
            DrawLineTexture(A.X, A.Z, B.X, B.Z, 0.05f, 0.05f);
        }
    }
}
