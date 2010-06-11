using System;
using System.Drawing;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using System.Resources;

namespace Mood
{
    public partial class frmMain : Form
    {
        int perspective = 100;

        Camera camera;

        World world;

        Player player;

        int textcont = 1;

        public frmMain()
        {
            InitializeComponent();

            bl_CameraPosition.InitializeContexts();

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Glut.glutInit();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            //Gl.glEnable(Gl.GL_DEPTH_TEST);

            camera = new Camera();

            SetProjection();

            world = new World();

            world.AddObject(new Wall(new Vector3d(3, -1, 3), new Vector3d(3, 1, 3), new Vector3d(3, 1, -3), new Vector3d(3, -1, -3), Color.Red));
            world.AddObject(new Wall(new Vector3d(-3, -1, -3), new Vector3d(-3, 1, -3), new Vector3d(-3, 1, 3), new Vector3d(-3, -1, 3), Color.Red));
            
            world.AddObject(new Wall(new Vector3d(3, -1, 3), new Vector3d(3, 1, 3), new Vector3d(-3, 1, 3), new Vector3d(-3, -1, 3)));

            world.AddObject(new Wall(new Vector3d(-3, -1, -3), new Vector3d(-3, 1, -3), new Vector3d(3, 1, -3), new Vector3d(3, -1, -3), new Texture(Properties.Resources.brick1, textcont++)));

            world.AddObject(new Sphere(new Vector3d(1, -0.8f, 1), 0.5d, Color.Blue));

            world.AddObject(new Weapon(camera.cameraEye, camera.cameraDirection));

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

            world.Draw();

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
                camera.RotateY(-0.1);
            }

            if (e.KeyCode == Keys.D)
            {
                camera.RotateY(0.1);
            }

            if (e.KeyCode == Keys.PageUp)
            {
                camera.RotateX(0.1);
            }

            if (e.KeyCode == Keys.PageDown)
            {
                camera.RotateX(-0.1);
            }

            if (e.KeyCode == Keys.P)
            {
                SpawnSphere();
            }

            if (e.KeyCode == Keys.L)
            {
                world.ShowLaser = !world.ShowLaser;
            }

            player.Position = camera.getCameraEye();

            IHitable obj = world.HitTest(player);

            if (obj != null)
            {
                //MessageBox.Show("HIT");

                if (obj is IMoveable)
                {
                    IMoveable objmv = obj as IMoveable;

                    Vector3d oldPosition = new Vector3d(objmv.GetPosition().X, objmv.GetPosition().Y, objmv.GetPosition().Z);

                    objmv.Move(camera.cameraDirection);

                    if (world.HitTest(objmv) != null)
                    {
                        objmv.SetPosition(oldPosition);

                        //MessageBox.Show("SPHERE HIT");
                    }
                }

                camera.cameraEye = lastPosition;
                camera.cameraDirection = lastDirection;
            }

            bl_CameraPosition.Refresh();
        }

        private void bl_CameraPosition_MouseDown(object sender, MouseEventArgs e)
        {
            Laser laser = new Laser(new Vector3d(camera.cameraEye), new Vector3d(camera.cameraDirection), Color.Blue);

            world.AddObject(laser);

            world.ShootTest(laser);

            bl_CameraPosition.Refresh();
        }

        private void SpawnSphere()
        {
            Random random = new Random();

            world.AddObject(new Sphere(new Vector3d((float)random.NextDouble() * 6f - 3f, -0.8f, (float)random.NextDouble() * 6f - 3f), 0.5d, Color.Blue));
        }
    }
}
