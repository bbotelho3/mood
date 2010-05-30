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
        int x = 1;
        int y = 1;

        int perspective = 100;

        public Form1()
        {
            InitializeComponent();

            simpleOpenGlControl1.InitializeContexts();

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            //Gl.glShadeModel(Gl.GL_FLAT);
            Glut.glutInit();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glLoadIdentity();             /* clear the matrix */
            /* viewing transformation  */
            Glu.gluLookAt(x, y, 3.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
            Gl.glScalef(1.0f, 1.0f, 1.0f);      /* modeling transformation */
            Gl.glTranslatef(0, 2f, 0);
            Glut.glutWireCube(1.0f);

            //Glut.glutSolidTeapot(1.0f);
            Gl.glLoadIdentity();
            Glu.gluLookAt(x, y, 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
            Gl.glColor3f(1.0f, 0, 0);
            Gl.glScalef(5.0f, 5.0f, 5.0f);

            Gl.glBegin(Gl.GL_QUADS);
            {
                Gl.glVertex3f(0.0f, 0.0f, 0.0f);
                Gl.glVertex3f(-1.0f, 0f, 0f);
                Gl.glVertex3f(-1.0f, 0f, -1.0f);
                Gl.glVertex3f(0f, 0f, -1f);
            }
            Gl.glEnd();

            Gl.glColor3f(0f, 1, 0);

            Gl.glBegin(Gl.GL_QUADS);
            {
                Gl.glVertex3f(0.0f, 0.0f, 0.0f);
                Gl.glVertex3f(1.0f, 0f, 0f);
                Gl.glVertex3f(1.0f, 0f, -1.0f);
                Gl.glVertex3f(0f, 0f, -1f);
            }
            Gl.glEnd();

            Gl.glFlush();

            resize();
        }

        private void simpleOpenGlControl1_Resize(object sender, EventArgs e)
        {
            resize();
        }

        public void resize()
        {
            Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            //Gl.glFrustum(-1.0, 1.0, -1.0, 1.0, 1.5, 20.0);
            Glu.gluPerspective(perspective, 1.0, 0.0, 20.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }

        private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                perspective -= 10;
                
                simpleOpenGlControl1.Refresh();
            }
            if (e.KeyCode == Keys.S)
            {
                perspective += 10;
                
                simpleOpenGlControl1.Refresh();
            }
            if (e.KeyCode == Keys.A)
            {
                x--;
                y++;
                simpleOpenGlControl1.Refresh();
            }
            if (e.KeyCode == Keys.D)
            {
                x++;
                y--;
                simpleOpenGlControl1.Refresh();
            }
        }
    }
}
