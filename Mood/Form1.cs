using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.Platform.Windows;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace Mood
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            simpleOpenGlControl1.InitializeContexts();

            //Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            //Gl.glMatrixMode(Gl.GL_PROJECTION);
            //Gl.glLoadIdentity();
            //Gl.glOrtho(0.0, 1.0, 0.0, 1.0, -1.0, 1.0);
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Gl.glShadeModel(Gl.GL_FLAT);
            Glut.glutInit();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);		// Clear The Screen And The Depth Buffer
            //Gl.glColor3f(1f, 1f, 1f);
            //Gl.glBegin(Gl.GL_TRIANGLES);						// Drawing Using Triangles
            //Gl.glVertex3f(1.0f, 1.0f, 0.0f);				// Top
            //Gl.glVertex3f(0.0f, 0.0f, 0.0f);				// Bottom Left
            //Gl.glVertex3f(1.0f, 0.0f, 0.0f);				// Bottom Right
            //Gl.glEnd();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glLoadIdentity();             /* clear the matrix */
            /* viewing transformation  */
            Glu.gluLookAt(5.0f, 0.0f, 10.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
            Gl.glScalef(1.0f, 2.0f, 1.0f);      /* modeling transformation */
            Glut.glutWireCube(1.0f);
            Gl.glFlush();
        }

        private void simpleOpenGlControl1_Resize(object sender, EventArgs e)
        {
            Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glFrustum(-1.0, 1.0, -1.0, 1.0, 1.5, 20.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }
    }
}
