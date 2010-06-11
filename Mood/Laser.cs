﻿using System;
using System.Drawing;
using Tao.OpenGl;

namespace Mood
{
    class Laser : WorldObject
    {
        public Vector3d A;
        public Vector3d B;
        private Color color;

        public float height;

        public Laser(Vector3d a, Vector3d b)
        {
            A = a;
            B = b;

            float step = 2f;

            Vector3d vector = B - A;

            //A.X += vector.X * (float)step;
            //A.Z += vector.Z * (float)step;

            B.X += vector.X * (float)step;
            B.Z += vector.Z * (float)step;

            color = Color.White;

            height = -0.4f;
        }

        public Laser(Vector3d a, Vector3d b, Color color)
            : this(a, b)
        {
            this.color = color;
        }

        public override void Draw()
        {
            //Gl.glPushMatrix();
            //Gl.glColor3f(color.R, color.G, color.B);
            //Gl.glBegin(Gl.GL_LINES);
            //Gl.glVertex3f(A.X, A.Y, A.Z);
            //Gl.glVertex3f(B.X, B.Y, B.Z);
            //Gl.glEnd();
            //Gl.glPopMatrix();


            DrawLine(A.X, A.Z, B.X, B.Z, 0.01f, 0.02f);
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
    }
}