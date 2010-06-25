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
        //private Weapon weapon;

        private enum CameraStyle
        {
            Top = 0,
            FPS = 1
        }

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
            world.AddObject(new Floor(new Vector3d(-3, -1, -3), new Vector3d(-3, -1, 0), new Vector3d(0, -1, 0), new Vector3d(0, -1, -3), new Texture(Resources.Wooden_Floor_01)));
            world.AddObject(new Floor(new Vector3d(0, -1, 0), new Vector3d(0, -1, 3), new Vector3d(3, -1, 3), new Vector3d(3, -1, 0), new Texture(Resources.Wooden_Floor_01)));
            world.AddObject(new Floor(new Vector3d(0, -1, 0), new Vector3d(0, -1, -3), new Vector3d(3, -1, -3), new Vector3d(3, -1, 0), new Texture(Resources.Wooden_Floor_01)));
            world.AddObject(new Floor(new Vector3d(-3, -1, 3), new Vector3d(-3, -1, 0), new Vector3d(0, -1, 0), new Vector3d(0, -1, 3), new Texture(Resources.Wooden_Floor_01)));
            world.AddObject(new Sphere(new Vector3d(1, -0.8f, 1), 0.5d, Color.Blue));

            weapons = new Weapon[10];
            weapons[0] = new Weapon(0.001f, Resources.CrowBar);// new Weapon(1, Resources.CrowBar);
            weapons[1] = new Weapon(20
                , Resources.Pistol); //new Weapon(10, Resources.Pistol);
            selectedWeapon = 1;
            player = new Player();
            fpsCamera = new FPSCamera();
            topCamera = new TopCamera();

            SetProjection();
        }

        private void ogl_Paint(object sender, PaintEventArgs e)
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();

            switch (this.cameraStyle)
            {
                case CameraStyle.FPS:
                    fpsCamera.Render();
                    world.Draw();
                    weapons[selectedWeapon].Draw(this.Width, this.Height);
                    break;
                
                case  CameraStyle.Top:
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
            Glu.gluPerspective(100, 1.0, 0.0, 20.0);
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
            Vector3d lastPosition = new Vector3d(fpsCamera.getCameraEye().X, fpsCamera.getCameraEye().Y, fpsCamera.getCameraEye().Z);
            Vector3d lastDirection = new Vector3d(fpsCamera.getCameraDirection().X, fpsCamera.getCameraDirection().Y, fpsCamera.getCameraDirection().Z);

            if (e.KeyCode == Keys.D0)
            {
                if (this.weapons[0] != null)
                {
                    this.selectedWeapon = 0;
                }
            }

            if (e.KeyCode == Keys.D1)
            {
                if (this.weapons[1] != null)
                {
                    this.selectedWeapon = 1;
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
                Laser laser = new Laser(new Vector3d(fpsCamera.cameraEye), new Vector3d(fpsCamera.cameraDirection), this.weapons[selectedWeapon].Range, Color.Blue);

                world.AddObject(laser);

                world.ShootTest(laser);
            }

            player.Position = fpsCamera.getCameraEye();

            IHitable obj = world.HitTest(player);

            if (obj != null && (!(obj is IShootable) || (obj is IShootable && !(obj as IShootable).IsDead)))
            {
                if (obj is IMoveable)
                {
                    IMoveable objmv = obj as IMoveable;

                    Vector3d oldPosition = new Vector3d(objmv.GetPosition().X, objmv.GetPosition().Y, objmv.GetPosition().Z);

                    Vector3d v = fpsCamera.cameraDirection - lastDirection;

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

                fpsCamera.cameraEye = lastPosition;
                fpsCamera.cameraDirection = lastDirection;
            }

            ogl.Refresh();
        }

        private void ogl_MouseDown(object sender, MouseEventArgs e)
        {
            Laser laser = new Laser(new Vector3d(fpsCamera.cameraEye), new Vector3d(fpsCamera.cameraDirection), this.weapons[selectedWeapon].Range, Color.Blue);

            world.AddObject(laser);

            world.ShootTest(laser);

            ogl.Refresh();
        }
    }
}
