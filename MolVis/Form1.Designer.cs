namespace MolVis
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movieManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.setSizesOfAtomsAndBondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setColorOfAtomsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.regimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAndDeleteToolStripMenuItem = new MolVis.ToolStripRadioButtonMenuItem();
            this.rotateAndMoveToolStripMenuItem = new MolVis.ToolStripRadioButtonMenuItem();
            this.typeOfAtomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hToolStripMenuItem = new MolVis.ToolStripRadioButtonMenuItem();
            this.cToolStripMenuItem = new MolVis.ToolStripRadioButtonMenuItem();
            this.oToolStripMenuItem = new MolVis.ToolStripRadioButtonMenuItem();
            this.oToolStripMenuItem2 = new MolVis.ToolStripRadioButtonMenuItem();
            this.relaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.method1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startRelaxingDllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ant = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.textBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarFrame = new System.Windows.Forms.TrackBar();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.setManager = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.frameForwardManager = new System.Windows.Forms.Button();
            this.stopManager = new System.Windows.Forms.Button();
            this.pauseManager = new System.Windows.Forms.Button();
            this.playManager = new System.Windows.Forms.Button();
            this.frameBackManager = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setColorOfAtomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerRelaxing = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.regimeToolStripMenuItem,
            this.typeOfAtomToolStripMenuItem,
            this.relaxToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(690, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creatToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // creatToolStripMenuItem
            // 
            this.creatToolStripMenuItem.Name = "creatToolStripMenuItem";
            this.creatToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.creatToolStripMenuItem.Text = "Create";
            this.creatToolStripMenuItem.Click += new System.EventHandler(this.creatToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.openFileToolStripMenuItem.Text = "Open file";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.saveFileToolStripMenuItem.Text = "Save file";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numbersToolStripMenuItem,
            this.movieManagerToolStripMenuItem,
            this.toolStripMenuItem2,
            this.setSizesOfAtomsAndBondsToolStripMenuItem,
            this.setColorOfAtomsToolStripMenuItem1});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(58, 25);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // numbersToolStripMenuItem
            // 
            this.numbersToolStripMenuItem.CheckOnClick = true;
            this.numbersToolStripMenuItem.Name = "numbersToolStripMenuItem";
            this.numbersToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.numbersToolStripMenuItem.Text = "Numbers";
            this.numbersToolStripMenuItem.Click += new System.EventHandler(this.numbersToolStripMenuItem_Click);
            // 
            // movieManagerToolStripMenuItem
            // 
            this.movieManagerToolStripMenuItem.Name = "movieManagerToolStripMenuItem";
            this.movieManagerToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.movieManagerToolStripMenuItem.Text = "Movie manager";
            this.movieManagerToolStripMenuItem.Click += new System.EventHandler(this.movieManagerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(294, 6);
            // 
            // setSizesOfAtomsAndBondsToolStripMenuItem
            // 
            this.setSizesOfAtomsAndBondsToolStripMenuItem.Name = "setSizesOfAtomsAndBondsToolStripMenuItem";
            this.setSizesOfAtomsAndBondsToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.setSizesOfAtomsAndBondsToolStripMenuItem.Text = "Set sizes of atoms and bonds";
            this.setSizesOfAtomsAndBondsToolStripMenuItem.Click += new System.EventHandler(this.setSizesOfAtomsAndBondsToolStripMenuItem_Click);
            // 
            // setColorOfAtomsToolStripMenuItem1
            // 
            this.setColorOfAtomsToolStripMenuItem1.Name = "setColorOfAtomsToolStripMenuItem1";
            this.setColorOfAtomsToolStripMenuItem1.Size = new System.Drawing.Size(297, 26);
            this.setColorOfAtomsToolStripMenuItem1.Text = "Set color of atoms";
            this.setColorOfAtomsToolStripMenuItem1.Click += new System.EventHandler(this.setColorOfAtomsToolStripMenuItem_Click);
            // 
            // regimeToolStripMenuItem
            // 
            this.regimeToolStripMenuItem.Checked = true;
            this.regimeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.regimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAndDeleteToolStripMenuItem,
            this.rotateAndMoveToolStripMenuItem});
            this.regimeToolStripMenuItem.Name = "regimeToolStripMenuItem";
            this.regimeToolStripMenuItem.Size = new System.Drawing.Size(78, 25);
            this.regimeToolStripMenuItem.Text = "Regime";
            // 
            // addAndDeleteToolStripMenuItem
            // 
            this.addAndDeleteToolStripMenuItem.CheckOnClick = true;
            this.addAndDeleteToolStripMenuItem.Name = "addAndDeleteToolStripMenuItem";
            this.addAndDeleteToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.addAndDeleteToolStripMenuItem.Text = "Add and delete";
            this.addAndDeleteToolStripMenuItem.Click += new System.EventHandler(this.addAndDeleteToolStripMenuItem_Click);
            // 
            // rotateAndMoveToolStripMenuItem
            // 
            this.rotateAndMoveToolStripMenuItem.Checked = true;
            this.rotateAndMoveToolStripMenuItem.CheckOnClick = true;
            this.rotateAndMoveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rotateAndMoveToolStripMenuItem.Name = "rotateAndMoveToolStripMenuItem";
            this.rotateAndMoveToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.rotateAndMoveToolStripMenuItem.Text = "Rotate and move";
            this.rotateAndMoveToolStripMenuItem.Click += new System.EventHandler(this.rotateAndMoveToolStripMenuItem_Click);
            // 
            // typeOfAtomToolStripMenuItem
            // 
            this.typeOfAtomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hToolStripMenuItem,
            this.cToolStripMenuItem,
            this.oToolStripMenuItem,
            this.oToolStripMenuItem2});
            this.typeOfAtomToolStripMenuItem.Name = "typeOfAtomToolStripMenuItem";
            this.typeOfAtomToolStripMenuItem.Size = new System.Drawing.Size(120, 25);
            this.typeOfAtomToolStripMenuItem.Text = "Type of atom";
            // 
            // hToolStripMenuItem
            // 
            this.hToolStripMenuItem.Checked = true;
            this.hToolStripMenuItem.CheckOnClick = true;
            this.hToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hToolStripMenuItem.Name = "hToolStripMenuItem";
            this.hToolStripMenuItem.Size = new System.Drawing.Size(92, 26);
            this.hToolStripMenuItem.Text = "H";
            this.hToolStripMenuItem.Click += new System.EventHandler(this.hToolStripMenuItem_Click);
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.CheckOnClick = true;
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(92, 26);
            this.cToolStripMenuItem.Text = "C";
            this.cToolStripMenuItem.Click += new System.EventHandler(this.cToolStripMenuItem_Click);
            // 
            // oToolStripMenuItem
            // 
            this.oToolStripMenuItem.CheckOnClick = true;
            this.oToolStripMenuItem.Name = "oToolStripMenuItem";
            this.oToolStripMenuItem.Size = new System.Drawing.Size(92, 26);
            this.oToolStripMenuItem.Text = "N";
            this.oToolStripMenuItem.Click += new System.EventHandler(this.nToolStripMenuItem_Click);
            // 
            // oToolStripMenuItem2
            // 
            this.oToolStripMenuItem2.CheckOnClick = true;
            this.oToolStripMenuItem2.Name = "oToolStripMenuItem2";
            this.oToolStripMenuItem2.Size = new System.Drawing.Size(92, 26);
            this.oToolStripMenuItem2.Text = "O";
            this.oToolStripMenuItem2.Click += new System.EventHandler(this.oToolStripMenuItem_Click);
            // 
            // relaxToolStripMenuItem
            // 
            this.relaxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.method1ToolStripMenuItem,
            this.startRelaxingDllToolStripMenuItem});
            this.relaxToolStripMenuItem.Name = "relaxToolStripMenuItem";
            this.relaxToolStripMenuItem.Size = new System.Drawing.Size(63, 25);
            this.relaxToolStripMenuItem.Text = "Relax";
            // 
            // method1ToolStripMenuItem
            // 
            this.method1ToolStripMenuItem.Name = "method1ToolStripMenuItem";
            this.method1ToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.method1ToolStripMenuItem.Text = "Start relaxing exe";
            this.method1ToolStripMenuItem.Click += new System.EventHandler(this.startRelaxingToolStripMenuItem_Click);
            // 
            // startRelaxingDllToolStripMenuItem
            // 
            this.startRelaxingDllToolStripMenuItem.Name = "startRelaxingDllToolStripMenuItem";
            this.startRelaxingDllToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.startRelaxingDllToolStripMenuItem.Text = "Start relaxing dll";
            this.startRelaxingDllToolStripMenuItem.Click += new System.EventHandler(this.startRelaxingDllToolStripMenuItem_Click);
            // 
            // ant
            // 
            this.ant.AccumBits = ((byte)(0));
            this.ant.AutoCheckErrors = false;
            this.ant.AutoFinish = false;
            this.ant.AutoMakeCurrent = true;
            this.ant.AutoSwapBuffers = true;
            this.ant.BackColor = System.Drawing.Color.Black;
            this.ant.ColorBits = ((byte)(32));
            this.ant.DepthBits = ((byte)(16));
            this.ant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ant.Location = new System.Drawing.Point(0, 29);
            this.ant.Name = "ant";
            this.ant.Size = new System.Drawing.Size(690, 633);
            this.ant.StencilBits = ((byte)(0));
            this.ant.TabIndex = 1;
            this.ant.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ant_MouseDown);
            this.ant.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ant_MouseMove);
            this.ant.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ant_MouseUp);
            // 
            // textBox
            // 
            this.textBox.AcceptsReturn = true;
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox.Location = new System.Drawing.Point(330, 29);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(360, 132);
            this.textBox.TabIndex = 3;
            this.textBox.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBarFrame);
            this.groupBox1.Controls.Add(this.trackBarSpeed);
            this.groupBox1.Controls.Add(this.setManager);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.frameForwardManager);
            this.groupBox1.Controls.Add(this.stopManager);
            this.groupBox1.Controls.Add(this.pauseManager);
            this.groupBox1.Controls.Add(this.playManager);
            this.groupBox1.Controls.Add(this.frameBackManager);
            this.groupBox1.Location = new System.Drawing.Point(0, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 255);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Movie manager";
            this.groupBox1.Visible = false;
            // 
            // trackBarFrame
            // 
            this.trackBarFrame.Location = new System.Drawing.Point(6, 63);
            this.trackBarFrame.Name = "trackBarFrame";
            this.trackBarFrame.Size = new System.Drawing.Size(280, 64);
            this.trackBarFrame.TabIndex = 8;
            this.trackBarFrame.Scroll += new System.EventHandler(this.trackBarFrame_Scroll);
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(31, 185);
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(104, 64);
            this.trackBarSpeed.TabIndex = 7;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            // 
            // setManager
            // 
            this.setManager.Location = new System.Drawing.Point(156, 133);
            this.setManager.Name = "setManager";
            this.setManager.Size = new System.Drawing.Size(51, 35);
            this.setManager.TabIndex = 6;
            this.setManager.Text = "set";
            this.setManager.UseVisualStyleBackColor = true;
            this.setManager.Click += new System.EventHandler(this.setManager_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(31, 133);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(104, 35);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frameForwardManager
            // 
            this.frameForwardManager.Location = new System.Drawing.Point(236, 25);
            this.frameForwardManager.Name = "frameForwardManager";
            this.frameForwardManager.Size = new System.Drawing.Size(50, 32);
            this.frameForwardManager.TabIndex = 4;
            this.frameForwardManager.Text = ">>";
            this.frameForwardManager.UseVisualStyleBackColor = true;
            this.frameForwardManager.Click += new System.EventHandler(this.frameForwardManager_Click);
            // 
            // stopManager
            // 
            this.stopManager.Location = new System.Drawing.Point(153, 25);
            this.stopManager.Name = "stopManager";
            this.stopManager.Size = new System.Drawing.Size(77, 32);
            this.stopManager.TabIndex = 3;
            this.stopManager.Text = "stop";
            this.stopManager.UseVisualStyleBackColor = true;
            this.stopManager.Click += new System.EventHandler(this.stopManager_Click);
            // 
            // pauseManager
            // 
            this.pauseManager.Location = new System.Drawing.Point(108, 25);
            this.pauseManager.Name = "pauseManager";
            this.pauseManager.Size = new System.Drawing.Size(39, 32);
            this.pauseManager.TabIndex = 2;
            this.pauseManager.Text = "||";
            this.pauseManager.UseVisualStyleBackColor = true;
            this.pauseManager.Click += new System.EventHandler(this.pauseManager_Click);
            // 
            // playManager
            // 
            this.playManager.Location = new System.Drawing.Point(63, 25);
            this.playManager.Name = "playManager";
            this.playManager.Size = new System.Drawing.Size(39, 32);
            this.playManager.TabIndex = 1;
            this.playManager.Text = ">";
            this.playManager.UseVisualStyleBackColor = true;
            this.playManager.Click += new System.EventHandler(this.playManager_Click);
            // 
            // frameBackManager
            // 
            this.frameBackManager.Location = new System.Drawing.Point(6, 25);
            this.frameBackManager.Name = "frameBackManager";
            this.frameBackManager.Size = new System.Drawing.Size(50, 32);
            this.frameBackManager.TabIndex = 0;
            this.frameBackManager.Text = "<<";
            this.frameBackManager.UseVisualStyleBackColor = true;
            this.frameBackManager.Click += new System.EventHandler(this.frameBackManager_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setColorOfAtomToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(208, 30);
            // 
            // setColorOfAtomToolStripMenuItem
            // 
            this.setColorOfAtomToolStripMenuItem.Name = "setColorOfAtomToolStripMenuItem";
            this.setColorOfAtomToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.setColorOfAtomToolStripMenuItem.Text = "Set color of atom";
            this.setColorOfAtomToolStripMenuItem.Click += new System.EventHandler(this.setColorOfAtomToolStripMenuItem_Click);
            // 
            // timerRelaxing
            // 
            this.timerRelaxing.Tick += new System.EventHandler(this.timerRelaxing_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 662);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.ant);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(360, 360);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Molecule visualizator";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numbersToolStripMenuItem;
        private Tao.Platform.Windows.SimpleOpenGlControl ant;
        private System.Windows.Forms.ToolStripMenuItem creatToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolStripMenuItem regimeToolStripMenuItem;
        private ToolStripRadioButtonMenuItem addAndDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relaxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem method1ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button frameForwardManager;
        private System.Windows.Forms.Button stopManager;
        private System.Windows.Forms.Button pauseManager;
        private System.Windows.Forms.Button playManager;
        private System.Windows.Forms.Button frameBackManager;
        private System.Windows.Forms.ToolStripMenuItem movieManagerToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        private System.Windows.Forms.Button setManager;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TrackBar trackBarFrame;
        private System.Windows.Forms.ToolStripMenuItem startRelaxingDllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setColorOfAtomToolStripMenuItem;
        private System.Windows.Forms.Timer timerRelaxing;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem setSizesOfAtomsAndBondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setColorOfAtomsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem typeOfAtomToolStripMenuItem;
        private ToolStripRadioButtonMenuItem hToolStripMenuItem;
        private ToolStripRadioButtonMenuItem cToolStripMenuItem;
        private ToolStripRadioButtonMenuItem oToolStripMenuItem;
        private ToolStripRadioButtonMenuItem oToolStripMenuItem2;
        private ToolStripRadioButtonMenuItem rotateAndMoveToolStripMenuItem;
    }
}

