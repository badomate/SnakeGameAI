namespace Snake.SnakeGame
{
    partial class SnakeGameForm
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
            this.components = new System.ComponentModel.Container();
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.m_RestartBtn = new System.Windows.Forms.Button();
            this.scoreLbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_Timer
            // 
            this.m_Timer.Interval = 1;
            this.m_Timer.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // m_RestartBtn
            // 
            this.m_RestartBtn.Location = new System.Drawing.Point(140, 0);
            this.m_RestartBtn.Margin = new System.Windows.Forms.Padding(2);
            this.m_RestartBtn.Name = "m_RestartBtn";
            this.m_RestartBtn.Size = new System.Drawing.Size(125, 34);
            this.m_RestartBtn.TabIndex = 4;
            this.m_RestartBtn.Text = "Restart";
            this.m_RestartBtn.UseVisualStyleBackColor = true;
            this.m_RestartBtn.Click += new System.EventHandler(this.OnRestartBtnClick);
            // 
            // scoreLbl
            // 
            this.scoreLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scoreLbl.AutoSize = true;
            this.scoreLbl.Location = new System.Drawing.Point(1208, 3);
            this.scoreLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.scoreLbl.Name = "scoreLbl";
            this.scoreLbl.Size = new System.Drawing.Size(50, 13);
            this.scoreLbl.TabIndex = 1;
            this.scoreLbl.Text = "Score : 0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictureBox1.Location = new System.Drawing.Point(0, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 512);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 34);
            this.button2.TabIndex = 0;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SnakeGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 674);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.scoreLbl);
            this.Controls.Add(this.m_RestartBtn);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SnakeGameForm";
            this.ShowIcon = false;
            this.Text = "Snake";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer m_Timer;
        private System.Windows.Forms.Button m_RestartBtn;
        private System.Windows.Forms.Label scoreLbl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
    }
}