using System;
using System.Drawing;
using System.Windows.Forms;
using Mood.Properties;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Mood
{
    public partial class frmMain : Form
    {
        private Camera camera;
        private World world;
        private Player player;
        private Weapon weapon;

        public frmMain()
        {
            InitializeComponent();

            ogl.InitializeContexts();

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Glut.glutInit();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            world = new World();

            Texture texture = new Texture(Resources.brick1);

            world.AddObject(new Wall(new Vector3d(3, -1, 3), new Vector3d(3, 1, 3), new Vector3d(3, 1, -3), new Vector3d(3, -1, -3), texture));
            world.AddObject(new Wall(new Vector3d(-3, -1, -3), new Vector3d(-3, 1, -3), new Vector3d(-3, 1, 3), new Vector3d(-3, -1, 3), new Texture(Resources.Dock)));
            world.AddObject(new Wall(new Vector3d(3, -1, 3), new Vector3d(3, 1, 3), new Vector3d(-3, 1, 3), new Vector3d(-3, -1, 3), texture));
            world.AddObject(new Wall(new Vector3d(-3, -1, -3), new Vector3d(-3, 1, -3), new Vector3d(3, 1, -3), new Vector3d(3, -1, -3), texture));
            world.AddObject(new Floor(new Vector3d(-3, -1, -3), new Vector3d(-3, -1, 0), new Vector3d(0, -1, 0), new Vector3d(0, -1, -3), texture));
            world.AddObject(new Floor(new Vector3d(0, -1, 0), new Vector3d(0, -1, 3), new Vector3d(3, -1, 3), new Vector3d(3, -1, 0), texture));
            world.AddObject(new Floor(new Vector3d(0, -1, 0), new Vector3d(0, -1, -3), new Vector3d(3, -1, -3), new Vector3d(3, -1, 0), texture));
            world.AddObject(new Floor(new Vector3d(-3, -1, 3), new Vector3d(-3, -1, 0), new Vector3d(0, -1, 0), new Vector3d(0, -1, 3), texture));
            world.AddObject(new Sphere(new Vector3d(1, -0.8f, 1), 0.5d, Color.Blue));

            weapon = new Weapon();
            player = new Player();
            camera = new Camera();

            SetProjection();
        }

        private void ogl_Paint(object sender, PaintEventArgs e)
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();

            camera.Render();

            world.Draw();

            weapon.Draw(this.Width, this.Height);

            Gl.glFlush();

            SetProjection();

            this.lbl_CameraPosition.Text = "Camera Position: " + this.camera.getCameraEye();
            this.lbl_CameraDirection.Text = "Camera Direction: " + this.camera.getCameraDirection();
        }

        private void ogl_Resize(object sender, EventArgs e)
        {
            SetProjection();
        }

        public void SetProjection()
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            //Gl.glFrustum(-1.0, 1.0, -1.0, 1.0, 1.5, 20.0);
            Glu.gluPerspective(100, 1.0, 0.0, 20.0);
            Gl.glViewport(0, 0, ogl.Width, ogl.Height);
        }

        private void ogl_KeyDown(object sender, KeyEventArgs e)
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

            if (e.KeyCode == Keys.P)
            {
                world.AddSphere();
            }

            if (e.KeyCode == Keys.L)
            {
                world.ShowAllLasers = !world.ShowAllLasers;
            }

            if (e.KeyCode == Keys.Space)
            {
                Laser laser = new Laser(new Vector3d(camera.cameraEye), new Vector3d(camera.cameraDirection), Color.Blue);

                world.AddObject(laser);

                world.ShootTest(laser);
            }

            player.Position = camera.getCameraEye();

            IHitable obj = world.HitTest(player);

            if (obj != null && (!(obj is IShootable) || (obj is IShootable && !(obj as IShootable).IsDead)))
            {
                if (obj is IMoveable)
                {
                    IMoveable objmv = obj as IMoveable;

                    Vector3d oldPosition = new Vector3d(objmv.GetPosition().X, objmv.GetPosition().Y, objmv.GetPosition().Z);

                    Vector3d v = camera.cameraDirection - lastDirection;

                    objmv.Move(v);

                    IHitable hit = world.HitTest(objmv);

                    while (hit != null)
                    {
                        if (hit is IMoveable)
                        {
                            (hit as IMoveable).Move(v);

                            objmv = hit as IMoveable;

                            hit = world.HitTest(objmv);
                        }
                        else
                        {
                            objmv.SetPosition(oldPosition);

                            hit = null;
                        }
                    }
                }

                camera.cameraEye = lastPosition;
                camera.cameraDirection = lastDirection;
            }

            ogl.Refresh();
        }

        private void ogl_MouseDown(object sender, MouseEventArgs e)
        {
            Laser laser = new Laser(new Vector3d(camera.cameraEye), new Vector3d(camera.cameraDirection), Color.Blue);

            world.AddObject(laser);

            world.ShootTest(laser);

            ogl.Refresh();
        }
    }
}
