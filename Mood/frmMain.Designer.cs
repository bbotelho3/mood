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
            this.bl_CameraPosition = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.lbl_CameraDirection = new System.Windows.Forms.Label();
            this.lbl_CameraPosition = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bl_CameraPosition
            // 
            this.bl_CameraPosition.AccumBits = ((byte)(0));
            this.bl_CameraPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bl_CameraPosition.AutoCheckErrors = false;
            this.bl_CameraPosition.AutoFinish = false;
            this.bl_CameraPosition.AutoMakeCurrent = true;
            this.bl_CameraPosition.AutoSwapBuffers = true;
            this.bl_CameraPosition.BackColor = System.Drawing.Color.Black;
            this.bl_CameraPosition.ColorBits = ((byte)(32));
            this.bl_CameraPosition.DepthBits = ((byte)(16));
            this.bl_CameraPosition.Location = new System.Drawing.Point(0, 0);
            this.bl_CameraPosition.Name = "bl_CameraPosition";
            this.bl_CameraPosition.Size = new System.Drawing.Size(375, 356);
            this.bl_CameraPosition.StencilBits = ((byte)(0));
            this.bl_CameraPosition.TabIndex = 0;
            this.bl_CameraPosition.Paint += new System.Windows.Forms.PaintEventHandler(this.simpleOpenGlControl1_Paint);
            this.bl_CameraPosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bl_CameraPosition_MouseDown);
            this.bl_CameraPosition.Resize += new System.EventHandler(this.simpleOpenGlControl1_Resize);
            this.bl_CameraPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.simpleOpenGlControl1_KeyDown);
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
            this.Controls.Add(this.bl_CameraPosition);
            this.Name = "frmMain";
            this.Text = "Mood";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl bl_CameraPosition;
        private System.Windows.Forms.Label lbl_CameraDirection;
        private System.Windows.Forms.Label lbl_CameraPosition;

    }
}

