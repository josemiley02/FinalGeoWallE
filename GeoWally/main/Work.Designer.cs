namespace Geo_Wall_E
{
    partial class Work
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Work));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Compile = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.saveFileArchive = new System.Windows.Forms.SaveFileDialog();
            this.LoadButton = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.Right = new System.Windows.Forms.PictureBox();
            this.Down = new System.Windows.Forms.PictureBox();
            this.Left = new System.Windows.Forms.PictureBox();
            this.Up = new System.Windows.Forms.PictureBox();
            this.lienzo = new System.Windows.Forms.PictureBox();
            this.Eve = new System.Windows.Forms.PictureBox();
            this.Wall_E = new System.Windows.Forms.PictureBox();
            this.numberDegub = new System.Windows.Forms.TextBox();
            this.DebugButton = new System.Windows.Forms.Button();
            this.lineCounter = new System.Windows.Forms.TextBox();
            this.ErrorShow = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Up)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lienzo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Eve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wall_E)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Myanmar Text", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(173, 615);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // Compile
            // 
            this.Compile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Compile.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Compile.Image = ((System.Drawing.Image)(resources.GetObject("Compile.Image")));
            this.Compile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Compile.Location = new System.Drawing.Point(248, 4);
            this.Compile.Name = "Compile";
            this.Compile.Size = new System.Drawing.Size(182, 41);
            this.Compile.TabIndex = 2;
            this.Compile.Text = "Compile and Run";
            this.Compile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Compile.UseVisualStyleBackColor = true;
            this.Compile.Click += new System.EventHandler(this.Button1_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.Location = new System.Drawing.Point(842, 4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(162, 41);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save Project";
            this.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.Button2_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadButton.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadButton.Image")));
            this.LoadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoadButton.Location = new System.Drawing.Point(1010, 4);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(162, 41);
            this.LoadButton.TabIndex = 5;
            this.LoadButton.Text = "Load Project";
            this.LoadButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.Button3_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            // 
            // Right
            // 
            this.Right.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Right.Image = global::Geo_Wall_E.Properties.Resources.DERECHA;
            this.Right.Location = new System.Drawing.Point(1178, 314);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(36, 49);
            this.Right.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Right.TabIndex = 10;
            this.Right.TabStop = false;
            this.Right.Click += new System.EventHandler(this.PictureBox4_Click);
            // 
            // Down
            // 
            this.Down.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Down.Image = global::Geo_Wall_E.Properties.Resources.ABAJO;
            this.Down.Location = new System.Drawing.Point(706, 633);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(47, 41);
            this.Down.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Down.TabIndex = 9;
            this.Down.TabStop = false;
            this.Down.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // Left
            // 
            this.Left.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Left.Image = global::Geo_Wall_E.Properties.Resources.IZQUIERDA;
            this.Left.Location = new System.Drawing.Point(206, 314);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(36, 49);
            this.Left.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Left.TabIndex = 8;
            this.Left.TabStop = false;
            this.Left.Click += new System.EventHandler(this.Left_Click);
            // 
            // Up
            // 
            this.Up.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Up.Image = global::Geo_Wall_E.Properties.Resources.ARRIBA;
            this.Up.Location = new System.Drawing.Point(706, 4);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(47, 41);
            this.Up.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Up.TabIndex = 7;
            this.Up.TabStop = false;
            this.Up.Click += new System.EventHandler(this.Up_Click);
            // 
            // lienzo
            // 
            this.lienzo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lienzo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lienzo.Location = new System.Drawing.Point(248, 51);
            this.lienzo.Name = "lienzo";
            this.lienzo.Size = new System.Drawing.Size(924, 576);
            this.lienzo.TabIndex = 0;
            this.lienzo.TabStop = false;
            // 
            // Eve
            // 
            this.Eve.BackgroundImage = global::Geo_Wall_E.Properties.Resources.pngwing_com__4_;
            this.Eve.Location = new System.Drawing.Point(1178, 539);
            this.Eve.Name = "Eve";
            this.Eve.Size = new System.Drawing.Size(160, 160);
            this.Eve.TabIndex = 6;
            this.Eve.TabStop = false;
            // 
            // Wall_E
            // 
            this.Wall_E.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Wall_E.BackgroundImage")));
            this.Wall_E.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Wall_E.Location = new System.Drawing.Point(1178, 12);
            this.Wall_E.Name = "Wall_E";
            this.Wall_E.Size = new System.Drawing.Size(160, 160);
            this.Wall_E.TabIndex = 3;
            this.Wall_E.TabStop = false;
            // 
            // numberDegub
            // 
            this.numberDegub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numberDegub.Location = new System.Drawing.Point(436, 16);
            this.numberDegub.Multiline = true;
            this.numberDegub.Name = "numberDegub";
            this.numberDegub.Size = new System.Drawing.Size(34, 20);
            this.numberDegub.TabIndex = 0;
            // 
            // DebugButton
            // 
            this.DebugButton.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugButton.Image = ((System.Drawing.Image)(resources.GetObject("DebugButton.Image")));
            this.DebugButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DebugButton.Location = new System.Drawing.Point(476, 4);
            this.DebugButton.Name = "DebugButton";
            this.DebugButton.Size = new System.Drawing.Size(182, 41);
            this.DebugButton.TabIndex = 1;
            this.DebugButton.Text = "Debug";
            this.DebugButton.UseVisualStyleBackColor = true;
            this.DebugButton.Click += new System.EventHandler(this.DebugButton_Click);
            // 
            // lineCounter
            // 
            this.lineCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lineCounter.Location = new System.Drawing.Point(182, 12);
            this.lineCounter.Multiline = true;
            this.lineCounter.Name = "lineCounter";
            this.lineCounter.Size = new System.Drawing.Size(18, 615);
            this.lineCounter.TabIndex = 11;
            // 
            // ErrorShow
            // 
            this.ErrorShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorShow.ForeColor = System.Drawing.Color.Red;
            this.ErrorShow.Location = new System.Drawing.Point(1178, 194);
            this.ErrorShow.Multiline = true;
            this.ErrorShow.Name = "ErrorShow";
            this.ErrorShow.Size = new System.Drawing.Size(160, 94);
            this.ErrorShow.TabIndex = 12;
            // 
            // Work
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.ErrorShow);
            this.Controls.Add(this.lineCounter);
            this.Controls.Add(this.numberDegub);
            this.Controls.Add(this.DebugButton);
            this.Controls.Add(this.Right);
            this.Controls.Add(this.Down);
            this.Controls.Add(this.Left);
            this.Controls.Add(this.Up);
            this.Controls.Add(this.lienzo);
            this.Controls.Add(this.Eve);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Wall_E);
            this.Controls.Add(this.Compile);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Work";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GEO-Wall E";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Up)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lienzo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Eve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wall_E)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox lienzo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Compile;
        private System.Windows.Forms.PictureBox Wall_E;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.SaveFileDialog saveFileArchive;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.PictureBox Eve;
        private System.Windows.Forms.PictureBox Up;
        private System.Windows.Forms.PictureBox Left;
        private System.Windows.Forms.PictureBox Down;
        private System.Windows.Forms.PictureBox Right;
        private System.Windows.Forms.TextBox numberDegub;
        private System.Windows.Forms.Button DebugButton;
        private System.Windows.Forms.TextBox lineCounter;
        private System.Windows.Forms.TextBox ErrorShow;
    }
}

