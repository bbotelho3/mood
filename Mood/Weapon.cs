using System.Drawing;
using Mood.Properties;
using Tao.OpenGl;

namespace Mood
{
    class Weapon
    {
        public float Range { get; private set; }
        private Bitmap wepTexture;

        public Weapon(float range, Bitmap bitmap)
        {
            this.Range = range;
            this.wepTexture = bitmap;
        }

        public void Draw(int width, int height)
        {
            //Gl.glPushMatrix();
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            //Gl.glOrtho(5f, 5f, 5f, 5f, 0f, 20f);
            Glu.gluOrtho2D(0, width, 0, height);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            //Gl.glBlendFunc(Gl.GL_DST_ALPHA, Gl.GL_ONE_MINUS_DST_ALPHA);

            //Gl.glColor3f(Color.Blue.R, Color.Blue.G, Color.Blue.B);

            Bitmap bitmap = this.wepTexture;// Resources.Pistol;

            bitmap = ResizeBitmap(bitmap, (int)(0.35 * width), (int)(0.35 * height));

            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            System.Drawing.Imaging.BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            bitmapdata = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Gl.glRasterPos2f(width / 2, 0);
            Gl.glDrawPixels(bitmap.Width, bitmap.Height, Gl.GL_BGRA_EXT, Gl.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

            bitmap.UnlockBits(bitmapdata);
            bitmap.Dispose();

            Gl.glMatrixMode(Gl.GL_PROJECTION);

            Gl.glLoadIdentity();
            Glu.gluPerspective(100, 1.0, 0.0, 20.0);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            //Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
        }

        public Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }
    }
}
