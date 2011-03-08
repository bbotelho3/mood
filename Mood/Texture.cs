using System.Drawing;
using Tao.OpenGl;

namespace Mood
{
    public class Texture
    {
        Bitmap bitmap;

        public int id;

        public Texture(string path)
        {
            bitmap = (Bitmap)Image.FromFile(path);

            LoadTextures();
        }

        public Texture(Bitmap bitmap)
        {
            this.bitmap = bitmap;

            LoadTextures();
        }

        protected bool LoadTextures()
        {
            Bitmap image = null;
            try
            {
                image = bitmap;
            }
            catch (System.ArgumentException)
            {
                return false;
            }

            if (image != null)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                Gl.glGenTextures(1, out id);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, id);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_NEAREST);
                Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, (int)Gl.GL_RGB, image.Width, image.Height, Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

                image.UnlockBits(bitmapdata);
                image.Dispose();
                return true;
            }
            return false;
        }
    }
}
