using System;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Mood
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            simpleOpenGlControl1.InitializeContexts();

            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Glut.glutInit();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            // Limpa com a cor especificada.
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            //Glu.gluLookAt(0, 0, 0, 1, 1, 1, 1, 1, 1);
            Gl.glBegin(Gl.GL_TRIANGLES);
            {
                Gl.glVertex3f(0.0f, 1.0f, -1.0f);
                Gl.glVertex3f(-1.0f, -1.0f, -1.0f);
                Gl.glVertex3f(1.0f, -1.0f, -1.0f);
            }
            Gl.glEnd();
        }

        private void simpleOpenGlControl1_Resize(object sender, EventArgs e)
        {
            // O viewport é toda a janela
            Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
            // Modo de projeção, visualização final.
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // Matrix identidade.
            Gl.glLoadIdentity();
            // Faz o frustum, isto é, corta as partes da imagem que não são vistas.
            Gl.glFrustum(1, -1, -1, 1, 0, 20.0);
            // Volta pro modo de criação de objetos.
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }
    }
}
