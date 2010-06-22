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
            //Gl.glGenTextures(textcont, out id);

            bitmap = (Bitmap)Image.FromFile(path);

            LoadTextures();
        }

        public Texture(Bitmap bitmap)
        {
            //Gl.glGenTextures(textcont, out id);

            this.bitmap = bitmap;

            LoadTextures();
        }

        protected bool LoadTextures()
        {
            Bitmap image = null;
            try
            {
                // If the file doesn't exist or can't be found, an ArgumentException is thrown instead of
                // just returning null
                image = bitmap;
            }
            catch (System.ArgumentException)
            {
                //MessageBox.Show("Could not load " + file + ".  Please make sure that Data is a subfolder from where the application is running.", "Error", MessageBoxButtons.OK);
            }
            if (image != null)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                Gl.glGenTextures(1, out id);

                //// Create Nearest Filtered Texture
                //Gl.glBindTexture(Gl.GL_TEXTURE_2D, id);
                //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
                //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
                //Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGB, image.Width, image.Height, 0, Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

                //// Create Linear Filtered Texture
                //Gl.glBindTexture(Gl.GL_TEXTURE_2D, id);
                //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
                //Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGB, image.Width, image.Height, 0, Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

                // Create MipMapped Texture
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
