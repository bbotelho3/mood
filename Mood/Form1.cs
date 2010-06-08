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
        int rotation = 0;

        Camera camera;

        public Form1()
        {
            InitializeComponent();

            bl_CameraPosition.InitializeContexts();

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            //Gl.glShadeModel(Gl.GL_FLAT);
            Glut.glutInit();
            camera = new Camera();

            //Glut.glutInit(&argc, argv);
            //Glut.glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
            //Glut.glutInitWindowSize(300, 300);
            //Glut.glutCreateWindow("Camera");
            //Camera.Move(F3dVector(0.0, 0.0, 3.0));
            //Camera.MoveForwards(1.0);
            //Glut.glutDisplayFunc(Display);
            //Glut.glutReshapeFunc(reshape);
            //Glut.glutKeyboardFunc(KeyDown);
            //Glut.glutMainLoop();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            //Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            //Gl.glClear(1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();
            camera.Render();
            Gl.glTranslatef(0.0f, 0.8f, 0.0f);

            Gl.glScalef(3.0f, 1.0f, 3.0f);

            float size = 2.0f;
            int LinesX = 30;
            int LinesZ = 30;

            float halfsize = size / 2.0f;
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glPushMatrix();
                Gl.glTranslatef(0.0f, -halfsize, 0.0f);
                DrawNet(size, LinesX, LinesZ);
                Gl.glTranslatef(0.0f, size, 0.0f);
                DrawNet(size, LinesX, LinesZ);
            Gl.glPopMatrix();
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            Gl.glPushMatrix();
                Gl.glTranslatef(-halfsize, 0.0f, 0.0f);
                Gl.glRotatef(90.0f, 0.0f, 0.0f, halfsize);
                DrawNet(size, LinesX, LinesZ);
                Gl.glTranslatef(0.0f, -size, 0.0f);
                DrawNet(size, LinesX, LinesZ);
            Gl.glPopMatrix();
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glPushMatrix();
                Gl.glTranslatef(0.0f, 0.0f, -halfsize);
                Gl.glRotatef(90.0f, halfsize, 0.0f, 0.0f);
                DrawNet(size, LinesX, LinesZ);
                Gl.glTranslatef(0.0f, size, 0.0f);
                DrawNet(size, LinesX, LinesZ);
            Gl.glPopMatrix();

            Gl.glFlush();

            resize();

            this.lbl_CameraPosition.Text = "Camera Position: " + this.camera.getCameraEye();
            this.lbl_CameraDirection.Text = "Camera Direction: " + this.camera.getCameraDirection();


            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            //Gl.glColor3f(1.0f, 1.0f, 1.0f);
            //Gl.glLoadIdentity();             /* clear the matrix */
            ///* viewing transformation  */

            //camera.Render();
            //Glu.gluLookAt(x, y, 3.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
            ////Gl.glRotatef(rotation, 0, 1, 0);
            //Gl.glScalef(1.0f, 1.0f, 1.0f);      /* modeling transformation */
            
            //Gl.glTranslatef(0, 2f, 0);

            ////Glut.glutWireCube(1.0f);

            ////Glut.glutSolidTeapot(1.0f);
            //Gl.glLoadIdentity();
            ////Glu.gluLookAt(x, y, 2.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);
            //Gl.glColor3f(1.0f, 0, 0);
            //Gl.glScalef(5.0f, 5.0f, 5.0f);

            //Gl.glBegin(Gl.GL_QUADS);
            //{
            //    Gl.glVertex3f(0.0f, 0.0f, 0.0f);
            //    Gl.glVertex3f(-1.0f, 0f, 0f);
            //    Gl.glVertex3f(-1.0f, 0f, -1.0f);
            //    Gl.glVertex3f(0f, 0f, -1f);
            //}
            //Gl.glEnd();

            //Gl.glColor3f(0f, 1, 0);

            //Gl.glBegin(Gl.GL_QUADS);
            //{
            //    Gl.glVertex3f(0.0f, 0.0f, 0.0f);
            //    Gl.glVertex3f(1.0f, 0f, 0f);
            //    Gl.glVertex3f(1.0f, 0f, -1.0f);
            //    Gl.glVertex3f(0f, 0f, -1f);
            //}
            //Gl.glEnd();

            //Gl.glFlush();

            //resize();
        }

        private void simpleOpenGlControl1_Resize(object sender, EventArgs e)
        {
            resize();
        }

        public void resize()
        {
            Gl.glViewport(0, 0, bl_CameraPosition.Width, bl_CameraPosition.Height);
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
                camera.MoveFwBw(0.1);
                bl_CameraPosition.Refresh();
            }
            if (e.KeyCode == Keys.S)
            {
                camera.MoveFwBw(-0.1);
                bl_CameraPosition.Refresh();
            }
            if (e.KeyCode == Keys.A)
            {
                camera.RotateY(-0.1);
                bl_CameraPosition.Refresh();
            }
            if (e.KeyCode == Keys.D)
            {
                camera.RotateY(0.1);
                bl_CameraPosition.Refresh();
            }
            if (e.KeyCode == Keys.PageUp)
            {
                //x--;
                //y++;
                //this.rotation++;
                camera.RotateX(0.1);
                bl_CameraPosition.Refresh();
            }
            if (e.KeyCode == Keys.PageDown)
            {
                //x++;
                //y--;
                //this.rotation--;
                camera.RotateX(-0.1);
                bl_CameraPosition.Refresh();
            }
        }

        private void DrawNet(float size, int LinesX, int LinesZ)
        {
            Gl.glBegin(Gl.GL_LINES);
            for (int xc = 0; xc < LinesX; xc++)
            {
                Gl.glVertex3f(-size / 2.0f + xc / (LinesX - 1) * size,
                            0.0f,
                            size / 2.0f);
                Gl.glVertex3f(-size / 2.0f + xc / (LinesX - 1) * size,
                            0.0f,
                            size / -2.0f);
            }
            for (int zc = 0; zc < LinesX; zc++)
            {
                Gl.glVertex3f(size / 2.0f,
                            0.0f,
                            -size / 2.0f + zc / (LinesZ - 1) * size);
                Gl.glVertex3f(size / -2.0f,
                            0.0f,
                            -size / 2.0f + zc / (LinesZ - 1) * size);
            }
            Gl.glEnd();
        }

    }
}
