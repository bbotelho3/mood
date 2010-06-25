namespace Mood
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ogl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.lbl_CameraDirection = new System.Windows.Forms.Label();
            this.lbl_CameraPosition = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bl_CameraPosition
            // 
            this.ogl.AccumBits = ((byte)(0));
            this.ogl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ogl.AutoCheckErrors = false;
            this.ogl.AutoFinish = false;
            this.ogl.AutoMakeCurrent = true;
            this.ogl.AutoSwapBuffers = true;
            this.ogl.BackColor = System.Drawing.Color.Black;
            this.ogl.ColorBits = ((byte)(32));
            this.ogl.DepthBits = ((byte)(16));
            this.ogl.Location = new System.Drawing.Point(0, 0);
            this.ogl.Name = "bl_CameraPosition";
            this.ogl.Size = new System.Drawing.Size(375, 356);
            this.ogl.StencilBits = ((byte)(0));
            this.ogl.TabIndex = 0;
            this.ogl.Paint += new System.Windows.Forms.PaintEventHandler(this.ogl_Paint);
            this.ogl.Resize += new System.EventHandler(this.ogl_Resize);
            this.ogl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ogl_KeyDown);
            // 
            // lbl_CameraDirection
            // 
            this.lbl_CameraDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_CameraDirection.AutoSize = true;
            this.lbl_CameraDirection.Location = new System.Drawing.Point(12, 376);
            this.lbl_CameraDirection.Name = "lbl_CameraDirection";
            this.lbl_CameraDirection.Size = new System.Drawing.Size(91, 13);
            this.lbl_CameraDirection.TabIndex = 1;
            this.lbl_CameraDirection.Text = "Camera Direction:";
            // 
            // lbl_CameraPosition
            // 
            this.lbl_CameraPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_CameraPosition.AutoSize = true;
            this.lbl_CameraPosition.Location = new System.Drawing.Point(12, 359);
            this.lbl_CameraPosition.Name = "lbl_CameraPosition";
            this.lbl_CameraPosition.Size = new System.Drawing.Size(86, 13);
            this.lbl_CameraPosition.TabIndex = 2;
            this.lbl_CameraPosition.Text = "Camera Position:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 398);
            this.Controls.Add(this.lbl_CameraPosition);
            this.Controls.Add(this.lbl_CameraDirection);
            this.Controls.Add(this.ogl);
            this.Name = "frmMain";
            this.Text = "Mood";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl ogl;
        private System.Windows.Forms.Label lbl_CameraDirection;
        private System.Windows.Forms.Label lbl_CameraPosition;

    }
}

