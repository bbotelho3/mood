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
        int perspective = 100;

        Camera camera;

        World world;

        Player player;

        public Form1()
        {
            InitializeComponent();

            bl_CameraPosition.InitializeContexts();

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Glut.glutInit();

            camera = new Camera();

            SetProjection();

            world = new World();

            world.Objects.Add(new Wall(new Vector3d(3, -1, 3), new Vector3d(3, 1, 3), new Vector3d(3, 1, -3), new Vector3d(3, -1, -3), Color.Red));
            world.Objects.Add(new Wall(new Vector3d(-3, -1, -3), new Vector3d(-3, 1, -3), new Vector3d(-3, 1, 3), new Vector3d(-3, -1, 3), Color.Red));
            world.Objects.Add(new Wall(new Vector3d(-3, -1, -3), new Vector3d(-3, 1, -3), new Vector3d(3, 1, -3), new Vector3d(3, -1, -3)));
            world.Objects.Add(new Wall(new Vector3d(3, -1, 3), new Vector3d(3, 1, 3), new Vector3d(-3, 1, 3), new Vector3d(-3, -1, 3)));

            world.Objects.Add(new Sphere(new Vector3d(1, -0.8f, 1), 0.5d, Color.Blue));

            //world.Objects.Add(new Line(new Vector3d(0, 0 ,0), new Vector3d(5, 0, 5)));

            player = new Player();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();

            camera.Render();

            //Gl.glTranslatef(0.0f, 0.8f, 0.0f);
            //Gl.glScalef(3.0f, 1.0f, 3.0f);

            foreach (WorldObject obj in world.Objects)
            {
                obj.Draw();
            }

            Gl.glFlush();

            SetProjection();

            this.lbl_CameraPosition.Text = "Camera Position: " + this.camera.getCameraEye();
            this.lbl_CameraDirection.Text = "Camera Direction: " + this.camera.getCameraDirection();
        }

        private void simpleOpenGlControl1_Resize(object sender, EventArgs e)
        {
            SetProjection();
        }

        public void SetProjection()
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            //Gl.glFrustum(-1.0, 1.0, -1.0, 1.0, 1.5, 20.0);
            Glu.gluPerspective(perspective, 1.0, 0.0, 20.0);
            Gl.glViewport(0, 0, bl_CameraPosition.Width, bl_CameraPosition.Height);
        }

        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            Vector3d lastPosition = new Vector3d(camera.getCameraEye().X, camera.getCameraEye().Y, camera.getCameraEye().Z);
            Vector3d lastDirection = new Vector3d(camera.getCameraDirection().X, camera.getCameraDirection().Y, camera.getCameraDirection().Z);

            if (e.KeyCode == Keys.W)
            {
                camera.MoveFwBw(0.1);
            }
            if (e.KeyCode == Keys.S)
            {
                camera.MoveFwBw(-0.1);
            }
            if (e.KeyCode == Keys.A)
            {
                camera.RotateY(-5);
            }
            if (e.KeyCode == Keys.D)
            {
                camera.RotateY(5);
            }
            if (e.KeyCode == Keys.PageUp)
            {
                camera.RotateX(-5);
            }
            if (e.KeyCode == Keys.PageDown)
            {
                camera.RotateX(5);
            }

            player.Position = camera.getCameraEye();

            WorldObject obj = world.HitTest(player);

            if (obj != null)
            {
                MessageBox.Show("HIT");

                if (obj is IMoveable)
                {
                    IMoveable objmv = obj as IMoveable;

                    Vector3d oldPosition = new Vector3d(objmv.GetPosition().X,objmv.GetPosition().Y,objmv.GetPosition().Z);

                    objmv.Move(camera.cameraDirection);

                    if (world.HitTest(objmv) != null)
                    {
                        objmv.SetPosition(oldPosition);
                        MessageBox.Show("SPHERE HIT");
                    }
                }

                camera.cameraEye = lastPosition;
                camera.cameraDirection = lastDirection;
            }

            bl_CameraPosition.Refresh();
        }

        private void bl_CameraPosition_MouseDown(object sender, MouseEventArgs e)
        {
            world.Objects.Add(new Line(camera.cameraEye, camera.cameraDirection + new Vector3d(5, 0 ,5), Color.Blue));

            bl_CameraPosition.Refresh();
        }
    }
}
