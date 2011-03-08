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
        private Camera fpsCamera;
        private Camera topCamera;
        private World world;
        private Player player;
        private Weapon[] weapons;
        private int selectedWeapon;
        private CameraStyle cameraStyle = CameraStyle.FPS;

        private enum CameraStyle
        {
            Top = 0,
            FPS = 1
        }

        private enum WeaponType
        {
            Crowbar = 0,
            Pistol = 1
        }

        public frmMain()
        {
            InitializeComponent();

            ogl.InitializeContexts();

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Glut.glutInit();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_BLEND);

            Gl.glDepthMask(Gl.GL_TRUE);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            world = new World();

            Texture brickWall = new Texture(Resources.brick1);
            Texture woodFloor = new Texture(Resources.Wooden_Floor_01);

            world.AddObject(new Wall(new Point3d(3, -1, 3), new Point3d(3, 1, 3), new Point3d(3, 1, -3), new Point3d(3, -1, -3), brickWall));
            world.AddObject(new Wall(new Point3d(-3, -1, -3), new Point3d(-3, 1, -3), new Point3d(-3, 1, 3), new Point3d(-3, -1, 3), new Texture(Resources.Dock)));
            world.AddObject(new Wall(new Point3d(3, -1, 3), new Point3d(3, 1, 3), new Point3d(-3, 1, 3), new Point3d(-3, -1, 3), brickWall));
            world.AddObject(new Wall(new Point3d(-3, -1, -3), new Point3d(-3, 1, -3), new Point3d(3, 1, -3), new Point3d(3, -1, -3), brickWall));
            world.AddObject(new Floor(new Point3d(-3, -1, -3), new Point3d(-3, -1, 0), new Point3d(0, -1, 0), new Point3d(0, -1, -3), woodFloor));
            world.AddObject(new Floor(new Point3d(0, -1, 0), new Point3d(0, -1, 3), new Point3d(3, -1, 3), new Point3d(3, -1, 0), woodFloor));
            world.AddObject(new Floor(new Point3d(0, -1, 0), new Point3d(0, -1, -3), new Point3d(3, -1, -3), new Point3d(3, -1, 0), woodFloor));
            world.AddObject(new Floor(new Point3d(-3, -1, 3), new Point3d(-3, -1, 0), new Point3d(0, -1, 0), new Point3d(0, -1, 3), woodFloor));
            world.AddObject(new Sphere(new Point3d(1, -0.5f, 1), 0.5d, Color.Blue));

            weapons = new Weapon[2];
            weapons[0] = new Weapon(-0.4f, Resources.CrowBar);
            weapons[1] = new Weapon(20, Resources.Pistol);
            
            selectedWeapon = (int)WeaponType.Pistol;

            player = new Player();
            fpsCamera = new FPSCamera();
            topCamera = new TopCamera();

            SetProjection();
        }

        private void ogl_Paint(object sender, PaintEventArgs e)
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();

            switch (this.cameraStyle)
            {
                case CameraStyle.FPS:
                    fpsCamera.Render();
                    world.Draw();
                    weapons[selectedWeapon].Draw(this.Width, this.Height);
                    break;

                case CameraStyle.Top:
                    topCamera.Render();
                    world.Draw();
                    break;
            }

            Gl.glFlush();

            SetProjection();

            this.lbl_CameraPosition.Text = "Camera Position: " + this.fpsCamera.getCameraEye();
            this.lbl_CameraDirection.Text = "Camera Direction: " + this.fpsCamera.getCameraDirection();
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
            Glu.gluPerspective(100, 1.0, 0.1f, 20.0);
            Gl.glViewport(0, 0, ogl.Width, ogl.Height);
        }

        private void treatFpsCamera(Keys key)
        {
            if (key == Keys.W)
            {
                fpsCamera.MoveFwBw(0.1);
            }

            if (key == Keys.S)
            {
                fpsCamera.MoveFwBw(-0.1);
            }

            if (key == Keys.A)
            {
                fpsCamera.RotateY(-5);
            }

            if (key == Keys.D)
            {
                fpsCamera.RotateY(5);
            }

            if (key == Keys.PageUp)
            {
                fpsCamera.RotateX(-5);
            }

            if (key == Keys.PageDown)
            {
                fpsCamera.RotateX(5);
            }
        }

        private void treatTopCamera(Keys key)
        {
            if (key == Keys.W)
            {
                topCamera.MoveFwBw(0.1);
            }

            if (key == Keys.S)
            {
                topCamera.MoveFwBw(-0.1);
            }
        }

        private void ogl_KeyDown(object sender, KeyEventArgs e)
        {
            Point3d lastPosition = new Point3d(fpsCamera.getCameraEye().X, fpsCamera.getCameraEye().Y, fpsCamera.getCameraEye().Z);
            Point3d lastDirection = new Point3d(fpsCamera.getCameraDirection().X, fpsCamera.getCameraDirection().Y, fpsCamera.getCameraDirection().Z);

            if (e.KeyCode == Keys.D1)
            {
                if (this.weapons[0] != null)
                {
                    this.selectedWeapon = (int)WeaponType.Crowbar;
                }
            }

            if (e.KeyCode == Keys.D2)
            {
                if (this.weapons[1] != null)
                {
                    this.selectedWeapon = (int)WeaponType.Pistol;
                }
            }

            switch (this.cameraStyle)
            {
                case CameraStyle.FPS:
                    treatFpsCamera(e.KeyCode);
                    break;

                case CameraStyle.Top:
                    treatTopCamera(e.KeyCode);
                    break;
            }

            if (e.KeyCode == Keys.P)
            {
                world.AddSphere();
            }

            if (e.KeyCode == Keys.L)
            {
                world.ShowAllLasers = !world.ShowAllLasers;
            }

            if (e.KeyCode == Keys.K)
            {
                world.ShowLastLaser = !world.ShowLastLaser;
            }

            if (e.KeyCode == Keys.M)
            {
                if (this.cameraStyle == CameraStyle.FPS)
                {
                    this.cameraStyle = CameraStyle.Top;
                }
                else
                {
                    this.cameraStyle = CameraStyle.FPS;
                }
            }

            if (e.KeyCode == Keys.Space)
            {
                Hit hit = new Hit(new Point3d(fpsCamera.cameraEye), new Point3d(fpsCamera.cameraDirection), this.weapons[selectedWeapon].Range, Color.Blue);

                world.AddObject(hit);

                world.ShootTest(hit);
            }

            player.Position = fpsCamera.getCameraEye();

            IHitable obj = world.HitTest(player);

            if (obj != null && (!(obj is IShootable) || (obj is IShootable && !(obj as IShootable).IsDead)))
            {
                if (obj is IMoveable)
                {
                    Point3d v = fpsCamera.cameraDirection - lastDirection;

                    world.MoveObject(obj as IMoveable, v);
                }

                fpsCamera.cameraEye = lastPosition;
                fpsCamera.cameraDirection = lastDirection;
            }

            ogl.Refresh();
        }
    }
}
